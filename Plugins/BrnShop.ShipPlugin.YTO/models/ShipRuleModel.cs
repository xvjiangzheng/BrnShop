using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BrnShop.ShipPlugin.YTO
{
    /// <summary>
    /// 配送规则模型类
    /// </summary>
    public class ShipRuleModel : IValidatableObject
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空！")]
        public string Name { get; set; }
        /// <summary>
        /// 省id
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 市id
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 县或区id
        /// </summary>
        public int CountyId { get; set; }
        /// <summary>
        /// 类型(0代表按重量计算,1代表按商品件数计算)
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 扩展1
        /// </summary>
        [Required(ErrorMessage = "*")]
        [DisplayName("值")]
        public decimal ExtCode1 { get; set; }
        /// <summary>
        /// 扩展2
        /// </summary>
        [DisplayName("值")]
        public decimal ExtCode2 { get; set; }
        /// <summary>
        /// 免费金额
        /// </summary>
        [Required(ErrorMessage = "请填写免费金额！")]
        [DisplayName("免费金额")]
        public decimal FreeMoney { get; set; }

        /// <summary>
        /// 货到付款支付手续费
        /// </summary>
        [Required(ErrorMessage = "请填写支付手续费！")]
        [DisplayName("支付手续费")]
        public decimal CODPayFee { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errorList = new List<ValidationResult>();

            if (Type == 0 && ExtCode2 <= 0)
                errorList.Add(new ValidationResult("*", new string[] { "ExtCode2" }));

            return errorList;
        }
    }
}
