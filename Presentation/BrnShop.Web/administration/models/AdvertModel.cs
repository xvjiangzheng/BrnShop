using System;
using System.Data;
using System.Web.Mvc;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;

namespace BrnShop.Web.Admin.Models
{
    /// <summary>
    /// 广告位置列表模型类
    /// </summary>
    public class AdvertPositionListModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 广告位置列表
        /// </summary>
        public List<AdvertPositionInfo> AdvertPositionList { get; set; }
    }

    /// <summary>
    /// 广告位置模型类
    /// </summary>
    public class AdvertPositionModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required(ErrorMessage = "标题不能为空")]
        [StringLength(25, ErrorMessage = "标题长度不能大于25")]
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(75, ErrorMessage = "描述长度不能大于75")]
        public string Description { get; set; }
    }

    /// <summary>
    /// 广告列表模型类
    /// </summary>
    public class AdvertListModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 广告位置id
        /// </summary>
        public int AdPosId { get; set; }
        /// <summary>
        /// 广告列表
        /// </summary>
        public DataTable AdvertList { get; set; }
    }

    /// <summary>
    /// 广告模型类
    /// </summary>
    public class AdvertModel : IValidatableObject
    {
        public AdvertModel()
        {
            StartTime = EndTime = DateTime.Now;
        }
        /// <summary>
        /// 广告位置id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "请选择广告位置")]
        public int AdPosId { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Required(ErrorMessage = "标题不能为空")]
        [StringLength(25, ErrorMessage = "标题长度不能大于25")]
        public string Title { get; set; }
        /// <summary>
        /// 主体
        /// </summary>
        [AllowHtml]
        [Required(ErrorMessage = "主体不能为空")]
        [StringLength(400, ErrorMessage = "主体长度不能大于400")]
        public string Body { get; set; }
        /// <summary>
        /// 网址
        /// </summary>
        [StringLength(100, ErrorMessage = "网址长度不能大于100")]
        [Url]
        public string Url { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Required(ErrorMessage = "开始时间不能为空")]
        [DisplayName("开始时间")]
        public DateTime StartTime { get; set; }
        /// 结束时间
        /// </summary>
        [Required(ErrorMessage = "结束时间不能为空")]
        [DateTimeNotLess("StartTime", "开始时间")]
        [DisplayName("结束时间")]
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 扩展字段1
        /// </summary>
        [StringLength(250, ErrorMessage = "内容长度不能大于250")]
        public string ExtField1 { get; set; }
        /// <summary>
        /// 扩展字段2
        /// </summary>
        [StringLength(250, ErrorMessage = "内容长度不能大于250")]
        public string ExtField2 { get; set; }
        /// <summary>
        /// 扩展字段3
        /// </summary>
        [StringLength(250, ErrorMessage = "内容长度不能大于250")]
        public string ExtField3 { get; set; }
        /// <summary>
        /// 扩展字段4
        /// </summary>
        [StringLength(250, ErrorMessage = "内容长度不能大于250")]
        public string ExtField4 { get; set; }
        /// <summary>
        /// 扩展字段5
        /// </summary>
        [StringLength(250, ErrorMessage = "内容长度不能大于250")]
        public string ExtField5 { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errorList = new List<ValidationResult>();

            if (Type != 3 && string.IsNullOrWhiteSpace(Url))
                errorList.Add(new ValidationResult("请输入网址", new string[] { "Url" }));

            return errorList;
        }
    }
}
