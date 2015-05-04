using System;
using System.Data;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 商品统计数据访问类
    /// </summary>
    public partial class ProductStats
    {
        /// <summary>
        /// 更新商品统计
        /// </summary>
        /// <param name="updateProductStatState">更新商品统计状态</param>
        public static void UpdateProductStat(UpdateProductStatState updateProductStatState)
        {
            BrnShop.Core.BSPData.RDBS.UpdateProductStat(updateProductStatState);
        }

        /// <summary>
        /// 获得商品总访问量列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProductTotalVisitCountList()
        {
            return BrnShop.Core.BSPData.RDBS.GetProductTotalVisitCountList();
        }
    }
}
