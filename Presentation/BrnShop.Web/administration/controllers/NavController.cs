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
    /// 后台导航控制器类
    /// </summary>
    public partial class NavController : BaseAdminController
    {
        /// <summary>
        /// 导航列表
        /// </summary>
        public ActionResult List()
        {
            NavListModel model = new NavListModel();
            model.NavList = AdminNavs.GetNavList();
            ShopUtils.SetAdminRefererCookie(Url.Action("list"));
            return View(model);
        }

        /// <summary>
        /// 添加导航
        /// </summary>
        [HttpGet]
        public ActionResult Add()
        {
            NavModel model = new NavModel();
            Load();
            return View(model);
        }

        /// <summary>
        /// 添加导航
        /// </summary>
        [HttpPost]
        public ActionResult Add(NavModel model)
        {
            if (model.Pid != 0 && AdminNavs.GetNavById(model.Pid) == null)
                ModelState.AddModelError("Pid", "父导航不存在");

            if (ModelState.IsValid)
            {
                NavInfo navInfo = new NavInfo()
                {
                    Pid = model.Pid,
                    Name = model.NavName,
                    Title = model.NavTitle == null ? "" : model.NavTitle,
                    Url = model.NavUrl,
                    Target = model.Target,
                    DisplayOrder = model.DisplayOrder
                };

                AdminNavs.CreateNav(navInfo);
                AddAdminOperateLog("添加导航", "添加导航,导航为:" + model.NavName);
                return PromptView("导航添加成功");
            }
            Load();
            return View(model);
        }

        /// <summary>
        /// 编辑导航
        /// </summary>
        [HttpGet]
        public ActionResult Edit(int id = -1)
        {
            NavInfo navInfo = AdminNavs.GetNavById(id);
            if (navInfo == null)
                return PromptView("导航不存在");

            NavModel model = new NavModel();
            model.Pid = navInfo.Pid;
            model.NavName = navInfo.Name;
            model.NavTitle = navInfo.Title;
            model.NavUrl = navInfo.Url;
            model.Target = navInfo.Target;
            model.DisplayOrder = navInfo.DisplayOrder;
            Load();

            return View(model);
        }

        /// <summary>
        /// 编辑导航
        /// </summary>
        [HttpPost]
        public ActionResult Edit(NavModel model, int id = -1)
        {
            NavInfo navInfo = AdminNavs.GetNavById(id);
            if (navInfo == null)
                return PromptView("导航不存在");

            if (model.Pid == navInfo.Id)
                ModelState.AddModelError("Pid", "不能将自己作为父导航");

            if (model.Pid != 0 && AdminNavs.GetNavById(model.Pid) == null)
                ModelState.AddModelError("Pid", "父导航不存在");

            if (model.Pid != 0 && AdminNavs.GetSubNavList(navInfo.Id).Exists(x => x.Id == model.Pid))
                ModelState.AddModelError("Pid", "不能将导航调整到自己子导航下");

            if (ModelState.IsValid)
            {
                int oldPid = navInfo.Pid;

                navInfo.Pid = model.Pid;
                navInfo.Name = model.NavName;
                navInfo.Title = model.NavTitle == null ? "" : model.NavTitle;
                navInfo.Url = model.NavUrl;
                navInfo.Target = model.Target;
                navInfo.DisplayOrder = model.DisplayOrder;

                AdminNavs.UpdateNav(navInfo, oldPid);
                AddAdminOperateLog("修改导航", "修改导航,导航ID为:" + id);
                return PromptView("导航修改成功");
            }

            Load();
            return View(model);
        }

        /// <summary>
        /// 删除导航
        /// </summary>
        public ActionResult Del(int id = -1)
        {
            int result = AdminNavs.DeleteNavById(id);
            if (result == 0)
                return PromptView("删除失败，请先删除此导航下的子导航");

            AddAdminOperateLog("删除导航", "删除导航,导航ID为:" + id);
            return PromptView("导航删除成功");
        }

        private void Load()
        {
            ViewData["navList"] = AdminNavs.GetNavList();
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
        }
    }
}
