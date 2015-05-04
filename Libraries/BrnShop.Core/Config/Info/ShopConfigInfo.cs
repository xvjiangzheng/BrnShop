using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 商城配置信息类
    /// </summary>
    [Serializable]
    public class ShopConfigInfo : IConfigInfo
    {
        #region 站点信息

        private string _shopname = "brnshop网上商城";//商城名称
        private string _siteurl = "http://www.ddd.com";//网站网址
        private string _sitetitle = "brnshop网上商城";//网站标题
        private string _seokeyword = "";//seo关键字
        private string _seodescription = "";//seo描述
        private string _icp = "冀ICP22222";//备案编号
        private string _script = "";//脚本
        private int _islicensed = 1;//是否显示版权(0代表不显示，1代表显示)

        /// <summary>
        /// 商城名称
        /// </summary>
        public string ShopName
        {
            get { return _shopname; }
            set { _shopname = value; }
        }
        /// <summary>
        /// 网站网址
        /// </summary>
        public string SiteUrl
        {
            get { return _siteurl; }
            set { _siteurl = value; }
        }
        /// <summary>
        /// 网站标题
        /// </summary>
        public string SiteTitle
        {
            get { return _sitetitle; }
            set { _sitetitle = value; }
        }
        /// <summary>
        /// seo关键字
        /// </summary>
        public string SEOKeyword
        {
            get { return _seokeyword; }
            set { _seokeyword = value; }
        }
        /// <summary>
        /// seo描述
        /// </summary>
        public string SEODescription
        {
            get { return _seodescription; }
            set { _seodescription = value; }
        }
        /// <summary>
        /// 备案编号
        /// </summary>
        public string ICP
        {
            get { return _icp; }
            set { _icp = value; }
        }
        /// <summary>
        /// 脚本
        /// </summary>
        public string Script
        {
            get { return _script; }
            set { _script = value; }
        }
        /// <summary>
        /// 是否显示版权(0代表不显示，1代表显示)
        /// </summary>
        public int IsLicensed
        {
            get { return _islicensed; }
            set { _islicensed = value; }
        }

        #endregion

        #region 主题设置

        private string _pctheme = "default";//PC主题
        private string _mobiletheme = "default";//移动主题
        private string _wechattheme = "default";//微信主题
        private string _apptheme = "default";//APP主题

        /// <summary>
        /// PC主题
        /// </summary>
        public string PCTheme
        {
            get { return _pctheme; }
            set { _pctheme = value; }
        }
        /// <summary>
        /// 移动主题
        /// </summary>
        public string MobileTheme
        {
            get { return _mobiletheme; }
            set { _mobiletheme = value; }
        }
        /// <summary>
        /// 微信主题
        /// </summary>
        public string WeChatTheme
        {
            get { return _wechattheme; }
            set { _wechattheme = value; }
        }
        /// <summary>
        /// APP主题
        /// </summary>
        public string AppTheme
        {
            get { return _apptheme; }
            set { _apptheme = value; }
        }

        #endregion

        #region 账号设置

        private string _regtype = "";//注册类型(1代表用户名注册，2代表邮箱注册，3代表手机注册，空字符串代表不允许注册)
        private string _reservedname = "";//保留用户名
        private int _regtimespan = 0;//注册时间间隔(单位为秒，0代表无限制)
        private int _iswebcomemsg = 0;//是否发送欢迎信息(0代表不发送，1代表发送)
        private string _webcomemsg = "";//欢迎信息
        private string _logintype = "";//登陆类型(1代表用户名登陆，2代表邮箱登陆，3代表手机登陆，空字符串代表不允许登陆)
        private string _shadowname = "";//影子登录名
        private int _isremember = 1;//是否记住密码(0代表不记住，1代表记住)
        private int _loginfailtimes = 0;//登陆失败次数

        /// <summary>
        /// 注册类型(1代表用户名注册，2代表邮箱注册，3代表手机注册，空字符串代表不允许注册)
        /// </summary>
        public string RegType
        {
            get { return _regtype; }
            set { _regtype = value; }
        }

        /// <summary>
        /// 保留用户名
        /// </summary>
        public string ReservedName
        {
            get { return _reservedname; }
            set { _reservedname = value; }
        }

        /// <summary>
        /// 是否发送欢迎信息(0代表不发送，1代表发送)
        /// </summary>
        public int IsWebcomeMsg
        {
            get { return _iswebcomemsg; }
            set { _iswebcomemsg = value; }
        }

        /// <summary>
        /// 欢迎信息
        /// </summary>
        public string WebcomeMsg
        {
            get { return _webcomemsg; }
            set { _webcomemsg = value; }
        }

        /// <summary>
        /// 注册时间间隔(单位为秒，0代表无限制)
        /// </summary>
        public int RegTimeSpan
        {
            get { return _regtimespan; }
            set { _regtimespan = value; }
        }

        /// <summary>
        /// 登陆类型(1代表用户名登陆，2代表邮箱登陆，3代表手机登陆，空字符串代表不允许登陆)
        /// </summary>
        public string LoginType
        {
            get { return _logintype; }
            set { _logintype = value; }
        }

        /// <summary>
        /// 影子登录名
        /// </summary>
        public string ShadowName
        {
            get { return _shadowname; }
            set { _shadowname = value; }
        }

        /// <summary>
        /// 是否记住密码(0代表不记住，1代表记住)
        /// </summary>
        public int IsRemember
        {
            get { return _isremember; }
            set { _isremember = value; }
        }

        /// <summary>
        /// 登陆失败次数
        /// </summary>
        public int LoginFailTimes
        {
            get { return _loginfailtimes; }
            set { _loginfailtimes = value; }
        }


        #endregion

        #region 上传设置

        private string _uploadserver = "";//上传服务器
        private string _uploadimgtype = "";//上传的图片类型,例如".jpg"
        private int _uploadimgsize = 22;//上传图片的大小(单位为字节)
        private int _watermarktype = 0;//水印类型(0代表没有水印，1代表文字水印，2代表图片水印)
        private int _watermarkquality = 0;//水印质量(必须位于0到100之间)
        private int _watermarkposition = 0;//水印位置(1代表上左，2代表上中，3代表上右，4代表中左，5代表中中，6代表中右，7代表下左，8代表下中，9代表下右)
        private string _watermarkimg = "";//水印图片
        private int _watermarkimgopacity = 0;//水印图片透明度(必须位于1到10之间)
        private string _watermarktext = "";//水印文字
        private string _watermarktextfont = "";//水印文字字体
        private int _watermarktextsize = 0;//水印文字大小
        private string _brandthumbsize = "";//品牌缩略图大小
        private string _productshowthumbsize = "";//商品展示缩略图大小
        private string _useravatarthumbsize = "";//用户头像缩略图大小
        private string _userrankavatarthumbsize = "";//用户等级头像缩略图大小


        /// <summary>
        /// 上传服务器
        /// </summary>
        public string UploadServer
        {
            get { return _uploadserver; }
            set { _uploadserver = value; }
        }

        /// <summary>
        /// 上传的图片类型,例如".jpg"
        /// </summary>
        public string UploadImgType
        {
            get { return _uploadimgtype; }
            set { _uploadimgtype = value; }
        }

        /// <summary>
        /// 上传图片的大小(单位为字节)
        /// </summary>
        public int UploadImgSize
        {
            get { return _uploadimgsize; }
            set { _uploadimgsize = value; }
        }

        /// <summary>
        /// 水印类型(0代表没有水印，1代表文字水印，2代表图片水印)
        /// </summary>
        public int WatermarkType
        {
            get { return _watermarktype; }
            set { _watermarktype = value; }
        }

        /// <summary>
        /// 水印质量(必须位于0到100之间)
        /// </summary>
        public int WatermarkQuality
        {
            get { return _watermarkquality; }
            set { _watermarkquality = value; }
        }

        /// <summary>
        /// 水印位置(1代表上左，2代表上中，3代表上右，4代表中左，5代表中中，6代表中右，7代表下左，8代表下中，9代表下右)
        /// </summary>
        public int WatermarkPosition
        {
            get { return _watermarkposition; }
            set { _watermarkposition = value; }
        }

        /// <summary>
        /// 水印图片
        /// </summary>
        public string WatermarkImg
        {
            get { return _watermarkimg; }
            set { _watermarkimg = value; }
        }

        /// <summary>
        /// 水印图片透明度(必须位于1到10之间)
        /// </summary>
        public int WatermarkImgOpacity
        {
            get { return _watermarkimgopacity; }
            set { _watermarkimgopacity = value; }
        }

        /// <summary>
        /// 水印文字
        /// </summary>
        public string WatermarkText
        {
            get { return _watermarktext; }
            set { _watermarktext = value; }
        }

        /// <summary>
        /// 水印文字字体
        /// </summary>
        public string WatermarkTextFont
        {
            get { return _watermarktextfont; }
            set { _watermarktextfont = value; }
        }

        /// <summary>
        /// 水印文字大小
        /// </summary>
        public int WatermarkTextSize
        {
            get { return _watermarktextsize; }
            set { _watermarktextsize = value; }
        }

        /// <summary>
        /// 品牌缩略图大小
        /// </summary>
        public string BrandThumbSize
        {
            get { return _brandthumbsize; }
            set { _brandthumbsize = value; }
        }

        /// <summary>
        /// 商品展示缩略图大小
        /// </summary>
        public string ProductShowThumbSize
        {
            get { return _productshowthumbsize; }
            set { _productshowthumbsize = value; }
        }

        /// <summary>
        /// 用户头像缩略图大小
        /// </summary>
        public string UserAvatarThumbSize
        {
            get { return _useravatarthumbsize; }
            set { _useravatarthumbsize = value; }
        }

        /// <summary>
        /// 用户等级头像缩略图大小
        /// </summary>
        public string UserRankAvatarThumbSize
        {
            get { return _userrankavatarthumbsize; }
            set { _userrankavatarthumbsize = value; }
        }

        #endregion

        #region 性能设置

        private string _imagecdn = "";//图片cdn(不能以"/"结尾)
        private string _csscdn = "";//csscdn(不能以"/"结尾)
        private string _scriptcdn = "";//脚本cdn(不能以"/"结尾)
        private int _onlineuserexpire = 10;//在线用户过期时间(单位为分钟)
        private int _maxonlinecount = 1000;//最大在线人数
        private int _onlinecountexpire = 5;//在线人数缓存时间(单位为分钟,0代表即时数量)
        private int _updateonlinetimespan = 5;//更新用户在线时间间隔(单位为分钟,0代表不更新)
        private int _updatepvstattimespan = 5;//更新pv统计时间间隔(单位为分钟,0代表不统计pv)
        private int _isstatbrowser = 1;//是否统计浏览器(0代表不统计，1代表统计)
        private int _isstatos = 1;//是否统计操作系统(0代表不统计，1代表统计)
        private int _isstatregion = 1;//是否统计区域(0代表不统计，1代表统计)

        /// <summary>
        /// 图片cdn(不能以"/"结尾)
        /// </summary>
        public string ImageCDN
        {
            get { return _imagecdn; }
            set { _imagecdn = value; }
        }

        /// <summary>
        /// csscdn(不能以"/"结尾)
        /// </summary>
        public string CSSCDN
        {
            get { return _csscdn; }
            set { _csscdn = value; }
        }

        /// <summary>
        /// 脚本cdn(不能以"/"结尾)
        /// </summary>
        public string ScriptCDN
        {
            get { return _scriptcdn; }
            set { _scriptcdn = value; }
        }

        /// <summary>
        /// 在线用户过期时间(单位为分钟)
        /// </summary>
        public int OnlineUserExpire
        {
            get { return _onlineuserexpire; }
            set { _onlineuserexpire = value; }
        }

        /// <summary>
        /// 最大在线人数
        /// </summary>
        public int MaxOnlineCount
        {
            get { return _maxonlinecount; }
            set { _maxonlinecount = value; }
        }

        /// <summary>
        /// 在线人数缓存时间(单位为分钟,0代表即时数量)
        /// </summary>
        public int OnlineCountExpire
        {
            get { return _onlinecountexpire; }
            set { _onlinecountexpire = value; }
        }

        /// <summary>
        /// 更新用户在线时间间隔(单位为分钟,0代表不更新)
        /// </summary>
        public int UpdateOnlineTimeSpan
        {
            get { return _updateonlinetimespan; }
            set { _updateonlinetimespan = value; }
        }

        /// <summary>
        /// 更新pv统计时间间隔(单位为分钟,0代表不统计pv)
        /// </summary>
        public int UpdatePVStatTimespan
        {
            get { return _updatepvstattimespan; }
            set { _updatepvstattimespan = value; }
        }

        /// <summary>
        /// 是否统计浏览器(0代表不统计，1代表统计)
        /// </summary>
        public int IsStatBrowser
        {
            get { return _isstatbrowser; }
            set { _isstatbrowser = value; }
        }

        /// <summary>
        /// 是否统计操作系统(0代表不统计，1代表统计)
        /// </summary>
        public int IsStatOS
        {
            get { return _isstatos; }
            set { _isstatos = value; }
        }

        /// <summary>
        /// 是否统计区域(0代表不统计，1代表统计)
        /// </summary>
        public int IsStatRegion
        {
            get { return _isstatregion; }
            set { _isstatregion = value; }
        }

        #endregion

        #region 访问控制

        private int _isclosed = 0;//是否关闭商城(0代表打开，1代表关闭)
        private string _closereason = "";//商城关闭原因
        private string _banaccesstime = "";//禁止访问时间
        private string _banaccessip = "";//禁止访问ip
        private string _allowaccessip = "";//允许访问ip
        private string _adminallowaccessip = "";//后台允许访问ip
        private string _secretkey = "12345678";//密钥
        private string _cookiedomain = "";//cookie的有效域
        private string _randomlibrary = "";//随机库
        private string _verifypages = "";//使用验证码的页面
        private string _ignorewords = "";//忽略词
        private string _allowemailprovider = "";//允许的邮箱提供者
        private string _banemailprovider = "";//禁止的邮箱提供者

        /// <summary>
        /// 是否关闭商城(0代表打开，1代表关闭)
        /// </summary>
        public int IsClosed
        {
            get { return _isclosed; }
            set { _isclosed = value; }
        }

        /// <summary>
        /// 商城关闭原因
        /// </summary>
        public string CloseReason
        {
            get { return _closereason; }
            set { _closereason = value; }
        }

        /// <summary>
        /// 禁止访问时间
        /// </summary>
        public string BanAccessTime
        {
            get { return _banaccesstime; }
            set { _banaccesstime = value; }
        }

        /// <summary>
        /// 禁止访问ip
        /// </summary>
        public string BanAccessIP
        {
            get { return _banaccessip; }
            set { _banaccessip = value; }
        }

        /// <summary>
        /// 允许访问ip
        /// </summary>
        public string AllowAccessIP
        {
            get { return _allowaccessip; }
            set { _allowaccessip = value; }
        }

        /// <summary>
        /// 后台允许访问ip
        /// </summary>
        public string AdminAllowAccessIP
        {
            get { return _adminallowaccessip; }
            set { _adminallowaccessip = value; }
        }

        /// <summary>
        /// 密钥
        /// </summary>
        public string SecretKey
        {
            get { return _secretkey; }
            set { _secretkey = value; }
        }

        /// <summary>
        /// cookie的有效域
        /// </summary>
        public string CookieDomain
        {
            get { return _cookiedomain; }
            set { _cookiedomain = value; }
        }

        /// <summary>
        /// 随机库
        /// </summary>
        public string RandomLibrary
        {
            get { return _randomlibrary; }
            set { _randomlibrary = value; }
        }

        /// <summary>
        /// 使用验证码的页面
        /// </summary>
        public string VerifyPages
        {
            get { return _verifypages; }
            set { _verifypages = value; }
        }

        /// <summary>
        /// 忽略词
        /// </summary>
        public string IgnoreWords
        {
            get { return _ignorewords; }
            set { _ignorewords = value; }
        }

        /// <summary>
        /// 允许的邮箱提供者
        /// </summary>
        public string AllowEmailProvider
        {
            get { return _allowemailprovider; }
            set { _allowemailprovider = value; }
        }

        /// <summary>
        /// 禁止的邮箱提供者
        /// </summary>
        public string BanEmailProvider
        {
            get { return _banemailprovider; }
            set { _banemailprovider = value; }
        }

        #endregion

        #region 商城设置

        private int _isguestsc = 1;//是否允许游客使用购物车
        private int _scsubmittype = 0;//购物车的提交方式(0代表跳转到提示页面，1代表跳转到列表页面，2代表ajax提交)
        private int _guestsccount = 30;//游客购物车最大商品数量
        private int _membersccount = 50;//会员购物车最大商品数量
        private int _scexpire = 8;//购物车过期时间(单位为天)
        private string _osnformat = "";//订单编号格式
        private int _onlinepayexpire = 24;//在线支付过期时间(单位为小时)
        private int _offlinepayexpire = 7;//线下支付过期时间(单位为天)
        private int _brohiscount = 8;//浏览历史数量
        private int _maxshipaddress = 12;//最大配送地址
        private int _favoritecount = 20;//收藏夹最大容量

        /// <summary>
        /// 是否允许游客使用购物车
        /// </summary>
        public int IsGuestSC
        {
            get { return _isguestsc; }
            set { _isguestsc = value; }
        }
        /// <summary>
        /// 购物车的提交方式(0代表跳转到提示页面，1代表跳转到列表页面，2代表ajax提交)
        /// </summary>
        public int SCSubmitType
        {
            get { return _scsubmittype; }
            set { _scsubmittype = value; }
        }
        /// <summary>
        /// 游客购物车最大商品数量
        /// </summary>
        public int GuestSCCount
        {
            get { return _guestsccount; }
            set { _guestsccount = value; }
        }
        /// <summary>
        /// 会员购物车最大商品数量
        /// </summary>
        public int MemberSCCount
        {
            get { return _membersccount; }
            set { _membersccount = value; }
        }
        /// <summary>
        /// 购物车过期时间(单位为天)
        /// </summary>
        public int SCExpire
        {
            get { return _scexpire; }
            set { _scexpire = value; }
        }
        /// <summary>
        /// 订单编号格式
        /// </summary>
        public string OSNFormat
        {
            get { return _osnformat; }
            set { _osnformat = value; }
        }
        /// <summary>
        /// 在线支付过期时间(单位为小时)
        /// </summary>
        public int OnlinePayExpire
        {
            get { return _onlinepayexpire; }
            set { _onlinepayexpire = value; }
        }
        /// <summary>
        /// 线下支付过期时间(单位为天)
        /// </summary>
        public int OfflinePayExpire
        {
            get { return _offlinepayexpire; }
            set { _offlinepayexpire = value; }
        }
        /// <summary>
        /// 浏览历史数量
        /// </summary>
        public int BroHisCount
        {
            get { return _brohiscount; }
            set { _brohiscount = value; }
        }
        /// <summary>
        /// 最大配送地址
        /// </summary>
        public int MaxShipAddress
        {
            get { return _maxshipaddress; }
            set { _maxshipaddress = value; }
        }
        /// <summary>
        /// 收藏夹最大容量
        /// </summary>
        public int FavoriteCount
        {
            get { return _favoritecount; }
            set { _favoritecount = value; }
        }

        #endregion
    }
}
