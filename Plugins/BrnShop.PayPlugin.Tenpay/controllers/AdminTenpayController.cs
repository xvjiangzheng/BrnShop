using System;
using System.Web;
using System.Web.Mvc;

using BrnShop.Core;
using BrnShop.Web.Framework;
using BrnShop.PayPlugin.Tenpay;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台财付通控制器类
    /// </summary>
    public class AdminTenpayController : BaseAdminController
    {
        /// <summary>
        /// 配置
        /// </summary>
        [HttpGet]
        [ChildActionOnly]
        public ActionResult Config()
        {
            ConfigModel model = new ConfigModel();

            PluginSetInfo pluginSetInfo = PluginUtils.GetPluginSet();
            model.BargainorId = pluginSetInfo.BargainorId;
            model.TenpayKey = pluginSetInfo.TenpayKey;
            model.PayFee = pluginSetInfo.PayFee;
            model.FreeMoney = pluginSetInfo.FreeMoney;

            return View("~/plugins/BrnShop.PayPlugin.Tenpay/views/admintenpay/config.cshtml", model);
        }

        /// <summary>
        /// 配置
        /// </summary>
        [HttpPost]
        public ActionResult Config(ConfigModel model)
        {
            if (ModelState.IsValid)
            {
                PluginSetInfo pluginSetInfo = new PluginSetInfo();
                pluginSetInfo.BargainorId = model.BargainorId.Trim();
                pluginSetInfo.TenpayKey = model.TenpayKey.Trim();
                pluginSetInfo.PayFee = model.PayFee;
                pluginSetInfo.FreeMoney = model.FreeMoney;
                PluginUtils.SavePluginSet(pluginSetInfo);

                AddAdminOperateLog("修改财付通插件配置信息");
                return PromptView(Url.Action("config", "plugin", new { configController = "AdminTenpay", configAction = "Config" }), "插件配置修改成功");
            }
            return PromptView(Url.Action("config", "plugin", new { configController = "AdminTenpay", configAction = "Config" }), "信息有误，请重新填写");
        }
    }
}
