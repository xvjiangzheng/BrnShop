using System;
using System.Web;
using System.Data;
using System.Web.Mvc;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Admin.Models;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台禁止IP控制器类
    /// </summary>
    public partial class BannedIPController : BaseAdminController
    {
        /// <summary>
        /// 禁止IP列表
        /// </summary>
        /// <param name="ip">ip</param>
        /// <param name="sortOptions">排序</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        public ActionResult List(string ip, string sortColumn, string sortDirection, int pageSize = 15, int pageNumber = 1)
        {
            string sort = AdminBannedIPs.AdminGetBannedIPListSort(sortColumn, sortDirection);
            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminBannedIPs.AdminGetBannedIPCount(ip));

            BannedIPListModel model = new BannedIPListModel()
            {
                BannedIPList = AdminBannedIPs.AdminGetBannedIPList(pageModel.PageSize, pageModel.PageNumber, ip, sort),
                PageModel = pageModel,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                IP = ip
            };
            ShopUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&sortColumn={3}&sortDirection={4}&ip={5}",
                                                          Url.Action("list"),
                                                          pageModel.PageNumber,
                                                          pageModel.PageSize,
                                                          sortColumn,
                                                          sortDirection,
                                                          ip));
            return View(model);
        }

        /// <summary>
        /// 添加禁止IP
        /// </summary>
        [HttpGet]
        public ActionResult Add()
        {
            BannedIPModel model = new BannedIPModel();
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 添加禁止IP
        /// </summary>
        [HttpPost]
        public ActionResult Add(BannedIPModel model)
        {
            string ip = "";
            if (string.IsNullOrWhiteSpace(model.IP4))
                ip = string.Format("{0}.{1}.{2}", model.IP1, model.IP2, model.IP3);
            else
                ip = string.Format("{0}.{1}.{2}.{3}", model.IP1, model.IP2, model.IP3, model.IP4);

            if (AdminBannedIPs.GetBannedIPIdByIP(ip) > 0)
                ModelState.AddModelError("IP4", "IP已经存在");

            if (ModelState.IsValid)
            {
                BannedIPInfo bannedIPInfo = new BannedIPInfo()
                {
                    IP = ip,
                    LiftBanTime = model.LiftBanTime
                };

                AdminBannedIPs.AddBannedIP(bannedIPInfo);
                AddAdminOperateLog("添加禁止IP", "添加禁止IP,禁止IP为:" + ip);
                return PromptView("禁止IP添加成功");
            }
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 编辑禁止IP
        /// </summary>
        [HttpGet]
        public ActionResult Edit(int id = -1)
        {
            BannedIPInfo bannedIPInfo = AdminBannedIPs.GetBannedIPById(id);
            if (bannedIPInfo == null)
                return PromptView("禁止IP不存在");

            string[] ipList = StringHelper.SplitString(bannedIPInfo.IP, ".");

            BannedIPModel model = new BannedIPModel();
            model.IP1 = ipList[0];
            model.IP2 = ipList[1];
            model.IP3 = ipList[2];
            model.IP4 = ipList.Length == 4 ? ipList[3] : "";
            model.LiftBanTime = bannedIPInfo.LiftBanTime;

            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();

            return View(model);
        }

        /// <summary>
        /// 编辑禁止IP
        /// </summary>
        [HttpPost]
        public ActionResult Edit(BannedIPModel model, int id = -1)
        {
            BannedIPInfo bannedIPInfo = AdminBannedIPs.GetBannedIPById(id);
            if (bannedIPInfo == null)
                return PromptView("禁止IP不存在");

            string ip = "";
            if (string.IsNullOrWhiteSpace(model.IP4))
                ip = string.Format("{0}.{1}.{2}", model.IP1, model.IP2, model.IP3);
            else
                ip = string.Format("{0}.{1}.{2}.{3}", model.IP1, model.IP2, model.IP3, model.IP4);

            int id2 = AdminBannedIPs.GetBannedIPIdByIP(ip);
            if (id2 > 0 && id2 != id)
                ModelState.AddModelError("IP4", "IP已经存在");

            if (ModelState.IsValid)
            {
                bannedIPInfo.IP = ip;
                bannedIPInfo.LiftBanTime = model.LiftBanTime;

                AdminBannedIPs.UpdateBannedIP(bannedIPInfo);
                AddAdminOperateLog("修改禁止IP", "修改禁止IP,禁止IPID为:" + id);
                return PromptView("禁止IP修改成功");
            }

            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 删除禁止IP
        /// </summary>
        public ActionResult Del(int[] idList)
        {
            AdminBannedIPs.DeleteBannedIPById(idList);
            AddAdminOperateLog("删除禁止IP", "删除禁止IP,禁止IPID为:" + CommonHelper.IntArrayToString(idList));
            return PromptView("禁止IP删除成功");
        }
    }
}
