using System;

namespace BrnShop.Core
{
    /// <summary>
    /// BrnShop基础插件接口
    /// </summary>
    public partial interface IPlugin
    {
        /// <summary>
        /// 插件配置控制器
        /// </summary>
        string ConfigController { get; }

        /// <summary>
        /// 插件配置动作方法
        /// </summary>
        string ConfigAction { get; }
    }
}
