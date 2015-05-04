using System;

namespace BrnShop.Web.Mobile
{
    public class AreaRegistration : System.Web.Mvc.AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "mob";
            }
        }

        public override void RegisterArea(System.Web.Mvc.AreaRegistrationContext context)
        {
            //此路由不能删除
            context.MapRoute("mobile_default",
                             "mob/{controller}/{action}",
                              new { controller = "home", action = "index", area = "mob" },
                              new[] { "BrnShop.Web.Mobile.Controllers" });

        }
    }
}
