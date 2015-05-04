using System;
using System.Web;
using System.Web.Mvc;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Admin.Models;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台banner控制器类
    /// </summary>
    public partial class BannerController : BaseAdminController
    {
        /// <summary>
        /// banner列表
        /// </summary>
        public ActionResult List(int pageSize = 15, int pageNumber = 1)
        {
            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminBanners.AdminGetBannerCount());

            BannerListModel model = new BannerListModel()
            {
                PageModel = pageModel,
                BannerList = AdminBanners.AdminGetBannerList(pageSize, pageNumber)
            };

            ShopUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}",
                                                          Url.Action("list"),
                                                          pageModel.PageNumber,
                                                          pageModel.PageSize));
            return View(model);
        }

        /// <summary>
        /// 添加banner
        /// </summary>
        [HttpGet]
        public ActionResult Add()
        {
            BannerModel model = new BannerModel();
            Load();
            return View(model);
        }

        /// <summary>
        /// 添加banner
        /// </summary>
        [HttpPost]
        public ActionResult Add(BannerModel model)
        {
            if (ModelState.IsValid)
            {
                BannerInfo bannerInfo = new BannerInfo()
                {
                    Type = model.BannerType,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    IsShow = model.IsShow,
                    Title = model.BannerTitle == null ? "" : model.BannerTitle,
                    Img = model.Img,
                    Url = model.Url,
                    DisplayOrder = model.DisplayOrder
                };

                AdminBanners.CreateBanner(bannerInfo);
                AddAdminOperateLog("添加banner", "添加banner,banner为:" + model.BannerTitle);
                return PromptView("banner添加成功");
            }
            Load();
            return View(model);
        }

        /// <summary>
        /// 编辑banner
        /// </summary>
        [HttpGet]
        public ActionResult Edit(int id = -1)
        {
            BannerInfo bannerInfo = AdminBanners.AdminGetBannerById(id);
            if (bannerInfo == null)
                return PromptView("Banner不存在");

            BannerModel model = new BannerModel();
            model.BannerType = bannerInfo.Type;
            model.StartTime = bannerInfo.StartTime;
            model.EndTime = bannerInfo.EndTime;
            model.IsShow = bannerInfo.IsShow;
            model.BannerTitle = bannerInfo.Title;
            model.Img = bannerInfo.Img;
            model.Url = bannerInfo.Url;
            model.DisplayOrder = bannerInfo.DisplayOrder;
            Load();

            return View(model);
        }

        /// <summary>
        /// 编辑banner
        /// </summary>
        [HttpPost]
        public ActionResult Edit(BannerModel model, int id = -1)
        {
            BannerInfo bannerInfo = AdminBanners.AdminGetBannerById(id);
            if (bannerInfo == null)
                return PromptView("Banner不存在");

            if (ModelState.IsValid)
            {
                //bannerInfo.Type = model.BannerType;
                bannerInfo.StartTime = model.StartTime;
                bannerInfo.EndTime = model.EndTime;
                bannerInfo.IsShow = model.IsShow;
                bannerInfo.Title = model.BannerTitle == null ? "" : model.BannerTitle;
                bannerInfo.Img = model.Img;
                bannerInfo.Url = model.Url;
                bannerInfo.DisplayOrder = model.DisplayOrder;

                AdminBanners.UpdateBanner(bannerInfo);
                AddAdminOperateLog("修改banner", "修改banner,bannerID为:" + id);
                return PromptView("banner修改成功");
            }

            Load();
            return View(model);
        }

        /// <summary>
        /// 删除banner
        /// </summary>
        public ActionResult Del(int[] idList)
        {
            AdminBanners.DeleteBannerById(idList);
            AddAdminOperateLog("删除banner", "删除banner,bannerID为:" + CommonHelper.IntArrayToString(idList));
            return PromptView("banner删除成功");
        }

        private void Load()
        {
            string allowImgType = string.Empty;
            string[] imgTypeList = StringHelper.SplitString(BSPConfig.ShopConfig.UploadImgType, ",");
            foreach (string imgType in imgTypeList)
                allowImgType += string.Format("*{0};", imgType.ToLower());

            ViewData["allowImgType"] = allowImgType;
            ViewData["maxImgSize"] = BSPConfig.ShopConfig.UploadImgSize;
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
        }
    }
}
