using System;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Web.Framework;

namespace BrnShop.Web.Models
{
    /// <summary>
    /// 购物车添加成功模型类
    /// </summary>
    public class CartAddSuccessModel
    {
        /// <summary>
        /// 项id
        /// </summary>
        public int ItemId { get; set; }
        /// <summary>
        /// 项类型(0代表商品，1代表套装)
        /// </summary>
        public int ItemType { get; set; }
    }

    /// <summary>
    /// 购物车模型类
    /// </summary>
    public class CartModel
    {
        /// <summary>
        /// 商品总数量
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 商品合计
        /// </summary>
        public decimal ProductAmount { get; set; }
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
    }

    /// <summary>
    /// 满赠主商品列表模型类
    /// </summary>
    public class FullSendMainProductListModel
    {
        /// <summary>
        /// 满赠促销活动id
        /// </summary>
        public int PmId { get; set; }
        /// <summary>
        /// 开始价格
        /// </summary>
        public int StartPrice { get; set; }
        /// <summary>
        /// 结束价格
        /// </summary>
        public int EndPrice { get; set; }
        /// <summary>
        /// 排序列
        /// </summary>
        public int SortColumn { get; set; }
        /// <summary>
        /// 排序方向
        /// </summary>
        public int SortDirection { get; set; }
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<PartProductInfo> ProductList { get; set; }
        /// <summary>
        /// 订单商品列表
        /// </summary>
        public List<OrderProductInfo> OrderProductList { get; set; }
    }

    /// <summary>
    /// 满减商品列表模型类
    /// </summary>
    public class FullCutProductListModel
    {
        /// <summary>
        /// 满减促销活动id
        /// </summary>
        public int PmId { get; set; }
        /// <summary>
        /// 开始价格
        /// </summary>
        public int StartPrice { get; set; }
        /// <summary>
        /// 结束价格
        /// </summary>
        public int EndPrice { get; set; }
        /// <summary>
        /// 排序列
        /// </summary>
        public int SortColumn { get; set; }
        /// <summary>
        /// 排序方向
        /// </summary>
        public int SortDirection { get; set; }
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<PartProductInfo> ProductList { get; set; }
        /// <summary>
        /// 订单商品列表
        /// </summary>
        public List<OrderProductInfo> OrderProductList { get; set; }
    }
}