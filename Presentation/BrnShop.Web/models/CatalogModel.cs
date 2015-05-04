using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;

namespace BrnShop.Web.Models
{
    /// <summary>
    /// 商品模型类
    /// </summary>
    public class ProductModel
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public int Pid { get; set; }
        /// <summary>
        /// 商品信息
        /// </summary>
        public ProductInfo ProductInfo { get; set; }
        /// <summary>
        /// 商品分类
        /// </summary>
        public CategoryInfo CategoryInfo { get; set; }
        /// <summary>
        /// 商品品牌
        /// </summary>
        public BrandInfo BrandInfo { get; set; }
        /// <summary>
        /// 商品图片列表
        /// </summary>
        public List<ProductImageInfo> ProductImageList { get; set; }
        /// <summary>
        /// 扩展商品属性列表
        /// </summary>
        public List<ExtProductAttributeInfo> ExtProductAttributeList { get; set; }
        /// <summary>
        /// 商品SKU列表
        /// </summary>
        public List<ExtProductSKUItemInfo> ProductSKUList { get; set; }
        /// <summary>
        /// 商品库存数量
        /// </summary>
        public int StockNumber { get; set; }
        /// <summary>
        /// 单品促销活动
        /// </summary>
        public SinglePromotionInfo SinglePromotionInfo { get; set; }
        /// <summary>
        /// 买送促销活动列表
        /// </summary>
        public List<BuySendPromotionInfo> BuySendPromotionList { get; set; }
        /// <summary>
        /// 赠品促销活动
        /// </summary>
        public GiftPromotionInfo GiftPromotionInfo { get; set; }
        /// <summary>
        /// 扩展赠品列表
        /// </summary>
        public List<ExtGiftInfo> ExtGiftList { get; set; }
        /// <summary>
        /// 套装商品列表
        /// </summary>
        public List<KeyValuePair<SuitPromotionInfo, List<ExtSuitProductInfo>>> SuitProductList { get; set; }
        /// <summary>
        /// 满赠促销活动
        /// </summary>
        public FullSendPromotionInfo FullSendPromotionInfo { get; set; }
        /// <summary>
        /// 满减促销活动
        /// </summary>
        public FullCutPromotionInfo FullCutPromotionInfo { get; set; }
        /// <summary>
        /// 广告语
        /// </summary>
        public string Slogan { get; set; }
        /// <summary>
        /// 商品促销信息
        /// </summary>
        public string PromotionMsg { get; set; }
        /// <summary>
        /// 商品折扣价格
        /// </summary>
        public decimal DiscountPrice { get; set; }
        /// <summary>
        /// 关联商品列表
        /// </summary>
        public List<PartProductInfo> RelateProductList { get; set; }
        /// <summary>
        /// 用户浏览历史
        /// </summary>
        public List<PartProductInfo> UserBrowseHistory { get; set; }
        /// <summary>
        /// 商品咨询类型列表
        /// </summary>
        public ProductConsultTypeInfo[] ProductConsultTypeList { get; set; }
    }

    /// <summary>
    /// 套装模型类
    /// </summary>
    public class SuitModel
    {
        /// <summary>
        /// 套装促销信息
        /// </summary>
        public SuitPromotionInfo SuitPromotionInfo { get; set; }
        /// <summary>
        /// 扩展套装商品列表
        /// </summary>
        public List<ExtSuitProductInfo> SuitProductList { get; set; }
        /// <summary>
        /// 套装折扣
        /// </summary>
        public int SuitDiscount { get; set; }
        /// <summary>
        /// 商品合计
        /// </summary>
        public decimal ProductAmount { get; set; }
        /// <summary>
        /// 套装合计
        /// </summary>
        public decimal SuitAmount { get; set; }
    }

    /// <summary>
    /// 分类模型类
    /// </summary>
    public class CategoryModel
    {
        /// <summary>
        /// 分类id
        /// </summary>
        public int CateId { get; set; }
        /// <summary>
        /// 品牌id
        /// </summary>
        public int BrandId { get; set; }
        /// <summary>
        /// 筛选价格
        /// </summary>
        public int FilterPrice { get; set; }
        /// <summary>
        /// 筛选属性
        /// </summary>
        public string FilterAttr { get; set; }
        /// <summary>
        /// 是否只显示有货
        /// </summary>
        public int OnlyStock { get; set; }
        /// <summary>
        /// 排序列
        /// </summary>
        public int SortColumn { get; set; }
        /// <summary>
        /// 排序方向
        /// </summary>
        public int SortDirection { get; set; }
        /// <summary>
        /// 分类信息
        /// </summary>
        public CategoryInfo CategoryInfo { get; set; }
        /// <summary>
        /// 品牌列表
        /// </summary>
        public List<BrandInfo> BrandList { get; set; }
        /// <summary>
        /// 分类价格范围列表
        /// </summary>
        public string[] CatePriceRangeList { get; set; }
        /// <summary>
        /// 属性及其值列表
        /// </summary>
        public List<KeyValuePair<AttributeInfo, List<AttributeValueInfo>>> AAndVList { get; set; }
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<PartProductInfo> ProductList { get; set; }
    }

    /// <summary>
    /// 搜索模型类
    /// </summary>
    public class SearchModel
    {
        /// <summary>
        /// 搜索词
        /// </summary>
        public string Word { get; set; }
        /// <summary>
        /// 分类id
        /// </summary>
        public int CateId { get; set; }
        /// <summary>
        /// 品牌id
        /// </summary>
        public int BrandId { get; set; }
        /// <summary>
        /// 筛选价格
        /// </summary>
        public int FilterPrice { get; set; }
        /// <summary>
        /// 筛选属性
        /// </summary>
        public string FilterAttr { get; set; }
        /// <summary>
        /// 是否只显示有货
        /// </summary>
        public int OnlyStock { get; set; }
        /// <summary>
        /// 排序列
        /// </summary>
        public int SortColumn { get; set; }
        /// <summary>
        /// 排序方向
        /// </summary>
        public int SortDirection { get; set; }
        /// <summary>
        /// 分类列表
        /// </summary>
        public List<CategoryInfo> CategoryList { get; set; }
        /// <summary>
        /// 分类信息
        /// </summary>
        public CategoryInfo CategoryInfo { get; set; }
        /// <summary>
        /// 品牌列表
        /// </summary>
        public List<BrandInfo> BrandList { get; set; }
        /// <summary>
        /// 分类价格范围列表
        /// </summary>
        public string[] CatePriceRangeList { get; set; }
        /// <summary>
        /// 属性及其值列表
        /// </summary>
        public List<KeyValuePair<AttributeInfo, List<AttributeValueInfo>>> AAndVList { get; set; }
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<PartProductInfo> ProductList { get; set; }
    }

    /// <summary>
    /// 商品评价列表模型类
    /// </summary>
    public class ProductReviewListModel
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public PartProductInfo ProductInfo { get; set; }
        /// <summary>
        /// 商品分类
        /// </summary>
        public CategoryInfo CategoryInfo { get; set; }
        /// <summary>
        /// 商品品牌
        /// </summary>
        public BrandInfo BrandInfo { get; set; }
        /// <summary>
        /// 评价类型
        /// </summary>
        public int ReviewType { get; set; }
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 商品评价列表
        /// </summary>
        public DataTable ProductReviewList { get; set; }
    }

    /// <summary>
    /// 商品评价列表模型类
    /// </summary>
    public class AjaxProductReviewListModel
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public int Pid { get; set; }
        /// <summary>
        /// 评价类型
        /// </summary>
        public int ReviewType { get; set; }
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 商品评价列表
        /// </summary>
        public DataTable ProductReviewList { get; set; }
    }

    /// <summary>
    /// 商品咨询列表模型类
    /// </summary>
    public class ProductConsultListModel
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public PartProductInfo ProductInfo { get; set; }
        /// <summary>
        /// 商品分类
        /// </summary>
        public CategoryInfo CategoryInfo { get; set; }
        /// <summary>
        /// 商品品牌
        /// </summary>
        public BrandInfo BrandInfo { get; set; }
        /// <summary>
        /// 咨询类型id
        /// </summary>
        public int ConsultTypeId { get; set; }
        /// <summary>
        /// 咨询信息
        /// </summary>
        public string ConsultMessage { get; set; }
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 商品咨询列表
        /// </summary>
        public List<ProductConsultInfo> ProductConsultList { get; set; }
        /// <summary>
        /// 商品咨询类型列表
        /// </summary>
        public ProductConsultTypeInfo[] ProductConsultTypeList { get; set; }
        /// <summary>
        /// 是否显示验证码
        /// </summary>
        public bool IsVerifyCode { get; set; }
    }

    /// <summary>
    /// 商品咨询列表模型类
    /// </summary>
    public class AjaxProductConsultListModel
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public int Pid { get; set; }
        /// <summary>
        /// 咨询类型id
        /// </summary>
        public int ConsultTypeId { get; set; }
        /// <summary>
        /// 咨询信息
        /// </summary>
        public string ConsultMessage { get; set; }
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 商品咨询列表
        /// </summary>
        public List<ProductConsultInfo> ProductConsultList { get; set; }
        /// <summary>
        /// 商品咨询类型列表
        /// </summary>
        public ProductConsultTypeInfo[] ProductConsultTypeList { get; set; }
        /// <summary>
        /// 是否显示验证码
        /// </summary>
        public bool IsVerifyCode { get; set; }
    }
}