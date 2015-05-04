using System;

namespace BrnShop.Web.Framework
{
    /// <summary>
    /// 移动前台视图页面基类型
    /// </summary>
    public abstract class MobileViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        public MobileWorkContext WorkContext;

        public sealed override void InitHelpers()
        {
            base.InitHelpers();
            WorkContext = ((BaseMobileController)(this.ViewContext.Controller)).WorkContext;
        }

        public sealed override void Write(object value)
        {
            Output.Write(value);
        }
    }

    /// <summary>
    /// 移动前台视图页面基类型
    /// </summary>
    public abstract class MobileViewPage : WebViewPage<dynamic>
    {
    }
}
