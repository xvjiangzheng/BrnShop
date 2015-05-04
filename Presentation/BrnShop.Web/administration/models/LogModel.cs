using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Web.Framework;

namespace BrnShop.Web.Admin.Models
{
    /// <summary>
    /// 管理员操作日志列表模型类
    /// </summary>
    public class AdminOperateLogListModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 日志列表
        /// </summary>
        public List<AdminOperateLogInfo> AdminOperateLogList { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 操作动作
        /// </summary>
        public string Operation { get; set; }
        /// <summary>
        /// 操作开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 操作结束时间
        /// </summary>
        public string EndTime { get; set; }
    }

    /// <summary>
    /// 积分日志列表模型类
    /// </summary>
    public class CreditLogListModel
    {
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 积分日志列表
        /// </summary>
        public DataTable CreditLogList { get; set; }
        /// <summary>
        /// 账户名
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
    }
}
