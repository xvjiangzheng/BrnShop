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
    /// 单品促销活动列表模型类
    /// </summary>
    public class SinglePromotionListModel
    {
        public PageModel PageModel { get; set; }
        public DataTable SinglePromotionList { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public int Pid { get; set; }
        public string ProductName { get; set; }
        public string PromotionName { get; set; }
        public string PromotionTime { get; set; }
    }

    /// <summary>
    /// 单品促销活动模型类
    /// </summary>
    public class SinglePromotionModel : IValidatableObject
    {
        public SinglePromotionModel()
        {
            Pid = 0;
            ProductName = "请选择商品";
            StartTime1 = DateTime.Now;
            EndTime1 = DateTime.Now;
            DiscountType = 0;
            DiscountValue = 5;
            State = 1;
        }

        /// <summary>
        /// 商品id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "请选择商品")]
        [DisplayName("商品")]
        public int Pid { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 促销活动名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(25, ErrorMessage = "名称长度不能大于25")]
        public string PromotionName { get; set; }

        /// <summary>
        /// 广告语
        /// </summary>
        [StringLength(30, ErrorMessage = "名称长度不能大于30")]
        public string Slogan { get; set; }

        /// <summary>
        /// 促销开始时间1
        /// </summary>
        [Required(ErrorMessage = "开始时间不能为空")]
        [DisplayName("开始时间")]
        public DateTime StartTime1 { get; set; }

        /// <summary>
        /// 促销结束时间1
        /// </summary>
        [Required(ErrorMessage = "结束时间不能为空")]
        [DisplayName("结束时间")]
        public DateTime EndTime1 { get; set; }

        /// <summary>
        /// 促销开始时间2
        /// </summary>
        [DisplayName("开始时间")]
        public DateTime? StartTime2 { get; set; }

        /// <summary>
        /// 促销结束时间2
        /// </summary>
        [DisplayName("结束时间")]
        public DateTime? EndTime2 { get; set; }

        /// <summary>
        /// 促销开始时间3
        /// </summary>
        [DisplayName("开始时间")]
        public DateTime? StartTime3 { get; set; }

        /// <summary>
        /// 促销结束时间3
        /// </summary>
        [DisplayName("结束时间")]
        public DateTime? EndTime3 { get; set; }

        /// <summary>
        /// 用户等级下限
        /// </summary>
        public int UserRankLower { get; set; }

        /// <summary>
        /// 促销活动状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 促销劵类型id
        /// </summary>
        public int CouponTypeId { get; set; }

        /// <summary>
        /// 赠送积分
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "赠送积分不能小于0")]
        [DisplayName("赠送积分")]
        public int PayCredits { get; set; }

        /// <summary>
        /// 折扣类型
        /// </summary>
        [Required(ErrorMessage = "折扣类型不能为空")]
        [Range(0, 2, ErrorMessage = "请选择折扣类型")]
        [DisplayName("折扣类型")]
        public int DiscountType { get; set; }

        /// <summary>
        /// 折扣值
        /// </summary>
        [Required(ErrorMessage = "折扣值不能为空")]
        [Range(0, int.MaxValue, ErrorMessage = "折扣值不能小于0")]
        [DisplayName("折扣值")]
        public int DiscountValue { get; set; }

        /// <summary>
        /// 是否限购
        /// </summary>
        public int IsStock { get; set; }

        /// <summary>
        /// 限购库存
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "限购库存不能小于0")]
        [DisplayName("限购库存")]
        public int Stock { get; set; }

        /// <summary>
        /// 配额下限
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "配额下限不能小于0")]
        [DisplayName("配额下限")]
        public int QuotaLower { get; set; }

        /// <summary>
        /// 配额上限
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "配额上限不能小于0")]
        [DisplayName("配额上限")]
        public int QuotaUpper { get; set; }

        /// <summary>
        /// 允许的最大购买数量
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "允许的最大购买数量不能小于0")]
        [DisplayName("允许的最大购买数量")]
        public int AllowBuyCount { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errorList = new List<ValidationResult>();

            if (DiscountType == 0 && (DiscountValue <= 0 || DiscountValue >= 10))
                errorList.Add(new ValidationResult("折扣必选大于0且小于10!", new string[] { "DiscountValue" }));

            if ((QuotaLower != 0 || QuotaUpper != 0) && QuotaUpper < QuotaLower)
                errorList.Add(new ValidationResult("配额上限必须大于配额下限!", new string[] { "QuotaUpper" }));

            if (EndTime1 <= StartTime1)
                errorList.Add(new ValidationResult("结束时间必须大于开始时间!", new string[] { "EndTime1" }));

            if (StartTime2 != null && EndTime2 == null)
            {
                errorList.Add(new ValidationResult("结束时间不能为空!", new string[] { "EndTime2" }));
            }
            else if (EndTime2 != null && StartTime2 == null)
            {
                errorList.Add(new ValidationResult("开始时间不能为空!", new string[] { "StartTime2" }));
            }
            else if (StartTime2 != null && EndTime2 != null)
            {
                if (EndTime2 <= StartTime2)
                    errorList.Add(new ValidationResult("结束时间必须大于开始时间!", new string[] { "EndTime2" }));
                if (StartTime2 <= EndTime1)
                    errorList.Add(new ValidationResult("此阶段开始时间必须大于上一阶段结束时间!", new string[] { "StartTime2" }));
            }

            if (StartTime3 != null && EndTime3 == null)
            {
                errorList.Add(new ValidationResult("结束时间不能为空!", new string[] { "EndTime3" }));
            }
            else if (EndTime3 != null && StartTime3 == null)
            {
                errorList.Add(new ValidationResult("开始时间不能为空!", new string[] { "StartTime3" }));
            }
            else if (StartTime3 != null && EndTime3 != null)
            {
                if (EndTime3 <= StartTime3)
                    errorList.Add(new ValidationResult("结束时间必须大于开始时间!", new string[] { "EndTime3" }));
                if (StartTime3 <= EndTime2)
                    errorList.Add(new ValidationResult("此阶段开始时间必须大于上一阶段结束时间!", new string[] { "StartTime3" }));
            }

            return errorList;
        }
    }

    /// <summary>
    /// 买送促销活动列表模型类
    /// </summary>
    public class BuySendPromotionListModel
    {
        public PageModel PageModel { get; set; }
        public DataTable BuySendPromotionList { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public string PromotionName { get; set; }
        public string PromotionTime { get; set; }
    }

    /// <summary>
    /// 买送促销活动模型类
    /// </summary>
    public class BuySendPromotionModel : IValidatableObject
    {
        public BuySendPromotionModel()
        {
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            State = 1;
        }

        /// <summary>
        /// 活动名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(25, ErrorMessage = "名称长度不能大于25")]
        public string PromotionName { get; set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        [Required(ErrorMessage = "开始时间不能为空")]
        [DisplayName("开始时间")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        [Required(ErrorMessage = "结束时间不能为空")]
        [DisplayName("结束时间")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 用户等级下限
        /// </summary>
        public int UserRankLower { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 活动类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 购买数量
        /// </summary>
        [Required(ErrorMessage = "购买数量不能为空")]
        [Range(0, int.MaxValue, ErrorMessage = "购买数量不能小于0")]
        [DisplayName("购买数量")]
        public int BuyCount { get; set; }

        /// <summary>
        /// 赠送数量
        /// </summary>
        [Required(ErrorMessage = "赠送数量不能为空")]
        [Range(0, int.MaxValue, ErrorMessage = "赠送数量不能小于0")]
        [DisplayName("赠送数量")]
        public int SendCount { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errorList = new List<ValidationResult>();

            if (EndTime <= StartTime)
                errorList.Add(new ValidationResult("结束时间必须大于开始时间!", new string[] { "EndTime" }));

            return errorList;
        }

    }

    /// <summary>
    /// 买送商品列表模型类
    /// </summary>
    public class BuySendProductListModel
    {
        /// <summary>
        /// 买送商品列表
        /// </summary>
        public DataTable BuySendProductList { get; set; }
        public PageModel PageModel { get; set; }
        public int PmId { get; set; }
        public int Pid { get; set; }
        public string ProductName { get; set; }
    }

    /// <summary>
    /// 赠品促销活动列表模型类
    /// </summary>
    public class GiftPromotionListModel
    {
        public PageModel PageModel { get; set; }
        public DataTable GiftPromotionList { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public int Pid { get; set; }
        public string ProductName { get; set; }
        public string PromotionName { get; set; }
        public string PromotionTime { get; set; }
    }

    /// <summary>
    /// 赠品促销活动模型类
    /// </summary>
    public class GiftPromotionModel : IValidatableObject
    {
        public GiftPromotionModel()
        {
            Pid = 0;
            ProductName = "请选择商品";
            StartTime1 = DateTime.Now;
            EndTime1 = DateTime.Now;
            State = 1;
        }

        /// <summary>
        /// 商品id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "请选择商品")]
        [DisplayName("商品")]
        public int Pid { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 促销活动名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(25, ErrorMessage = "名称长度不能大于25")]
        public string PromotionName { get; set; }

        /// <summary>
        /// 促销开始时间1
        /// </summary>
        [Required(ErrorMessage = "开始时间不能为空")]
        [DisplayName("开始时间")]
        public DateTime StartTime1 { get; set; }

        /// <summary>
        /// 促销结束时间1
        /// </summary>
        [Required(ErrorMessage = "结束时间不能为空")]
        [DisplayName("结束时间")]
        public DateTime EndTime1 { get; set; }

        /// <summary>
        /// 促销开始时间2
        /// </summary>
        [DisplayName("开始时间")]
        public DateTime? StartTime2 { get; set; }

        /// <summary>
        /// 促销结束时间2
        /// </summary>
        [DisplayName("结束时间")]
        public DateTime? EndTime2 { get; set; }

        /// <summary>
        /// 促销开始时间3
        /// </summary>
        [DisplayName("开始时间")]
        public DateTime? StartTime3 { get; set; }

        /// <summary>
        /// 促销结束时间3
        /// </summary>
        [DisplayName("结束时间")]
        public DateTime? EndTime3 { get; set; }

        /// <summary>
        /// 用户等级下限
        /// </summary>
        public int UserRankLower { get; set; }

        /// <summary>
        /// 促销活动状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 配额上限
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "配额上限不能小于0")]
        [DisplayName("配额上限")]
        public int QuotaUpper { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errorList = new List<ValidationResult>();

            if (EndTime1 <= StartTime1)
                errorList.Add(new ValidationResult("结束时间必须大于开始时间!", new string[] { "EndTime1" }));

            if (StartTime2 != null && EndTime2 == null)
            {
                errorList.Add(new ValidationResult("结束时间不能为空!", new string[] { "EndTime2" }));
            }
            else if (EndTime2 != null && StartTime2 == null)
            {
                errorList.Add(new ValidationResult("开始时间不能为空!", new string[] { "StartTime2" }));
            }
            else if (StartTime2 != null && EndTime2 != null)
            {
                if (EndTime2 <= StartTime2)
                    errorList.Add(new ValidationResult("结束时间必须大于开始时间!", new string[] { "EndTime2" }));
                if (StartTime2 <= EndTime1)
                    errorList.Add(new ValidationResult("此阶段开始时间必须大于上一阶段结束时间!", new string[] { "StartTime2" }));
            }

            if (StartTime3 != null && EndTime3 == null)
            {
                errorList.Add(new ValidationResult("结束时间不能为空!", new string[] { "EndTime3" }));
            }
            else if (EndTime3 != null && StartTime3 == null)
            {
                errorList.Add(new ValidationResult("开始时间不能为空!", new string[] { "StartTime3" }));
            }
            else if (StartTime3 != null && EndTime3 != null)
            {
                if (EndTime3 <= StartTime3)
                    errorList.Add(new ValidationResult("结束时间必须大于开始时间!", new string[] { "EndTime3" }));
                if (StartTime3 <= EndTime2)
                    errorList.Add(new ValidationResult("此阶段开始时间必须大于上一阶段结束时间!", new string[] { "StartTime3" }));
            }

            return errorList;
        }
    }

    /// <summary>
    /// 赠品列表模型类
    /// </summary>
    public class GiftListModel
    {
        /// <summary>
        /// 扩展赠品列表
        /// </summary>
        public List<ExtGiftInfo> ExtGiftList { get; set; }
        /// <summary>
        /// 赠品促销活动id
        /// </summary>
        public int PmId { get; set; }
    }

    /// <summary>
    /// 套装促销活动列表模型类
    /// </summary>
    public class SuitPromotionListModel
    {
        /// <summary>
        /// 促销活动列表
        /// </summary>
        public DataTable SuitPromotionList { get; set; }
        public PageModel PageModel { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public int Pid { get; set; }
        public string ProductName { get; set; }
        public string PromotionName { get; set; }
        public string PromotionTime { get; set; }
    }

    /// <summary>
    /// 套装促销活动模型类
    /// </summary>
    public class SuitPromotionModel : IValidatableObject
    {
        public SuitPromotionModel()
        {
            StartTime1 = DateTime.Now;
            EndTime1 = DateTime.Now;
            State = 1;
        }

        /// <summary>
        /// 促销活动名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(25, ErrorMessage = "名称长度不能大于25")]
        public string PromotionName { get; set; }

        /// <summary>
        /// 促销开始时间1
        /// </summary>
        [Required(ErrorMessage = "开始时间不能为空")]
        [DisplayName("开始时间")]
        public DateTime StartTime1 { get; set; }

        /// <summary>
        /// 促销结束时间1
        /// </summary>
        [Required(ErrorMessage = "结束时间不能为空")]
        [DisplayName("结束时间")]
        public DateTime EndTime1 { get; set; }

        /// <summary>
        /// 促销开始时间2
        /// </summary>
        [DisplayName("开始时间")]
        public DateTime? StartTime2 { get; set; }

        /// <summary>
        /// 促销结束时间2
        /// </summary>
        [DisplayName("结束时间")]
        public DateTime? EndTime2 { get; set; }

        /// <summary>
        /// 促销开始时间3
        /// </summary>
        [DisplayName("开始时间")]
        public DateTime? StartTime3 { get; set; }

        /// <summary>
        /// 促销结束时间3
        /// </summary>
        [DisplayName("结束时间")]
        public DateTime? EndTime3 { get; set; }

        /// <summary>
        /// 用户等级下限
        /// </summary>
        public int UserRankLower { get; set; }

        /// <summary>
        /// 促销活动状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 配额上限
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "配额上限不能小于0")]
        [DisplayName("配额上限")]
        public int QuotaUpper { get; set; }

        /// <summary>
        /// 限购一次
        /// </summary>
        public int OnlyOnce { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errorList = new List<ValidationResult>();

            if (EndTime1 <= StartTime1)
                errorList.Add(new ValidationResult("结束时间必须大于开始时间!", new string[] { "EndTime1" }));

            if (StartTime2 != null && EndTime2 == null)
            {
                errorList.Add(new ValidationResult("结束时间不能为空!", new string[] { "EndTime2" }));
            }
            else if (EndTime2 != null && StartTime2 == null)
            {
                errorList.Add(new ValidationResult("开始时间不能为空!", new string[] { "StartTime2" }));
            }
            else if (StartTime2 != null && EndTime2 != null)
            {
                if (EndTime2 <= StartTime2)
                    errorList.Add(new ValidationResult("结束时间必须大于开始时间!", new string[] { "EndTime2" }));
                if (StartTime2 <= EndTime1)
                    errorList.Add(new ValidationResult("此阶段开始时间必须大于上一阶段结束时间!", new string[] { "StartTime2" }));
            }

            if (StartTime3 != null && EndTime3 == null)
            {
                errorList.Add(new ValidationResult("结束时间不能为空!", new string[] { "EndTime3" }));
            }
            else if (EndTime3 != null && StartTime3 == null)
            {
                errorList.Add(new ValidationResult("开始时间不能为空!", new string[] { "StartTime3" }));
            }
            else if (StartTime3 != null && EndTime3 != null)
            {
                if (EndTime3 <= StartTime3)
                    errorList.Add(new ValidationResult("结束时间必须大于开始时间!", new string[] { "EndTime3" }));
                if (StartTime3 <= EndTime2)
                    errorList.Add(new ValidationResult("此阶段开始时间必须大于上一阶段结束时间!", new string[] { "StartTime3" }));
            }


            return errorList;
        }
    }

    /// <summary>
    /// 套装商品列表模型类
    /// </summary>
    public class SuitProductListModel
    {
        /// <summary>
        /// 扩展套装商品列表
        /// </summary>
        public List<ExtSuitProductInfo> ExtSuitProductList { get; set; }
        /// <summary>
        /// 促销活动id
        /// </summary>
        public int PmId { get; set; }
    }

    /// <summary>
    /// 满赠促销活动列表模型类
    /// </summary>
    public class FullSendPromotionListModel
    {
        public DataTable FullSendPromotionList { get; set; }
        public PageModel PageModel { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public string PromotionName { get; set; }
        public string PromotionTime { get; set; }
    }

    /// <summary>
    /// 满赠促销活动模型类
    /// </summary>
    public class FullSendPromotionModel : IValidatableObject
    {
        public FullSendPromotionModel()
        {
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            State = 1;
        }

        /// <summary>
        /// 活动名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(25, ErrorMessage = "名称长度不能大于25")]
        public string PromotionName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required(ErrorMessage = "开始时间不能为空")]
        [DisplayName("开始时间")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required(ErrorMessage = "结束时间不能为空")]
        [DisplayName("结束时间")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 用户等级下限
        /// </summary>
        public int UserRankLower { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 活动额度
        /// </summary>
        [Required(ErrorMessage = "活动额度不能为空")]
        [Range(0, int.MaxValue, ErrorMessage = "活动额度不能小于0")]
        [DisplayName("活动额度")]
        public int LimitMoney { get; set; }

        /// <summary>
        /// 补充额度
        /// </summary>
        [Required(ErrorMessage = "补充额度不能为空")]
        [Range(0, int.MaxValue, ErrorMessage = "补充额度不能小于0")]
        [DisplayName("补充额度")]
        public int AddMoney { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errorList = new List<ValidationResult>();

            if (EndTime <= StartTime)
                errorList.Add(new ValidationResult("结束时间必须大于开始时间!", new string[] { "EndTime" }));

            return errorList;
        }

    }

    /// <summary>
    /// 满赠商品列表模型类
    /// </summary>
    public class FullSendProductListModel
    {
        /// <summary>
        /// 满赠商品列表
        /// </summary>
        public DataTable FullSendProductList { get; set; }
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 满赠促销活动id
        /// </summary>
        public int PmId { get; set; }
        public int Type { get; set; }
    }

    /// <summary>
    /// 满减促销活动列表模型类
    /// </summary>
    public class FullCutPromotionListModel
    {
        public DataTable FullCutPromotionList { get; set; }
        public PageModel PageModel { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public string PromotionName { get; set; }
        public string PromotionTime { get; set; }
    }

    /// <summary>
    /// 满减促销活动模型类
    /// </summary>
    public class FullCutPromotionModel : IValidatableObject
    {
        public FullCutPromotionModel()
        {
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            State = 1;
        }

        /// <summary>
        /// 活动名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(25, ErrorMessage = "名称长度不能大于25")]
        public string PromotionName { get; set; }

        /// <summary>
        /// 活动类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Required(ErrorMessage = "开始时间不能为空")]
        [DisplayName("开始时间")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Required(ErrorMessage = "结束时间不能为空")]
        [DisplayName("结束时间")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 用户等级下限
        /// </summary>
        public int UserRankLower { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 活动额度1
        /// </summary>
        [Required(ErrorMessage = "活动额度不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "活动额度不能小于0")]
        [DisplayName("活动额度")]
        public int LimitMoney1 { get; set; }

        /// <summary>
        /// 减免额度1
        /// </summary>
        [Required(ErrorMessage = "减免额度不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "减免额度不能小于0")]
        [DisplayName("减免额度")]
        public int CutMoney1 { get; set; }

        /// <summary>
        /// 活动额度2
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "活动额度不能小于0")]
        [DisplayName("活动额度")]
        public int LimitMoney2 { get; set; }

        /// <summary>
        /// 减免额度2
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "减免额度不能小于0")]
        [DisplayName("减免额度")]
        public int CutMoney2 { get; set; }

        /// <summary>
        /// 活动额度3
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "活动额度不能小于0")]
        [DisplayName("活动额度")]
        public int LimitMoney3 { get; set; }

        /// <summary>
        /// 减免额度3
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "减免额度不能小于0")]
        [DisplayName("减免额度")]
        public int CutMoney3 { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errorList = new List<ValidationResult>();

            if (EndTime <= StartTime)
                errorList.Add(new ValidationResult("结束时间必须大于开始时间!", new string[] { "EndTime" }));

            if (LimitMoney1 <= CutMoney1)
                errorList.Add(new ValidationResult("减免金额必须小于限制金额!", new string[] { "CutMoney1" }));

            if (LimitMoney2 == 0 && CutMoney2 != 0)
            {
                errorList.Add(new ValidationResult("请填写正确的限制金额!", new string[] { "LimitMoney2" }));
            }
            else if (LimitMoney2 != 0 && CutMoney2 == 0)
            {
                errorList.Add(new ValidationResult("请填写正确的减免金额!", new string[] { "CutMoney2" }));
            }
            else if (LimitMoney2 != 0 && CutMoney2 != 0)
            {
                if (LimitMoney2 <= CutMoney2)
                    errorList.Add(new ValidationResult("减免金额必须小于限制金额!", new string[] { "CutMoney2" }));
                else if (LimitMoney2 <= LimitMoney1)
                    errorList.Add(new ValidationResult("限制金额2必须大于限制金额1!", new string[] { "LimitMoney2" }));
                else if (CutMoney2 <= CutMoney1)
                    errorList.Add(new ValidationResult("减免金额2必须大于减免金额1!", new string[] { "CutMoney2" }));
            }

            if (LimitMoney3 == 0 && CutMoney3 != 0)
            {
                errorList.Add(new ValidationResult("请填写正确的限制金额!", new string[] { "LimitMoney3" }));
            }
            else if (LimitMoney3 != 0 && CutMoney3 == 0)
            {
                errorList.Add(new ValidationResult("请填写正确的减免金额!", new string[] { "CutMoney3" }));
            }
            else if (LimitMoney3 != 0 && CutMoney3 != 0)
            {
                if (LimitMoney3 <= CutMoney3)
                    errorList.Add(new ValidationResult("减免金额必须小于限制金额!", new string[] { "CutMoney3" }));
                else if (LimitMoney3 <= LimitMoney2)
                    errorList.Add(new ValidationResult("限制金额3必须大于限制金额2!", new string[] { "LimitMoney3" }));
                else if (CutMoney3 <= CutMoney2)
                    errorList.Add(new ValidationResult("减免金额3必须大于减免金额2!", new string[] { "CutMoney3" }));
            }


            return errorList;
        }
    }

    /// <summary>
    /// 满减商品列表模型类
    /// </summary>
    public class FullCutProductListModel
    {
        /// <summary>
        /// 满减商品列表
        /// </summary>
        public DataTable FullCutProductList { get; set; }
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 满减促销活动id
        /// </summary>
        public int PmId { get; set; }
    }
}
