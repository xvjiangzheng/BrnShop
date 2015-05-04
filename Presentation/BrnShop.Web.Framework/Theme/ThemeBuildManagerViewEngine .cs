using System;
using System.Web.Mvc;
using System.Web.Compilation;

namespace BrnShop.Web.Framework
{
    /// <summary>
    /// 主题构建引擎
    /// </summary>
    public abstract class ThemeBuildManagerViewEngine : ThemeVirtualPathProviderViewEngine
    {
        //判读文件是否存在
        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            return BuildManager.GetObjectFactory(virtualPath, false) != null;
        }
    }
}
