using System;
using System.Collections.Generic;

namespace BrnShop.Core
{
    /// <summary>
    /// 购物车策略接口
    /// </summary>
    public partial interface ICartStrategy
    {
        /// <summary>
        /// 是否持久化订单商品
        /// </summary>
        bool IsPersistOrderProduct { get; }

        /// <summary>
        /// 获得购物车中商品数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        int GetCartProductCount(int uid);

        /// <summary>
        /// 获得购物车中商品数量
        /// </summary>
        /// <param name="sid">用户sid</param>
        /// <returns></returns>
        int GetCartProductCount(string sid);

        /// <summary>
        /// 添加订单商品列表
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        void AddOrderProductList(List<OrderProductInfo> orderProductList);

        /// <summary>
        /// 删除订单商品列表
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        void DeleteOrderProductList(List<OrderProductInfo> orderProductList);

        /// <summary>
        /// 更新订单商品的数量
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        void UpdateOrderProductCount(List<OrderProductInfo> orderProductList);

        /// <summary>
        /// 更新购物车的用户id
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="sid">用户sid</param>
        void UpdateCartUidBySid(int uid, string sid);

        /// <summary>
        /// 更新订单商品的买送促销活动
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        void UpdateOrderProductBuySend(List<OrderProductInfo> orderProductList);

        /// <summary>
        /// 更新订单商品的单品促销活动
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        void UpdateOrderProductSingle(List<OrderProductInfo> orderProductList);

        /// <summary>
        /// 更新订单商品的赠品促销活动
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        void UpdateOrderProductGift(List<OrderProductInfo> orderProductList);

        /// <summary>
        /// 更新订单商品的满赠促销活动
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        void UpdateOrderProductFullSend(List<OrderProductInfo> orderProductList);

        /// <summary>
        /// 更新订单商品的满减促销活动
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        void UpdateOrderProductFullCut(List<OrderProductInfo> orderProductList);

        /// <summary>
        /// 获得购物车商品列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        List<OrderProductInfo> GetCartProductList(int uid);

        /// <summary>
        /// 获得购物车商品列表
        /// </summary>
        /// <param name="sid">用户sid</param>
        /// <returns></returns>
        List<OrderProductInfo> GetCartProductList(string sid);

        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        int ClearCart(int uid);

        /// <summary>
        /// 清空购物车的商品
        /// </summary>
        /// <param name="sid">sid</param>
        /// <returns></returns>
        int ClearCart(string sid);

        /// <summary>
        /// 清空过期购物车
        /// </summary>
        /// <param name="expireTime">过期时间</param>
        void ClearExpiredCart(DateTime expireTime);
    }
}
