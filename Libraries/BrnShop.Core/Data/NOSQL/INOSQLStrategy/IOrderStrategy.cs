using System;
using System.Collections.Generic;

namespace BrnShop.Core
{
    /// <summary>
    /// BrnShop非关系型数据库策略之订单接口
    /// </summary>
    public partial interface IOrderNOSQLStrategy
    {
        #region 订单

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="orderInfo">订单信息</param>
        void CreateOrder(OrderInfo orderInfo);

        /// <summary>
        /// 获得订单信息
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns>订单信息</returns>
        OrderInfo GetOrderByOid(int oid);

        /// <summary>
        /// 获得订单商品列表
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        List<OrderProductInfo> GetOrderProductList(int oid);

        /// <summary>
        /// 创建订单商品列表
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="orderProductList">订单商品列表</param>
        void CreateOrderProductList(int oid, List<OrderProductInfo> orderProductList);

        /// <summary>
        /// 更新订单折扣
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="discount">折扣</param>
        /// <param name="orderAmount">订单合计</param>
        /// <param name="surplusMoney">剩余金额</param>
        void UpdateOrderDiscount(int oid, decimal discount, decimal orderAmount, decimal surplusMoney);

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="orderState">订单状态</param>
        void UpdateOrderState(int oid, OrderState orderState);

        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="shipSN">配送单号</param>
        /// <param name="shipTime">配送时间</param>
        void SendOrderProduct(int oid, OrderState orderState, string shipSN, DateTime shipTime);

        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="orderState">订单状态</param>
        /// <param name="paySN">支付单号</param>
        /// <param name="payTime">支付时间</param>
        void PayOrder(int oid, OrderState orderState, string paySN, DateTime payTime);

        /// <summary>
        /// 更新订单的评价
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="isReview">是否评价</param>
        void UpdateOrderIsReview(int oid, int isReview);

        /// <summary>
        /// 评价商品
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="recordId">记录id</param>
        void ReviewProduct(int oid, int recordId);

        #endregion

        #region 订单处理

        /// <summary>
        /// 创建订单处理
        /// </summary>
        /// <param name="orderActionInfo">订单处理信息</param>
        void CreateOrderAction(OrderActionInfo orderActionInfo);

        /// <summary>
        /// 获得订单处理列表
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        List<OrderActionInfo> GetOrderActionList(int oid);

        /// <summary>
        /// 创建订单处理列表
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <param name="orderActionList">订单处理列表</param>
        void CreateOrderActionList(int oid, List<OrderActionInfo> orderActionList);

        #endregion
    }
}
