using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;

namespace BrnShop.Web.Admin.Models
{
    /// <summary>
    /// 订单列表模型类
    /// </summary>
    public class OrderListModel
    {
        public PageModel PageModel { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public DataTable OrderList { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OSN { get; set; }
        /// <summary>
        /// 账户名
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderState { get; set; }
    }

    /// <summary>
    /// 订单信息模型类
    /// </summary>
    public class OrderInfoModel
    {
        /// <summary>
        /// 订单信息
        /// </summary>
        public OrderInfo OrderInfo { get; set; }
        /// <summary>
        /// 区域信息
        /// </summary>
        public RegionInfo RegionInfo { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo UserInfo { get; set; }
        /// <summary>
        /// 用户等级
        /// </summary>
        public UserRankInfo UserRankInfo { get; set; }
        /// <summary>
        /// 订单商品列表
        /// </summary>
        public List<OrderProductInfo> OrderProductList { get; set; }
        /// <summary>
        /// 订单处理列表
        /// </summary>
        public List<OrderActionInfo> OrderActionList { get; set; }
    }

    /// <summary>
    /// 操作订单模型类
    /// </summary>
    public class OperateOrderModel
    {
        public int Oid { get; set; }
        public OrderInfo OrderInfo { get; set; }
        public OrderActionType OrderActionType { get; set; }
        public string ActionDes { get; set; }
    }

    /// <summary>
    /// 打印订单模型类
    /// </summary>
    public class PrintOrderModel
    {
        /// <summary>
        /// 订单信息
        /// </summary>
        public OrderInfo OrderInfo { get; set; }
        /// <summary>
        /// 区域信息
        /// </summary>
        public RegionInfo RegionInfo { get; set; }
        /// <summary>
        /// 订单商品列表
        /// </summary>
        public List<OrderProductInfo> OrderProductList { get; set; }
        /// <summary>
        /// 管理员真实姓名
        /// </summary>
        public string AdminRealName { get; set; }
    }

    /// <summary>
    /// 订单退款列表模型类
    /// </summary>
    public class OrderRefundListModel
    {
        public List<OrderRefundInfo> OrderRefundList { get; set; }
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OSN { get; set; }
    }
}
