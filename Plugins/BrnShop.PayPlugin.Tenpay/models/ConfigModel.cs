using System;
using System.ComponentModel.DataAnnotations;

namespace BrnShop.PayPlugin.Tenpay
{
    /// <summary>
    /// 配置模型类
    /// </summary>
    public class ConfigModel
    {
        /// <summary>
        /// 财付通商户号
        /// </summary>
        [Required(ErrorMessage = "财付通商户号不能为空！")]
        public string BargainorId { get; set; }
        /// <summary>
        /// 财付通密钥
        /// </summary>
        [Required(ErrorMessage = "财付通密钥不能为空！")]
        public string TenpayKey { get; set; }
        /// <summary>
        /// 支付手续费
        /// </summary>
        [Required(ErrorMessage = "支付手续费不能为空！")]
        public decimal PayFee { get; set; }
        /// <summary>
        /// 免费金额
        /// </summary>
        [Required(ErrorMessage = "免费金额不能为空！")]
        public decimal FreeMoney { get; set; }
    }
}
