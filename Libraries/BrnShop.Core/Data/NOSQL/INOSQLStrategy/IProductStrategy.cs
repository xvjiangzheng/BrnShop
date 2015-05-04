using System;
using System.Collections.Generic;

namespace BrnShop.Core
{
    /// <summary>
    /// BrnShop非关系型数据库策略之商品接口
    /// </summary>
    public partial interface IProductNOSQLStrategy
    {
        #region 商品

        /// <summary>
        /// 创建商品
        /// </summary>
        /// <param name="productInfo">商品信息</param>
        void CreateProduct(ProductInfo productInfo);

        /// <summary>
        /// 更新商品
        /// </summary>
        /// <param name="productInfo">商品信息</param>
        void UpdateProduct(ProductInfo productInfo);

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="pidList">商品id</param>
        void DeleteProductById(string pidList);

        /// <summary>
        /// 获得商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        ProductInfo GetProductById(int pid);

        /// <summary>
        /// 获得部分商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        PartProductInfo GetPartProductById(int pid);

        /// <summary>
        /// 修改商品状态
        /// </summary>
        /// <param name="pidList">商品id</param>
        /// <param name="state">商品状态</param>
        void UpdateProductState(string pidList, ProductState state);

        /// <summary>
        /// 修改商品排序
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="displayOrder">商品排序</param>
        void UpdateProductDisplayOrder(int pid, int displayOrder);

        /// <summary>
        /// 改变商品新品状态
        /// </summary>
        /// <param name="pidList">商品id</param>
        /// <param name="isNew">是否新品</param>
        void ChangeProductIsNew(string pidList, int isNew);

        /// <summary>
        /// 改变商品热销状态
        /// </summary>
        /// <param name="pidList">商品id</param>
        /// <param name="isHot">是否热销</param>
        void ChangeProductIsHot(string pidList, int isHot);

        /// <summary>
        /// 改变商品精品状态
        /// </summary>
        /// <param name="pidList">商品id</param>
        /// <param name="isBest">是否精品</param>
        void ChangeProductIsBest(string pidList, int isBest);

        /// <summary>
        /// 修改商品商城价格
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="shopPrice">商城价格</param>
        void UpdateProductShopPrice(int pid, decimal shopPrice);

        /// <summary>
        /// 更新商品图片
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="showImg">商品图片</param>
        void UpdateProductShowImage(int pid, string showImg);

        /// <summary>
        /// 获得部分商品列表
        /// </summary>
        /// <param name="pidList">商品id列表</param>
        /// <returns></returns>
        List<PartProductInfo> GetPartProductList(List<string> pidList);

        /// <summary>
        /// 更新商品的影子访问数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="visitCount">访问数量</param>
        void UpdateProductShadowVisitCount(int pid, int visitCount);

        /// <summary>
        /// 增加商品的影子销售数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="saleCount">销售数量</param>
        void AddProductShadowSaleCount(int pid, int saleCount);

        /// <summary>
        /// 增加商品的影子评价数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="starType">星星类型</param>
        void AddProductShadowReviewCount(int pid, int starType);

        #endregion

        #region 商品属性

        /// <summary>
        /// 清空商品属性
        /// </summary>
        /// <param name="pid">商品id</param>
        void ClearProductAttribute(int pid);

        /// <summary>
        /// 获得扩展商品属性列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        List<ExtProductAttributeInfo> GetExtProductAttributeList(int pid);

        /// <summary>
        /// 创建扩展商品属性列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="extProductAttributeList">扩展商品属性列表</param>
        void CreateExtProductAttributeList(int pid, List<ExtProductAttributeInfo> extProductAttributeList);

        #endregion

        #region 商品SKU

        /// <summary>
        /// 清空商品sku
        /// </summary>
        /// <param name="skuGid">sku组id</param>
        void ClearProductSKU(int skuGid);

        /// <summary>
        /// 获得商品的sku列表
        /// </summary>
        /// <param name="skuGid">sku组id</param>
        /// <returns></returns>
        List<ExtProductSKUItemInfo> GetProductSKUListBySKUGid(int skuGid);

        /// <summary>
        /// 创建商品的sku列表
        /// </summary>
        /// <param name="skuGid">sku组id</param>
        /// <param name="productSKUList">商品的sku列表</param>
        void CreateProductSKUList(int skuGid, List<ExtProductSKUItemInfo> productSKUList);

        #endregion

        #region 商品图片

        /// <summary>
        /// 清空商品图片
        /// </summary>
        /// <param name="pid">商品id</param>
        void ClearProductImage(int pid);

        /// <summary>
        /// 获得商品图片列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        List<ProductImageInfo> GetProductImageList(int pid);

        /// <summary>
        /// 创建商品图片列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="productImageList">商品图片列表</param>
        void CreateProductImageList(int pid, List<ProductImageInfo> productImageList);

        #endregion

        #region 商品库存

        /// <summary>
        /// 清空商品库存数量
        /// </summary>
        /// <param name="pid">商品id</param>
        void ClearProductStockNumber(int pid);

        /// <summary>
        /// 创建商品库存数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="number">商品库存数量</param>
        void CreateProductStockNumber(int pid, int number);

        /// <summary>
        /// 获得商品库存数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns>如果商品库存数量已经存在于数据库中则直接返回库存数量，否则返回-1</returns>
        int GetProductStockNumberByPid(int pid);

        /// <summary>
        /// 增加商品库存数量
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        void IncreaseProductStockNumber(List<OrderProductInfo> orderProductList);

        /// <summary>
        /// 减少商品库存数量
        /// </summary>
        /// <param name="orderProductList">订单商品列表</param>
        void DecreaseProductStockNumber(List<OrderProductInfo> orderProductList);

        #endregion

        #region  关联商品

        /// <summary>
        /// 获得关联商品id列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        List<string> GetRelatePidList(int pid);

        /// <summary>
        /// 创建关联商品id列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="relatePidList">关联商品id列表</param>
        void CreateRelatePidList(int pid, List<string> relatePidList);

        /// <summary>
        /// 清空关联商品id列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        void ClearRelatePidList(int pid);

        #endregion
    }
}
