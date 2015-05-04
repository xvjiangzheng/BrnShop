using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 订单处理操作管理类
    /// </summary>
    public partial class OrderActions
    {
        /// <summary>
        /// 创建订单处理
        /// </summary>
        /// <param name="orderActionInfo">订单处理信息</param>
        public static void CreateOrderAction(OrderActionInfo orderActionInfo)
        {
            BrnShop.Data.OrderActions.CreateOrderAction(orderActionInfo);
        }

        /// <summary>
        /// 获得订单处理列表
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static List<OrderActionInfo> GetOrderActionList(int oid)
        {
            return BrnShop.Data.OrderActions.GetOrderActionList(oid);
        }

        /// <summary>
        /// 获得订单id列表
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="orderActionType">订单操作类型</param>
        /// <returns></returns>
        public static DataTable GetOrderIdList(DateTime startTime, DateTime endTime, int orderActionType)
        {
            return BrnShop.Data.OrderActions.GetOrderIdList(startTime, endTime, orderActionType);
        }
    }
}
