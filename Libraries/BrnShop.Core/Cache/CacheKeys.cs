using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 缓存键
    /// </summary>
    public partial class CacheKeys
    {
        /// <summary>
        /// 在线游客数量缓存键
        /// </summary>
        public const string SHOP_ONLINEUSER_GUESTCOUNT = "/Shop/OnlineGuestCount";
        /// <summary>
        /// 全部在线人数缓存键
        /// </summary>
        public const string SHOP_ONLINEUSER_ALLUSERCOUNT = "/Shop/OnlineAllUserCount";

        /// <summary>
        /// 用户等级列表缓存键
        /// </summary>
        public const string SHOP_USERRANK_LIST = "/Shop/UserRankList";

        /// <summary>
        /// 后台操作HashSet缓存键
        /// </summary>
        public const string SHOP_ADMINACTION_HASHSET = "/Shop/AdminActionHashSet";
        /// <summary>
        /// 管理员组列表缓存键
        /// </summary>
        public const string SHOP_ADMINGROUP_LIST = "/Shop/AdminGroupList";
        /// <summary>
        /// 管理员组操作HashSet缓存键
        /// </summary>
        public const string SHOP_ADMINGROUP_ACTIONHASHSET = "/Shop/AdminGroupActionHashSet/";

        /// <summary>
        /// 被禁止的IPHashSet缓存键
        /// </summary>
        public const string SHOP_BANNEDIP_HASHSET = "/Shop/BannedIPHashSet";

        /// <summary>
        /// 筛选词正则列表缓存键
        /// </summary>
        public const string SHOP_FILTERWORD_REGEXLIST = "/Shop/FilterWordRegexList";

        /// <summary>
        /// 品牌信息缓存键
        /// </summary>
        public const string SHOP_BRAND_INFO = "/Shop/BrandInfo/";
        /// <summary>
        /// 品牌分类缓存键
        /// </summary>
        public const string SHOP_BRAND_CATEGORYLIST = "/Shop/BrandCategoryList/";

        /// <summary>
        /// 分类列表缓存键
        /// </summary>
        public const string SHOP_CATEGORY_LIST = "/Shop/CategoryList";
        /// <summary>
        /// 分类筛选属性及其值列表缓存键
        /// </summary>
        public const string SHOP_CATEGORY_FILTERAANDVLIST = "/Shop/CategoryFilterAANDVList/";
        /// <summary>
        /// 分类属性及其值列表JSON缓存键
        /// </summary>
        public const string SHOP_CATEGORY_AANDVLISTJSONCACHE = "/Shop/CategoryAANDVListJSONCache/";
        /// <summary>
        /// 分类品牌列表缓存键
        /// </summary>
        public const string SHOP_CATEGORY_BRANDLIST = "/Shop/CategoryBrandList/";

        /// <summary>
        /// 签名商品列表
        /// </summary>
        public const string SHOP_SIGNPRODUCT_LIST = "/Shop/SignProductList/";

        /// <summary>
        /// 商品咨询类型列表缓存键
        /// </summary>
        public const string SHOP_PRODUCTCONSULTTYPE_LIST = "/Shop/ProductConsultTypeList";

        /// <summary>
        /// 活动专题信息缓存键
        /// </summary>
        public const string SHOP_TOPIC_INFO = "/Shop/TopicInfo/";

        /// <summary>
        /// 发放中的优惠劵类型列表缓存键
        /// </summary>
        public const string SHOP_COUPONTYPE_SENDINGLIST = "/Shop/SendingCouponTypeList";
        /// <summary>
        /// 使用中的优惠劵类型列表缓存键
        /// </summary>
        public const string SHOP_COUPONTYPE_USINGLIST = "/Shop/UsingCouponTypeList";
        /// <summary>
        /// 优惠劵类型信息缓存键
        /// </summary>
        public const string SHOP_COUPONTYPE_INFO = "/Shop/CouponTypeInfo/";
        /// <summary>
        /// 优惠劵类型发放数量缓存键
        /// </summary>
        public const string SHOP_COUPONTYPE_SENDCOUNT = "/Shop/CouponTypeSendCount/";

        /// <summary>
        /// 新闻类型列表缓存键
        /// </summary>
        public const string SHOP_NEWSTYPE_LIST = "/Shop/NewsTypeList";
        /// <summary>
        /// 置首新闻列表缓存键
        /// </summary>
        public const string SHOP_NEWS_HOMELIST = "/Shop/HomeNewsList/";

        /// <summary>
        /// 首页banner列表缓存键
        /// </summary>
        public const string SHOP_BANNER_HOMELIST = "/Shop/HomeBannerList/";

        /// <summary>
        /// 帮助列表缓存键
        /// </summary>
        public const string SHOP_HELP_LIST = "/Shop/HelpList";

        /// <summary>
        /// 友情链接列表缓存键
        /// </summary>
        public const string SHOP_FRIENDLINK_LIST = "/Shop/FriendLinkList";

        /// <summary>
        /// 导航列表缓存键
        /// </summary>
        public const string SHOP_NAV_LIST = "/Shop/NavList";
        /// <summary>
        /// 主导航列表缓存键
        /// </summary>
        public const string SHOP_NAV_MAINLIST = "/Shop/MainNavList";

        /// <summary>
        /// 子区域列表缓存键
        /// </summary>
        public const string SHOP_REGION_CHILDLIST = "/Shop/ChildRegionList/";
        /// <summary>
        /// 区域缓存键
        /// </summary>
        public const string SHOP_REGION_INFOBYID = "/Shop/RegionInfo/";
        /// <summary>
        /// 区域缓存键
        /// </summary>
        public const string SHOP_REGION_INFOBYNAMEANDLAYER = "/Shop/RegionInfo/{0}/{1}";

        /// <summary>
        /// 广告列表缓存键
        /// </summary>
        public const string SHOP_ADVERT_LIST = "/Shop/AdvertList/";
    }
}
