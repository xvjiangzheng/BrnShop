using System;
using System.Web.Mvc;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Models;

namespace BrnShop.Web.Controllers
{
    /// <summary>
    /// 新闻控制器类
    /// </summary>
    public partial class NewsController : BaseWebController
    {
        /// <summary>
        /// 详细信息
        /// </summary>
        public ActionResult Details()
        {
            //新闻id
            int newsId = GetRouteInt("newsId");
            if (newsId == 0)
                newsId = WebHelper.GetQueryInt("newsId");

            NewsInfo newsInfo = News.GetNewsById(newsId);
            if (newsInfo == null)
                return PromptView("/", "你访问的页面不存在");

            NewsModel model = new NewsModel();
            model.NewsInfo = newsInfo;
            model.NewsTypeList = News.GetNewsTypeList();

            return View(model);
        }

        /// <summary>
        /// 新闻列表
        /// </summary>
        public ActionResult List()
        {
            string newsTitle = WebHelper.GetQueryString("newsTitle");
            int newsTypeId = WebHelper.GetQueryInt("newsTypeId");
            int page = WebHelper.GetQueryInt("page");

            if (!SecureHelper.IsSafeSqlString(newsTitle))
                return PromptView(WorkContext.UrlReferrer, "您搜索的新闻不存在");

            string condition = News.GetNewsListCondition(newsTypeId, newsTitle);
            PageModel pageModel = new PageModel(10, page, News.GetNewsCount(condition));
            NewsListModel model = new NewsListModel()
            {
                PageModel = pageModel,
                NewsList = News.GetNewsList(pageModel.PageSize, pageModel.PageNumber, condition),
                NewsTitle = newsTitle,
                NewsTypeId = newsTypeId,
                NewsTypeList = News.GetNewsTypeList()
            };

            return View(model);
        }
    }
}
