using System;
using System.Web.Mvc;

namespace BrnShop.Web.Framework
{
    /// <summary>
    /// 主题视图引擎
    /// </summary>
    public class ThemeRazorViewEngine : ThemeBuildManagerViewEngine
    {
        /// <summary>
        /// 创建Razor视图
        /// </summary>
        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return new RazorView(controllerContext, viewPath, masterPath, true, FileExtensions);
        }

        /// <summary>
        /// 创建Razor分部视图
        /// </summary>
        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return new RazorView(controllerContext, partialPath, null, false, FileExtensions);
        }
    }
}
