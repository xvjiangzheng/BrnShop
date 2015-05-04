using System;

using BrnShop.Core;

namespace BrnShop.OAuthPlugin.QQ
{
    /// <summary>
    /// 插件服务类
    /// </summary>
    public class PluginService : IOAuthPlugin
    {
        /// <summary>
        /// 插件配置控制器
        /// </summary>
        public string ConfigController
        {
            get { return "adminqqoauth"; }
        }
        /// <summary>
        /// 插件配置动作方法
        /// </summary>
        public string ConfigAction
        {
            get { return "config"; }
        }

        /// <summary>
        /// 登陆控制器
        /// </summary>
        public string LoginController
        {
            get { return "qqoauth"; }
        }
        /// <summary>
        /// 登陆动作方法
        /// </summary>
        public string LoginAction
        {
            get { return "login"; }
        }
    }
}
