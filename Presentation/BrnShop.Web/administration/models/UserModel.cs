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
    /// 用户列表模型类
    /// </summary>
    public class UserListModel
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        public DataTable UserList { get; set; }
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 用户手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 用户等级id
        /// </summary>
        public int UserRid { get; set; }
        /// <summary>
        /// 管理员组id
        /// </summary>
        public int AdminGid { get; set; }
    }

    /// <summary>
    /// 用户模型类
    /// </summary>
    public class UserModel : IValidatableObject
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(10, ErrorMessage = "名称长度不能大于10")]
        public string UserName { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Email]
        public string Email { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        [Mobile]
        public string Mobile { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(32, MinimumLength = 4, ErrorMessage = "密码长度必须大于3且小于33")]
        public string Password { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        [Compare("Password", ErrorMessage = "密码必须相同")]
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// 用户等级id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "请选择正确的用户等级")]
        [DisplayName("用户等级")]
        public int UserRid { get; set; }
        /// <summary>
        /// 管理员组id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "请选择正确的管理员组")]
        [DisplayName("管理员组")]
        public int AdminGid { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(10, ErrorMessage = "名称长度不能大于10")]
        public string NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 支付积分
        /// </summary>
        [Required(ErrorMessage = "支付积分不能为空")]
        [Range(0, int.MaxValue, ErrorMessage = "支付积分不能为负数")]
        [DisplayName("支付积分")]
        public int PayCredits { get; set; }
        /// <summary>
        /// 性别(0代表未知，1代表男，2代表女)
        /// </summary>
        [Range(0, 2, ErrorMessage = "请选择确切的性别")]
        [DisplayName("性别")]
        public int Gender { get; set; }
        /// <summary>
        /// 真实名称
        /// </summary>
        [StringLength(5, ErrorMessage = "名称长度不能大于5")]
        public string RealName { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        [DisplayName("出生日期")]
        public DateTime? Bday { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [IdCard]
        public string IdCard { get; set; }
        /// <summary>
        /// 所在地区域
        /// </summary>
        [DisplayName("区域")]
        public int RegionId { get; set; }
        /// <summary>
        /// 所在地详细机制
        /// </summary>
        [StringLength(75, ErrorMessage = "密码长度不能大于75")]
        public string Address { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [StringLength(150, ErrorMessage = "密码长度不能大于125")]
        public string Bio { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errorList = new List<ValidationResult>();

            if (!SecureHelper.IsSafeSqlString(UserName))
            {
                errorList.Add(new ValidationResult("用户名中包含不安全的字符,请删除!", new string[] { "UserName" }));
            }

            return errorList;
        }
    }

}
