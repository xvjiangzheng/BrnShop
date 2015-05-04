using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 订单数据访问类
    /// </summary>
    public partial class Orders
    {
        private static IOrderNOSQLStrategy _ordernosql = BSPData.OrderNOSQL;//订单非关系型数据库

        #region 辅助方法

        /// <summary>
        /// 从IDataReader创建OrderProductInfo
        /// </summary>
        public static OrderProductInfo BuildOrderProductFromReader(IDataReader reader)
        {
            OrderProductInfo orderProductInfo = new OrderProductInfo();
            orderProductInfo.RecordId = TypeHelper.ObjectToInt(reader["recordid"]);
            orderProductInfo.Oid = TypeHelper.ObjectToInt(reader["oid"]);
            orderProductInfo.Uid = TypeHelper.ObjectToInt(reader["uid"]);
            orderProductInfo.Sid = reader["sid"].ToString();
            orderProductInfo.Pid = TypeHelper.ObjectToInt(reader["pid"]);
            orderProductInfo.PSN = reader["psn"].ToString();
            orderProductInfo.CateId = TypeHelper.ObjectToInt(reader["cateid"]);
            orderProductInfo.BrandId = TypeHelper.ObjectToInt(reader["brandid"]);
            orderProductInfo.Name = reader["name"].ToString();
            orderProductInfo.ShowImg = reader["showimg"].ToString();
            orderProductInfo.DiscountPrice = TypeHelper.ObjectToDecimal(reader["discountprice"]);
            orderProductInfo.ShopPrice = TypeHelper.ObjectToDecimal(reader["shopprice"]);
            orderProductInfo.CostPrice = TypeHelper.ObjectToDecimal(reader["costprice"]);
            orderProductInfo.MarketPrice = TypeHelper.ObjectToDecimal(reader["marketprice"]);
            orderProductInfo.Weight = TypeHelper.ObjectToInt(reader["weight"]);
            orderProductInfo.IsReview = TypeHelper.ObjectToInt(reader["isreview"]);
            orderProductInfo.RealCount = TypeHelper.ObjectToInt(reader["realcount"]);
            orderProductInfo.BuyCount = TypeHelper.ObjectToInt(reader["buycount"]);
            orderProductInfo.SendCount = TypeHelper.ObjectToInt(reader["sendcount"]);
            orderProductInfo.Type = TypeHelper.ObjectToInt(reader["type"]);
            orderProductInfo.PayCredits = TypeHelper.ObjectToInt(reader["paycredits"]);
            orderProductInfo.CouponTypeId = TypeHelper.ObjectToInt(reader["coupontypeid"]);
            orderProductInfo.ExtCode1 = TypeHelper.ObjectToInt(reader["extcode1"]);
            orderProductInfo.ExtCode2 = TypeHelper.ObjectToInt(reader["extcode2"]);
            orderProductInfo.ExtCode3 = TypeHelper.ObjectToInt(reader["extcode3"]);
            orderProductInfo.ExtCode4 = TypeHelper.ObjectToInt(reader["extcode4"]);
            orderProductInfo.ExtCode5 = TypeHelper.ObjectToInt(reader["extcode5"]);
            orderProductInfo.AddTime = TypeHelper.ObjectToDateTime(reader["addtime"]);
            return orderProductInfo;
        }

        /// <summary>
        /// 从IDataReader创建OrderInfo
        /// </summary>
        public static OrderInfo BuildOrderFromReader(IDataReader reader)
        {
            OrderInfo orderInfo = new OrderInfo();

            orderInfo.Oid = TypeHelper.ObjectToInt(reader["oid"]);
            orderInfo.OSN = reader["osn"].ToString();
            orderInfo.Uid = TypeHelper.ObjectToInt(reader["uid"]);

            orderInfo.OrderState = TypeHelper.ObjectToInt(reader["orderstate"]);

            orderInfo.ProductAmount = TypeHelper.ObjectToDecimal(reader["productamount"]);
            orderInfo.OrderAmount = TypeHelper.ObjectToDecimal(reader["orderamount"]);
            orderInfo.SurplusMoney = TypeHelper.ObjectToDecimal(reader["surplusmoney"]);

            orderInfo.ParentId = TypeHelper.ObjectToInt(reader["parentid"]);
            orderInfo.IsReview = TypeHelper.ObjectToInt(reader["isreview"]);
            orderInfo.AddTime = TypeHelper.ObjectToDateTime(reader["addtime"]);
            orderInfo.ShipSN = reader["shipsn"].ToString();
            orderInfo.ShipFriendName = reader["shipfriendname"].ToString();
            orderInfo.ShipSystemName = reader["shipsystemname"].ToString();
            orderInfo.ShipTime = TypeHelper.ObjectToDateTime(reader["shiptime"]);
            orderInfo.PaySN = reader["paysn"].ToString();
            orderInfo.PayFriendName = reader["payfriendname"].ToString();
            orderInfo.PaySystemName = reader["paysystemname"].ToString();
            orderInfo.PayMode = TypeHelper.ObjectToInt(reader["paymode"]);
            orderInfo.PayTime = TypeHelper.ObjectToDateTime(reader["paytime"]);

            orderInfo.RegionId = TypeHelper.ObjectToInt(reader["regionid"]);
            orderInfo.Consignee = reader["consignee"].ToString();
            orderInfo.Mobile = reader["mobile"].ToString();
            orderInfo.Phone = reader["phone"].ToString();
            orderInfo.Email = reader["email"].ToString();
            orderInfo.ZipCode = reader["zipcode"].ToString();
            orderInfo.Address = reader["address"].ToString();
            orderInfo.BestTime = TypeHelper.ObjectToDateTime(reader["besttime"]);

            orderInfo.ShipFee = TypeHelper.ObjectToDecimal(reader["shipfee"]);
            orderInfo.PayFee = TypeHelper.ObjectToDecimal(reader["payfee"]);
            orderInfo.FullCut = TypeHelper.ObjectToInt(reader["fullcut"]);
            orderInfo.Discount = TypeHelper.ObjectToDecimal(reader["discount"]);
            orderInfo.PayCreditCount = TypeHelper.ObjectToInt(reader["paycreditcount"]);
            orderInfo.PayCreditMoney = TypeHelper.ObjectToDecimal(reader["paycreditmoney"]);
            orderInfo.CouponMoney = TypeHelper.ObjectToInt(reader["couponmoney"]);
            orderInfo.Weight = TypeHelper.ObjectToInt(reader["weight"]);

            orderInfo.BuyerRemark = reader["buyerremark"].ToString();
            orderInfo.IP = reader["ip"].ToString();

            return orderInfo;
        }

        #endregion

        /// <summary>
        /// 获得订单信息
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns>订单信息</returns>
        public static OrderInfo GetOrderByOid(int oid)
        {
            OrderInfo orderInfo = null;

            if (_ordernosql != null)
            {
                orderInfo = _ordernosql.GetOrderByOid(oid);
                if (orderInfo == null)
                {
                    IDataReader reader = BrnShop.Core.BSPData.RDBS.GetOrderByOid(oid);
                    if (reader.Read())
                    {
                        orderInfo = BuildOrderFromReader(reader);
                    }
                    reader.Close();
                    if (orderInfo != null)
                        _ordernosql.CreateOrder(orderInfo);
                }
            }
            else
            {
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetOrderByOid(oid);
                if (reader.Read())
                {
                    orderInfo = BuildOrderFromReader(reader);
                }
                reader.Close();
            }

            return orderInfo;
        }

        /// <summary>
        /// 获得订单信息
        /// </summary>
        /// <param name="osn">订单编号</param>
        /// <returns>订单信息</returns>
        public static OrderInfo GetOrderByOSN(string osn)
        {
            OrderInfo orderInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetOrderByOSN(osn);
            if (reader.Read())
            {
                orderInfo = BuildOrderFromReader(reader);
            }
            reader.Close();
            return orderInfo;
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
            return BrnShop.Core.BSPData.RDBS.GetOrderCountByOrderStateAndAddTime(orderState, startTime, endTime);
        }

        /// <summary>
        /// 获得订单列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable GetOrderList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BrnShop.Core.BSPData.RDBS.GetOrderList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 获得列表搜索条件
        /// </summary>
        /// <param name="osn">订单编号</param>
        /// <param name="uid">用户id</param>
        /// <param name="consignee">收货人</param>
        /// <param name="orderState">订单状态</param>
        /// <returns></returns>
        public static string GetOrderListCondition(string osn, int uid, string consignee, int orderState)
        {
            return BrnShop.Core.BSPData.RDBS.GetOrderListCondition(osn, uid, consignee, orderState);
        }

        /// <summary>
        /// 获得列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string GetOrderListSort(string sortColumn, string sortDirection)
        {
            return BrnShop.Core.BSPData.RDBS.GetOrderListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 获得订单数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int GetOrderCount(string condition)
        {
            return BrnShop.Core.BSPData.RDBS.GetOrderCount(condition);
        }

        /// <summary>
        /// 获得订单商品列表
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static List<OrderProductInfo> GetOrderProductList(int oid)
        {
            List<OrderProductInfo> orderProductList = null;

            if (_ordernosql != null)
            {
                orderProductList = _ordernosql.GetOrderProductList(oid);
                if (orderProductList == null)
                {
                    orderProductList = new List<OrderProductInfo>();
                    IDataReader reader = BrnShop.Core.BSPData.RDBS.GetOrderProductList(oid);
                    while (reader.Read())
                    {
                        OrderProductInfo orderProductInfo = BuildOrderProductFromReader(reader);
                        orderProductList.Add(orderProductInfo);
                    }
                    reader.Close();
                    _ordernosql.CreateOrderProductList(oid, orderProductList);
                }
            }
            else
            {
                orderProductList = new List<OrderProductInfo>();
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetOrderProductList(oid);
                while (reader.Read())
                {
                    OrderProductInfo orderProductInfo = BuildOrderProductFromReader(reader);
                    orderProductList.Add(orderProductInfo);
                }
                reader.Close();
            }

            return orderProductList;
        }

        /// <summary>
        /// 获得订单商品列表
        /// </summary>
        /// <param name="oidList">订单id列表</param>
        /// <returns></returns>
        public static List<OrderProductInfo> GetOrderProductList(string oidList)
        {
            List<OrderProductInfo> orderProductList = new List<OrderProductInfo>();

            if (_ordernosql != null)
            {
                foreach (string oid in StringHelper.SplitString(oidList))
                    orderProductList.AddRange(GetOrderProductList(TypeHelper.StringToInt(oid)));
            }
            else
            {
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetOrderProductList(oidList);
                while (reader.Read())
                {
                    OrderProductInfo orderProductInfo = BuildOrderProductFromReader(reader);
                    orderProductList.Add(orderProductInfo);
                }
                reader.Close();
            }

            return orderProductList;
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
            BrnShop.Core.BSPData.RDBS.UpdateOrderDiscount(oid, discount, orderAmount, surplusMoney);
            if (_ordernosql != null)
                _ordernosql.UpdateOrderDiscount(oid, discount, orderAmount, surplusMoney);
        }

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="orderState">订单状态</param>
        public static void UpdateOrderState(int oid, OrderState orderState)
        {
            BrnShop.Core.BSPData.RDBS.UpdateOrderState(oid, orderState);
            if (_ordernosql != null)
                _ordernosql.UpdateOrderState(oid, orderState);
        }

        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="shipSN">配送单号</param>
        /// <param name="shipTime">配送时间</param>
        public static void SendOrderProduct(int oid, OrderState orderState, string shipSN, DateTime shipTime)
        {
            BrnShop.Core.BSPData.RDBS.SendOrderProduct(oid, orderState, shipSN, shipTime);
            if (_ordernosql != null)
                _ordernosql.SendOrderProduct(oid, orderState, shipSN, shipTime);
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
            BrnShop.Core.BSPData.RDBS.PayOrder(oid, orderState, paySN, payTime);
            if (_ordernosql != null)
                _ordernosql.PayOrder(oid, orderState, paySN, payTime);
        }

        /// <summary>
        /// 更新订单的评价
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="isReview">是否评价</param>
        public static void UpdateOrderIsReview(int oid, int isReview)
        {
            BrnShop.Core.BSPData.RDBS.UpdateOrderIsReview(oid, isReview);
            if (_ordernosql != null)
                _ordernosql.UpdateOrderIsReview(oid, isReview);
        }

        /// <summary>
        /// 清空过期的在线支付订单
        /// </summary>
        /// <param name="expireTime">过期时间</param>
        public static void ClearExpiredOnlinePayOrder(DateTime expireTime)
        {
            BrnShop.Core.BSPData.RDBS.ClearExpiredOnlinePayOrder(expireTime);
        }

        /// <summary>
        /// 清空过期的线下支付订单
        /// </summary>
        /// <param name="expireTime">过期时间</param>
        public static void ClearExpiredOfflinePayOrder(DateTime expireTime)
        {
            BrnShop.Core.BSPData.RDBS.ClearExpiredOfflinePayOrder(expireTime);
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
            return BrnShop.Core.BSPData.RDBS.GetUserOrderList(uid, pageSize, pageNumber, startAddTime, endAddTime, orderState);
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
            return BrnShop.Core.BSPData.RDBS.GetUserOrderCount(uid, startAddTime, endAddTime, orderState);
        }

        /// <summary>
        /// 获得销售商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static DataTable GetSaleProductList(int pageSize, int pageNumber, string startTime, string endTime)
        {
            return BrnShop.Core.BSPData.RDBS.GetSaleProductList(pageSize, pageNumber, startTime, endTime);
        }

        /// <summary>
        /// 获得销售商品数量
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static int GetSaleProductCount(string startTime, string endTime)
        {
            return BrnShop.Core.BSPData.RDBS.GetSaleProductCount(startTime, endTime);
        }

        /// <summary>
        /// 获得销售趋势
        /// </summary>
        /// <param name="trendType">趋势类型(0代表订单数，1代表订单合计)</param>
        /// <param name="timeType">时间类型(0代表小时，1代表天，2代表月，3代表年)</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static DataTable GetSaleTrend(int trendType, int timeType, string startTime, string endTime)
        {
            return BrnShop.Core.BSPData.RDBS.GetSaleTrend(trendType, timeType, startTime, endTime);
        }
    }
}
