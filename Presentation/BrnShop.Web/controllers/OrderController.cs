using System;
using System.Text;
using System.Data;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Models;

namespace BrnShop.Web.Controllers
{
    /// <summary>
    /// 订单控制器类
    /// </summary>
    public partial class OrderController : BaseWebController
    {
        private static object _locker = new object();//锁对象

        /// <summary>
        /// 确认订单
        /// </summary>
        public ActionResult ConfirmOrder()
        {
            //选中的购物车项键列表
            string selectedCartItemKeyList = WebHelper.GetRequestString("selectedCartItemKeyList");

            //订单商品列表
            List<OrderProductInfo> orderProductList = Carts.GetCartProductList(WorkContext.Uid);
            if (orderProductList.Count < 1)
                return PromptView("购物车中没有商品，请先添加商品");

            //选中的订单商品列表
            List<OrderProductInfo> selectedOrderProductList = null;
            //购物车项列表
            List<CartItemInfo> cartItemList = Carts.TidyOrderProductList(StringHelper.SplitString(selectedCartItemKeyList), orderProductList, out selectedOrderProductList);
            if (selectedOrderProductList.Count < 1)
                return PromptView("请先选择购物车商品");

            ConfirmOrderModel model = new ConfirmOrderModel();

            model.SelectedCartItemKeyList = selectedCartItemKeyList;

            model.DefaultFullShipAddressInfo = ShipAddresses.GetDefaultFullShipAddress(WorkContext.Uid);

            model.DefaultPayPluginInfo = Plugins.GetDefaultPayPlugin();
            model.PayPluginList = Plugins.GetPayPluginList();

            model.DefaultShipPluginInfo = Plugins.GetDefaultShipPlugin();
            model.ShipPluginList = Plugins.GetShipPluginList();

            model.PayCreditName = Credits.PayCreditName;
            model.UserPayCredits = WorkContext.PartUserInfo.PayCredits;
            model.MaxUsePayCredits = Credits.GetOrderMaxUsePayCredits(WorkContext.PartUserInfo.PayCredits);

            model.TotalWeight = Carts.SumOrderProductWeight(selectedOrderProductList);
            model.TotalCount = Carts.SumOrderProductCount(selectedOrderProductList);

            model.ProductAmount = Carts.SumOrderProductAmount(selectedOrderProductList);
            model.FullCut = Carts.SumFullCut(cartItemList);

            decimal amount = model.ProductAmount - model.FullCut;
            //计算配送费用
            if (model.DefaultFullShipAddressInfo != null && model.DefaultShipPluginInfo != null)
            {
                model.ShipFee = ((IShipPlugin)model.DefaultShipPluginInfo.Instance).GetShipFee(model.TotalWeight, amount, selectedOrderProductList, DateTime.Now, model.DefaultFullShipAddressInfo.ProvinceId, model.DefaultFullShipAddressInfo.CityId, model.DefaultFullShipAddressInfo.RegionId, WorkContext.PartUserInfo);
            }
            //计算支付费用
            if (model.DefaultPayPluginInfo != null)
            {
                IPayPlugin payPlugin = (IPayPlugin)model.DefaultPayPluginInfo.Instance;
                if (payPlugin.PayMode == 0)
                {
                    if (model.DefaultFullShipAddressInfo != null && model.DefaultShipPluginInfo != null)
                    {
                        IShipPlugin shipPlugin = (IShipPlugin)model.DefaultShipPluginInfo.Instance;
                        if (shipPlugin.SupportCOD)
                            model.PayFee = shipPlugin.GetCODPayFee(amount, DateTime.Now, model.DefaultFullShipAddressInfo.ProvinceId, model.DefaultFullShipAddressInfo.CityId, model.DefaultFullShipAddressInfo.RegionId, WorkContext.PartUserInfo);
                    }
                }
                else
                {
                    model.PayFee = payPlugin.GetPayFee(amount, DateTime.Now, WorkContext.PartUserInfo);
                }
            }

            model.OrderAmount = model.ProductAmount - model.FullCut + model.ShipFee + model.PayFee;

            model.CartItemList = cartItemList;

            model.IsVerifyCode = CommonHelper.IsInArray(WorkContext.PageKey, WorkContext.ShopConfig.VerifyPages);

            return View(model);
        }

        /// <summary>
        /// 改变配送地址
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangeShipAddress()
        {
            int saId = WebHelper.GetQueryInt("saId");//配送地址id
            //验证配送地址是否为空
            FullShipAddressInfo fullShipAddressInfo = ShipAddresses.GetFullShipAddressBySAId(saId, WorkContext.Uid);
            if (fullShipAddressInfo == null)
                return AjaxResult("emptysa", "请选择配送地址");

            string shipName = WebHelper.GetQueryString("shipName");//配送方式名称
            //验证配送方式是否为空
            PluginInfo shipPluginInfo = Plugins.GetShipPluginBySystemName(shipName);
            if (shipPluginInfo == null)
                return AjaxResult("emptyship", "请选择配送方式");

            IShipPlugin shipPlugin = (IShipPlugin)shipPluginInfo.Instance;
            //验证配送方式是否支持此收货地址
            if (!shipPlugin.IsShipRegion(fullShipAddressInfo.ProvinceId, fullShipAddressInfo.CityId, fullShipAddressInfo.RegionId))
                return AjaxResult("faraddress", "当前配送方式无法配送到此地址");

            //选中的购物车项键列表
            string selectedCartItemKeyList = WebHelper.GetQueryString("selectedCartItemKeyList");
            //购物车商品列表
            List<OrderProductInfo> orderProductList = Carts.GetCartProductList(WorkContext.Uid);
            //选中的订单商品列表
            List<OrderProductInfo> selectedOrderProductList = null;
            //购物车项列表
            List<CartItemInfo> cartItemList = Carts.TidyOrderProductList(StringHelper.SplitString(selectedCartItemKeyList), orderProductList, out selectedOrderProductList);
            int weight = Carts.SumOrderProductWeight(selectedOrderProductList);
            decimal productAmount = Carts.SumOrderProductAmount(selectedOrderProductList);
            int fullCut = Carts.SumFullCut(cartItemList);
            decimal amount = productAmount - fullCut;

            decimal shipFee = shipPlugin.GetShipFee(weight, amount, selectedOrderProductList, DateTime.Now, fullShipAddressInfo.ProvinceId, fullShipAddressInfo.CityId, fullShipAddressInfo.RegionId, WorkContext.PartUserInfo);
            return AjaxResult("success", shipFee.ToString());
        }

        /// <summary>
        /// 选择配送插件
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectShipPlugin()
        {
            string shipName = WebHelper.GetQueryString("shipName");//配送方式名称
            //验证配送方式是否为空
            PluginInfo shipPluginInfo = Plugins.GetShipPluginBySystemName(shipName);
            if (shipPluginInfo == null)
                return AjaxResult("emptyship", "请选择配送方式");

            string payName = WebHelper.GetQueryString("payName");//支付方式名称
            //验证支付方式是否为空
            PluginInfo payPluginInfo = Plugins.GetPayPluginBySystemName(payName);
            if (payPluginInfo == null)
                return AjaxResult("emptypay", "请选择支付方式");

            int saId = WebHelper.GetQueryInt("saId");//配送地址id
            //验证配送地址是否为空
            FullShipAddressInfo fullShipAddressInfo = ShipAddresses.GetFullShipAddressBySAId(saId, WorkContext.Uid);
            if (fullShipAddressInfo == null)
                return AjaxResult("emptysa", "请选择配送地址");

            IShipPlugin shipPlugin = (IShipPlugin)shipPluginInfo.Instance;
            IPayPlugin payPlugin = (IPayPlugin)payPluginInfo.Instance;
            //验证配送方式是否兼容支付方式
            if (payPlugin.PayMode == 0 && !shipPlugin.SupportCOD)
                return AjaxResult("nosupportcod", "当前配送方式不支持货到付款");
            //验证配送方式是否支持此收货地址
            if (!shipPlugin.IsShipRegion(fullShipAddressInfo.ProvinceId, fullShipAddressInfo.CityId, fullShipAddressInfo.RegionId))
                return AjaxResult("faraddress", "当前配送方式无法配送到此地址");

            //选中的购物车项键列表
            string selectedCartItemKeyList = WebHelper.GetQueryString("selectedCartItemKeyList");
            //购物车商品列表
            List<OrderProductInfo> orderProductList = Carts.GetCartProductList(WorkContext.Uid);
            //选中的订单商品列表
            List<OrderProductInfo> selectedOrderProductList = null;
            //购物车项列表
            List<CartItemInfo> cartItemList = Carts.TidyOrderProductList(StringHelper.SplitString(selectedCartItemKeyList), orderProductList, out selectedOrderProductList);
            int weight = Carts.SumOrderProductWeight(selectedOrderProductList);
            decimal productAmount = Carts.SumOrderProductAmount(selectedOrderProductList);
            int fullCut = Carts.SumFullCut(cartItemList);
            decimal amount = productAmount - fullCut;

            decimal shipFee = shipPlugin.GetShipFee(weight, amount, selectedOrderProductList, DateTime.Now, fullShipAddressInfo.ProvinceId, fullShipAddressInfo.CityId, fullShipAddressInfo.RegionId, WorkContext.PartUserInfo);
            return AjaxResult("success", shipFee.ToString());
        }

        /// <summary>
        /// 选择支付插件
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectPayPlugin()
        {
            string payName = WebHelper.GetQueryString("payName");//支付方式名称
            //验证支付方式是否为空
            PluginInfo payPluginInfo = Plugins.GetPayPluginBySystemName(payName);
            if (payPluginInfo == null)
                return AjaxResult("emptypay", "请选择支付方式");

            string shipName = WebHelper.GetQueryString("shipName");//配送方式名称
            //验证配送方式是否为空
            PluginInfo shipPluginInfo = Plugins.GetShipPluginBySystemName(shipName);
            if (shipPluginInfo == null)
                return AjaxResult("emptyship", "请选择配送方式");

            IShipPlugin shipPlugin = (IShipPlugin)shipPluginInfo.Instance;
            IPayPlugin payPlugin = (IPayPlugin)payPluginInfo.Instance;
            //验证配送方式是否兼容支付方式
            if (payPlugin.PayMode == 0 && !shipPlugin.SupportCOD)
                return AjaxResult("nosupportcod", "当前配送方式不支持货到付款");

            //选中的购物车项键列表
            string selectedCartItemKeyList = WebHelper.GetQueryString("selectedCartItemKeyList");
            //购物车商品列表
            List<OrderProductInfo> orderProductList = Carts.GetCartProductList(WorkContext.Uid);
            //选中的订单商品列表
            List<OrderProductInfo> selectedOrderProductList = null;
            //购物车项列表
            List<CartItemInfo> cartItemList = Carts.TidyOrderProductList(StringHelper.SplitString(selectedCartItemKeyList), orderProductList, out selectedOrderProductList);
            int weight = Carts.SumOrderProductWeight(selectedOrderProductList);
            decimal productAmount = Carts.SumOrderProductAmount(selectedOrderProductList);
            int fullCut = Carts.SumFullCut(cartItemList);
            decimal amount = productAmount - fullCut;

            decimal payFee = 0M;
            if (payPlugin.PayMode == 0)
            {
                int saId = WebHelper.GetQueryInt("saId");//配送地址id
                //验证配送地址是否为空
                FullShipAddressInfo fullShipAddressInfo = ShipAddresses.GetFullShipAddressBySAId(saId, WorkContext.Uid);
                if (fullShipAddressInfo == null)
                    return AjaxResult("emptysa", "请选择配送地址");
                payFee = shipPlugin.GetCODPayFee(amount, DateTime.Now, fullShipAddressInfo.ProvinceId, fullShipAddressInfo.CityId, fullShipAddressInfo.RegionId, WorkContext.PartUserInfo);
            }
            else
            {
                payFee = payPlugin.GetPayFee(amount, DateTime.Now, WorkContext.PartUserInfo);
            }
            return AjaxResult("success", payFee.ToString());
        }

        /// <summary>
        /// 获得有效的优惠劵列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetValidCouponList()
        {
            //选中的购物车项键列表
            string selectedCartItemKeyList = WebHelper.GetQueryString("selectedCartItemKeyList");
            //购物车商品列表
            List<OrderProductInfo> orderProductList = Carts.GetCartProductList(WorkContext.Uid);
            //选中的订单商品列表
            List<OrderProductInfo> selectedOrderProductList = null;
            //购物车项列表
            List<CartItemInfo> cartItemList = Carts.TidyOrderProductList(StringHelper.SplitString(selectedCartItemKeyList), orderProductList, out selectedOrderProductList);
            decimal productAmount = Carts.SumOrderProductAmount(selectedOrderProductList);
            int fullCut = Carts.SumFullCut(cartItemList);
            decimal amount = productAmount - fullCut;

            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (DataRow row in Coupons.GetUnUsedCouponList(WorkContext.Uid).Rows)
            {
                if (TypeHelper.ObjectToInt(row["state"]) == 0)
                {
                    break;
                }
                else if (TypeHelper.ObjectToInt(row["useexpiretime"]) == 0 && TypeHelper.ObjectToDateTime(row["usestarttime"]) > DateTime.Now)
                {
                    break;
                }
                else if (TypeHelper.ObjectToInt(row["useexpiretime"]) == 0 && TypeHelper.ObjectToDateTime(row["useendtime"]) <= DateTime.Now)
                {
                    break;
                }
                else if (TypeHelper.ObjectToInt(row["useexpiretime"]) > 0 && TypeHelper.ObjectToDateTime(row["activatetime"]) <= DateTime.Now.AddDays(-1 * TypeHelper.ObjectToInt(row["useexpiretime"])))
                {
                    break;
                }
                else if (TypeHelper.ObjectToInt(row["userranklower"]) > WorkContext.UserRid)
                {
                    break;
                }
                else if (TypeHelper.ObjectToInt(row["orderamountlower"]) > amount)
                {
                    break;
                }
                else
                {
                    int limitCateId = TypeHelper.ObjectToInt(row["limitcateid"]);
                    if (limitCateId > 0)
                    {
                        foreach (OrderProductInfo orderProductInfo in orderProductList)
                        {
                            if (orderProductInfo.Type == 0 && orderProductInfo.CateId != limitCateId)
                            {
                                break;
                            }
                        }
                    }

                    int limitBrandId = TypeHelper.ObjectToInt(row["limitbrandid"]);
                    if (limitBrandId > 0)
                    {
                        foreach (OrderProductInfo orderProductInfo in orderProductList)
                        {
                            if (orderProductInfo.Type == 0 && orderProductInfo.BrandId != limitBrandId)
                            {
                                break;
                            }
                        }
                    }

                    if (TypeHelper.ObjectToInt(row["limitproduct"]) == 1)
                    {
                        List<OrderProductInfo> commonOrderProductList = Carts.GetCommonOrderProductList(orderProductList);
                        string pidList = Carts.GetPidList(commonOrderProductList);
                        if (!Coupons.IsSameCouponType(TypeHelper.ObjectToInt(row["coupontypeid"]), pidList))
                        {
                            break;
                        }
                    }
                }
                sb.AppendFormat("{0}\"couponId\":\"{1}\",\"name\":\"{2}\",\"money\":\"{3}\",\"useMode\":\"{4}\"{5},", "{", row["couponid"], row["name"], row["money"], row["usemode"], "}");
            }
            if (sb.Length > 1)
                sb.Remove(sb.Length - 1, 1);
            sb.Append("]");

            return AjaxResult("success", sb.ToString(), true);
        }

        /// <summary>
        /// 验证优惠劵编号
        /// </summary>
        /// <returns></returns>
        public ActionResult VerifyCouponSN()
        {
            string couponSN = WebHelper.GetQueryString("couponSN");//优惠劵编号
            if (couponSN.Length == 0)
                return AjaxResult("emptycouponsn", "请输入优惠劵编号");
            if (couponSN.Length != 16)
                return AjaxResult("errorcouponsn", "优惠劵编号不正确");

            CouponInfo couponInfo = Coupons.GetCouponByCouponSN(couponSN);
            if (couponInfo == null)//不存在
                return AjaxResult("noexist", "优惠劵不存在");
            return AjaxResult("success", "此优惠劵可以正常使用");
        }

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <returns></returns>
        public ActionResult SubmitOrder()
        {
            lock (_locker)
            {
                string selectedCartItemKeyList = WebHelper.GetFormString("selectedCartItemKeyList");//选中的购物车项键列表
                int saId = WebHelper.GetFormInt("saId");//配送地址id
                string shipName = WebHelper.GetFormString("shipName");//配送方式名称
                string payName = WebHelper.GetFormString("payName");//支付方式名称
                int payCreditCount = WebHelper.GetFormInt("payCreditCount");//支付积分
                string[] couponIdList = StringHelper.SplitString(WebHelper.GetFormString("couponIdList"));//客户已经激活的优惠劵
                string[] couponSNList = StringHelper.SplitString(WebHelper.GetFormString("couponSNList"));//客户还未激活的优惠劵
                int fullCut = WebHelper.GetFormInt("fullCut");//满减金额
                DateTime bestTime = TypeHelper.StringToDateTime(WebHelper.GetFormString("bestTime"), new DateTime(1970, 1, 1));//最佳配送时间
                string buyerRemark = WebHelper.GetFormString("buyerRemark");//买家备注
                string verifyCode = WebHelper.GetFormString("verifyCode");//验证码

                //验证验证码
                if (CommonHelper.IsInArray(WorkContext.PageKey, WorkContext.ShopConfig.VerifyPages))
                {
                    if (string.IsNullOrWhiteSpace(verifyCode))
                    {
                        return AjaxResult("emptyverifycode", "验证码不能为空");
                    }
                    else if (verifyCode.ToLower() != Sessions.GetValueString(WorkContext.Sid, "verifyCode"))
                    {
                        return AjaxResult("wrongverifycode", "验证码不正确");
                    }
                }

                //验证买家备注的内容长度
                if (StringHelper.GetStringLength(buyerRemark) > 125)
                    return AjaxResult("muchbuyerremark", "备注最多填写125个字");

                //验证支付积分
                if (payCreditCount > 0)
                {
                    if (payCreditCount > WorkContext.PartUserInfo.PayCredits)
                        return AjaxResult("noenoughpaycredit", "你使用的" + Credits.PayCreditName + "数超过你所拥有的" + WorkContext.PartUserInfo.PayCredits + "数");
                    if (payCreditCount > Credits.OrderMaxUsePayCredits)
                        return AjaxResult("maxusepaycredit", "此笔订单最多使用" + Credits.OrderMaxUsePayCredits + "个" + Credits.PayCreditName);
                }

                //验证支付方式是否为空
                PluginInfo payPluginInfo = Plugins.GetPayPluginBySystemName(payName);
                if (payPluginInfo == null)
                    return AjaxResult("empaypay", "请选择支付方式");

                //验证配送方式是否为空
                PluginInfo shipPluginInfo = Plugins.GetShipPluginBySystemName(shipName);
                if (shipPluginInfo == null)
                    return AjaxResult("empayship", "请选择配送方式");

                //验证配送地址是否为空
                FullShipAddressInfo fullShipAddressInfo = ShipAddresses.GetFullShipAddressBySAId(saId, WorkContext.Uid);
                if (fullShipAddressInfo == null)
                    return AjaxResult("emptysaid", "请选择配送地址");

                IShipPlugin shipPlugin = (IShipPlugin)shipPluginInfo.Instance;
                //验证配送方式是否支持此收货地址
                if (!shipPlugin.IsShipRegion(fullShipAddressInfo.ProvinceId, fullShipAddressInfo.CityId, fullShipAddressInfo.RegionId))
                    return AjaxResult("faraddress", "当前配送方式无法配送到此地址");

                IPayPlugin payPlugin = (IPayPlugin)payPluginInfo.Instance;
                //验证配送方式是否兼容支付方式
                if (payPlugin.PayMode == 0 && !shipPlugin.SupportCOD)
                    return AjaxResult("nosupportcod", "当前配送方式不支持货到付款");

                //购物车商品列表
                List<OrderProductInfo> orderProductList = Carts.GetCartProductList(WorkContext.Uid);
                if (orderProductList.Count < 1)
                    return AjaxResult("emptycart", "购物车中没有商品");

                //选中的订单商品列表
                List<OrderProductInfo> selectedOrderProductList = null;
                //剩余的订单商品列表
                List<OrderProductInfo> remainedOrderProductList = null;
                //购物车项列表
                List<CartItemInfo> cartItemList = Carts.TidyOrderProductList(StringHelper.SplitString(selectedCartItemKeyList), orderProductList, out selectedOrderProductList, out remainedOrderProductList);
                if (selectedOrderProductList.Count < 1)
                    return AjaxResult("nonselected", "购物车中没有选中的商品");

                //验证优惠劵
                List<CouponInfo> couponList = new List<CouponInfo>();
                if (couponIdList.Length > 0)
                {
                    foreach (string couponId in couponIdList)
                    {
                        int tempCouponId = TypeHelper.StringToInt(couponId);
                        if (tempCouponId > 0)
                        {
                            CouponInfo couponInfo = Coupons.GetCouponByCouponId(TypeHelper.StringToInt(couponId));
                            if (couponInfo == null)
                                return AjaxResult("nocoupon", "优惠劵不存在");
                            else
                                couponList.Add(couponInfo);
                        }
                    }
                }
                if (couponSNList.Length > 0)
                {
                    foreach (string couponSN in couponSNList)
                    {
                        if (!string.IsNullOrWhiteSpace(couponSN))
                        {
                            CouponInfo couponInfo = Coupons.GetCouponByCouponSN(couponSN);
                            if (couponInfo == null)
                                return AjaxResult("nocoupon", "优惠劵" + couponSN + "不存在");
                            else
                                couponList.Add(couponInfo);
                        }
                    }
                }
                foreach (CouponInfo couponInfo in couponList)
                {
                    #region  验证

                    CouponTypeInfo couponTypeInfo = Coupons.GetCouponTypeById(couponInfo.CouponTypeId);
                    if (couponTypeInfo == null)
                    {
                        return AjaxResult("nocoupontype", "编号为" + couponInfo.CouponSN + "优惠劵类型不存在");
                    }
                    else if (couponTypeInfo.State == 0)
                    {
                        return AjaxResult("closecoupontype", "编号为" + couponInfo.CouponSN + "优惠劵类型已关闭");
                    }
                    else if (couponTypeInfo.UseExpireTime == 0 && couponTypeInfo.UseStartTime > DateTime.Now)
                    {
                        return AjaxResult("unstartcoupon", "编号为" + couponInfo.CouponSN + "优惠劵还未到使用时间");
                    }
                    else if (couponTypeInfo.UseExpireTime == 0 && couponTypeInfo.UseEndTime <= DateTime.Now)
                    {
                        return AjaxResult("expiredcoupon", "编号为" + couponInfo.CouponSN + "优惠劵已过期");
                    }
                    else if (couponTypeInfo.UseExpireTime > 0 && couponInfo.ActivateTime <= DateTime.Now.AddDays(-1 * couponTypeInfo.UseExpireTime))
                    {
                        return AjaxResult("expiredcoupon", "编号为" + couponInfo.CouponSN + "优惠劵已过期");
                    }
                    else if (couponTypeInfo.UserRankLower > WorkContext.PartUserInfo.UserRid)
                    {
                        return AjaxResult("userranklowercoupon", "你的用户等级太低，不能使用编号为" + couponInfo.CouponSN + "优惠劵");
                    }
                    else if (couponList.Count > 1 && couponTypeInfo.UseMode == 1)
                    {
                        return AjaxResult("nomutcoupon", "编号为" + couponInfo.CouponSN + "优惠劵不能叠加使用");
                    }
                    else if (couponTypeInfo.OrderAmountLower > Carts.SumOrderProductAmount(selectedOrderProductList))
                    {
                        return AjaxResult("orderamountlowercoupon", "订单金额太低，不能使用编号为" + couponInfo.CouponSN + "优惠劵");
                    }
                    else
                    {
                        if (couponTypeInfo.LimitCateId > 0)
                        {
                            foreach (OrderProductInfo orderProductInfo in selectedOrderProductList)
                            {
                                if (orderProductInfo.Type == 0 && orderProductInfo.CateId != couponTypeInfo.LimitCateId)
                                {
                                    return AjaxResult("limitcategorycoupon", "编号为" + couponInfo.CouponSN + "优惠劵只能在购买" + Categories.GetCategoryById(couponTypeInfo.LimitCateId).Name + "类的商品时使用");
                                }
                            }
                        }

                        if (couponTypeInfo.LimitBrandId > 0)
                        {
                            foreach (OrderProductInfo orderProductInfo in selectedOrderProductList)
                            {
                                if (orderProductInfo.Type == 0 && orderProductInfo.BrandId != couponTypeInfo.LimitBrandId)
                                {
                                    return AjaxResult("limitbrandcoupon", "编号为" + couponInfo.CouponSN + "优惠劵只能在购买" + Brands.GetBrandById(couponTypeInfo.LimitBrandId).Name + "品牌的商品时使用");
                                }
                            }
                        }

                        if (couponTypeInfo.LimitProduct == 1)
                        {
                            List<OrderProductInfo> commonOrderProductList = Carts.GetCommonOrderProductList(selectedOrderProductList);
                            if (!Coupons.IsSameCouponType(couponTypeInfo.CouponTypeId, Carts.GetPidList(commonOrderProductList)))
                                return AjaxResult("limitproductcoupon", "编号为" + couponInfo.CouponSN + "优惠劵只能在购买指定商品时使用");
                        }
                    }

                    #endregion
                }

                string pidList = Carts.GetPidList(selectedOrderProductList);//商品id列表
                List<ProductStockInfo> productStockList = Products.GetProductStockList(pidList);//商品库存列表
                List<SinglePromotionInfo> singlePromotionList = new List<SinglePromotionInfo>();//单品促销活动列表
                //循环购物车项列表，依次验证
                foreach (CartItemInfo cartItemInfo in cartItemList)
                {
                    if (cartItemInfo.Type == 0)
                    {
                        CartProductInfo cartProductInfo = (CartProductInfo)cartItemInfo.Item;

                        #region 验证

                        OrderProductInfo orderProductInfo = cartProductInfo.OrderProductInfo;

                        //验证商品信息
                        PartProductInfo partProductInfo = Products.GetPartProductById(orderProductInfo.Pid);
                        if (partProductInfo == null)
                        {
                            return AjaxResult("outsaleproduct", "商品" + partProductInfo.Name + "已经下架，请删除此商品");
                        }
                        if (orderProductInfo.Name != partProductInfo.Name || orderProductInfo.ShopPrice != partProductInfo.ShopPrice || orderProductInfo.MarketPrice != partProductInfo.MarketPrice || orderProductInfo.CostPrice != partProductInfo.CostPrice || orderProductInfo.Weight != partProductInfo.Weight || orderProductInfo.PSN != partProductInfo.PSN)
                        {
                            return AjaxResult("changeproduct", "商品" + partProductInfo.Name + "信息有变化，请删除后重新添加");
                        }

                        //验证商品库存
                        ProductStockInfo productStockInfo = Products.GetProductStock(orderProductInfo.Pid, productStockList);
                        if (productStockInfo.Number < orderProductInfo.RealCount)
                        {
                            return AjaxResult("outstock", "商品" + partProductInfo.Name + "库存不足");
                        }
                        else
                        {
                            productStockInfo.Number -= orderProductInfo.RealCount;
                        }

                        //验证买送促销活动
                        if (orderProductInfo.ExtCode2 > 0)
                        {
                            BuySendPromotionInfo buySendPromotionInfo = Promotions.GetBuySendPromotion(orderProductInfo.BuyCount, orderProductInfo.Pid, DateTime.Now);
                            if (buySendPromotionInfo == null)
                            {
                                return AjaxResult("stopbuysend", "商品" + orderProductInfo.Name + "的买送促销活动已经停止,请删除此商品后重新添加");
                            }
                            else if (buySendPromotionInfo.PmId != orderProductInfo.ExtCode2)
                            {
                                return AjaxResult("replacebuysend", "商品" + orderProductInfo.Name + "的买送促销活动已经替换，请删除此商品后重新添加");
                            }
                            else if (buySendPromotionInfo.UserRankLower > WorkContext.UserRid)
                            {
                                orderProductInfo.RealCount = orderProductInfo.BuyCount;
                                orderProductInfo.ExtCode2 = -1 * orderProductInfo.ExtCode2;
                                Carts.UpdateOrderProductBuySend(new List<OrderProductInfo>() { orderProductInfo });
                                if (cartProductInfo.GiftList.Count > 0)
                                {
                                    foreach (OrderProductInfo gift in cartProductInfo.GiftList)
                                    {
                                        gift.RealCount = orderProductInfo.BuyCount * gift.ExtCode2;
                                    }
                                    Carts.UpdateOrderProductCount(cartProductInfo.GiftList);
                                }
                                return AjaxResult("userranklowerbuysend", "你的用户等级太低，无法参加商品" + orderProductInfo.Name + "的买送促销活动,所以您当前只能享受普通购买，请返回购物车重新确认");
                            }
                            else if (orderProductInfo.RealCount != (orderProductInfo.BuyCount + (orderProductInfo.BuyCount / buySendPromotionInfo.BuyCount) * buySendPromotionInfo.SendCount))
                            {
                                return AjaxResult("changebuysend", "商品" + orderProductInfo.Name + "的买送促销活动已经改变，请删除此商品后重新添加");
                            }
                        }

                        //验证单品促销活动
                        if (orderProductInfo.ExtCode1 > 0)
                        {
                            SinglePromotionInfo singlePromotionInfo = Promotions.GetSinglePromotionByPidAndTime(orderProductInfo.Pid, DateTime.Now);
                            if (singlePromotionInfo == null)
                            {
                                return AjaxResult("stopsingle", "商品" + orderProductInfo.Name + "的单品促销活动已经停止，请删除此商品后重新添加");
                            }
                            if (singlePromotionInfo.PayCredits != orderProductInfo.PayCredits || singlePromotionInfo.CouponTypeId != orderProductInfo.CouponTypeId)
                            {
                                return AjaxResult("changesingle", "商品" + orderProductInfo.Name + "的单品促销活动已经改变，请删除此商品后重新添加");
                            }
                            if (singlePromotionInfo.UserRankLower > WorkContext.PartUserInfo.UserRid)
                            {
                                orderProductInfo.DiscountPrice = orderProductInfo.ShopPrice;
                                orderProductInfo.PayCredits = 0;
                                orderProductInfo.CouponTypeId = 0;
                                orderProductInfo.ExtCode1 = -1 * orderProductInfo.ExtCode1;
                                Carts.UpdateOrderProductSingle(new List<OrderProductInfo>() { orderProductInfo });
                                return AjaxResult("userranklowersingle", "你的用户等级太低，无法参加商品" + orderProductInfo.Name + "的单品促销活动,所以您当前只能享受普通购买，请返回购物车重新确认");
                            }
                            if (singlePromotionInfo.QuotaLower > 0 && orderProductInfo.BuyCount < singlePromotionInfo.QuotaLower)
                            {
                                orderProductInfo.DiscountPrice = orderProductInfo.ShopPrice;
                                orderProductInfo.PayCredits = 0;
                                orderProductInfo.CouponTypeId = 0;
                                orderProductInfo.ExtCode1 = -1 * orderProductInfo.ExtCode1;
                                Carts.UpdateOrderProductSingle(new List<OrderProductInfo>() { orderProductInfo });
                                return AjaxResult("orderminsingle", "商品" + orderProductInfo.Name + "的单品促销活动要求每单最少购买" + singlePromotionInfo.QuotaLower + "个,所以您当前只能享受普通购买，请返回购物车重新确认");
                            }
                            if (singlePromotionInfo.QuotaUpper > 0 && orderProductInfo.BuyCount > singlePromotionInfo.QuotaUpper)
                            {
                                orderProductInfo.DiscountPrice = orderProductInfo.ShopPrice;
                                orderProductInfo.PayCredits = 0;
                                orderProductInfo.CouponTypeId = 0;
                                orderProductInfo.ExtCode1 = -1 * orderProductInfo.ExtCode1;
                                Carts.UpdateOrderProductSingle(new List<OrderProductInfo>() { orderProductInfo });
                                return AjaxResult("ordermuchsingle", "商品" + orderProductInfo.Name + "的单品促销活动要求每单最多购买" + singlePromotionInfo.QuotaLower + "个,所以您当前只能享受普通购买，请返回购物车重新确认");
                            }
                            if (singlePromotionInfo.AllowBuyCount > 0 && Promotions.GetSinglePromotionProductBuyCount(WorkContext.Uid, singlePromotionInfo.PmId) > singlePromotionInfo.AllowBuyCount)
                            {
                                orderProductInfo.DiscountPrice = orderProductInfo.ShopPrice;
                                orderProductInfo.PayCredits = 0;
                                orderProductInfo.CouponTypeId = 0;
                                orderProductInfo.ExtCode1 = -1 * orderProductInfo.ExtCode1;
                                Carts.UpdateOrderProductSingle(new List<OrderProductInfo>() { orderProductInfo });
                                return AjaxResult("userminsingle", "商品" + orderProductInfo.Name + "的单品促销活动每个人最多购买" + singlePromotionInfo.AllowBuyCount + "个,所以您当前只能享受普通购买，请返回购物车重新确认");
                            }
                            if (singlePromotionInfo.IsStock == 1)
                            {
                                SinglePromotionInfo temp = singlePromotionList.Find(x => x.PmId == singlePromotionInfo.PmId);
                                if (temp == null)
                                {
                                    temp = singlePromotionInfo;
                                    singlePromotionList.Add(temp);
                                }

                                if (temp.Stock < orderProductInfo.RealCount)
                                {
                                    orderProductInfo.DiscountPrice = orderProductInfo.ShopPrice;
                                    orderProductInfo.PayCredits = 0;
                                    orderProductInfo.CouponTypeId = 0;
                                    orderProductInfo.ExtCode1 = -1 * orderProductInfo.ExtCode1;
                                    Carts.UpdateOrderProductSingle(new List<OrderProductInfo>() { orderProductInfo });
                                    return AjaxResult("stockoutsingle", "商品" + orderProductInfo.Name + "的单品促销活动库存不足,所以您当前只能享受普通购买，请返回购物车重新确认");
                                }
                                else
                                {
                                    temp.Stock -= orderProductInfo.RealCount;
                                }
                            }
                        }

                        //验证赠品促销活动
                        if (orderProductInfo.ExtCode3 > 0)
                        {
                            GiftPromotionInfo giftPromotionInfo = Promotions.GetGiftPromotionByPidAndTime(orderProductInfo.Pid, DateTime.Now);
                            if (giftPromotionInfo == null)
                            {
                                return AjaxResult("stopgift", "商品" + orderProductInfo.Name + "的赠品促销活动已经停止,请删除此商品后重新添加");
                            }
                            else if (giftPromotionInfo.PmId != orderProductInfo.ExtCode3)
                            {
                                return AjaxResult("changegift", "商品" + orderProductInfo.Name + "的赠品已经改变，请删除此商品后重新添加");
                            }
                            else if (giftPromotionInfo.UserRankLower > WorkContext.UserRid)
                            {
                                orderProductInfo.ExtCode3 = -1 * orderProductInfo.ExtCode3;
                                Carts.UpdateOrderProductGift(new List<OrderProductInfo>() { orderProductInfo });
                                Carts.DeleteOrderProductList(cartProductInfo.GiftList);
                                return AjaxResult("userranklowergift", "你的用户等级太低，无法参加商品" + orderProductInfo.Name + "的赠品促销活动,所以您当前只能享受普通购买，请返回购物车重新确认");
                            }
                            else if (giftPromotionInfo.QuotaUpper > 0 && orderProductInfo.BuyCount > giftPromotionInfo.QuotaUpper)
                            {
                                orderProductInfo.ExtCode3 = -1 * orderProductInfo.ExtCode3;
                                Carts.UpdateOrderProductGift(new List<OrderProductInfo>() { orderProductInfo });
                                Carts.DeleteOrderProductList(cartProductInfo.GiftList);
                                return AjaxResult("ordermuchgift", "商品" + orderProductInfo.Name + "的赠品要求每单最多购买" + giftPromotionInfo.QuotaUpper + "个,所以您当前只能享受普通购买，请返回购物车重新确认");
                            }
                            else if (cartProductInfo.GiftList.Count > 0)
                            {
                                List<ExtGiftInfo> extGiftList = Promotions.GetExtGiftList(orderProductInfo.ExtCode3);
                                if (extGiftList.Count != cartProductInfo.GiftList.Count)
                                {
                                    return AjaxResult("changegift", "商品" + orderProductInfo.Name + "的赠品已经改变，请删除此商品后重新添加");
                                }
                                List<OrderProductInfo> newGiftOrderProductList = new List<OrderProductInfo>(extGiftList.Count);
                                foreach (ExtGiftInfo extGiftInfo in extGiftList)
                                {
                                    OrderProductInfo giftOrderProduct = Carts.BuildOrderProduct(extGiftInfo);
                                    Carts.SetGiftOrderProduct(giftOrderProduct, 1, orderProductInfo.RealCount, extGiftInfo.Number, giftPromotionInfo.PmId);
                                    newGiftOrderProductList.Add(giftOrderProduct);
                                }

                                //验证赠品信息是否改变
                                for (int i = 0; i < newGiftOrderProductList.Count; i++)
                                {
                                    OrderProductInfo newSuitOrderProductInfo = newGiftOrderProductList[i];
                                    OrderProductInfo oldSuitOrderProductInfo = cartProductInfo.GiftList[i];
                                    if (newSuitOrderProductInfo.Pid != oldSuitOrderProductInfo.Pid ||
                                        newSuitOrderProductInfo.Name != oldSuitOrderProductInfo.Name ||
                                        newSuitOrderProductInfo.ShopPrice != oldSuitOrderProductInfo.ShopPrice ||
                                        newSuitOrderProductInfo.MarketPrice != oldSuitOrderProductInfo.MarketPrice ||
                                        newSuitOrderProductInfo.CostPrice != oldSuitOrderProductInfo.CostPrice ||
                                        newSuitOrderProductInfo.Type != oldSuitOrderProductInfo.Type ||
                                        newSuitOrderProductInfo.RealCount != oldSuitOrderProductInfo.RealCount ||
                                        newSuitOrderProductInfo.BuyCount != oldSuitOrderProductInfo.BuyCount ||
                                        newSuitOrderProductInfo.ExtCode1 != oldSuitOrderProductInfo.ExtCode1 ||
                                        newSuitOrderProductInfo.ExtCode2 != oldSuitOrderProductInfo.ExtCode2 ||
                                        newSuitOrderProductInfo.ExtCode3 != oldSuitOrderProductInfo.ExtCode3 ||
                                        newSuitOrderProductInfo.ExtCode4 != oldSuitOrderProductInfo.ExtCode4 ||
                                        newSuitOrderProductInfo.ExtCode5 != oldSuitOrderProductInfo.ExtCode5)
                                    {
                                        return AjaxResult("changegift", "商品" + orderProductInfo.Name + "的赠品已经改变，请删除此商品后重新添加");
                                    }
                                }

                                //验证赠品库存
                                foreach (OrderProductInfo gift in cartProductInfo.GiftList)
                                {
                                    ProductStockInfo stockInfo = Products.GetProductStock(gift.Pid, productStockList);
                                    if (stockInfo.Number < gift.RealCount)
                                    {
                                        if (stockInfo.Number == 0)
                                        {
                                            Carts.DeleteOrderProductList(new List<OrderProductInfo>() { gift });
                                            if (cartProductInfo.GiftList.Count == 1)
                                            {
                                                orderProductInfo.ExtCode3 = -1 * orderProductInfo.ExtCode3;
                                                Carts.UpdateOrderProductGift(new List<OrderProductInfo>() { orderProductInfo });
                                            }
                                        }
                                        else
                                        {
                                            gift.RealCount = stockInfo.Number;
                                            Carts.UpdateOrderProductCount(new List<OrderProductInfo>() { gift });
                                        }
                                        return AjaxResult("outstock", "商品" + orderProductInfo.Name + "的赠品" + gift.Name + "库存不足,请返回购物车重新确认");
                                    }
                                    else
                                    {
                                        stockInfo.Number -= gift.RealCount;
                                    }
                                }
                            }
                        }

                        #endregion
                    }
                    else if (cartItemInfo.Type == 1)
                    {
                        CartSuitInfo cartSuitInfo = (CartSuitInfo)cartItemInfo.Item;

                        #region 验证

                        SuitPromotionInfo suitPromotionInfo = Promotions.GetSuitPromotionByPmIdAndTime(cartSuitInfo.PmId, DateTime.Now);
                        if (suitPromotionInfo == null)
                        {
                            return AjaxResult("stopsuit", "套装" + suitPromotionInfo.Name + "已经停止,请删除此套装");
                        }
                        else if (suitPromotionInfo.UserRankLower > WorkContext.UserRid)
                        {
                            return AjaxResult("userranklowersuit", "你的用户等级太低，无法购买套装" + suitPromotionInfo.Name + "请删除此套装");
                        }
                        else if (suitPromotionInfo.QuotaUpper > 0 && cartSuitInfo.BuyCount > suitPromotionInfo.QuotaUpper)
                        {
                            return AjaxResult("ordermuchsuit", "套装" + suitPromotionInfo.Name + "要求每单最多购买" + suitPromotionInfo.QuotaUpper + "个");
                        }
                        else if (suitPromotionInfo.OnlyOnce == 1 && Promotions.IsJoinSuitPromotion(WorkContext.Uid, suitPromotionInfo.PmId))
                        {
                            return AjaxResult("usermuchsuit", "套装" + suitPromotionInfo.Name + "要求每人最多购买1个");
                        }

                        List<OrderProductInfo> newSuitOrderProductList = new List<OrderProductInfo>();
                        foreach (ExtSuitProductInfo extSuitProductInfo in Promotions.GetExtSuitProductList(cartSuitInfo.PmId))
                        {
                            OrderProductInfo suitOrderProductInfo = Carts.BuildOrderProduct(extSuitProductInfo);
                            Carts.SetSuitOrderProduct(suitOrderProductInfo, cartSuitInfo.BuyCount, extSuitProductInfo.Number, extSuitProductInfo.Discount, suitPromotionInfo);
                            newSuitOrderProductList.Add(suitOrderProductInfo);
                        }
                        List<OrderProductInfo> oldSuitOrderProductList = new List<OrderProductInfo>();
                        foreach (CartProductInfo cartProductInfo in cartSuitInfo.CartProductList)
                        {
                            oldSuitOrderProductList.Add(cartProductInfo.OrderProductInfo);
                        }
                        if (newSuitOrderProductList.Count != oldSuitOrderProductList.Count)
                        {
                            return AjaxResult("changesuit", "套装" + suitPromotionInfo.Name + "已经改变，请删除此套装后重新下单");
                        }
                        else
                        {
                            for (int i = 0; i < newSuitOrderProductList.Count; i++)
                            {
                                OrderProductInfo newSuitOrderProductInfo = newSuitOrderProductList[i];
                                OrderProductInfo oldSuitOrderProductInfo = oldSuitOrderProductList[i];
                                if (newSuitOrderProductInfo.Pid != oldSuitOrderProductInfo.Pid ||
                                    newSuitOrderProductInfo.Name != oldSuitOrderProductInfo.Name ||
                                    newSuitOrderProductInfo.ShopPrice != oldSuitOrderProductInfo.ShopPrice ||
                                    newSuitOrderProductInfo.MarketPrice != oldSuitOrderProductInfo.MarketPrice ||
                                    newSuitOrderProductInfo.CostPrice != oldSuitOrderProductInfo.CostPrice ||
                                    newSuitOrderProductInfo.Type != oldSuitOrderProductInfo.Type ||
                                    newSuitOrderProductInfo.RealCount != oldSuitOrderProductInfo.RealCount ||
                                    newSuitOrderProductInfo.BuyCount != oldSuitOrderProductInfo.BuyCount ||
                                    newSuitOrderProductInfo.ExtCode1 != oldSuitOrderProductInfo.ExtCode1 ||
                                    newSuitOrderProductInfo.ExtCode2 != oldSuitOrderProductInfo.ExtCode2 ||
                                    newSuitOrderProductInfo.ExtCode3 != oldSuitOrderProductInfo.ExtCode3 ||
                                    newSuitOrderProductInfo.ExtCode4 != oldSuitOrderProductInfo.ExtCode4 ||
                                    newSuitOrderProductInfo.ExtCode5 != oldSuitOrderProductInfo.ExtCode5)
                                {
                                    return AjaxResult("changesuit", "套装" + suitPromotionInfo.Name + "已经改变，请删除此套装后重新下单");
                                }
                            }

                            foreach (CartProductInfo cartProductInfo in cartSuitInfo.CartProductList)
                            {
                                OrderProductInfo orderProductInfo = cartProductInfo.OrderProductInfo;

                                //验证商品信息
                                PartProductInfo partProductInfo = Products.GetPartProductById(orderProductInfo.Pid);
                                if (partProductInfo == null)
                                {
                                    return AjaxResult("outsaleproduct", "套装中的商品" + partProductInfo.Name + "已经下架，请删除此套装");
                                }
                                if (orderProductInfo.Name != partProductInfo.Name || orderProductInfo.ShopPrice != partProductInfo.ShopPrice || orderProductInfo.MarketPrice != partProductInfo.MarketPrice || orderProductInfo.CostPrice != partProductInfo.CostPrice || orderProductInfo.Weight != partProductInfo.Weight || orderProductInfo.PSN != partProductInfo.PSN)
                                {
                                    return AjaxResult("changeproduct", "套装中的商品" + partProductInfo.Name + "信息有变化，请删除套装后重新添加");
                                }

                                //验证商品库存
                                ProductStockInfo productStockInfo = Products.GetProductStock(orderProductInfo.Pid, productStockList);
                                if (productStockInfo.Number < orderProductInfo.RealCount)
                                {
                                    return AjaxResult("outstock", "套装中的商品" + partProductInfo.Name + "库存不足");
                                }
                                else
                                {
                                    productStockInfo.Number -= orderProductInfo.RealCount;
                                }

                                //验证赠品促销活动
                                if (orderProductInfo.ExtCode3 > 0)
                                {
                                    GiftPromotionInfo giftPromotionInfo = Promotions.GetGiftPromotionByPidAndTime(orderProductInfo.Pid, DateTime.Now);
                                    if (giftPromotionInfo == null)
                                    {
                                        return AjaxResult("stopgift", "套装中的商品" + orderProductInfo.Name + "的赠品促销活动已经停止,请删除此套装后重新添加");
                                    }
                                    else if (giftPromotionInfo.PmId != orderProductInfo.ExtCode3)
                                    {
                                        return AjaxResult("changegift", "套装中的商品" + orderProductInfo.Name + "的赠品已经改变，请删除此套装后重新添加");
                                    }
                                    else if (cartProductInfo.GiftList.Count > 0)
                                    {
                                        List<ExtGiftInfo> extGiftList = Promotions.GetExtGiftList(orderProductInfo.ExtCode3);
                                        if (extGiftList.Count != cartProductInfo.GiftList.Count)
                                        {
                                            return AjaxResult("changegift", "套装中的商品" + orderProductInfo.Name + "的赠品已经改变，请删除此套装后重新添加");
                                        }
                                        List<OrderProductInfo> newGiftOrderProductList = new List<OrderProductInfo>(extGiftList.Count);
                                        foreach (ExtGiftInfo extGiftInfo in extGiftList)
                                        {
                                            OrderProductInfo giftOrderProduct = Carts.BuildOrderProduct(extGiftInfo);
                                            Carts.SetGiftOrderProduct(giftOrderProduct, 1, orderProductInfo.RealCount, extGiftInfo.Number, giftPromotionInfo.PmId);
                                            newGiftOrderProductList.Add(giftOrderProduct);
                                        }

                                        //验证赠品信息是否改变
                                        for (int i = 0; i < newGiftOrderProductList.Count; i++)
                                        {
                                            OrderProductInfo newSuitOrderProductInfo = newGiftOrderProductList[i];
                                            OrderProductInfo oldSuitOrderProductInfo = cartProductInfo.GiftList[i];
                                            if (newSuitOrderProductInfo.Pid != oldSuitOrderProductInfo.Pid ||
                                                newSuitOrderProductInfo.Name != oldSuitOrderProductInfo.Name ||
                                                newSuitOrderProductInfo.ShopPrice != oldSuitOrderProductInfo.ShopPrice ||
                                                newSuitOrderProductInfo.MarketPrice != oldSuitOrderProductInfo.MarketPrice ||
                                                newSuitOrderProductInfo.CostPrice != oldSuitOrderProductInfo.CostPrice ||
                                                newSuitOrderProductInfo.Type != oldSuitOrderProductInfo.Type ||
                                                newSuitOrderProductInfo.RealCount != oldSuitOrderProductInfo.RealCount ||
                                                newSuitOrderProductInfo.BuyCount != oldSuitOrderProductInfo.BuyCount ||
                                                newSuitOrderProductInfo.ExtCode1 != oldSuitOrderProductInfo.ExtCode1 ||
                                                newSuitOrderProductInfo.ExtCode2 != oldSuitOrderProductInfo.ExtCode2 ||
                                                newSuitOrderProductInfo.ExtCode3 != oldSuitOrderProductInfo.ExtCode3 ||
                                                newSuitOrderProductInfo.ExtCode4 != oldSuitOrderProductInfo.ExtCode4 ||
                                                newSuitOrderProductInfo.ExtCode5 != oldSuitOrderProductInfo.ExtCode5)
                                            {
                                                return AjaxResult("changegift", "套装中的商品" + orderProductInfo.Name + "的赠品已经改变，请删除此套装后重新添加");
                                            }
                                        }

                                        //验证赠品库存
                                        foreach (OrderProductInfo gift in cartProductInfo.GiftList)
                                        {
                                            ProductStockInfo stockInfo = Products.GetProductStock(gift.Pid, productStockList);
                                            if (stockInfo.Number < gift.RealCount)
                                            {
                                                if (stockInfo.Number == 0)
                                                {
                                                    Carts.DeleteOrderProductList(new List<OrderProductInfo>() { gift });
                                                    if (cartProductInfo.GiftList.Count == 1)
                                                    {
                                                        orderProductInfo.ExtCode3 = -1 * orderProductInfo.ExtCode3;
                                                        Carts.UpdateOrderProductGift(new List<OrderProductInfo>() { orderProductInfo });
                                                    }
                                                }
                                                else
                                                {
                                                    gift.RealCount = stockInfo.Number;
                                                    Carts.UpdateOrderProductCount(new List<OrderProductInfo>() { gift });
                                                }
                                                return AjaxResult("outstock", "套装中的商品" + orderProductInfo.Name + "的赠品" + gift.Name + "库存不足,请返回购物车重新确认");
                                            }
                                            else
                                            {
                                                stockInfo.Number -= gift.RealCount;
                                            }
                                        }
                                    }
                                }
                            }

                        }

                        #endregion
                    }
                    else if (cartItemInfo.Type == 2)
                    {
                        CartFullSendInfo cartFullSendInfo = (CartFullSendInfo)cartItemInfo.Item;

                        #region 验证满赠促销

                        if (cartFullSendInfo.IsEnough && cartFullSendInfo.FullSendMinorOrderProductInfo != null)
                        {
                            if (cartFullSendInfo.FullSendPromotionInfo.UserRankLower > WorkContext.UserRid)
                            {
                                return AjaxResult("userranklowerfullsend", "你的用户等级太低，无法参加商品" + cartFullSendInfo.FullSendMinorOrderProductInfo.Name + "的满赠促销活动,请先删除此赠品");
                            }
                            if (cartFullSendInfo.FullSendMinorOrderProductInfo.DiscountPrice != cartFullSendInfo.FullSendPromotionInfo.AddMoney)
                            {
                                return AjaxResult("fullsendadd", "商品" + cartFullSendInfo.FullSendMinorOrderProductInfo.Name + "的满赠促销活动金额错误,请删除后重新添加");
                            }
                            if (!Promotions.IsExistFullSendProduct(cartFullSendInfo.FullSendPromotionInfo.PmId, cartFullSendInfo.FullSendMinorOrderProductInfo.Pid, 1))
                            {
                                return AjaxResult("nonfullsendminor", "商品" + cartFullSendInfo.FullSendMinorOrderProductInfo.Name + "不是满赠赠品,请删除后再结账");
                            }

                            ProductStockInfo productStockInfo = Products.GetProductStock(cartFullSendInfo.FullSendMinorOrderProductInfo.Pid, productStockList);
                            if (productStockInfo.Number < cartFullSendInfo.FullSendMinorOrderProductInfo.RealCount)
                            {
                                return AjaxResult("outstock", "满赠赠品" + cartFullSendInfo.FullSendMinorOrderProductInfo.Name + "库存不足,请删除此满赠赠品");
                            }
                            else
                            {
                                productStockInfo.Number -= cartFullSendInfo.FullSendMinorOrderProductInfo.RealCount;
                            }
                        }

                        #endregion
                        foreach (CartProductInfo cartProductInfo in cartFullSendInfo.FullSendMainCartProductList)
                        {
                            #region 验证

                            OrderProductInfo orderProductInfo = cartProductInfo.OrderProductInfo;

                            //验证商品信息
                            PartProductInfo partProductInfo = Products.GetPartProductById(orderProductInfo.Pid);
                            if (partProductInfo == null)
                            {
                                return AjaxResult("outsaleproduct", "商品" + partProductInfo.Name + "已经下架，请删除此商品");
                            }
                            if (orderProductInfo.Name != partProductInfo.Name || orderProductInfo.ShopPrice != partProductInfo.ShopPrice || orderProductInfo.MarketPrice != partProductInfo.MarketPrice || orderProductInfo.CostPrice != partProductInfo.CostPrice || orderProductInfo.Weight != partProductInfo.Weight || orderProductInfo.PSN != partProductInfo.PSN)
                            {
                                return AjaxResult("changeproduct", "商品" + partProductInfo.Name + "信息有变化，请删除后重新添加");
                            }

                            //验证商品库存
                            ProductStockInfo productStockInfo = Products.GetProductStock(orderProductInfo.Pid, productStockList);
                            if (productStockInfo.Number < orderProductInfo.RealCount)
                            {
                                return AjaxResult("outstock", "商品" + partProductInfo.Name + "库存不足");
                            }
                            else
                            {
                                productStockInfo.Number -= orderProductInfo.RealCount;
                            }

                            //验证买送促销活动
                            if (orderProductInfo.ExtCode2 > 0)
                            {
                                BuySendPromotionInfo buySendPromotionInfo = Promotions.GetBuySendPromotion(orderProductInfo.BuyCount, orderProductInfo.Pid, DateTime.Now);
                                if (buySendPromotionInfo == null)
                                {
                                    return AjaxResult("stopbuysend", "商品" + orderProductInfo.Name + "的买送促销活动已经停止,请删除此商品后重新添加");
                                }
                                else if (buySendPromotionInfo.PmId != orderProductInfo.ExtCode2)
                                {
                                    return AjaxResult("replacebuysend", "商品" + orderProductInfo.Name + "的买送促销活动已经替换，请删除此商品后重新添加");
                                }
                                else if (buySendPromotionInfo.UserRankLower > WorkContext.UserRid)
                                {
                                    orderProductInfo.RealCount = orderProductInfo.BuyCount;
                                    orderProductInfo.ExtCode2 = -1 * orderProductInfo.ExtCode2;
                                    Carts.UpdateOrderProductBuySend(new List<OrderProductInfo>() { orderProductInfo });
                                    if (cartProductInfo.GiftList.Count > 0)
                                    {
                                        foreach (OrderProductInfo gift in cartProductInfo.GiftList)
                                        {
                                            gift.RealCount = orderProductInfo.BuyCount * gift.ExtCode2;
                                        }
                                        Carts.UpdateOrderProductCount(cartProductInfo.GiftList);
                                    }
                                    return AjaxResult("userranklowerbuysend", "你的用户等级太低，无法参加商品" + orderProductInfo.Name + "的买送促销活动,所以您当前只能享受普通购买，请返回购物车重新确认");
                                }
                                else if (orderProductInfo.RealCount != (orderProductInfo.BuyCount + (orderProductInfo.BuyCount / buySendPromotionInfo.BuyCount) * buySendPromotionInfo.SendCount))
                                {
                                    return AjaxResult("changebuysend", "商品" + orderProductInfo.Name + "的买送促销活动已经改变，请删除此商品后重新添加");
                                }
                            }

                            //验证单品促销活动
                            if (orderProductInfo.ExtCode1 > 0)
                            {
                                SinglePromotionInfo singlePromotionInfo = Promotions.GetSinglePromotionByPidAndTime(orderProductInfo.Pid, DateTime.Now);
                                if (singlePromotionInfo == null)
                                {
                                    return AjaxResult("stopsingle", "商品" + orderProductInfo.Name + "的单品促销活动已经停止，请删除此商品后重新添加");
                                }
                                if (singlePromotionInfo.PayCredits != orderProductInfo.PayCredits || singlePromotionInfo.CouponTypeId != orderProductInfo.CouponTypeId)
                                {
                                    return AjaxResult("changesingle", "商品" + orderProductInfo.Name + "的单品促销活动已经改变，请删除此商品后重新添加");
                                }
                                if (singlePromotionInfo.UserRankLower > WorkContext.PartUserInfo.UserRid)
                                {
                                    orderProductInfo.DiscountPrice = orderProductInfo.ShopPrice;
                                    orderProductInfo.PayCredits = 0;
                                    orderProductInfo.CouponTypeId = 0;
                                    orderProductInfo.ExtCode1 = -1 * orderProductInfo.ExtCode1;
                                    Carts.UpdateOrderProductSingle(new List<OrderProductInfo>() { orderProductInfo });
                                    return AjaxResult("userranklowersingle", "你的用户等级太低，无法参加商品" + orderProductInfo.Name + "的单品促销活动,所以您当前只能享受普通购买，请返回购物车重新确认");
                                }
                                if (singlePromotionInfo.QuotaLower > 0 && orderProductInfo.BuyCount < singlePromotionInfo.QuotaLower)
                                {
                                    orderProductInfo.DiscountPrice = orderProductInfo.ShopPrice;
                                    orderProductInfo.PayCredits = 0;
                                    orderProductInfo.CouponTypeId = 0;
                                    orderProductInfo.ExtCode1 = -1 * orderProductInfo.ExtCode1;
                                    Carts.UpdateOrderProductSingle(new List<OrderProductInfo>() { orderProductInfo });
                                    return AjaxResult("orderminsingle", "商品" + orderProductInfo.Name + "的单品促销活动要求每单最少购买" + singlePromotionInfo.QuotaLower + "个,所以您当前只能享受普通购买，请返回购物车重新确认");
                                }
                                if (singlePromotionInfo.QuotaUpper > 0 && orderProductInfo.BuyCount > singlePromotionInfo.QuotaUpper)
                                {
                                    orderProductInfo.DiscountPrice = orderProductInfo.ShopPrice;
                                    orderProductInfo.PayCredits = 0;
                                    orderProductInfo.CouponTypeId = 0;
                                    orderProductInfo.ExtCode1 = -1 * orderProductInfo.ExtCode1;
                                    Carts.UpdateOrderProductSingle(new List<OrderProductInfo>() { orderProductInfo });
                                    return AjaxResult("ordermuchsingle", "商品" + orderProductInfo.Name + "的单品促销活动要求每单最多购买" + singlePromotionInfo.QuotaLower + "个,所以您当前只能享受普通购买，请返回购物车重新确认");
                                }
                                if (singlePromotionInfo.AllowBuyCount > 0 && Promotions.GetSinglePromotionProductBuyCount(WorkContext.Uid, singlePromotionInfo.PmId) > singlePromotionInfo.AllowBuyCount)
                                {
                                    orderProductInfo.DiscountPrice = orderProductInfo.ShopPrice;
                                    orderProductInfo.PayCredits = 0;
                                    orderProductInfo.CouponTypeId = 0;
                                    orderProductInfo.ExtCode1 = -1 * orderProductInfo.ExtCode1;
                                    Carts.UpdateOrderProductSingle(new List<OrderProductInfo>() { orderProductInfo });
                                    return AjaxResult("userminsingle", "商品" + orderProductInfo.Name + "的单品促销活动每个人最多购买" + singlePromotionInfo.AllowBuyCount + "个,所以您当前只能享受普通购买，请返回购物车重新确认");
                                }
                                if (singlePromotionInfo.IsStock == 1)
                                {
                                    SinglePromotionInfo temp = singlePromotionList.Find(x => x.PmId == singlePromotionInfo.PmId);
                                    if (temp == null)
                                    {
                                        temp = singlePromotionInfo;
                                        singlePromotionList.Add(temp);
                                    }

                                    if (temp.Stock < orderProductInfo.RealCount)
                                    {
                                        orderProductInfo.DiscountPrice = orderProductInfo.ShopPrice;
                                        orderProductInfo.PayCredits = 0;
                                        orderProductInfo.CouponTypeId = 0;
                                        orderProductInfo.ExtCode1 = -1 * orderProductInfo.ExtCode1;
                                        Carts.UpdateOrderProductSingle(new List<OrderProductInfo>() { orderProductInfo });
                                        return AjaxResult("stockoutsingle", "商品" + orderProductInfo.Name + "的单品促销活动库存不足,所以您当前只能享受普通购买，请返回购物车重新确认");
                                    }
                                    else
                                    {
                                        temp.Stock -= orderProductInfo.RealCount;
                                    }
                                }
                            }

                            //验证赠品促销活动
                            if (orderProductInfo.ExtCode3 > 0)
                            {
                                GiftPromotionInfo giftPromotionInfo = Promotions.GetGiftPromotionByPidAndTime(orderProductInfo.Pid, DateTime.Now);
                                if (giftPromotionInfo == null)
                                {
                                    return AjaxResult("stopgift", "商品" + orderProductInfo.Name + "的赠品促销活动已经停止,请删除此商品后重新添加");
                                }
                                else if (giftPromotionInfo.PmId != orderProductInfo.ExtCode3)
                                {
                                    return AjaxResult("changegift", "商品" + orderProductInfo.Name + "的赠品已经改变，请删除此商品后重新添加");
                                }
                                else if (giftPromotionInfo.UserRankLower > WorkContext.UserRid)
                                {
                                    orderProductInfo.ExtCode3 = -1 * orderProductInfo.ExtCode3;
                                    Carts.UpdateOrderProductGift(new List<OrderProductInfo>() { orderProductInfo });
                                    Carts.DeleteOrderProductList(cartProductInfo.GiftList);
                                    return AjaxResult("userranklowergift", "你的用户等级太低，无法参加商品" + orderProductInfo.Name + "的赠品促销活动,所以您当前只能享受普通购买，请返回购物车重新确认");
                                }
                                else if (giftPromotionInfo.QuotaUpper > 0 && orderProductInfo.BuyCount > giftPromotionInfo.QuotaUpper)
                                {
                                    orderProductInfo.ExtCode3 = -1 * orderProductInfo.ExtCode3;
                                    Carts.UpdateOrderProductGift(new List<OrderProductInfo>() { orderProductInfo });
                                    Carts.DeleteOrderProductList(cartProductInfo.GiftList);
                                    return AjaxResult("ordermuchgift", "商品" + orderProductInfo.Name + "的赠品要求每单最多购买" + giftPromotionInfo.QuotaUpper + "个,所以您当前只能享受普通购买，请返回购物车重新确认");
                                }
                                else if (cartProductInfo.GiftList.Count > 0)
                                {
                                    List<ExtGiftInfo> extGiftList = Promotions.GetExtGiftList(orderProductInfo.ExtCode3);
                                    if (extGiftList.Count != cartProductInfo.GiftList.Count)
                                    {
                                        return AjaxResult("changegift", "商品" + orderProductInfo.Name + "的赠品已经改变，请删除此商品后重新添加");
                                    }
                                    List<OrderProductInfo> newGiftOrderProductList = new List<OrderProductInfo>(extGiftList.Count);
                                    foreach (ExtGiftInfo extGiftInfo in extGiftList)
                                    {
                                        OrderProductInfo giftOrderProduct = Carts.BuildOrderProduct(extGiftInfo);
                                        Carts.SetGiftOrderProduct(giftOrderProduct, 1, orderProductInfo.RealCount, extGiftInfo.Number, giftPromotionInfo.PmId);
                                        newGiftOrderProductList.Add(giftOrderProduct);
                                    }

                                    //验证赠品信息是否改变
                                    for (int i = 0; i < newGiftOrderProductList.Count; i++)
                                    {
                                        OrderProductInfo newSuitOrderProductInfo = newGiftOrderProductList[i];
                                        OrderProductInfo oldSuitOrderProductInfo = cartProductInfo.GiftList[i];
                                        if (newSuitOrderProductInfo.Pid != oldSuitOrderProductInfo.Pid ||
                                            newSuitOrderProductInfo.Name != oldSuitOrderProductInfo.Name ||
                                            newSuitOrderProductInfo.ShopPrice != oldSuitOrderProductInfo.ShopPrice ||
                                            newSuitOrderProductInfo.MarketPrice != oldSuitOrderProductInfo.MarketPrice ||
                                            newSuitOrderProductInfo.CostPrice != oldSuitOrderProductInfo.CostPrice ||
                                            newSuitOrderProductInfo.Type != oldSuitOrderProductInfo.Type ||
                                            newSuitOrderProductInfo.RealCount != oldSuitOrderProductInfo.RealCount ||
                                            newSuitOrderProductInfo.BuyCount != oldSuitOrderProductInfo.BuyCount ||
                                            newSuitOrderProductInfo.ExtCode1 != oldSuitOrderProductInfo.ExtCode1 ||
                                            newSuitOrderProductInfo.ExtCode2 != oldSuitOrderProductInfo.ExtCode2 ||
                                            newSuitOrderProductInfo.ExtCode3 != oldSuitOrderProductInfo.ExtCode3 ||
                                            newSuitOrderProductInfo.ExtCode4 != oldSuitOrderProductInfo.ExtCode4 ||
                                            newSuitOrderProductInfo.ExtCode5 != oldSuitOrderProductInfo.ExtCode5)
                                        {
                                            return AjaxResult("changegift", "商品" + orderProductInfo.Name + "的赠品已经改变，请删除此商品后重新添加");
                                        }
                                    }

                                    //验证赠品库存
                                    foreach (OrderProductInfo gift in cartProductInfo.GiftList)
                                    {
                                        ProductStockInfo stockInfo = Products.GetProductStock(gift.Pid, productStockList);
                                        if (stockInfo.Number < gift.RealCount)
                                        {
                                            if (stockInfo.Number == 0)
                                            {
                                                Carts.DeleteOrderProductList(new List<OrderProductInfo>() { gift });
                                                if (cartProductInfo.GiftList.Count == 1)
                                                {
                                                    orderProductInfo.ExtCode3 = -1 * orderProductInfo.ExtCode3;
                                                    Carts.UpdateOrderProductGift(new List<OrderProductInfo>() { orderProductInfo });
                                                }
                                            }
                                            else
                                            {
                                                gift.RealCount = stockInfo.Number;
                                                Carts.UpdateOrderProductCount(new List<OrderProductInfo>() { gift });
                                            }
                                            return AjaxResult("outstock", "商品" + orderProductInfo.Name + "的赠品" + gift.Name + "库存不足,请返回购物车重新确认");
                                        }
                                        else
                                        {
                                            stockInfo.Number -= gift.RealCount;
                                        }
                                    }
                                }
                            }

                            #endregion
                        }
                    }
                    else if (cartItemInfo.Type == 3)
                    {
                        CartFullCutInfo cartFullCutInfo = (CartFullCutInfo)cartItemInfo.Item;

                        #region 验证满减促销

                        if (cartFullCutInfo.FullCutPromotionInfo.UserRankLower > WorkContext.UserRid)
                        {
                            List<OrderProductInfo> updateFullCutOrderProductList = new List<OrderProductInfo>();
                            foreach (CartProductInfo cartProductInfo in cartFullCutInfo.FullCutCartProductList)
                            {
                                cartProductInfo.OrderProductInfo.ExtCode5 = -1 * cartProductInfo.OrderProductInfo.ExtCode5;
                                updateFullCutOrderProductList.Add(cartProductInfo.OrderProductInfo);
                            }
                            Carts.UpdateOrderProductFullCut(updateFullCutOrderProductList);
                            return AjaxResult("userranklowerfullcut", "你的用户等级太低，无法参加" + cartFullCutInfo.FullCutPromotionInfo.Name + "满减促销活动,所以您当前只能享受普通购买，请返回购物车重新确认");
                        }

                        #endregion
                        foreach (CartProductInfo cartProductInfo in cartFullCutInfo.FullCutCartProductList)
                        {
                            #region 验证

                            OrderProductInfo orderProductInfo = cartProductInfo.OrderProductInfo;

                            //验证商品信息
                            PartProductInfo partProductInfo = Products.GetPartProductById(orderProductInfo.Pid);
                            if (partProductInfo == null)
                            {
                                return AjaxResult("outsaleproduct", "商品" + partProductInfo.Name + "已经下架，请删除此商品");
                            }
                            if (orderProductInfo.Name != partProductInfo.Name || orderProductInfo.ShopPrice != partProductInfo.ShopPrice || orderProductInfo.MarketPrice != partProductInfo.MarketPrice || orderProductInfo.CostPrice != partProductInfo.CostPrice || orderProductInfo.Weight != partProductInfo.Weight || orderProductInfo.PSN != partProductInfo.PSN)
                            {
                                return AjaxResult("changeproduct", "商品" + partProductInfo.Name + "信息有变化，请删除后重新添加");
                            }

                            //验证商品库存
                            ProductStockInfo productStockInfo = Products.GetProductStock(orderProductInfo.Pid, productStockList);
                            if (productStockInfo.Number < orderProductInfo.RealCount)
                            {
                                return AjaxResult("outstock", "商品" + partProductInfo.Name + "库存不足");
                            }
                            else
                            {
                                productStockInfo.Number -= orderProductInfo.RealCount;
                            }

                            //验证买送促销活动
                            if (orderProductInfo.ExtCode2 > 0)
                            {
                                BuySendPromotionInfo buySendPromotionInfo = Promotions.GetBuySendPromotion(orderProductInfo.BuyCount, orderProductInfo.Pid, DateTime.Now);
                                if (buySendPromotionInfo == null)
                                {
                                    return AjaxResult("stopbuysend", "商品" + orderProductInfo.Name + "的买送促销活动已经停止,请删除此商品后重新添加");
                                }
                                else if (buySendPromotionInfo.PmId != orderProductInfo.ExtCode2)
                                {
                                    return AjaxResult("replacebuysend", "商品" + orderProductInfo.Name + "的买送促销活动已经替换，请删除此商品后重新添加");
                                }
                                else if (buySendPromotionInfo.UserRankLower > WorkContext.UserRid)
                                {
                                    orderProductInfo.RealCount = orderProductInfo.BuyCount;
                                    orderProductInfo.ExtCode2 = -1 * orderProductInfo.ExtCode2;
                                    Carts.UpdateOrderProductBuySend(new List<OrderProductInfo>() { orderProductInfo });
                                    if (cartProductInfo.GiftList.Count > 0)
                                    {
                                        foreach (OrderProductInfo gift in cartProductInfo.GiftList)
                                        {
                                            gift.RealCount = orderProductInfo.BuyCount * gift.ExtCode2;
                                        }
                                        Carts.UpdateOrderProductCount(cartProductInfo.GiftList);
                                    }
                                    return AjaxResult("userranklowerbuysend", "你的用户等级太低，无法参加商品" + orderProductInfo.Name + "的买送促销活动,所以您当前只能享受普通购买，请返回购物车重新确认");
                                }
                                else if (orderProductInfo.RealCount != (orderProductInfo.BuyCount + (orderProductInfo.BuyCount / buySendPromotionInfo.BuyCount) * buySendPromotionInfo.SendCount))
                                {
                                    return AjaxResult("changebuysend", "商品" + orderProductInfo.Name + "的买送促销活动已经改变，请删除此商品后重新添加");
                                }
                            }

                            //验证单品促销活动
                            if (orderProductInfo.ExtCode1 > 0)
                            {
                                SinglePromotionInfo singlePromotionInfo = Promotions.GetSinglePromotionByPidAndTime(orderProductInfo.Pid, DateTime.Now);
                                if (singlePromotionInfo == null)
                                {
                                    return AjaxResult("stopsingle", "商品" + orderProductInfo.Name + "的单品促销活动已经停止，请删除此商品后重新添加");
                                }
                                if (singlePromotionInfo.PayCredits != orderProductInfo.PayCredits || singlePromotionInfo.CouponTypeId != orderProductInfo.CouponTypeId)
                                {
                                    return AjaxResult("changesingle", "商品" + orderProductInfo.Name + "的单品促销活动已经改变，请删除此商品后重新添加");
                                }
                                if (singlePromotionInfo.UserRankLower > WorkContext.PartUserInfo.UserRid)
                                {
                                    orderProductInfo.DiscountPrice = orderProductInfo.ShopPrice;
                                    orderProductInfo.PayCredits = 0;
                                    orderProductInfo.CouponTypeId = 0;
                                    orderProductInfo.ExtCode1 = -1 * orderProductInfo.ExtCode1;
                                    Carts.UpdateOrderProductSingle(new List<OrderProductInfo>() { orderProductInfo });
                                    return AjaxResult("userranklowersingle", "你的用户等级太低，无法参加商品" + orderProductInfo.Name + "的单品促销活动,所以您当前只能享受普通购买，请返回购物车重新确认");
                                }
                                if (singlePromotionInfo.QuotaLower > 0 && orderProductInfo.BuyCount < singlePromotionInfo.QuotaLower)
                                {
                                    orderProductInfo.DiscountPrice = orderProductInfo.ShopPrice;
                                    orderProductInfo.PayCredits = 0;
                                    orderProductInfo.CouponTypeId = 0;
                                    orderProductInfo.ExtCode1 = -1 * orderProductInfo.ExtCode1;
                                    Carts.UpdateOrderProductSingle(new List<OrderProductInfo>() { orderProductInfo });
                                    return AjaxResult("orderminsingle", "商品" + orderProductInfo.Name + "的单品促销活动要求每单最少购买" + singlePromotionInfo.QuotaLower + "个,所以您当前只能享受普通购买，请返回购物车重新确认");
                                }
                                if (singlePromotionInfo.QuotaUpper > 0 && orderProductInfo.BuyCount > singlePromotionInfo.QuotaUpper)
                                {
                                    orderProductInfo.DiscountPrice = orderProductInfo.ShopPrice;
                                    orderProductInfo.PayCredits = 0;
                                    orderProductInfo.CouponTypeId = 0;
                                    orderProductInfo.ExtCode1 = -1 * orderProductInfo.ExtCode1;
                                    Carts.UpdateOrderProductSingle(new List<OrderProductInfo>() { orderProductInfo });
                                    return AjaxResult("ordermuchsingle", "商品" + orderProductInfo.Name + "的单品促销活动要求每单最多购买" + singlePromotionInfo.QuotaLower + "个,所以您当前只能享受普通购买，请返回购物车重新确认");
                                }
                                if (singlePromotionInfo.AllowBuyCount > 0 && Promotions.GetSinglePromotionProductBuyCount(WorkContext.Uid, singlePromotionInfo.PmId) > singlePromotionInfo.AllowBuyCount)
                                {
                                    orderProductInfo.DiscountPrice = orderProductInfo.ShopPrice;
                                    orderProductInfo.PayCredits = 0;
                                    orderProductInfo.CouponTypeId = 0;
                                    orderProductInfo.ExtCode1 = -1 * orderProductInfo.ExtCode1;
                                    Carts.UpdateOrderProductSingle(new List<OrderProductInfo>() { orderProductInfo });
                                    return AjaxResult("userminsingle", "商品" + orderProductInfo.Name + "的单品促销活动每个人最多购买" + singlePromotionInfo.AllowBuyCount + "个,所以您当前只能享受普通购买，请返回购物车重新确认");
                                }
                                if (singlePromotionInfo.IsStock == 1)
                                {
                                    SinglePromotionInfo temp = singlePromotionList.Find(x => x.PmId == singlePromotionInfo.PmId);
                                    if (temp == null)
                                    {
                                        temp = singlePromotionInfo;
                                        singlePromotionList.Add(temp);
                                    }

                                    if (temp.Stock < orderProductInfo.RealCount)
                                    {
                                        orderProductInfo.DiscountPrice = orderProductInfo.ShopPrice;
                                        orderProductInfo.PayCredits = 0;
                                        orderProductInfo.CouponTypeId = 0;
                                        orderProductInfo.ExtCode1 = -1 * orderProductInfo.ExtCode1;
                                        Carts.UpdateOrderProductSingle(new List<OrderProductInfo>() { orderProductInfo });
                                        return AjaxResult("stockoutsingle", "商品" + orderProductInfo.Name + "的单品促销活动库存不足,所以您当前只能享受普通购买，请返回购物车重新确认");
                                    }
                                    else
                                    {
                                        temp.Stock -= orderProductInfo.RealCount;
                                    }
                                }
                            }

                            //验证赠品促销活动
                            if (orderProductInfo.ExtCode3 > 0)
                            {
                                GiftPromotionInfo giftPromotionInfo = Promotions.GetGiftPromotionByPidAndTime(orderProductInfo.Pid, DateTime.Now);
                                if (giftPromotionInfo == null)
                                {
                                    return AjaxResult("stopgift", "商品" + orderProductInfo.Name + "的赠品促销活动已经停止,请删除此商品后重新添加");
                                }
                                else if (giftPromotionInfo.PmId != orderProductInfo.ExtCode3)
                                {
                                    return AjaxResult("changegift", "商品" + orderProductInfo.Name + "的赠品已经改变，请删除此商品后重新添加");
                                }
                                else if (giftPromotionInfo.UserRankLower > WorkContext.UserRid)
                                {
                                    orderProductInfo.ExtCode3 = -1 * orderProductInfo.ExtCode3;
                                    Carts.UpdateOrderProductGift(new List<OrderProductInfo>() { orderProductInfo });
                                    Carts.DeleteOrderProductList(cartProductInfo.GiftList);
                                    return AjaxResult("userranklowergift", "你的用户等级太低，无法参加商品" + orderProductInfo.Name + "的赠品促销活动,所以您当前只能享受普通购买，请返回购物车重新确认");
                                }
                                else if (giftPromotionInfo.QuotaUpper > 0 && orderProductInfo.BuyCount > giftPromotionInfo.QuotaUpper)
                                {
                                    orderProductInfo.ExtCode3 = -1 * orderProductInfo.ExtCode3;
                                    Carts.UpdateOrderProductGift(new List<OrderProductInfo>() { orderProductInfo });
                                    Carts.DeleteOrderProductList(cartProductInfo.GiftList);
                                    return AjaxResult("ordermuchgift", "商品" + orderProductInfo.Name + "的赠品要求每单最多购买" + giftPromotionInfo.QuotaUpper + "个,所以您当前只能享受普通购买，请返回购物车重新确认");
                                }
                                else if (cartProductInfo.GiftList.Count > 0)
                                {
                                    List<ExtGiftInfo> extGiftList = Promotions.GetExtGiftList(orderProductInfo.ExtCode3);
                                    if (extGiftList.Count != cartProductInfo.GiftList.Count)
                                    {
                                        return AjaxResult("changegift", "商品" + orderProductInfo.Name + "的赠品已经改变，请删除此商品后重新添加");
                                    }
                                    List<OrderProductInfo> newGiftOrderProductList = new List<OrderProductInfo>(extGiftList.Count);
                                    foreach (ExtGiftInfo extGiftInfo in extGiftList)
                                    {
                                        OrderProductInfo giftOrderProduct = Carts.BuildOrderProduct(extGiftInfo);
                                        Carts.SetGiftOrderProduct(giftOrderProduct, 1, orderProductInfo.RealCount, extGiftInfo.Number, giftPromotionInfo.PmId);
                                        newGiftOrderProductList.Add(giftOrderProduct);
                                    }

                                    //验证赠品信息是否改变
                                    for (int i = 0; i < newGiftOrderProductList.Count; i++)
                                    {
                                        OrderProductInfo newSuitOrderProductInfo = newGiftOrderProductList[i];
                                        OrderProductInfo oldSuitOrderProductInfo = cartProductInfo.GiftList[i];
                                        if (newSuitOrderProductInfo.Pid != oldSuitOrderProductInfo.Pid ||
                                            newSuitOrderProductInfo.Name != oldSuitOrderProductInfo.Name ||
                                            newSuitOrderProductInfo.ShopPrice != oldSuitOrderProductInfo.ShopPrice ||
                                            newSuitOrderProductInfo.MarketPrice != oldSuitOrderProductInfo.MarketPrice ||
                                            newSuitOrderProductInfo.CostPrice != oldSuitOrderProductInfo.CostPrice ||
                                            newSuitOrderProductInfo.Type != oldSuitOrderProductInfo.Type ||
                                            newSuitOrderProductInfo.RealCount != oldSuitOrderProductInfo.RealCount ||
                                            newSuitOrderProductInfo.BuyCount != oldSuitOrderProductInfo.BuyCount ||
                                            newSuitOrderProductInfo.ExtCode1 != oldSuitOrderProductInfo.ExtCode1 ||
                                            newSuitOrderProductInfo.ExtCode2 != oldSuitOrderProductInfo.ExtCode2 ||
                                            newSuitOrderProductInfo.ExtCode3 != oldSuitOrderProductInfo.ExtCode3 ||
                                            newSuitOrderProductInfo.ExtCode4 != oldSuitOrderProductInfo.ExtCode4 ||
                                            newSuitOrderProductInfo.ExtCode5 != oldSuitOrderProductInfo.ExtCode5)
                                        {
                                            return AjaxResult("changegift", "商品" + orderProductInfo.Name + "的赠品已经改变，请删除此商品后重新添加");
                                        }
                                    }

                                    //验证赠品库存
                                    foreach (OrderProductInfo gift in cartProductInfo.GiftList)
                                    {
                                        ProductStockInfo stockInfo = Products.GetProductStock(gift.Pid, productStockList);
                                        if (stockInfo.Number < gift.RealCount)
                                        {
                                            if (stockInfo.Number == 0)
                                            {
                                                Carts.DeleteOrderProductList(new List<OrderProductInfo>() { gift });
                                                if (cartProductInfo.GiftList.Count == 1)
                                                {
                                                    orderProductInfo.ExtCode3 = -1 * orderProductInfo.ExtCode3;
                                                    Carts.UpdateOrderProductGift(new List<OrderProductInfo>() { orderProductInfo });
                                                }
                                            }
                                            else
                                            {
                                                gift.RealCount = stockInfo.Number;
                                                Carts.UpdateOrderProductCount(new List<OrderProductInfo>() { gift });
                                            }
                                            return AjaxResult("outstock", "商品" + orderProductInfo.Name + "的赠品" + gift.Name + "库存不足,请返回购物车重新确认");
                                        }
                                        else
                                        {
                                            stockInfo.Number -= gift.RealCount;
                                        }
                                    }
                                }
                            }

                            #endregion
                        }
                    }
                }
                if (Carts.SumFullCut(cartItemList) != fullCut)
                    return AjaxResult("wrongfullcutmoney", "满减金额不正确,请刷新页面重新提交");

                //验证已经通过,进行订单保存
                OrderInfo orderInfo = Orders.CreateOrder(WorkContext.PartUserInfo, selectedOrderProductList, singlePromotionList, fullShipAddressInfo, payPluginInfo, shipPluginInfo, payCreditCount, couponList, fullCut, buyerRemark, bestTime, WorkContext.IP);
                if (orderInfo != null)
                {
                    int pCount = 0;
                    //删除剩余的满赠赠品
                    if (remainedOrderProductList.Count > 0)
                    {
                        List<OrderProductInfo> delOrderProductList = Carts.GetFullSendMinorOrderProductList(remainedOrderProductList);
                        if (delOrderProductList.Count > 0)
                        {
                            Carts.DeleteOrderProductList(delOrderProductList);
                            pCount = Carts.SumOrderProductCount(remainedOrderProductList) - delOrderProductList.Count;
                        }
                    }
                    //创建订单处理
                    OrderActions.CreateOrderAction(new OrderActionInfo()
                    {
                        Oid = orderInfo.Oid,
                        Uid = WorkContext.Uid,
                        RealName = "本人",
                        AdminGid = 0,
                        AdminGTitle = "非管理员",
                        ActionType = (int)OrderActionType.Submit,
                        ActionTime = DateTime.Now,
                        ActionDes = orderInfo.OrderState == (int)OrderState.WaitPaying ? "您提交了订单，等待您付款" : "您提交了订单，请等待系统确认"
                    });

                    Carts.SetCartProductCountCookie(pCount);
                    return AjaxResult("success", Url.Action("submitresult", new RouteValueDictionary { { "oid", orderInfo.Oid } }));
                }
                else
                {
                    return AjaxResult("error", "提交失败，请联系管理员");
                }
            }
        }

        /// <summary>
        /// 提交结果
        /// </summary>
        public ActionResult SubmitResult()
        {
            //订单id
            int oid = WebHelper.GetQueryInt("oid");
            //订单信息
            OrderInfo orderInfo = Orders.GetOrderByOid(oid);
            if (orderInfo == null || orderInfo.Uid != WorkContext.Uid)
                return Redirect("/");

            SubmitResultModel model = new SubmitResultModel();
            model.Oid = orderInfo.Oid;
            model.OrderInfo = orderInfo;
            model.PayPlugin = Plugins.GetPayPluginBySystemName(orderInfo.PaySystemName);
            return View(model);
        }

        /// <summary>
        /// 支付展示
        /// </summary>
        public ActionResult PayShow()
        {
            //订单id
            int oid = WebHelper.GetQueryInt("oid");
            //订单信息
            OrderInfo orderInfo = Orders.GetOrderByOid(oid);

            if (orderInfo == null)
            {
                return Redirect("/");
            }
            else if (orderInfo.Uid != WorkContext.Uid)
            {
                return Redirect("/");
            }
            else if (orderInfo.OrderState != (int)OrderState.WaitPaying)
            {
                return PromptView(Url.Action("orderlist", "ucenter"), "订单当前不能支付");
            }
            else if (orderInfo.PayMode != 1)
            {
                return PromptView(Url.Action("orderlist", "ucenter"), "此订单不能在线支付");
            }
            else
            {
                PayShowModel model = new PayShowModel();
                model.Oid = orderInfo.Oid;
                model.OrderInfo = orderInfo;
                model.PayPlugin = Plugins.GetPayPluginBySystemName(orderInfo.PaySystemName);
                model.ShowView = "~/plugins/" + model.PayPlugin.Folder + "/views/show.cshtml";
                return View(model);
            }
        }

        /// <summary>
        /// 支付结果
        /// </summary>
        public ActionResult PayResult()
        {
            //订单id
            int oid = WebHelper.GetQueryInt("oid");
            //订单信息
            OrderInfo orderInfo = Orders.GetOrderByOid(oid);
            if (orderInfo == null || orderInfo.Uid != WorkContext.Uid || orderInfo.PayMode != 1)
                return Redirect("/");

            PayResultModel model = new PayResultModel();
            model.OrderInfo = orderInfo;
            if (orderInfo.OrderState != (int)OrderState.Confirming)
                model.State = 0;
            else
                model.State = 1;

            return View(model);
        }

        protected sealed override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            //不允许游客访问
            if (WorkContext.Uid < 1)
            {
                if (WorkContext.IsHttpAjax)//如果为ajax请求
                    filterContext.Result = AjaxResult("nologin", "请先登录");
                else//如果为普通请求
                    filterContext.Result = RedirectToAction("login", "account", new RouteValueDictionary { { "returnUrl", WorkContext.Url } });
            }
        }
    }
}
