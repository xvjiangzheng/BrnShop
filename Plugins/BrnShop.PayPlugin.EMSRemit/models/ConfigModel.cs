using System;
using System.ComponentModel.DataAnnotations;

namespace BrnShop.PayPlugin.EMSRemit
{
    /// <summary>
    /// 配置模型类
    /// </summary>
    public class ConfigModel
    {
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
