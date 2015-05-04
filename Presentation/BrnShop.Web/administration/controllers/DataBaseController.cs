using System;
using System.Web.Mvc;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Admin.Models;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台数据库控制器类
    /// </summary>
    public partial class DataBaseController : BaseAdminController
    {
        /// <summary>
        /// 数据库管理
        /// </summary>
        public ActionResult Manage()
        {
            return View();
        }

        /// <summary>
        /// 运行SQL语句
        /// </summary>
        public ActionResult RunSql(string sql = "")
        {
            if (string.IsNullOrWhiteSpace(sql))
                return PromptView(Url.Action("manage"), "SQL语句不能为空");

            string message = DataBases.RunSql(sql);
            if (string.IsNullOrWhiteSpace(message))
            {
                AddAdminOperateLog("运行SQL语句", "运行SQL语句,SQL语句为:" + sql);
                return PromptView(Url.Action("manage"), "SQL语句运行成功");
            }
            else
            {
                return PromptView(Url.Action("manage"), "SQL语句运行失败错误信息为：" + message, false);
            }
        }
    }
}
