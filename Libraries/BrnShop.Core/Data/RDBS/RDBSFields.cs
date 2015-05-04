using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 关系数据库表
    /// </summary>
    public partial class RDBSFields
    {
        /// <summary>
        /// 后台操作表
        /// </summary>
        public const string ADMIN_ACTIONS = "[adminaid],[title],[action],[parentid],[displayorder]";

        /// <summary>
        /// 管理员组表
        /// </summary>
        public const string ADMIN_GROUPS = "[admingid],[title],[actionlist]";

        /// <summary>
        /// 管理员操作日志表
        /// </summary>
        public const string ADMIN_OPERATELOGS = "[logid],[uid],[nickname],[admingid],[admingtitle],[operation],[description],[ip],[operatetime]";

        /// <summary>
        /// 广告位置表
        /// </summary>
        public const string ADVERT_POSITIONS = "[adposid],[title],[description]";

        /// <summary>
        /// 广告表
        /// </summary>
        public const string ADVERTS = "[adid],[clickcount],[adposid],[state],[starttime],[endtime],[displayorder],[type],[title],[url],[body],[extfield1],[extfield2],[extfield3],[extfield4],[extfield5]";

        /// <summary>
        /// 属性分组表
        /// </summary>
        public const string ATTRIBUTE_GROUPS = "[attrgroupid],[cateid],[name],[displayorder]";

        /// <summary>
        /// 属性表
        /// </summary>
        public const string ATTRIBUTES = "[attrid],[name],[cateid],[attrgroupid],[showtype],[isfilter],[displayorder]";

        /// <summary>
        /// 属性值表
        /// </summary>
        public const string ATTRIBUTE_VALUES = "[attrvalueid],[attrvalue],[isinput],[attrname],[attrdisplayorder],[attrshowtype],[attrvaluedisplayorder],[attrgroupid],[attrgroupname],[attrgroupdisplayorder],[attrid]";

        /// <summary>
        /// 被禁止的ip表
        /// </summary>
        public const string BANNEDIPS = "[id],[ip],[liftbantime]";

        /// <summary>
        /// banner表
        /// </summary>
        public const string BANNERS = "[id],[type],[starttime],[endtime],[isshow],[title],[img],[url],[displayorder]";

        /// <summary>
        /// 品牌表
        /// </summary>
        public const string BRANDS = "[brandid],[displayorder],[name],[logo]";

        /// <summary>
        /// 浏览历史表
        /// </summary>
        public const string BROWSEHISTORIES = "[recordid],[uid],[pid],[times],[updatetime]";

        /// <summary>
        /// 买送促销商品表
        /// </summary>
        public const string BUYSEND_PRODUCTS = "[recordid],[pmid],[pid]";

        /// <summary>
        /// 买送促销表
        /// </summary>
        public const string BUYSEND_PROMOTIONS = "[pmid],[starttime],[endtime],[userranklower],[state],[name],[type],[buycount],[sendcount]";

        /// <summary>
        /// 分类表
        /// </summary>
        public const string CATEGORIES = "[cateid],[displayorder],[name],[pricerange],[parentid],[layer],[haschild],[path]";

        /// <summary>
        /// 优惠劵商品表
        /// </summary>
        public const string COUPON_PRODUCTS = "[recordid],[coupontypeid],[pid]";

        /// <summary>
        /// 优惠劵表
        /// </summary>
        public const string COUPONS = "[couponid],[couponsn],[uid],[coupontypeid],[oid],[usetime],[useip],[money],[activatetime],[activateip],[createuid],[createoid],[createtime],[createip]";

        /// <summary>
        /// 优惠劵类型表
        /// </summary>
        public const string COUPON_TYPES = "[coupontypeid],[state],[name],[money],[count],[sendmode],[getmode],[usemode],[userranklower],[orderamountlower],[limitcateid],[limitbrandid],[limitproduct],[sendstarttime],[sendendtime],[useexpiretime],[usestarttime],[useendtime]";

        /// <summary>
        /// 积分日志表
        /// </summary>
        public const string CREDITLOGS = "[logid],[uid],[paycredits],[rankcredits],[action],[actioncode],[actiontime],[actiondes],[operator]";

        /// <summary>
        /// 事件日志表
        /// </summary>
        public const string EVENTLOGS = "[id],[key],[title],[server],[executetime]";

        /// <summary>
        /// 收藏夹表
        /// </summary>
        public const string FAVORITES = "[recordid],[uid],[pid],[state],[addtime]";

        /// <summary>
        /// 筛选词表
        /// </summary>
        public const string FILTERWORDS = "[id],[match],[replace]";

        /// <summary>
        /// 友情链接表
        /// </summary>
        public const string FRIENDLINKS = "[id],[name],[title],[logo],[url],[target],[displayorder]";

        /// <summary>
        /// 满减促销商品表
        /// </summary>
        public const string FULLCUT_PRODUCTS = "[recordid],[pmid],[pid]";

        /// <summary>
        /// 满减促销表
        /// </summary>
        public const string FULLCUT_PROMOTIONS = "[pmid],[type],[starttime],[endtime],[userranklower],[state],[name],[limitmoney1],[cutmoney1],[limitmoney2],[cutmoney2],[limitmoney3],[cutmoney3]";

        /// <summary>
        /// 满赠促销商品表
        /// </summary>
        public const string FULLSEND_PRODUCTS = "[recordid],[pmid],[pid],[type]";

        /// <summary>
        /// 满赠促销表
        /// </summary>
        public const string FULLSEND_PROMOTIONS = "[pmid],[starttime],[endtime],[userranklower],[state],[name],[limitmoney],[addmoney]";

        /// <summary>
        /// 赠品表
        /// </summary>
        public const string GIFTS = "[recordid],[pmid],[giftid],[number],[pid]";

        /// <summary>
        /// 赠品促销表
        /// </summary>
        public const string GIFT_PROMOTIONS = "[pmid],[pid],[starttime1],[endtime1],[starttime2],[endtime2],[starttime3],[endtime3],[userranklower],[state],[name],[quotaupper]";

        /// <summary>
        /// 商城帮助表
        /// </summary>
        public const string HELPS = "[id],[pid],[title],[url],[description],[displayorder]";

        /// <summary>
        /// 登陆失败日志表
        /// </summary>
        public const string LOGINFAILLOGS = "[id],[loginip],[failtimes],[lastlogintime]";

        /// <summary>
        /// 导航栏表
        /// </summary>
        public const string NAVS = "[id],[pid],[layer],[name],[title],[url],[target],[displayorder]";

        /// <summary>
        /// 新闻表
        /// </summary>
        public const string NEWS = "[newsid],[newstypeid],[isshow],[istop],[ishome],[displayorder],[addtime],[title],[url],[body]";

        /// <summary>
        /// 新闻类型表
        /// </summary>
        public const string NEWS_TYPES = "[newstypeid],[name],[displayorder]";

        /// <summary>
        /// 开放授权表
        /// </summary>
        public const string OAUTH = "[id],[uid],[openid],[server]";

        /// <summary>
        /// 用户在线时间表
        /// </summary>
        public const string ONLINE_TIME = "[uid],[total],[year],[month],[week],[day],[updatetime]";

        /// <summary>
        /// 在线用户表
        /// </summary>
        public const string ONLINE_USERS = "[olid],[uid],[sid],[nickname],[ip],[regionid],[updatetime]";

        /// <summary>
        /// 订单处理表
        /// </summary>
        public const string ORDER_ACTIONS = "[aid],[oid],[uid],[realname],[admingid],[admingtitle],[actiontype],[actiontime],[actiondes]";

        /// <summary>
        /// 订单商品表
        /// </summary>
        public const string ORDER_PRODUCTS = "[recordid],[oid],[uid],[sid],[pid],[psn],[cateid],[brandid],[name],[showimg],[discountprice],[shopprice],[costprice],[marketprice],[weight],[isreview],[realcount],[buycount],[sendcount],[type],[paycredits],[coupontypeid],[extcode1],[extcode2],[extcode3],[extcode4],[extcode5],[addtime]";

        /// <summary>
        /// 订单退款表
        /// </summary>
        public const string ORDER_REFUNDS = "[refundid],[oid],[osn],[uid],[state],[applytime],[paymoney],[refundmoney],[refundsn],[refundsystemname],[refundfriendname],[refundtime],[paysn],[paysystemname],[payfriendname]";

        /// <summary>
        /// 订单表
        /// </summary>
        public const string ORDERS = "[oid],[osn],[uid],[orderstate],[productamount],[orderamount],[surplusmoney],[parentid],[isreview],[addtime],[shipsn],[shipsystemname],[shipfriendname],[shiptime],[paysn],[paysystemname],[payfriendname],[paymode],[paytime],[regionid],[consignee],[mobile],[phone],[email],[zipcode],[address],[besttime],[shipfee],[payfee],[fullcut],[discount],[paycreditcount],[paycreditmoney],[couponmoney],[weight],[buyerremark],[ip]";

        /// <summary>
        /// 商品属性表
        /// </summary>
        public const string PRODUCT_ATTRIBUTES = "[recordid],[pid],[attrid],[attrvalueid],[inputvalue]";

        /// <summary>
        /// 商品咨询表
        /// </summary>
        public const string PRODUCT_CONSULTS = "[consultid],[pid],[consulttypeid],[state],[consultuid],[replyuid],[consulttime],[replytime],[consultmessage],[replymessage],[consultnickname],[replynickname],[pname],[pshowimg],[consultip],[replyip]";

        /// <summary>
        /// 商品咨询类型表
        /// </summary>
        public const string PRODUCT_CONSULTTYPES = "[consulttypeid],[title],[displayorder]";

        /// <summary>
        /// 商品图片表
        /// </summary>
        public const string PRODUCT_IMAGES = "[pimgid],[pid],[showimg],[ismain],[displayorder]";

        /// <summary>
        /// 商品关键词表
        /// </summary>
        public const string PRODUCT_KEYWORDS = "[keywordid],[keyword],[pid],[relevancy]";

        /// <summary>
        /// 商品评价表
        /// </summary>
        public const string PRODUCT_REVIEWS = "[reviewid],[pid],[uid],[oprid],[oid],[parentid],[state],[star],[quality],[message],[reviewtime],[paycredits],[pname],[pshowimg],[buytime],[ip]";

        /// <summary>
        /// 商品表
        /// </summary>
        public const string PRODUCTS = "[pid],[psn],[cateid],[brandid],[skugid],[name],[shopprice],[marketprice],[costprice],[state],[isbest],[ishot],[isnew],[displayorder],[weight],[showimg],[salecount],[visitcount],[reviewcount],[star1],[star2],[star3],[star4],[star5],[addtime],[description]";

        /// <summary>
        /// 商品部分表
        /// </summary>
        public const string PART_PRODUCTS = "[pid],[psn],[cateid],[brandid],[skugid],[name],[shopprice],[marketprice],[costprice],[state],[isbest],[ishot],[isnew],[displayorder],[weight],[showimg],[salecount],[visitcount],[reviewcount],[star1],[star2],[star3],[star4],[star5],[addtime]";

        /// <summary>
        /// 商品SKU表
        /// </summary>
        public const string PRODUCT_SKUS = "[recordid],[skugid],[pid],[attrid],[attrvalueid],[inputvalue]";

        /// <summary>
        /// 商品统计表
        /// </summary>
        public const string PRODUCT_STATS = "[recordid],[pid],[category],[value],[count]";

        /// <summary>
        /// 商品库存表
        /// </summary>
        public const string PRODUCT_STOCKS = "[pid],[number],[limit]";

        /// <summary>
        /// PV统计表
        /// </summary>
        public const string PVSTATS = "[category],[value],[count]";

        /// <summary>
        /// 全国行政区域表
        /// </summary>
        public const string REGIONS = "[regionid],[name],[spell],[shortspell],[displayorder],[parentid],[layer],[provinceid],[provincename],[cityid],[cityname]";

        /// <summary>
        /// 关联商品表
        /// </summary>
        public const string RELATEPRODUCTS = "[recordid],[pid],[relatepid]";

        /// <summary>
        /// 搜索历史表
        /// </summary>
        public const string SEARCHHISTORIES = "[recordid],[uid],[keyword],[times],[updatetime]";

        /// <summary>
        /// 用户配送地址表
        /// </summary>
        public const string SHIPADDRESSES = "[said],[uid],[regionid],[isdefault],[consignee],[mobile],[phone],[email],[zipcode],[address]";

        /// <summary>
        /// 签名商品表
        /// </summary>
        public const string SIGNPRODUCTS = "[recordid],[sign],[pid]";

        /// <summary>
        /// 单品促销表
        /// </summary>
        public const string SINGLE_PROMOTIONS = "[pmid],[pid],[starttime1],[endtime1],[starttime2],[endtime2],[starttime3],[endtime3],[userranklower],[state],[name],[slogan],[discounttype],[discountvalue],[coupontypeid],[paycredits],[isstock],[stock],[quotalower],[quotaupper],[allowbuycount]";

        /// <summary>
        /// 套装商品表
        /// </summary>
        public const string SUIT_PRODUCTS = "[recordid],[pmid],[pid],[discount],[number]";

        /// <summary>
        /// 套装促销活动表
        /// </summary>
        public const string SUIT_PROMOTIONS = "[pmid],[starttime1],[endtime1],[starttime2],[endtime2],[starttime3],[endtime3],[userranklower],[state],[name],[quotaupper],[onlyonce]";

        /// <summary>
        /// 定时商品表
        /// </summary>
        public const string TIMEPRODUCTS = "[recordid],[pid],[onsalestate],[outsalestate],[onsaletime],[outsaletime]";

        /// <summary>
        /// 活动专题表
        /// </summary>
        public const string TOPICS = "[topicid],[starttime],[endtime],[isshow],[sn],[title],[headhtml],[bodyhtml]";

        /// <summary>
        /// 部分用户表
        /// </summary>
        public const string PARTUSERS = "[uid],[username],[email],[mobile],[password],[userrid],[admingid],[nickname],[avatar],[paycredits],[rankcredits],[verifyemail],[verifymobile],[liftbantime],[salt]";

        /// <summary>
        /// 用户细节表
        /// </summary>
        public const string USERDETAILS = "[uid],[lastvisittime],[lastvisitip],[lastvisitrgid],[registertime],[registerip],[registerrgid],[gender],[realname],[bday],[idcard],[regionid],[address],[bio]";

        /// <summary>
        /// 用户等级表
        /// </summary>
        public const string USER_RANKS = "[userrid],[system],[title],[avatar],[creditslower],[creditsupper],[limitdays]";
    }
}
