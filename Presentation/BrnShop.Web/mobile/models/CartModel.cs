using System;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Web.Mobile.Models
{
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
}