using System;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Web.Models
{
    /// <summary>
    /// 确认订单模型类
    /// </summary>
    public class ConfirmOrderModel
    {
        /// <summary>
        /// 选中的购物车项键列表
        /// </summary>
        public string SelectedCartItemKeyList { get; set; }

        /// <summary>
        /// 默认完整用户配送地址
        /// </summary>
        public FullShipAddressInfo DefaultFullShipAddressInfo { get; set; }

        /// <summary>
        /// 默认支付插件
        /// </summary>
        public PluginInfo DefaultPayPluginInfo { get; set; }
        /// <summary>
        /// 支付插件列表
        /// </summary>
        public List<PluginInfo> PayPluginList { get; set; }

        /// <summary>
        /// 默认配送插件
        /// </summary>
        public PluginInfo DefaultShipPluginInfo { get; set; }
        /// <summary>
        /// 配送插件列表
        /// </summary>
        public List<PluginInfo> ShipPluginList { get; set; }

        /// <summary>
        /// 支付积分名称
        /// </summary>
        public string PayCreditName { get; set; }
        /// <summary>
        /// 用户支付积分
        /// </summary>
        public int UserPayCredits { get; set; }
        /// <summary>
        /// 最大使用支付积分
        /// </summary>
        public int MaxUsePayCredits { get; set; }

        /// <summary>
        /// 商品总重量
        /// </summary>
        public int TotalWeight { get; set; }
        /// <summary>
        /// 商品总数量
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 商品合计
        /// </summary>
        public decimal ProductAmount { get; set; }
        /// <summary>
        /// 支付费用
        /// </summary>
        public decimal PayFee { get; set; }
        /// <summary>
        /// 配送费用
        /// </summary>
        public decimal ShipFee { get; set; }
        /// <summary>
        /// 满减
        /// </summary>
        public int FullCut { get; set; }
        /// <summary>
        /// 订单合计
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 购物车项列表
        /// </summary>
        public List<CartItemInfo> CartItemList { get; set; }

        /// <summary>
        /// 是否显示验证码
        /// </summary>
        public bool IsVerifyCode { get; set; }
    }

    /// <summary>
    /// 提交结果模型类
    /// </summary>
    public class SubmitResultModel
    {
        public int Oid { get; set; }
        public OrderInfo OrderInfo { get; set; }
        public PluginInfo PayPlugin { get; set; }
    }

    /// <summary>
    /// 支付展示模型类
    /// </summary>
    public class PayShowModel
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public int Oid { get; set; }
        /// <summary>
        /// 订单信息
        /// </summary>
        public OrderInfo OrderInfo { get; set; }
        /// <summary>
        /// 支付插件
        /// </summary>
        public PluginInfo PayPlugin { get; set; }
        /// <summary>
        /// 展示视图
        /// </summary>
        public string ShowView { get; set; }
    }

    /// <summary>
    /// 支付结果模型类
    /// </summary>
    public class PayResultModel
    {
        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 订单信息
        /// </summary>
        public OrderInfo OrderInfo { get; set; }
    }
}