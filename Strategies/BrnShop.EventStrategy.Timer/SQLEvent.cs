using System;
using System.Text;

using BrnShop.Core;
using BrnShop.Services;

namespace BrnShop.EventStrategy.Timer
{
    /// <summary>
    /// SQL事件
    /// </summary>
    public class SQLEvent : IEvent
    {
        /// <summary>
        /// 事件执行方法
        /// </summary>
        /// <param name="eventInfo">事件信息</param>
        public void Execute(object eventInfo)
        {
            //执行sql语句
            EventInfo e = (EventInfo)eventInfo;

            string sql = e.Code;
            if (!string.IsNullOrWhiteSpace(sql))
            {
                StringBuilder sb = new StringBuilder(sql);
                sb.Replace("rdbstablepre", BSPConfig.RDBSConfig.RDBSTablePre);
                sb.Replace("nowdate", CommonHelper.GetDate());
                sb.Replace("nowtime", CommonHelper.GetDateTime());
                DataBases.RunSql(sb.ToString());
            }

            EventLogs.CreateEventLog(e.Key, e.Title, Environment.MachineName, DateTime.Now);
        }
    }
}
