using System;
using System.Web.Mvc;
using System.Web.Routing;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;

namespace BrnShop.Web.Controllers
{
    /// <summary>
    /// 首页控制器类
    /// </summary>
    public partial class HomeController : BaseWebController
    {
        /// <summary>
        /// 首页
        /// </summary>
        public ActionResult Index()
        {
            //判断请求是否来自移动设备，如果是则重定向到移动主题
            if (WebHelper.IsMobile())
                return RedirectToAction("index", "home", new RouteValueDictionary { { "area", "mob" } });

            //首页的数据需要在其视图文件中直接调用，所以此处不再需要视图模型
            return View();
        }
    }
}
