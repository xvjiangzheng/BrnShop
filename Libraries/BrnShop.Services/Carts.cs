using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 购物车操作管理类
    /// </summary>
    public partial class Carts
    {
        private static ICartStrategy _icartstrategy = BSPCart.Instance;//购物车策略

        /// <summary>
        /// 是否持久化订单商品
        /// </summary>
        public static bool IsPersistOrderProduct
        {
            get { return _icartstrategy.IsPersistOrderProduct; }
        }

        /// <summary>
        /// 获得购物车中商品数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static int GetCartProductCount(int uid)
        {
            if (uid < 1)
                return 0;
            return _icartstrategy.GetCartProductCount(uid);
        }

        /// <summary>
        /// 获得购物车中商品数量
        /// </summary>
        /// <param name="sid">用户sid</param>
        /// <returns></returns>
        public static int GetCartProductCount(string sid)
        {
            if (string.IsNullOrWhiteSpace(sid))
                return 0;
            return _icartstrategy.GetCartProductCount(sid);
        }

        /// <summary>
        /// 获得购物车中商品数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="sid">用户sid</param>
        /// <returns></returns>
        public static int GetCartProductCount(int uid, string sid)
        {
            if (uid > 0)
                return GetCartProductCount(uid);
            else
                return GetCartProductCount(sid);
        }

        /// <summary>
        /// 添加订单商品列表
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public static void AddOrderProductList(List<OrderProductInfo> orderProductList)
        {
            _icartstrategy.AddOrderProductList(orderProductList);
        }

        /// <summary>
        /// 删除订单商品列表
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public static void DeleteOrderProductList(List<OrderProductInfo> orderProductList)
        {
            _icartstrategy.DeleteOrderProductList(orderProductList);
        }

        /// <summary>
        /// 更新订单商品的数量
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public static void UpdateOrderProductCount(List<OrderProductInfo> orderProductList)
        {
            _icartstrategy.UpdateOrderProductCount(orderProductList);
        }

        /// <summary>
        /// 更新购物车的用户id
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="sid">用户sid</param>
        public static void UpdateCartUidBySid(int uid, string sid)
        {
            _icartstrategy.UpdateCartUidBySid(uid, sid);
        }

        /// <summary>
        /// 更新订单商品的买送促销活动
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public static void UpdateOrderProductBuySend(List<OrderProductInfo> orderProductList)
        {
            _icartstrategy.UpdateOrderProductBuySend(orderProductList);
        }

        /// <summary>
        /// 更新订单商品的单品促销活动
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public static void UpdateOrderProductSingle(List<OrderProductInfo> orderProductList)
        {
            _icartstrategy.UpdateOrderProductSingle(orderProductList);
        }

        /// <summary>
        /// 更新订单商品的赠品促销活动
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public static void UpdateOrderProductGift(List<OrderProductInfo> orderProductList)
        {
            _icartstrategy.UpdateOrderProductGift(orderProductList);
        }

        /// <summary>
        /// 更新订单商品的满赠促销活动
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public static void UpdateOrderProductFullSend(List<OrderProductInfo> orderProductList)
        {
            _icartstrategy.UpdateOrderProductFullSend(orderProductList);
        }

        /// <summary>
        /// 更新订单商品的满减促销活动
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public static void UpdateOrderProductFullCut(List<OrderProductInfo> orderProductList)
        {
            _icartstrategy.UpdateOrderProductFullCut(orderProductList);
        }

        /// <summary>
        /// 获得购物车商品列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static List<OrderProductInfo> GetCartProductList(int uid)
        {
            return _icartstrategy.GetCartProductList(uid);
        }

        /// <summary>
        /// 获得购物车商品列表
        /// </summary>
        /// <param name="sid">用户sid</param>
        /// <returns></returns>
        public static List<OrderProductInfo> GetCartProductList(string sid)
        {
            return _icartstrategy.GetCartProductList(sid);
        }

        /// <summary>
        /// 获得购物车商品列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="sid">用户sid</param>
        /// <returns></returns>
        public static List<OrderProductInfo> GetCartProductList(int uid, string sid)
        {
            if (uid > 0)
                return GetCartProductList(uid);
            else
                return GetCartProductList(sid);
        }

        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static int ClearCart(int uid)
        {
            return _icartstrategy.ClearCart(uid);
        }

        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="sid">sid</param>
        /// <returns></returns>
        public static int ClearCart(string sid)
        {
            return _icartstrategy.ClearCart(sid);
        }

        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="sid">sid</param>
        /// <returns></returns>
        public static int ClearCart(int uid, string sid)
        {
            if (uid > 0)
                return ClearCart(uid);
            else
                return ClearCart(sid);
        }

        /// <summary>
        /// 清空过期购物车
        /// </summary>
        /// <param name="expireTime">过期时间</param>
        public static void ClearExpiredCart(DateTime expireTime)
        {
            _icartstrategy.ClearExpiredCart(expireTime);
        }





        /// <summary>
        /// 判断购物车项是否被选中
        /// </summary>
        /// <param name="type">类型(0代表单一项,1代表组合项)</param>
        /// <param name="id">id</param>
        /// <param name="selectedCartItemKeyList">选中的购物车项键列表</param>
        /// <returns></returns>
        public static bool IsSelectCartItem(int type, int id, string[] selectedCartItemKeyList)
        {
            if (selectedCartItemKeyList == null || selectedCartItemKeyList.Length == 0)
                return true;
            string cartItemKey = string.Format("{0}_{1}", type, id);
            foreach (string selectedCartItemKey in selectedCartItemKeyList)
            {
                if (cartItemKey == selectedCartItemKey)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 整理订单商品列表
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        /// <param name="selectedOrderProductList">选中的订单商品列表</param>
        /// <returns></returns>
        public static List<CartItemInfo> TidyOrderProductList(List<OrderProductInfo> orderProductList, out List<OrderProductInfo> selectedOrderProductList)
        {
            return TidyOrderProductList(null, orderProductList, out selectedOrderProductList);
        }

        /// <summary>
        /// 整理订单商品列表
        /// </summary>
        /// <param name="selectedCartItemKeyList">选中的购物车项键列表</param>
        /// <param name="orderProductList">订单商品列表</param>
        /// <param name="selectedOrderProductList">选中的订单商品列表</param>
        /// <returns></returns>
        public static List<CartItemInfo> TidyOrderProductList(string[] selectedCartItemKeyList, List<OrderProductInfo> orderProductList, out List<OrderProductInfo> selectedOrderProductList)
        {
            List<OrderProductInfo> remainedOrderProductList = null;
            return TidyOrderProductList(selectedCartItemKeyList, orderProductList, out selectedOrderProductList, out remainedOrderProductList);
        }

        /// <summary>
        /// 整理订单商品列表
        /// </summary>
        /// <param name="selectedCartItemKeyList">选中的购物车项键列表</param>
        /// <param name="orderProductList">订单商品列表</param>
        /// <param name="selectedOrderProductList">选中的订单商品列表</param>
        /// <param name="remainedOrderProductList">剩余的订单商品列表</param>
        /// <returns></returns>
        public static List<CartItemInfo> TidyOrderProductList(string[] selectedCartItemKeyList, List<OrderProductInfo> orderProductList, out List<OrderProductInfo> selectedOrderProductList, out List<OrderProductInfo> remainedOrderProductList)
        {
            //声明一个购物车项列表
            List<CartItemInfo> cartItemList = new List<CartItemInfo>();
            //初始化选中的订单商品列表
            selectedOrderProductList = new List<OrderProductInfo>();
            //初始化剩余的订单商品列表
            remainedOrderProductList = new List<OrderProductInfo>();

            //订单商品商品数量
            int count = orderProductList.Count;
            for (int i = 0; i < count; i++)
            {
                OrderProductInfo orderProductInfo = orderProductList[i];
                if (orderProductInfo == null)//如果此订单商品已经被置为null则跳过
                    continue;

                if (orderProductInfo.Type == 0)//当商品是普通订单商品时
                {
                    if (orderProductInfo.ExtCode4 > 0)//满赠订单商品处理
                    {
                        #region 满赠订单商品处理

                        FullSendPromotionInfo fullSendPromotionInfo = Promotions.GetFullSendPromotionByPmIdAndTime(orderProductInfo.ExtCode4, DateTime.Now);
                        if (fullSendPromotionInfo != null)
                        {
                            CartFullSendInfo cartFullSendInfo = new CartFullSendInfo();

                            cartFullSendInfo.FullSendPromotionInfo = fullSendPromotionInfo;

                            List<CartProductInfo> fullSendMainCartProductList = new List<CartProductInfo>();
                            CartProductInfo cartProductInfo1 = new CartProductInfo();
                            cartProductInfo1.Selected = IsSelectCartItem(0, orderProductInfo.Pid, selectedCartItemKeyList);
                            cartProductInfo1.OrderProductInfo = orderProductInfo;
                            orderProductList[i] = null;
                            List<OrderProductInfo> giftList1 = new List<OrderProductInfo>();
                            //获取商品的赠品
                            for (int j = 0; j < count; j++)
                            {
                                OrderProductInfo item = orderProductList[j];
                                if (item != null && item.Type == 1 && item.ExtCode1 == orderProductInfo.ExtCode3)
                                {
                                    giftList1.Add(item);
                                    orderProductList[j] = null;
                                }
                            }
                            cartProductInfo1.GiftList = giftList1;
                            fullSendMainCartProductList.Add(cartProductInfo1);
                            //获取同一满赠商品
                            for (int j = 0; j < count; j++)
                            {
                                OrderProductInfo item = orderProductList[j];
                                if (item != null && item.Type == 0 && item.ExtCode4 == orderProductInfo.ExtCode4)
                                {
                                    CartProductInfo cartProductInfo2 = new CartProductInfo();
                                    cartProductInfo2.Selected = IsSelectCartItem(0, item.Pid, selectedCartItemKeyList);
                                    cartProductInfo2.OrderProductInfo = item;
                                    orderProductList[j] = null;
                                    List<OrderProductInfo> giftList2 = new List<OrderProductInfo>();
                                    for (int k = 0; k < count; k++)
                                    {
                                        OrderProductInfo item2 = orderProductList[k];
                                        if (item2 != null && item2.Type == 1 && item2.ExtCode1 == item.ExtCode3)
                                        {
                                            giftList2.Add(item2);
                                            orderProductList[k] = null;
                                        }
                                    }
                                    cartProductInfo2.GiftList = giftList2;
                                    fullSendMainCartProductList.Add(cartProductInfo2);
                                }
                            }
                            cartFullSendInfo.FullSendMainCartProductList = fullSendMainCartProductList;

                            decimal selectedFullSendMainCartProductAmount = 0M;
                            foreach (CartProductInfo fullSendMainCartProductInfo in fullSendMainCartProductList)
                            {
                                if (fullSendMainCartProductInfo.Selected)
                                    selectedFullSendMainCartProductAmount += fullSendMainCartProductInfo.OrderProductInfo.DiscountPrice * fullSendMainCartProductInfo.OrderProductInfo.BuyCount;
                            }
                            cartFullSendInfo.IsEnough = selectedFullSendMainCartProductAmount >= fullSendPromotionInfo.LimitMoney;

                            //获取商品的满赠商品
                            for (int j = 0; j < count; j++)
                            {
                                OrderProductInfo item = orderProductList[j];
                                if (item != null && item.Type == 4 && item.ExtCode1 == orderProductInfo.ExtCode4)
                                {
                                    cartFullSendInfo.FullSendMinorOrderProductInfo = item;
                                    orderProductList[j] = null;
                                    break;
                                }
                            }

                            CartItemInfo cartItemInfo = new CartItemInfo();
                            cartItemInfo.Type = 2;
                            cartItemInfo.Item = cartFullSendInfo;
                            cartItemList.Add(cartItemInfo);
                        }
                        else//当满赠促销活动不存在时，按照没有满赠促销商品处理
                        {
                            List<OrderProductInfo> updateFullSendOrderProductList = new List<OrderProductInfo>();

                            orderProductInfo.ExtCode4 = 0;
                            updateFullSendOrderProductList.Add(orderProductInfo);

                            CartProductInfo cartProductInfo1 = new CartProductInfo();
                            cartProductInfo1.Selected = IsSelectCartItem(0, orderProductInfo.Pid, selectedCartItemKeyList);
                            cartProductInfo1.OrderProductInfo = orderProductInfo;
                            orderProductList[i] = null;
                            List<OrderProductInfo> giftList1 = new List<OrderProductInfo>();
                            //获取商品的赠品
                            for (int j = 0; j < count; j++)
                            {
                                OrderProductInfo item = orderProductList[j];
                                if (item != null && item.Type == 1 && item.ExtCode1 == orderProductInfo.ExtCode3)
                                {
                                    giftList1.Add(item);
                                    orderProductList[j] = null;
                                }
                            }
                            cartProductInfo1.GiftList = giftList1;

                            CartItemInfo cartItemInfo1 = new CartItemInfo();
                            cartItemInfo1.Type = 0;
                            cartItemInfo1.Item = cartProductInfo1;
                            cartItemList.Add(cartItemInfo1);

                            //获取同一满赠商品
                            for (int j = 0; j < count; j++)
                            {
                                OrderProductInfo item = orderProductList[j];
                                if (item != null && item.Type == 0 && item.ExtCode4 == orderProductInfo.ExtCode4)
                                {
                                    item.ExtCode4 = 0;
                                    updateFullSendOrderProductList.Add(item);

                                    CartProductInfo cartProductInfo2 = new CartProductInfo();
                                    cartProductInfo2.Selected = IsSelectCartItem(0, item.Pid, selectedCartItemKeyList);
                                    cartProductInfo2.OrderProductInfo = item;
                                    orderProductList[j] = null;
                                    List<OrderProductInfo> giftList2 = new List<OrderProductInfo>();
                                    for (int k = 0; k < count; k++)
                                    {
                                        OrderProductInfo item2 = orderProductList[k];
                                        if (item2 != null && item2.Type == 1 && item2.ExtCode1 == item.ExtCode3)
                                        {
                                            giftList2.Add(item2);
                                            orderProductList[k] = null;
                                        }
                                    }
                                    cartProductInfo2.GiftList = giftList2;

                                    CartItemInfo cartItemInfo2 = new CartItemInfo();
                                    cartItemInfo2.Type = 0;
                                    cartItemInfo2.Item = cartProductInfo2;
                                    cartItemList.Add(cartItemInfo2);
                                }
                            }

                            //更新商品的满赠促销活动
                            UpdateOrderProductFullSend(updateFullSendOrderProductList);

                            //获取商品的满赠商品
                            for (int j = 0; j < count; j++)
                            {
                                OrderProductInfo item = orderProductList[j];
                                //当满赠赠品存在时删除满赠赠品
                                if (item != null && item.Type == 4 && item.ExtCode1 == orderProductInfo.ExtCode4)
                                {
                                    DeleteOrderProductList(new List<OrderProductInfo>() { item });
                                    break;
                                }
                            }
                        }

                        #endregion
                    }
                    else if (orderProductInfo.ExtCode5 > 0)//满减订单商品处理
                    {
                        #region 满减订单商品处理

                        FullCutPromotionInfo fullCutPromotionInfo = Promotions.GetFullCutPromotionByPmIdAndTime(orderProductInfo.ExtCode5, DateTime.Now);
                        if (fullCutPromotionInfo != null)
                        {
                            CartFullCutInfo cartFullCutInfo = new CartFullCutInfo();

                            cartFullCutInfo.FullCutPromotionInfo = fullCutPromotionInfo;

                            List<CartProductInfo> fullCutCartProductList = new List<CartProductInfo>();
                            CartProductInfo cartProductInfo1 = new CartProductInfo();
                            cartProductInfo1.Selected = IsSelectCartItem(0, orderProductInfo.Pid, selectedCartItemKeyList);
                            cartProductInfo1.OrderProductInfo = orderProductInfo;
                            orderProductList[i] = null;
                            List<OrderProductInfo> giftList1 = new List<OrderProductInfo>();
                            //获取商品的赠品
                            for (int j = 0; j < count; j++)
                            {
                                OrderProductInfo item = orderProductList[j];
                                if (item != null && item.Type == 1 && item.ExtCode1 == orderProductInfo.ExtCode3)
                                {
                                    giftList1.Add(item);
                                    orderProductList[j] = null;
                                }
                            }
                            cartProductInfo1.GiftList = giftList1;
                            fullCutCartProductList.Add(cartProductInfo1);
                            //获取同一满减商品
                            for (int j = 0; j < count; j++)
                            {
                                OrderProductInfo item = orderProductList[j];
                                if (item != null && item.Type == 0 && item.ExtCode5 == orderProductInfo.ExtCode5)
                                {
                                    CartProductInfo cartProductInfo2 = new CartProductInfo();
                                    cartProductInfo2.Selected = IsSelectCartItem(0, item.Pid, selectedCartItemKeyList);
                                    cartProductInfo2.OrderProductInfo = item;
                                    orderProductList[j] = null;
                                    List<OrderProductInfo> giftList2 = new List<OrderProductInfo>();
                                    for (int k = 0; k < count; k++)
                                    {
                                        OrderProductInfo item2 = orderProductList[k];
                                        if (item2 != null && item2.Type == 1 && item2.ExtCode1 == item.ExtCode3)
                                        {
                                            giftList2.Add(item2);
                                            orderProductList[k] = null;
                                        }
                                    }
                                    cartProductInfo2.GiftList = giftList2;
                                    fullCutCartProductList.Add(cartProductInfo2);
                                }
                            }
                            cartFullCutInfo.FullCutCartProductList = fullCutCartProductList;

                            decimal selectedFullCutCartProductAmount = 0M;
                            foreach (CartProductInfo fullCutCartProductInfo in fullCutCartProductList)
                            {
                                if (fullCutCartProductInfo.Selected)
                                    selectedFullCutCartProductAmount += fullCutCartProductInfo.OrderProductInfo.DiscountPrice * fullCutCartProductInfo.OrderProductInfo.BuyCount;
                            }
                            if (fullCutPromotionInfo.LimitMoney3 > 0 && selectedFullCutCartProductAmount >= fullCutPromotionInfo.LimitMoney3)
                            {
                                cartFullCutInfo.IsEnough = true;
                                cartFullCutInfo.LimitMoney = fullCutPromotionInfo.LimitMoney3;
                                cartFullCutInfo.CutMoney = fullCutPromotionInfo.CutMoney3;
                            }
                            else if (fullCutPromotionInfo.LimitMoney2 > 0 && selectedFullCutCartProductAmount >= fullCutPromotionInfo.LimitMoney2)
                            {
                                cartFullCutInfo.IsEnough = true;
                                cartFullCutInfo.LimitMoney = fullCutPromotionInfo.LimitMoney2;
                                cartFullCutInfo.CutMoney = fullCutPromotionInfo.CutMoney2;
                            }
                            else if (selectedFullCutCartProductAmount >= fullCutPromotionInfo.LimitMoney1)
                            {
                                cartFullCutInfo.IsEnough = true;
                                cartFullCutInfo.LimitMoney = fullCutPromotionInfo.LimitMoney1;
                                cartFullCutInfo.CutMoney = fullCutPromotionInfo.CutMoney1;
                            }
                            else
                            {
                                cartFullCutInfo.IsEnough = false;
                                cartFullCutInfo.LimitMoney = fullCutPromotionInfo.LimitMoney1;
                                cartFullCutInfo.CutMoney = fullCutPromotionInfo.CutMoney1;
                            }

                            CartItemInfo cartItemInfo = new CartItemInfo();
                            cartItemInfo.Type = 3;
                            cartItemInfo.Item = cartFullCutInfo;
                            cartItemList.Add(cartItemInfo);
                        }
                        else//当满减促销活动不存在时，按照没有满减促销商品处理
                        {
                            List<OrderProductInfo> updateFullCutOrderProductList = new List<OrderProductInfo>();

                            orderProductInfo.ExtCode5 = 0;
                            updateFullCutOrderProductList.Add(orderProductInfo);

                            CartProductInfo cartProductInfo1 = new CartProductInfo();
                            cartProductInfo1.Selected = IsSelectCartItem(0, orderProductInfo.Pid, selectedCartItemKeyList);
                            cartProductInfo1.OrderProductInfo = orderProductInfo;
                            orderProductList[i] = null;
                            List<OrderProductInfo> giftList1 = new List<OrderProductInfo>();
                            //获取商品的赠品
                            for (int j = 0; j < count; j++)
                            {
                                OrderProductInfo item = orderProductList[j];
                                if (item != null && item.Type == 1 && item.ExtCode1 == orderProductInfo.ExtCode3)
                                {
                                    giftList1.Add(item);
                                    orderProductList[j] = null;
                                }
                            }
                            cartProductInfo1.GiftList = giftList1;

                            CartItemInfo cartItemInfo1 = new CartItemInfo();
                            cartItemInfo1.Type = 0;
                            cartItemInfo1.Item = cartProductInfo1;
                            cartItemList.Add(cartItemInfo1);

                            //获取同一满减商品
                            for (int j = 0; j < count; j++)
                            {
                                OrderProductInfo item = orderProductList[j];
                                if (item != null && item.Type == 0 && item.ExtCode5 == orderProductInfo.ExtCode5)
                                {
                                    item.ExtCode5 = 0;
                                    updateFullCutOrderProductList.Add(item);

                                    CartProductInfo cartProductInfo2 = new CartProductInfo();
                                    cartProductInfo2.Selected = IsSelectCartItem(0, item.Pid, selectedCartItemKeyList);
                                    cartProductInfo2.OrderProductInfo = item;
                                    orderProductList[j] = null;
                                    List<OrderProductInfo> giftList2 = new List<OrderProductInfo>();
                                    for (int k = 0; k < count; k++)
                                    {
                                        OrderProductInfo item2 = orderProductList[k];
                                        if (item2 != null && item2.Type == 1 && item2.ExtCode1 == item.ExtCode3)
                                        {
                                            giftList2.Add(item2);
                                            orderProductList[k] = null;
                                        }
                                    }
                                    cartProductInfo2.GiftList = giftList2;

                                    CartItemInfo cartItemInfo2 = new CartItemInfo();
                                    cartItemInfo2.Type = 0;
                                    cartItemInfo2.Item = cartProductInfo2;
                                    cartItemList.Add(cartItemInfo2);
                                }
                            }

                            //更新商品的满减促销活动
                            UpdateOrderProductFullCut(updateFullCutOrderProductList);
                        }

                        #endregion
                    }
                    else//非满赠和满减订单商品处理
                    {
                        #region 非满赠和满减订单商品处理

                        CartProductInfo cartProductInfo = new CartProductInfo();
                        cartProductInfo.Selected = IsSelectCartItem(0, orderProductInfo.Pid, selectedCartItemKeyList);
                        cartProductInfo.OrderProductInfo = orderProductInfo;
                        orderProductList[i] = null;
                        List<OrderProductInfo> giftList = new List<OrderProductInfo>();
                        //获取商品的赠品
                        for (int j = 0; j < count; j++)
                        {
                            OrderProductInfo item = orderProductList[j];
                            if (item != null && item.Type == 1 && item.ExtCode1 == orderProductInfo.ExtCode3)
                            {
                                giftList.Add(item);
                                orderProductList[j] = null;
                            }
                        }
                        cartProductInfo.GiftList = giftList;

                        CartItemInfo cartItemInfo = new CartItemInfo();
                        cartItemInfo.Type = 0;
                        cartItemInfo.Item = cartProductInfo;
                        cartItemList.Add(cartItemInfo);

                        #endregion
                    }
                }
                else if (orderProductInfo.Type == 3)//当商品是套装商品时
                {
                    #region 套装商品处理

                    CartSuitInfo cartSuitInfo = new CartSuitInfo();
                    cartSuitInfo.Checked = IsSelectCartItem(1, orderProductInfo.ExtCode1, selectedCartItemKeyList);
                    cartSuitInfo.PmId = orderProductInfo.ExtCode1;
                    cartSuitInfo.BuyCount = orderProductInfo.RealCount / orderProductInfo.ExtCode2;

                    decimal suitAmount = 0M;
                    List<CartProductInfo> cartProductList = new List<CartProductInfo>();
                    for (int j = 0; j < count; j++)
                    {
                        OrderProductInfo item = orderProductList[j];
                        //获取同一套装商品
                        if (item != null && item.Type == 3 && item.ExtCode1 == orderProductInfo.ExtCode1)
                        {
                            suitAmount += item.DiscountPrice * item.RealCount;

                            CartProductInfo cartProductInfo = new CartProductInfo();
                            cartProductInfo.Selected = cartSuitInfo.Checked;
                            cartProductInfo.OrderProductInfo = item;
                            orderProductList[j] = null;
                            List<OrderProductInfo> giftList = new List<OrderProductInfo>();
                            //获取商品的赠品
                            for (int k = 0; k < count; k++)
                            {
                                OrderProductInfo item2 = orderProductList[k];
                                if (item2 != null && item2.Type == 2 && item2.ExtCode1 == item.ExtCode2)
                                {
                                    giftList.Add(item2);
                                    orderProductList[k] = null;
                                }
                            }
                            cartProductInfo.GiftList = giftList;

                            cartProductList.Add(cartProductInfo);
                        }
                    }
                    cartSuitInfo.SuitPrice = suitAmount / cartSuitInfo.BuyCount;
                    cartSuitInfo.SuitAmount = suitAmount;
                    cartSuitInfo.CartProductList = cartProductList;

                    CartItemInfo cartItemInfo = new CartItemInfo();
                    cartItemInfo.Type = 1;
                    cartItemInfo.Item = cartSuitInfo;
                    cartItemList.Add(cartItemInfo);

                    #endregion
                }
            }
            cartItemList.Sort();

            foreach (CartItemInfo cartItemInfo in cartItemList)
            {
                if (cartItemInfo.Type == 0)
                {
                    CartProductInfo cartProductInfo = (CartProductInfo)cartItemInfo.Item;
                    if (cartProductInfo.Selected)
                    {
                        selectedOrderProductList.Add(cartProductInfo.OrderProductInfo);
                        selectedOrderProductList.AddRange(cartProductInfo.GiftList);
                    }
                    else
                    {
                        remainedOrderProductList.Add(cartProductInfo.OrderProductInfo);
                        remainedOrderProductList.AddRange(cartProductInfo.GiftList);
                    }
                }
                else if (cartItemInfo.Type == 1)
                {
                    CartSuitInfo cartSuitInfo = (CartSuitInfo)cartItemInfo.Item;
                    if (cartSuitInfo.Checked)
                    {
                        foreach (CartProductInfo cartProductInfo in cartSuitInfo.CartProductList)
                        {
                            selectedOrderProductList.Add(cartProductInfo.OrderProductInfo);
                            selectedOrderProductList.AddRange(cartProductInfo.GiftList);
                        }
                    }
                    else
                    {
                        foreach (CartProductInfo cartProductInfo in cartSuitInfo.CartProductList)
                        {
                            remainedOrderProductList.Add(cartProductInfo.OrderProductInfo);
                            remainedOrderProductList.AddRange(cartProductInfo.GiftList);
                        }
                    }
                }
                else if (cartItemInfo.Type == 2)
                {
                    CartFullSendInfo cartFullSendInfo = (CartFullSendInfo)cartItemInfo.Item;
                    if (cartFullSendInfo.FullSendMinorOrderProductInfo != null)
                    {
                        if (cartFullSendInfo.IsEnough)//当金额足够时才添加
                        {
                            selectedOrderProductList.Add(cartFullSendInfo.FullSendMinorOrderProductInfo);
                        }
                        else
                        {
                            remainedOrderProductList.Add(cartFullSendInfo.FullSendMinorOrderProductInfo);
                        }
                    }
                    foreach (CartProductInfo cartProductInfo in cartFullSendInfo.FullSendMainCartProductList)
                    {
                        if (cartProductInfo.Selected)
                        {
                            selectedOrderProductList.Add(cartProductInfo.OrderProductInfo);
                            selectedOrderProductList.AddRange(cartProductInfo.GiftList);
                        }
                        else
                        {
                            remainedOrderProductList.Add(cartProductInfo.OrderProductInfo);
                            remainedOrderProductList.AddRange(cartProductInfo.GiftList);
                        }
                    }
                }
                else if (cartItemInfo.Type == 3)
                {
                    CartFullCutInfo cartFullCutInfo = (CartFullCutInfo)cartItemInfo.Item;
                    foreach (CartProductInfo cartProductInfo in cartFullCutInfo.FullCutCartProductList)
                    {
                        if (cartProductInfo.Selected)
                        {
                            selectedOrderProductList.Add(cartProductInfo.OrderProductInfo);
                            selectedOrderProductList.AddRange(cartProductInfo.GiftList);
                        }
                        else
                        {
                            remainedOrderProductList.Add(cartProductInfo.OrderProductInfo);
                            remainedOrderProductList.AddRange(cartProductInfo.GiftList);
                        }
                    }
                }
            }

            return cartItemList;
        }

        /// <summary>
        /// 获得购物车商品数量
        /// </summary>
        /// <returns></returns>
        public static int GetCartProductCountCookie()
        {
            return TypeHelper.StringToInt(WebHelper.GetCookie("cart", "pcount"));
        }

        /// <summary>
        /// 设置购物车商品数量cookie
        /// </summary>
        /// <param name="count">购物车商品数量</param>
        /// <returns></returns>
        public static void SetCartProductCountCookie(int count)
        {
            if (count < 0) count = 0;
            WebHelper.SetCookie("cart", "pcount", count.ToString(), BSPConfig.ShopConfig.SCExpire * 24 * 60);
        }





        /// <summary>
        /// 汇总订单商品数量
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        /// <returns></returns>
        public static int SumOrderProductCount(List<OrderProductInfo> orderProductList)
        {
            int count = 0;
            foreach (OrderProductInfo orderProductInfo in orderProductList)
                count = count + orderProductInfo.RealCount;
            return count;
        }

        /// <summary>
        /// 获得商品总计
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        /// <returns></returns>
        public static decimal SumOrderProductAmount(List<OrderProductInfo> orderProductList)
        {
            decimal productAmount = 0M;
            foreach (OrderProductInfo orderProductInfo in orderProductList)
                productAmount = productAmount + orderProductInfo.BuyCount * orderProductInfo.DiscountPrice;
            return productAmount;
        }

        /// <summary>
        /// 汇总订单商品重量
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        /// <returns></returns>
        public static int SumOrderProductWeight(List<OrderProductInfo> orderProductList)
        {
            int totalWeight = 0;
            foreach (OrderProductInfo orderProductInfo in orderProductList)
                totalWeight = totalWeight + orderProductInfo.RealCount * orderProductInfo.Weight;
            return totalWeight;
        }

        /// <summary>
        /// 汇总满减
        /// </summary>
        /// <param name="cartItemList">购物车项列表</param>
        /// <returns></returns>
        public static int SumFullCut(List<CartItemInfo> cartItemList)
        {
            //满减
            int fullCut = 0;
            foreach (CartItemInfo cartItemInfo in cartItemList)
            {
                if (cartItemInfo.Type == 3)
                {
                    CartFullCutInfo cartFullCutInfo = (CartFullCutInfo)cartItemInfo.Item;
                    if (cartFullCutInfo.IsEnough)
                        fullCut += cartFullCutInfo.CutMoney;
                }
            }
            return fullCut;
        }

        /// <summary>
        /// 汇总支付积分
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        /// <returns></returns>
        public static int SumPayCredits(List<OrderProductInfo> orderProductList)
        {
            int payCredits = 0;
            foreach (OrderProductInfo orderProductInfo in orderProductList)
                payCredits = payCredits + orderProductInfo.RealCount * orderProductInfo.PayCredits;
            return payCredits;
        }

        /// <summary>
        /// 汇总商品折扣
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        /// <returns></returns>
        public static decimal SumOrderProductDiscount(List<OrderProductInfo> orderProductList)
        {
            decimal discount = 0M;
            foreach (OrderProductInfo orderProductInfo in orderProductList)
                discount += (orderProductInfo.RealCount * orderProductInfo.ShopPrice - orderProductInfo.BuyCount * orderProductInfo.ShopPrice);
            return discount;
        }

        /// <summary>
        /// 汇总优惠劵
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        /// <returns></returns>
        public static Dictionary<int, CouponTypeInfo> SumCouponType(List<OrderProductInfo> orderProductList)
        {
            Dictionary<int, CouponTypeInfo> couponTypeList = new Dictionary<int, CouponTypeInfo>();
            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                if (orderProductInfo.CouponTypeId > 0)
                {
                    CouponTypeInfo couponTypeInfo = Coupons.GetCouponTypeById(orderProductInfo.CouponTypeId);
                    if (couponTypeInfo != null && !couponTypeList.ContainsKey(couponTypeInfo.CouponTypeId))
                    {
                        couponTypeList.Add(couponTypeInfo.CouponTypeId, couponTypeInfo);
                    }
                }
            }
            return couponTypeList;
        }








        /// <summary>
        /// 获得普通订单商品列表
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        /// <returns></returns>
        public static List<OrderProductInfo> GetCommonOrderProductList(List<OrderProductInfo> orderProductList)
        {
            List<OrderProductInfo> commonOrderProductList = new List<OrderProductInfo>();
            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                if (orderProductInfo.Type == 0)
                    commonOrderProductList.Add(orderProductInfo);
            }
            return commonOrderProductList;
        }

        /// <summary>
        /// 从订单商品列表中获得普通商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="orderProductList">订单商品列表</param>
        /// <returns></returns>
        public static OrderProductInfo GetCommonOrderProductByPid(int pid, List<OrderProductInfo> orderProductList)
        {
            foreach (OrderProductInfo info in orderProductList)
            {
                if (info.Type == 0 && info.Pid == pid)
                    return info;
            }
            return null;
        }

        /// <summary>
        /// 从订单商品列表中获得赠品列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="pmId">赠品促销活动id</param>
        /// <param name="orderProductList">订单商品列表</param>
        /// <returns></returns>
        public static List<OrderProductInfo> GetGiftOrderProductList(int type, int pmId, List<OrderProductInfo> orderProductList)
        {
            List<OrderProductInfo> giftOrderProductList = new List<OrderProductInfo>();
            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                if (orderProductInfo.Type == type && orderProductInfo.ExtCode1 == pmId)
                    giftOrderProductList.Add(orderProductInfo);
            }
            return giftOrderProductList;
        }

        /// <summary>
        /// 从订单商品列表中获得指定套装商品列表
        /// </summary>
        /// <param name="pmId">套装促销活动id</param>
        /// <param name="orderProductList">订单商品列表</param>
        /// <param name="isContainGift">是否包含赠品</param>
        /// <returns></returns>
        public static List<OrderProductInfo> GetSuitOrderProductList(int pmId, List<OrderProductInfo> orderProductList, bool isContainGift)
        {
            List<OrderProductInfo> suitOrderProductList = new List<OrderProductInfo>();
            if (isContainGift)
            {
                foreach (OrderProductInfo orderProductInfo in orderProductList)
                {
                    if (orderProductInfo.Type == 3 && orderProductInfo.ExtCode1 == pmId)
                    {
                        suitOrderProductList.Add(orderProductInfo);
                        foreach (OrderProductInfo item in orderProductList)
                        {
                            if (item.Type == 2 && item.ExtCode1 == orderProductInfo.ExtCode2)
                                suitOrderProductList.Add(item);
                        }
                    }
                }
            }
            else
            {
                foreach (OrderProductInfo orderProductInfo in orderProductList)
                {
                    if (orderProductInfo.Type == 3 && orderProductInfo.ExtCode1 == pmId)
                        suitOrderProductList.Add(orderProductInfo);
                }
            }
            return suitOrderProductList;
        }

        /// <summary>
        /// 从订单商品列表中获得指定的满赠主商品列表
        /// </summary>
        /// <param name="pmId">满赠促销活动id</param>
        /// <param name="orderProductList">订单商品列表</param>
        /// <returns></returns>
        public static List<OrderProductInfo> GetFullSendMainOrderProductList(int pmId, List<OrderProductInfo> orderProductList)
        {
            List<OrderProductInfo> list = new List<OrderProductInfo>();
            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                if (orderProductInfo.Type == 0 && orderProductInfo.ExtCode4 == pmId)
                    list.Add(orderProductInfo);
            }
            return list;
        }

        /// <summary>
        /// 从订单商品列表中获的满赠次商品列表
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        /// <returns></returns>
        public static List<OrderProductInfo> GetFullSendMinorOrderProductList(List<OrderProductInfo> orderProductList)
        {
            List<OrderProductInfo> list = new List<OrderProductInfo>();
            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                if (orderProductInfo.Type == 4)
                    list.Add(orderProductInfo);
            }
            return list;
        }

        /// <summary>
        /// 从订单商品列表中获得指定的满赠次商品
        /// </summary>
        /// <param name="pmId">满赠促销活动id</param>
        /// <param name="orderProductList">订单商品列表</param>
        /// <returns></returns>
        public static OrderProductInfo GetFullSendMinorOrderProduct(int pmId, List<OrderProductInfo> orderProductList)
        {
            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                if (orderProductInfo.Type == 4 && orderProductInfo.ExtCode1 == pmId)
                    return orderProductInfo;
            }
            return null;
        }

        /// <summary>
        /// 从订单商品列表中获得指定满减商品列表
        /// </summary>
        /// <param name="pmId">满减促销活动id</param>
        /// <param name="orderProductList">订单商品列表</param>
        /// <returns></returns>
        public static List<OrderProductInfo> GetFullCutOrderProductList(int pmId, List<OrderProductInfo> orderProductList)
        {
            List<OrderProductInfo> fullCutOrderProductList = new List<OrderProductInfo>();
            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                if (orderProductInfo.Type == 0 && orderProductInfo.ExtCode5 == pmId)
                    fullCutOrderProductList.Add(orderProductInfo);
            }
            return fullCutOrderProductList;
        }

        /// <summary>
        /// 从订单商品列表中获得指定商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="orderProductList">订单商品列表</param>
        /// <returns></returns>
        public static OrderProductInfo GetOrderProductByPid(int pid, List<OrderProductInfo> orderProductList)
        {
            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                if (orderProductInfo.Pid == pid)
                    return orderProductInfo;
            }
            return null;
        }

        /// <summary>
        /// 获得商品id列表
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        /// <returns></returns>
        public static string GetPidList(List<OrderProductInfo> orderProductList)
        {
            if (orderProductList.Count == 0)
                return string.Empty;

            if (orderProductList.Count == 1)
                return orderProductList[0].Pid.ToString();

            List<int> pidList = new List<int>();
            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                pidList.Add(orderProductInfo.Pid);
            }

            StringBuilder sb = new StringBuilder();
            foreach (int pid in pidList.Distinct<int>())
                sb.AppendFormat("{0},", pid);
            return CommonHelper.GetUniqueString(sb.Remove(sb.Length - 1, 1).ToString());
        }









        /// <summary>
        /// 设置订单商品的买送促销活动
        /// </summary>
        /// <param name="orderProductInfo">订单商品</param>
        /// <param name="buyCount">购买数量</param>
        /// <param name="buySendPromotionInfo">买送优惠活动</param>
        public static void SetBuySendPromotionOfOrderProduct(OrderProductInfo orderProductInfo, int buyCount, BuySendPromotionInfo buySendPromotionInfo)
        {
            orderProductInfo.RealCount = buyCount + (buyCount / buySendPromotionInfo.BuyCount) * buySendPromotionInfo.SendCount;
            orderProductInfo.BuyCount = buyCount;
            orderProductInfo.ExtCode2 = buySendPromotionInfo.PmId;
        }

        /// <summary>
        /// 设置订单商品的单品促销活动
        /// </summary>
        /// <param name="orderProductInfo">订单商品</param>
        /// <param name="singlePromotionInfo">单品促销活动</param>
        public static void SetSinglePromotionOfOrderProduct(OrderProductInfo orderProductInfo, SinglePromotionInfo singlePromotionInfo)
        {
            orderProductInfo.ExtCode1 = singlePromotionInfo.PmId;
            switch (singlePromotionInfo.DiscountType)
            {
                case 0://折扣
                    {
                        decimal temp = Math.Ceiling((orderProductInfo.ShopPrice * singlePromotionInfo.DiscountValue) / 10);
                        orderProductInfo.DiscountPrice = temp < 0 ? orderProductInfo.ShopPrice : temp;
                        break;
                    }
                case 1://直降
                    {
                        decimal temp = orderProductInfo.ShopPrice - singlePromotionInfo.DiscountValue;
                        orderProductInfo.DiscountPrice = temp < 0 ? orderProductInfo.ShopPrice : temp;
                        break;
                    }
                case 2://折后价
                    {
                        orderProductInfo.DiscountPrice = singlePromotionInfo.DiscountValue;
                        break;
                    }
            }
            //设置赠送积分
            if (singlePromotionInfo.PayCredits > 0)
                orderProductInfo.PayCredits = singlePromotionInfo.PayCredits;

            //设置赠送优惠劵
            if (singlePromotionInfo.CouponTypeId > 0)
                orderProductInfo.CouponTypeId = singlePromotionInfo.CouponTypeId;
        }

        /// <summary>
        /// 设置赠品订单商品
        /// </summary>
        /// <param name="orderProductInfo">订单商品</param>
        /// <param name="type">类型</param>
        /// <param name="buyCount">购买数量</param>
        /// <param name="number">赠送数量</param>
        /// <param name="pmId">赠品促销活动id</param>
        public static void SetGiftOrderProduct(OrderProductInfo orderProductInfo, int type, int buyCount, int number, int pmId)
        {
            orderProductInfo.DiscountPrice = 0M;
            orderProductInfo.RealCount = buyCount * number;
            orderProductInfo.BuyCount = 0;
            orderProductInfo.Type = type;
            orderProductInfo.ExtCode1 = pmId;
            orderProductInfo.ExtCode2 = number;
        }

        /// <summary>
        /// 设置订单商品的满赠促销活动
        /// </summary>
        /// <param name="orderProductInfo">订单商品</param>
        /// <param name="fullSendPromotionInfo">满赠促销活动</param>
        public static void SetFullSendPromotionOfOrderProduct(OrderProductInfo orderProductInfo, FullSendPromotionInfo fullSendPromotionInfo)
        {
            orderProductInfo.ExtCode4 = fullSendPromotionInfo.PmId;
        }

        /// <summary>
        /// 设置订单商品的满减促销活动
        /// </summary>
        /// <param name="orderProductInfo">订单商品</param>
        /// <param name="fullCutPromotionInfo">满减促销活动</param>
        public static void SetFullCutPromotionOfOrderProduct(OrderProductInfo orderProductInfo, FullCutPromotionInfo fullCutPromotionInfo)
        {
            orderProductInfo.ExtCode5 = fullCutPromotionInfo.PmId;
        }

        /// <summary>
        /// 更新订单商品的买送促销活动
        /// </summary>
        /// <param name="orderProductInfo">订单商品</param>
        /// <param name="buyCount">购买数量</param>
        /// <param name="buyTime">购买时间</param>
        public static void UpdateBuySendPromotionOfOrderProduct(OrderProductInfo orderProductInfo, int buyCount, DateTime buyTime)
        {
            //获得商品的买送促销活动
            BuySendPromotionInfo buySendPromotionInfo = Promotions.GetBuySendPromotion(buyCount, orderProductInfo.Pid, buyTime);

            if (buySendPromotionInfo == null && orderProductInfo.ExtCode2 > 0)//当商品存在买送促销活动但添加后不存在买送促销活动时
            {
                orderProductInfo.RealCount = buyCount;
                orderProductInfo.BuyCount = buyCount;
                orderProductInfo.ExtCode2 = 0;
            }
            else if (buySendPromotionInfo != null && orderProductInfo.ExtCode2 <= 0)//当商品不存在买送促销活动但添加后存在买送促销活动时
            {
                SetBuySendPromotionOfOrderProduct(orderProductInfo, buyCount, buySendPromotionInfo);
            }
            else if (buySendPromotionInfo != null && orderProductInfo.ExtCode2 > 0)//当商品存在买送促销活动但添加后仍然满足买送促销活动时
            {
                SetBuySendPromotionOfOrderProduct(orderProductInfo, buyCount, buySendPromotionInfo);
            }
            else
            {
                orderProductInfo.RealCount = buyCount;
                orderProductInfo.BuyCount = buyCount;
            }
        }

        /// <summary>
        /// 更新赠品订单商品
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        /// <param name="orderProductInfo">订单商品</param>
        /// <param name="buyCount">购买数量</param>
        /// <returns></returns>
        public static List<OrderProductInfo> UpdateGiftOrderProduct(List<OrderProductInfo> orderProductList, OrderProductInfo orderProductInfo, int buyCount)
        {
            if (orderProductInfo.ExtCode3 < 1)
                return new List<OrderProductInfo>();

            //获得赠品订单商品列表
            List<OrderProductInfo> giftOrderProductList = GetGiftOrderProductList(1, orderProductInfo.ExtCode3, orderProductList);
            foreach (OrderProductInfo item in giftOrderProductList)
                item.RealCount = buyCount * item.ExtCode2;
            return giftOrderProductList;
        }

        /// <summary>
        /// 设置套装订单商品
        /// </summary>
        /// <param name="orderProductInfo">订单商品</param>
        /// <param name="buyCount">购买数量</param>
        /// <param name="number">数量</param>
        /// <param name="discount">折扣值</param>
        /// <param name="suitPromotionInfo">套装促销活动</param>
        public static void SetSuitOrderProduct(OrderProductInfo orderProductInfo, int buyCount, int number, int discount, SuitPromotionInfo suitPromotionInfo)
        {
            orderProductInfo.Type = 3;
            orderProductInfo.ExtCode1 = suitPromotionInfo.PmId;
            orderProductInfo.ExtCode2 = number;
            orderProductInfo.RealCount = number * buyCount;
            orderProductInfo.BuyCount = number * buyCount;
            orderProductInfo.DiscountPrice = orderProductInfo.ShopPrice - discount;
        }

        /// <summary>
        /// 设置满赠订单商品
        /// </summary>
        /// <param name="orderProductInfo">订单商品</param>
        /// <param name="fullSendPromotionInfo">满赠促销活动</param>
        public static void SetFullSendOrderProduct(OrderProductInfo orderProductInfo, FullSendPromotionInfo fullSendPromotionInfo)
        {
            orderProductInfo.RealCount = 1;
            orderProductInfo.BuyCount = 1;
            orderProductInfo.DiscountPrice = fullSendPromotionInfo.AddMoney;
            orderProductInfo.Type = 4;
            orderProductInfo.ExtCode1 = fullSendPromotionInfo.PmId;
        }






        /// <summary>
        /// 删除购物车中的商品
        /// </summary>
        /// <param name="orderProductList">购物车中商品列表</param>
        /// <param name="orderProductInfo">删除商品</param>
        public static void DeleteCartProduct(ref List<OrderProductInfo> orderProductList, OrderProductInfo orderProductInfo)
        {
            //需要删除的商品列表
            List<OrderProductInfo> delOrderProductList = new List<OrderProductInfo>();

            //将主商品添加到需要删除的商品列表中
            delOrderProductList.Add(orderProductInfo);

            if (orderProductInfo.ExtCode3 > 0)
            {
                //赠品商品列表
                List<OrderProductInfo> giftOrderProductList = GetGiftOrderProductList(1, orderProductInfo.ExtCode3, orderProductList);
                //将赠品添加到需要删除的商品列表中
                delOrderProductList.AddRange(giftOrderProductList);
            }

            if (orderProductInfo.ExtCode4 > 0)
            {
                //满赠赠品
                OrderProductInfo fullSendMinorOrderProductInfo = GetFullSendMinorOrderProduct(orderProductInfo.ExtCode4, orderProductList);
                if (fullSendMinorOrderProductInfo != null)
                {
                    FullSendPromotionInfo fullSendPromotionInfo = Promotions.GetFullSendPromotionByPmIdAndTime(orderProductInfo.ExtCode4, DateTime.Now);
                    if (fullSendPromotionInfo != null)
                    {
                        List<OrderProductInfo> fullSendMainOrderProductList = GetFullSendMainOrderProductList(orderProductInfo.ExtCode4, orderProductList);
                        decimal amount = SumOrderProductAmount(fullSendMainOrderProductList) - orderProductInfo.DiscountPrice * orderProductInfo.BuyCount;
                        if (amount < fullSendPromotionInfo.LimitMoney || fullSendMinorOrderProductInfo.DiscountPrice != fullSendPromotionInfo.AddMoney)
                            delOrderProductList.Add(fullSendMinorOrderProductInfo);
                    }
                    else
                    {
                        delOrderProductList.Add(fullSendMinorOrderProductInfo);
                    }
                }
            }

            //删除商品
            DeleteOrderProductList(delOrderProductList);
            foreach (OrderProductInfo item in delOrderProductList)
                orderProductList.Remove(item);
        }

        /// <summary>
        /// 添加已经存在的商品到购物车中
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        /// <param name="buyCount">购买数量</param>
        /// <param name="orderProductInfo">订单商品信息</param>
        /// <param name="buyTime">购买时间</param>
        public static void AddExistProductToCart(ref List<OrderProductInfo> orderProductList, int buyCount, OrderProductInfo orderProductInfo, DateTime buyTime)
        {
            List<OrderProductInfo> updateOrderProductList = new List<OrderProductInfo>();

            //更新买送促销活动
            UpdateBuySendPromotionOfOrderProduct(orderProductInfo, buyCount, buyTime);
            //更新订单商品的赠品促销活动
            List<OrderProductInfo> giftOrderProductList = UpdateGiftOrderProduct(orderProductList, orderProductInfo, buyCount);

            updateOrderProductList.Add(orderProductInfo);
            updateOrderProductList.AddRange(giftOrderProductList);

            UpdateOrderProductCount(updateOrderProductList);
        }

        /// <summary>
        /// 添加新商品到购物车
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        /// <param name="buyCount">购买数量</param>
        /// <param name="partProductInfo">购买商品</param>
        /// <param name="sid">用户sessionId</param>
        /// <param name="uid">用户id</param>
        /// <param name="buyTime">购买时间</param>
        public static void AddNewProductToCart(ref List<OrderProductInfo> orderProductList, int buyCount, PartProductInfo partProductInfo, string sid, int uid, DateTime buyTime)
        {
            //需要添加的商品列表
            List<OrderProductInfo> addOrderProductList = new List<OrderProductInfo>();

            //初始化订单商品
            OrderProductInfo mainOrderProductInfo = BuildOrderProduct(partProductInfo);
            InitOrderProduct(mainOrderProductInfo, buyCount, sid, uid, buyTime);

            //获得买送促销活动
            BuySendPromotionInfo buySendPromotionInfo = Promotions.GetBuySendPromotion(buyCount, partProductInfo.Pid, buyTime);
            //当买送促销活动存在时设置订单商品信息
            if (buySendPromotionInfo != null)
                SetBuySendPromotionOfOrderProduct(mainOrderProductInfo, buyCount, buySendPromotionInfo);

            //获得单品促销活动
            SinglePromotionInfo singlePromotionInfo = Promotions.GetSinglePromotionByPidAndTime(partProductInfo.Pid, buyTime);
            //当单品促销活动存在时则设置订单商品信息
            if (singlePromotionInfo != null)
                SetSinglePromotionOfOrderProduct(mainOrderProductInfo, singlePromotionInfo);

            //获得满赠促销活动
            FullSendPromotionInfo fullSendPromotionInfo = Promotions.GetFullSendPromotionByPidAndTime(partProductInfo.Pid, buyTime);
            if (fullSendPromotionInfo != null)
                SetFullSendPromotionOfOrderProduct(mainOrderProductInfo, fullSendPromotionInfo);

            //获得满减促销活动
            FullCutPromotionInfo fullCutPromotionInfo = Promotions.GetFullCutPromotionByPidAndTime(partProductInfo.Pid, buyTime);
            if (fullCutPromotionInfo != null)
                SetFullCutPromotionOfOrderProduct(mainOrderProductInfo, fullCutPromotionInfo);


            //将商品添加到"需要添加的商品列表"中
            addOrderProductList.Add(mainOrderProductInfo);

            //获得赠品列表
            GiftPromotionInfo giftPromotionInfo = Promotions.GetGiftPromotionByPidAndTime(partProductInfo.Pid, buyTime);
            if (giftPromotionInfo != null)
            {
                List<ExtGiftInfo> extGiftList = Promotions.GetExtGiftList(giftPromotionInfo.PmId);
                if (extGiftList.Count > 0)
                {
                    mainOrderProductInfo.ExtCode3 = giftPromotionInfo.PmId;
                    foreach (ExtGiftInfo extGiftInfo in extGiftList)
                    {
                        OrderProductInfo giftOrderProduct = BuildOrderProduct(extGiftInfo);
                        InitOrderProduct(giftOrderProduct, 0, sid, uid, buyTime);
                        SetGiftOrderProduct(giftOrderProduct, 1, mainOrderProductInfo.RealCount, extGiftInfo.Number, giftPromotionInfo.PmId);
                        //将赠品添加到"需要添加的商品列表"中
                        addOrderProductList.Add(giftOrderProduct);
                    }
                }
            }

            //将需要添加的商品持久化
            AddOrderProductList(addOrderProductList);

            orderProductList.AddRange(addOrderProductList);
        }

        /// <summary>
        /// 将商品添加到购物车
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        /// <param name="buyCount">购买数量</param>
        /// <param name="partProductInfo">购买商品</param>
        /// <param name="sid">用户sessionId</param>
        /// <param name="uid">用户id</param>
        /// <param name="buyTime">购买时间</param>
        public static void AddProductToCart(ref List<OrderProductInfo> orderProductList, int buyCount, PartProductInfo partProductInfo, string sid, int uid, DateTime buyTime)
        {
            if (orderProductList.Count == 0)
            {
                AddNewProductToCart(ref orderProductList, buyCount, partProductInfo, sid, uid, buyTime);
            }
            else
            {
                OrderProductInfo orderProductInfo = GetCommonOrderProductByPid(partProductInfo.Pid, orderProductList);
                if (orderProductInfo == null)//此商品作为普通商品不存在于购物车中时
                    AddNewProductToCart(ref orderProductList, buyCount, partProductInfo, sid, uid, buyTime);
                else//此商品作为普通商品存在于购物车中时
                    AddExistProductToCart(ref orderProductList, buyCount, orderProductInfo, buyTime);
            }
        }

        /// <summary>
        /// 删除购物车中的套装
        /// </summary>
        /// <param name="orderProductList">购物车商品列表</param>
        /// <param name="pmId">套装促销活动id</param>
        public static void DeleteCartSuit(ref List<OrderProductInfo> orderProductList, int pmId)
        {
            //需要删除的商品列表
            List<OrderProductInfo> delOrderProductList = new List<OrderProductInfo>();

            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                if (orderProductInfo.Type == 3 && orderProductInfo.ExtCode1 == pmId)
                {
                    delOrderProductList.Add(orderProductInfo);
                    if (orderProductInfo.ExtCode3 > 0)
                    {
                        delOrderProductList.AddRange(GetGiftOrderProductList(2, orderProductInfo.ExtCode3, orderProductList));
                    }
                }
            }

            //删除商品
            DeleteOrderProductList(delOrderProductList);
            foreach (OrderProductInfo orderProductInfo in delOrderProductList)
                orderProductList.Remove(orderProductInfo);
        }

        /// <summary>
        /// 添加已经存在的套装到购物车中
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        /// <param name="suitOrderProductList">套装商品列表</param>
        /// <param name="buyCount">购买数量</param>
        public static void AddExistSuitToCart(ref List<OrderProductInfo> orderProductList, List<OrderProductInfo> suitOrderProductList, int buyCount)
        {
            List<OrderProductInfo> list = new List<OrderProductInfo>();

            foreach (OrderProductInfo orderProductInfo in suitOrderProductList)
            {
                if (orderProductInfo.Type == 3)
                {
                    orderProductInfo.RealCount = buyCount * orderProductInfo.ExtCode2;
                    list.Add(orderProductInfo);
                    if (orderProductInfo.ExtCode3 > 0)
                    {
                        foreach (OrderProductInfo item in suitOrderProductList)
                        {
                            if (item.Type == 2)
                            {
                                item.RealCount = orderProductInfo.RealCount * item.ExtCode2;
                                list.Add(item);
                            }
                        }
                    }
                }
            }

            UpdateOrderProductCount(list);
        }

        /// <summary>
        /// 添加新套装到购物车中
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        /// <param name="extSuitProductList">扩展套装商品列表</param>
        /// <param name="suitPromotionInfo">套装促销活动信息</param>
        /// <param name="buyCount">购买数量</param>
        /// <param name="sid">用户sessionId</param>
        /// <param name="uid">用户id</param>
        /// <param name="buyTime">购买时间</param>
        public static void AddNewSuitToCart(ref List<OrderProductInfo> orderProductList, List<ExtSuitProductInfo> extSuitProductList, SuitPromotionInfo suitPromotionInfo, int buyCount, string sid, int uid, DateTime buyTime)
        {
            List<OrderProductInfo> addOrderProductList = new List<OrderProductInfo>();

            foreach (ExtSuitProductInfo extSuitProductInfo in extSuitProductList)
            {
                OrderProductInfo suitOrderProductInfo = BuildOrderProduct(extSuitProductInfo);
                InitOrderProduct(suitOrderProductInfo, 0, sid, uid, buyTime);
                SetSuitOrderProduct(suitOrderProductInfo, buyCount, extSuitProductInfo.Number, extSuitProductInfo.Discount, suitPromotionInfo);

                addOrderProductList.Add(suitOrderProductInfo);

                //获得赠品列表
                GiftPromotionInfo giftPromotionInfo = Promotions.GetGiftPromotionByPidAndTime(suitOrderProductInfo.Pid, buyTime);
                if (giftPromotionInfo != null)
                {
                    List<ExtGiftInfo> extGiftList = Promotions.GetExtGiftList(giftPromotionInfo.PmId);
                    if (extGiftList.Count > 0)
                    {
                        suitOrderProductInfo.ExtCode3 = giftPromotionInfo.PmId;
                        foreach (ExtGiftInfo extGiftInfo in extGiftList)
                        {
                            OrderProductInfo giftOrderProduct = BuildOrderProduct(extGiftInfo);
                            InitOrderProduct(giftOrderProduct, 0, sid, uid, buyTime);
                            SetGiftOrderProduct(giftOrderProduct, 2, suitOrderProductInfo.RealCount, extGiftInfo.Number, giftPromotionInfo.PmId);
                            //将赠品添加到"需要添加的商品列表"中
                            addOrderProductList.Add(giftOrderProduct);
                        }
                    }
                }
            }

            //将需要添加的商品持久化
            AddOrderProductList(addOrderProductList);

            orderProductList.AddRange(addOrderProductList);
        }

        /// <summary>
        /// 添加套装到购物车
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        /// <param name="extSuitProductList">扩展套装商品列表</param>
        /// <param name="suitPromotionInfo">套装促销活动</param>
        /// <param name="buyCount">购买数量</param>
        /// <param name="sid">用户sessionId</param>
        /// <param name="uid">用户id</param>
        /// <param name="buyTime">购买时间</param>
        public static void AddSuitToCart(ref List<OrderProductInfo> orderProductList, List<ExtSuitProductInfo> extSuitProductList, SuitPromotionInfo suitPromotionInfo, int buyCount, string sid, int uid, DateTime buyTime)
        {
            //套装商品列表
            List<OrderProductInfo> suitOrderProductList = GetSuitOrderProductList(suitPromotionInfo.PmId, orderProductList, true);
            if (suitOrderProductList.Count < 1)
                AddNewSuitToCart(ref orderProductList, extSuitProductList, suitPromotionInfo, buyCount, sid, uid, buyTime);
            else
                AddExistSuitToCart(ref orderProductList, suitOrderProductList, buyCount);
        }

        /// <summary>
        /// 删除购物车中的满赠
        /// </summary>
        /// <param name="orderProductList">购物车商品列表</param>
        /// <param name="pmId">满赠促销活动id</param>
        public static void DeleteCartFullSend(ref List<OrderProductInfo> orderProductList, int pmId)
        {
            //赠送商品
            OrderProductInfo fullSendMinorOrderProductInfo = GetFullSendMinorOrderProduct(pmId, orderProductList);
            if (fullSendMinorOrderProductInfo != null)
            {
                orderProductList.Remove(fullSendMinorOrderProductInfo);
                DeleteOrderProductList(new List<OrderProductInfo>() { fullSendMinorOrderProductInfo });
            }
        }

        /// <summary>
        /// 删除购物车中的满赠
        /// </summary>
        /// <param name="orderProductList">购物车商品列表</param>
        /// <param name="fullSendMinorOrderProductInfo">赠送商品</param>
        public static void DeleteCartFullSend(ref List<OrderProductInfo> orderProductList, OrderProductInfo fullSendMinorOrderProductInfo)
        {
            orderProductList.Remove(fullSendMinorOrderProductInfo);
            DeleteOrderProductList(new List<OrderProductInfo>() { fullSendMinorOrderProductInfo });
        }

        /// <summary>
        /// 添加满赠到购物车中
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        /// <param name="partProductInfo">购买商品</param>
        /// <param name="fullSendPromotionInfo">满赠促销活动</param>
        /// <param name="sid">用户sessionId</param>
        /// <param name="uid">用户id</param>
        /// <param name="buyTime">购买时间</param>
        public static void AddFullSendToCart(ref List<OrderProductInfo> orderProductList, PartProductInfo partProductInfo, FullSendPromotionInfo fullSendPromotionInfo, string sid, int uid, DateTime buyTime)
        {
            OrderProductInfo orderProductInfo = BuildOrderProduct(partProductInfo);
            InitOrderProduct(orderProductInfo, 0, sid, uid, buyTime);
            SetFullSendOrderProduct(orderProductInfo, fullSendPromotionInfo);
            AddOrderProductList(new List<OrderProductInfo>() { orderProductInfo });
            orderProductList.Add(orderProductInfo);
        }






        /// <summary>
        /// 创建OrderProductInfo
        /// </summary>
        public static OrderProductInfo BuildOrderProduct(PartProductInfo partProuctInfo)
        {
            OrderProductInfo orderProductInfo = new OrderProductInfo();
            orderProductInfo.Pid = partProuctInfo.Pid;
            orderProductInfo.PSN = partProuctInfo.PSN;
            orderProductInfo.CateId = partProuctInfo.CateId;
            orderProductInfo.BrandId = partProuctInfo.BrandId;
            orderProductInfo.Name = partProuctInfo.Name;
            orderProductInfo.DiscountPrice = partProuctInfo.ShopPrice;
            orderProductInfo.ShopPrice = partProuctInfo.ShopPrice;
            orderProductInfo.MarketPrice = partProuctInfo.MarketPrice;
            orderProductInfo.CostPrice = partProuctInfo.CostPrice;
            orderProductInfo.Weight = partProuctInfo.Weight;
            orderProductInfo.ShowImg = partProuctInfo.ShowImg;
            return orderProductInfo;
        }

        /// <summary>
        /// 创建OrderProductInfo
        /// </summary>
        public static OrderProductInfo BuildOrderProduct(ExtGiftInfo extGiftInfo)
        {
            OrderProductInfo orderProductInfo = new OrderProductInfo();
            orderProductInfo.Pid = extGiftInfo.Pid;
            orderProductInfo.PSN = extGiftInfo.PSN;
            orderProductInfo.CateId = extGiftInfo.CateId;
            orderProductInfo.BrandId = extGiftInfo.BrandId;
            orderProductInfo.Name = extGiftInfo.Name;
            orderProductInfo.DiscountPrice = extGiftInfo.ShopPrice;
            orderProductInfo.ShopPrice = extGiftInfo.ShopPrice;
            orderProductInfo.MarketPrice = extGiftInfo.MarketPrice;
            orderProductInfo.CostPrice = extGiftInfo.CostPrice;
            orderProductInfo.Weight = extGiftInfo.Weight;
            orderProductInfo.ShowImg = extGiftInfo.ShowImg;
            return orderProductInfo;
        }

        /// <summary>
        /// 创建OrderProductInfo
        /// </summary>
        public static OrderProductInfo BuildOrderProduct(ExtSuitProductInfo extSuitProductInfo)
        {
            OrderProductInfo orderProductInfo = new OrderProductInfo();
            orderProductInfo.Pid = extSuitProductInfo.Pid;
            orderProductInfo.PSN = extSuitProductInfo.PSN;
            orderProductInfo.CateId = extSuitProductInfo.CateId;
            orderProductInfo.BrandId = extSuitProductInfo.BrandId;
            orderProductInfo.Name = extSuitProductInfo.Name;
            orderProductInfo.DiscountPrice = extSuitProductInfo.ShopPrice;
            orderProductInfo.ShopPrice = extSuitProductInfo.ShopPrice;
            orderProductInfo.MarketPrice = extSuitProductInfo.MarketPrice;
            orderProductInfo.CostPrice = extSuitProductInfo.CostPrice;
            orderProductInfo.Weight = extSuitProductInfo.Weight;
            orderProductInfo.ShowImg = extSuitProductInfo.ShowImg;
            return orderProductInfo;
        }

        /// <summary>
        /// 初始化订单商品
        /// </summary>
        private static void InitOrderProduct(OrderProductInfo orderProductInfo, int buyCount, string sid, int uid, DateTime buyTime)
        {
            if (uid > 0)
                orderProductInfo.Sid = "";
            else
                orderProductInfo.Sid = sid;
            orderProductInfo.Uid = uid;
            orderProductInfo.RealCount = buyCount;
            orderProductInfo.BuyCount = buyCount;
            orderProductInfo.AddTime = buyTime;
        }
    }
}
