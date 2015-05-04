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
    /// 后台筛选词控制器类
    /// </summary>
    public partial class FilterWordController : BaseAdminController
    {
        /// <summary>
        /// 筛选词列表
        /// </summary>
        /// <param name="searchFilterWord">搜索筛选词</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        public ActionResult List(string searchFilterWord, int pageSize = 15, int pageNumber = 1)
        {
            List<FilterWordInfo> filterWordList = AdminFilterWords.GetFilterWordList();
            if (!string.IsNullOrWhiteSpace(searchFilterWord))
                filterWordList = filterWordList.FindAll(x => x.Match.Contains(searchFilterWord));

            PageModel pageModel = new PageModel(pageSize, pageNumber, filterWordList.Count);

            FilterWordListModel model = new FilterWordListModel()
            {
                FilterWordList = filterWordList,
                PageModel = pageModel,
                SearchFilterWord = searchFilterWord
            };
            ShopUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&searchFilterWord={3}",
                                                          Url.Action("list"),
                                                          pageModel.PageNumber,
                                                          pageModel.PageSize,
                                                          searchFilterWord));
            return View(model);
        }

        /// <summary>
        /// 添加筛选词
        /// </summary>
        [HttpGet]
        public ActionResult Add()
        {
            FilterWordModel model = new FilterWordModel();
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 添加筛选词
        /// </summary>
        [HttpPost]
        public ActionResult Add(FilterWordModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Match) && AdminFilterWords.GetFilterWordList().Find(x => x.Match == model.Match.Trim()) != null)
                ModelState.AddModelError("Match", "匹配词已经存在");

            if (ModelState.IsValid)
            {
                FilterWordInfo filterWordInfo = new FilterWordInfo()
                {
                    Match = model.Match.Trim(),
                    Replace = model.Replace.Trim()
                };

                AdminFilterWords.AddFilterWord(filterWordInfo);
                AddAdminOperateLog("添加筛选词", "添加筛选词,筛选词为:" + model.Match);
                return PromptView("筛选词添加成功");
            }
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 编辑筛选词
        /// </summary>
        [HttpGet]
        public ActionResult Edit(int id = -1)
        {
            FilterWordInfo filterWordInfo = AdminFilterWords.GetFilterWordList().Find(x => x.Id == id);
            if (filterWordInfo == null)
                return PromptView("筛选词不存在");

            FilterWordModel model = new FilterWordModel();
            model.Match = filterWordInfo.Match;
            model.Replace = filterWordInfo.Replace;

            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();

            return View(model);
        }

        /// <summary>
        /// 编辑筛选词
        /// </summary>
        [HttpPost]
        public ActionResult Edit(FilterWordModel model, int id = -1)
        {
            List<FilterWordInfo> filterWordList = AdminFilterWords.GetFilterWordList();
            FilterWordInfo filterWordInfo = filterWordList.Find(x => x.Id == id);
            if (filterWordInfo == null)
                return PromptView("筛选词不存在");

            if (!string.IsNullOrWhiteSpace(model.Match))
            {
                FilterWordInfo filterWordInfo2 = filterWordList.Find(x => x.Match == model.Match.Trim());
                if (filterWordInfo2 != null && filterWordInfo2.Id != id)
                    ModelState.AddModelError("Match", "筛选词已经存在");
            }

            if (ModelState.IsValid)
            {
                filterWordInfo.Match = model.Match.Trim();
                filterWordInfo.Replace = model.Replace.Trim();

                AdminFilterWords.UpdateFilterWord(filterWordInfo);
                AddAdminOperateLog("修改筛选词", "修改筛选词,筛选词ID为:" + id);
                return PromptView("筛选词修改成功");
            }

            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 删除筛选词
        /// </summary>
        public ActionResult Del(int[] idList)
        {
            AdminFilterWords.DeleteFilterWordById(idList);
            AddAdminOperateLog("删除筛选词", "删除筛选词,筛选词ID为:" + CommonHelper.IntArrayToString(idList));
            return PromptView("筛选词删除成功");
        }
    }
}
