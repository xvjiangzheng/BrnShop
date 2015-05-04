using System;

namespace BrnShop.Web.Framework
{
    /// <summary>
    /// PC前台视图页面基类型
    /// </summary>
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        public WebWorkContext WorkContext;

        public sealed override void InitHelpers()
        {
            base.InitHelpers();
            WorkContext = ((BaseWebController)(this.ViewContext.Controller)).WorkContext;
        }

        public sealed override void Write(object value)
        {
            Output.Write(value);
        }
    }

    /// <summary>
    /// PC前台视图页面基类型
    /// </summary>
    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }
}
