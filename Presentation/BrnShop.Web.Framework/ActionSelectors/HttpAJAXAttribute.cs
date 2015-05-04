using System;
using System.Web.Mvc;
using System.Reflection;

namespace BrnShop.Web.Framework
{
    /// <summary>
    /// ajax动作方法筛选属性
    /// </summary>
    public class HttpAJAXAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}
