using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;

namespace BrnShop.Web.Admin.Models
{
    /// <summary>
    /// banner模型类
    /// </summary>
    public class BannerModel
    {
        public BannerModel()
        {
            StartTime = EndTime = DateTime.Now;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public int BannerType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required(ErrorMessage = "开始时间不能为空")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required(ErrorMessage = "结束时间不能为空")]
        [DateTimeNotLess("StartTime", "开始时间")]
        [DisplayName("结束时间")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public int IsShow { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(50, ErrorMessage = "标题长度不能大于50")]
        public string BannerTitle { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Required(ErrorMessage = "地址不能为空")]
        [StringLength(125, ErrorMessage = "地址长度不能大于125")]
        [Url]
        public string Url { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [Required(ErrorMessage = "图片不能为空")]
        [StringLength(125, ErrorMessage = "图片长度不能大于125")]
        public string Img { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required(ErrorMessage = "排序不能为空")]
        [DisplayName("排序")]
        public int DisplayOrder { get; set; }
    }

    /// <summary>
    /// banner列表模型类
    /// </summary>
    public class BannerListModel
    {
        public PageModel PageModel { get; set; }
        public List<BannerInfo> BannerList { get; set; }
    }
}
