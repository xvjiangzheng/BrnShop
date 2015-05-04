using System;

namespace BrnShop.Core
{
    /// <summary>
    /// BrnShop支付插件接口
    /// </summary>
    public partial interface IPayPlugin : IPlugin
    {
        /// <summary>
        /// 付款方式(0代表货到付款，1代表在线付款，2代表线下付款)
        /// </summary>
        int PayMode { get; }

        /// <summary>
        /// 获得支付手续费
        /// </summary>
        /// <param name="productAmount">商品合计</param>
        /// <param name="buyTime">购买时间</param>
        /// <param name="partUserInfo">购买用户</param>
        /// <returns></returns>
        decimal GetPayFee(decimal productAmount, DateTime buyTime, PartUserInfo partUserInfo);

        /// <summary>
        /// 支付控制器
        /// </summary>
        string PayController { get; }

        /// <summary>
        /// 支付动作方法
        /// </summary>
        string PayAction { get; }
    }
}
