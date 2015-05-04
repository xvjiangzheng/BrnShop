using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Admin.Models;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台插件控制器类
    /// </summary>
    public partial class PluginController : BaseAdminController
    {
        /// <summary>
        /// 插件列表
        /// </summary>
        /// <param name="type">插件类型</param>
        public ActionResult List(int type = -1)
        {
            PluginType pluginType = (PluginType)type;

            PluginListModel model = new PluginListModel();
            model.PluginType = pluginType;
            model.InstalledPluginList = AdminPlugins.GetInstalledPluginList(pluginType);
            model.UninstalledPluginList = AdminPlugins.GetUnInstalledPluginList(pluginType);

            ShopUtils.SetAdminRefererCookie(string.Format("{0}?type={1}", Url.Action("list"), type));
            return View(model);
        }

        /// <summary>
        /// 安装插件
        /// </summary>
        /// <param name="systemName">插件系统名称</param>
        /// <returns></returns>
        public ActionResult Install(string systemName)
        {
            AdminPlugins.Install(systemName);
            AddAdminOperateLog("安装插件", "安装插件,插件为:" + systemName);
            return PromptView("插件安装成功");
        }

        /// <summary>
        /// 卸载插件
        /// </summary>
        /// <param name="systemName">插件系统名称</param>
        /// <returns></returns>
        public ActionResult Uninstall(string systemName)
        {
            AdminPlugins.Uninstall(systemName);
            AddAdminOperateLog("卸载插件", "卸载插件,插件为:" + systemName);
            return PromptView("插件卸载成功");
        }

        /// <summary>
        /// 编辑插件
        /// </summary>
        [HttpGet]
        public ActionResult Edit(string systemName)
        {
            PluginInfo pluginInfo = AdminPlugins.GetPluginBySystemName(systemName);
            if (pluginInfo == null)
                return PromptView("插件不存在");

            PluginModel model = new PluginModel();
            model.FriendlyName = pluginInfo.FriendlyName;
            model.Description = pluginInfo.Description;
            model.DisplayOrder = pluginInfo.DisplayOrder;
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();

            return View(model);
        }

        /// <summary>
        /// 编辑插件
        /// </summary>
        /// <param name="systemName">插件系统名称</param>
        /// <param name="model">插件模型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(string systemName, PluginModel model)
        {
            PluginInfo pluginInfo = AdminPlugins.GetPluginBySystemName(systemName);
            if (pluginInfo == null)
                return PromptView("插件不存在");

            AdminPlugins.Edit(systemName, model.FriendlyName, model.Description, model.DisplayOrder);
            AddAdminOperateLog("编辑插件", "编辑插件,插件为:" + systemName);
            return PromptView("插件编辑成功");
        }

        /// <summary>
        /// 设置默认插件
        /// </summary>
        /// <param name="systemName">插件系统名称</param>
        /// <returns></returns>
        public ActionResult Default(string systemName)
        {
            AdminPlugins.Default(systemName);
            AddAdminOperateLog("设置默认插件", "设置默认插件,插件为:" + systemName);
            return PromptView("设置默认插件成功");
        }

        /// <summary>
        /// 配置插件
        /// </summary>
        /// <param name="systemName">插件系统名称</param>
        /// <param name="configController">配置控制器</param>
        /// <param name="configAction">配置动作方法</param>
        /// <returns></returns>
        public ActionResult Config(string systemName = "", string configController = "", string configAction = "")
        {
            ConfigModel model = new ConfigModel();
            if (!string.IsNullOrWhiteSpace(systemName))
            {
                PluginInfo pluginInfo = AdminPlugins.GetPluginBySystemName(systemName);
                if (pluginInfo == null)
                    return PromptView("插件不存在");

                model.ConfigController = pluginInfo.Instance.ConfigController;
                model.ConfigAction = pluginInfo.Instance.ConfigAction;
            }
            else
            {
                model.ConfigController = configController;
                model.ConfigAction = configAction;
            }

            if (Request.QueryString.Count > 0)
            {
                RouteValueDictionary routeValues = new RouteValueDictionary();
                foreach (string key in Request.QueryString.AllKeys)
                {
                    routeValues.Add(key, Request.QueryString[key]);
                }
                model.ConfigRouteValues = routeValues;
            }

            return View(model);
        }
    }
}
