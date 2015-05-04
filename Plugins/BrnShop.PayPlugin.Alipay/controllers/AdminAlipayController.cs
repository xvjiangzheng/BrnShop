using System;
using System.Web;
using System.Web.Mvc;

using BrnShop.Core;
using BrnShop.Web.Framework;
using BrnShop.PayPlugin.Alipay;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台支付宝控制器类
    /// </summary>
    public class AdminAlipayController : BaseAdminController
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
            model.Partner = pluginSetInfo.Partner;
            model.Key = pluginSetInfo.Key;
            model.Seller = pluginSetInfo.Seller;
            model.PayFee = pluginSetInfo.PayFee;
            model.FreeMoney = pluginSetInfo.FreeMoney;

            return View("~/plugins/BrnShop.PayPlugin.Alipay/views/adminalipay/config.cshtml", model);
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
                pluginSetInfo.Partner = model.Partner.Trim();
                pluginSetInfo.Key = model.Key.Trim();
                pluginSetInfo.Seller = model.Seller.Trim();
                pluginSetInfo.PayFee = model.PayFee;
                pluginSetInfo.FreeMoney = model.FreeMoney;
                PluginUtils.SavePluginSet(pluginSetInfo);

                AddAdminOperateLog("修改支付宝插件配置信息");
                return PromptView(Url.Action("config", "plugin", new { configController = "AdminAlipay", configAction = "Config" }), "插件配置修改成功");
            }
            return PromptView(Url.Action("config", "plugin", new { configController = "AdminAlipay", configAction = "Config" }), "信息有误，请重新填写");
        }
    }
}
