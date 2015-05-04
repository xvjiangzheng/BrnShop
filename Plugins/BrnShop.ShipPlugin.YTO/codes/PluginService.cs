using System;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.ShipPlugin.YTO
{
    /// <summary>
    /// 插件服务类
    /// </summary>
    public class PluginService : IShipPlugin
    {
        /// <summary>
        /// 插件配置控制器
        /// </summary>
        public string ConfigController
        {
            get { return "adminyto"; }
        }

        /// <summary>
        /// 插件配置动作方法
        /// </summary>
        public string ConfigAction
        {
            get { return "shiprulelist"; }
        }

        /// <summary>
        /// 是否支持货到付款
        /// </summary>
        public bool SupportCOD
        {
            get { return true; }
        }

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
        public decimal GetCODPayFee(decimal productAmount, DateTime buyTime, int provinceId, int cityId, int countyId, PartUserInfo partUserInfo)
        {
            foreach (ShipRuleInfo shipRuleInfo in PluginUtils.GetShipRuleList())
            {
                if (shipRuleInfo.RegionId == 0 || shipRuleInfo.RegionId == provinceId || shipRuleInfo.RegionId == cityId || shipRuleInfo.RegionId == countyId)
                    return shipRuleInfo.CODPayFee;
            }
            return 0M;
        }

        /// <summary>
        /// 判断是否配送此区域
        /// </summary>
        /// <param name="provinceId">省id</param>
        /// <param name="cityId">市id</param>
        /// <param name="countyId">县或区id</param>
        /// <returns></returns>
        public bool IsShipRegion(int provinceId, int cityId, int countyId)
        {
            foreach (ShipRuleInfo shipRuleInfo in PluginUtils.GetShipRuleList())
            {
                if (shipRuleInfo.RegionId == 0 || shipRuleInfo.RegionId == provinceId || shipRuleInfo.RegionId == cityId || shipRuleInfo.RegionId == countyId)
                    return true;
            }
            return false;
        }

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
        /// <exception cref="System.NotImplementedException"></exception>
        public decimal GetShipFee(int totalWeight, decimal productAmount, List<OrderProductInfo> orderProductList, DateTime buyTime, int provinceId, int cityId, int countyId, PartUserInfo partUserInfo)
        {
            foreach (ShipRuleInfo shipRuleInfo in PluginUtils.GetShipRuleList())
            {
                if (shipRuleInfo.RegionId == 0 || shipRuleInfo.RegionId == provinceId || shipRuleInfo.RegionId == cityId || shipRuleInfo.RegionId == countyId)
                {
                    if (productAmount >= shipRuleInfo.FreeMoney)
                        return 0M;
                    if (shipRuleInfo.Type == 0)
                    {
                        if (totalWeight <= 1000)
                        {
                            return shipRuleInfo.ExtCode1;
                        }
                        else
                        {
                            if (((totalWeight - shipRuleInfo.ExtCode1 * 1000) % (shipRuleInfo.ExtCode2 * 1000)) == 0)
                                return shipRuleInfo.ExtCode1 + shipRuleInfo.ExtCode2 * ((totalWeight - shipRuleInfo.ExtCode1 * 1000) / (shipRuleInfo.ExtCode2 * 1000));
                            else
                                return shipRuleInfo.ExtCode1 + shipRuleInfo.ExtCode2 * (((totalWeight - shipRuleInfo.ExtCode1 * 1000) / (shipRuleInfo.ExtCode2 * 1000)) + 1);
                        }
                    }
                    else if (shipRuleInfo.Type == 1)
                    {
                        decimal shipFee = 0M;
                        foreach (OrderProductInfo orderProductInfo in orderProductList)
                            shipFee += shipRuleInfo.ExtCode1 * orderProductInfo.RealCount;
                        return shipFee;
                    }
                }
            }
            return 0M;
        }
    }
}
