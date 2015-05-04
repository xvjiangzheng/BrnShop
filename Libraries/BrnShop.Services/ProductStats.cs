using System;
using System.Data;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 商品统计操作管理类
    /// </summary>
    public partial class ProductStats
    {
        /// <summary>
        /// 更新商品统计
        /// </summary>
        /// <param name="updateProductStatState">更新商品统计状态</param>
        public static void UpdateProductStat(object updateProductStatState)
        {
            BrnShop.Data.ProductStats.UpdateProductStat((UpdateProductStatState)updateProductStatState);

        }

        /// <summary>
        /// 获得商品总访问量列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProductTotalVisitCountList()
        {
            return BrnShop.Data.ProductStats.GetProductTotalVisitCountList();
        }
    }
}
