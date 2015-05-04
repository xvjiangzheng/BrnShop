using System;
using System.Data;

using BrnShop.Core;
using BrnShop.Services;

namespace BrnShop.EventStrategy.Timer
{
    /// <summary>
    /// 商品评价数量事件
    /// </summary>
    public class ProductReviewCountEvent : IEvent
    {
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="eventInfo">事件信息</param>
        public void Execute(object eventInfo)
        {
            EventInfo e = (EventInfo)eventInfo;

            //同步商品评价数量
            DateTime lastExecuteTime = EventLogs.GetEventLastExecuteTimeByKey(e.Key);
            if (lastExecuteTime.Date < DateTime.Now.Date)
            {
                DateTime startTime = lastExecuteTime.Date;
                DateTime endTime = DateTime.Now.Date;
                DataTable dt = ProductReviews.GetProductReviewList(startTime, endTime);
                foreach (DataRow row in dt.Rows)
                {
                    int pid = TypeHelper.ObjectToInt(row["pid"]);
                    int starType = TypeHelper.ObjectToInt(row["star"]);
                    Products.AddProductShadowReviewCount(pid, starType);
                }
            }

            EventLogs.CreateEventLog(e.Key, e.Title, Environment.MachineName, DateTime.Now);
        }
    }
}
