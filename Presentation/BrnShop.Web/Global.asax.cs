using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;

namespace BrnShop.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //商品路由
            routes.MapRoute("product",
                            "{pid}.html",
                            new { controller = "catalog", action = "product" },
                            new[] { "BrnShop.Web.Controllers" });
            //分类路由
            routes.MapRoute("category",
                            "list/{filterAttr}-{cateId}-{brandId}-{filterPrice}-{onlyStock}-{sortColumn}-{sortDirection}-{page}.html",
                            new { controller = "catalog", action = "category" },
                            new[] { "BrnShop.Web.Controllers" });
            //分类路由
            routes.MapRoute("shortcategory",
                            "list/{cateId}.html",
                            new { controller = "catalog", action = "category" },
                            new[] { "BrnShop.Web.Controllers" });
            //搜索路由
            routes.MapRoute("search",
                            "search",
                            new { controller = "catalog", action = "search" },
                            new[] { "BrnShop.Web.Controllers" });
            //默认路由(此路由不能删除)
            routes.MapRoute("default", 
                            "{controller}/{action}", 
                            new { controller = "home", action = "index" },
                            new[] { "BrnShop.Web.Controllers" });
        }
        
        protected void Application_Start()
        {
            //将默认视图引擎替换为ThemeRazorViewEngine引擎
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ThemeRazorViewEngine());

            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            //启动事件机制
            BSPEvent.Start();
            //服务器宕机启动后重置在线用户表
            if (Environment.TickCount > 0 && Environment.TickCount < 900000)
                OnlineUsers.ResetOnlineUserTable();
        }
    }
}