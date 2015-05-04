using System;
using System.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;

namespace BrnShop.Web.Admin.Models
{
    /// <summary>
    /// 品牌列表模型类
    /// </summary>
    public class BrandListModel
    {
        public PageModel PageModel { get; set; }
        public DataTable BrandList { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }
    }

    /// <summary>
    /// 品牌模型类
    /// </summary>
    public class BrandModel
    {
        /// <summary>
        /// 品牌名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(10, ErrorMessage = "名称长度不能大于10")]
        public string BrandName { get; set; }
        /// <summary>
        /// 品牌logo
        /// </summary>
        [StringLength(50, ErrorMessage = "Logo文件名长度不能大于50")]
        [Required(ErrorMessage = "图片不能为空")]
        public string Logo { get; set; }
        /// <summary>
        /// 品牌排序
        /// </summary>
        [DisplayName("排序")]
        public int DisplayOrder { get; set; }
    }
}
