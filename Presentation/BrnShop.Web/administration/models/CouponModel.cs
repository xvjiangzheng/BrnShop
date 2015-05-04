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
    /// 优惠劵类型列表模型类
    /// </summary>
    public class CouponTypeListModel
    {
        /// <summary>
        /// 分页模型
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 优惠劵类型列表
        /// </summary>
        public DataTable CouponTypeList { get; set; }
        public int Type { get; set; }
        /// <summary>
        /// 优惠劵类型名称
        /// </summary>
        public string CouponTypeName { get; set; }
    }

    /// <summary>
    /// 优惠劵类型模型类
    /// </summary>
    public class CouponTypeModel : IValidatableObject
    {
        public CouponTypeModel()
        {
            SendStartTime = DateTime.Now;
            SendEndTime = DateTime.Now;
            UseStartTime = DateTime.Now;
            UseEndTime = DateTime.Now;
            LimitCateName = "未限制分类";
            LimitBrandName = "未限制品牌";
        }

        /// <summary>
        /// 优惠劵类型名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(25, ErrorMessage = "名称长度不能大于25")]
        public string CouponTypeName { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [Required(ErrorMessage = "金额不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "金额必须大于0")]
        [DisplayName("金额")]
        public int Money { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Required(ErrorMessage = "数量不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "数量必须大于0")]
        [DisplayName("数量")]
        public int Count { get; set; }
        /// <summary>
        /// 发放方式
        /// </summary>
        public int SendModel { get; set; }
        /// <summary>
        /// 获得方式
        /// </summary>
        public int GetModel { get; set; }
        /// <summary>
        /// 使用方式
        /// </summary>
        public int UseModel { get; set; }
        /// <summary>
        /// 最小用户等级
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "请选择正确的用户等级")]
        [DisplayName("最低用户等级")]
        public int UserRankLower { get; set; }
        /// <summary>
        /// 最小订单金额为空
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "最小订单金额不能为负数")]
        [DisplayName("最小订单金额")]
        public int OrderAmountLower { get; set; }
        /// <summary>
        /// 限制分类id
        /// </summary>
        public int LimitCateId { get; set; }
        /// <summary>
        /// 限制分类名称
        /// </summary>
        public string LimitCateName { get; set; }
        /// <summary>
        /// 限制品牌id
        /// </summary>
        public int LimitBrandId { get; set; }
        /// <summary>
        /// 限制品牌名称
        /// </summary>
        public string LimitBrandName { get; set; }
        /// <summary>
        /// 限制商品
        /// </summary>
        public int LimitProduct { get; set; }
        /// <summary>
        /// 发放开始时间
        /// </summary>
        [DisplayName("发放开始时间")]
        public DateTime? SendStartTime { get; set; }
        /// <summary>
        /// 发放结束时间
        /// </summary>
        [DisplayName("发放结束时间")]
        public DateTime? SendEndTime { get; set; }
        /// <summary>
        /// 使用时间类型
        /// </summary>
        public int UseTimeType { get; set; }
        /// <summary>
        /// 使用过期时间
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "使用过期时间不能为负数")]
        [DisplayName("使用过期时间")]
        public int UseExpireTime { get; set; }
        /// <summary>
        /// 使用开始时间
        /// </summary>
        [DisplayName("使用开始时间")]
        public DateTime? UseStartTime { get; set; }
        /// <summary>
        /// 使用结束时间
        /// </summary>
        [DisplayName("使用结束时间")]
        public DateTime? UseEndTime { get; set; }

        public int State { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errorList = new List<ValidationResult>();

            //验证发放时间
            if (SendModel == 0)
            {
                if (SendStartTime == null || SendEndTime == null)
                {
                    errorList.Add(new ValidationResult("发放时间不能为空!", new string[] { "SendEndTime" }));
                    return errorList;
                }
                if (SendEndTime <= SendStartTime)
                {
                    errorList.Add(new ValidationResult("发放结束时间必须大于发放开始时间!", new string[] { "SendEndTime" }));
                    return errorList;
                }
            }

            //验证使用时间
            if (UseTimeType == 0)
            {
                if (UseStartTime == null || UseEndTime == null)
                {
                    errorList.Add(new ValidationResult("使用时间不能为空!", new string[] { "UseEndTime" }));
                    return errorList;
                }
                else if (UseEndTime <= UseStartTime)
                {
                    errorList.Add(new ValidationResult("使用结束时间必须大于使用开始时间!", new string[] { "UseEndTime" }));
                    return errorList;
                }
            }

            if (SendModel == 0 && UseTimeType == 0)
            {
                if (UseStartTime < SendStartTime)
                    errorList.Add(new ValidationResult("使用开始时间必须小于发放开始时间!", new string[] { "UseStartTime" }));
                else if (UseEndTime < SendEndTime)
                    errorList.Add(new ValidationResult("使用结束时间必须小于发放结束时间!", new string[] { "UseEndTime" }));
            }

            return errorList;
        }
    }

    /// <summary>
    /// 优惠劵商品列表模型类
    /// </summary>
    public class CouponProductListModel
    {
        /// <summary>
        /// 优惠劵商品列表
        /// </summary>
        public DataTable CouponProductList { get; set; }
        public PageModel PageModel { get; set; }
        public int CouponTypeId { get; set; }
    }

    /// <summary>
    /// 优惠劵列表模型类
    /// </summary>
    public class CouponListModel
    {
        /// <summary>
        /// 分页模型
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 优惠劵列表
        /// </summary>
        public DataTable CouponList { get; set; }
        /// <summary>
        /// 优惠劵编号
        /// </summary>
        public string SN { get; set; }
        /// <summary>
        /// 账户名称
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 优惠劵类型id
        /// </summary>
        public int CouponTypeId { get; set; }
    }

    /// <summary>
    /// 优惠劵发放模型类
    /// </summary>
    public class SendCouponModel : IValidatableObject
    {
        [Required(ErrorMessage = "数量不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "数量必须大于0")]
        [DisplayName("数量")]
        public int Count { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int UType { get; set; }

        /// <summary>
        /// 用户值
        /// </summary>
        public string UValue { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errorList = new List<ValidationResult>();

            if (UType == 0)
            {
                if (Count > 10)
                    errorList.Add(new ValidationResult("最多只能发放10张优惠劵!", new string[] { "Count" }));

                if (string.IsNullOrWhiteSpace(UValue))
                {
                    errorList.Add(new ValidationResult("请输入用户id!", new string[] { "UValue" }));
                }
                else
                {
                    PartUserInfo partUserInfo = Users.GetPartUserById(TypeHelper.StringToInt(UValue));
                    if (partUserInfo == null)
                        errorList.Add(new ValidationResult("请输入正确的用户id!", new string[] { "UValue" }));
                }
            }
            else if (UType == 1)
            {
                if (Count > 10)
                    errorList.Add(new ValidationResult("最多只能发放10张优惠劵!", new string[] { "Count" }));

                if (string.IsNullOrWhiteSpace(UValue))
                {
                    errorList.Add(new ValidationResult("请输入账户名!", new string[] { "UValue" }));
                }
                else
                {
                    if (AdminUsers.GetUidByAccountName(UValue) < 1)
                        errorList.Add(new ValidationResult("账户不存在!", new string[] { "UValue" }));
                }
            }

            return errorList;
        }
    }
}
