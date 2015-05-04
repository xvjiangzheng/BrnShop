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
    /// 后台管理员组控制器类
    /// </summary>
    public partial class AdminGroupController : BaseAdminController
    {
        /// <summary>
        /// 管理员组列表
        /// </summary>
        public ActionResult List()
        {
            AdminGroupListModel model = new AdminGroupListModel()
            {
                AdminGroupList = AdminGroups.GetCustomerAdminGroupList()
            };
            ShopUtils.SetAdminRefererCookie(Url.Action("list"));
            return View(model);
        }

        /// <summary>
        /// 添加管理员组
        /// </summary>
        [HttpGet]
        public ActionResult Add()
        {
            AdminGroupModel model = new AdminGroupModel();
            Load();
            return View(model);
        }

        /// <summary>
        /// 添加管理员组
        /// </summary>
        [HttpPost]
        public ActionResult Add(AdminGroupModel model)
        {
            if (AdminGroups.GetAdminGroupIdByTitle(model.AdminGroupTitle) > 0)
                ModelState.AddModelError("AdminGroupTitle", "名称已经存在");

            if (ModelState.IsValid)
            {
                AdminGroupInfo adminGroupInfo = new AdminGroupInfo()
                {
                    Title = model.AdminGroupTitle,
                    ActionList = CommonHelper.StringArrayToString(model.ActionList).ToLower()
                };

                AdminGroups.CreateAdminGroup(adminGroupInfo);
                AddAdminOperateLog("添加管理员组", "添加管理员组,管理员组为:" + model.AdminGroupTitle);
                return PromptView("管理员组添加成功");
            }
            Load();
            return View(model);
        }

        /// <summary>
        /// 编辑管理员组
        /// </summary>
        [HttpGet]
        public ActionResult Edit(int adminGid = -1)
        {
            if (adminGid < 3)
                return PromptView("内置管理员组不能修改");

            AdminGroupInfo adminGroupInfo = AdminGroups.GetAdminGroupById(adminGid);
            if (adminGroupInfo == null)
                return PromptView("管理员组不存在");

            AdminGroupModel model = new AdminGroupModel();
            model.AdminGroupTitle = adminGroupInfo.Title;
            model.ActionList = StringHelper.SplitString(adminGroupInfo.ActionList);

            Load();
            return View(model);
        }

        /// <summary>
        /// 编辑管理员组
        /// </summary>
        [HttpPost]
        public ActionResult Edit(AdminGroupModel model, int adminGid = -1)
        {
            if (adminGid < 3)
                return PromptView("内置管理员组不能修改");

            AdminGroupInfo adminGroupInfo = AdminGroups.GetAdminGroupById(adminGid);
            if (adminGroupInfo == null)
                return PromptView("管理员组不存在");

            int adminGid2 = AdminGroups.GetAdminGroupIdByTitle(model.AdminGroupTitle);
            if (adminGid2 > 0 && adminGid2 != adminGid)
                ModelState.AddModelError("AdminGroupTitle", "名称已经存在");

            if (ModelState.IsValid)
            {
                adminGroupInfo.Title = model.AdminGroupTitle;
                adminGroupInfo.ActionList = CommonHelper.StringArrayToString(model.ActionList).ToLower();

                AdminGroups.UpdateAdminGroup(adminGroupInfo);
                AddAdminOperateLog("修改管理员组", "修改管理员组,管理员组ID为:" + adminGid);
                return PromptView("管理员组修改成功");
            }

            Load();
            return View(model);
        }

        /// <summary>
        /// 删除管理员组
        /// </summary>
        public ActionResult Del(int adminGid = -1)
        {
            int result = AdminGroups.DeleteAdminGroupById(adminGid);
            if (result == -1)
                return PromptView("删除失败请先转移或删除此管理员组下的用户");
            else if (result == -2)
                return PromptView("内置管理员组不能删除");

            AddAdminOperateLog("删除管理员组", "删除管理员组,管理员组ID为:" + adminGid);
            return PromptView("管理员组删除成功");
        }

        private void Load()
        {
            ViewData["adminActionTree"] = AdminActions.GetAdminActionTree();
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
        }
    }
}
