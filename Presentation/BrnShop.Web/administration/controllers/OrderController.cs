using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Admin.Models;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台订单控制器类
    /// </summary>
    public partial class OrderController : BaseAdminController
    {
        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="osn">订单编号</param>
        /// <param name="accountName">账户名</param>
        /// <param name="consignee">收货人</param>
        /// <param name="sortOptions">排序</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        public ActionResult OrderList(string osn, string accountName, string consignee, string sortColumn, string sortDirection, int orderState = 0, int pageSize = 15, int pageNumber = 1)
        {
            //获取用户id
            int uid = Users.GetUidByAccountName(accountName);

            string condition = AdminOrders.GetOrderListCondition(osn, uid, consignee, orderState);
            string sort = AdminOrders.GetOrderListSort(sortColumn, sortDirection);

            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminOrders.GetOrderCount(condition));

            OrderListModel model = new OrderListModel()
            {
                OrderList = AdminOrders.GetOrderList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                PageModel = pageModel,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                OSN = osn,
                AccountName = accountName,
                Consignee = consignee,
                OrderState = orderState
            };
            ShopUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&sortColumn={3}&sortDirection={4}&OSN={5}&AccountName={6}&Consignee={7}&OrderState={8}",
                                                          Url.Action("orderlist"),
                                                          pageModel.PageNumber,
                                                          pageModel.PageSize,
                                                          sortColumn, sortDirection,
                                                          osn, accountName, consignee, orderState));

            List<SelectListItem> itemList = new List<SelectListItem>();
            itemList.Add(new SelectListItem() { Text = "全部", Value = "0" });
            itemList.Add(new SelectListItem() { Text = "已提交", Value = ((int)OrderState.Submitted).ToString() });
            itemList.Add(new SelectListItem() { Text = "等待付款", Value = ((int)OrderState.WaitPaying).ToString() });
            itemList.Add(new SelectListItem() { Text = "待确认", Value = ((int)OrderState.Confirming).ToString() });
            itemList.Add(new SelectListItem() { Text = "已确认", Value = ((int)OrderState.Confirmed).ToString() });
            itemList.Add(new SelectListItem() { Text = "备货中", Value = ((int)OrderState.PreProducting).ToString() });
            itemList.Add(new SelectListItem() { Text = "已发货", Value = ((int)OrderState.Sended).ToString() });
            itemList.Add(new SelectListItem() { Text = "已完成", Value = ((int)OrderState.Completed).ToString() });
            itemList.Add(new SelectListItem() { Text = "已锁定", Value = ((int)OrderState.Locked).ToString() });
            itemList.Add(new SelectListItem() { Text = "已取消", Value = ((int)OrderState.Cancelled).ToString() });
            itemList.Add(new SelectListItem() { Text = "已退货", Value = ((int)OrderState.Returned).ToString() });
            ViewData["orderStateList"] = itemList;

            return View(model);
        }

        /// <summary>
        /// 订单信息
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public ActionResult OrderInfo(int oid = -1)
        {
            OrderInfo orderInfo = AdminOrders.GetOrderByOid(oid);
            if (orderInfo == null)
                return PromptView("订单不存在");

            OrderInfoModel model = new OrderInfoModel();
            model.OrderInfo = orderInfo;
            model.RegionInfo = Regions.GetRegionById(orderInfo.RegionId);
            model.UserInfo = Users.GetUserById(orderInfo.Uid);
            model.UserRankInfo = AdminUserRanks.GetUserRankById(model.UserInfo.UserRid);
            model.OrderProductList = AdminOrders.GetOrderProductList(oid);
            model.OrderActionList = OrderActions.GetOrderActionList(oid);

            string[] sizeList = StringHelper.SplitString(WorkContext.ShopConfig.ProductShowThumbSize);

            ViewData["size"] = sizeList[sizeList.Length / 2];
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 操作订单
        /// </summary>
        [HttpGet]
        public ActionResult OperateOrder(int oid = -1, int actionType = -1)
        {
            OrderInfo orderInfo = AdminOrders.GetOrderByOid(oid);
            if (orderInfo == null)
                return PromptView("订单不存在");

            OrderActionType orderActionType = (OrderActionType)actionType;
            OrderState orderState = (OrderState)orderInfo.OrderState;

            if (orderActionType == OrderActionType.Pay)//支付订单
            {
                if (orderInfo.PayMode != 2)
                    return PromptView(Url.Action("orderinfo", new { oid = oid }), "付款操作只适用于线下付款");

                if (orderState != OrderState.WaitPaying)
                    return PromptView(Url.Action("orderinfo", new { oid = oid }), "当前不能支付订单");
            }
            else if (orderActionType == OrderActionType.Confirm)//确认订单
            {
                if (orderState != OrderState.Confirming)
                    return PromptView(Url.Action("orderinfo", new { oid = oid }), "买家还未付款，不能确认订单");
            }
            else if (orderActionType == OrderActionType.PreProduct)//备货
            {
                if (orderState != OrderState.Confirmed)
                    return PromptView(Url.Action("orderinfo", new { oid = oid }), "订单还未确认，不能备货");
            }
            else if (orderActionType == OrderActionType.Send)//发货
            {
                if (orderState != OrderState.PreProducting)
                    return PromptView(Url.Action("orderinfo", new { oid = oid }), "订单还未备货，不能发货");
            }
            else if (orderActionType == OrderActionType.Complete)//完成订单
            {
                if (orderState != OrderState.Sended)
                    return PromptView(Url.Action("orderinfo", new { oid = oid }), "订单还未发货，不能完成订单");
            }
            else if (orderActionType == OrderActionType.Return)//退货
            {
                if (orderState != OrderState.Sended && orderState != OrderState.Completed)
                    return PromptView(Url.Action("orderinfo", new { oid = oid }), "订单当前不能退货");
            }
            else if (orderActionType == OrderActionType.Lock)//锁定订单
            {
                if (!(orderState == OrderState.WaitPaying || (orderState == OrderState.Confirming && orderInfo.PayMode == 0)))
                    return PromptView(Url.Action("orderinfo", new { oid = oid }), "订单当前不能锁定");
            }
            else if (orderActionType == OrderActionType.Cancel)//取消订单
            {
                if (!(orderState == OrderState.WaitPaying || (orderState == OrderState.Confirming && orderInfo.PayMode == 0)))
                    return PromptView(Url.Action("orderinfo", new { oid = oid }), "订单当前不能取消");
            }
            else
            {
                return PromptView(Url.Action("orderinfo", new { oid = oid }), "当前操作不存在");
            }

            OperateOrderModel model = new OperateOrderModel();
            model.Oid = oid;
            model.OrderInfo = orderInfo;
            model.OrderActionType = orderActionType;
            model.ActionDes = "";

            return View(model);
        }

        /// <summary>
        /// 操作订单
        /// </summary>
        [HttpPost]
        public ActionResult OperateOrder(int oid = -1, int actionType = -1, string actionDes = "")
        {
            OrderInfo orderInfo = AdminOrders.GetOrderByOid(oid);
            if (orderInfo == null)
                return PromptView("订单不存在");

            if (actionDes.Length > 125)
            {
                OperateOrderModel model = new OperateOrderModel();
                model.Oid = oid;
                model.OrderInfo = orderInfo;
                model.OrderActionType = (OrderActionType)actionType;
                model.ActionDes = actionDes;

                ModelState.AddModelError("actionDes", "最多只能输入125个字");
                return View(model);
            }

            OrderActionType orderActionType = (OrderActionType)actionType;
            OrderState orderState = (OrderState)orderInfo.OrderState;

            if (orderActionType == OrderActionType.Pay)//支付订单
            {
                if (orderInfo.PayMode != 2)
                    return PromptView(Url.Action("orderinfo", new { oid = oid }), "付款操作只适用于线下付款");

                if (orderState != OrderState.WaitPaying)
                    return PromptView(Url.Action("orderinfo", new { oid = oid }), "当前不能支付订单");

                string paySN = WebHelper.GetFormString("paySN").Trim();
                if (paySN.Length < 1)
                {
                    OperateOrderModel model = new OperateOrderModel();
                    model.Oid = oid;
                    model.OrderInfo = orderInfo;
                    model.OrderActionType = orderActionType;
                    model.ActionDes = actionDes;

                    ModelState.AddModelError("paySN", "请填写支付单号");
                    return View(model);
                }
                AdminOrders.PayOrder(oid, OrderState.Confirming, paySN, DateTime.Now);
                CreateOrderAction(oid, orderActionType, actionDes.Length == 0 ? "您的订单成功支付" : actionDes);

            }
            else if (orderActionType == OrderActionType.Confirm)//确认订单
            {
                if (orderState != OrderState.Confirming)
                    return PromptView(Url.Action("orderinfo", new { oid = oid }), "买家还未付款，不能确认订单");

                AdminOrders.ConfirmOrder(orderInfo);
                CreateOrderAction(oid, orderActionType, actionDes.Length == 0 ? "您的订单已经确认" : actionDes);
            }
            else if (orderActionType == OrderActionType.PreProduct)//备货
            {
                if (orderState != OrderState.Confirmed)
                    return PromptView(Url.Action("orderinfo", new { oid = oid }), "订单还未确认，不能备货");

                AdminOrders.PreProduct(orderInfo);
                CreateOrderAction(oid, orderActionType, actionDes.Length == 0 ? "您的订单正在备货" : actionDes);
            }
            else if (orderActionType == OrderActionType.Send)//发货
            {
                if (orderState != OrderState.PreProducting)
                    return PromptView(Url.Action("orderinfo", new { oid = oid }), "订单还未备货，不能发货");

                string shipSN = WebHelper.GetFormString("shipSN").Trim();
                if (shipSN.Length < 1)
                {
                    OperateOrderModel model = new OperateOrderModel();
                    model.Oid = oid;
                    model.OrderInfo = orderInfo;
                    model.OrderActionType = orderActionType;
                    model.ActionDes = actionDes;

                    ModelState.AddModelError("shipSN", "请填写配送单号");
                    return View(model);
                }
                AdminOrders.SendOrder(oid, OrderState.Sended, shipSN, DateTime.Now);
                CreateOrderAction(oid, orderActionType, actionDes.Length == 0 ? "您的订单已经发货,单号为：" + shipSN : actionDes);
            }
            else if (orderActionType == OrderActionType.Complete)//完成订单
            {
                if (orderState != OrderState.Sended)
                    return PromptView(Url.Action("orderinfo", new { oid = oid }), "订单还未发货，不能完成订单");

                if (orderInfo.PayMode == 0)
                {
                    string paySN = WebHelper.GetFormString("paySN").Trim();
                    if (paySN.Length < 1)
                    {
                        OperateOrderModel model = new OperateOrderModel();
                        model.Oid = oid;
                        model.OrderInfo = orderInfo;
                        model.OrderActionType = orderActionType;
                        model.ActionDes = actionDes;

                        ModelState.AddModelError("paySN", "请填写支付单号");
                        return View(model);
                    }
                    else
                    {
                        AdminOrders.PayOrder(oid, OrderState.Sended, paySN, DateTime.Now);
                        CreateOrderAction(oid, orderActionType, actionDes.Length == 0 ? "您的订单成功支付" : actionDes);
                    }
                }

                PartUserInfo partUserInfo = Users.GetPartUserById(orderInfo.Uid);
                AdminOrders.CompleteOrder(ref partUserInfo, orderInfo, DateTime.Now, WorkContext.IP);
                CreateOrderAction(oid, orderActionType, actionDes.Length == 0 ? "已完成配送，感谢您在" + WorkContext.ShopConfig.ShopName + "购物，欢迎您再次光临" : actionDes);
            }
            else if (orderActionType == OrderActionType.Return)//退货
            {
                if (orderState != OrderState.Sended && orderState != OrderState.Completed)
                    return PromptView(Url.Action("orderinfo", new { oid = oid }), "订单当前不能退货");

                PartUserInfo partUserInfo = Users.GetPartUserById(orderInfo.Uid);
                AdminOrders.ReturnOrder(ref partUserInfo, orderInfo, WorkContext.Uid, DateTime.Now);
                CreateOrderAction(oid, orderActionType, actionDes.Length == 0 ? "订单已退货" : actionDes);
            }
            else if (orderActionType == OrderActionType.Lock)//锁定订单
            {
                if (!(orderState == OrderState.WaitPaying || (orderState == OrderState.Confirming && orderInfo.PayMode == 0)))
                    return PromptView(Url.Action("orderinfo", new { oid = oid }), "订单当前不能锁定");

                AdminOrders.LockOrder(orderInfo);
                CreateOrderAction(oid, orderActionType, "订单已锁定：" + actionDes);
            }
            else if (orderActionType == OrderActionType.Cancel)//取消订单
            {
                if (!(orderState == OrderState.WaitPaying || (orderState == OrderState.Confirming && orderInfo.PayMode == 0)))
                    return PromptView(Url.Action("orderinfo", new { oid = oid }), "订单当前不能取消");

                PartUserInfo partUserInfo = Users.GetPartUserById(orderInfo.Uid);
                AdminOrders.CancelOrder(ref partUserInfo, orderInfo, WorkContext.Uid, DateTime.Now);
                CreateOrderAction(oid, orderActionType, actionDes.Length == 0 ? "订单已取消" : actionDes);
            }
            else
            {
                return PromptView(Url.Action("orderinfo", new { oid = oid }), "当前操作不存在");
            }

            AddAdminOperateLog("操作订单", "操作订单,订单ID为:" + oid);
            return PromptView(Url.Action("orderinfo", new { oid = oid }), "操作已完成");
        }

        /// <summary>
        /// 打印订单
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public ActionResult PrintOrder(int oid)
        {
            OrderInfo orderInfo = AdminOrders.GetOrderByOid(oid);
            if (orderInfo == null)
                return PromptView("订单不存在");

            PrintOrderModel model = new PrintOrderModel()
            {
                OrderInfo = orderInfo,
                RegionInfo = Regions.GetRegionById(orderInfo.RegionId),
                OrderProductList = AdminOrders.GetOrderProductList(oid),
                AdminRealName = AdminUsers.GetUserDetailById(WorkContext.Uid).RealName
            };

            return View(model);
        }

        /// <summary>
        /// 订单退款列表
        /// </summary>
        /// <param name="osn">订单编号</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        public ActionResult RefundList(string osn, int pageSize = 15, int pageNumber = 1)
        {
            string condition = AdminOrderRefunds.GetOrderRefundListCondition(osn);
            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminOrderRefunds.GetOrderRefundCount(condition));

            OrderRefundListModel model = new OrderRefundListModel()
            {
                OrderRefundList = AdminOrderRefunds.GetOrderRefundList(pageModel.PageSize, pageModel.PageNumber, condition),
                PageModel = pageModel,
                OSN = osn
            };
            ShopUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&OSN={3}",
                                                          Url.Action("refundlist"),
                                                          pageModel.PageNumber,
                                                          pageModel.PageSize,
                                                          osn));


            return View(model);
        }

        private void CreateOrderAction(int oid, OrderActionType orderActionType, string actionDes)
        {
            OrderActions.CreateOrderAction(new OrderActionInfo()
            {
                Oid = oid,
                Uid = WorkContext.Uid,
                RealName = AdminUsers.GetUserDetailById(WorkContext.Uid).RealName,
                AdminGid = WorkContext.AdminGid,
                AdminGTitle = WorkContext.AdminGTitle,
                ActionType = (int)orderActionType,
                ActionTime = DateTime.Now,
                ActionDes = actionDes
            });
        }
    }
}
