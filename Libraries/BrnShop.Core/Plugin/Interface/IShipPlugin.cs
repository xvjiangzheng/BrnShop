using System;
using System.Collections.Generic;

namespace BrnShop.Core
{
    /// <summary>
    /// BrnShop配送插件接口
    /// </summary>
    public partial interface IShipPlugin : IPlugin
    {
        /// <summary>
        /// 是否支持货到付款
        /// </summary>
        bool SupportCOD { get; }

        /// <summary>
        /// 获得货到付款支付手续费
        /// </summary>
        /// <param name="productAmount">商品合计</param>
        /// <param name="buyTime">购买时间</param>
        /// <param name="provinceId">省id</param>
        /// <param name="cityId">市id</param>
        /// <param name="countyId">县或区id</param>
        /// <param name="partUserInfo">购买用户</param>
        /// <returns></returns>
        decimal GetCODPayFee(decimal productAmount, DateTime buyTime, int provinceId, int cityId, int countyId, PartUserInfo partUserInfo);

        /// <summary>
        /// 判断是否配送此区域
        /// </summary>
        /// <param name="provinceId">省id</param>
        /// <param name="cityId">市id</param>
        /// <param name="countyId">县或区id</param>
        /// <returns></returns>
        bool IsShipRegion(int provinceId, int cityId, int countyId);

        /// <summary>
        /// 获得配送费用
        /// </summary>
        /// <param name="totalWeight">商品总重量</param>
        /// <param name="productAmount">商品合计</param>
        /// <param name="orderProductList">订单商品列表</param>
        /// <param name="buyTime">购买时间</param>
        /// <param name="provinceId">省id</param>
        /// <param name="cityId">市id</param>
        /// <param name="countyId">县或区id</param>
        /// <param name="partUserInfo">购买用户</param>
        /// <returns></returns>
        decimal GetShipFee(int totalWeight, decimal productAmount, List<OrderProductInfo> orderProductList, DateTime buyTime, int provinceId, int cityId, int countyId, PartUserInfo partUserInfo);
    }
}
