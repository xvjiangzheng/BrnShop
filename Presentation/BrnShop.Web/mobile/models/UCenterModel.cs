using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;

namespace BrnShop.Web.Mobile.Models
{
    /// <summary>
    /// 安全验证模型类
    /// </summary>
    public class SafeVerifyModel
    {
        /// <summary>
        /// 动作
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 验证方式
        /// </summary>
        public string Mode { get; set; }
    }

    /// <summary>
    /// 安全更新模型类
    /// </summary>
    public class SafeUpdateModel
    {
        /// <summary>
        /// 动作
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// V
        /// </summary>
        public string V { get; set; }
    }

    /// <summary>
    /// 安全成功模型类
    /// </summary>
    public class SafeSuccessModel
    {
        /// <summary>
        /// 动作
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    /// 订单列表模型类
    /// </summary>
    public class OrderListModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 订单列表
        /// </summary>
        public DataTable OrderList { get; set; }
        /// <summary>
        /// 订单商品列表
        /// </summary>
        public List<OrderProductInfo> OrderProductList { get; set; }
        /// <summary>
        /// 开始添加时间
        /// </summary>
        public string StartAddTime { get; set; }
        /// <summary>
        /// 结束添加时间
        /// </summary>
        public string EndAddTime { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderState { get; set; }
    }

    /// <summary>
    /// 订单列表模型类
    /// </summary>
    public class AjaxOrderListModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 订单列表
        /// </summary>
        public List<Dictionary<string, object>> OrderList { get; set; }
        /// <summary>
        /// 订单商品列表
        /// </summary>
        public List<OrderProductInfo> OrderProductList { get; set; }
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
        /// 订单商品列表
        /// </summary>
        public List<OrderProductInfo> OrderProductList { get; set; }
    }

    /// <summary>
    /// 订单动作列表模型类
    /// </summary>
    public class OrderActionListModel
    {
        /// <summary>
        /// 订单信息
        /// </summary>
        public OrderInfo OrderInfo { get; set; }
        /// <summary>
        /// 订单处理列表
        /// </summary>
        public List<OrderActionInfo> OrderActionList { get; set; }
    }

    /// <summary>
    /// 收藏夹商品列表模型类
    /// </summary>
    public class AjaxFavoriteProductListModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<Dictionary<string, object>> ProductList { get; set; }
    }

    /// <summary>
    /// 浏览商品列表模型类
    /// </summary>
    public class AjaxBrowseProductListModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<PartProductInfo> ProductList { get; set; }
    }

    /// <summary>
    /// 配送地址列表模型类
    /// </summary>
    public class ShipAddressListModel
    {
        /// <summary>
        /// 配送地址列表
        /// </summary>
        public List<FullShipAddressInfo> ShipAddressList { get; set; }
        /// <summary>
        /// 配送地址数量
        /// </summary>
        public int ShipAddressCount { get; set; }
    }

    /// <summary>
    /// 配送地址模型类
    /// </summary>
    public class ShipAddressModel
    {
        public string Alias { get; set; }
        public string Consignee { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ZipCode { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public int RegionId { get; set; }
        public string Address { get; set; }
        public int IsDefault { get; set; }
    }

    /// <summary>
    /// 优惠劵列表模型类
    /// </summary>
    public class CouponListModel
    {
        /// <summary>
        /// 列表类型
        /// </summary>
        public int ListType { get; set; }
        /// <summary>
        /// 优惠劵列表
        /// </summary>
        public DataTable CouponList { get; set; }
    }

    /// <summary>
    /// 评价订单模型类
    /// </summary>
    public class ReviewOrderModel
    {
        /// <summary>
        /// 订单信息
        /// </summary>
        public OrderInfo OrderInfo { get; set; }
        /// <summary>
        /// 订单商品列表
        /// </summary>
        public List<OrderProductInfo> OrderProductList { get; set; }
    }
}