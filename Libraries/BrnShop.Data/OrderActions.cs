using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 订单处理数据访问类
    /// </summary>
    public partial class OrderActions
    {
        private static IOrderNOSQLStrategy _ordernosql = BSPData.OrderNOSQL;//订单非关系型数据库

        #region 辅助方法

        /// <summary>
        /// 从IDataReader创建OrderActionInfo
        /// </summary>
        public static OrderActionInfo BuildOrderActionFromReader(IDataReader reader)
        {
            OrderActionInfo orderActionInfo = new OrderActionInfo();

            orderActionInfo.Oid = TypeHelper.ObjectToInt(reader["oid"]);
            orderActionInfo.Uid = TypeHelper.ObjectToInt(reader["uid"]);
            orderActionInfo.RealName = reader["realname"].ToString();
            orderActionInfo.AdminGid = TypeHelper.ObjectToInt(reader["admingid"]);
            orderActionInfo.AdminGTitle = reader["admingtitle"].ToString();
            orderActionInfo.ActionType = TypeHelper.ObjectToInt(reader["actiontype"]);
            orderActionInfo.ActionTime = TypeHelper.ObjectToDateTime(reader["actiontime"]);
            orderActionInfo.ActionDes = reader["actiondes"].ToString();

            return orderActionInfo;
        }

        #endregion

        /// <summary>
        /// 创建订单处理
        /// </summary>
        /// <param name="orderActionInfo">订单处理信息</param>
        public static void CreateOrderAction(OrderActionInfo orderActionInfo)
        {
            BrnShop.Core.BSPData.RDBS.CreateOrderAction(orderActionInfo);
            if (_ordernosql != null)
                _ordernosql.CreateOrderAction(orderActionInfo);
        }

        /// <summary>
        /// 获得订单处理列表
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static List<OrderActionInfo> GetOrderActionList(int oid)
        {
            List<OrderActionInfo> orderActionList = null;

            if (_ordernosql != null)
            {
                orderActionList = _ordernosql.GetOrderActionList(oid);
                if (orderActionList == null)
                {
                    orderActionList = new List<OrderActionInfo>();
                    IDataReader reader = BrnShop.Core.BSPData.RDBS.GetOrderActionList(oid);
                    while (reader.Read())
                    {
                        OrderActionInfo orderActionInfo = BuildOrderActionFromReader(reader);
                        orderActionList.Add(orderActionInfo);
                    }
                    reader.Close();
                    _ordernosql.CreateOrderActionList(oid, orderActionList);
                }
            }
            else
            {
                orderActionList = new List<OrderActionInfo>();
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetOrderActionList(oid);
                while (reader.Read())
                {
                    OrderActionInfo orderActionInfo = BuildOrderActionFromReader(reader);
                    orderActionList.Add(orderActionInfo);
                }
                reader.Close();
            }

            return orderActionList;
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
            return BrnShop.Core.BSPData.RDBS.GetOrderIdList(startTime, endTime, orderActionType);
        }
    }
}
