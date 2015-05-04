using System;
using System.Collections.Generic;

namespace BrnShop.Core
{
    /// <summary>
    /// 订单策略接口
    /// </summary>
    public partial interface IOrderStrategy
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="orderInfo">订单信息</param>
        /// <param name="isPersistOrderProduct">是否需要持久化订单商品</param>
        /// <param name="orderProductList">订单商品列表</param>
        /// <returns>订单id</returns>
        int CreateOrder(OrderInfo orderInfo, bool isPersistOrderProduct, List<OrderProductInfo> orderProductList);
    }
}
