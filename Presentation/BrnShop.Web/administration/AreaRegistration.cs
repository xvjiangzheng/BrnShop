using System;

namespace BrnShop.Admin
{
    public class AreaRegistration : System.Web.Mvc.AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "admin";
            }
        }

        public override void RegisterArea(System.Web.Mvc.AreaRegistrationContext context)
        {
            //此路由不能删除
            context.MapRoute("admin_default",
                              "admin/{controller}/{action}",
                              new { controller = "home", action = "index", area = "admin" },
                              new[] { "BrnShop.Web.Admin.Controllers" });

        }
    }
}
