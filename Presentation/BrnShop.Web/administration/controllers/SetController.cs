using System;
using System.IO;
using System.Web;
using System.Xml;
using System.Data;
using System.Text;
using System.Drawing;
using System.Web.Mvc;
using System.Reflection;
using System.Drawing.Text;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Admin.Models;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台系统设置控制器类
    /// </summary>
    public partial class SetController : BaseAdminController
    {
        /// <summary>
        /// 站点设置
        /// </summary>
        [HttpGet]
        public ActionResult Site()
        {
            ShopConfigInfo shopConfigInfo = BSPConfig.ShopConfig;

            SiteModel model = new SiteModel();
            model.ShopName = shopConfigInfo.ShopName;
            model.SiteUrl = shopConfigInfo.SiteUrl;
            model.SiteTitle = shopConfigInfo.SiteTitle;
            model.SEOKeyword = shopConfigInfo.SEOKeyword;
            model.SEODescription = shopConfigInfo.SEODescription;
            model.ICP = shopConfigInfo.ICP;
            model.Script = shopConfigInfo.Script;
            model.IsLicensed = shopConfigInfo.IsLicensed;

            return View(model);
        }

        /// <summary>
        /// 站点设置
        /// </summary>
        [HttpPost]
        public ActionResult Site(SiteModel model)
        {
            if (ModelState.IsValid)
            {
                ShopConfigInfo shopConfigInfo = BSPConfig.ShopConfig;

                shopConfigInfo.ShopName = model.ShopName == null ? "" : model.ShopName;
                shopConfigInfo.SiteUrl = model.SiteUrl == null ? "" : model.SiteUrl;
                shopConfigInfo.SiteTitle = model.SiteTitle == null ? "" : model.SiteTitle;
                shopConfigInfo.SEOKeyword = model.SEOKeyword == null ? "" : model.SEOKeyword;
                shopConfigInfo.SEODescription = model.SEODescription == null ? "" : model.SEODescription;
                shopConfigInfo.ICP = model.ICP == null ? "" : model.ICP;
                shopConfigInfo.Script = model.Script == null ? "" : model.Script;
                shopConfigInfo.IsLicensed = model.IsLicensed;

                BSPConfig.SaveShopConfig(shopConfigInfo);
                Emails.ResetShop();
                SMSes.ResetShop();
                AddAdminOperateLog("修改站点信息");
                return PromptView(Url.Action("site"), "修改站点信息成功");
            }
            return View(model);
        }

        /// <summary>
        /// 主题
        /// </summary>
        public ActionResult Theme()
        {
            ThemeModel model = new ThemeModel();

            #region 加载主题列表

            Type stringType = Type.GetType("System.String");

            DataTable pcThemeList = new DataTable();
            pcThemeList.Columns.Add("name", stringType);
            pcThemeList.Columns.Add("title", stringType);
            pcThemeList.Columns.Add("author", stringType);
            pcThemeList.Columns.Add("version", stringType);
            pcThemeList.Columns.Add("supVersion", stringType);
            pcThemeList.Columns.Add("createTime", stringType);
            pcThemeList.Columns.Add("copyright", stringType);

            string path = IOHelper.GetMapPath("/themes");
            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo[] themeDirList = dir.GetDirectories();

            foreach (DirectoryInfo themeDir in themeDirList)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(themeDir.FullName + @"\theme.xml");

                DataRow row = pcThemeList.NewRow();
                row["name"] = themeDir.Name;
                foreach (XmlAttribute attribute in doc.DocumentElement.Attributes)
                    row[attribute.Name] = attribute.Value;
                pcThemeList.Rows.Add(row);
            }

            DataTable mobileThemeList = new DataTable();
            mobileThemeList.Columns.Add("name", stringType);
            mobileThemeList.Columns.Add("title", stringType);
            mobileThemeList.Columns.Add("author", stringType);
            mobileThemeList.Columns.Add("version", stringType);
            mobileThemeList.Columns.Add("supVersion", stringType);
            mobileThemeList.Columns.Add("createTime", stringType);
            mobileThemeList.Columns.Add("copyright", stringType);

            path = IOHelper.GetMapPath("/mobile/themes");
            dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                themeDirList = dir.GetDirectories();

                foreach (DirectoryInfo themeDir in themeDirList)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(themeDir.FullName + @"\theme.xml");

                    DataRow row = mobileThemeList.NewRow();
                    row["name"] = themeDir.Name;
                    foreach (XmlAttribute attribute in doc.DocumentElement.Attributes)
                        row[attribute.Name] = attribute.Value;
                    mobileThemeList.Rows.Add(row);
                }
            }

            #endregion

            model.PCThemeList = pcThemeList;
            model.DefaultPCTheme = WorkContext.ShopConfig.PCTheme;
            model.MobileThemeList = mobileThemeList;
            model.DefaultMobileTheme = WorkContext.ShopConfig.MobileTheme;

            return View(model);
        }

        /// <summary>
        /// 默认主题
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="name">主题名称</param>
        /// <returns></returns>
        public ActionResult DefaultTheme(int type = 0, string name = "")
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                string path = null;
                if (type == 0)
                {
                    path = IOHelper.GetMapPath("/themes");
                }
                else if (type == 1)
                {
                    path = IOHelper.GetMapPath("/mobile/themes");
                }
                DirectoryInfo dir = new DirectoryInfo(path);
                DirectoryInfo[] themeDirList = dir.GetDirectories(name);
                if (themeDirList != null)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(themeDirList[0].FullName + @"\theme.xml");

                    ShopConfigInfo shopConfigInfo = BSPConfig.ShopConfig;
                    if (type == 0)
                    {
                        shopConfigInfo.PCTheme = themeDirList[0].Name;
                    }
                    else if (type == 1)
                    {
                        shopConfigInfo.MobileTheme = themeDirList[0].Name;
                    }

                    BSPConfig.SaveShopConfig(shopConfigInfo);
                    AddAdminOperateLog("设置默认主题", "设置默认主题,主题为:" + doc.DocumentElement.Attributes["title"].Value);
                    return PromptView(Url.Action("theme"), "设置默认主题成功");
                }
            }
            return PromptView(Url.Action("theme"), "主题不存在");
        }

        /// <summary>
        /// 账号
        /// </summary>
        [HttpGet]
        public ActionResult Account()
        {
            ShopConfigInfo shopConfigInfo = BSPConfig.ShopConfig;

            AccountModel model = new AccountModel();
            model.RegType = StringToIntArray(shopConfigInfo.RegType);
            model.ReservedName = shopConfigInfo.ReservedName;
            model.RegTimeSpan = shopConfigInfo.RegTimeSpan;
            model.IsWebcomeMsg = shopConfigInfo.IsWebcomeMsg;
            model.WebcomeMsg = shopConfigInfo.WebcomeMsg;
            model.LoginType = StringToIntArray(shopConfigInfo.LoginType);
            model.ShadowName = shopConfigInfo.ShadowName;
            model.IsRemember = shopConfigInfo.IsRemember;
            model.LoginFailTimes = shopConfigInfo.LoginFailTimes;

            return View(model);
        }

        /// <summary>
        /// 账号
        /// </summary>
        [HttpPost]
        public ActionResult Account(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                ShopConfigInfo shopConfigInfo = BSPConfig.ShopConfig;

                shopConfigInfo.RegType = model.RegType == null ? "" : CommonHelper.IntArrayToString(model.RegType, "");
                shopConfigInfo.ReservedName = model.ReservedName ?? "";
                shopConfigInfo.RegTimeSpan = model.RegTimeSpan;
                shopConfigInfo.IsWebcomeMsg = model.IsWebcomeMsg;
                shopConfigInfo.WebcomeMsg = model.WebcomeMsg ?? "";
                shopConfigInfo.LoginType = model.LoginType == null ? "" : CommonHelper.IntArrayToString(model.LoginType, "");
                shopConfigInfo.ShadowName = model.ShadowName ?? "";
                shopConfigInfo.IsRemember = model.IsRemember;
                shopConfigInfo.LoginFailTimes = model.LoginFailTimes;

                BSPConfig.SaveShopConfig(shopConfigInfo);
                Emails.ResetShop();
                SMSes.ResetShop();
                AddAdminOperateLog("修改账号设置");
                return PromptView(Url.Action("account"), "修改账号设置成功");
            }
            return View(model);
        }

        private int[] StringToIntArray(string s)
        {
            if (s != null && s.Length > 0)
            {
                int[] array = new int[s.Length];
                for (int i = 0; i < s.Length; i++)
                {
                    array[i] = TypeHelper.ObjectToInt(s[i]);
                }
                return array;
            }

            return new int[0];
        }

        /// <summary>
        /// 上传
        /// </summary>
        [HttpGet]
        public ActionResult Upload()
        {
            ShopConfigInfo shopConfigInfo = BSPConfig.ShopConfig;

            UploadModel model = new UploadModel();
            model.UploadServer = shopConfigInfo.UploadServer;
            model.UploadImgType = shopConfigInfo.UploadImgType;
            model.UploadImgSize = shopConfigInfo.UploadImgSize / 1000;
            model.WatermarkType = shopConfigInfo.WatermarkType;
            model.WatermarkQuality = shopConfigInfo.WatermarkQuality;
            model.WatermarkPosition = shopConfigInfo.WatermarkPosition;
            model.WatermarkImg = shopConfigInfo.WatermarkImg;
            model.WatermarkImgOpacity = shopConfigInfo.WatermarkImgOpacity;
            model.WatermarkText = shopConfigInfo.WatermarkText;
            model.WatermarkTextFont = shopConfigInfo.WatermarkTextFont;
            model.WatermarkTextSize = shopConfigInfo.WatermarkTextSize;
            model.BrandThumbSize = shopConfigInfo.BrandThumbSize;
            model.ProductShowThumbSize = shopConfigInfo.ProductShowThumbSize;
            model.UserAvatarThumbSize = shopConfigInfo.UserAvatarThumbSize;
            model.UserRankAvatarThumbSize = shopConfigInfo.UserRankAvatarThumbSize;

            LoadFont();
            return View(model);
        }

        /// <summary>
        /// 上传
        /// </summary>
        [HttpPost]
        public ActionResult Upload(UploadModel model)
        {
            if (ModelState.IsValid)
            {
                ShopConfigInfo shopConfigInfo = BSPConfig.ShopConfig;

                shopConfigInfo.UploadServer = model.UploadServer == null ? "" : model.UploadServer;
                shopConfigInfo.UploadImgType = model.UploadImgType;
                shopConfigInfo.UploadImgSize = model.UploadImgSize * 1000;
                shopConfigInfo.WatermarkType = model.WatermarkType;
                shopConfigInfo.WatermarkQuality = model.WatermarkQuality;
                shopConfigInfo.WatermarkPosition = model.WatermarkPosition;
                shopConfigInfo.WatermarkImg = model.WatermarkImg == null ? "" : model.WatermarkImg;
                shopConfigInfo.WatermarkImgOpacity = model.WatermarkImgOpacity;
                shopConfigInfo.WatermarkText = model.WatermarkText == null ? "" : model.WatermarkText;
                shopConfigInfo.WatermarkTextFont = model.WatermarkTextFont;
                shopConfigInfo.WatermarkTextSize = model.WatermarkTextSize;
                shopConfigInfo.BrandThumbSize = model.BrandThumbSize;
                shopConfigInfo.ProductShowThumbSize = model.ProductShowThumbSize;
                shopConfigInfo.UserAvatarThumbSize = model.UserAvatarThumbSize;
                shopConfigInfo.UserRankAvatarThumbSize = model.UserRankAvatarThumbSize;

                BSPConfig.SaveShopConfig(shopConfigInfo);
                Emails.ResetShop();
                SMSes.ResetShop();
                AddAdminOperateLog("修改上传设置");
                return PromptView(Url.Action("upload"), "修改上传设置成功");
            }

            LoadFont();
            return View(model);
        }

        private void LoadFont()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            InstalledFontCollection fontList = new InstalledFontCollection();
            foreach (FontFamily family in fontList.Families)
                itemList.Add(new SelectListItem() { Text = family.Name, Value = family.Name });
            ViewData["fontList"] = itemList;
        }

        /// <summary>
        /// 性能
        /// </summary>
        [HttpGet]
        public ActionResult Performance()
        {
            ShopConfigInfo shopConfigInfo = BSPConfig.ShopConfig;

            PerformanceModel model = new PerformanceModel();
            model.ImageCDN = shopConfigInfo.ImageCDN;
            model.CSSCDN = shopConfigInfo.CSSCDN;
            model.ScriptCDN = shopConfigInfo.ScriptCDN;
            model.OnlineUserExpire = shopConfigInfo.OnlineUserExpire;
            model.UpdateOnlineTimeSpan = shopConfigInfo.UpdateOnlineTimeSpan;
            model.MaxOnlineCount = shopConfigInfo.MaxOnlineCount;
            model.OnlineCountExpire = shopConfigInfo.OnlineCountExpire;
            model.UpdatePVStatTimespan = shopConfigInfo.UpdatePVStatTimespan;
            model.IsStatBrowser = shopConfigInfo.IsStatBrowser;
            model.IsStatOS = shopConfigInfo.IsStatOS;
            model.IsStatRegion = shopConfigInfo.IsStatRegion;

            return View(model);
        }

        /// <summary>
        /// 性能
        /// </summary>
        [HttpPost]
        public ActionResult Performance(PerformanceModel model)
        {
            if (ModelState.IsValid)
            {
                ShopConfigInfo shopConfigInfo = BSPConfig.ShopConfig;

                shopConfigInfo.ImageCDN = model.ImageCDN == null ? "" : model.ImageCDN;
                shopConfigInfo.CSSCDN = model.CSSCDN == null ? "" : model.CSSCDN;
                shopConfigInfo.ScriptCDN = model.ScriptCDN == null ? "" : model.ScriptCDN;
                shopConfigInfo.OnlineUserExpire = model.OnlineUserExpire;
                shopConfigInfo.UpdateOnlineTimeSpan = model.UpdateOnlineTimeSpan;
                shopConfigInfo.MaxOnlineCount = model.MaxOnlineCount;
                shopConfigInfo.OnlineCountExpire = model.OnlineCountExpire;
                shopConfigInfo.UpdatePVStatTimespan = model.UpdatePVStatTimespan;
                shopConfigInfo.IsStatBrowser = model.IsStatBrowser;
                shopConfigInfo.IsStatOS = model.IsStatOS;
                shopConfigInfo.IsStatRegion = model.IsStatRegion;

                BSPConfig.SaveShopConfig(shopConfigInfo);
                Emails.ResetShop();
                SMSes.ResetShop();
                AddAdminOperateLog("修改性能设置");
                return PromptView(Url.Action("performance"), "修改性能设置成功");
            }
            return View(model);
        }

        /// <summary>
        /// 访问
        /// </summary>
        [HttpGet]
        public ActionResult Access()
        {
            ShopConfigInfo shopConfigInfo = BSPConfig.ShopConfig;

            AccessModel model = new AccessModel();
            model.IsClosed = shopConfigInfo.IsClosed;
            model.CloseReason = shopConfigInfo.CloseReason;
            model.BanAccessTime = shopConfigInfo.BanAccessTime;
            model.BanAccessIP = shopConfigInfo.BanAccessIP;
            model.AllowAccessIP = shopConfigInfo.AllowAccessIP;
            model.AdminAllowAccessIP = shopConfigInfo.AdminAllowAccessIP;
            model.SecretKey = shopConfigInfo.SecretKey;
            model.CookieDomain = shopConfigInfo.CookieDomain;
            model.RandomLibrary = shopConfigInfo.RandomLibrary;
            model.VerifyPages = StringHelper.SplitString(shopConfigInfo.VerifyPages);
            model.IgnoreWords = shopConfigInfo.IgnoreWords;
            model.AllowEmailProvider = shopConfigInfo.AllowEmailProvider;
            model.BanEmailProvider = shopConfigInfo.BanEmailProvider;

            ViewData["verifyPages"] = shopConfigInfo.VerifyPages;
            return View(model);
        }

        /// <summary>
        /// 访问
        /// </summary>
        [HttpPost]
        public ActionResult Access(AccessModel model)
        {
            if (ModelState.IsValid)
            {
                ShopConfigInfo shopConfigInfo = BSPConfig.ShopConfig;

                shopConfigInfo.IsClosed = model.IsClosed;
                shopConfigInfo.CloseReason = model.CloseReason == null ? "" : model.CloseReason;
                shopConfigInfo.BanAccessTime = model.BanAccessTime == null ? "" : model.BanAccessTime;
                shopConfigInfo.BanAccessIP = model.BanAccessIP == null ? "" : model.BanAccessIP;
                shopConfigInfo.AllowAccessIP = model.AllowAccessIP == null ? "" : model.AllowAccessIP;
                shopConfigInfo.AdminAllowAccessIP = model.AdminAllowAccessIP == null ? "" : model.AdminAllowAccessIP;
                shopConfigInfo.SecretKey = model.SecretKey;
                shopConfigInfo.CookieDomain = model.CookieDomain == null ? "" : model.CookieDomain.Trim('.');
                shopConfigInfo.RandomLibrary = model.RandomLibrary == null ? "" : model.RandomLibrary;
                shopConfigInfo.VerifyPages = CommonHelper.StringArrayToString(model.VerifyPages);
                shopConfigInfo.IgnoreWords = model.IgnoreWords == null ? "" : model.IgnoreWords;
                shopConfigInfo.AllowEmailProvider = model.AllowEmailProvider == null ? "" : model.AllowEmailProvider;
                shopConfigInfo.BanEmailProvider = model.BanEmailProvider == null ? "" : model.BanEmailProvider;

                BSPConfig.SaveShopConfig(shopConfigInfo);
                Emails.ResetShop();
                SMSes.ResetShop();
                Randoms.ResetRandomLibrary();
                FilterWords.ResetIgnoreWordsRegex();
                AddAdminOperateLog("修改访问控制");
                return PromptView(Url.Action("access"), "修改访问控制成功");
            }

            ViewData["verifyPages"] = CommonHelper.StringArrayToString(model.VerifyPages);
            return View(model);
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        [HttpGet]
        public ActionResult Email()
        {
            EmailConfigInfo emailConfigInfo = BSPConfig.EmailConfig;

            EmailModel model = new EmailModel();
            model.Host = emailConfigInfo.Host;
            model.Port = emailConfigInfo.Port;
            model.From = emailConfigInfo.From;
            model.FromName = emailConfigInfo.FromName;
            model.UserName = emailConfigInfo.UserName;
            model.Password = emailConfigInfo.Password;
            model.FindPwdBody = emailConfigInfo.FindPwdBody;
            model.SCVerifyBody = emailConfigInfo.SCVerifyBody;
            model.SCUpdateBody = emailConfigInfo.SCUpdateBody;
            model.WebcomeBody = emailConfigInfo.WebcomeBody;

            return View(model);
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        [HttpPost]
        public ActionResult Email(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                EmailConfigInfo emailConfigInfo = BSPConfig.EmailConfig;

                emailConfigInfo.Host = model.Host;
                emailConfigInfo.Port = model.Port;
                emailConfigInfo.From = model.From;
                emailConfigInfo.FromName = model.FromName;
                emailConfigInfo.UserName = model.UserName;
                emailConfigInfo.Password = model.Password;
                emailConfigInfo.FindPwdBody = model.FindPwdBody;
                emailConfigInfo.SCVerifyBody = model.SCVerifyBody;
                emailConfigInfo.SCUpdateBody = model.SCUpdateBody;
                emailConfigInfo.WebcomeBody = model.WebcomeBody;

                BSPConfig.SaveEmailConfig(emailConfigInfo);
                Emails.ResetEmail();
                AddAdminOperateLog("修改邮箱设置");
                return PromptView(Url.Action("email"), "修改邮箱设置成功");
            }
            return View(model);
        }

        /// <summary>
        /// 短信
        /// </summary>
        [HttpGet]
        public ActionResult SMS()
        {
            SMSConfigInfo smsConfigInfo = BSPConfig.SMSConfig;

            SMSModel model = new SMSModel();
            model.Url = smsConfigInfo.Url;
            model.UserName = smsConfigInfo.UserName;
            model.Password = smsConfigInfo.Password;
            model.FindPwdBody = smsConfigInfo.FindPwdBody;
            model.SCVerifyBody = smsConfigInfo.SCVerifyBody;
            model.SCUpdateBody = smsConfigInfo.SCUpdateBody;
            model.WebcomeBody = smsConfigInfo.WebcomeBody;

            return View(model);
        }

        /// <summary>
        /// 短信
        /// </summary>
        [HttpPost]
        public ActionResult SMS(SMSModel model)
        {
            if (ModelState.IsValid)
            {
                SMSConfigInfo smsConfigInfo = BSPConfig.SMSConfig;

                smsConfigInfo.Url = model.Url;
                smsConfigInfo.UserName = model.UserName;
                smsConfigInfo.Password = model.Password;
                smsConfigInfo.FindPwdBody = model.FindPwdBody;
                smsConfigInfo.SCVerifyBody = model.SCVerifyBody;
                smsConfigInfo.SCUpdateBody = model.SCUpdateBody;
                smsConfigInfo.WebcomeBody = model.WebcomeBody;

                BSPConfig.SaveSMSConfig(smsConfigInfo);
                SMSes.ResetSMS();
                AddAdminOperateLog("修改短信设置");
                return PromptView(Url.Action("sms"), "修改短信设置成功");
            }
            return View(model);
        }

        /// <summary>
        /// 积分
        /// </summary>
        [HttpGet]
        public ActionResult Credit()
        {
            CreditConfigInfo creditConfigInfo = BSPConfig.CreditConfig;

            CreditModel model = new CreditModel();

            model.PayCreditName = creditConfigInfo.PayCreditName;
            model.PayCreditPrice = creditConfigInfo.PayCreditPrice;
            model.DayMaxSendPayCredits = creditConfigInfo.DayMaxSendPayCredits;
            model.OrderMaxUsePayCredits = creditConfigInfo.OrderMaxUsePayCredits;
            model.RegisterPayCredits = creditConfigInfo.RegisterPayCredits;
            model.LoginPayCredits = creditConfigInfo.LoginPayCredits;
            model.VerifyEmailPayCredits = creditConfigInfo.VerifyEmailPayCredits;
            model.VerifyMobilePayCredits = creditConfigInfo.VerifyMobilePayCredits;
            model.CompleteUserInfoPayCredits = creditConfigInfo.CompleteUserInfoPayCredits;
            model.CompleteOrderPayCredits = creditConfigInfo.CompleteOrderPayCredits;
            model.ReviewProductPayCredits = creditConfigInfo.ReviewProductPayCredits;

            model.RankCreditName = creditConfigInfo.RankCreditName;
            model.DayMaxSendRankCredits = creditConfigInfo.DayMaxSendRankCredits;
            model.RegisterRankCredits = creditConfigInfo.RegisterRankCredits;
            model.LoginRankCredits = creditConfigInfo.LoginRankCredits;
            model.VerifyEmailRankCredits = creditConfigInfo.VerifyEmailRankCredits;
            model.VerifyMobileRankCredits = creditConfigInfo.VerifyMobileRankCredits;
            model.CompleteUserInfoRankCredits = creditConfigInfo.CompleteUserInfoRankCredits;
            model.CompleteOrderRankCredits = creditConfigInfo.CompleteOrderRankCredits;
            model.ReviewProductRankCredits = creditConfigInfo.ReviewProductRankCredits;

            return View(model);
        }

        /// <summary>
        /// 积分
        /// </summary>
        [HttpPost]
        public ActionResult Credit(CreditModel model)
        {
            if (ModelState.IsValid)
            {
                CreditConfigInfo creditConfigInfo = BSPConfig.CreditConfig;

                creditConfigInfo.PayCreditName = model.PayCreditName;
                creditConfigInfo.PayCreditPrice = model.PayCreditPrice;
                creditConfigInfo.DayMaxSendPayCredits = model.DayMaxSendPayCredits;
                creditConfigInfo.OrderMaxUsePayCredits = model.OrderMaxUsePayCredits;
                creditConfigInfo.RegisterPayCredits = model.RegisterPayCredits;
                creditConfigInfo.LoginPayCredits = model.LoginPayCredits;
                creditConfigInfo.VerifyEmailPayCredits = model.VerifyEmailPayCredits;
                creditConfigInfo.VerifyMobilePayCredits = model.VerifyMobilePayCredits;
                creditConfigInfo.CompleteUserInfoPayCredits = model.CompleteUserInfoPayCredits;
                creditConfigInfo.CompleteOrderPayCredits = model.CompleteOrderPayCredits;
                creditConfigInfo.ReviewProductPayCredits = model.ReviewProductPayCredits;

                creditConfigInfo.RankCreditName = model.RankCreditName;
                creditConfigInfo.DayMaxSendRankCredits = model.DayMaxSendRankCredits;
                creditConfigInfo.RegisterRankCredits = model.RegisterRankCredits;
                creditConfigInfo.LoginRankCredits = model.LoginRankCredits;
                creditConfigInfo.VerifyEmailRankCredits = model.VerifyEmailRankCredits;
                creditConfigInfo.VerifyMobileRankCredits = model.VerifyMobileRankCredits;
                creditConfigInfo.CompleteUserInfoRankCredits = model.CompleteUserInfoRankCredits;
                creditConfigInfo.CompleteOrderRankCredits = model.CompleteOrderRankCredits;
                creditConfigInfo.ReviewProductRankCredits = model.ReviewProductRankCredits;

                BSPConfig.SaveCreditConfig(creditConfigInfo);
                Credits.ResetCreditConfig();
                AddAdminOperateLog("修改积分设置");
                return PromptView(Url.Action("credit"), "修改积分设置成功");
            }
            return View(model);
        }

        /// <summary>
        /// 商城设置
        /// </summary>
        [HttpGet]
        public ActionResult Shop()
        {
            ShopConfigInfo shopConfigInfo = BSPConfig.ShopConfig;

            ShopModel model = new ShopModel();
            model.IsGuestSC = shopConfigInfo.IsGuestSC;
            model.SCSubmitType = shopConfigInfo.SCSubmitType;
            model.GuestSCCount = shopConfigInfo.GuestSCCount;
            model.MemberSCCount = shopConfigInfo.MemberSCCount;
            model.SCExpire = shopConfigInfo.SCExpire;
            model.OSNFormat = shopConfigInfo.OSNFormat;
            model.OnlinePayExpire = shopConfigInfo.OnlinePayExpire;
            model.OfflinePayExpire = shopConfigInfo.OfflinePayExpire;
            model.BroHisCount = shopConfigInfo.BroHisCount;
            model.MaxShipAddress = shopConfigInfo.MaxShipAddress;
            model.FavoriteCount = shopConfigInfo.FavoriteCount;

            return View(model);
        }

        /// <summary>
        /// 商城设置
        /// </summary>
        [HttpPost]
        public ActionResult Shop(ShopModel model)
        {
            if (ModelState.IsValid)
            {
                ShopConfigInfo shopConfigInfo = BSPConfig.ShopConfig;

                shopConfigInfo.IsGuestSC = model.IsGuestSC;
                shopConfigInfo.SCSubmitType = model.SCSubmitType;
                shopConfigInfo.GuestSCCount = model.GuestSCCount;
                shopConfigInfo.MemberSCCount = model.MemberSCCount;
                shopConfigInfo.SCExpire = model.SCExpire;
                shopConfigInfo.OSNFormat = model.OSNFormat;
                shopConfigInfo.OnlinePayExpire = model.OnlinePayExpire;
                shopConfigInfo.OfflinePayExpire = model.OfflinePayExpire;
                shopConfigInfo.BroHisCount = model.BroHisCount;
                shopConfigInfo.MaxShipAddress = model.MaxShipAddress;
                shopConfigInfo.FavoriteCount = model.FavoriteCount;

                BSPConfig.SaveShopConfig(shopConfigInfo);
                Emails.ResetShop();
                SMSes.ResetShop();
                AddAdminOperateLog("修改商城设置");
                return PromptView(Url.Action("shop"), "修改商城设置成功");
            }
            return View(model);
        }

        /// <summary>
        /// 打印订单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PrintOrder()
        {
            string content = System.IO.File.ReadAllText(IOHelper.GetMapPath("/administration/views/order/printorder.cshtml"), Encoding.UTF8);
            return View((object)content);
        }

        /// <summary>
        /// 打印订单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PrintOrder(string content)
        {
            System.IO.File.WriteAllText(IOHelper.GetMapPath("/administration/views/order/printorder.cshtml"), content, Encoding.UTF8);
            return PromptView(Url.Action("printorder"), "修改成功");
        }
    }
}
