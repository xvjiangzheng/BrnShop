using System;
using System.Web;
using System.Text;
using System.Data;
using System.Web.Mvc;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Admin.Models;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台商城帮助控制器类
    /// </summary>
    public partial class HelpController : BaseAdminController
    {
        /// <summary>
        /// 商城帮助列表
        /// </summary>
        public ActionResult List()
        {
            HelpListModel model = new HelpListModel()
            {
                HelpList = AdminHelps.GetHelpList()
            };
            ShopUtils.SetAdminRefererCookie(Url.Action("list"));
            return View(model);
        }

        /// <summary>
        /// 添加商城帮助分类
        /// </summary>
        [HttpGet]
        public ActionResult AddHelpCategory()
        {
            HelpCategoryModel model = new HelpCategoryModel();
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 添加商城帮助分类
        /// </summary>
        [HttpPost]
        public ActionResult AddHelpCategory(HelpCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                HelpInfo helpInfo = new HelpInfo()
                {
                    Pid = 0,
                    Title = model.HelpCategoryTitle,
                    Url = "",
                    Description = "",
                    DisplayOrder = model.DisplayOrder
                };

                AdminHelps.CreateHelp(helpInfo);
                AddAdminOperateLog("添加帮助分类", "添加帮助分类,帮助分类为:" + model.HelpCategoryTitle);
                return PromptView("帮助分类添加成功");
            }
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 添加商城帮助
        /// </summary>
        [HttpGet]
        public ActionResult AddHelp()
        {
            HelpModel model = new HelpModel();
            Load();
            return View(model);
        }

        /// <summary>
        /// 添加商城帮助
        /// </summary>
        [HttpPost]
        public ActionResult AddHelp(HelpModel model)
        {
            if (ModelState.IsValid)
            {
                HelpInfo helpInfo = new HelpInfo()
                {
                    Pid = model.Pid,
                    Title = model.HelpTitle,
                    Url = model.Url == null ? "" : model.Url,
                    Description = model.Description ?? "",
                    DisplayOrder = model.DisplayOrder
                };

                AdminHelps.CreateHelp(helpInfo);
                AddAdminOperateLog("添加帮助", "添加帮助,帮助为:" + model.HelpTitle);
                return PromptView("帮助添加成功");
            }
            Load();
            return View(model);
        }

        /// <summary>
        /// 编辑商城帮助分类
        /// </summary>
        [HttpGet]
        public ActionResult EditHelpCategory(int id = -1)
        {
            HelpInfo helpInfo = AdminHelps.GetHelpById(id);
            if (helpInfo == null)
                return PromptView("帮助分类不存在");

            HelpCategoryModel model = new HelpCategoryModel();
            model.HelpCategoryTitle = helpInfo.Title;
            model.DisplayOrder = helpInfo.DisplayOrder;
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();

            return View(model);
        }

        /// <summary>
        /// 编辑商城帮助分类
        /// </summary>
        [HttpPost]
        public ActionResult EditHelpCategory(HelpCategoryModel model, int id = -1)
        {
            HelpInfo helpInfo = AdminHelps.GetHelpById(id);
            if (helpInfo == null)
                return PromptView("帮助分类不存在");

            if (ModelState.IsValid)
            {
                helpInfo.Pid = 0;
                helpInfo.Title = model.HelpCategoryTitle;
                helpInfo.Url = "";
                helpInfo.Description = "";
                helpInfo.DisplayOrder = model.DisplayOrder;

                AdminHelps.UpdateHelp(helpInfo);
                AddAdminOperateLog("修改帮助分类", "修改帮助分类,帮助分类ID为:" + id);
                return PromptView("帮助分类修改成功");
            }

            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 编辑商城帮助
        /// </summary>
        [HttpGet]
        public ActionResult EditHelp(int id = -1)
        {
            HelpInfo helpInfo = AdminHelps.GetHelpById(id);
            if (helpInfo == null)
                return PromptView("帮助不存在");

            HelpModel model = new HelpModel();
            model.Pid = helpInfo.Pid;
            model.HelpTitle = helpInfo.Title;
            model.Url = helpInfo.Url;
            model.Description = helpInfo.Description;
            model.DisplayOrder = helpInfo.DisplayOrder;
            Load();

            return View(model);
        }

        /// <summary>
        /// 编辑商城帮助
        /// </summary>
        [HttpPost]
        public ActionResult EditHelp(HelpModel model, int id = -1)
        {
            HelpInfo helpInfo = AdminHelps.GetHelpById(id);
            if (helpInfo == null)
                return PromptView("帮助不存在");

            if (ModelState.IsValid)
            {
                helpInfo.Pid = model.Pid;
                helpInfo.Title = model.HelpTitle;
                helpInfo.Url = model.Url == null ? "" : model.Url;
                helpInfo.Description = model.Description ?? "";
                helpInfo.DisplayOrder = model.DisplayOrder;

                AdminHelps.UpdateHelp(helpInfo);
                AddAdminOperateLog("修改帮助", "修改帮助,帮助ID为:" + id);
                return PromptView("帮助修改成功");
            }

            Load();
            return View(model);
        }

        /// <summary>
        /// 删除商城帮助或分类
        /// </summary>
        public ActionResult Del(int id = -1)
        {
            int result = AdminHelps.DeleteHelpById(id);
            if (result == -1)
                return PromptView("删除失败，请先删除此分类下的帮助");

            AddAdminOperateLog("删除帮助", "删除帮助,帮助ID为:" + id);
            return PromptView("帮助删除成功");
        }

        /// <summary>
        /// 改变帮助的排序
        /// </summary>
        public ActionResult UpdateHelpDisplayOrder(int id = -1, int displayOrder = 0)
        {
            bool result = AdminHelps.UpdateHelpDisplayOrder(id, displayOrder);
            AddAdminOperateLog("修改帮助的顺序", "修改帮助的顺序,帮助ID:" + id);
            if (result)
                return Content("1");
            else
                return Content("0");
        }

        private void Load()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            List<HelpInfo> helpCategoryList = AdminHelps.GetHelpCategoryList();
            itemList.Add(new SelectListItem() { Text = "请选择分类", Value = "0" });
            foreach (HelpInfo helpInfo in helpCategoryList)
            {
                itemList.Add(new SelectListItem() { Text = helpInfo.Title, Value = helpInfo.Id.ToString() });
            }
            ViewData["helpCategoryList"] = itemList;
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
        }
    }
}
