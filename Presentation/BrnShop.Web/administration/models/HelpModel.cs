using System;
using System.Web.Mvc;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using BrnShop.Core;
using BrnShop.Services;

namespace BrnShop.Web.Admin.Models
{
    /// <summary>
    /// 商城帮助分类模型类
    /// </summary>
    public class HelpCategoryModel
    {
        /// <summary>
        /// 帮助分类
        /// </summary>
        [Required(ErrorMessage = "分类不能为空")]
        [StringLength(30, ErrorMessage = "分类长度不能大于30")]
        public string HelpCategoryTitle { get; set; }

        /// <summary>
        /// 帮助排序
        /// </summary>
        [Required(ErrorMessage = "排序不能为空")]
        [DisplayName("排序")]
        public int DisplayOrder { get; set; }
    }

    /// <summary>
    /// 商城帮助列表模型类
    /// </summary>
    public class HelpListModel
    {
        public List<HelpInfo> HelpList { get; set; }
    }

    /// <summary>
    /// 商城帮助模型类
    /// </summary>
    public class HelpModel
    {
        /// <summary>
        /// 帮助分类id
        /// </summary>
        [Range(1,int.MaxValue, ErrorMessage = "请选择分类")]
        [DisplayName("分类")]
        public int Pid { get; set; }

        /// <summary>
        /// 帮助标题
        /// </summary>
        [Required(ErrorMessage = "标题不能为空")]
        [StringLength(30, ErrorMessage = "标题长度不能大于30")]
        public string HelpTitle { get; set; }

        /// <summary>
        /// 帮助网址
        /// </summary>
        [StringLength(100, ErrorMessage = "网址长度不能大于100")]
        public string Url { get; set; }

        /// <summary>
        /// 帮助内容
        /// </summary>
        [AllowHtml]
        public string Description { get; set; }

        /// <summary>
        /// 帮助排序
        /// </summary>
        [Required(ErrorMessage = "排序不能为空")]
        [DisplayName("排序")]
        public int DisplayOrder { get; set; }
    }
}
