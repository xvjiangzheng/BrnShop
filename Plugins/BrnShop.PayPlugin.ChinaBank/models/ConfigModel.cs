using System;
using System.ComponentModel.DataAnnotations;

namespace BrnShop.PayPlugin.ChinaBank
{
    /// <summary>
    /// 配置模型类
    /// </summary>
    public class ConfigModel
    {
        /// <summary>
        /// 商户编号
        /// </summary>
        [Required(ErrorMessage = "商户编号不能为空！")]
        public string Mid { get; set; }
        /// <summary>
        /// MD5私钥值
        /// </summary>
        [Required(ErrorMessage = "MD5私钥值不能为空！")]
        public string Key { get; set; }
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
