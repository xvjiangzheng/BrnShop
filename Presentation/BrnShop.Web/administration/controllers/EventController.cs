using System;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Threading;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Admin.Models;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台事件控制器类
    /// </summary>
    public partial class EventController : BaseAdminController
    {
        /// <summary>
        /// 事件列表
        /// </summary>
        public ActionResult List()
        {
            EventListModel model = new EventListModel();

            model.BSPEventState = BSPConfig.EventConfig.BSPEventState;
            model.BSPEventList = BSPConfig.EventConfig.BSPEventList;
            model.BSPEventPeriod = BSPConfig.EventConfig.BSPEventPeriod;

            ShopUtils.SetAdminRefererCookie(Url.Action("list"));

            return View(model);
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        [HttpGet]
        public ActionResult Add()
        {
            EventModel model = new EventModel();
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        [HttpPost]
        public ActionResult Add(EventModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Key) && BSPConfig.EventConfig.BSPEventList.Find(x => x.Key == model.Key.Trim().ToLower()) != null)
                ModelState.AddModelError("Key", "键已经存在");

            if (!string.IsNullOrWhiteSpace(model.Title) && BSPConfig.EventConfig.BSPEventList.Find(x => x.Title == model.Title.Trim().ToLower()) != null)
                ModelState.AddModelError("Title", "名称已经存在");

            if (ModelState.IsValid)
            {
                EventInfo eventInfo = new EventInfo()
                {
                    Key = model.Key.Trim().ToLower(),
                    Title = model.Title.Trim().ToLower(),
                    TimeType = model.TimeType,
                    TimeValue = model.TimeValue,
                    ClassName = model.ClassName,
                    Code = model.Code ?? "",
                    Enabled = model.Enabled
                };

                BSPConfig.EventConfig.BSPEventList.Add(eventInfo);
                BSPConfig.SaveEventConfig(BSPConfig.EventConfig);
                AddAdminOperateLog("添加事件", "添加事件,事件为:" + model.Title);
                return PromptView("事件添加成功");
            }
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 编辑事件
        /// </summary>
        [HttpGet]
        public ActionResult Edit(string key = "")
        {
            EventInfo eventInfo = BSPConfig.EventConfig.BSPEventList.Find(x => x.Key == key);
            if (eventInfo == null)
                return PromptView("事件不存在");

            EventModel model = new EventModel();
            model.Key = eventInfo.Key;
            model.Title = eventInfo.Title;
            model.TimeType = eventInfo.TimeType;
            model.TimeValue = eventInfo.TimeValue;
            model.ClassName = eventInfo.ClassName;
            model.Code = eventInfo.Code;
            model.Enabled = eventInfo.Enabled;

            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 编辑事件
        /// </summary>
        [HttpPost]
        public ActionResult Edit(EventModel model)
        {
            EventInfo eventInfo = null;

            if (!string.IsNullOrWhiteSpace(model.Key))
                eventInfo = BSPConfig.EventConfig.BSPEventList.Find(x => x.Key == model.Key);

            if (eventInfo == null)
                return PromptView("事件不存在");

            if (!string.IsNullOrWhiteSpace(model.Title))
            {
                EventInfo temp = BSPConfig.EventConfig.BSPEventList.Find(x => x.Title == model.Title.Trim().ToLower());
                if (temp != null && temp.Key != eventInfo.Key)
                    ModelState.AddModelError("Title", "名称已经存在");
            }

            if (ModelState.IsValid)
            {
                //eventInfo.Key = model.Key.Trim().ToLower(),
                eventInfo.Title = model.Title.Trim().ToLower();
                eventInfo.TimeType = model.TimeType;
                eventInfo.TimeValue = model.TimeValue;
                eventInfo.ClassName = model.ClassName;
                eventInfo.Code = model.Code ?? "";
                eventInfo.Enabled = model.Enabled;

                BSPConfig.SaveEventConfig(BSPConfig.EventConfig);
                AddAdminOperateLog("编辑事件", "编辑事件,事件为:" + model.Title);
                return PromptView("事件编辑成功");
            }
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 执行事件
        /// </summary>
        public ActionResult Execute(string key = "")
        {
            EventInfo eventInfo = BSPConfig.EventConfig.BSPEventList.Find(x => x.Key == key);
            if (eventInfo == null)
                return PromptView("事件不存在");

            BSPEvent.Execute(eventInfo.Key);

            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();
            return PromptView("事件执行成功");
        }
    }
}
