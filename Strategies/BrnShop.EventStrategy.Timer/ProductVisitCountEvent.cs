using System;
using System.Data;

using BrnShop.Core;
using BrnShop.Services;

namespace BrnShop.EventStrategy.Timer
{
    /// <summary>
    /// 商品访问量事件
    /// </summary>
    public class ProductVisitCountEvent : IEvent
    {
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="eventInfo">事件信息</param>
        public void Execute(object eventInfo)
        {
            EventInfo e = (EventInfo)eventInfo;

            //同步商品的访问量
            DataTable dt = ProductStats.GetProductTotalVisitCountList();
            foreach (DataRow row in dt.Rows)
            {
                int pid = TypeHelper.ObjectToInt(row["pid"]);
                int visitCount = TypeHelper.ObjectToInt(row["count"]);
                if (visitCount != Products.GetProductShadowVisitCountById(pid))
                {
                    Products.UpdateProductShadowVisitCount(pid, visitCount);
                }
            }

            EventLogs.CreateEventLog(e.Key, e.Title, Environment.MachineName, DateTime.Now);
        }
    }
}
