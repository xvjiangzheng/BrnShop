using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using BrnShop.Core;
using BrnShop.Web.Framework;
using BrnShop.PayPlugin.EMSRemit;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台邮局汇款控制器类
    /// </summary>
    public class AdminEMSRemitController : BaseAdminController
    {
        /// <summary>
        /// 配置
        /// </summary>
        [HttpGet]
        [ChildActionOnly]
        public ActionResult Config()
        {
            ConfigModel model = new ConfigModel();
            model.PayFee = PluginUtils.GetPluginSet().PayFee;
            model.FreeMoney = PluginUtils.GetPluginSet().FreeMoney;
            return View("~/plugins/BrnShop.PayPlugin.EMSRemit/views/adminemsremit/config.cshtml", model);
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
                pluginSetInfo.PayFee = model.PayFee;
                pluginSetInfo.FreeMoney = model.FreeMoney;
                PluginUtils.SavePluginSet(pluginSetInfo);

                AddAdminOperateLog("修改邮局汇款插件配置信息");
                return PromptView(Url.Action("config", "plugin", new { configController = "AdminEMSRemit", configAction = "Config" }), "插件配置修改成功");
            }
            return PromptView(Url.Action("config", "plugin", new { configController = "AdminEMSRemit", configAction = "Config" }), "信息有误，请重新填写");
        }
    }
}
