using System;
using System.ComponentModel.DataAnnotations;

namespace BrnShop.PayPlugin.Alipay
{
    /// <summary>
    /// 配置模型类
    /// </summary>
    public class ConfigModel
    {
        /// <summary>
        /// 合作者身份ID
        /// </summary>
        [Required(ErrorMessage = "合作者身份ID不能为空！")]
        public string Partner { get; set; }
        /// <summary>
        /// 交易安全检验码
        /// </summary>
        [Required(ErrorMessage = "交易安全检验码不能为空！")]
        public string Key { get; set; }
        /// <summary>
        /// 收款支付宝帐户
        /// </summary>
        [Required(ErrorMessage = "收款支付宝帐户不能为空！")]
        public string Seller { get; set; }
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
