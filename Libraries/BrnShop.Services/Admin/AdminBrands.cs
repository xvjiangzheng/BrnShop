using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 后台品牌操作管理类
    /// </summary>
    public partial class AdminBrands : Brands
    {
        /// <summary>
        /// 后台获得列表搜索条件
        /// </summary>
        /// <param name="brandName">品牌名称</param>
        /// <returns></returns>
        public static string AdminGetBrandListCondition(string brandName)
        {
            return BrnShop.Data.Brands.AdminGetBrandListCondition(brandName);
        }

        /// <summary>
        /// 后台获得列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetBrandListSort(string sortColumn, string sortDirection)
        {
            return BrnShop.Data.Brands.AdminGetBrandListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得品牌列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable AdminGetBrandList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BrnShop.Data.Brands.AdminGetBrandList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 更新品牌
        /// </summary>
        /// <param name="brandInfo"></param>
        public static void UpdateBrand(BrandInfo brandInfo)
        {
            BrnShop.Data.Brands.UpdateBrand(brandInfo);
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_BRAND_INFO + brandInfo.BrandId);
        }

        /// <summary>
        /// 创建品牌
        /// </summary>
        /// <param name="brandInfo"></param>
        public static void CreateBrand(BrandInfo brandInfo)
        {
            BrnShop.Data.Brands.CreateBrand(brandInfo);
        }

        /// <summary>
        /// 删除品牌
        /// </summary>
        /// <param name="brandId">品牌id</param>
        /// <returns>0代表此品牌下还有商品未删除,1代表删除成功</returns>
        public static int DeleteBrandById(int brandId)
        {
            if (AdminProducts.AdminGetBrandProductCount(brandId) > 0)
                return 0;
            BrnShop.Data.Brands.DeleteBrandById(brandId);
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_BRAND_INFO + brandId);
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_BRAND_CATEGORYLIST + brandId);
            return 1;
        }

        /// <summary>
        /// 后台获得品牌数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetBrandCount(string condition)
        {
            return BrnShop.Data.Brands.AdminGetBrandCount(condition);
        }

        /// <summary>
        /// 后台获得品牌选择列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static DataTable AdminGetBrandSelectList(int pageSize, int pageNumber, string condition)
        {
            return BrnShop.Data.Brands.AdminGetBrandSelectList(pageSize, pageNumber, condition);
        }
    }
}
