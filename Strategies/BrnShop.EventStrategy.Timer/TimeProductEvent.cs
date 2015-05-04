using System;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;

namespace BrnShop.EventStrategy.Timer
{
    /// <summary>
    /// 定时商品事件
    /// </summary>
    public class TimeProductEvent : IEvent
    {
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="eventInfo">事件信息</param>
        public void Execute(object eventInfo)
        {
            EventInfo e = (EventInfo)eventInfo;
            List<TimeProductInfo> timeProductList = null;

            timeProductList = AdminProducts.GetTimeProductList(0);
            foreach (TimeProductInfo timeProductInfo in timeProductList)
            {
                AdminProducts.UpdateProductState(new int[1] { timeProductInfo.Pid }, ProductState.OnSale);
                timeProductInfo.OnSaleState = 2;
                AdminProducts.UpdateTimeProduct(timeProductInfo);
            }

            timeProductList = AdminProducts.GetTimeProductList(1);
            foreach (TimeProductInfo timeProductInfo in timeProductList)
            {
                AdminProducts.UpdateProductState(new int[1] { timeProductInfo.Pid }, ProductState.OutSale);
                timeProductInfo.OutSaleState = 2;
                AdminProducts.UpdateTimeProduct(timeProductInfo);
            }

            EventLogs.CreateEventLog(e.Key, e.Title, Environment.MachineName, DateTime.Now);
        }
    }
}
