using System;
using System.Web.Mvc;

namespace BrnShop.Web.Framework
{
    /// <summary>
    /// 分页Html扩展
    /// </summary>
    public static class PagerHtmlExtension
    {
        /// <summary>
        /// 后台分页
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pageModel">分页对象</param>
        /// <returns></returns>
        public static AdminPager AdminPager(this HtmlHelper helper, PageModel pageModel)
        {
            return new AdminPager(pageModel);
        }

        /// <summary>
        /// 前台分页
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pageModel">分页对象</param>
        /// <returns></returns>
        public static WebPager WebPager(this HtmlHelper helper, PageModel pageModel)
        {
            return new WebPager(pageModel, helper.ViewContext);
        }
    }
}
