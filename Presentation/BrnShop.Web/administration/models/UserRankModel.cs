using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;

namespace BrnShop.Web.Admin.Models
{
    /// <summary>
    /// 会员等级列表模型类
    /// </summary>
    public class UserRankListModel
    {
        /// <summary>
        /// 会员等级列表
        /// </summary>
        public List<UserRankInfo> UserRankList { get; set; }
    }

    /// <summary>
    /// 会员等级模型类
    /// </summary>
    public class UserRankModel : IValidatableObject
    {
        /// <summary>
        /// 会员等级标题
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(25, ErrorMessage = "名称长度不能大于25")]
        public string UserRankTitle { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(50, ErrorMessage = "头像文件名长度不能大于50")]
        [Required(ErrorMessage = "头像不能为空")]
        public string Avatar { get; set; }

        /// <summary>
        /// 积分上限
        /// </summary>
        [Range(-1, int.MaxValue, ErrorMessage = "积分上限不能为负数")]
        [DisplayName("积分上限")]
        public int CreditsUpper { get; set; }

        /// <summary>
        /// 积分下限
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "积分下限不能为负数")]
        [DisplayName("积分下限")]
        public int CreditsLower { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errorList = new List<ValidationResult>();

            if (CreditsUpper > 0 && CreditsUpper <= CreditsLower)
                errorList.Add(new ValidationResult("积分上限必须大于积分下限!", new string[] { "CreditsUpper" }));

            return errorList;
        }

    }
}
