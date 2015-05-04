using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 后台商品操作管理类
    /// </summary>
    public partial class AdminProducts : Products
    {
        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="productInfo">商品信息</param>
        /// <param name="stockNumber">库存数量</param>
        /// <param name="stockLimit">库存警戒线</param>
        /// <param name="productAttributeList">商品属性列表</param>
        /// <returns></returns>
        public static int AddProduct(ProductInfo productInfo, int stockNumber, int stockLimit, List<ProductAttributeInfo> productAttributeList)
        {
            //创建商品
            int pid = CreateProduct(productInfo);
            if (pid > 0)
            {
                //创建商品库存
                CreateProductStock(pid, stockNumber, stockLimit);
                //创建商品属性
                if (productAttributeList != null)
                {
                    foreach (ProductAttributeInfo productAttributeInfo in productAttributeList)
                    {
                        productAttributeInfo.Pid = pid;
                        CreateProductAttribute(productAttributeInfo);
                    }
                }
            }
            return pid;
        }

        /// <summary>
        /// 添加SKU
        /// </summary>
        /// <param name="productInfo">商品信息</param>
        /// <param name="productSKUItemList">商品SKU项列表</param>
        public static void AddSKU(ProductInfo productInfo, List<ProductSKUItemInfo> productSKUItemList)
        {
            List<ProductSKUItemInfo> attrList = new List<ProductSKUItemInfo>();
            List<ProductSKUItemInfo> attrValueList = new List<ProductSKUItemInfo>();

            foreach (ProductSKUItemInfo productSKUItemInfo in productSKUItemList)
            {
                if (attrValueList.Find(x => x.AttrValueId == productSKUItemInfo.AttrValueId) == null)
                {
                    attrValueList.Add(productSKUItemInfo);
                }
            }
            foreach (ProductSKUItemInfo productSKUItemInfo in attrValueList)
            {
                if (attrList.Find(x => x.AttrId == productSKUItemInfo.AttrId) == null)
                {
                    attrList.Add(productSKUItemInfo);
                }
            }

            //sku数量
            int skuCount = 1;
            for (var i = 0; i < attrList.Count; i++)
            {
                skuCount = skuCount * attrValueList.FindAll(x => x.AttrId == attrList[i].AttrId).Count;
            }


            //sku项数量
            int skuItemCount = productSKUItemList.Count / skuCount;

            //sku组id
            int skuGid = TypeHelper.StringToInt(Randoms.CreateRandomValue(8));
            while (IsExistSKUGid(skuGid))
                skuGid = TypeHelper.StringToInt(Randoms.CreateRandomValue(8));
            productInfo.SKUGid = skuGid;

            //商品原始名称
            string oName = productInfo.Name;
            //循环创建商品，商品属性，商品sku项s
            for (int i = 0; i < skuCount; i++)
            {
                //格式化商品名称
                StringBuilder pName = new StringBuilder(oName);
                for (int j = skuItemCount * i; j < skuItemCount * (i + 1); j++)
                {
                    AttributeValueInfo attributeValueInfo = AdminCategories.GetAttributeValueById(productSKUItemList[j].AttrValueId);
                    if (attributeValueInfo.IsInput == 0)
                        pName.AppendFormat(" {0}", attributeValueInfo.AttrValue);
                    else
                        pName.AppendFormat(" {0}", productSKUItemList[j].InputValue);
                }
                productInfo.Name = pName.ToString();
                int pid = AddProduct(productInfo, 0, 0, null);//创建商品及其属性
                if (pid > 0)
                {
                    //创建商品sku项
                    for (int j = skuItemCount * i; j < skuItemCount * (i + 1); j++)
                    {
                        productSKUItemList[j].Pid = pid;
                        productSKUItemList[j].SKUGid = skuGid;
                        CreateProductSKUItem(productSKUItemList[j]);
                    }
                }
            }
        }

        /// <summary>
        /// 更新商品
        /// </summary>
        /// <param name="productInfo">商品信息</param>
        /// <param name="stockNumber">库存数量</param>
        /// <param name="stockLimit">库存警戒线</param>
        public static void UpdateProduct(ProductInfo productInfo, int stockNumber, int stockLimit)
        {
            UpdateProduct(productInfo);
            UpdateProductStock(productInfo.Pid, stockNumber, stockLimit);
        }

        /// <summary>
        /// 创建商品
        /// </summary>
        /// <param name="productInfo">商品信息</param>
        /// <returns>商品id</returns>
        public static int CreateProduct(ProductInfo productInfo)
        {
            return BrnShop.Data.Products.CreateProduct(productInfo);
        }

        /// <summary>
        /// 更新商品
        /// </summary>
        /// <param name="productInfo">商品信息</param>
        public static void UpdateProduct(ProductInfo productInfo)
        {
            BrnShop.Data.Products.UpdateProduct(productInfo);
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
            return BrnShop.Data.Products.AdminGetProductListCondition(productName, cateId, brandId, state);
        }

        /// <summary>
        /// 后台获得商品列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetProductListSort(string sortColumn, string sortDirection)
        {
            return BrnShop.Data.Products.AdminGetProductListSort(sortColumn, sortDirection);
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
            return BrnShop.Data.Products.AdminGetProductList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 后台获得商品数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetProductCount(string condition)
        {
            return BrnShop.Data.Products.AdminGetProductCount(condition);
        }

        /// <summary>
        /// 后台获得商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static ProductInfo AdminGetProductById(int pid)
        {
            if (pid < 1) return null;
            return BrnShop.Data.Products.AdminGetProductById(pid);
        }

        /// <summary>
        /// 后台获得部分商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static PartProductInfo AdminGetPartProductById(int pid)
        {
            if (pid < 1) return null;
            return BrnShop.Data.Products.AdminGetPartProductById(pid);
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="pidList">商品id</param>
        public static void DeleteProductById(int[] pidList)
        {
            if (pidList != null && pidList.Length > 0)
                BrnShop.Data.Products.DeleteProductById(CommonHelper.IntArrayToString(pidList));
        }

        /// <summary>
        /// 修改商品状态
        /// </summary>
        /// <param name="pidList">商品id</param>
        /// <param name="state">商品状态</param>
        public static bool UpdateProductState(int[] pidList, ProductState state)
        {
            if (pidList != null && pidList.Length > 0)
                return BrnShop.Data.Products.UpdateProductState(CommonHelper.IntArrayToString(pidList), state);
            return false;
        }

        /// <summary>
        /// 上架商品
        /// </summary>
        /// <param name="pidList">商品id</param>
        public static bool OnSaleProductById(int[] pidList)
        {
            return UpdateProductState(pidList, ProductState.OnSale);
        }

        /// <summary>
        /// 下架商品
        /// </summary>
        /// <param name="pidList">商品id</param>
        public static bool OutSaleProductById(int[] pidList)
        {
            return UpdateProductState(pidList, ProductState.OutSale);
        }

        /// <summary>
        /// 回收商品
        /// </summary>
        /// <param name="pidList">商品id</param>
        public static bool RecycleProductById(int[] pidList)
        {
            return UpdateProductState(pidList, ProductState.RecycleBin);
        }

        /// <summary>
        /// 恢复商品
        /// </summary>
        /// <param name="pidList">商品id</param>
        public static bool RestoreProductById(int[] pidList)
        {
            return UpdateProductState(pidList, ProductState.OutSale);
        }

        /// <summary>
        /// 修改商品排序
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="displayOrder">商品排序</param>
        public static bool UpdateProductDisplayOrder(int pid, int displayOrder)
        {
            if (pid < 1) return false;
            return BrnShop.Data.Products.UpdateProductDisplayOrder(pid, displayOrder);
        }

        /// <summary>
        /// 改变商品新品状态
        /// </summary>
        /// <param name="pidList">商品id</param>
        /// <param name="isNew">是否新品</param>
        public static bool ChangeProductIsNew(int[] pidList, int isNew)
        {
            if (pidList != null && pidList.Length > 0)
                return BrnShop.Data.Products.ChangeProductIsNew(CommonHelper.IntArrayToString(pidList), isNew);
            return false;
        }

        /// <summary>
        /// 改变商品热销状态
        /// </summary>
        /// <param name="pidList">商品id</param>
        /// <param name="isHot">是否热销</param>
        public static bool ChangeProductIsHot(int[] pidList, int isHot)
        {
            if (pidList != null && pidList.Length > 0)
                return BrnShop.Data.Products.ChangeProductIsHot(CommonHelper.IntArrayToString(pidList), isHot);
            return false;
        }

        /// <summary>
        /// 改变商品精品状态
        /// </summary>
        /// <param name="pidList">商品id</param>
        /// <param name="isBest">是否精品</param>
        public static bool ChangeProductIsBest(int[] pidList, int isBest)
        {
            if (pidList != null && pidList.Length > 0)
                return BrnShop.Data.Products.ChangeProductIsBest(CommonHelper.IntArrayToString(pidList), isBest);
            return false;
        }

        /// <summary>
        /// 修改商品商城价格
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="shopPrice">商城价格</param>
        public static bool UpdateProductShopPrice(int pid, decimal shopPrice)
        {
            if (pid < 1 || shopPrice < 0M) return false;
            return BrnShop.Data.Products.UpdateProductShopPrice(pid, shopPrice);

        }

        /// <summary>
        /// 更新商品图片
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="showImg">商品图片</param>
        public static void UpdateProductShowImage(int pid, string showImg)
        {
            BrnShop.Data.Products.UpdateProductShowImage(pid, showImg);
        }

        /// <summary>
        /// 后台通过商品名称获得商品id
        /// </summary>
        /// <param name="name">商品名称</param>
        /// <returns></returns>
        public static int AdminGetProductIdByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return -1;
            return BrnShop.Data.Products.AdminGetProductIdByName(name);
        }

        /// <summary>
        /// 后台获得指定品牌商品的数量
        /// </summary>
        /// <param name="brandId">品牌id</param>
        /// <returns></returns>
        public static int AdminGetBrandProductCount(int brandId)
        {
            return BrnShop.Data.Products.AdminGetBrandProductCount(brandId);
        }

        /// <summary>
        /// 后台获得指定分类商品的数量
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        public static int AdminGetCategoryProductCount(int cateId)
        {
            return BrnShop.Data.Products.AdminGetCategoryProductCount(cateId);
        }

        /// <summary>
        /// 后台获得属性值商品的数量
        /// </summary>
        /// <param name="attrValueId">属性值id</param>
        /// <returns></returns>
        public static int AdminGetAttrValueProductCount(int attrValueId)
        {
            return BrnShop.Data.Products.AdminGetAttrValueProductCount(attrValueId);
        }




        /// <summary>
        /// 创建商品属性
        /// </summary>
        public static bool CreateProductAttribute(ProductAttributeInfo productAttributeInfo)
        {
            return BrnShop.Data.Products.CreateProductAttribute(productAttributeInfo);
        }

        /// <summary>
        /// 更新商品属性
        /// </summary>
        public static bool UpdateProductAttribute(ProductAttributeInfo productAttributeInfo)
        {
            if (productAttributeInfo.Pid < 1 || productAttributeInfo.AttrId < 1 || productAttributeInfo.AttrValueId < 0 || (productAttributeInfo.AttrValueId == 0 && string.IsNullOrWhiteSpace(productAttributeInfo.InputValue)))
                return false;
            return BrnShop.Data.Products.UpdateProductAttribute(productAttributeInfo);
        }

        /// <summary>
        /// 删除商品属性
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="attrId">属性id</param>
        /// <returns></returns>
        public static bool DeleteProductAttributeByPidAndAttrId(int pid, int attrId)
        {
            if (pid > 0 && attrId > 0)
                return BrnShop.Data.Products.DeleteProductAttributeByPidAndAttrId(pid, attrId);
            return false;
        }






        /// <summary>
        /// 创建商品sku项
        /// </summary>
        /// <param name="productSKUItemInfo">商品sku项信息</param>
        public static void CreateProductSKUItem(ProductSKUItemInfo productSKUItemInfo)
        {
            BrnShop.Data.Products.CreateProductSKUItem(productSKUItemInfo);
        }

        /// <summary>
        /// 判断sku组id是否存在
        /// </summary>
        /// <param name="skuGid">sku组id</param>
        /// <returns></returns>
        public static bool IsExistSKUGid(int skuGid)
        {
            return BrnShop.Data.Products.IsExistSKUGid(skuGid);
        }






        /// <summary>
        /// 删除商品图片
        /// </summary>
        /// <param name="pImgId">商品id</param>
        /// <returns></returns>
        public static bool DeleteProductImageById(int pImgId)
        {
            ProductImageInfo productImageInfo = GetProductImageById(pImgId);
            bool result = BrnShop.Data.Products.DeleteProductImageById(productImageInfo.Pid, pImgId);
            if (result && productImageInfo.IsMain == 1)
            {
                UpdateProductShowImage(productImageInfo.Pid, "");
            }
            return result;
        }

        /// <summary>
        /// 设置图片为商品主图
        /// </summary>
        /// <param name="pImgId">商品图片id</param>
        /// <returns></returns>
        public static bool SetProductMainImage(int pImgId)
        {
            bool result = false;
            ProductImageInfo productImageInfo = GetProductImageById(pImgId);
            if (productImageInfo != null && productImageInfo.IsMain != 1)
            {
                result = BrnShop.Data.Products.SetProductMainImage(productImageInfo.Pid, pImgId);
                if (result)
                    UpdateProductShowImage(productImageInfo.Pid, productImageInfo.ShowImg);
            }
            return result;
        }

        /// <summary>
        /// 改变商品图片排序
        /// </summary>
        /// <param name="pImgId">商品图片id</param>
        /// <param name="showImg">图片排序</param>
        /// <returns></returns>
        public static bool ChangeProductImageDisplayOrder(int pImgId, int displayOrder)
        {
            ProductImageInfo productImageInfo = GetProductImageById(pImgId);
            if (productImageInfo != null)
                return BrnShop.Data.Products.ChangeProductImageDisplayOrder(productImageInfo.Pid, pImgId, displayOrder);
            return false;
        }

        /// <summary>
        /// 创建商品图片
        /// </summary>
        public static bool CreateProductImage(ProductImageInfo productImageInfo)
        {
            return BrnShop.Data.Products.CreateProductImage(productImageInfo);
        }

        /// <summary>
        /// 获得商品图片
        /// </summary>
        /// <param name="pImgId">图片id</param>
        /// <returns></returns>
        public static ProductImageInfo GetProductImageById(int pImgId)
        {
            if (pImgId < 1) return null;
            return BrnShop.Data.Products.GetProductImageById(pImgId);
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
            return BrnShop.Data.Products.CreateProductStock(pid, number, limit);
        }

        /// <summary>
        /// 更新商品库存
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="number">商品数量</param>
        /// <param name="limit">商品库存警戒线</param>
        public static void UpdateProductStock(int pid, int number, int limit)
        {
            BrnShop.Data.Products.UpdateProductStock(pid, number, limit);
        }

        /// <summary>
        /// 更新商品库存数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="number">库存数量</param>
        /// <returns></returns>
        public static bool UpdateProductStockNumber(int pid, int number)
        {
            return BrnShop.Data.Products.UpdateProductStockNumber(pid, number);
        }





        /// <summary>
        /// 创建商品关键词
        /// </summary>
        /// <param name="productKeywordInfo">商品关键词信息</param>
        public static void CreateProductKeyword(ProductKeywordInfo productKeywordInfo)
        {
            BrnShop.Data.Products.CreateProductKeyword(productKeywordInfo);
        }

        /// <summary>
        /// 更新商品关键词
        /// </summary>
        /// <param name="productKeywordInfo">商品关键词信息</param>
        public static void UpdateProductKeyword(ProductKeywordInfo productKeywordInfo)
        {
            BrnShop.Data.Products.UpdateProductKeyword(productKeywordInfo);
        }

        /// <summary>
        /// 删除商品关键词
        /// </summary>
        /// <param name="keywordIdList">关键词id列表</param>
        public static bool DeleteProductKeyword(int[] keywordIdList)
        {
            if (keywordIdList != null && keywordIdList.Length > 0)
                return BrnShop.Data.Products.DeleteProductKeyword(CommonHelper.IntArrayToString(keywordIdList));
            return false;
        }

        /// <summary>
        /// 获得商品关键词列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static List<ProductKeywordInfo> GetProductKeywordList(int pid)
        {
            return BrnShop.Data.Products.GetProductKeywordList(pid);
        }

        /// <summary>
        /// 是否存在商品关键词
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public static bool IsExistProductKeyWord(int pid, string keyword)
        {
            return BrnShop.Data.Products.IsExistProductKeyword(pid, keyword);
        }

        /// <summary>
        /// 更新商品关键词的相关性
        /// </summary>
        /// <param name="keywordId">关键词id</param>
        /// <param name="relevancy">相关性</param>
        /// <returns></returns>
        public static bool UpdateProductKeywordRelevancy(int keywordId, int relevancy)
        {
            return BrnShop.Data.Products.UpdateProductKeywordRelevancy(keywordId, relevancy);
        }





        /// <summary>
        /// 添加关联商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="relatePid">关联商品id</param>
        public static void AddRelateProduct(int pid, int relatePid)
        {
            BrnShop.Data.Products.AddRelateProduct(pid, relatePid);
        }

        /// <summary>
        /// 删除关联商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="relatePid">关联商品id</param>
        /// <returns></returns>
        public static bool DeleteRelateProductByPidAndRelatePid(int pid, int relatePid)
        {
            return BrnShop.Data.Products.DeleteRelateProductByPidAndRelatePid(pid, relatePid);
        }

        /// <summary>
        /// 关联商品是否已经存在
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="relatePid">关联商品id</param>
        public static bool IsExistRelateProduct(int pid, int relatePid)
        {
            return BrnShop.Data.Products.IsExistRelateProduct(pid, relatePid);
        }

        /// <summary>
        /// 后台获得关联商品列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static DataTable AdminGetRelateProductList(int pid)
        {
            return BrnShop.Data.Products.AdminGetRelateProductList(pid);
        }





        /// <summary>
        /// 添加签名商品
        /// </summary>
        /// <param name="sign">签名</param>
        /// <param name="pid">商品id</param>
        public static void AddSignProduct(string sign, int pid)
        {
            BrnShop.Data.Products.AddSignProduct(sign, pid);
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_SIGNPRODUCT_LIST + sign);
        }

        /// <summary>
        /// 删除签名商品
        /// </summary>
        /// <param name="recordId">记录id</param>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public static bool DeleteSignProduct(int recordId, string sign)
        {
            bool result = BrnShop.Data.Products.DeleteSignProduct(recordId);
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_SIGNPRODUCT_LIST + sign);
            return result;
        }

        /// <summary>
        /// 是否存在签名商品
        /// </summary>
        /// <param name="sign">签名</param>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static bool IsExistSignProduct(string sign, int pid)
        {
            if (string.IsNullOrWhiteSpace(sign))
                return false;
            return BrnShop.Data.Products.IsExistSignProduct(sign, pid);
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
            return BrnShop.Data.Products.AdminGetSignProductList(pageSize, pageNumber, sign);
        }

        /// <summary>
        /// 后台获得签名商品数量
        /// </summary>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public static int AdminGetSignProductCount(string sign)
        {
            return BrnShop.Data.Products.AdminGetSignProductCount(sign);
        }





        /// <summary>
        /// 获得定时商品列表
        /// </summary>
        /// <param name="type">类型(0代表需要上架定时商品,1代表需要下架定时商品)</param>
        /// <returns></returns>
        public static List<TimeProductInfo> GetTimeProductList(int type)
        {
            return BrnShop.Data.Products.GetTimeProductList(type);
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
            return BrnShop.Data.Products.GetTimeProductList(pageSize, pageNumber, productName);
        }

        /// <summary>
        /// 获得定时商品数量
        /// </summary>
        /// <param name="productName">商品名称</param>
        /// <returns></returns>
        public static int GetTimeProductCount(string productName)
        {
            return BrnShop.Data.Products.GetTimeProductCount(productName);
        }

        /// <summary>
        /// 获得定时商品
        /// </summary>
        /// <param name="recordId">记录id</param>
        /// <returns></returns>
        public static TimeProductInfo GetTimeProductByRecordId(int recordId)
        {
            return BrnShop.Data.Products.GetTimeProductByRecordId(recordId);
        }

        /// <summary>
        /// 添加定时商品
        /// </summary>
        /// <param name="timeProductInfo">定时商品信息</param>
        public static void AddTimeProduct(TimeProductInfo timeProductInfo)
        {
            BrnShop.Data.Products.AddTimeProduct(timeProductInfo);
        }

        /// <summary>
        /// 更新定时商品
        /// </summary>
        /// <param name="timeProductInfo">定时商品信息</param>
        public static void UpdateTimeProduct(TimeProductInfo timeProductInfo)
        {
            BrnShop.Data.Products.UpdateTimeProduct(timeProductInfo);
        }

        /// <summary>
        /// 是否存在定时商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static bool IsExistTimeProduct(int pid)
        {
            if (pid < 1)
                return false;
            return BrnShop.Data.Products.IsExistTimeProduct(pid);
        }

        /// <summary>
        /// 删除定时商品
        /// </summary>
        /// <param name="recordId">记录id</param>
        public static void DeleteTimeProductByRecordId(int recordId)
        {
            BrnShop.Data.Products.DeleteTimeProductByRecordId(recordId);
        }
    }
}
