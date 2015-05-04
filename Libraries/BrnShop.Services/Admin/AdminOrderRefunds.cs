using System;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 后台订单退款操作管理类
    /// </summary>
    public partial class AdminOrderRefunds : OrderRefunds
    {
        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="refundId">退款id</param>
        /// <param name="refundSN">退款单号</param>
        /// <param name="refundSystemName">退款方式系统名</param>
        /// <param name="refundFriendName">退款方式昵称</param>
        /// <param name="refundTime">退款时间</param>
        public static void RefundOrder(int refundId, string refundSN, string refundSystemName, string refundFriendName, DateTime refundTime)
        {
            BrnShop.Data.OrderRefunds.RefundOrder(refundId, refundSN, refundSystemName, refundFriendName, refundTime);
        }

        /// <summary>
        /// 获得订单退款列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static List<OrderRefundInfo> GetOrderRefundList(int pageSize, int pageNumber, string condition)
        {
            return BrnShop.Data.OrderRefunds.GetOrderRefundList(pageSize, pageNumber, condition);
        }

        /// <summary>
        /// 获得列表搜索条件
        /// </summary>
        /// <param name="osn">订单编号</param>
        /// <returns></returns>
        public static string GetOrderRefundListCondition(string osn)
        {
            return BrnShop.Data.OrderRefunds.GetOrderRefundListCondition(osn);
        }

        /// <summary>
        /// 获得订单退款数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int GetOrderRefundCount(string condition)
        {
            return BrnShop.Data.OrderRefunds.GetOrderRefundCount(condition);
        }
    }
}
