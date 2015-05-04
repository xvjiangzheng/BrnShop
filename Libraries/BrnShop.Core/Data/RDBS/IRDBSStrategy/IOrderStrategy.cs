using System;
using System.Data;

namespace BrnShop.Core
{
    /// <summary>
    /// BrnShop关系数据库策略之订单分部接口
    /// </summary>
    public partial interface IRDBSStrategy
    {
        #region 订单

        /// <summary>
        /// 获得订单信息
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns>订单信息</returns>
        IDataReader GetOrderByOid(int oid);

        /// <summary>
        /// 获得订单信息
        /// </summary>
        /// <param name="osn">订单编号</param>
        /// <returns>订单信息</returns>
        IDataReader GetOrderByOSN(string osn);

        /// <summary>
        /// 根据下单时间获得订单数量
        /// </summary>
        /// <param name="orderState">订单状态</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        int GetOrderCountByOrderStateAndAddTime(int orderState, string startTime, string endTime);

        /// <summary>
        /// 获得订单列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        DataTable GetOrderList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 获得列表搜索条件
        /// </summary>
        /// <param name="osn">订单编号</param>
        /// <param name="uid">用户id</param>
        /// <param name="consignee">收货人</param>
        /// <param name="orderState">订单状态</param>
        /// <returns></returns>
        string GetOrderListCondition(string osn, int uid, string consignee, int orderState);

        /// <summary>
        /// 获得列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        string GetOrderListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 获得订单数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int GetOrderCount(string condition);

        /// <summary>
        /// 获得订单商品列表
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        IDataReader GetOrderProductList(int oid);

        /// <summary>
        /// 获得订单商品列表
        /// </summary>
        /// <param name="oidList">订单id列表</param>
        /// <returns></returns>
        IDataReader GetOrderProductList(string oidList);

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
        /// 清空过期的在线支付订单
        /// </summary>
        /// <param name="expireTime">过期时间</param>
        void ClearExpiredOnlinePayOrder(DateTime expireTime);

        /// <summary>
        /// 清空过期的线下支付订单
        /// </summary>
        /// <param name="expireTime">过期时间</param>
        void ClearExpiredOfflinePayOrder(DateTime expireTime);

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
        DataTable GetUserOrderList(int uid, int pageSize, int pageNumber, string startAddTime, string endAddTime, int orderState);

        /// <summary>
        /// 获得用户订单列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="startAddTime">添加开始时间</param>
        /// <param name="endAddTime">添加结束时间</param>
        /// <param name="orderState">订单状态(0代表全部状态)</param>
        /// <returns></returns>
        int GetUserOrderCount(int uid, string startAddTime, string endAddTime, int orderState);

        /// <summary>
        /// 获得销售商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        DataTable GetSaleProductList(int pageSize, int pageNumber, string startTime, string endTime);

        /// <summary>
        /// 获得销售商品数量
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        int GetSaleProductCount(string startTime, string endTime);

        /// <summary>
        /// 获得销售趋势
        /// </summary>
        /// <param name="trendType">趋势类型(0代表订单数，1代表订单合计)</param>
        /// <param name="timeType">时间类型(0代表小时，1代表天，2代表月，3代表年)</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        DataTable GetSaleTrend(int trendType, int timeType, string startTime, string endTime);

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
        IDataReader GetOrderActionList(int oid);

        /// <summary>
        /// 获得订单id列表
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="orderActionType">订单操作类型</param>
        /// <returns></returns>
        DataTable GetOrderIdList(DateTime startTime, DateTime endTime, int orderActionType);

        #endregion

        #region 订单退款

        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="orderRefundInfo">订单退款信息</param>
        void ApplyRefund(OrderRefundInfo orderRefundInfo);

        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="refundId">退款id</param>
        /// <param name="refundSN">退款单号</param>
        /// <param name="refundSystemName">退款方式系统名</param>
        /// <param name="refundFriendName">退款方式昵称</param>
        /// <param name="refundTime">退款时间</param>
        void RefundOrder(int refundId, string refundSN, string refundSystemName, string refundFriendName, DateTime refundTime);

        /// <summary>
        /// 获得订单退款列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        IDataReader GetOrderRefundList(int pageSize, int pageNumber, string condition);

        /// <summary>
        /// 获得列表搜索条件
        /// </summary>
        /// <param name="osn">订单编号</param>
        /// <returns></returns>
        string GetOrderRefundListCondition(string osn);

        /// <summary>
        /// 获得订单退款数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int GetOrderRefundCount(string condition);

        #endregion
    }
}
