using System;
using System.Data;
using System.Collections.Generic;

namespace BrnShop.Core
{
    /// <summary>
    /// BrnShop关系数据库策略之商品分部接口
    /// </summary>
    public partial interface IRDBSStrategy
    {
        #region 品牌

        /// <summary>
        /// 获得品牌
        /// </summary>
        /// <param name="brandId">品牌id</param>
        /// <returns></returns>
        IDataReader GetBrandById(int brandId);

        /// <summary>
        /// 创建品牌
        /// </summary>
        void CreateBrand(BrandInfo brandInfo);

        /// <summary>
        /// 删除品牌
        /// </summary>
        /// <param name="brandId">品牌id</param>
        void DeleteBrandById(int brandId);

        /// <summary>
        /// 更新品牌
        /// </summary>
        void UpdateBrand(BrandInfo brandInfo);

        /// <summary>
        /// 后台获得品牌列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        DataTable AdminGetBrandList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 后台获得品牌选择列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        DataTable AdminGetBrandSelectList(int pageSize, int pageNumber, string condition);

        /// <summary>
        /// 后台获得列表搜索条件
        /// </summary>
        /// <param name="brandName">品牌名称</param>
        /// <returns></returns>
        string AdminGetBrandListCondition(string brandName);

        /// <summary>
        /// 后台获得列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        string AdminGetBrandListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 后台获得品牌数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int AdminGetBrandCount(string condition);

        /// <summary>
        /// 根据品牌名称得到品牌id
        /// </summary>
        /// <param name="brandName">品牌名称</param>
        /// <returns></returns>
        int GetBrandIdByName(string brandName);

        /// <summary>
        /// 获得品牌关联的分类
        /// </summary>
        /// <param name="brandId">品牌id</param>
        /// <returns></returns>
        IDataReader GetBrandCategoryList(int brandId);

        /// <summary>
        /// 获得品牌列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="brandName">品牌名称</param>
        /// <returns></returns>
        IDataReader GetBrandList(int pageSize, int pageNumber, string brandName);

        /// <summary>
        /// 获得品牌数量
        /// </summary>
        /// <param name="brandName">品牌名称</param>
        /// <returns></returns>
        int GetBrandCount(string brandName);

        #endregion

        #region 分类

        /// <summary>
        /// 获得分类列表
        /// </summary>
        /// <returns></returns>
        IDataReader GetCategoryList();

        /// <summary>
        /// 创建分类
        /// </summary>
        int CreateCategory(CategoryInfo categoryInfo);

        /// <summary>
        /// 更新分类
        /// </summary>
        void UpdateCategory(CategoryInfo categoryInfo);

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="cateId">分类id</param>
        void DeleteCategoryById(int cateId);

        /// <summary>
        /// 获得分类关联的品牌
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        IDataReader GetCategoryBrandList(int cateId);

        #endregion

        #region 属性分组

        /// <summary>
        /// 获得属性分组
        /// </summary>
        /// <param name="attrGroupId">属性分组id</param>
        IDataReader GetAttributeGroupById(int attrGroupId);

        /// <summary>
        /// 创建属性分组
        /// </summary>
        void CreateAttributeGroup(AttributeGroupInfo attributeGroupInfo);

        /// <summary>
        /// 删除属性分组
        /// </summary>
        /// <param name="attrGroupId">属性分组id</param>
        void DeleteAttributeGroupById(int attrGroupId);

        /// <summary>
        /// 更新属性分组
        /// </summary>
        /// <param name="newAttributeGroupInfo">新属性分组</param>
        /// <param name="oldAttributeGroupInfo">原属性分组</param>
        void UpdateAttributeGroup(AttributeGroupInfo attributeGroupInfo);

        /// <summary>
        /// 获得分类的属性分组列表
        /// </summary>
        /// <param name="cateId">The cate id.</param>
        /// <returns></returns>
        IDataReader GetAttributeGroupListByCateId(int cateId);

        /// <summary>
        /// 通过分类id和属性分组名称获得分组id
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <param name="name">分组名称</param>
        /// <returns></returns>
        int GetAttrGroupIdByCateIdAndName(int cateId, string name);

        #endregion

        #region 属性

        /// <summary>
        /// 获得属性
        /// </summary>
        /// <param name="attrId">属性id</param>
        /// <returns></returns>
        IDataReader GetAttributeById(int attrId);

        /// <summary>
        /// 创建属性
        /// </summary>
        /// <param name="attributeInfo">属性信息</param>
        /// <param name="attrGroupId">属性组id</param>
        /// <param name="attrGroupName">属性组名称</param>
        /// <param name="attrGroupDisplayOrder">属性组排序</param>
        void CreateAttribute(AttributeInfo attributeInfo, int attrGroupId, string attrGroupName, int attrGroupDisplayOrder);

        /// <summary>
        /// 更新属性
        /// </summary>
        /// <param name="newAttributeInfo">新属性</param>
        /// <param name="oldAttributeInfo">原属性</param>
        void UpdateAttribute(AttributeInfo attributeInfo);

        /// <summary>
        /// 删除属性
        /// </summary>
        /// <param name="attrId">属性id</param>
        void DeleteAttributeById(int attrId);

        /// <summary>
        /// 后台获得属性列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        DataTable AdminGetAttributeList(int cateId, string sort);

        /// <summary>
        /// 后台获得属性列表排序
        /// </summary>
        /// <param name="sortColumn">排序字段</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        string AdminGetAttributeListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 获得属性列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        IDataReader GetAttributeListByCateId(int cateId);

        /// <summary>
        /// 获得属性列表
        /// </summary>
        /// <param name="attrGroupId">属性分组id</param>
        /// <returns></returns>
        IDataReader GetAttributeListByAttrGroupId(int attrGroupId);

        /// <summary>
        /// 获得筛选属性列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        IDataReader GetFilterAttributeListByCateId(int cateId);

        /// <summary>
        /// 通过分类id和属性名称获得属性id
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <param name="attributeName">属性名称</param>
        /// <returns></returns>
        int GetAttrIdByCateIdAndName(int cateId, string attributeName);

        #endregion

        #region 属性值

        /// <summary>
        /// 获得属性值
        /// </summary>
        /// <param name="attrValueId">属性值id</param>
        /// <returns></returns>
        IDataReader GetAttributeValueById(int attrValueId);

        /// <summary>
        /// 创建属性值
        /// </summary>
        void CreateAttributeValue(AttributeValueInfo attributeValueInfo);

        /// <summary>
        /// 更新属性值
        /// </summary>
        void UpdateAttributeValue(AttributeValueInfo attributeValueInfo);

        /// <summary>
        /// 删除属性值
        /// </summary>
        /// <param name="attrValueId">属性值id</param>
        void DeleteAttributeValueById(int attrValueId);

        /// <summary>
        /// 获得属性值列表
        /// </summary>
        /// <param name="attrId">属性id</param>
        /// <returns></returns>
        IDataReader GetAttributeValueListByAttrId(int attrId);

        /// <summary>
        /// 通过属性id和属性值获得属性值的id
        /// </summary>
        /// <param name="attrId">属性id</param>
        /// <param name="attrValue">属性值</param>
        /// <returns></returns>
        int GetAttributeValueIdByAttrIdAndValue(int attrId, string attrValue);

        #endregion

        #region 商品

        /// <summary>
        /// 后台获得商品列表条件
        /// </summary>
        /// <param name="productName">商品名称</param>
        /// <param name="cateId">分类id</param>
        /// <param name="brandId">品牌id</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        string AdminGetProductListCondition(string productName, int cateId, int brandId, int state);

        /// <summary>
        /// 后台获得商品列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        string AdminGetProductListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 后台获得商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        DataTable AdminGetProductList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 后台获得商品数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int AdminGetProductCount(string condition);

        /// <summary>
        /// 创建商品
        /// </summary>
        /// <param name="productInfo">商品信息</param>
        /// <returns>商品id</returns>
        int CreateProduct(ProductInfo productInfo);

        /// <summary>
        /// 更新商品
        /// </summary>
        void UpdateProduct(ProductInfo productInfo);

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="pidList">商品id</param>
        void DeleteProductById(string pidList);

        /// <summary>
        /// 后台获得商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        IDataReader AdminGetProductById(int pid);

        /// <summary>
        /// 后台获得部分商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        IDataReader AdminGetPartProductById(int pid);

        /// <summary>
        /// 获得商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        IDataReader GetProductById(int pid);

        /// <summary>
        /// 获得部分商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        IDataReader GetPartProductById(int pid);

        /// <summary>
        /// 修改商品状态
        /// </summary>
        /// <param name="pidList">商品id</param>
        /// <param name="state">商品状态</param>
        bool UpdateProductState(string pidList, ProductState state);

        /// <summary>
        /// 修改商品排序
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="displayOrder">商品排序</param>
        bool UpdateProductDisplayOrder(int pid, int displayOrder);

        /// <summary>
        /// 改变商品新品状态
        /// </summary>
        /// <param name="pidList">商品id</param>
        /// <param name="isNew">是否新品</param>
        bool ChangeProductIsNew(string pidList, int isNew);

        /// <summary>
        /// 改变商品热销状态
        /// </summary>
        /// <param name="pidList">商品id</param>
        /// <param name="isHot">是否热销</param>
        bool ChangeProductIsHot(string pidList, int isHot);

        /// <summary>
        /// 改变商品精品状态
        /// </summary>
        /// <param name="pidList">商品id</param>
        /// <param name="isBest">是否精品</param>
        bool ChangeProductIsBest(string pidList, int isBest);

        /// <summary>
        /// 修改商品商城价格
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="shopPrice">商城价格</param>
        bool UpdateProductShopPrice(int pid, decimal shopPrice);

        /// <summary>
        /// 更新商品图片
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="showImg">商品图片</param>
        void UpdateProductShowImage(int pid, string showImg);

        /// <summary>
        /// 后台通过商品名称获得商品id
        /// </summary>
        /// <param name="name">商品名称</param>
        /// <returns></returns>
        int AdminGetProductIdByName(string name);

        /// <summary>
        /// 获得部分商品列表
        /// </summary>
        /// <param name="pidList">商品id列表</param>
        /// <returns></returns>
        IDataReader GetPartProductList(string pidList);

        /// <summary>
        /// 获得商品的影子访问数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        int GetProductShadowVisitCountById(int pid);

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
        IDataReader GetCategoryProductList(int pageSize, int pageNumber, int cateId, int brandId, int filterPrice, string[] catePriceRangeList, List<int> attrValueIdList, int onlyStock, int sortColumn, int sortDirection);

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
        int GetCategoryProductCount(int cateId, int brandId, int filterPrice, string[] catePriceRangeList, List<int> attrValueIdList, int onlyStock);

        /// <summary>
        /// 获得商品汇总列表
        /// </summary>
        /// <param name="pidList">商品id列表</param>
        /// <returns></returns>
        DataTable GetProductSummaryList(string pidList);

        /// <summary>
        /// 后台获得指定品牌商品的数量
        /// </summary>
        /// <param name="brandId">品牌id</param>
        /// <returns></returns>
        int AdminGetBrandProductCount(int brandId);

        /// <summary>
        /// 后台获得指定分类商品的数量
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        int AdminGetCategoryProductCount(int cateId);

        /// <summary>
        /// 后台获得属性值商品的数量
        /// </summary>
        /// <param name="attrValueId">属性值id</param>
        /// <returns></returns>
        int AdminGetAttrValueProductCount(int attrValueId);

        #endregion

        #region 商品属性

        /// <summary>
        /// 创建商品属性
        /// </summary>
        bool CreateProductAttribute(ProductAttributeInfo productAttributeInfo);

        /// <summary>
        /// 更新商品属性
        /// </summary>
        bool UpdateProductAttribute(ProductAttributeInfo productAttributeInfo);

        /// <summary>
        /// 删除商品属性
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="attrId">属性id</param>
        /// <returns></returns>
        bool DeleteProductAttributeByPidAndAttrId(int pid, int attrId);

        /// <summary>
        /// 获得商品属性
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="attrId">属性id</param>
        /// <returns></returns>
        IDataReader GetProductAttributeByPidAndAttrId(int pid, int attrId);

        /// <summary>
        /// 获得商品属性列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        IDataReader GetProductAttributeList(int pid);

        /// <summary>
        /// 获得扩展商品属性列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        IDataReader GetExtProductAttributeList(int pid);

        #endregion

        #region 商品SKU

        /// <summary>
        /// 创建商品sku项
        /// </summary>
        /// <param name="productSKUInfo">商品sku项信息</param>
        void CreateProductSKUItem(ProductSKUItemInfo productSKUInfo);

        /// <summary>
        /// 获得商品的sku项列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        DataTable GetProductSKUItemList(int pid);

        /// <summary>
        /// 判断sku组id是否存在
        /// </summary>
        /// <param name="skuGid">sku组id</param>
        /// <returns></returns>
        bool IsExistSKUGid(int skuGid);

        /// <summary>
        /// 获得商品的sku列表
        /// </summary>
        /// <param name="skuGid">sku组id</param>
        /// <returns></returns>
        IDataReader GetProductSKUListBySKUGid(int skuGid);

        #endregion

        #region  商品图片

        /// <summary>
        /// 创建商品图片
        /// </summary>
        bool CreateProductImage(ProductImageInfo productImageInfo);

        /// <summary>
        /// 获得商品图片
        /// </summary>
        /// <param name="pImgId">图片id</param>
        /// <returns></returns>
        IDataReader GetProductImageById(int pImgId);

        /// <summary>
        /// 删除商品图片
        /// </summary>
        /// <param name="pImgId">图片id</param>
        /// <returns></returns>
        bool DeleteProductImageById(int pImgId);

        /// <summary>
        /// 设置图片为商品主图
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="pImgId">商品图片id</param>
        /// <returns></returns>
        bool SetProductMainImage(int pid, int pImgId);

        /// <summary>
        /// 改变商品图片排序
        /// </summary>
        /// <param name="pImgId">商品图片id</param>
        /// <param name="showImg">图片排序</param>
        /// <returns></returns>
        bool ChangeProductImageDisplayOrder(int pImgId, int displayOrder);

        /// <summary>
        /// 获得商品图片列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        IDataReader GetProductImageList(int pid);

        #endregion

        #region 商品库存

        /// <summary>
        /// 获得商品库存
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        IDataReader GetProductStockByPid(int pid);

        /// <summary>
        /// 创建商品库存
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="number">商品数量</param>
        /// <param name="limit">商品库存警戒线</param>
        /// <returns></returns>
        bool CreateProductStock(int pid, int number, int limit);

        /// <summary>
        /// 更新商品库存
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="number">商品数量</param>
        /// <param name="limit">商品库存警戒线</param>
        void UpdateProductStock(int pid, int number, int limit);

        /// <summary>
        /// 更新商品库存数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="number">库存数量</param>
        /// <returns></returns>
        bool UpdateProductStockNumber(int pid, int number);

        /// <summary>
        /// 获得商品库存数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
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

        /// <summary>
        /// 获得商品库存列表
        /// </summary>
        /// <param name="pidList">商品id列表</param>
        /// <returns></returns>
        IDataReader GetProductStockList(string pidList);

        #endregion

        #region 商品关键词

        /// <summary>
        /// 创建商品关键词
        /// </summary>
        /// <param name="productKeywordInfo">商品关键词信息</param>
        void CreateProductKeyword(ProductKeywordInfo productKeywordInfo);

        /// <summary>
        /// 更新商品关键词
        /// </summary>
        /// <param name="productKeywordInfo">商品关键词信息</param>
        void UpdateProductKeyword(ProductKeywordInfo productKeywordInfo);

        /// <summary>
        /// 删除商品关键词
        /// </summary>
        /// <param name="keywordIdList">关键词id列表</param>
        bool DeleteProductKeyword(string keywordIdList);

        /// <summary>
        /// 获得商品关键词列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        IDataReader GetProductKeywordList(int pid);

        /// <summary>
        /// 是否存在商品关键词
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        bool IsExistProductKeyword(int pid, string keyword);

        /// <summary>
        /// 更新商品关键词的相关性
        /// </summary>
        /// <param name="keywordId">关键词id</param>
        /// <param name="relevancy">相关性</param>
        /// <returns></returns>
        bool UpdateProductKeywordRelevancy(int keywordId, int relevancy);

        #endregion

        #region 关联商品

        /// <summary>
        /// 添加关联商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="relatePid">关联商品id</param>
        void AddRelateProduct(int pid, int relatePid);

        /// <summary>
        /// 删除关联商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="relatePid">关联商品id</param>
        /// <returns></returns>
        bool DeleteRelateProductByPidAndRelatePid(int pid, int relatePid);

        /// <summary>
        /// 关联商品是否已经存在
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="relatePid">关联商品id</param>
        bool IsExistRelateProduct(int pid, int relatePid);

        /// <summary>
        /// 后台获得关联商品列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        DataTable AdminGetRelateProductList(int pid);

        /// <summary>
        /// 获得关联商品列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        IDataReader GetRelateProductList(int pid);

        #endregion

        #region 签名商品

        /// <summary>
        /// 添加签名商品
        /// </summary>
        /// <param name="sign">签名</param>
        /// <param name="pid">商品id</param>
        void AddSignProduct(string sign, int pid);

        /// <summary>
        /// 删除签名商品
        /// </summary>
        /// <param name="recordId">记录id</param>
        /// <returns></returns>
        bool DeleteSignProduct(int recordId);

        /// <summary>
        /// 是否存在签名商品
        /// </summary>
        /// <param name="sign">签名</param>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        bool IsExistSignProduct(string sign, int pid);

        /// <summary>
        /// 获得签名商品列表
        /// </summary>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        IDataReader GetSignProductList(string sign);

        /// <summary>
        /// 后台获得签名商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        DataTable AdminGetSignProductList(int pageSize, int pageNumber, string sign);

        /// <summary>
        /// 后台获得签名商品数量
        /// </summary>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        int AdminGetSignProductCount(string sign);

        #endregion

        #region 定时商品

        /// <summary>
        /// 获得定时商品列表
        /// </summary>
        /// <param name="type">类型(0代表需要上架定时商品,1代表需要下架定时商品)</param>
        /// <returns></returns>
        IDataReader GetTimeProductList(int type);

        /// <summary>
        /// 获得定时商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="productName">商品名称</param>
        /// <returns></returns>
        DataTable GetTimeProductList(int pageSize, int pageNumber, string productName);

        /// <summary>
        /// 获得定时商品数量
        /// </summary>
        /// <param name="productName">商品名称</param>
        /// <returns></returns>
        int GetTimeProductCount(string productName);

        /// <summary>
        /// 获得定时商品
        /// </summary>
        /// <param name="recordId">记录id</param>
        /// <returns></returns>
        IDataReader GetTimeProductByRecordId(int recordId);

        /// <summary>
        /// 添加定时商品
        /// </summary>
        /// <param name="timeProductInfo">定时商品信息</param>
        void AddTimeProduct(TimeProductInfo timeProductInfo);

        /// <summary>
        /// 更新定时商品
        /// </summary>
        /// <param name="timeProductInfo">定时商品信息</param>
        void UpdateTimeProduct(TimeProductInfo timeProductInfo);

        /// <summary>
        /// 是否存在定时商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        bool IsExistTimeProduct(int pid);

        /// <summary>
        /// 删除定时商品
        /// </summary>
        /// <param name="recordId">记录id</param>
        void DeleteTimeProductByRecordId(int recordId);

        #endregion

        #region 商品统计

        /// <summary>
        /// 更新商品统计
        /// </summary>
        /// <param name="updateProductStatState">更新商品统计状态</param>
        void UpdateProductStat(UpdateProductStatState updateProductStatState);

        /// <summary>
        /// 获得商品总访问量列表
        /// </summary>
        /// <returns></returns>
        DataTable GetProductTotalVisitCountList();

        #endregion

        #region 浏览历史

        /// <summary>
        /// 获得用户浏览商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        IDataReader GetUserBrowseProductList(int pageSize, int pageNumber, int uid);

        /// <summary>
        /// 获得用户浏览商品数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        int GetUserBrowseProductCount(int uid);

        /// <summary>
        /// 更新浏览历史
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pid">商品id</param>
        /// <param name="updateTime">更新时间</param>
        void UpdateBrowseHistory(int uid, int pid, DateTime updateTime);

        /// <summary>
        /// 清空过期浏览历史
        /// </summary>
        void ClearExpiredBrowseHistory();

        #endregion

        #region 商品咨询类型

        /// <summary>
        /// 创建商品咨询类型
        /// </summary>
        void CreateProductConsultType(ProductConsultTypeInfo productConsultTypeInfo);

        /// <summary>
        /// 更新商品咨询类型
        /// </summary>
        void UpdateProductConsultType(ProductConsultTypeInfo productConsultTypeInfo);

        /// <summary>
        /// 删除商品咨询类型
        /// </summary>
        /// <param name="consultTypeId">商品咨询类型id</param>
        void DeleteProductConsultTypeById(int consultTypeId);

        /// <summary>
        /// 获得商品咨询类型列表
        /// </summary>
        /// <returns></returns>
        DataTable GetProductConsultTypeList();

        #endregion

        #region 商品咨询

        /// <summary>
        /// 咨询商品
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="consultTypeId">咨询类型id</param>
        /// <param name="consultUid">咨询用户id</param>
        /// <param name="consultTime">咨询时间</param>
        /// <param name="consultMessage">咨询内容</param>
        /// <param name="consultNickName">咨询昵称</param>
        /// <param name="pName">商品名称</param>
        /// <param name="pShowImg">商品图片</param>
        /// <param name="consultIP">咨询ip</param>
        void ConsultProduct(int pid, int consultTypeId, int consultUid, DateTime consultTime, string consultMessage, string consultNickName, string pName, string pShowImg, string consultIP);

        /// <summary>
        /// 回复商品咨询
        /// </summary>
        /// <param name="consultId">咨询id</param>
        /// <param name="replyUid">回复用户id</param>
        /// <param name="replyTime">回复时间</param>
        /// <param name="replyMessage">回复内容</param>
        /// <param name="replyNickName">回复昵称</param>
        /// <param name="replyIP">回复ip</param>
        void ReplyProductConsult(int consultId, int replyUid, DateTime replyTime, string replyMessage, string replyNickName, string replyIP);

        /// <summary>
        /// 获得商品咨询
        /// </summary>
        /// <param name="consultId">咨询id</param>
        /// <returns></returns>
        IDataReader GetProductConsultById(int consultId);

        /// <summary>
        /// 后台获得商品咨询
        /// </summary>
        /// <param name="consultId">咨询id</param>
        /// <returns></returns>
        IDataReader AdminGetProductConsultById(int consultId);

        /// <summary>
        /// 删除商品咨询
        /// </summary>
        /// <param name="consultIdList">咨询id</param>
        void DeleteProductConsultById(string consultIdList);

        /// <summary>
        /// 后台获得商品咨询列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        IDataReader AdminGetProductConsultList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 后台获得商品咨询列表条件
        /// </summary>
        /// <param name="consultTypeId">商品咨询类型id</param>
        /// <param name="pid">商品id</param>
        /// <param name="uid">用户id</param>
        /// <param name="consultMessage">咨询内容</param>
        /// <param name="consultStartTime">咨询开始时间</param>
        /// <param name="consultEndTime">咨询结束时间</param>
        /// <returns></returns>
        string AdminGetProductConsultListCondition(int consultTypeId, int pid, int uid, string consultMessage, string consultStartTime, string consultEndTime);

        /// <summary>
        /// 后台获得商品咨询列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        string AdminGetProductConsultListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 后台获得商品咨询数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int AdminGetProductConsultCount(string condition);

        /// <summary>
        /// 更新商品咨询状态
        /// </summary>
        /// <param name="consultId">咨询id</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        bool UpdateProductConsultState(int consultId, int state);

        /// <summary>
        /// 获得商品咨询列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pid">商品id</param>
        /// <param name="consultTypeId">咨询类型id</param>
        /// <param name="consultMessage">咨询内容</param>
        /// <returns></returns>
        IDataReader GetProductConsultList(int pageSize, int pageNumber, int pid, int consultTypeId, string consultMessage);

        /// <summary>
        /// 获得商品咨询数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="consultTypeId">咨询类型id</param>
        /// <param name="consultMessage">咨询内容</param>
        /// <returns></returns>
        int GetProductConsultCount(int pid, int consultTypeId, string consultMessage);

        /// <summary>
        /// 获得用户商品咨询列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        IDataReader GetUserProductConsultList(int uid, int pageSize, int pageNumber);

        /// <summary>
        /// 获得用户商品咨询数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        int GetUserProductConsultCount(int uid);

        #endregion

        #region 商品评价

        /// <summary>
        /// 获得商品评价
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <returns></returns>
        IDataReader GetProductReviewById(int reviewId);

        /// <summary>
        /// 后台获得商品评价
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <returns></returns>
        IDataReader AdminGetProductReviewById(int reviewId);

        /// <summary>
        /// 评价商品
        /// </summary>
        void ReviewProduct(ProductReviewInfo productReviewInfo);

        /// <summary>
        /// 删除商品评价
        /// </summary>
        /// <param name="reviewId">评价id</param>
        void DeleteProductReviewById(int reviewId);

        /// <summary>
        /// 后台获得商品评价列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        DataTable AdminGetProductReviewList(int pageSize, int pageNumber, string condition, string sort);

        /// <summary>
        /// 后台获得商品评价列表条件
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="message">评价内容</param>
        /// <param name="startTime">评价开始时间</param>
        /// <param name="endTime">评价结束时间</param>
        /// <returns></returns>
        string AdminGetProductReviewListCondition(int pid, string message, string startTime, string endTime);

        /// <summary>
        /// 后台获得商品评价列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        string AdminGetProductReviewListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 后台获得商品评价数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int AdminGetProductReviewCount(string condition);

        /// <summary>
        /// 后台获得商品评价回复列表
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <returns></returns>
        DataTable AdminGetProductReviewReplyList(int reviewId);

        /// <summary>
        /// 对商品评价投票
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <param name="uid">用户id</param>
        /// <param name="voteTime">投票时间</param>
        void VoteProductReview(int reviewId, int uid, DateTime voteTime);

        /// <summary>
        /// 是否对商品评价投过票
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <param name="uid">用户id</param>
        bool IsVoteProductReview(int reviewId, int uid);

        /// <summary>
        /// 更改商品评价状态
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <param name="state">评价状态</param>
        /// <returns></returns>
        bool ChangeProductReviewState(int reviewId, int state);

        /// <summary>
        /// 获得用户商品评价列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        IDataReader GetUserProductReviewList(int uid, int pageSize, int pageNumber);

        /// <summary>
        /// 获得用户商品评价数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        int GetUserProductReviewCount(int uid);

        /// <summary>
        /// 获得商品评价列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="type">类型(0代表全部评价,1代表好评,2代表中评,3代表差评)</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        DataTable GetProductReviewList(int pid, int type, int pageSize, int pageNumber);

        /// <summary>
        /// 获得商品评价数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="type">类型(0代表全部评价,1代表好评,2代表中评,3代表差评)</param>
        /// <returns></returns>
        int GetProductReviewCount(int pid, int type);

        /// <summary>
        /// 获得商品评价及其回复
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <returns></returns>
        DataTable GetProductReviewWithReplyById(int reviewId);

        /// <summary>
        /// 获得商品评价列表
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        DataTable GetProductReviewList(DateTime startTime, DateTime endTime);

        #endregion

        #region 搜索历史

        /// <summary>
        /// 更新搜索历史
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="word">搜索词</param>
        /// <param name="updateTime">更新时间</param>
        void UpdateSearchHistory(int uid, string word, DateTime updateTime);

        /// <summary>
        /// 清空过期搜索历史
        /// </summary>
        void ClearExpiredSearchHistory();

        /// <summary>
        /// 获得搜索词统计列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="word">搜索词</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        DataTable GetSearchWordStatList(int pageSize, int pageNumber, string word, string sort);

        /// <summary>
        /// 获得搜索词统计列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        string GetSearchWordStatListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 获得搜索词统计数量
        /// </summary>
        /// <param name="word">搜索词</param>
        /// <returns></returns>
        int GetSearchWordStatCount(string word);

        #endregion
    }
}
