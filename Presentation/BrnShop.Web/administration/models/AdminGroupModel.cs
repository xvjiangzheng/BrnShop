using System;
using System.ComponentModel.DataAnnotations;

using BrnShop.Core;
using BrnShop.Services;

namespace BrnShop.Web.Admin.Models
{
    /// <summary>
    /// 管理员组列表模型类
    /// </summary>
    public class AdminGroupListModel
    {
        /// <summary>
        /// 管理员组列表
        /// </summary>
        public AdminGroupInfo[] AdminGroupList { get; set; }
    }

    /// <summary>
    /// 管理员组模型类
    /// </summary>
    public class AdminGroupModel
    {
        /// <summary>
        /// 管理员组标题
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(25, ErrorMessage = "名称长度不能大于25")]
        public string AdminGroupTitle { get; set; }

        /// <summary>
        /// 动作列表
        /// </summary>
        public string[] ActionList { get; set; }
    }
}
