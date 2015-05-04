using System;
using System.Web;
using System.Web.Mvc;

using BrnShop.Core;
using BrnShop.Web.Framework;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台货到付款控制器类
    /// </summary>
    public class AdminCODController : BaseAdminController
    {
        /// <summary>
        /// 配置
        /// </summary>
        [HttpGet]
        [ChildActionOnly]
        public ActionResult Config()
        {
            return View("~/plugins/BrnShop.PayPlugin.COD/views/admincod/config.cshtml");
        }
    }
}
