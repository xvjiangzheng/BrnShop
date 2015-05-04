using System;
using System.Data;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;

namespace BrnShop.Web.Admin.Models
{
    /// <summary>
    /// 活动专题列表模型类
    /// </summary>
    public class TopicListModel
    {
        public PageModel PageModel { get; set; }
        public DataTable TopicList { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public string TopicSN { get; set; }
        public string TopicTitle { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }

    /// <summary>
    /// 活动专题模型类
    /// </summary>
    public class TopicModel
    {
        public TopicModel()
        {
            StartTime = EndTime = DateTime.Now;
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required(ErrorMessage = "请选择开始时间")]
        [DisplayName("开始时间")]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Required(ErrorMessage = "请选择结束时间")]
        [DateTimeNotLess("StartTime", "开始时间")]
        [DisplayName("结束时间")]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Required(ErrorMessage = "请填写标题")]
        [StringLength(50, ErrorMessage = "最多输入50个字")]
        public string Title { get; set; }
        /// <summary>
        /// 头部html
        /// </summary>
        [AllowHtml]
        public string HeadHtml { get; set; }
        /// <summary>
        /// 主体html
        /// </summary>
        [AllowHtml]
        public string BodyHtml { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public int IsShow { get; set; }
    }
}
