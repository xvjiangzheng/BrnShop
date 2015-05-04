using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 插件类型枚举
    /// </summary>
    public enum PluginType
    {
        /// <summary>
        /// 开放授权插件
        /// </summary>
        OAuthPlugin=0,
        /// <summary>
        /// 支付插件
        /// </summary>
        PayPlugin=1,
        /// <summary>
        /// 配送插件
        /// </summary>
        ShipPlugin=2
    }
}
