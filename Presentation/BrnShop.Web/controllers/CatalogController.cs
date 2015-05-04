using System;
using System.Text;
using System.Data;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Models;

namespace BrnShop.Web.Controllers
{
    /// <summary>
    /// 商城目录控制器类
    /// </summary>
    public partial class CatalogController : BaseWebController
    {
        /// <summary>
        /// 商品
        /// </summary>
        public ActionResult Product()
        {
            //商品id
            int pid = GetRouteInt("pid");
            if (pid == 0)
                pid = WebHelper.GetQueryInt("pid");

            //判断商品是否存在
            ProductInfo productInfo = Products.GetProductById(pid);
            if (productInfo == null)
                return PromptView("/", "你访问的商品不存在");

            //商品存在时
            ProductModel model = new ProductModel();
            //商品id
            model.Pid = pid;
            //商品信息
            model.ProductInfo = productInfo;
            //商品分类
            model.CategoryInfo = Categories.GetCategoryById(productInfo.CateId);
            //商品品牌
            model.BrandInfo = Brands.GetBrandById(productInfo.BrandId);
            //商品图片列表
            model.ProductImageList = Products.GetProductImageList(pid);
            //扩展商品属性列表
            model.ExtProductAttributeList = Products.GetExtProductAttributeList(pid);
            //商品SKU列表
            model.ProductSKUList = Products.GetProductSKUListBySKUGid(productInfo.SKUGid);
            //商品库存数量
            model.StockNumber = Products.GetProductStockNumberByPid(pid);


            //单品促销
            model.SinglePromotionInfo = Promotions.GetSinglePromotionByPidAndTime(pid, DateTime.Now);
            //买送促销活动列表
            model.BuySendPromotionList = Promotions.GetBuySendPromotionList(pid, DateTime.Now);
            //赠品促销活动
            model.GiftPromotionInfo = Promotions.GetGiftPromotionByPidAndTime(pid, DateTime.Now);
            //赠品列表
            if (model.GiftPromotionInfo != null)
                model.ExtGiftList = Promotions.GetExtGiftList(model.GiftPromotionInfo.PmId);
            //套装商品列表
            model.SuitProductList = Promotions.GetProductAllSuitPromotion(pid, DateTime.Now);
            //满赠促销活动
            model.FullSendPromotionInfo = Promotions.GetFullSendPromotionByPidAndTime(pid, DateTime.Now);
            //满减促销活动
            model.FullCutPromotionInfo = Promotions.GetFullCutPromotionByPidAndTime(pid, DateTime.Now);

            //广告语
            model.Slogan = model.SinglePromotionInfo == null ? "" : model.SinglePromotionInfo.Slogan;
            //商品促销信息
            model.PromotionMsg = Promotions.GeneratePromotionMsg(model.SinglePromotionInfo, model.BuySendPromotionList, model.FullSendPromotionInfo, model.FullCutPromotionInfo);
            //商品折扣价格
            model.DiscountPrice = Promotions.ComputeDiscountPrice(model.ProductInfo.ShopPrice, model.SinglePromotionInfo);

            //关联商品列表
            model.RelateProductList = Products.GetRelateProductList(pid);

            //用户浏览历史
            model.UserBrowseHistory = BrowseHistories.GetUserBrowseHistory(WorkContext.Uid, pid);

            //商品咨询类型列表
            model.ProductConsultTypeList = ProductConsults.GetProductConsultTypeList();

            //更新浏览历史
            if (WorkContext.Uid > 0)
                Asyn.UpdateBrowseHistory(WorkContext.Uid, pid);
            //更新商品统计
            Asyn.UpdateProductStat(pid, WorkContext.RegionId);

            return View(model);
        }

        /// <summary>
        /// 套装
        /// </summary>
        public ActionResult Suit()
        {
            //套装id
            int pmId = GetRouteInt("pmId");
            if (pmId == 0)
                pmId = WebHelper.GetQueryInt("pmId");

            //判断套装是否存在或过期
            SuitPromotionInfo suitPromotionInfo = Promotions.GetSuitPromotionByPmIdAndTime(pmId, DateTime.Now);
            if (suitPromotionInfo == null)
                return PromptView("/", "你访问的套装不存在或过期");

            //扩展套装商品列表
            List<ExtSuitProductInfo> extSuitProductList = Promotions.GetExtSuitProductList(pmId);

            SuitModel model = new SuitModel();
            model.SuitPromotionInfo = suitPromotionInfo;
            model.SuitProductList = extSuitProductList;

            foreach (ExtSuitProductInfo extSuitProductInfo in extSuitProductList)
            {
                model.SuitDiscount += extSuitProductInfo.Number * extSuitProductInfo.Discount;
                model.ProductAmount += extSuitProductInfo.Number * extSuitProductInfo.ShopPrice;
            }
            model.SuitAmount = model.ProductAmount - model.SuitDiscount;

            return View(model);
        }

        /// <summary>
        /// 分类
        /// </summary>
        public ActionResult Category()
        {
            //分类id
            int cateId = GetRouteInt("cateId");
            if (cateId == 0)
                cateId = WebHelper.GetQueryInt("cateId");
            //品牌id
            int brandId = GetRouteInt("brandId");
            if (brandId == 0)
                brandId = WebHelper.GetQueryInt("brandId");
            //筛选价格
            int filterPrice = GetRouteInt("filterPrice");
            if (filterPrice == 0)
                filterPrice = WebHelper.GetQueryInt("filterPrice");
            //筛选属性
            string filterAttr = GetRouteString("filterAttr");
            if (filterAttr.Length == 0)
                filterAttr = WebHelper.GetQueryString("filterAttr");
            //是否只显示有货
            int onlyStock = GetRouteInt("onlyStock");
            if (onlyStock == 0)
                onlyStock = WebHelper.GetQueryInt("onlyStock");
            //排序列
            int sortColumn = GetRouteInt("sortColumn");
            if (sortColumn == 0)
                sortColumn = WebHelper.GetQueryInt("sortColumn");
            //排序方向
            int sortDirection = GetRouteInt("sortDirection");
            if (sortDirection == 0)
                sortDirection = WebHelper.GetQueryInt("sortDirection");
            //当前页数
            int page = GetRouteInt("page");
            if (page == 0)
                page = WebHelper.GetQueryInt("page");

            //分类信息
            CategoryInfo categoryInfo = Categories.GetCategoryById(cateId);
            if (categoryInfo == null)
                return PromptView("/", "此分类不存在");

            //分类关联品牌列表
            List<BrandInfo> brandList = Categories.GetCategoryBrandList(cateId);
            //分类筛选属性及其值列表
            List<KeyValuePair<AttributeInfo, List<AttributeValueInfo>>> cateAAndVList = Categories.GetCategoryFilterAAndVList(cateId);
            //分类价格范围列表
            string[] catePriceRangeList = StringHelper.SplitString(categoryInfo.PriceRange, "\r\n");

            //筛选属性处理
            List<int> attrValueIdList = new List<int>();
            string[] filterAttrValueIdList = StringHelper.SplitString(filterAttr, "-");
            if (filterAttrValueIdList.Length != cateAAndVList.Count)//当筛选属性和分类的筛选属性数目不对应时，重置筛选属性
            {
                if (cateAAndVList.Count == 0)
                {
                    filterAttr = "0";
                }
                else
                {
                    int count = cateAAndVList.Count;
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < count; i++)
                        sb.Append("0-");
                    filterAttr = sb.Remove(sb.Length - 1, 1).ToString();
                }
            }
            else
            {
                foreach (string attrValueId in filterAttrValueIdList)
                {
                    int temp = TypeHelper.StringToInt(attrValueId);
                    if (temp > 0) attrValueIdList.Add(temp);
                }
            }


            //分页对象
            PageModel pageModel = new PageModel(20, page, Products.GetCategoryProductCount(cateId, brandId, filterPrice, catePriceRangeList, attrValueIdList, onlyStock));
            //视图对象
            CategoryModel model = new CategoryModel()
            {
                CateId = cateId,
                BrandId = brandId,
                FilterPrice = filterPrice,
                FilterAttr = filterAttr,
                OnlyStock = onlyStock,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                CategoryInfo = categoryInfo,
                BrandList = brandList,
                CatePriceRangeList = catePriceRangeList,
                AAndVList = cateAAndVList,
                PageModel = pageModel,
                ProductList = Products.GetCategoryProductList(pageModel.PageSize, pageModel.PageNumber, cateId, brandId, filterPrice, catePriceRangeList, attrValueIdList, onlyStock, sortColumn, sortDirection)
            };

            return View(model);
        }

        /// <summary>
        /// 分类
        /// </summary>
        public ActionResult AjaxCategory()
        {
            //分类id
            int cateId = WebHelper.GetQueryInt("cateId");
            //品牌id
            int brandId = WebHelper.GetQueryInt("brandId");
            //筛选价格
            int filterPrice = WebHelper.GetQueryInt("filterPrice");
            //筛选属性
            string filterAttr = WebHelper.GetQueryString("filterAttr");
            //是否只显示有货
            int onlyStock = WebHelper.GetQueryInt("onlyStock");
            //排序列
            int sortColumn = WebHelper.GetQueryInt("sortColumn");
            //排序方向
            int sortDirection = WebHelper.GetQueryInt("sortDirection");
            //当前页数
            int page = WebHelper.GetQueryInt("page");

            //分类信息
            CategoryInfo categoryInfo = Categories.GetCategoryById(cateId);
            if (categoryInfo == null)
                return View(new CategoryModel());

            //分类关联品牌列表
            List<BrandInfo> brandList = Categories.GetCategoryBrandList(cateId);
            //分类筛选属性及其值列表
            List<KeyValuePair<AttributeInfo, List<AttributeValueInfo>>> cateAAndVList = Categories.GetCategoryFilterAAndVList(cateId);
            //分类价格范围列表
            string[] catePriceRangeList = StringHelper.SplitString(categoryInfo.PriceRange, "\r\n");

            //筛选属性处理
            List<int> attrValueIdList = new List<int>();
            string[] filterAttrValueIdList = StringHelper.SplitString(filterAttr, "-");
            if (filterAttrValueIdList.Length != cateAAndVList.Count)//当筛选属性和分类的筛选属性数目不对应时，重置筛选属性
            {
                if (cateAAndVList.Count == 0)
                {
                    filterAttr = "0";
                }
                else
                {
                    int count = cateAAndVList.Count;
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < count; i++)
                        sb.Append("0-");
                    filterAttr = sb.Remove(sb.Length - 1, 1).ToString();
                }
            }
            else
            {
                foreach (string attrValueId in filterAttrValueIdList)
                {
                    int temp = TypeHelper.StringToInt(attrValueId);
                    if (temp > 0) attrValueIdList.Add(temp);
                }
            }


            //分页对象
            PageModel pageModel = new PageModel(20, page, Products.GetCategoryProductCount(cateId, brandId, filterPrice, catePriceRangeList, attrValueIdList, onlyStock));
            //视图对象
            CategoryModel model = new CategoryModel()
            {
                CateId = cateId,
                BrandId = brandId,
                FilterPrice = filterPrice,
                FilterAttr = filterAttr,
                OnlyStock = onlyStock,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                CategoryInfo = categoryInfo,
                BrandList = brandList,
                CatePriceRangeList = catePriceRangeList,
                AAndVList = cateAAndVList,
                PageModel = pageModel,
                ProductList = Products.GetCategoryProductList(pageModel.PageSize, pageModel.PageNumber, cateId, brandId, filterPrice, catePriceRangeList, attrValueIdList, onlyStock, sortColumn, sortDirection)
            };

            return View(model);
        }

        /// <summary>
        /// 搜索
        /// </summary>
        public ActionResult Search()
        {
            //搜索词
            string keyword = WebHelper.GetQueryString("keyword");
            WorkContext.SearchWord = WebHelper.HtmlEncode(keyword);
            if (keyword.Length == 0)
                return PromptView(WorkContext.UrlReferrer, "请输入搜索词");
            if (!SecureHelper.IsSafeSqlString(keyword))
                return PromptView(WorkContext.UrlReferrer, "您搜索的商品不存在");

            //异步保存搜索历史
            Asyn.UpdateSearchHistory(WorkContext.Uid, keyword);

            //判断搜索词是否为分类名称，如果是则重定向到分类页面
            int cateId = Categories.GetCateIdByName(keyword);
            if (cateId > 0)
            {
                return Redirect(Url.Action("category", new RouteValueDictionary { { "cateId", cateId } }));
            }
            else
            {
                cateId = WebHelper.GetQueryInt("cateId");
            }

            //分类列表
            List<CategoryInfo> categoryList = null;
            //分类信息
            CategoryInfo categoryInfo = null;
            //品牌列表
            List<BrandInfo> brandList = null;

            //品牌id
            int brandId = Brands.GetBrandIdByName(keyword);
            if (brandId > 0)//当搜索词为品牌名称时
            {
                //获取品牌相关的分类
                categoryList = Brands.GetBrandCategoryList(brandId);

                //由于搜索结果的展示是以分类为基础的，所以当分类不存在时直接将搜索结果设为“搜索的商品不存在”
                if (categoryList.Count == 0)
                    return PromptView(WorkContext.UrlReferrer, "您搜索的商品不存在");

                if (cateId > 0)
                {
                    categoryInfo = Categories.GetCategoryById(cateId);
                }
                else
                {
                    //当没有进行分类的筛选时，将分类列表中的首项设为当前选中的分类
                    categoryInfo = categoryList[0];
                    cateId = categoryInfo.CateId;
                }
                brandList = new List<BrandInfo>();
                brandList.Add(Brands.GetBrandById(brandId));
            }
            else//当搜索词为商品关键词时
            {
                //获取商品关键词相关的分类
                categoryList = Searches.GetCategoryListByKeyword(keyword);

                //由于搜索结果的展示是以分类为基础的，所以当分类不存在时直接将搜索结果设为“搜索的商品不存在”
                if (categoryList.Count == 0)
                    return PromptView(WorkContext.UrlReferrer, "您搜索的商品不存在");

                if (cateId > 0)
                {
                    categoryInfo = Categories.GetCategoryById(cateId);
                }
                else
                {
                    categoryInfo = categoryList[0];
                    cateId = categoryInfo.CateId;
                }
                //根据商品关键词获取分类相关的品牌
                brandList = Searches.GetCategoryBrandListByKeyword(cateId, keyword);
                if (brandList.Count == 0)
                    return PromptView(WorkContext.UrlReferrer, "您搜索的商品不存在");
                brandId = WebHelper.GetQueryInt("brandId");
            }
            //最后再检查一遍分类是否存在
            if (categoryInfo == null)
                return PromptView(WorkContext.UrlReferrer, "您搜索的商品不存在");

            //筛选价格
            int filterPrice = WebHelper.GetQueryInt("filterPrice");
            //筛选属性
            string filterAttr = WebHelper.GetQueryString("filterAttr");
            //是否只显示有货
            int onlyStock = WebHelper.GetQueryInt("onlyStock");
            //排序列
            int sortColumn = WebHelper.GetQueryInt("sortColumn");
            //排序方向
            int sortDirection = WebHelper.GetQueryInt("sortDirection");
            //当前页数
            int page = WebHelper.GetQueryInt("page");

            //分类筛选属性及其值列表
            List<KeyValuePair<AttributeInfo, List<AttributeValueInfo>>> cateAAndVList = Categories.GetCategoryFilterAAndVList(cateId);
            //分类价格范围列表
            string[] catePriceRangeList = StringHelper.SplitString(categoryInfo.PriceRange, "\r\n");

            //筛选属性处理
            List<int> attrValueIdList = new List<int>();
            string[] filterAttrValueIdList = StringHelper.SplitString(filterAttr, "-");
            if (filterAttrValueIdList.Length != cateAAndVList.Count)//当筛选属性和分类的筛选属性数目不对应时，重置筛选属性
            {
                if (cateAAndVList.Count == 0)
                {
                    filterAttr = "0";
                }
                else
                {
                    int count = cateAAndVList.Count;
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < count; i++)
                        sb.Append("0-");
                    filterAttr = sb.Remove(sb.Length - 1, 1).ToString();
                }
            }
            else
            {
                foreach (string attrValueId in filterAttrValueIdList)
                {
                    int temp = TypeHelper.StringToInt(attrValueId);
                    if (temp > 0) attrValueIdList.Add(temp);
                }
            }

            //分页对象
            PageModel pageModel = new PageModel(20, page, Searches.GetSearchProductCount(keyword, cateId, brandId, filterPrice, catePriceRangeList, attrValueIdList, onlyStock));
            //视图对象
            SearchModel model = new SearchModel()
            {
                Word = keyword,
                CateId = cateId,
                BrandId = brandId,
                FilterPrice = filterPrice,
                FilterAttr = filterAttr,
                OnlyStock = onlyStock,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                CategoryList = categoryList,
                CategoryInfo = categoryInfo,
                BrandList = brandList,
                CatePriceRangeList = catePriceRangeList,
                AAndVList = cateAAndVList,
                PageModel = pageModel,
                ProductList = Searches.SearchProducts(pageModel.PageSize, pageModel.PageNumber, keyword, cateId, brandId, filterPrice, catePriceRangeList, attrValueIdList, onlyStock, sortColumn, sortDirection)
            };

            return View(model);
        }

        /// <summary>
        /// 搜索
        /// </summary>
        public ActionResult AjaxSearch()
        {
            //搜索词
            string keyword = WebHelper.GetQueryString("keyword");
            if (keyword.Length == 0)
                return View(new SearchModel());
            if (!SecureHelper.IsSafeSqlString(keyword))
                return View(new SearchModel());

            int cateId = Categories.GetCateIdByName(keyword);
            if (cateId > 0)
            {
                return View(new SearchModel());
            }
            else
            {
                cateId = WebHelper.GetQueryInt("cateId");
            }

            //分类列表
            List<CategoryInfo> categoryList = null;
            //分类信息
            CategoryInfo categoryInfo = null;
            //品牌列表
            List<BrandInfo> brandList = null;

            //品牌id
            int brandId = Brands.GetBrandIdByName(keyword);
            if (brandId > 0)//当搜索词为品牌名称时
            {
                //获取品牌相关的分类
                categoryList = Brands.GetBrandCategoryList(brandId);

                if (categoryList.Count == 0)
                    return View(new SearchModel());

                if (cateId > 0)
                {
                    categoryInfo = Categories.GetCategoryById(cateId);
                }
                else
                {
                    //当没有进行分类的筛选时，将分类列表中的首项设为当前选中的分类
                    categoryInfo = categoryList[0];
                    cateId = categoryInfo.CateId;
                }
                brandList = new List<BrandInfo>();
                brandList.Add(Brands.GetBrandById(brandId));
            }
            else//当搜索词为商品关键词时
            {
                //获取商品关键词相关的分类
                categoryList = Searches.GetCategoryListByKeyword(keyword);

                if (categoryList.Count == 0)
                    return View(new SearchModel());

                if (cateId > 0)
                {
                    categoryInfo = Categories.GetCategoryById(cateId);
                }
                else
                {
                    categoryInfo = categoryList[0];
                    cateId = categoryInfo.CateId;
                }
                //根据商品关键词获取分类相关的品牌
                brandList = Searches.GetCategoryBrandListByKeyword(cateId, keyword);
                if (brandList.Count == 0)
                    return View(new SearchModel());
                brandId = WebHelper.GetQueryInt("brandId");
            }
            if (categoryInfo == null)
                return View(new SearchModel());

            //筛选价格
            int filterPrice = WebHelper.GetQueryInt("filterPrice");
            //筛选属性
            string filterAttr = WebHelper.GetQueryString("filterAttr");
            //是否只显示有货
            int onlyStock = WebHelper.GetQueryInt("onlyStock");
            //排序列
            int sortColumn = WebHelper.GetQueryInt("sortColumn");
            //排序方向
            int sortDirection = WebHelper.GetQueryInt("sortDirection");
            //当前页数
            int page = WebHelper.GetQueryInt("page");

            //分类筛选属性及其值列表
            List<KeyValuePair<AttributeInfo, List<AttributeValueInfo>>> cateAAndVList = Categories.GetCategoryFilterAAndVList(cateId);
            //分类价格范围列表
            string[] catePriceRangeList = StringHelper.SplitString(categoryInfo.PriceRange, "\r\n");

            //筛选属性处理
            List<int> attrValueIdList = new List<int>();
            string[] filterAttrValueIdList = StringHelper.SplitString(filterAttr, "-");
            if (filterAttrValueIdList.Length != cateAAndVList.Count)//当筛选属性和分类的筛选属性数目不对应时，重置筛选属性
            {
                if (cateAAndVList.Count == 0)
                {
                    filterAttr = "0";
                }
                else
                {
                    int count = cateAAndVList.Count;
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < count; i++)
                        sb.Append("0-");
                    filterAttr = sb.Remove(sb.Length - 1, 1).ToString();
                }
            }
            else
            {
                foreach (string attrValueId in filterAttrValueIdList)
                {
                    int temp = TypeHelper.StringToInt(attrValueId);
                    if (temp > 0) attrValueIdList.Add(temp);
                }
            }

            //分页对象
            PageModel pageModel = new PageModel(20, page, Searches.GetSearchProductCount(keyword, cateId, brandId, filterPrice, catePriceRangeList, attrValueIdList, onlyStock));
            //视图对象
            SearchModel model = new SearchModel()
            {
                Word = keyword,
                CateId = cateId,
                BrandId = brandId,
                FilterPrice = filterPrice,
                FilterAttr = filterAttr,
                OnlyStock = onlyStock,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                CategoryList = categoryList,
                CategoryInfo = categoryInfo,
                BrandList = brandList,
                CatePriceRangeList = catePriceRangeList,
                AAndVList = cateAAndVList,
                PageModel = pageModel,
                ProductList = Searches.SearchProducts(pageModel.PageSize, pageModel.PageNumber, keyword, cateId, brandId, filterPrice, catePriceRangeList, attrValueIdList, onlyStock, sortColumn, sortDirection)
            };

            return View(model);
        }

        /// <summary>
        /// 活动专题
        /// </summary>
        public ActionResult Topic()
        {
            //专题id
            int topicId = GetRouteInt("topicId");
            if (topicId == 0)
                topicId = WebHelper.GetQueryInt("topicId");

            TopicInfo topicInfo = Topics.GetTopicById(topicId);
            if (topicInfo == null)
                return PromptView("此活动专题不存在");

            return View(topicInfo);
        }

        /// <summary>
        /// 商品评价列表
        /// </summary>
        public ActionResult ProductReviewList()
        {
            int pid = WebHelper.GetQueryInt("pid");
            int reviewType = WebHelper.GetQueryInt("reviewType");
            int page = WebHelper.GetQueryInt("page");

            //判断商品是否存在
            PartProductInfo productInfo = Products.GetPartProductById(pid);
            if (productInfo == null)
                return PromptView("/", "你访问的商品不存在");

            if (reviewType < 0 || reviewType > 3) reviewType = 0;

            PageModel pageModel = new PageModel(10, page, ProductReviews.GetProductReviewCount(pid, reviewType));
            ProductReviewListModel model = new ProductReviewListModel()
            {
                ProductInfo = productInfo,
                CategoryInfo = Categories.GetCategoryById(productInfo.CateId),
                BrandInfo = Brands.GetBrandById(productInfo.BrandId),
                ReviewType = reviewType,
                PageModel = pageModel,
                ProductReviewList = ProductReviews.GetProductReviewList(pid, reviewType, pageModel.PageSize, pageModel.PageNumber)
            };

            return View(model);
        }

        /// <summary>
        /// 商品评价列表
        /// </summary>
        public ActionResult AjaxProductReviewList()
        {
            int pid = WebHelper.GetQueryInt("pid");
            int reviewType = WebHelper.GetQueryInt("reviewType");
            int page = WebHelper.GetQueryInt("page");

            if (reviewType < 0 || reviewType > 3) reviewType = 0;

            PageModel pageModel = new PageModel(10, page, ProductReviews.GetProductReviewCount(pid, reviewType));
            AjaxProductReviewListModel model = new AjaxProductReviewListModel()
            {
                Pid = pid,
                ReviewType = reviewType,
                PageModel = pageModel,
                ProductReviewList = ProductReviews.GetProductReviewList(pid, reviewType, pageModel.PageSize, pageModel.PageNumber)
            };

            return View(model);
        }

        /// <summary>
        /// 商品咨询列表
        /// </summary>
        public ActionResult ProductConsultList()
        {
            int pid = WebHelper.GetQueryInt("pid");
            int consultTypeId = WebHelper.GetQueryInt("consultTypeId");
            string consultMessage = WebHelper.GetQueryString("consultMessage");
            int page = WebHelper.GetQueryInt("page");

            //判断商品是否存在
            PartProductInfo productInfo = Products.GetPartProductById(pid);
            if (productInfo == null)
                return PromptView("/", "你访问的商品不存在");

            if (!SecureHelper.IsSafeSqlString(consultMessage))
                return PromptView(WorkContext.UrlReferrer, "您搜索的内容不存在");

            PageModel pageModel = new PageModel(10, page, ProductConsults.GetProductConsultCount(pid, consultTypeId, consultMessage));
            ProductConsultListModel model = new ProductConsultListModel()
            {
                ProductInfo = productInfo,
                CategoryInfo = Categories.GetCategoryById(productInfo.CateId),
                BrandInfo = Brands.GetBrandById(productInfo.BrandId),
                ConsultTypeId = consultTypeId,
                ConsultMessage = consultMessage,
                PageModel = pageModel,
                ProductConsultList = ProductConsults.GetProductConsultList(pageModel.PageSize, pageModel.PageNumber, pid, consultTypeId, consultMessage),
                ProductConsultTypeList = ProductConsults.GetProductConsultTypeList(),
                IsVerifyCode = CommonHelper.IsInArray(WorkContext.PageKey, WorkContext.ShopConfig.VerifyPages)
            };

            return View(model);
        }

        /// <summary>
        /// 商品咨询列表
        /// </summary>
        public ActionResult AjaxProductConsultList()
        {
            int pid = WebHelper.GetQueryInt("pid");
            int consultTypeId = WebHelper.GetQueryInt("consultTypeId");
            string consultMessage = WebHelper.GetQueryString("consultMessage");
            int page = WebHelper.GetQueryInt("page");

            if (!SecureHelper.IsSafeSqlString(consultMessage))
                return View(new AjaxProductConsultListModel());

            PageModel pageModel = new PageModel(10, page, ProductConsults.GetProductConsultCount(pid, consultTypeId, consultMessage));
            AjaxProductConsultListModel model = new AjaxProductConsultListModel()
            {
                Pid = pid,
                ConsultTypeId = consultTypeId,
                ConsultMessage = consultMessage,
                PageModel = pageModel,
                ProductConsultList = ProductConsults.GetProductConsultList(pageModel.PageSize, pageModel.PageNumber, pid, consultTypeId, consultMessage),
                ProductConsultTypeList = ProductConsults.GetProductConsultTypeList(),
                IsVerifyCode = CommonHelper.IsInArray(WorkContext.PageKey, WorkContext.ShopConfig.VerifyPages)
            };

            return View(model);
        }

        /// <summary>
        /// 咨询商品
        /// </summary>
        public ActionResult ConsultProduct()
        {
            //不允许游客访问
            if (WorkContext.Uid < 1)
                return AjaxResult("nologin", "请先登录");

            //验证验证码
            if (CommonHelper.IsInArray(WorkContext.PageKey, WorkContext.ShopConfig.VerifyPages))
            {
                string verifyCode = WebHelper.GetFormString("verifyCode");//验证码
                if (string.IsNullOrWhiteSpace(verifyCode))
                {
                    return AjaxResult("emptyverifycode", "验证码不能为空"); ;
                }
                else if (verifyCode.ToLower() != Sessions.GetValueString(WorkContext.Sid, "verifyCode"))
                {
                    return AjaxResult("wrongverifycode", "验证码错误"); ;
                }
            }

            int pid = WebHelper.GetFormInt("pid");
            int consultTypeId = WebHelper.GetFormInt("consultTypeId");
            string consultMessage = WebHelper.GetFormString("consultMessage");

            PartProductInfo partProductInfo = Products.GetPartProductById(pid);
            if (partProductInfo == null)
                return AjaxResult("noproduct", "请选择商品");

            if (consultTypeId < 1 || ProductConsults.GetProductConsultTypeById(consultTypeId) == null)
                return AjaxResult("noproductconsulttype", "请选择咨询类型"); ;

            if (string.IsNullOrWhiteSpace(consultMessage))
                return AjaxResult("noconsultmessage", "请填写咨询内容"); ;
            if (consultMessage.Length > 100)
                return AjaxResult("muchconsultmessage", "咨询内容内容太长"); ;
            if (!SecureHelper.IsSafeSqlString(consultMessage))
                return AjaxResult("dangerconsultmessage", "咨询内中包含非法字符"); ;

            ProductConsults.ConsultProduct(pid, consultTypeId, WorkContext.Uid, DateTime.Now, WebHelper.HtmlEncode(consultMessage), WorkContext.NickName, partProductInfo.Name, partProductInfo.ShowImg, WorkContext.IP);
            return AjaxResult("success", Url.Action("product", new RouteValueDictionary { { "pid", pid } })); ;
        }
    }
}
