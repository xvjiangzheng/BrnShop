using System;

namespace BrnShop.Core
{
    /// <summary>
    /// BrnShop开放授权插件接口
    /// </summary>
    public partial interface IOAuthPlugin : IPlugin
    {
        /// <summary>
        /// 登陆控制器
        /// </summary>
        string LoginController { get; }

        /// <summary>
        /// 登陆动作方法
        /// </summary>
        string LoginAction { get; }
    }
}
