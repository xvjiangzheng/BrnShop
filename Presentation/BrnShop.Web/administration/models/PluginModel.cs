using System;
using System.Web.Routing;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using BrnShop.Core;
using BrnShop.Services;

namespace BrnShop.Web.Admin.Models
{
    /// <summary>
    /// 插件列表模型类
    /// </summary>
    public class PluginListModel
    {
        /// <summary>
        /// 插件类型
        /// </summary>
        public PluginType PluginType { get; set; }
        /// <summary>
        /// 安装的插件列表
        /// </summary>
        public List<PluginInfo> InstalledPluginList { get; set; }
        /// <summary>
        /// 未安装的插件列表
        /// </summary>
        public List<PluginInfo> UninstalledPluginList { get; set; }
    }

    /// <summary>
    /// 插件模型类
    /// </summary>
    public class PluginModel
    {
        /// <summary>
        /// 友好名称
        /// </summary>
        [Required(ErrorMessage = "友好名称不能为空")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        [Required(ErrorMessage = "排序不能为空")]
        [DisplayName("排序")]
        public int DisplayOrder { get; set; }
    }

    /// <summary>
    /// 插件配置模型类
    /// </summary>
    public class ConfigModel
    {
        /// <summary>
        /// 插件配置控制器
        /// </summary>
        public string ConfigController { get; set; }
        /// <summary>
        /// 插件配置动作方法
        /// </summary>
        public string ConfigAction { get; set; }
        /// <summary>
        /// 插件配置路由数据
        /// </summary>
        public RouteValueDictionary ConfigRouteValues { get; set; }
    }
}
