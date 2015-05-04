using System;
using System.Text;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 订单操作管理类
    /// </summary>
    public partial class Orders
    {
        private static IOrderStrategy _iorderstrategy = BSPOrder.Instance;//订单策略

        private static object _locker = new object();//锁对象

        private static string _osnformat;//订单编号格式

        static Orders()
        {
            _osnformat = BSPConfig.ShopConfig.OSNFormat;
        }

        /// <summary>
        /// 重置订单编号格式
        /// </summary>
        public static void ResetOSNFormat()
        {
            lock (_locker)
            {
                _osnformat = BSPConfig.ShopConfig.OSNFormat;
            }
        }

        /// <summary>
        /// 生成订单编号
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="shipRegionId">配送区域id</param>
        /// <param name="addTime">下单时间</param>
        /// <returns>订单编号</returns>
        private static string GenerateOSN(int uid, int shipRegionId, DateTime addTime)
        {
            StringBuilder osn = new StringBuilder(_osnformat);
            osn.Replace("{uid}", uid.ToString());
            osn.Replace("{srid}", shipRegionId.ToString());
            osn.Replace("{addtime}", addTime.ToString("yyyyMMddHHmmss"));
            return osn.ToString();
        }

        /// <summary>
        /// 从订单商品列表中获得指定订单的商品列表
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="orderProductList">订单商品列表</param>
        /// <returns></returns>
        public static List<OrderProductInfo> GetOrderProductList(int oid, List<OrderProductInfo> orderProductList)
        {
            List<OrderProductInfo> list = new List<OrderProductInfo>();
            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                if (orderProductInfo.Oid == oid)
                    list.Add(orderProductInfo);
            }
            return list;
        }

        /// <summary>
        /// 获得订单信息
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns>订单信息</returns>
        public static OrderInfo GetOrderByOid(int oid)
        {
            if (oid > 0)
                return BrnShop.Data.Orders.GetOrderByOid(oid);
            else
                return null;
        }

        /// <summary>
        /// 获得订单信息
        /// </summary>
        /// <param name="osn">订单编号</param>
        /// <returns>订单信息</returns>
        public static OrderInfo GetOrderByOSN(string osn)
        {
            if (!string.IsNullOrWhiteSpace(osn))
                return BrnShop.Data.Orders.GetOrderByOSN(osn);
            return null;
        }

        /// <summary>
        /// 根据下单时间获得订单数量
        /// </summary>
        /// <param name="orderState">订单状态</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static int GetOrderCountByOrderStateAndAddTime(int orderState, string startTime, string endTime)
        {
            return BrnShop.Data.Orders.GetOrderCountByOrderStateAndAddTime(orderState, startTime, endTime);
        }

        /// <summary>
        /// 获得订单商品列表
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static List<OrderProductInfo> GetOrderProductList(int oid)
        {
            return BrnShop.Data.Orders.GetOrderProductList(oid);
        }

        /// <summary>
        /// 获得订单商品列表
        /// </summary>
        /// <param name="oidList">订单id列表</param>
        /// <returns></returns>
        public static List<OrderProductInfo> GetOrderProductList(string oidList)
        {
            if (!string.IsNullOrEmpty(oidList))
                return BrnShop.Data.Orders.GetOrderProductList(oidList);
            return new List<OrderProductInfo>();
        }

        #region 订单操作

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="partUserInfo">用户信息</param>
        /// <param name="orderProductList">订单商品列表</param>
        /// <param name="singlePromotionList">单品促销活动列表</param>
        /// <param name="fullShipAddressInfo">配送地址</param>
        /// <param name="payPluginInfo">支付方式</param>
        /// <param name="shipPluginInfo">配送方式</param>
        /// <param name="payCreditCount">支付积分数</param>
        /// <param name="couponList">优惠劵列表</param>
        /// <param name="fullCut">满减</param>
        /// <param name="buyerRemark">买家备注</param>
        /// <param name="bestTime">最佳配送时间</param>
        /// <param name="ip">ip地址</param>
        /// <returns>订单信息</returns>
        public static OrderInfo CreateOrder(PartUserInfo partUserInfo, List<OrderProductInfo> orderProductList, List<SinglePromotionInfo> singlePromotionList, FullShipAddressInfo fullShipAddressInfo, PluginInfo payPluginInfo, PluginInfo shipPluginInfo, int payCreditCount, List<CouponInfo> couponList, int fullCut, string buyerRemark, DateTime bestTime, string ip)
        {
            DateTime nowTime = DateTime.Now;
            IShipPlugin shipPlugin = (IShipPlugin)shipPluginInfo.Instance;
            IPayPlugin payPlugin = (IPayPlugin)payPluginInfo.Instance;

            OrderInfo orderInfo = new OrderInfo();

            orderInfo.OSN = GenerateOSN(partUserInfo.Uid, fullShipAddressInfo.RegionId, nowTime);
            orderInfo.Uid = partUserInfo.Uid;

            orderInfo.Weight = Carts.SumOrderProductWeight(orderProductList);
            orderInfo.ProductAmount = Carts.SumOrderProductAmount(orderProductList);
            orderInfo.FullCut = fullCut;

            decimal amount = orderInfo.ProductAmount - orderInfo.FullCut;
            orderInfo.ShipFee = shipPlugin.GetShipFee(orderInfo.Weight, amount, orderProductList, nowTime, fullShipAddressInfo.ProvinceId, fullShipAddressInfo.CityId, fullShipAddressInfo.RegionId, partUserInfo);
            if (payPlugin.PayMode == 0)
                orderInfo.PayFee = shipPlugin.GetCODPayFee(amount, nowTime, fullShipAddressInfo.ProvinceId, fullShipAddressInfo.CityId, fullShipAddressInfo.RegionId, partUserInfo);
            else
                orderInfo.PayFee = payPlugin.GetPayFee(amount, nowTime, partUserInfo);

            orderInfo.OrderAmount = orderInfo.ProductAmount - orderInfo.FullCut + orderInfo.ShipFee + orderInfo.PayFee;
            orderInfo.PayCreditCount = payCreditCount;
            orderInfo.PayCreditMoney = Credits.PayCreditsToMoney(payCreditCount);
            orderInfo.CouponMoney = Coupons.SumCouponMoney(couponList);
            orderInfo.SurplusMoney = orderInfo.OrderAmount - orderInfo.PayCreditMoney - orderInfo.CouponMoney;

            orderInfo.OrderState = (orderInfo.SurplusMoney <= 0 || payPlugin.PayMode == 0) ? (int)OrderState.Confirming : (int)OrderState.WaitPaying;

            orderInfo.ParentId = 0;
            orderInfo.IsReview = 0;
            orderInfo.AddTime = nowTime;
            orderInfo.ShipSystemName = shipPluginInfo.SystemName;
            orderInfo.ShipFriendName = shipPluginInfo.FriendlyName;
            orderInfo.ShipTime = new DateTime(1900, 1, 1);
            orderInfo.PaySystemName = payPluginInfo.SystemName;
            orderInfo.PayFriendName = payPluginInfo.FriendlyName;
            orderInfo.PayMode = payPlugin.PayMode;
            orderInfo.PayTime = new DateTime(1900, 1, 1);

            orderInfo.RegionId = fullShipAddressInfo.RegionId;
            orderInfo.Consignee = fullShipAddressInfo.Consignee;
            orderInfo.Mobile = fullShipAddressInfo.Mobile;
            orderInfo.Phone = fullShipAddressInfo.Phone;
            orderInfo.Email = fullShipAddressInfo.Email;
            orderInfo.ZipCode = fullShipAddressInfo.ZipCode;
            orderInfo.Address = fullShipAddressInfo.Address;
            orderInfo.BestTime = bestTime;

            orderInfo.BuyerRemark = buyerRemark;
            orderInfo.IP = ip;

            try
            {
                //添加订单
                int oid = _iorderstrategy.CreateOrder(orderInfo, Carts.IsPersistOrderProduct, orderProductList);
                if (oid > 0)
                {
                    orderInfo.Oid = oid;

                    //减少商品库存数量
                    Products.DecreaseProductStockNumber(orderProductList);
                    //更新限购库存
                    if (singlePromotionList.Count > 0)
                        Promotions.UpdateSinglePromotionStock(singlePromotionList);
                    //使用支付积分
                    Credits.PayOrder(ref partUserInfo, orderInfo, orderInfo.PayCreditCount, nowTime);
                    //使用优惠劵
                    foreach (CouponInfo couponInfo in couponList)
                    {
                        if (couponInfo.Uid > 0)
                            Coupons.UseCoupon(couponInfo.CouponId, oid, nowTime, ip);
                        else
                            Coupons.ActivateAndUseCoupon(couponInfo.CouponId, partUserInfo.Uid, oid, nowTime, ip);
                    }

                    return orderInfo;
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }

            return null;
        }

        /// <summary>
        /// 更新订单折扣
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="discount">折扣</param>
        /// <param name="orderAmount">订单合计</param>
        /// <param name="surplusMoney">剩余金额</param>
        public static void UpdateOrderDiscount(int oid, decimal discount, decimal orderAmount, decimal surplusMoney)
        {
            BrnShop.Data.Orders.UpdateOrderDiscount(oid, discount, orderAmount, surplusMoney);
        }

        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="paySN">支付单号</param>
        /// <param name="payTime">支付时间</param>
        public static void PayOrder(int oid, OrderState orderState, string paySN, DateTime payTime)
        {
            BrnShop.Data.Orders.PayOrder(oid, orderState, paySN, payTime);
        }

        /// <summary>
        /// 确认订单
        /// </summary>
        /// <param name="orderInfo">订单信息</param>
        public static void ConfirmOrder(OrderInfo orderInfo)
        {
            UpdateOrderState(orderInfo.Oid, OrderState.Confirmed);
        }

        /// <summary>
        /// 备货
        /// </summary>
        /// <param name="orderInfo">订单信息</param>
        public static void PreProduct(OrderInfo orderInfo)
        {
            UpdateOrderState(orderInfo.Oid, OrderState.PreProducting);
        }

        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="shipSN">配送单号</param>
        /// <param name="shipTime">配送时间</param>
        public static void SendOrder(int oid, OrderState orderState, string shipSN, DateTime shipTime)
        {
            BrnShop.Data.Orders.SendOrderProduct(oid, orderState, shipSN, shipTime);
        }

        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="partUserInfo">用户信息</param>
        /// <param name="orderInfo">订单信息</param>
        /// <param name="completeTime">完成时间</param>
        /// <param name="ip">ip</param>
        public static void CompleteOrder(ref PartUserInfo partUserInfo, OrderInfo orderInfo, DateTime completeTime, string ip)
        {
            UpdateOrderState(orderInfo.Oid, OrderState.Completed);//将订单状态设为完成状态

            //订单商品列表
            List<OrderProductInfo> orderProductList = GetOrderProductList(orderInfo.Oid);

            //发放完成订单积分
            Credits.SendCompleteOrderCredits(ref partUserInfo, orderInfo, orderProductList, completeTime);

            //发放单品促销活动支付积分和优惠劵
            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                if (orderProductInfo.Type == 0)
                {
                    if (orderProductInfo.PayCredits > 0)
                        Credits.SendSinglePromotionCredits(ref partUserInfo, orderInfo, orderProductInfo.PayCredits, completeTime);
                    if (orderProductInfo.CouponTypeId > 0)
                        Coupons.SendSinglePromotionCoupon(partUserInfo, orderProductInfo.CouponTypeId, orderInfo, ip);
                }
            }
        }

        /// <summary>
        /// 退货
        /// </summary>
        /// <param name="partUserInfo">用户信息</param>
        /// <param name="orderInfo">订单信息</param>
        /// <param name="operatorId">操作人id</param>
        /// <param name="returnTime">退货时间</param>
        public static void ReturnOrder(ref PartUserInfo partUserInfo, OrderInfo orderInfo, int operatorId, DateTime returnTime)
        {
            UpdateOrderState(orderInfo.Oid, OrderState.Returned);//将订单状态设为退货状态

            if (orderInfo.OrderState == (int)OrderState.Sended)//用户收货时退货
            {
                if (orderInfo.CouponMoney > 0)//退回用户使用的优惠劵
                    Coupons.ReturnUserOrderUseCoupons(orderInfo.Oid);

                if (orderInfo.PayCreditCount > 0)//退回用户使用的积分
                    Credits.ReturnUserOrderUseCredits(ref partUserInfo, orderInfo, operatorId, returnTime);

                if (orderInfo.PaySN.Length > 0)//退回用户支付的金钱(此操作只是将退款记录保存到表'orderrefunds'中，实际退款还需要再次操作)
                    OrderRefunds.ApplyRefund(new OrderRefundInfo
                    {
                        Oid = orderInfo.Oid,
                        OSN = orderInfo.OSN,
                        Uid = orderInfo.Uid,
                        State = 0,
                        ApplyTime = returnTime,
                        PayMoney = orderInfo.SurplusMoney,
                        RefundMoney = orderInfo.SurplusMoney,
                        PaySN = orderInfo.PaySN,
                        PaySystemName = orderInfo.PaySystemName,
                        PayFriendName = orderInfo.PayFriendName
                    });

            }
            else if (orderInfo.OrderState == (int)OrderState.Completed)//订单完成后退货
            {
                if (orderInfo.CouponMoney > 0)//退回用户使用的优惠劵
                    Coupons.ReturnUserOrderUseCoupons(orderInfo.Oid);

                if (orderInfo.PayCreditCount > 0)//退回用户使用的积分
                    Credits.ReturnUserOrderUseCredits(ref partUserInfo, orderInfo, operatorId, returnTime);

                //应退金钱
                decimal returnMoney = orderInfo.SurplusMoney;

                //订单发放的积分
                DataTable sendCredits = Credits.GetUserOrderSendCredits(orderInfo.Oid);
                int payCreditAmount = TypeHelper.ObjectToInt(sendCredits.Rows[0]["paycreditamount"]);
                int rankCreditAmount = TypeHelper.ObjectToInt(sendCredits.Rows[0]["rankcreditamount"]);
                //判断用户当前积分是否足够退回，如果不足够就将差额核算成金钱并在应退金钱中减去
                if (partUserInfo.PayCredits < payCreditAmount)
                {
                    returnMoney = returnMoney - Credits.PayCreditsToMoney(payCreditAmount - partUserInfo.PayCredits);
                    payCreditAmount = partUserInfo.PayCredits;
                }
                //收回订单发放的积分
                Credits.ReturnUserOrderSendCredits(ref partUserInfo, orderInfo, payCreditAmount, rankCreditAmount, operatorId, returnTime);

                StringBuilder couponIdList = new StringBuilder();
                //订单发放的优惠劵列表
                List<CouponInfo> couponList = Coupons.GetUserOrderSendCouponList(orderInfo.Oid);
                //判断优惠劵是否已经被使用，如果已经使用就在应退金钱中减去优惠劵金额
                foreach (CouponInfo couponInfo in couponList)
                {
                    if (couponInfo.Oid > 0)
                        returnMoney = returnMoney - couponInfo.Money;
                    else
                        couponIdList.AppendFormat("{0},", couponInfo.CouponId);
                }
                //收回订单发放的优惠劵
                if (couponIdList.Length > 0)
                {
                    Coupons.DeleteCouponById(couponIdList.Remove(couponIdList.Length - 1, 1).ToString());
                }

                if (returnMoney > 0)//退回用户支付的金钱(此操作只是将退款记录保存到表'orderrefunds'中，实际退款还需要再次操作)
                    OrderRefunds.ApplyRefund(new OrderRefundInfo
                    {
                        Oid = orderInfo.Oid,
                        OSN = orderInfo.OSN,
                        Uid = orderInfo.Uid,
                        State = 0,
                        ApplyTime = returnTime,
                        PayMoney = orderInfo.SurplusMoney,
                        RefundMoney = returnMoney,
                        PaySN = orderInfo.PaySN,
                        PaySystemName = orderInfo.PaySystemName,
                        PayFriendName = orderInfo.PayFriendName
                    });
            }

            Products.IncreaseProductStockNumber(GetOrderProductList(orderInfo.Oid));//增加商品库存数量
        }

        /// <summary>
        /// 锁定订单
        /// </summary>
        /// <param name="orderInfo">订单信息</param>
        public static void LockOrder(OrderInfo orderInfo)
        {
            UpdateOrderState(orderInfo.Oid, OrderState.Locked);
            Products.IncreaseProductStockNumber(GetOrderProductList(orderInfo.Oid));//增加商品库存数量
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="partUserInfo">用户信息</param>
        /// <param name="orderInfo">订单信息</param>
        /// <param name="operatorId">操作人id</param>
        /// <param name="cancelTime">取消时间</param>
        public static void CancelOrder(ref PartUserInfo partUserInfo, OrderInfo orderInfo, int operatorId, DateTime cancelTime)
        {
            UpdateOrderState(orderInfo.Oid, OrderState.Cancelled);//将订单状态设为取消状态

            if (orderInfo.CouponMoney > 0)//退回用户使用的优惠劵
                Coupons.ReturnUserOrderUseCoupons(orderInfo.Oid);

            if (orderInfo.PayCreditCount > 0)//退回用户使用的积分
                Credits.ReturnUserOrderUseCredits(ref partUserInfo, orderInfo, operatorId, cancelTime);

            if (orderInfo.PaySN.Length > 0)//退回用户支付的金钱(此操作只是将退款记录保存到表'orderrefunds'中，实际退款还需要再次操作)
                OrderRefunds.ApplyRefund(new OrderRefundInfo
                {
                    Oid = orderInfo.Oid,
                    OSN = orderInfo.OSN,
                    Uid = orderInfo.Uid,
                    State = 0,
                    ApplyTime = cancelTime,
                    PayMoney = orderInfo.SurplusMoney,
                    RefundMoney = orderInfo.SurplusMoney,
                    PaySN = orderInfo.PaySN,
                    PaySystemName = orderInfo.PaySystemName,
                    PayFriendName = orderInfo.PayFriendName
                });

            Products.IncreaseProductStockNumber(GetOrderProductList(orderInfo.Oid));//增加商品库存数量
        }

        #endregion

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="orderState">订单状态</param>
        public static void UpdateOrderState(int oid, OrderState orderState)
        {
            BrnShop.Data.Orders.UpdateOrderState(oid, orderState);
        }

        /// <summary>
        /// 更新订单的评价
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="isReview">是否评价</param>
        public static void UpdateOrderIsReview(int oid, int isReview)
        {
            BrnShop.Data.Orders.UpdateOrderIsReview(oid, isReview);
        }

        /// <summary>
        /// 清空过期的在线支付订单
        /// </summary>
        /// <param name="expireTime">过期时间</param>
        public static void ClearExpiredOnlinePayOrder(DateTime expireTime)
        {
            BrnShop.Data.Orders.ClearExpiredOnlinePayOrder(expireTime);
        }

        /// <summary>
        /// 清空过期的线下支付订单
        /// </summary>
        /// <param name="expireTime">过期时间</param>
        public static void ClearExpiredOfflinePayOrder(DateTime expireTime)
        {
            BrnShop.Data.Orders.ClearExpiredOfflinePayOrder(expireTime);
        }

        /// <summary>
        /// 获得用户订单列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="startAddTime">添加开始时间</param>
        /// <param name="endAddTime">添加结束时间</param>
        /// <param name="orderState">订单状态(0代表全部状态)</param>
        /// <returns></returns>
        public static DataTable GetUserOrderList(int uid, int pageSize, int pageNumber, string startAddTime, string endAddTime, int orderState)
        {
            return BrnShop.Data.Orders.GetUserOrderList(uid, pageSize, pageNumber, startAddTime, endAddTime, orderState);
        }

        /// <summary>
        /// 获得用户订单列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="startAddTime">添加开始时间</param>
        /// <param name="endAddTime">添加结束时间</param>
        /// <param name="orderState">订单状态(0代表全部状态)</param>
        /// <returns></returns>
        public static int GetUserOrderCount(int uid, string startAddTime, string endAddTime, int orderState)
        {
            return BrnShop.Data.Orders.GetUserOrderCount(uid, startAddTime, endAddTime, orderState);
        }

        /// <summary>
        /// 是否评价了所有订单商品
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        /// <returns></returns>
        public static bool IsReviewAllOrderProduct(List<OrderProductInfo> orderProductList)
        {
            foreach (OrderProductInfo orderProductInfo in orderProductList)
            {
                if (orderProductInfo.IsReview == 0)
                    return false;
            }
            return true;
        }
    }
}
