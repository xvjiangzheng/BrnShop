using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Admin.Models;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台新闻控制器类
    /// </summary>
    public partial class NewsController : BaseAdminController
    {
        /// <summary>
        /// 新闻类型列表
        /// </summary>
        public ActionResult NewsTypeList()
        {
            List<NewsTypeInfo> newsTypeList = News.GetNewsTypeList();
            ShopUtils.SetAdminRefererCookie(Url.Action("newstypelist"));
            return View(newsTypeList);
        }

        /// <summary>
        /// 添加新闻类型
        /// </summary>
        [HttpGet]
        public ActionResult AddNewsType()
        {
            NewsTypeModel model = new NewsTypeModel();
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 添加新闻类型
        /// </summary>
        [HttpPost]
        public ActionResult AddNewsType(NewsTypeModel model)
        {
            if (AdminNews.GetNewsTypeByName(model.NewsTypeName) != null)
                ModelState.AddModelError("NewsTypeName", "名称已经存在");

            if (ModelState.IsValid)
            {
                NewsTypeInfo newsTypeInfo = new NewsTypeInfo()
                {
                    Name = model.NewsTypeName,
                    DisplayOrder = model.DisplayOrder
                };

                AdminNews.CreateNewsType(newsTypeInfo);
                AddAdminOperateLog("添加新闻类型", "添加新闻类型,新闻类型为:" + model.NewsTypeName);
                return PromptView("新闻类型添加成功");
            }
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 编辑新闻类型
        /// </summary>
        [HttpGet]
        public ActionResult EditNewsType(int newsTypeId = -1)
        {
            NewsTypeInfo newsTypeInfo = AdminNews.GetNewsTypeById(newsTypeId);
            if (newsTypeInfo == null)
                return PromptView("新闻类型不存在");

            NewsTypeModel model = new NewsTypeModel();
            model.NewsTypeName = newsTypeInfo.Name;
            model.DisplayOrder = newsTypeInfo.DisplayOrder;
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();

            return View(model);
        }

        /// <summary>
        /// 编辑新闻类型
        /// </summary>
        [HttpPost]
        public ActionResult EditNewsType(NewsTypeModel model, int newsTypeId = -1)
        {
            NewsTypeInfo newsTypeInfo = AdminNews.GetNewsTypeById(newsTypeId);
            if (newsTypeInfo == null)
                return PromptView("新闻类型不存在");

            NewsTypeInfo newsTypeInfo2 = AdminNews.GetNewsTypeByName(model.NewsTypeName);
            if (newsTypeInfo2 != null && newsTypeInfo2.NewsTypeId != newsTypeId)
                ModelState.AddModelError("NewsTypeName", "名称已经存在");

            if (ModelState.IsValid)
            {
                newsTypeInfo.Name = model.NewsTypeName;
                newsTypeInfo.DisplayOrder = model.DisplayOrder;

                AdminNews.UpdateNewsType(newsTypeInfo);
                AddAdminOperateLog("修改新闻类型", "修改新闻类型,新闻类型ID为:" + newsTypeId);
                return PromptView("新闻类型修改成功");
            }

            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 删除新闻类型
        /// </summary>
        public ActionResult DelNewsType(int newsTypeId = -1)
        {
            AdminNews.DeleteNewsTypeById(newsTypeId);
            AddAdminOperateLog("删除新闻类型", "删除新闻类型,新闻类型ID为:" + newsTypeId);
            return PromptView("新闻类型删除成功");
        }



        /// <summary>
        /// 新闻列表
        /// </summary>
        public ActionResult NewsList(string newsTitle, string sortColumn, string sortDirection, int newsTypeId = 0, int pageSize = 15, int pageNumber = 1)
        {
            string condition = AdminNews.AdminGetNewsListCondition(newsTypeId, newsTitle);
            string sort = AdminNews.AdminGetNewsListSort(sortColumn, sortDirection);

            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminNews.AdminGetNewsCount(condition));

            NewsListModel model = new NewsListModel()
            {
                NewsList = AdminNews.AdminGetNewsList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                PageModel = pageModel,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                NewsTypeId = newsTypeId,
                NewsTitle = newsTitle
            };
            ShopUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&sortColumn={3}&sortDirection={4}&newsTypeId={5}&newsTitle={6}",
                                                          Url.Action("newslist"),
                                                          pageModel.PageNumber,
                                                          pageModel.PageSize,
                                                          sortColumn,
                                                          sortDirection,
                                                          newsTypeId,
                                                          newsTitle));
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "全部类型", Value = "0" });
            foreach (NewsTypeInfo newsTypeInfo in AdminNews.GetNewsTypeList())
            {
                list.Add(new SelectListItem() { Text = newsTypeInfo.Name, Value = newsTypeInfo.NewsTypeId.ToString() });
            }
            ViewData["newsTypeList"] = list;
            return View(model);
        }

        /// <summary>
        /// 添加新闻
        /// </summary>
        [HttpGet]
        public ActionResult AddNews()
        {
            NewsModel model = new NewsModel();
            Load();
            return View(model);
        }

        /// <summary>
        /// 添加新闻
        /// </summary>
        [HttpPost]
        public ActionResult AddNews(NewsModel model)
        {
            if (AdminNews.AdminGetNewsIdByTitle(model.Title) > 0)
                ModelState.AddModelError("Title", "标题已经存在");

            if (ModelState.IsValid)
            {
                NewsInfo newsInfo = new NewsInfo()
                {
                    NewsTypeId = model.NewsTypeId,
                    IsShow = model.IsShow,
                    IsTop = model.IsTop,
                    IsHome = model.IsHome,
                    DisplayOrder = model.DisplayOrder,
                    AddTime = DateTime.Now,
                    Title = model.Title,
                    Url = model.Url == null ? "" : model.Url,
                    Body = model.Body ?? ""
                };

                AdminNews.CreateNews(newsInfo);
                AddAdminOperateLog("添加新闻", "添加新闻,新闻为:" + model.Title);
                return PromptView("新闻添加成功");
            }

            Load();
            return View(model);
        }

        /// <summary>
        /// 编辑新闻
        /// </summary>
        [HttpGet]
        public ActionResult EditNews(int newsId = -1)
        {
            NewsInfo newsInfo = AdminNews.AdminGetNewsById(newsId);
            if (newsInfo == null)
                return PromptView("新闻不存在");

            NewsModel model = new NewsModel();
            model.NewsTypeId = newsInfo.NewsTypeId;
            model.IsShow = newsInfo.IsShow;
            model.IsTop = newsInfo.IsTop;
            model.IsHome = newsInfo.IsHome;
            model.DisplayOrder = newsInfo.DisplayOrder;
            model.Title = newsInfo.Title;
            model.Url = newsInfo.Url;
            model.Body = newsInfo.Body;

            Load();
            return View(model);
        }

        /// <summary>
        /// 编辑新闻
        /// </summary>
        [HttpPost]
        public ActionResult EditNews(NewsModel model, int newsId = -1)
        {
            NewsInfo newsInfo = AdminNews.AdminGetNewsById(newsId);
            if (newsInfo == null)
                return PromptView("新闻不存在");

            int newsId2 = AdminNews.AdminGetNewsIdByTitle(model.Title);
            if (newsId2 > 0 && newsId2 != newsId)
                ModelState.AddModelError("Title", "名称已经存在");

            if (ModelState.IsValid)
            {
                newsInfo.NewsTypeId = model.NewsTypeId;
                newsInfo.IsShow = model.IsShow;
                newsInfo.IsTop = model.IsTop;
                newsInfo.IsHome = model.IsHome;
                newsInfo.DisplayOrder = model.DisplayOrder;
                newsInfo.Title = model.Title;
                newsInfo.Url = model.Url == null ? "" : model.Url;
                newsInfo.Body = model.Body ?? "";

                AdminNews.UpdateNews(newsInfo);
                AddAdminOperateLog("修改新闻", "修改新闻,新闻ID为:" + newsId);
                return PromptView("新闻修改成功");
            }

            Load();
            return View(model);
        }

        /// <summary>
        /// 删除新闻
        /// </summary>
        public ActionResult DelNews(int[] newsIdList)
        {
            AdminNews.DeleteNewsById(newsIdList);
            AddAdminOperateLog("删除新闻", "删除新闻,新闻ID为:" + CommonHelper.IntArrayToString(newsIdList));
            return PromptView("新闻删除成功");
        }

        private void Load()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "请选择类型", Value = "0" });
            foreach (NewsTypeInfo newsTypeInfo in AdminNews.GetNewsTypeList())
            {
                list.Add(new SelectListItem() { Text = newsTypeInfo.Name, Value = newsTypeInfo.NewsTypeId.ToString() });
            }
            ViewData["newsTypeList"] = list;
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
        }
    }
}
