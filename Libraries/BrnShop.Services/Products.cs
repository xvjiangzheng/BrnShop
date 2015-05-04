using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 商品操作管理类
    /// </summary>
    public partial class Products
    {
        /// <summary>
        /// 获得商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static ProductInfo GetProductById(int pid)
        {
            if (pid < 1) return null;
            return BrnShop.Data.Products.GetProductById(pid);
        }

        /// <summary>
        /// 获得部分商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static PartProductInfo GetPartProductById(int pid)
        {
            if (pid < 1) return null;
            return BrnShop.Data.Products.GetPartProductById(pid);
        }

        /// <summary>
        /// 获得部分商品列表
        /// </summary>
        /// <param name="pidList">商品id列表</param>
        /// <returns></returns>
        public static List<PartProductInfo> GetPartProductList(string pidList)
        {
            if (string.IsNullOrEmpty(pidList))
                return new List<PartProductInfo>();
            return BrnShop.Data.Products.GetPartProductList(pidList);
        }

        /// <summary>
        /// 获得商品的影子访问数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static int GetProductShadowVisitCountById(int pid)
        {
            return BrnShop.Data.Products.GetProductShadowVisitCountById(pid);
        }

        /// <summary>
        /// 更新商品的影子访问数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="visitCount">访问数量</param>
        public static void UpdateProductShadowVisitCount(int pid, int visitCount)
        {
            BrnShop.Data.Products.UpdateProductShadowVisitCount(pid, visitCount);
        }

        /// <summary>
        /// 增加商品的影子销售数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="saleCount">销售数量</param>
        public static void AddProductShadowSaleCount(int pid, int saleCount)
        {
            BrnShop.Data.Products.AddProductShadowSaleCount(pid, saleCount);
        }

        /// <summary>
        /// 增加商品的影子评价数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="starType">星星类型</param>
        public static void AddProductShadowReviewCount(int pid, int starType)
        {
            BrnShop.Data.Products.AddProductShadowReviewCount(pid, starType);
        }

        /// <summary>
        /// 获得分类商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="cateId">分类id</param>
        /// <param name="brandId">品牌id</param>
        /// <param name="filterPrice">筛选价格</param>
        /// <param name="catePriceRangeList">分类价格范围列表</param>
        /// <param name="attrValueIdList">属性值id列表</param>
        /// <param name="onlyStock">是否只显示有货</param>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static List<PartProductInfo> GetCategoryProductList(int pageSize, int pageNumber, int cateId, int brandId, int filterPrice, string[] catePriceRangeList, List<int> attrValueIdList, int onlyStock, int sortColumn, int sortDirection)
        {
            return BrnShop.Data.Products.GetCategoryProductList(pageSize, pageNumber, cateId, brandId, filterPrice, catePriceRangeList, attrValueIdList, onlyStock, sortColumn, sortDirection);
        }

        /// <summary>
        /// 获得分类商品数量
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <param name="brandId">品牌id</param>
        /// <param name="filterPrice">筛选价格</param>
        /// <param name="catePriceRangeList">分类价格范围列表</param>
        /// <param name="attrValueIdList">属性值id列表</param>
        /// <param name="onlyStock">是否只显示有货</param>
        /// <returns></returns>
        public static int GetCategoryProductCount(int cateId, int brandId, int filterPrice, string[] catePriceRangeList, List<int> attrValueIdList, int onlyStock)
        {
            return BrnShop.Data.Products.GetCategoryProductCount(cateId, brandId, filterPrice, catePriceRangeList, attrValueIdList, onlyStock);
        }

        /// <summary>
        /// 获得商品汇总列表
        /// </summary>
        /// <param name="pidList">商品id列表</param>
        /// <returns></returns>
        public static DataTable GetProductSummaryList(string pidList)
        {
            return BrnShop.Data.Products.GetProductSummaryList(pidList);
        }





        /// <summary>
        /// 获得商品属性
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="attrId">属性id</param>
        /// <returns></returns>
        public static ProductAttributeInfo GetProductAttributeByPidAndAttrId(int pid, int attrId)
        {
            return BrnShop.Data.Products.GetProductAttributeByPidAndAttrId(pid, attrId);
        }

        /// <summary>
        /// 获得商品属性列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static List<ProductAttributeInfo> GetProductAttributeList(int pid)
        {
            return BrnShop.Data.Products.GetProductAttributeList(pid);
        }

        /// <summary>
        /// 获得扩展商品属性列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static List<ExtProductAttributeInfo> GetExtProductAttributeList(int pid)
        {
            return BrnShop.Data.Products.GetExtProductAttributeList(pid);
        }




        /// <summary>
        /// 获得商品的sku项列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static DataTable GetProductSKUItemList(int pid)
        {
            return BrnShop.Data.Products.GetProductSKUItemList(pid);
        }

        /// <summary>
        /// 获得商品的sku列表
        /// </summary>
        /// <param name="skuGid">sku组id</param>
        /// <returns></returns>
        public static List<ExtProductSKUItemInfo> GetProductSKUListBySKUGid(int skuGid)
        {
            if (skuGid > 0)
                return BrnShop.Data.Products.GetProductSKUListBySKUGid(skuGid);
            return new List<ExtProductSKUItemInfo>();
        }





        /// <summary>
        /// 获得商品图片列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static List<ProductImageInfo> GetProductImageList(int pid)
        {
            if (pid > 0)
                return BrnShop.Data.Products.GetProductImageList(pid);

            return new List<ProductImageInfo>();
        }




        /// <summary>
        /// 获得商品库存
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static ProductStockInfo GetProductStockByPid(int pid)
        {
            return BrnShop.Data.Products.GetProductStockByPid(pid);
        }

        /// <summary>
        /// 获得商品库存数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static int GetProductStockNumberByPid(int pid)
        {
            return BrnShop.Data.Products.GetProductStockNumberByPid(pid);
        }

        /// <summary>
        /// 增加商品库存数量
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public static void IncreaseProductStockNumber(List<OrderProductInfo> orderProductList)
        {
            BrnShop.Data.Products.IncreaseProductStockNumber(orderProductList);
        }

        /// <summary>
        /// 减少商品库存数量
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public static void DecreaseProductStockNumber(List<OrderProductInfo> orderProductList)
        {
            BrnShop.Data.Products.DecreaseProductStockNumber(orderProductList);
        }

        /// <summary>
        /// 获得商品库存列表
        /// </summary>
        /// <param name="pidList">商品id列表</param>
        /// <returns></returns>
        public static List<ProductStockInfo> GetProductStockList(string pidList)
        {
            if (!string.IsNullOrEmpty(pidList))
                return BrnShop.Data.Products.GetProductStockList(pidList);

            return new List<ProductStockInfo>();
        }

        /// <summary>
        /// 获得商品库存
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="productStockList">商品库存列表</param>
        /// <returns></returns>
        public static ProductStockInfo GetProductStock(int pid, List<ProductStockInfo> productStockList)
        {
            foreach (ProductStockInfo productStockInfo in productStockList)
            {
                if (productStockInfo.Pid == pid)
                    return productStockInfo;
            }
            return null;
        }





        /// <summary>
        /// 获得关联商品列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static List<PartProductInfo> GetRelateProductList(int pid)
        {
            return BrnShop.Data.Products.GetRelateProductList(pid);
        }




        /// <summary>
        /// 获得签名商品列表
        /// </summary>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public static List<PartProductInfo> GetSignProductList(string sign)
        {
            List<PartProductInfo> signProductList = BrnShop.Core.BSPCache.Get(CacheKeys.SHOP_SIGNPRODUCT_LIST + sign) as List<PartProductInfo>;
            if (signProductList == null)
            {
                signProductList = BrnShop.Data.Products.GetSignProductList(sign);
                BrnShop.Core.BSPCache.Insert(CacheKeys.SHOP_SIGNPRODUCT_LIST + sign, signProductList);
            }

            return signProductList;
        }
    }
}
