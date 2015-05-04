using System;
using System.Data;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using BrnShop.Core;
using BrnShop.Web.Framework;

namespace BrnShop.Web.Admin.Models
{
    /// <summary>
    /// 禁止IP列表模型类
    /// </summary>
    public class BannedIPListModel
    {
        public PageModel PageModel { get; set; }
        public DataTable BannedIPList { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public string IP { get; set; }
    }

    /// <summary>
    /// 禁止IP模型类
    /// </summary>
    public class BannedIPModel : IValidatableObject
    {
        public BannedIPModel()
        {
            LiftBanTime = DateTime.Now;
        }

        /// <summary>
        /// IP1
        /// </summary>
        [Required(ErrorMessage = "*")]
        public string IP1 { get; set; }

        /// <summary>
        /// IP2
        /// </summary>
        [Required(ErrorMessage = "*")]
        public string IP2 { get; set; }

        /// <summary>
        /// IP3
        /// </summary>
        [Required(ErrorMessage = "*")]
        public string IP3 { get; set; }

        /// <summary>
        /// IP4
        /// </summary>
        public string IP4 { get; set; }

        /// <summary>
        /// 解禁时间
        /// </summary>
        [Required(ErrorMessage = "解禁时间不能为空")]
        public DateTime LiftBanTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errorList = new List<ValidationResult>();

            if (LiftBanTime <= DateTime.Now)
                errorList.Add(new ValidationResult("解禁时间必须大于当前时间", new string[] { "LiftBanTime" }));

            if (string.IsNullOrWhiteSpace(IP4))
            {
                if (!ValidateHelper.IsIP(string.Format("{0}.{1}.{2}.10", IP1, IP2, IP3)))
                    errorList.Add(new ValidationResult("请输入正确的IP格式", new string[] { "IP4" }));
            }
            else
            {
                if (!ValidateHelper.IsIP(string.Format("{0}.{1}.{2}.{3}", IP1, IP2, IP3, IP4)))
                    errorList.Add(new ValidationResult("请输入正确的IP格式", new string[] { "IP4" }));
            }

            return errorList;
        }
    }
}
