using System;
using System.Text;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 品牌操作管理类
    /// </summary>
    public partial class Brands
    {
        /// <summary>
        /// 获得品牌
        /// </summary>
        /// <param name="brandId">品牌id</param>
        /// <returns></returns>
        public static BrandInfo GetBrandById(int brandId)
        {
            BrandInfo brandInfo = BrnShop.Core.BSPCache.Get(CacheKeys.SHOP_BRAND_INFO + brandId) as BrandInfo;
            if (brandInfo == null)
            {
                brandInfo = BrnShop.Data.Brands.GetBrandById(brandId);
                BrnShop.Core.BSPCache.Insert(CacheKeys.SHOP_BRAND_INFO + brandId, brandInfo);
            }

            return brandInfo;
        }

        /// <summary>
        /// 根据品牌名称得到品牌id
        /// </summary>
        /// <param name="brandName">品牌名称</param>
        /// <returns></returns>
        public static int GetBrandIdByName(string brandName)
        {
            if (string.IsNullOrWhiteSpace(brandName))
                return 0;
            return BrnShop.Data.Brands.GetBrandIdByName(brandName);
        }

        /// <summary>
        /// 获得品牌关联的分类
        /// </summary>
        /// <param name="brandId">品牌id</param>
        /// <returns></returns>
        public static List<CategoryInfo> GetBrandCategoryList(int brandId)
        {
            List<CategoryInfo> categoryList = BrnShop.Core.BSPCache.Get(CacheKeys.SHOP_BRAND_CATEGORYLIST + brandId) as List<CategoryInfo>;
            if (categoryList == null)
            {
                categoryList = BrnShop.Data.Brands.GetBrandCategoryList(brandId);
                BrnShop.Core.BSPCache.Insert(CacheKeys.SHOP_BRAND_CATEGORYLIST + brandId, categoryList);
            }

            return categoryList;
        }

        /// <summary>
        /// 获得品牌列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="brandName">品牌名称</param>
        /// <returns></returns>
        public static List<BrandInfo> GetBrandList(int pageSize, int pageNumber, string brandName)
        {
            return BrnShop.Data.Brands.GetBrandList(pageSize, pageNumber, brandName);
        }

        /// <summary>
        /// 获得品牌数量
        /// </summary>
        /// <param name="brandName">品牌名称</param>
        /// <returns></returns>
        public static int GetBrandCount(string brandName)
        {
            return BrnShop.Data.Brands.GetBrandCount(brandName);
        }
    }
}
