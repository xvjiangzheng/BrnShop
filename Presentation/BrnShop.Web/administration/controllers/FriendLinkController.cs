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
    /// 后台友情链接控制器类
    /// </summary>
    public partial class FriendLinkController : BaseAdminController
    {
        /// <summary>
        /// 友情链接列表
        /// </summary>
        public ActionResult List()
        {
            FriendLinkListModel model = new FriendLinkListModel();
            model.FriendLinkList = AdminFriendLinks.GetFriendLinkList();

            string[] sizeList = StringHelper.SplitString(WorkContext.ShopConfig.BrandThumbSize);

            ViewData["size"] = sizeList[sizeList.Length / 2];
            ShopUtils.SetAdminRefererCookie(Url.Action("list"));
            return View(model);
        }

        /// <summary>
        /// 添加友情链接
        /// </summary>
        [HttpGet]
        public ActionResult Add()
        {
            FriendLinkModel model = new FriendLinkModel();
            Load();
            return View(model);
        }

        /// <summary>
        /// 添加友情链接
        /// </summary>
        [HttpPost]
        public ActionResult Add(FriendLinkModel model)
        {
            if (ModelState.IsValid)
            {
                FriendLinkInfo friendLinkInfo = new FriendLinkInfo()
                {
                    Name = model.FriendLinkName,
                    Title = model.FriendLinkTitle == null ? "" : model.FriendLinkTitle,
                    Logo = model.FriendLinkLogo == null ? "" : model.FriendLinkLogo,
                    Url = model.FriendLinkUrl,
                    Target = model.Target,
                    DisplayOrder = model.DisplayOrder
                };

                AdminFriendLinks.CreateFriendLink(friendLinkInfo);
                AddAdminOperateLog("添加友情链接", "添加友情链接,友情链接为:" + model.FriendLinkName);
                return PromptView("友情链接添加成功");
            }
            Load();
            return View(model);
        }

        /// <summary>
        /// 编辑友情链接
        /// </summary>
        [HttpGet]
        public ActionResult Edit(int id = -1)
        {
            FriendLinkInfo friendLinkInfo = AdminFriendLinks.GetFriendLinkById(id);
            if (friendLinkInfo == null)
                return PromptView("友情链接不存在");

            FriendLinkModel model = new FriendLinkModel();
            model.FriendLinkName = friendLinkInfo.Name;
            model.FriendLinkTitle = friendLinkInfo.Title;
            model.FriendLinkLogo = friendLinkInfo.Logo;
            model.FriendLinkUrl = friendLinkInfo.Url;
            model.Target = friendLinkInfo.Target;
            model.DisplayOrder = friendLinkInfo.DisplayOrder;
            Load();

            return View(model);
        }

        /// <summary>
        /// 编辑友情链接
        /// </summary>
        [HttpPost]
        public ActionResult Edit(FriendLinkModel model, int id = -1)
        {
            FriendLinkInfo friendLinkInfo = AdminFriendLinks.GetFriendLinkById(id);
            if (friendLinkInfo == null)
                return PromptView("友情链接不存在");

            if (ModelState.IsValid)
            {
                friendLinkInfo.Name = model.FriendLinkName;
                friendLinkInfo.Title = model.FriendLinkTitle == null ? "" : model.FriendLinkTitle;
                friendLinkInfo.Logo = model.FriendLinkLogo == null ? "" : model.FriendLinkLogo;
                friendLinkInfo.Url = model.FriendLinkUrl;
                friendLinkInfo.Target = model.Target;
                friendLinkInfo.DisplayOrder = model.DisplayOrder;

                AdminFriendLinks.UpdateFriendLink(friendLinkInfo);
                AddAdminOperateLog("修改友情链接", "修改友情链接,友情链接ID为:" + id);
                return PromptView("友情链接修改成功");
            }

            Load();
            return View(model);
        }

        /// <summary>
        /// 删除友情链接
        /// </summary>
        public ActionResult Del(int[] idList)
        {
            AdminFriendLinks.DeleteFriendLinkById(idList);
            AddAdminOperateLog("删除友情链接", "删除友情链接,友情链接ID为:" + CommonHelper.IntArrayToString(idList));
            return PromptView("友情链接删除成功");
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
