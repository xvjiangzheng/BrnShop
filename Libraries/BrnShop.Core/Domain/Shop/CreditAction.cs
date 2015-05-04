using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 积分动作枚举
    /// </summary>
    public enum CreditAction
    {
        /// <summary>
        /// 管理员发放
        /// </summary>
        AdminSend = 0,
        /// <summary>
        /// 注册
        /// </summary>
        Register = 1,
        /// <summary>
        /// 登陆
        /// </summary>
        Login = 2,
        /// <summary>
        /// 验证邮箱
        /// </summary>
        VerifyEmail = 3,
        /// <summary>
        /// 验证手机
        /// </summary>
        VerifyMobile = 4,
        /// <summary>
        /// 完善用户资料
        /// </summary>
        CompleteUserInfo = 5,
        /// <summary>
        /// 支付订单
        /// </summary>
        PayOrder = 6,
        /// <summary>
        /// 完成订单
        /// </summary>
        CompleteOrder = 7,
        /// <summary>
        /// 评价商品
        /// </summary>
        ReviewProduct = 8,
        /// <summary>
        /// 单品促销活动
        /// </summary>
        SinglePromotion = 9,
        /// <summary>
        /// 退回订单使用
        /// </summary>
        ReturnOrderUse = 10,
        /// <summary>
        /// 退回订单发放
        /// </summary>
        ReturnOrderSend = 11
    }
}
