using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 商品数据访问类
    /// </summary>
    public partial class Products
    {
        private static IProductNOSQLStrategy _productnosql = BSPData.ProductNOSQL;//商品非关系型数据库

        #region 辅助方法

        /// <summary>
        /// 从IDataReader创建ProductInfo
        /// </summary>
        public static ProductInfo BuildProductFromReader(IDataReader reader)
        {
            ProductInfo productInfo = new ProductInfo();

            productInfo.Pid = TypeHelper.ObjectToInt(reader["pid"]);
            productInfo.PSN = reader["psn"].ToString();
            productInfo.CateId = TypeHelper.ObjectToInt(reader["cateid"]);
            productInfo.BrandId = TypeHelper.ObjectToInt(reader["brandid"]);
            productInfo.SKUGid = TypeHelper.ObjectToInt(reader["skugid"]);
            productInfo.Name = reader["name"].ToString();
            productInfo.ShopPrice = TypeHelper.ObjectToDecimal(reader["shopprice"]);
            productInfo.MarketPrice = TypeHelper.ObjectToDecimal(reader["marketprice"]);
            productInfo.CostPrice = TypeHelper.ObjectToDecimal(reader["costprice"]);
            productInfo.State = TypeHelper.ObjectToInt(reader["state"]);
            productInfo.IsBest = TypeHelper.ObjectToInt(reader["isbest"]);
            productInfo.IsHot = TypeHelper.ObjectToInt(reader["ishot"]);
            productInfo.IsNew = TypeHelper.ObjectToInt(reader["isnew"]);
            productInfo.DisplayOrder = TypeHelper.ObjectToInt(reader["displayorder"]);
            productInfo.Weight = TypeHelper.ObjectToInt(reader["weight"]);
            productInfo.ShowImg = reader["showimg"].ToString();
            productInfo.SaleCount = TypeHelper.ObjectToInt(reader["salecount"]);
            productInfo.VisitCount = TypeHelper.ObjectToInt(reader["visitcount"]);
            productInfo.ReviewCount = TypeHelper.ObjectToInt(reader["reviewcount"]);
            productInfo.Star1 = TypeHelper.ObjectToInt(reader["star1"]);
            productInfo.Star2 = TypeHelper.ObjectToInt(reader["star2"]);
            productInfo.Star3 = TypeHelper.ObjectToInt(reader["star3"]);
            productInfo.Star4 = TypeHelper.ObjectToInt(reader["star4"]);
            productInfo.Star5 = TypeHelper.ObjectToInt(reader["star5"]);
            productInfo.AddTime = TypeHelper.ObjectToDateTime(reader["addtime"]);
            productInfo.Description = reader["description"].ToString();

            return productInfo;
        }

        /// <summary>
        /// 从IDataReader创建PartProductInfo
        /// </summary>
        public static PartProductInfo BuildPartProductFromReader(IDataReader reader)
        {
            PartProductInfo partProductInfo = new PartProductInfo();

            partProductInfo.Pid = TypeHelper.ObjectToInt(reader["pid"]);
            partProductInfo.PSN = reader["psn"].ToString();
            partProductInfo.CateId = TypeHelper.ObjectToInt(reader["cateid"]);
            partProductInfo.BrandId = TypeHelper.ObjectToInt(reader["brandid"]);
            partProductInfo.SKUGid = TypeHelper.ObjectToInt(reader["skugid"]);
            partProductInfo.Name = reader["name"].ToString();
            partProductInfo.ShopPrice = TypeHelper.ObjectToDecimal(reader["shopprice"]);
            partProductInfo.MarketPrice = TypeHelper.ObjectToDecimal(reader["marketprice"]);
            partProductInfo.CostPrice = TypeHelper.ObjectToDecimal(reader["costprice"]);
            partProductInfo.State = TypeHelper.ObjectToInt(reader["state"]);
            partProductInfo.IsBest = TypeHelper.ObjectToInt(reader["isbest"]);
            partProductInfo.IsHot = TypeHelper.ObjectToInt(reader["ishot"]);
            partProductInfo.IsNew = TypeHelper.ObjectToInt(reader["isnew"]);
            partProductInfo.DisplayOrder = TypeHelper.ObjectToInt(reader["displayorder"]);
            partProductInfo.Weight = TypeHelper.ObjectToInt(reader["weight"]);
            partProductInfo.ShowImg = reader["showimg"].ToString();
            partProductInfo.SaleCount = TypeHelper.ObjectToInt(reader["salecount"]);
            partProductInfo.VisitCount = TypeHelper.ObjectToInt(reader["visitcount"]);
            partProductInfo.ReviewCount = TypeHelper.ObjectToInt(reader["reviewcount"]);
            partProductInfo.Star1 = TypeHelper.ObjectToInt(reader["star1"]);
            partProductInfo.Star2 = TypeHelper.ObjectToInt(reader["star2"]);
            partProductInfo.Star3 = TypeHelper.ObjectToInt(reader["star3"]);
            partProductInfo.Star4 = TypeHelper.ObjectToInt(reader["star4"]);
            partProductInfo.Star5 = TypeHelper.ObjectToInt(reader["star5"]);
            partProductInfo.AddTime = TypeHelper.ObjectToDateTime(reader["addtime"]);

            return partProductInfo;
        }

        /// <summary>
        /// 从IDataReader创建ProductAttributeInfo
        /// </summary>
        public static ProductAttributeInfo BuildProductAttributeFromReader(IDataReader reader)
        {
            ProductAttributeInfo productAttributeInfo = new ProductAttributeInfo();

            productAttributeInfo.RecordId = TypeHelper.ObjectToInt(reader["recordid"]);
            productAttributeInfo.Pid = TypeHelper.ObjectToInt(reader["pid"]);
            productAttributeInfo.AttrId = TypeHelper.ObjectToInt(reader["attrid"]);
            productAttributeInfo.AttrValueId = TypeHelper.ObjectToInt(reader["attrvalueid"]);
            productAttributeInfo.InputValue = reader["inputvalue"].ToString();

            return productAttributeInfo;
        }

        /// <summary>
        /// 从IDataReader创建ExtProductAttributeInfo
        /// </summary>
        public static ExtProductAttributeInfo BuildExtProductAttributeFromReader(IDataReader reader)
        {
            ExtProductAttributeInfo extProductAttributeInfo = new ExtProductAttributeInfo();

            extProductAttributeInfo.RecordId = TypeHelper.ObjectToInt(reader["recordid"]);
            extProductAttributeInfo.Pid = TypeHelper.ObjectToInt(reader["pid"]);
            extProductAttributeInfo.AttrId = TypeHelper.ObjectToInt(reader["attrid"]);
            extProductAttributeInfo.AttrValueId = TypeHelper.ObjectToInt(reader["attrvalueid"]);
            extProductAttributeInfo.InputValue = reader["inputvalue"].ToString();
            extProductAttributeInfo.AttrValue = reader["attrvalue"].ToString();
            extProductAttributeInfo.IsInput = TypeHelper.ObjectToInt(reader["isinput"]);
            extProductAttributeInfo.AttrName = reader["attrname"].ToString();
            extProductAttributeInfo.AttrGroupId = TypeHelper.ObjectToInt(reader["attrgroupid"]);
            extProductAttributeInfo.AttrGroupName = reader["attrgroupname"].ToString();

            return extProductAttributeInfo;
        }

        /// <summary>
        /// 从IDataReader创建ExtProductSKUItemInfo
        /// </summary>
        public static ExtProductSKUItemInfo BuildExtProductSKUItemFromReader(IDataReader reader)
        {
            ExtProductSKUItemInfo extProductSKUItemInfo = new ExtProductSKUItemInfo();

            extProductSKUItemInfo.RecordId = TypeHelper.ObjectToInt(reader["recordid"]);
            extProductSKUItemInfo.SKUGid = TypeHelper.ObjectToInt(reader["skugid"]);
            extProductSKUItemInfo.Pid = TypeHelper.ObjectToInt(reader["pid"]);
            extProductSKUItemInfo.AttrId = TypeHelper.ObjectToInt(reader["attrid"]);
            extProductSKUItemInfo.AttrValueId = TypeHelper.ObjectToInt(reader["attrvalueid"]);
            extProductSKUItemInfo.InputValue = reader["inputvalue"].ToString();
            extProductSKUItemInfo.AttrValue = reader["attrvalue"].ToString();
            extProductSKUItemInfo.IsInput = TypeHelper.ObjectToInt(reader["isinput"]);
            extProductSKUItemInfo.AttrName = reader["attrname"].ToString();
            extProductSKUItemInfo.AttrShowType = TypeHelper.ObjectToInt(reader["attrshowtype"]);
            extProductSKUItemInfo.ShowImg = reader["showimg"].ToString();

            return extProductSKUItemInfo;
        }

        /// <summary>
        /// 从IDataReader创建ProductImageInfo
        /// </summary>
        public static ProductImageInfo BuildProductImageFromReader(IDataReader reader)
        {
            ProductImageInfo productImageInfo = new ProductImageInfo();

            productImageInfo.PImgId = TypeHelper.ObjectToInt(reader["pimgid"]);
            productImageInfo.Pid = TypeHelper.ObjectToInt(reader["pid"]);
            productImageInfo.ShowImg = reader["showimg"].ToString();
            productImageInfo.IsMain = TypeHelper.ObjectToInt(reader["ismain"]);
            productImageInfo.DisplayOrder = TypeHelper.ObjectToInt(reader["displayorder"]);

            return productImageInfo;
        }

        /// <summary>
        /// 从IDataReader创建ProductStockInfo
        /// </summary>
        public static ProductStockInfo BuildProductStockFromReader(IDataReader reader)
        {
            ProductStockInfo productStockInfo = new ProductStockInfo();

            productStockInfo.Pid = TypeHelper.ObjectToInt(reader["pid"]);
            productStockInfo.Number = TypeHelper.ObjectToInt(reader["number"]);
            productStockInfo.Limit = TypeHelper.ObjectToInt(reader["limit"]);

            return productStockInfo;
        }

        /// <summary>
        /// 从IDataReader创建ProductKeyWordInfo
        /// </summary>
        public static ProductKeywordInfo BuildProductKeyWordFromReader(IDataReader reader)
        {
            ProductKeywordInfo productKeyWordInfo = new ProductKeywordInfo();

            productKeyWordInfo.KeywordId = TypeHelper.ObjectToInt(reader["keywordid"]);
            productKeyWordInfo.Keyword = reader["keyword"].ToString();
            productKeyWordInfo.Pid = TypeHelper.ObjectToInt(reader["pid"]);
            productKeyWordInfo.Relevancy = TypeHelper.ObjectToInt(reader["relevancy"]);

            return productKeyWordInfo;
        }

        /// <summary>
        /// 从IDataReader创建TimeProductInfo
        /// </summary>
        public static TimeProductInfo BuildTimeProductFromReader(IDataReader reader)
        {
            TimeProductInfo timeProductInfo = new TimeProductInfo();

            timeProductInfo.RecordId = TypeHelper.ObjectToInt(reader["recordid"]);
            timeProductInfo.Pid = TypeHelper.ObjectToInt(reader["pid"]);
            timeProductInfo.OnSaleState = TypeHelper.ObjectToInt(reader["onsalestate"]);
            timeProductInfo.OutSaleState = TypeHelper.ObjectToInt(reader["outsalestate"]);
            timeProductInfo.OnSaleTime = TypeHelper.ObjectToDateTime(reader["onsaletime"]);
            timeProductInfo.OutSaleTime = TypeHelper.ObjectToDateTime(reader["outsaletime"]);

            return timeProductInfo;
        }

        #endregion

        /// <summary>
        /// 创建商品
        /// </summary>
        /// <param name="productInfo">商品信息</param>
        /// <returns>商品id</returns>
        public static int CreateProduct(ProductInfo productInfo)
        {
            int pid = BrnShop.Core.BSPData.RDBS.CreateProduct(productInfo);
            if (_productnosql != null && pid > 0)
            {
                productInfo.Pid = pid;
                _productnosql.CreateProduct(productInfo);
            }
            return pid;
        }

        /// <summary>
        /// 更新商品
        /// </summary>
        public static void UpdateProduct(ProductInfo productInfo)
        {
            BrnShop.Core.BSPData.RDBS.UpdateProduct(productInfo);
            if (_productnosql != null)
                _productnosql.UpdateProduct(productInfo);
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="pidList">商品id</param>
        public static void DeleteProductById(string pidList)
        {
            BrnShop.Core.BSPData.RDBS.DeleteProductById(pidList);
            if (_productnosql != null)
                _productnosql.DeleteProductById(pidList);
        }

        /// <summary>
        /// 后台获得商品列表条件
        /// </summary>
        /// <param name="productName">商品名称</param>
        /// <param name="cateId">分类id</param>
        /// <param name="brandId">品牌id</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public static string AdminGetProductListCondition(string productName, int cateId, int brandId, int state)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetProductListCondition(productName, cateId, brandId, state);
        }

        /// <summary>
        /// 后台获得商品列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetProductListSort(string sortColumn, string sortDirection)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetProductListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable AdminGetProductList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetProductList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 后台获得商品数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetProductCount(string condition)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetProductCount(condition);
        }

        /// <summary>
        /// 后台获得商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static ProductInfo AdminGetProductById(int pid)
        {
            ProductInfo productInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.AdminGetProductById(pid);
            if (reader.Read())
            {
                productInfo = BuildProductFromReader(reader);
            }

            reader.Close();
            return productInfo;
        }

        /// <summary>
        /// 后台获得部分商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static PartProductInfo AdminGetPartProductById(int pid)
        {
            PartProductInfo partProductInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.AdminGetPartProductById(pid);
            if (reader.Read())
            {
                partProductInfo = BuildPartProductFromReader(reader);
            }

            reader.Close();
            return partProductInfo;
        }

        /// <summary>
        /// 获得商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static ProductInfo GetProductById(int pid)
        {
            ProductInfo productInfo = null;

            if (_productnosql != null)
            {
                productInfo = _productnosql.GetProductById(pid);
            }
            else
            {
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetProductById(pid);
                if (reader.Read())
                {
                    productInfo = BuildProductFromReader(reader);
                }
                reader.Close();
            }

            return productInfo;
        }

        /// <summary>
        /// 获得部分商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static PartProductInfo GetPartProductById(int pid)
        {
            PartProductInfo partProductInfo = null;

            if (_productnosql != null)
            {
                partProductInfo = _productnosql.GetPartProductById(pid);
            }
            else
            {
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetPartProductById(pid);
                if (reader.Read())
                {
                    partProductInfo = BuildPartProductFromReader(reader);
                }
                reader.Close();
            }

            return partProductInfo;
        }

        /// <summary>
        /// 修改商品状态
        /// </summary>
        /// <param name="pidList">商品id</param>
        /// <param name="state">商品状态</param>
        public static bool UpdateProductState(string pidList, ProductState state)
        {
            bool result = BrnShop.Core.BSPData.RDBS.UpdateProductState(pidList, state);
            if (_productnosql != null)
                _productnosql.UpdateProductState(pidList, state);
            return result;
        }

        /// <summary>
        /// 修改商品排序
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="displayOrder">商品排序</param>
        public static bool UpdateProductDisplayOrder(int pid, int displayOrder)
        {
            bool result = BrnShop.Core.BSPData.RDBS.UpdateProductDisplayOrder(pid, displayOrder);
            if (_productnosql != null)
                _productnosql.UpdateProductDisplayOrder(pid, displayOrder);
            return result;
        }

        /// <summary>
        /// 改变商品新品状态
        /// </summary>
        /// <param name="pidList">商品id</param>
        /// <param name="isNew">是否新品</param>
        public static bool ChangeProductIsNew(string pidList, int isNew)
        {
            bool result = BrnShop.Core.BSPData.RDBS.ChangeProductIsNew(pidList, isNew);
            if (_productnosql != null)
                _productnosql.ChangeProductIsNew(pidList, isNew);
            return result;
        }

        /// <summary>
        /// 改变商品热销状态
        /// </summary>
        /// <param name="pidList">商品id</param>
        /// <param name="isHot">是否热销</param>
        public static bool ChangeProductIsHot(string pidList, int isHot)
        {
            bool result = BrnShop.Core.BSPData.RDBS.ChangeProductIsHot(pidList, isHot);
            if (_productnosql != null)
                _productnosql.ChangeProductIsHot(pidList, isHot);
            return result;
        }

        /// <summary>
        /// 改变商品精品状态
        /// </summary>
        /// <param name="pidList">商品id</param>
        /// <param name="isBest">是否精品</param>
        public static bool ChangeProductIsBest(string pidList, int isBest)
        {
            bool result = BrnShop.Core.BSPData.RDBS.ChangeProductIsBest(pidList, isBest);
            if (_productnosql != null)
                _productnosql.ChangeProductIsBest(pidList, isBest);
            return result;
        }

        /// <summary>
        /// 修改商品商城价格
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="shopPrice">商城价格</param>
        public static bool UpdateProductShopPrice(int pid, decimal shopPrice)
        {
            bool result = BrnShop.Core.BSPData.RDBS.UpdateProductShopPrice(pid, shopPrice);
            if (_productnosql != null)
                _productnosql.UpdateProductShopPrice(pid, shopPrice);
            return result;
        }

        /// <summary>
        /// 更新商品图片
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="showImg">商品图片</param>
        public static void UpdateProductShowImage(int pid, string showImg)
        {
            BrnShop.Core.BSPData.RDBS.UpdateProductShowImage(pid, showImg);
            if (_productnosql != null)
                _productnosql.UpdateProductShowImage(pid, showImg);
        }

        /// <summary>
        /// 后台通过商品名称获得商品id
        /// </summary>
        /// <param name="name">商品名称</param>
        /// <returns></returns>
        public static int AdminGetProductIdByName(string name)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetProductIdByName(name);
        }

        /// <summary>
        /// 获得部分商品列表
        /// </summary>
        /// <param name="pidList">商品id列表</param>
        /// <returns></returns>
        public static List<PartProductInfo> GetPartProductList(string pidList)
        {
            List<PartProductInfo> partProductList = null;

            if (_productnosql != null)
            {
                partProductList = _productnosql.GetPartProductList(CommonHelper.ArrayToList(StringHelper.SplitString(pidList)));
            }
            else
            {
                partProductList = new List<PartProductInfo>();
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetPartProductList(pidList);
                while (reader.Read())
                {
                    PartProductInfo partProductInfo = BuildPartProductFromReader(reader);
                    partProductList.Add(partProductInfo);
                }

                reader.Close();
            }

            return partProductList;
        }

        /// <summary>
        /// 获得商品的影子访问数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static int GetProductShadowVisitCountById(int pid)
        {
            return BrnShop.Core.BSPData.RDBS.GetProductShadowVisitCountById(pid);
        }

        /// <summary>
        /// 更新商品的影子访问数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="visitCount">访问数量</param>
        public static void UpdateProductShadowVisitCount(int pid, int visitCount)
        {
            BrnShop.Core.BSPData.RDBS.UpdateProductShadowVisitCount(pid, visitCount);
            if (_productnosql != null)
                _productnosql.UpdateProductShadowVisitCount(pid, visitCount);
        }

        /// <summary>
        /// 增加商品的影子销售数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="saleCount">销售数量</param>
        public static void AddProductShadowSaleCount(int pid, int saleCount)
        {
            BrnShop.Core.BSPData.RDBS.AddProductShadowSaleCount(pid, saleCount);
            if (_productnosql != null)
                _productnosql.AddProductShadowSaleCount(pid, saleCount);
        }

        /// <summary>
        /// 增加商品的影子评价数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="starType">星星类型</param>
        public static void AddProductShadowReviewCount(int pid, int starType)
        {
            BrnShop.Core.BSPData.RDBS.AddProductShadowReviewCount(pid, starType);
            if (_productnosql != null)
                _productnosql.AddProductShadowReviewCount(pid, starType);
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
            List<PartProductInfo> partProductList = new List<PartProductInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetCategoryProductList(pageSize, pageNumber, cateId, brandId, filterPrice, catePriceRangeList, attrValueIdList, onlyStock, sortColumn, sortDirection);
            while (reader.Read())
            {
                PartProductInfo partProductInfo = BuildPartProductFromReader(reader);
                partProductList.Add(partProductInfo);
            }

            reader.Close();
            return partProductList;
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
            return BrnShop.Core.BSPData.RDBS.GetCategoryProductCount(cateId, brandId, filterPrice, catePriceRangeList, attrValueIdList, onlyStock);
        }

        /// <summary>
        /// 获得商品汇总列表
        /// </summary>
        /// <param name="pidList">商品id列表</param>
        /// <returns></returns>
        public static DataTable GetProductSummaryList(string pidList)
        {
            return BrnShop.Core.BSPData.RDBS.GetProductSummaryList(pidList);
        }

        /// <summary>
        /// 后台获得指定品牌商品的数量
        /// </summary>
        /// <param name="brandId">品牌id</param>
        /// <returns></returns>
        public static int AdminGetBrandProductCount(int brandId)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetBrandProductCount(brandId);
        }

        /// <summary>
        /// 后台获得指定分类商品的数量
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        public static int AdminGetCategoryProductCount(int cateId)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetCategoryProductCount(cateId);
        }

        /// <summary>
        /// 后台获得属性值商品的数量
        /// </summary>
        /// <param name="attrValueId">属性值id</param>
        /// <returns></returns>
        public static int AdminGetAttrValueProductCount(int attrValueId)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetAttrValueProductCount(attrValueId);
        }





        /// <summary>
        /// 创建商品属性
        /// </summary>
        public static bool CreateProductAttribute(ProductAttributeInfo productAttributeInfo)
        {
            bool result = BrnShop.Core.BSPData.RDBS.CreateProductAttribute(productAttributeInfo);
            if (_productnosql != null)
                _productnosql.ClearProductAttribute(productAttributeInfo.Pid);
            return result;
        }

        /// <summary>
        /// 更新商品属性
        /// </summary>
        public static bool UpdateProductAttribute(ProductAttributeInfo productAttributeInfo)
        {
            bool result = BrnShop.Core.BSPData.RDBS.UpdateProductAttribute(productAttributeInfo);
            if (_productnosql != null)
                _productnosql.ClearProductAttribute(productAttributeInfo.Pid);
            return result;
        }

        /// <summary>
        /// 删除商品属性
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="attrId">属性id</param>
        /// <returns></returns>
        public static bool DeleteProductAttributeByPidAndAttrId(int pid, int attrId)
        {
            bool result = BrnShop.Core.BSPData.RDBS.DeleteProductAttributeByPidAndAttrId(pid, attrId);
            if (_productnosql != null)
                _productnosql.ClearProductAttribute(pid);
            return result;
        }

        /// <summary>
        /// 获得商品属性
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="attrId">属性id</param>
        /// <returns></returns>
        public static ProductAttributeInfo GetProductAttributeByPidAndAttrId(int pid, int attrId)
        {
            ProductAttributeInfo productAttributeInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetProductAttributeByPidAndAttrId(pid, attrId);
            if (reader.Read())
            {
                productAttributeInfo = BuildProductAttributeFromReader(reader);
            }
            reader.Close();
            return productAttributeInfo;
        }

        /// <summary>
        /// 获得商品属性列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static List<ProductAttributeInfo> GetProductAttributeList(int pid)
        {
            List<ProductAttributeInfo> productAttributeList = new List<ProductAttributeInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetProductAttributeList(pid);
            while (reader.Read())
            {
                ProductAttributeInfo productAttributeInfo = BuildProductAttributeFromReader(reader);
                productAttributeList.Add(productAttributeInfo);
            }
            reader.Close();
            return productAttributeList;
        }

        /// <summary>
        /// 获得扩展商品属性列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static List<ExtProductAttributeInfo> GetExtProductAttributeList(int pid)
        {
            List<ExtProductAttributeInfo> extProductAttributeList = null;

            if (_productnosql != null)
            {
                extProductAttributeList = _productnosql.GetExtProductAttributeList(pid);
                if (extProductAttributeList == null)
                {
                    extProductAttributeList = new List<ExtProductAttributeInfo>();
                    IDataReader reader = BrnShop.Core.BSPData.RDBS.GetExtProductAttributeList(pid);
                    while (reader.Read())
                    {
                        ExtProductAttributeInfo extProductAttributeInfo = BuildExtProductAttributeFromReader(reader);
                        extProductAttributeList.Add(extProductAttributeInfo);
                    }
                    reader.Close();
                    _productnosql.CreateExtProductAttributeList(pid, extProductAttributeList);
                }
            }
            else
            {
                extProductAttributeList = new List<ExtProductAttributeInfo>();
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetExtProductAttributeList(pid);
                while (reader.Read())
                {
                    ExtProductAttributeInfo extProductAttributeInfo = BuildExtProductAttributeFromReader(reader);
                    extProductAttributeList.Add(extProductAttributeInfo);
                }
                reader.Close();
            }

            return extProductAttributeList;
        }




        /// <summary>
        /// 创建商品sku项
        /// </summary>
        /// <param name="productSKUItemInfo">商品sku项信息</param>
        public static void CreateProductSKUItem(ProductSKUItemInfo productSKUItemInfo)
        {
            BrnShop.Core.BSPData.RDBS.CreateProductSKUItem(productSKUItemInfo);
            if (_productnosql != null)
                _productnosql.ClearProductSKU(productSKUItemInfo.Pid);
        }

        /// <summary>
        /// 获得商品的sku项列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static DataTable GetProductSKUItemList(int pid)
        {
            return BrnShop.Core.BSPData.RDBS.GetProductSKUItemList(pid);
        }

        /// <summary>
        /// 判断sku组id是否存在
        /// </summary>
        /// <param name="skuGid">sku组id</param>
        /// <returns></returns>
        public static bool IsExistSKUGid(int skuGid)
        {
            return BrnShop.Core.BSPData.RDBS.IsExistSKUGid(skuGid);
        }

        /// <summary>
        /// 获得商品的sku列表
        /// </summary>
        /// <param name="skuGid">sku组id</param>
        /// <returns></returns>
        public static List<ExtProductSKUItemInfo> GetProductSKUListBySKUGid(int skuGid)
        {
            List<ExtProductSKUItemInfo> productSKUList = null;

            if (_productnosql != null)
            {
                productSKUList = _productnosql.GetProductSKUListBySKUGid(skuGid);
                if (productSKUList == null)
                {
                    productSKUList = new List<ExtProductSKUItemInfo>();
                    IDataReader reader = BrnShop.Core.BSPData.RDBS.GetProductSKUListBySKUGid(skuGid);
                    while (reader.Read())
                    {
                        ExtProductSKUItemInfo extProductSKUItemInfo = BuildExtProductSKUItemFromReader(reader);
                        productSKUList.Add(extProductSKUItemInfo);
                    }
                    reader.Close();
                    _productnosql.CreateProductSKUList(skuGid, productSKUList);
                }
            }
            else
            {
                productSKUList = new List<ExtProductSKUItemInfo>();
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetProductSKUListBySKUGid(skuGid);
                while (reader.Read())
                {
                    ExtProductSKUItemInfo extProductSKUItemInfo = BuildExtProductSKUItemFromReader(reader);
                    productSKUList.Add(extProductSKUItemInfo);
                }
                reader.Close();
            }

            return productSKUList;
        }






        /// <summary>
        /// 创建商品图片
        /// </summary>
        public static bool CreateProductImage(ProductImageInfo productImageInfo)
        {
            bool result = BrnShop.Core.BSPData.RDBS.CreateProductImage(productImageInfo);
            if (_productnosql != null)
                _productnosql.ClearProductImage(productImageInfo.Pid);
            return result;
        }

        /// <summary>
        /// 获得商品图片
        /// </summary>
        /// <param name="pImgId">图片id</param>
        /// <returns></returns>
        public static ProductImageInfo GetProductImageById(int pImgId)
        {
            ProductImageInfo productImageInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetProductImageById(pImgId);
            if (reader.Read())
            {
                productImageInfo = BuildProductImageFromReader(reader);
            }
            reader.Close();
            return productImageInfo;

        }

        /// <summary>
        /// 删除商品图片
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="pImgId">图片id</param>
        /// <returns></returns>
        public static bool DeleteProductImageById(int pid, int pImgId)
        {
            bool result = BrnShop.Core.BSPData.RDBS.DeleteProductImageById(pImgId);
            if (_productnosql != null)
                _productnosql.ClearProductImage(pid);
            return result;
        }

        /// <summary>
        /// 设置图片为商品主图
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="pImgId">商品图片id</param>
        /// <returns></returns>
        public static bool SetProductMainImage(int pid, int pImgId)
        {
            bool result = BrnShop.Core.BSPData.RDBS.SetProductMainImage(pid, pImgId);
            if (_productnosql != null)
                _productnosql.ClearProductImage(pid);
            return result;
        }

        /// <summary>
        /// 改变商品图片排序
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="pImgId">商品图片id</param>
        /// <param name="displayOrder">The display order.</param>
        /// <returns></returns>
        public static bool ChangeProductImageDisplayOrder(int pid, int pImgId, int displayOrder)
        {
            bool result = BrnShop.Core.BSPData.RDBS.ChangeProductImageDisplayOrder(pImgId, displayOrder);
            if (_productnosql != null)
                _productnosql.ClearProductImage(pid);
            return result;
        }

        /// <summary>
        /// 获得商品图片列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static List<ProductImageInfo> GetProductImageList(int pid)
        {
            List<ProductImageInfo> productImageList = null;

            if (_productnosql != null)
            {
                productImageList = _productnosql.GetProductImageList(pid);
                if (productImageList == null)
                {
                    productImageList = new List<ProductImageInfo>();
                    IDataReader reader = BrnShop.Core.BSPData.RDBS.GetProductImageList(pid);
                    while (reader.Read())
                    {
                        ProductImageInfo productImageInfo = BuildProductImageFromReader(reader);
                        productImageList.Add(productImageInfo);
                    }
                    reader.Close();
                    _productnosql.CreateProductImageList(pid, productImageList);
                }
            }
            else
            {
                productImageList = new List<ProductImageInfo>();
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetProductImageList(pid);
                while (reader.Read())
                {
                    ProductImageInfo productImageInfo = BuildProductImageFromReader(reader);
                    productImageList.Add(productImageInfo);
                }
                reader.Close();
            }

            return productImageList;
        }




        /// <summary>
        /// 获得商品库存
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static ProductStockInfo GetProductStockByPid(int pid)
        {
            ProductStockInfo productStockInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetProductStockByPid(pid);
            if (reader.Read())
            {
                productStockInfo = BuildProductStockFromReader(reader);
            }
            reader.Close();
            return productStockInfo;
        }

        /// <summary>
        /// 创建商品库存
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="number">商品数量</param>
        /// <param name="limit">商品库存警戒线</param>
        /// <returns></returns>
        public static bool CreateProductStock(int pid, int number, int limit)
        {
            return BrnShop.Core.BSPData.RDBS.CreateProductStock(pid, number, limit);
        }

        /// <summary>
        /// 更新商品库存
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="number">商品数量</param>
        /// <param name="limit">商品库存警戒线</param>
        public static void UpdateProductStock(int pid, int number, int limit)
        {
            BrnShop.Core.BSPData.RDBS.UpdateProductStock(pid, number, limit);
            if (_productnosql != null)
                _productnosql.ClearProductStockNumber(pid);
        }

        /// <summary>
        /// 更新商品库存数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="number">库存数量</param>
        /// <returns></returns>
        public static bool UpdateProductStockNumber(int pid, int number)
        {
            bool result = BrnShop.Core.BSPData.RDBS.UpdateProductStockNumber(pid, number);
            if (_productnosql != null)
                _productnosql.ClearProductStockNumber(pid);
            return result;
        }

        /// <summary>
        /// 获得商品库存数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static int GetProductStockNumberByPid(int pid)
        {
            int productStockNumber;

            if (_productnosql != null)
            {
                productStockNumber = _productnosql.GetProductStockNumberByPid(pid);
                if (productStockNumber == -1)
                {
                    productStockNumber = BrnShop.Core.BSPData.RDBS.GetProductStockNumberByPid(pid);
                    _productnosql.CreateProductStockNumber(pid, productStockNumber);
                }
            }
            else
            {
                productStockNumber = BrnShop.Core.BSPData.RDBS.GetProductStockNumberByPid(pid);
            }

            return productStockNumber;
        }

        /// <summary>
        /// 增加商品库存数量
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public static void IncreaseProductStockNumber(List<OrderProductInfo> orderProductList)
        {
            BrnShop.Core.BSPData.RDBS.IncreaseProductStockNumber(orderProductList);
            if (_productnosql != null)
                _productnosql.IncreaseProductStockNumber(orderProductList);
        }

        /// <summary>
        /// 减少商品库存数量
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        public static void DecreaseProductStockNumber(List<OrderProductInfo> orderProductList)
        {
            BrnShop.Core.BSPData.RDBS.DecreaseProductStockNumber(orderProductList);
            if (_productnosql != null)
                _productnosql.DecreaseProductStockNumber(orderProductList);
        }

        /// <summary>
        /// 获得商品库存列表
        /// </summary>
        /// <param name="pidList">商品id列表</param>
        /// <returns></returns>
        public static List<ProductStockInfo> GetProductStockList(string pidList)
        {
            List<ProductStockInfo> productStockList = new List<ProductStockInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetProductStockList(pidList);
            while (reader.Read())
            {
                ProductStockInfo productStockInfo = BuildProductStockFromReader(reader);
                productStockList.Add(productStockInfo);
            }
            reader.Close();
            return productStockList;
        }





        /// <summary>
        /// 创建商品关键词
        /// </summary>
        /// <param name="productKeywordInfo">商品关键词信息</param>
        public static void CreateProductKeyword(ProductKeywordInfo productKeywordInfo)
        {
            BrnShop.Core.BSPData.RDBS.CreateProductKeyword(productKeywordInfo);
        }

        /// <summary>
        /// 更新商品关键词
        /// </summary>
        /// <param name="productKeywordInfo">商品关键词信息</param>
        public static void UpdateProductKeyword(ProductKeywordInfo productKeywordInfo)
        {
            BrnShop.Core.BSPData.RDBS.UpdateProductKeyword(productKeywordInfo);
        }

        /// <summary>
        /// 删除商品关键词
        /// </summary>
        /// <param name="keywordIdList">关键词id列表</param>
        public static bool DeleteProductKeyword(string keywordIdList)
        {
            return BrnShop.Core.BSPData.RDBS.DeleteProductKeyword(keywordIdList);
        }

        /// <summary>
        /// 获得商品关键词列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static List<ProductKeywordInfo> GetProductKeywordList(int pid)
        {
            List<ProductKeywordInfo> productKeyWordList = new List<ProductKeywordInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetProductKeywordList(pid);
            while (reader.Read())
            {
                ProductKeywordInfo productKeyWordInfo = BuildProductKeyWordFromReader(reader);
                productKeyWordList.Add(productKeyWordInfo);
            }

            reader.Close();
            return productKeyWordList;
        }

        /// <summary>
        /// 是否存在商品关键词
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public static bool IsExistProductKeyword(int pid, string keyword)
        {
            return BrnShop.Core.BSPData.RDBS.IsExistProductKeyword(pid, keyword);
        }

        /// <summary>
        /// 更新商品关键词的相关性
        /// </summary>
        /// <param name="keywordId">关键词id</param>
        /// <param name="relevancy">相关性</param>
        /// <returns></returns>
        public static bool UpdateProductKeywordRelevancy(int keywordId, int relevancy)
        {
            return BrnShop.Core.BSPData.RDBS.UpdateProductKeywordRelevancy(keywordId, relevancy);
        }






        /// <summary>
        /// 添加关联商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="relatePid">关联商品id</param>
        public static void AddRelateProduct(int pid, int relatePid)
        {
            BrnShop.Core.BSPData.RDBS.AddRelateProduct(pid, relatePid);
            if (_productnosql != null)
                _productnosql.ClearRelatePidList(pid);
        }

        /// <summary>
        /// 删除关联商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="relatePid">关联商品id</param>
        /// <returns></returns>
        public static bool DeleteRelateProductByPidAndRelatePid(int pid, int relatePid)
        {
            bool result = BrnShop.Core.BSPData.RDBS.DeleteRelateProductByPidAndRelatePid(pid, relatePid);
            if (_productnosql != null)
                _productnosql.ClearRelatePidList(pid);
            return result;
        }

        /// <summary>
        /// 关联商品是否已经存在
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="relatePid">关联商品id</param>
        public static bool IsExistRelateProduct(int pid, int relatePid)
        {
            return BrnShop.Core.BSPData.RDBS.IsExistRelateProduct(pid, relatePid);
        }

        /// <summary>
        /// 后台获得关联商品列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static DataTable AdminGetRelateProductList(int pid)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetRelateProductList(pid);
        }

        /// <summary>
        /// 获得关联商品列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static List<PartProductInfo> GetRelateProductList(int pid)
        {
            List<PartProductInfo> partProductList = null;

            if (_productnosql != null)
            {
                List<string> relatePidList = _productnosql.GetRelatePidList(pid);
                if (relatePidList == null)
                {
                    relatePidList = new List<string>();
                    foreach (DataRow row in AdminGetRelateProductList(pid).Rows)
                    {
                        relatePidList.Add(row["relatepid"].ToString());
                    }
                    _productnosql.CreateRelatePidList(pid, relatePidList);
                }
                partProductList = _productnosql.GetPartProductList(relatePidList);
            }
            else
            {
                partProductList = new List<PartProductInfo>();
                IDataReader reader = BrnShop.Core.BSPData.RDBS.GetRelateProductList(pid);
                while (reader.Read())
                {
                    PartProductInfo partProductInfo = BuildPartProductFromReader(reader);
                    partProductList.Add(partProductInfo);
                }
                reader.Close();
            }

            return partProductList;
        }





        /// <summary>
        /// 添加签名商品
        /// </summary>
        /// <param name="sign">签名</param>
        /// <param name="pid">商品id</param>
        public static void AddSignProduct(string sign, int pid)
        {
            BrnShop.Core.BSPData.RDBS.AddSignProduct(sign, pid);
        }

        /// <summary>
        /// 删除签名商品
        /// </summary>
        /// <param name="recordId">记录id</param>
        /// <returns></returns>
        public static bool DeleteSignProduct(int recordId)
        {
            return BrnShop.Core.BSPData.RDBS.DeleteSignProduct(recordId);
        }

        /// <summary>
        /// 是否存在签名商品
        /// </summary>
        /// <param name="sign">签名</param>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static bool IsExistSignProduct(string sign, int pid)
        {
            return BrnShop.Core.BSPData.RDBS.IsExistSignProduct(sign, pid);
        }

        /// <summary>
        /// 获得签名商品列表
        /// </summary>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public static List<PartProductInfo> GetSignProductList(string sign)
        {
            List<PartProductInfo> partProductList = new List<PartProductInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetSignProductList(sign);
            while (reader.Read())
            {
                PartProductInfo partProductInfo = BuildPartProductFromReader(reader);
                partProductList.Add(partProductInfo);
            }
            reader.Close();

            return partProductList;
        }

        /// <summary>
        /// 后台获得签名商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public static DataTable AdminGetSignProductList(int pageSize, int pageNumber, string sign)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetSignProductList(pageSize, pageNumber, sign);
        }

        /// <summary>
        /// 后台获得签名商品数量
        /// </summary>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public static int AdminGetSignProductCount(string sign)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetSignProductCount(sign);
        }





        /// <summary>
        /// 获得定时商品列表
        /// </summary>
        /// <param name="type">类型(0代表需要上架定时商品,1代表需要下架定时商品)</param>
        /// <returns></returns>
        public static List<TimeProductInfo> GetTimeProductList(int type)
        {
            List<TimeProductInfo> timeProductList = new List<TimeProductInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetTimeProductList(type);
            while (reader.Read())
            {
                TimeProductInfo timeProductInfo = BuildTimeProductFromReader(reader);
                timeProductList.Add(timeProductInfo);
            }
            reader.Close();

            return timeProductList;
        }

        /// <summary>
        /// 获得定时商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="productName">商品名称</param>
        /// <returns></returns>
        public static DataTable GetTimeProductList(int pageSize, int pageNumber, string productName)
        {
            return BrnShop.Core.BSPData.RDBS.GetTimeProductList(pageSize, pageNumber, productName);
        }

        /// <summary>
        /// 获得定时商品数量
        /// </summary>
        /// <param name="productName">商品名称</param>
        /// <returns></returns>
        public static int GetTimeProductCount(string productName)
        {
            return BrnShop.Core.BSPData.RDBS.GetTimeProductCount(productName);
        }

        /// <summary>
        /// 获得定时商品
        /// </summary>
        /// <param name="recordId">记录id</param>
        /// <returns></returns>
        public static TimeProductInfo GetTimeProductByRecordId(int recordId)
        {
            TimeProductInfo timeProductInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetTimeProductByRecordId(recordId);
            if (reader.Read())
            {
                timeProductInfo = BuildTimeProductFromReader(reader);
            }

            reader.Close();
            return timeProductInfo;
        }

        /// <summary>
        /// 添加定时商品
        /// </summary>
        /// <param name="timeProductInfo">定时商品信息</param>
        public static void AddTimeProduct(TimeProductInfo timeProductInfo)
        {
            BrnShop.Core.BSPData.RDBS.AddTimeProduct(timeProductInfo);
        }

        /// <summary>
        /// 更新定时商品
        /// </summary>
        /// <param name="timeProductInfo">定时商品信息</param>
        public static void UpdateTimeProduct(TimeProductInfo timeProductInfo)
        {
            BrnShop.Core.BSPData.RDBS.UpdateTimeProduct(timeProductInfo);
        }

        /// <summary>
        /// 是否存在定时商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static bool IsExistTimeProduct(int pid)
        {
            return BrnShop.Core.BSPData.RDBS.IsExistTimeProduct(pid);
        }

        /// <summary>
        /// 删除定时商品
        /// </summary>
        /// <param name="recordId">记录id</param>
        public static void DeleteTimeProductByRecordId(int recordId)
        {
            BrnShop.Core.BSPData.RDBS.DeleteTimeProductByRecordId(recordId);
        }
    }
}
