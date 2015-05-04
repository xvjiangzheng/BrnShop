using System;
using System.Web;
using System.Data;
using System.Drawing;
using System.Web.Mvc;
using System.Drawing.Text;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using BrnShop.Core;

namespace BrnShop.Web.Admin.Models
{
    /// <summary>
    /// 站点模型类
    /// </summary>
    public class SiteModel
    {
        public string ShopName { get; set; }
        [AllowHtml]
        public string SiteUrl { get; set; }
        public string SiteTitle { get; set; }
        public string SEOKeyword { get; set; }
        public string SEODescription { get; set; }
        public string ICP { get; set; }
        [AllowHtml]
        public string Script { get; set; }
        [Range(0, 1, ErrorMessage = "请选择正确的选项")]
        public int IsLicensed { get; set; }
    }

    /// <summary>
    /// 主题模型类
    /// </summary>
    public class ThemeModel
    {
        /// <summary>
        /// PC主题列表
        /// </summary>
        public DataTable PCThemeList { get; set; }
        /// <summary>
        /// 默认PC主题
        /// </summary>
        public string DefaultPCTheme { get; set; }
        /// <summary>
        /// 移动主题列表
        /// </summary>
        public DataTable MobileThemeList { get; set; }
        /// <summary>
        /// 默认移动主题
        /// </summary>
        public string DefaultMobileTheme { get; set; }
    }

    /// <summary>
    /// 账号模型类
    /// </summary>
    public class AccountModel : IValidatableObject
    {
        public int[] RegType { get; set; }

        public string ReservedName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "注册时间间隔不能为负数")]
        [DisplayName("注册时间间隔")]
        public int RegTimeSpan { get; set; }

        [Range(0, 1, ErrorMessage = "请选择正确的类型")]
        [DisplayName("是否发送欢迎信息")]
        public int IsWebcomeMsg { get; set; }

        [AllowHtml]
        public string WebcomeMsg { get; set; }

        public int[] LoginType { get; set; }

        public string ShadowName { get; set; }

        [Range(0, 1, ErrorMessage = "请选择正确的类型")]
        [DisplayName("是否记住密码")]
        public int IsRemember { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "登陆失败次数不能小于0")]
        [DisplayName("登陆失败次数")]
        public int LoginFailTimes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errorList = new List<ValidationResult>();

            if (IsWebcomeMsg == 1 && string.IsNullOrWhiteSpace(WebcomeMsg))
                errorList.Add(new ValidationResult("欢迎信息不能为空!", new string[] { "WebcomeMsg" }));

            return errorList;
        }
    }

    /// <summary>
    /// 上传模型类
    /// </summary>
    public class UploadModel : IValidatableObject
    {
        /// <summary>
        /// 上传服务器
        /// </summary>
        public string UploadServer { get; set; }

        [Required(ErrorMessage = "图片类型不能为空")]
        public string UploadImgType { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "图片大小必须大于0")]
        [DisplayName("图片大小")]
        public int UploadImgSize { get; set; }

        [Range(0, 2, ErrorMessage = "请选择正确的水印类型")]
        [DisplayName("水印类型")]
        public int WatermarkType { get; set; }

        [Range(0, 100, ErrorMessage = "水印质量只能位于0到100")]
        [DisplayName("水印质量")]
        public int WatermarkQuality { get; set; }

        [Range(1, 9, ErrorMessage = "请选择正确的水印位置")]
        [DisplayName("水印位置")]
        public int WatermarkPosition { get; set; }

        public string WatermarkImg { get; set; }

        [Range(1, 10, ErrorMessage = "水印图片透明度必须位于1到10之间")]
        [DisplayName("水印图片透明度")]
        public int WatermarkImgOpacity { get; set; }

        public string WatermarkText { get; set; }

        public string WatermarkTextFont { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "水印文字大小必须大于0")]
        [DisplayName("水印文字大小")]
        public int WatermarkTextSize { get; set; }

        /// <summary>
        /// 品牌缩略图大小
        /// </summary>
        [Required(ErrorMessage = "请输入内容")]
        public string BrandThumbSize { get; set; }

        /// <summary>
        /// 商品展示缩略图大小
        /// </summary>
        [Required(ErrorMessage = "请输入内容")]
        public string ProductShowThumbSize { get; set; }

        /// <summary>
        /// 用户头像缩略图大小
        /// </summary>
        [Required(ErrorMessage = "请输入内容")]
        public string UserAvatarThumbSize { get; set; }

        /// <summary>
        /// 用户等级头像缩略图大小
        /// </summary>
        [Required(ErrorMessage = "请输入内容")]
        public string UserRankAvatarThumbSize { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errorList = new List<ValidationResult>();

            if (WatermarkType == 1 && string.IsNullOrWhiteSpace(WatermarkText))
                errorList.Add(new ValidationResult("水印文字不能为空!", new string[] { "WatermarkText" }));
            else if (WatermarkType == 2 && string.IsNullOrWhiteSpace(WatermarkImg))
                errorList.Add(new ValidationResult("水印图片不能为空!", new string[] { "WatermarkImg" }));


            return errorList;
        }
    }

    /// <summary>
    /// 性能模型类
    /// </summary>
    public class PerformanceModel
    {
        public string ImageCDN { get; set; }

        public string CSSCDN { get; set; }

        public string ScriptCDN { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "在线用户过期时间不能小于0")]
        [DisplayName("在线用户过期时间")]
        public int OnlineUserExpire { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "更新用户在线时间间隔不能小于0")]
        [DisplayName("更新用户在线时间间隔")]
        public int UpdateOnlineTimeSpan { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "最大在线用户不能小于0")]
        [DisplayName("最大在线人数")]
        public int MaxOnlineCount { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "在线人数缓存时间不能小于0")]
        [DisplayName("在线人数缓存时间")]
        public int OnlineCountExpire { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "更新PV统计时间间隔不能小于0")]
        [DisplayName("更新PV统计时间间隔")]
        public int UpdatePVStatTimespan { get; set; }

        [Range(0, 1, ErrorMessage = "请选择正确的选项")]
        [DisplayName("是否统计浏览器")]
        public int IsStatBrowser { get; set; }

        [Range(0, 1, ErrorMessage = "请选择正确的选项")]
        [DisplayName("是否统计操作系统")]
        public int IsStatOS { get; set; }

        [Range(0, 1, ErrorMessage = "请选择正确的选项")]
        [DisplayName("是否统计区域")]
        public int IsStatRegion { get; set; }
    }

    /// <summary>
    /// 访问模型类
    /// </summary>
    public class AccessModel : IValidatableObject
    {
        [Range(0, 1, ErrorMessage = "请选择正确的选项")]
        public int IsClosed { get; set; }

        [AllowHtml]
        public string CloseReason { get; set; }

        public string BanAccessTime { get; set; }

        public string BanAccessIP { get; set; }

        public string AllowAccessIP { get; set; }

        public string AdminAllowAccessIP { get; set; }

        [StringLength(32, MinimumLength = 8, ErrorMessage = "密钥长度必须大于7且小于33")]
        [Required(ErrorMessage = "密钥不能为空")]
        public string SecretKey { get; set; }

        public string CookieDomain { get; set; }

        public string RandomLibrary { get; set; }

        public string[] VerifyPages { get; set; }

        public string IgnoreWords { get; set; }

        public string AllowEmailProvider { get; set; }

        public string BanEmailProvider { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errorList = new List<ValidationResult>();

            if (IsClosed == 1 && string.IsNullOrWhiteSpace(CloseReason))
                errorList.Add(new ValidationResult("请填写商城关闭原因!", new string[] { "CloseReason" }));

            if (CookieDomain != null && (!CookieDomain.Contains(".") || !WebHelper.GetHost().Contains(CookieDomain)))
                errorList.Add(new ValidationResult("cookie域不合法!", new string[] { "CookieDomain" }));

            //if (!string.IsNullOrWhiteSpace(BanAccessTime))
            //{
            //    string[] timeList = StringHelper.SplitString(BanAccessTime, "\r\n");
            //    foreach (string time in timeList)
            //    {
            //        string[] startTimeAndEndTime = StringHelper.SplitString(time, "-");
            //        if (startTimeAndEndTime.Length == 2)
            //        {
            //            foreach (string item in startTimeAndEndTime)
            //            {
            //                string[] hourAndMinute = StringHelper.SplitString(item, ":");
            //                if (hourAndMinute.Length == 2)
            //                {
            //                    if (!CheckTime(hourAndMinute[0], hourAndMinute[1]))
            //                    {
            //                        errorList.Add(new ValidationResult("时间格式不正确!", new string[] { "BanAccessTime" }));
            //                        break;
            //                    }
            //                }
            //                else
            //                {
            //                    errorList.Add(new ValidationResult("时间格式不正确!", new string[] { "BanAccessTime" }));
            //                    break;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            errorList.Add(new ValidationResult("时间格式不正确!", new string[] { "BanAccessTime" }));
            //            break;
            //        }
            //    }
            //}

            return errorList;
        }

        private bool CheckTime(string hour, string minute)
        {
            if (hour.Length == 1 || hour.Length == 2)
            {
                int hourInt = TypeHelper.StringToInt(hour.Trim('0'));
                if (hourInt < 0 || hourInt > 23)
                    return false;
            }
            else
            {
                return false;
            }

            if (minute.Length == 1 || minute.Length == 2)
            {
                int minuteInt = TypeHelper.StringToInt(minute.Trim('0'));
                if (minuteInt < 0 || minuteInt > 59)
                    return false;
            }
            else
            {
                return false;
            }

            return true;
        }
    }

    /// <summary>
    /// 邮箱模型类
    /// </summary>
    public class EmailModel
    {
        [Required(ErrorMessage = "主机不能为空")]
        public string Host { get; set; }

        [Required(ErrorMessage = "端口不能为空")]
        public int Port { get; set; }

        [Required(ErrorMessage = "发送邮箱不能为空")]
        public string From { get; set; }

        [Required(ErrorMessage = "发送邮箱昵称不能为空")]
        public string FromName { get; set; }

        [Required(ErrorMessage = "账号不能为空")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "内容不能为空")]
        public string FindPwdBody { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "内容不能为空")]
        public string SCVerifyBody { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "内容不能为空")]
        public string SCUpdateBody { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "内容不能为空")]
        public string WebcomeBody { get; set; }
    }

    /// <summary>
    /// 短信模型类
    /// </summary>
    public class SMSModel
    {
        [Required(ErrorMessage = "地址不能为空")]
        public string Url { get; set; }

        [Required(ErrorMessage = "账号不能为空")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }

        [Required(ErrorMessage = "内容不能为空")]
        public string FindPwdBody { get; set; }

        [Required(ErrorMessage = "内容不能为空")]
        public string SCVerifyBody { get; set; }

        [Required(ErrorMessage = "内容不能为空")]
        public string SCUpdateBody { get; set; }

        [Required(ErrorMessage = "内容不能为空")]
        public string WebcomeBody { get; set; }
    }

    /// <summary>
    /// 积分模型类
    /// </summary>
    public class CreditModel
    {
        /// <summary>
        /// 支付积分名称
        /// </summary>
        [Required(ErrorMessage = "支付积分名称不能为空")]
        public string PayCreditName { get; set; }
        /// <summary>
        /// 支付积分价格(单位为100个)
        /// </summary>
        [Range(1, 100, ErrorMessage = "支付积分价格必须位于1和100之间")]
        [DisplayName("支付积分价格")]
        public int PayCreditPrice { get; set; }
        /// <summary>
        /// 每天最大发放支付积分
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "每天最大发放支付积分不能小于0")]
        [DisplayName("每天最大发放支付积分")]
        public int DayMaxSendPayCredits { get; set; }
        /// <summary>
        /// 每笔订单最大使用支付积分
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "每笔订单最大使用支付积分不能小于0")]
        [DisplayName("每笔订单最大使用支付积分")]
        public int OrderMaxUsePayCredits { get; set; }
        /// <summary>
        /// 注册支付积分
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "注册支付积分不能小于0")]
        [DisplayName("注册支付积分")]
        public int RegisterPayCredits { get; set; }
        /// <summary>
        /// 每天登陆支付积分
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "每天登陆支付积分不能小于0")]
        [DisplayName("每天登陆支付积分")]
        public int LoginPayCredits { get; set; }
        /// <summary>
        /// 验证邮箱支付积分
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "验证邮箱支付积分不能小于0")]
        [DisplayName("验证邮箱支付积分")]
        public int VerifyEmailPayCredits { get; set; }
        /// <summary>
        /// 验证手机支付积分
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "验证手机支付积分不能小于0")]
        [DisplayName("验证手机支付积分")]
        public int VerifyMobilePayCredits { get; set; }
        /// <summary>
        /// 完善用户信息支付积分
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "完善用户信息支付积分不能小于0")]
        [DisplayName("完善用户信息支付积分")]
        public int CompleteUserInfoPayCredits { get; set; }
        /// <summary>
        /// 完成订单支付积分(以订单金额的百分比计算)
        /// </summary>
        [Range(0, 100, ErrorMessage = "完成订单支付积分必须位于0和100之间")]
        [DisplayName("完成订单支付积分")]
        public int CompleteOrderPayCredits { get; set; }
        /// <summary>
        /// 评价商品支付积分
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "评价商品支付积分不能小于0")]
        [DisplayName("评价商品支付积分")]
        public int ReviewProductPayCredits { get; set; }

        /// <summary>
        /// 等级积分名称
        /// </summary>
        [Required(ErrorMessage = "等级积分名称不能为空")]
        public string RankCreditName { get; set; }
        /// <summary>
        /// 每天最大发放等级积分
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "每天最大发放等级积分不能小于0")]
        [DisplayName("每天最大发放等级积分")]
        public int DayMaxSendRankCredits { get; set; }
        /// <summary>
        /// 注册等级积分
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "注册等级积分不能小于0")]
        [DisplayName("注册等级积分")]
        public int RegisterRankCredits { get; set; }
        /// <summary>
        /// 每天登陆等级积分
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "每天登陆等级积分不能小于0")]
        [DisplayName("每天登陆等级积分")]
        public int LoginRankCredits { get; set; }
        /// <summary>
        /// 验证邮箱等级积分
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "验证邮箱等级积分不能小于0")]
        [DisplayName("验证邮箱等级积分")]
        public int VerifyEmailRankCredits { get; set; }
        /// <summary>
        /// 验证手机等级积分
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "验证手机等级积分不能小于0")]
        [DisplayName("验证手机等级积分")]
        public int VerifyMobileRankCredits { get; set; }
        /// <summary>
        /// 完善用户信息等级积分
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "完善用户信息等级积分不能小于0")]
        [DisplayName("完善用户信息等级积分")]
        public int CompleteUserInfoRankCredits { get; set; }
        /// <summary>
        /// 完成订单等级积分(以订单金额的百分比计算)
        /// </summary>
        [Range(0, 100, ErrorMessage = "完成订单等级积分必须位于0和100之间")]
        [DisplayName("完成订单等级积分")]
        public int CompleteOrderRankCredits { get; set; }
        /// <summary>
        /// 评价商品等级积分
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "评价商品等级积分不能小于0")]
        [DisplayName("评价商品等级积分")]
        public int ReviewProductRankCredits { get; set; }
    }

    /// <summary>
    /// 商城模型类
    /// </summary>
    public class ShopModel
    {
        /// <summary>
        /// 是否允许游客使用购物车
        /// </summary>
        public int IsGuestSC { get; set; }
        /// <summary>
        /// 购物车的提交方式(0代表跳转到提示页面，1代表跳转到列表页面，2代表ajax提交)
        /// </summary>
        public int SCSubmitType { get; set; }
        /// <summary>
        /// 游客购物车最大商品数量
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "数量必须大于0")]
        [DisplayName("游客购物车最大商品数量")]
        public int GuestSCCount { get; set; }
        /// <summary>
        /// 会员购物车最大商品数量
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "数量必须大于0")]
        [DisplayName("会员购物车最大商品数量")]
        public int MemberSCCount { get; set; }
        /// <summary>
        /// 购物车过期时间(单位为天)
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "过期时间必须大于0")]
        [DisplayName("购物车过期时间")]
        public int SCExpire { get; set; }
        /// <summary>
        /// 订单编号格式
        /// </summary>
        [Required(ErrorMessage = "订单编号格式不能为空")]
        public string OSNFormat { get; set; }
        /// <summary>
        /// 在线支付过期时间
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "时间必须大于0")]
        [DisplayName("在线支付过期时间")]
        public int OnlinePayExpire { get; set; }
        /// <summary>
        /// 线下支付过期时间
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "时间必须大于0")]
        [DisplayName("线下支付过期时间")]
        public int OfflinePayExpire { get; set; }
        /// <summary>
        /// 浏览历史数量
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "数量不能小于0")]
        [DisplayName("浏览历史数量")]
        public int BroHisCount { get; set; }
        /// <summary>
        /// 浏览历史的过期时间(单位为天)
        /// </summary>
        /// <summary>
        /// 最大配送地址
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "数量必须大于0")]
        [DisplayName("最大配送地址")]
        public int MaxShipAddress { get; set; }
        /// <summary>
        /// 收藏夹最大容量
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "数量必须大于0")]
        [DisplayName("收藏夹最大容量")]
        public int FavoriteCount { get; set; }
    }
}
