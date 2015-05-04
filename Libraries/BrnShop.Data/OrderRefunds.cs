using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 订单退款数据访问类
    /// </summary>
    public partial class OrderRefunds
    {
        #region 辅助方法

        /// <summary>
        /// 从IDataReader创建OrderRefundInfo
        /// </summary>
        public static OrderRefundInfo BuildOrderRefundFromReader(IDataReader reader)
        {
            OrderRefundInfo orderRefundInfo = new OrderRefundInfo();

            orderRefundInfo.Oid = TypeHelper.ObjectToInt(reader["oid"]);
            orderRefundInfo.OSN = reader["osn"].ToString();
            orderRefundInfo.Uid = TypeHelper.ObjectToInt(reader["uid"]);
            orderRefundInfo.State = TypeHelper.ObjectToInt(reader["state"]);
            orderRefundInfo.ApplyTime = TypeHelper.ObjectToDateTime(reader["applytime"]);
            orderRefundInfo.PayMoney = TypeHelper.ObjectToDecimal(reader["paymoney"]);
            orderRefundInfo.RefundMoney = TypeHelper.ObjectToDecimal(reader["refundmoney"]);
            orderRefundInfo.RefundSN = reader["refundsn"].ToString();
            orderRefundInfo.RefundSystemName = reader["refundsystemname"].ToString();
            orderRefundInfo.RefundFriendName = reader["refundfriendname"].ToString();
            orderRefundInfo.RefundTime = TypeHelper.ObjectToDateTime(reader["refundtime"]);
            orderRefundInfo.PaySN = reader["paysn"].ToString();
            orderRefundInfo.PaySystemName = reader["paysystemname"].ToString();
            orderRefundInfo.PayFriendName = reader["payfriendname"].ToString();

            return orderRefundInfo;
        }

        #endregion

        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="orderRefundInfo">订单退款信息</param>
        public static void ApplyRefund(OrderRefundInfo orderRefundInfo)
        {
            BrnShop.Core.BSPData.RDBS.ApplyRefund(orderRefundInfo);
        }

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
            BrnShop.Core.BSPData.RDBS.RefundOrder(refundId, refundSN, refundSystemName, refundFriendName, refundTime);
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
            List<OrderRefundInfo> orderRefundList = new List<OrderRefundInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetOrderRefundList(pageSize, pageNumber, condition);
            while (reader.Read())
            {
                OrderRefundInfo orderRefundInfo = BuildOrderRefundFromReader(reader);
                orderRefundList.Add(orderRefundInfo);
            }

            reader.Close();
            return orderRefundList;
        }

        /// <summary>
        /// 获得列表搜索条件
        /// </summary>
        /// <param name="osn">订单编号</param>
        /// <returns></returns>
        public static string GetOrderRefundListCondition(string osn)
        {
            return BrnShop.Core.BSPData.RDBS.GetOrderRefundListCondition(osn);
        }

        /// <summary>
        /// 获得订单退款数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int GetOrderRefundCount(string condition)
        {
            return BrnShop.Core.BSPData.RDBS.GetOrderRefundCount(condition);
        }
    }
}
