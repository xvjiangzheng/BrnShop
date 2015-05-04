using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;

namespace BrnShop.Web.Admin.Models
{
    /// <summary>
    /// 友情链接模型类
    /// </summary>
    public class FriendLinkModel
    {
        /// <summary>
        /// 友情链接名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(25, ErrorMessage = "名称长度不能大于25")]
        public string FriendLinkName { get; set; }

        /// <summary>
        /// 友情链接提示
        /// </summary>
        [StringLength(50, ErrorMessage = "提示长度不能大于50")]
        public string FriendLinkTitle { get; set; }

        /// <summary>
        /// 友情链接地址
        /// </summary>
        [Required(ErrorMessage = "地址不能为空")]
        [StringLength(125, ErrorMessage = "地址长度不能大于125")]
        [Url]
        public string FriendLinkUrl { get; set; }

        /// <summary>
        /// 友情链接logo
        /// </summary>
        [StringLength(125, ErrorMessage = "logo长度不能大于125")]
        public string FriendLinkLogo { get; set; }

        /// <summary>
        /// 友情链接打开目标
        /// </summary>
        [Range(0, 1, ErrorMessage = "请选择正确的目标类型")]
        [DisplayName("目标")]
        public int Target { get; set; }

        /// <summary>
        /// 友情链接排序
        /// </summary>
        [Required(ErrorMessage = "排序不能为空")]
        [DisplayName("排序")]
        public int DisplayOrder { get; set; }
    }

    /// <summary>
    /// 友情链接列表模型类
    /// </summary>
    public class FriendLinkListModel
    {
        public FriendLinkInfo[] FriendLinkList { get; set; }
    }
}
