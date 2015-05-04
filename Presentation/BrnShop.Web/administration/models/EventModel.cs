using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using BrnShop.Core;

namespace BrnShop.Web.Admin.Models
{
    /// <summary>
    /// 事件列表模型类
    /// </summary>
    public class EventListModel
    {
        /// <summary>
        /// BrnShop事件状态
        /// </summary>
        public int BSPEventState { get; set; }

        /// <summary>
        /// BrnShop事件列表
        /// </summary>
        public List<EventInfo> BSPEventList { get; set; }

        /// <summary>
        /// BrnShop事件执行间隔
        /// </summary>
        public int BSPEventPeriod { get; set; }
    }

    /// <summary>
    /// 事件模型类
    /// </summary>
    public class EventModel : IValidatableObject
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        [Required(ErrorMessage = "键不能为空")]
        public string Key { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Required(ErrorMessage = "标题不能为空")]
        public string Title { get; set; }
        /// <summary>
        /// 时间类型(0代表特定时间，1代表时间间隔)
        /// </summary>
        public int TimeType { get; set; }
        /// 时间(单位为分钟)
        /// </summary>
        [Required(ErrorMessage = "时间不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "时间必须大于0")]
        [DisplayName("时间")]
        public int TimeValue { get; set; }
        /// <summary>
        /// 类完全限定名
        /// </summary>
        [Required(ErrorMessage = "类完全限定名不能为空")]
        public string ClassName { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 是否启动
        /// </summary>
        public int Enabled { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errorList = new List<ValidationResult>();

            if (TimeType == 0 && TimeValue >= 60 * 24)
                errorList.Add(new ValidationResult("时间值必须小于" + 60 * 24 + "!", new string[] { "TimeValue" }));

            return errorList;
        }
    }
}
