using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Mobile.Models;

namespace BrnShop.Web.Mobile.Controllers
{
    /// <summary>
    /// 购物车控制器类
    /// </summary>
    public partial class CartController : BaseMobileController
    {
        /// <summary>
        /// 购物车
        /// </summary>
        public ActionResult Index()
        {
            //当商城不允许游客使用购物车时
            if (WorkContext.ShopConfig.IsGuestSC == 0 && WorkContext.Uid < 1)
                return Redirect(Url.Action("login", "account", new RouteValueDictionary { { "returnUrl", Url.Action("index") } }));

            //购物车商品列表
            List<OrderProductInfo> orderProductList = Carts.GetCartProductList(WorkContext.Uid, WorkContext.Sid);
            //商品数量
            int pCount = Carts.SumOrderProductCount(orderProductList);
            //选中的订单商品列表
            List<OrderProductInfo> selectedOrderProductList = null;
            //购物车项列表
            List<CartItemInfo> cartItemList = Carts.TidyOrderProductList(orderProductList, out selectedOrderProductList);

            //商品总数量
            int totalCount = Carts.SumOrderProductCount(selectedOrderProductList);
            //商品合计
            decimal productAmount = Carts.SumOrderProductAmount(selectedOrderProductList);
            //满减折扣
            int fullCut = Carts.SumFullCut(cartItemList);
            //订单合计
            decimal orderAmount = productAmount - fullCut;

            CartModel model = new CartModel
            {
                TotalCount = totalCount,
                ProductAmount = productAmount,
                FullCut = fullCut,
                OrderAmount = orderAmount,
                CartItemList = cartItemList
            };

            //将购物车中商品数量写入cookie
            Carts.SetCartProductCountCookie(pCount);

            return View(model);
        }

        /// <summary>
        /// 添加商品到购物车
        /// </summary>
        public ActionResult AddProduct()
        {
            int pid = WebHelper.GetQueryInt("pid");//商品id
            int buyCount = WebHelper.GetQueryInt("buyCount");//购买数量
            int scSubmitType = WorkContext.ShopConfig.SCSubmitType;//购物车的提交方式

            //当商城不允许游客使用购物车时
            if (WorkContext.ShopConfig.IsGuestSC == 0 && WorkContext.Uid < 1)
                return AjaxResult("nologin", "请先登录");

            //判断商品是否存在
            PartProductInfo partProductInfo = Products.GetPartProductById(pid);
            if (partProductInfo == null)
                return AjaxResult("noproduct", "请选择商品");

            //购买数量不能小于1
            if (buyCount < 1)
                return AjaxResult("buycountmin", "请填写购买数量");

            //商品库存
            int stockNumber = Products.GetProductStockNumberByPid(pid);
            if (stockNumber < buyCount)
                return AjaxResult("stockout", "商品库存不足");

            //购物车中已经存在的商品列表
            List<OrderProductInfo> orderProductList = Carts.GetCartProductList(WorkContext.Uid, WorkContext.Sid);
            OrderProductInfo orderProductInfo = Carts.GetCommonOrderProductByPid(pid, orderProductList);
            if (orderProductInfo == null)
            {
                if ((WorkContext.Uid > 0 && orderProductList.Count >= WorkContext.ShopConfig.MemberSCCount) || (WorkContext.Uid < 1 && orderProductList.Count >= WorkContext.ShopConfig.GuestSCCount))
                    return AjaxResult("full", "购物车已满");
            }

            buyCount = orderProductInfo == null ? buyCount : orderProductInfo.BuyCount + buyCount;
            //将商品添加到购物车
            Carts.AddProductToCart(ref orderProductList, buyCount, partProductInfo, WorkContext.Sid, WorkContext.Uid, DateTime.Now);
            //将购物车中商品数量写入cookie
            Carts.SetCartProductCountCookie(Carts.SumOrderProductCount(orderProductList));

            return AjaxResult("success", "添加成功");
        }

        /// <summary>
        /// 修改购物车中商品数量
        /// </summary>
        public ActionResult ChangePruductCount()
        {
            //当商城不允许游客使用购物车时
            if (WorkContext.ShopConfig.IsGuestSC == 0 && WorkContext.Uid < 1)
                return AjaxResult("nologin", "请先登录");

            int pid = WebHelper.GetQueryInt("pid");//商品id
            int buyCount = WebHelper.GetQueryInt("buyCount");//购买数量
            string selectedCartItemKeyList = WebHelper.GetQueryString("selectedCartItemKeyList");//选中的购物车项键列表

            //购物车商品列表
            List<OrderProductInfo> orderProductList = Carts.GetCartProductList(WorkContext.Uid, WorkContext.Sid);
            //对应商品
            OrderProductInfo orderProductInfo = Carts.GetCommonOrderProductByPid(pid, orderProductList);
            if (orderProductInfo != null)//当商品已经存在
            {
                if (buyCount < 1)//当购买数量小于1时，删除此商品
                {
                    Carts.DeleteCartProduct(ref orderProductList, orderProductInfo);
                }
                else if (buyCount != orderProductInfo.BuyCount)
                {
                    Carts.AddExistProductToCart(ref orderProductList, buyCount, orderProductInfo, DateTime.Now);
                }
            }

            //商品数量
            int pCount = Carts.SumOrderProductCount(orderProductList);
            //选中的订单商品列表
            List<OrderProductInfo> selectedOrderProductList = null;
            //购物车项列表
            List<CartItemInfo> cartItemList = Carts.TidyOrderProductList(StringHelper.SplitString(selectedCartItemKeyList), orderProductList, out selectedOrderProductList);

            //商品数量
            int totalCount = Carts.SumOrderProductCount(selectedOrderProductList);
            //商品合计
            decimal productAmount = Carts.SumOrderProductAmount(selectedOrderProductList);
            //满减折扣
            int fullCut = Carts.SumFullCut(cartItemList);
            //订单合计
            decimal orderAmount = productAmount - fullCut;

            CartModel model = new CartModel
            {
                TotalCount = totalCount,
                ProductAmount = productAmount,
                FullCut = fullCut,
                OrderAmount = orderAmount,
                CartItemList = cartItemList
            };

            //将购物车中商品数量写入cookie
            Carts.SetCartProductCountCookie(pCount);

            return View("ajaxindex", model);
        }

        /// <summary>
        /// 删除购物车中商品
        /// </summary>
        public ActionResult DelPruduct()
        {
            //当商城不允许游客使用购物车时
            if (WorkContext.ShopConfig.IsGuestSC == 0 && WorkContext.Uid < 1)
                return AjaxResult("nologin", "请先登录");

            int pid = WebHelper.GetQueryInt("pid");//商品id
            int pos = WebHelper.GetQueryInt("pos");//位置
            string selectedCartItemKeyList = WebHelper.GetQueryString("selectedCartItemKeyList");//选中的购物车项键列表

            //购物车商品列表
            List<OrderProductInfo> orderProductList = Carts.GetCartProductList(WorkContext.Uid, WorkContext.Sid);
            //对应商品
            OrderProductInfo orderProductInfo = Carts.GetCommonOrderProductByPid(pid, orderProductList);
            if (orderProductInfo != null)
                Carts.DeleteCartProduct(ref orderProductList, orderProductInfo);

            //商品数量
            int pCount = Carts.SumOrderProductCount(orderProductList);
            //选中的订单商品列表
            List<OrderProductInfo> selectedOrderProductList = null;
            //购物车项列表
            List<CartItemInfo> cartItemList = Carts.TidyOrderProductList(StringHelper.SplitString(selectedCartItemKeyList), orderProductList, out selectedOrderProductList);

            //商品总数量
            int totalCount = Carts.SumOrderProductCount(selectedOrderProductList);
            //商品合计
            decimal productAmount = Carts.SumOrderProductAmount(selectedOrderProductList);
            //满减折扣
            int fullCut = Carts.SumFullCut(cartItemList);
            //订单合计
            decimal orderAmount = productAmount - fullCut;

            CartModel model = new CartModel
            {
                TotalCount = totalCount,
                ProductAmount = productAmount,
                FullCut = fullCut,
                OrderAmount = orderAmount,
                CartItemList = cartItemList
            };

            //将购物车中商品数量写入cookie
            Carts.SetCartProductCountCookie(pCount);

            if (pos == 0)
                return View("ajaxindex", model);
            else
                return View("snap", model);
        }

        /// <summary>
        /// 添加套装到购物车
        /// </summary>
        public ActionResult AddSuit()
        {
            int pmId = WebHelper.GetQueryInt("pmId");//套装id
            int buyCount = WebHelper.GetQueryInt("buyCount");//购买数量

            //当商城不允许游客使用购物车时
            if (WorkContext.ShopConfig.IsGuestSC == 0 && WorkContext.Uid < 1)
                return AjaxResult("nologin", "请先登录");

            //购买数量不能小于1
            if (buyCount < 1)
                return AjaxResult("buycountmin", "请填写购买数量");

            //获得套装促销活动
            SuitPromotionInfo suitPromotionInfo = Promotions.GetSuitPromotionByPmIdAndTime(pmId, DateTime.Now);
            //套装促销活动不存在时
            if (suitPromotionInfo == null)
                return AjaxResult("nosuit", "请选择套装");

            //购物车商品列表
            List<OrderProductInfo> orderProductList = Carts.GetCartProductList(WorkContext.Uid, WorkContext.Sid);
            List<OrderProductInfo> suitOrderProductList = Carts.GetSuitOrderProductList(pmId, orderProductList, false);
            if (suitOrderProductList.Count == 0)
            {
                if ((WorkContext.Uid > 0 && orderProductList.Count >= WorkContext.ShopConfig.MemberSCCount) || (WorkContext.Uid < 1 && orderProductList.Count >= WorkContext.ShopConfig.GuestSCCount))
                    return AjaxResult("full", "购物车已满");
            }

            //扩展套装商品列表
            List<ExtSuitProductInfo> extSuitProductList = Promotions.GetExtSuitProductList(suitPromotionInfo.PmId);
            if (extSuitProductList.Count < 1)
                return AjaxResult("noproduct", "套装中没有商品");

            //套装商品id列表
            StringBuilder pidList = new StringBuilder();
            foreach (ExtSuitProductInfo extSuitProductInfo in extSuitProductList)
                pidList.AppendFormat("{0},", extSuitProductInfo.Pid);
            pidList.Remove(pidList.Length - 1, 1);

            //套装商品库存列表
            List<ProductStockInfo> productStockList = Products.GetProductStockList(pidList.ToString());
            foreach (ProductStockInfo item in productStockList)
            {
                if (item.Number < buyCount)
                    return AjaxResult("stockout", item.Pid.ToString());
            }

            buyCount = suitOrderProductList.Count == 0 ? buyCount : suitOrderProductList[0].BuyCount / suitOrderProductList[0].ExtCode2 + buyCount;
            Carts.AddSuitToCart(ref orderProductList, extSuitProductList, suitPromotionInfo, buyCount, WorkContext.Sid, WorkContext.Uid, DateTime.Now);
            //将购物车中商品数量写入cookie
            Carts.SetCartProductCountCookie(Carts.SumOrderProductCount(orderProductList));

            return AjaxResult("success", "添加成功");
        }

        /// <summary>
        /// 修改购物车中套装数量
        /// </summary>
        public ActionResult ChangeSuitCount()
        {
            //当商城不允许游客使用购物车时
            if (WorkContext.ShopConfig.IsGuestSC == 0 && WorkContext.Uid < 1)
                return AjaxResult("nologin", "请先登录");

            int pmId = WebHelper.GetQueryInt("pmId");//套装id
            int buyCount = WebHelper.GetQueryInt("buyCount");//购买数量
            string selectedCartItemKeyList = WebHelper.GetQueryString("selectedCartItemKeyList");//选中的购物车项键列表

            //购物车商品列表
            List<OrderProductInfo> orderProductList = Carts.GetCartProductList(WorkContext.Uid, WorkContext.Sid);
            //套装商品列表
            List<OrderProductInfo> suitOrderProductList = Carts.GetSuitOrderProductList(pmId, orderProductList, true);
            if (suitOrderProductList.Count > 0)//当套装已经存在
            {
                if (buyCount < 1)//当购买数量小于1时，删除此套装
                {
                    Carts.DeleteCartSuit(ref orderProductList, pmId);
                }
                else
                {
                    OrderProductInfo orderProductInfo = suitOrderProductList.Find(x => x.Type == 3);
                    int oldBuyCount = orderProductInfo.RealCount / orderProductInfo.ExtCode2;
                    if (buyCount != oldBuyCount)
                        Carts.AddExistSuitToCart(ref orderProductList, suitOrderProductList, buyCount);
                }
            }

            //商品数量
            int pCount = Carts.SumOrderProductCount(orderProductList);
            //选中的订单商品列表
            List<OrderProductInfo> selectedOrderProductList = null;
            //购物车项列表
            List<CartItemInfo> cartItemList = Carts.TidyOrderProductList(StringHelper.SplitString(selectedCartItemKeyList), orderProductList, out selectedOrderProductList);

            //商品总数量
            int totalCount = Carts.SumOrderProductCount(selectedOrderProductList);
            //商品合计
            decimal productAmount = Carts.SumOrderProductAmount(selectedOrderProductList);
            //满减折扣
            int fullCut = Carts.SumFullCut(cartItemList);
            //订单合计
            decimal orderAmount = productAmount - fullCut;

            CartModel model = new CartModel
            {
                TotalCount = totalCount,
                ProductAmount = productAmount,
                FullCut = fullCut,
                OrderAmount = orderAmount,
                CartItemList = cartItemList
            };

            //将购物车中商品数量写入cookie
            Carts.SetCartProductCountCookie(pCount);

            return View("ajaxindex", model);
        }

        /// <summary>
        /// 删除购物车中套装
        /// </summary>
        public ActionResult DelSuit()
        {
            //当商城不允许游客使用购物车时
            if (WorkContext.ShopConfig.IsGuestSC == 0 && WorkContext.Uid < 1)
                return AjaxResult("nologin", "请先登录");

            int pmId = WebHelper.GetQueryInt("pmId");//套装id
            string selectedCartItemKeyList = WebHelper.GetQueryString("selectedCartItemKeyList");//选中的购物车项键列表

            //购物车商品列表
            List<OrderProductInfo> orderProductList = Carts.GetCartProductList(WorkContext.Uid, WorkContext.Sid);
            Carts.DeleteCartSuit(ref orderProductList, pmId);

            //商品数量
            int pCount = Carts.SumOrderProductCount(orderProductList);
            //选中的订单商品列表
            List<OrderProductInfo> selectedOrderProductList = null;
            //购物车项列表
            List<CartItemInfo> cartItemList = Carts.TidyOrderProductList(StringHelper.SplitString(selectedCartItemKeyList), orderProductList, out selectedOrderProductList);

            //商品总数量
            int totalCount = Carts.SumOrderProductCount(selectedOrderProductList);
            //商品合计
            decimal productAmount = Carts.SumOrderProductAmount(selectedOrderProductList);
            //满减折扣
            int fullCut = Carts.SumFullCut(cartItemList);
            //订单合计
            decimal orderAmount = productAmount - fullCut;

            CartModel model = new CartModel
            {
                TotalCount = totalCount,
                ProductAmount = productAmount,
                FullCut = fullCut,
                OrderAmount = orderAmount,
                CartItemList = cartItemList
            };

            //将购物车中商品数量写入cookie
            Carts.SetCartProductCountCookie(pCount);

            return View("ajaxindex", model);
        }

        /// <summary>
        /// 获得满赠赠品
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFullSend()
        {
            //当商城不允许游客使用购物车时
            if (WorkContext.ShopConfig.IsGuestSC == 0 && WorkContext.Uid < 1)
                return AjaxResult("nologin", "请先登录");

            int pmId = WebHelper.GetQueryInt("pmId");//满赠id
            //获得满赠赠品列表
            List<PartProductInfo> fullSendPresentList = Promotions.GetFullSendPresentList(pmId);

            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (PartProductInfo partProductInfo in fullSendPresentList)
            {
                sb.AppendFormat("{0}\"pid\":\"{1}\",\"name\":\"{2}\",\"shopPrice\":\"{3}\",\"showImg\":\"{4}\",\"url\":\"{5}\"{6},", "{", partProductInfo.Pid, partProductInfo.Name, partProductInfo.ShopPrice, partProductInfo.ShowImg, Url.Action("product", "catalog", new RouteValueDictionary { { "pid", partProductInfo.Pid } }), "}");
            }
            if (fullSendPresentList.Count > 0)
                sb.Remove(sb.Length - 1, 1);
            sb.Append("]");

            return AjaxResult("success", sb.ToString(), true);
        }

        /// <summary>
        /// 添加满赠到购物车
        /// </summary>
        public ActionResult AddFullSend()
        {
            //当商城不允许游客使用购物车时
            if (WorkContext.ShopConfig.IsGuestSC == 0 && WorkContext.Uid < 1)
                return AjaxResult("nologin", "请先登录");

            int pmId = WebHelper.GetQueryInt("pmId");//满赠id
            int pid = WebHelper.GetQueryInt("pid");//商品id
            string selectedCartItemKeyList = WebHelper.GetQueryString("selectedCartItemKeyList");//选中的购物车项键列表

            //添加的商品
            PartProductInfo partProductInfo = Products.GetPartProductById(pid);
            if (partProductInfo == null)
                return AjaxResult("noproduct", "请选择商品");

            //商品库存
            int stockNumber = Products.GetProductStockNumberByPid(pid);
            if (stockNumber < 1)
                return AjaxResult("stockout", "商品库存不足");

            //满赠促销活动
            FullSendPromotionInfo fullSendPromotionInfo = Promotions.GetFullSendPromotionByPmIdAndTime(pmId, DateTime.Now);
            if (partProductInfo == null)
                return AjaxResult("nopromotion", "满赠促销活动不存在或已经结束");

            //购物车商品列表
            List<OrderProductInfo> orderProductList = Carts.GetCartProductList(WorkContext.Uid, WorkContext.Sid);

            //满赠主商品列表
            List<OrderProductInfo> fullSendMainOrderProductList = Carts.GetFullSendMainOrderProductList(pmId, orderProductList);
            if (fullSendMainOrderProductList.Count < 1)
                return AjaxResult("nolimit", "不符合活动条件");
            decimal amount = Carts.SumOrderProductAmount(fullSendMainOrderProductList);
            if (fullSendPromotionInfo.LimitMoney > amount)
                return AjaxResult("nolimit", "不符合活动条件");

            if (!Promotions.IsExistFullSendProduct(pmId, pid, 1))
                return AjaxResult("nofullsendproduct", "此商品不是满赠商品");

            //赠送商品
            OrderProductInfo fullSendMinorOrderProductInfo = Carts.GetFullSendMinorOrderProduct(pmId, orderProductList);
            if (fullSendMinorOrderProductInfo != null)
            {
                if (fullSendMinorOrderProductInfo.Pid != pid)
                    Carts.DeleteCartFullSend(ref orderProductList, fullSendMinorOrderProductInfo);
                else
                    return AjaxResult("exist", "此商品已经添加");
            }

            //添加满赠商品
            Carts.AddFullSendToCart(ref orderProductList, partProductInfo, fullSendPromotionInfo, WorkContext.Sid, WorkContext.Uid, DateTime.Now);

            //商品数量
            int pCount = Carts.SumOrderProductCount(orderProductList);
            //选中的订单商品列表
            List<OrderProductInfo> selectedOrderProductList = null;
            //购物车项列表
            List<CartItemInfo> cartItemList = Carts.TidyOrderProductList(StringHelper.SplitString(selectedCartItemKeyList), orderProductList, out selectedOrderProductList);

            //商品总数量
            int totalCount = Carts.SumOrderProductCount(selectedOrderProductList);
            //商品合计
            decimal productAmount = Carts.SumOrderProductAmount(selectedOrderProductList);
            //满减折扣
            int fullCut = Carts.SumFullCut(cartItemList);
            //订单合计
            decimal orderAmount = productAmount - fullCut;

            CartModel model = new CartModel
            {
                TotalCount = totalCount,
                ProductAmount = productAmount,
                FullCut = fullCut,
                OrderAmount = orderAmount,
                CartItemList = cartItemList
            };

            //将购物车中商品数量写入cookie
            Carts.SetCartProductCountCookie(pCount);

            return View("ajaxindex", model);
        }

        /// <summary>
        /// 删除购物车中满赠
        /// </summary>
        public ActionResult DelFullSend()
        {
            //当商城不允许游客使用购物车时
            if (WorkContext.ShopConfig.IsGuestSC == 0 && WorkContext.Uid < 1)
                return AjaxResult("nologin", "请先登录");

            int pmId = WebHelper.GetQueryInt("pmId");//满赠id
            int pos = WebHelper.GetQueryInt("pos");//位置
            string selectedCartItemKeyList = WebHelper.GetQueryString("selectedCartItemKeyList");//选中的购物车项键列表

            //购物车商品列表
            List<OrderProductInfo> orderProductList = Carts.GetCartProductList(WorkContext.Uid, WorkContext.Sid);
            //删除满赠
            Carts.DeleteCartFullSend(ref orderProductList, pmId);

            //商品数量
            int pCount = Carts.SumOrderProductCount(orderProductList);
            //选中的订单商品列表
            List<OrderProductInfo> selectedOrderProductList = null;
            //购物车项列表
            List<CartItemInfo> cartItemList = Carts.TidyOrderProductList(StringHelper.SplitString(selectedCartItemKeyList), orderProductList, out selectedOrderProductList);

            //商品总数量
            int totalCount = Carts.SumOrderProductCount(selectedOrderProductList);
            //商品合计
            decimal productAmount = Carts.SumOrderProductAmount(selectedOrderProductList);
            //满减折扣
            int fullCut = Carts.SumFullCut(cartItemList);
            //订单合计
            decimal orderAmount = productAmount - fullCut;

            CartModel model = new CartModel
            {
                TotalCount = totalCount,
                ProductAmount = productAmount,
                FullCut = fullCut,
                OrderAmount = orderAmount,
                CartItemList = cartItemList
            };

            //将购物车中商品数量写入cookie
            Carts.SetCartProductCountCookie(pCount);

            if (pos == 0)
                return View("ajaxindex", model);
            else
                return View("snap", model);
        }

        /// <summary>
        /// 取消或选中购物车项
        /// </summary>
        /// <returns></returns>
        public ActionResult CancelOrSelectCartItem()
        {
            //当商城不允许游客使用购物车时
            if (WorkContext.ShopConfig.IsGuestSC == 0 && WorkContext.Uid < 1)
                return AjaxResult("nologin", "请先登录");

            string selectedCartItemKeyList = WebHelper.GetQueryString("selectedCartItemKeyList");//选中的购物车项键列表

            //购物车商品列表
            List<OrderProductInfo> orderProductList = Carts.GetCartProductList(WorkContext.Uid, WorkContext.Sid);
            //选中的订单商品列表
            List<OrderProductInfo> selectedOrderProductList = null;
            //购物车项列表
            List<CartItemInfo> cartItemList = Carts.TidyOrderProductList(StringHelper.SplitString(selectedCartItemKeyList), orderProductList, out selectedOrderProductList);

            //商品总数量
            int totalCount = Carts.SumOrderProductCount(selectedOrderProductList);
            //商品合计
            decimal productAmount = Carts.SumOrderProductAmount(selectedOrderProductList);
            //满减折扣
            int fullCut = Carts.SumFullCut(cartItemList);
            //订单合计
            decimal orderAmount = productAmount - fullCut;

            CartModel model = new CartModel
            {
                TotalCount = totalCount,
                ProductAmount = productAmount,
                FullCut = fullCut,
                OrderAmount = orderAmount,
                CartItemList = cartItemList
            };

            return View("ajaxindex", model);
        }

        /// <summary>
        /// 选中全部购物车项
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectAllCartItem()
        {
            //当商城不允许游客使用购物车时
            if (WorkContext.ShopConfig.IsGuestSC == 0 && WorkContext.Uid < 1)
                return AjaxResult("nologin", "请先登录");

            //购物车商品列表
            List<OrderProductInfo> orderProductList = Carts.GetCartProductList(WorkContext.Uid, WorkContext.Sid);
            //选中的订单商品列表
            List<OrderProductInfo> selectedOrderProductList = null;
            //购物车项列表
            List<CartItemInfo> cartItemList = Carts.TidyOrderProductList(orderProductList, out selectedOrderProductList);

            //商品总数量
            int totalCount = Carts.SumOrderProductCount(selectedOrderProductList);
            //商品合计
            decimal productAmount = Carts.SumOrderProductAmount(selectedOrderProductList);
            //满减折扣
            int fullCut = Carts.SumFullCut(cartItemList);
            //订单合计
            decimal orderAmount = productAmount - fullCut;

            CartModel model = new CartModel
            {
                TotalCount = totalCount,
                ProductAmount = productAmount,
                FullCut = fullCut,
                OrderAmount = orderAmount,
                CartItemList = cartItemList
            };

            return View("ajaxindex", model);
        }
    }
}
