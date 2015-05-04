using System;
using System.Web.Mvc;
using System.Web.Routing;

using BrnShop.Core;
using BrnShop.Services;

namespace BrnShop.Web.Framework
{
    /// <summary>
    /// 移动前台基础控制器类
    /// </summary>
    public class BaseMobileController : Controller
    {
        //工作上下文
        public MobileWorkContext WorkContext = new MobileWorkContext();

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.ValidateRequest = false;

            WorkContext.IsHttpAjax = WebHelper.IsAjax();
            WorkContext.IP = WebHelper.GetIP();
            WorkContext.RegionInfo = Regions.GetRegionByIP(WorkContext.IP);
            WorkContext.RegionId = WorkContext.RegionInfo.RegionId;
            WorkContext.Url = WebHelper.GetUrl();
            WorkContext.UrlReferrer = WebHelper.GetUrlReferrer();

            //获得用户唯一标示符sid
            WorkContext.Sid = ShopUtils.GetSidCookie();
            if (WorkContext.Sid.Length == 0)
            {
                //生成sid
                WorkContext.Sid = Sessions.GenerateSid();
                //将sid保存到cookie中
                ShopUtils.SetSidCookie(WorkContext.Sid);
            }

            PartUserInfo partUserInfo;

            //获得用户id
            int uid = ShopUtils.GetUidCookie();
            if (uid < 1)//当用户为游客时
            {
                //创建游客
                partUserInfo = Users.CreatePartGuest();
            }
            else//当用户为会员时
            {
                //获得保存在cookie中的密码
                string encryptPwd = ShopUtils.GetCookiePassword();
                //防止用户密码被篡改为危险字符
                if (encryptPwd.Length == 0 || !SecureHelper.IsBase64String(encryptPwd))
                {
                    //创建游客
                    partUserInfo = Users.CreatePartGuest();
                    encryptPwd = string.Empty;
                    ShopUtils.SetUidCookie(-1);
                    ShopUtils.SetCookiePassword("");
                }
                else
                {
                    partUserInfo = Users.GetPartUserByUidAndPwd(uid, ShopUtils.DecryptCookiePassword(encryptPwd));
                    if (partUserInfo != null)
                    {
                        //发放登陆积分
                        Credits.SendLoginCredits(ref partUserInfo, DateTime.Now);
                    }
                    else//当会员的账号或密码不正确时，将用户置为游客
                    {
                        partUserInfo = Users.CreatePartGuest();
                        encryptPwd = string.Empty;
                        ShopUtils.SetUidCookie(-1);
                        ShopUtils.SetCookiePassword("");
                    }
                }
                WorkContext.EncryptPwd = encryptPwd;
            }

            //设置用户等级
            if (UserRanks.IsBanUserRank(partUserInfo.UserRid) && partUserInfo.LiftBanTime <= DateTime.Now)
            {
                UserRankInfo userRankInfo = UserRanks.GetUserRankByCredits(partUserInfo.PayCredits);
                Users.UpdateUserRankByUid(partUserInfo.Uid, userRankInfo.UserRid);
                partUserInfo.UserRid = userRankInfo.UserRid;
            }

            WorkContext.PartUserInfo = partUserInfo;

            WorkContext.Uid = partUserInfo.Uid;
            WorkContext.UserName = partUserInfo.UserName;
            WorkContext.UserEmail = partUserInfo.Email;
            WorkContext.UserMobile = partUserInfo.Mobile;
            WorkContext.Password = partUserInfo.Password;
            WorkContext.NickName = partUserInfo.NickName;
            WorkContext.Avatar = partUserInfo.Avatar;
            WorkContext.PayCreditName = Credits.PayCreditName;
            WorkContext.PayCreditCount = partUserInfo.PayCredits;
            WorkContext.RankCreditName = Credits.RankCreditName;
            WorkContext.RankCreditCount = partUserInfo.RankCredits;

            WorkContext.UserRid = partUserInfo.UserRid;
            WorkContext.UserRankInfo = UserRanks.GetUserRankById(partUserInfo.UserRid);
            WorkContext.UserRTitle = WorkContext.UserRankInfo.Title;
            //设置用户管理员组
            WorkContext.AdminGid = partUserInfo.AdminGid;
            WorkContext.AdminGroupInfo = AdminGroups.GetAdminGroupById(partUserInfo.AdminGid);
            WorkContext.AdminGTitle = WorkContext.AdminGroupInfo.Title;

            //设置当前控制器类名
            WorkContext.Controller = RouteData.Values["controller"].ToString().ToLower();
            //设置当前动作方法名
            WorkContext.Action = RouteData.Values["action"].ToString().ToLower();
            WorkContext.PageKey = string.Format("/{0}/{1}", WorkContext.Controller, WorkContext.Action);

            //当前商城主题
            WorkContext.Theme = WorkContext.ShopConfig.MobileTheme;
            //设置图片cdn
            WorkContext.ImageCDN = WorkContext.ShopConfig.ImageCDN;
            //设置csscdn
            WorkContext.CSSCDN = WorkContext.ShopConfig.CSSCDN;
            //设置脚本cdn
            WorkContext.ScriptCDN = WorkContext.ShopConfig.ScriptCDN;

            //在线总人数
            WorkContext.OnlineUserCount = OnlineUsers.GetOnlineUserCount();
            //在线游客数
            WorkContext.OnlineGuestCount = OnlineUsers.GetOnlineGuestCount();
            //在线会员数
            WorkContext.OnlineMemberCount = WorkContext.OnlineUserCount - WorkContext.OnlineGuestCount;
            //搜索词
            WorkContext.SearchWord = string.Empty;
            //购物车中商品数量
            WorkContext.CartProductCount = Carts.GetCartProductCountCookie();
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            //不能应用在子方法上
            if (filterContext.IsChildAction)
                return;

            //商城已经关闭
            if (WorkContext.ShopConfig.IsClosed == 1 && WorkContext.AdminGid == 1 && WorkContext.PageKey != Url.Action("login", "account") && WorkContext.PageKey != Url.Action("logout", "account"))
            {
                filterContext.Result = PromptView(WorkContext.ShopConfig.CloseReason);
                return;
            }

            //当前时间为禁止访问时间
            if (ValidateHelper.BetweenPeriod(WorkContext.ShopConfig.BanAccessTime) && WorkContext.AdminGid == 1 && WorkContext.PageKey != Url.Action("login", "account") && WorkContext.PageKey != Url.Action("logout", "account"))
            {
                filterContext.Result = PromptView("当前时间不能访问本商城");
                return;
            }

            //当用户ip在被禁止的ip列表时
            if (ValidateHelper.InIPList(WorkContext.IP, WorkContext.ShopConfig.BanAccessIP))
            {
                filterContext.Result = PromptView("您的IP被禁止访问本商城");
                return;
            }

            //当用户ip不在允许的ip列表时
            if (!string.IsNullOrEmpty(WorkContext.ShopConfig.AllowAccessIP) && !ValidateHelper.InIPList(WorkContext.IP, WorkContext.ShopConfig.AllowAccessIP))
            {
                filterContext.Result = PromptView("您的IP被禁止访问本商城");
                return;
            }

            //当用户IP被禁止时
            if (BannedIPs.CheckIP(WorkContext.IP))
            {
                filterContext.Result = PromptView("您的IP被禁止访问本商城");
                return;
            }

            //当用户等级是禁止访问等级时
            if (WorkContext.UserRid == 1)
            {
                filterContext.Result = PromptView("您的账号当前被锁定,不能访问");
                return;
            }

            //判断目前访问人数是否达到允许的最大人数
            if (WorkContext.OnlineUserCount > WorkContext.ShopConfig.MaxOnlineCount && WorkContext.AdminGid == 1 && (WorkContext.Controller != "account" && (WorkContext.Action != "login" || WorkContext.Action != "logout")))
            {
                filterContext.Result = PromptView("商城人数达到访问上限, 请稍等一会再访问！");
                return;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //不能应用在子方法上
            if (filterContext.IsChildAction)
                return;

            //当用户为会员时,更新用户的在线时间
            if (WorkContext.Uid > 0)
                Users.UpdateUserOnlineTime(WorkContext.Uid);

            //更新在线用户
            Asyn.UpdateOnlineUser(WorkContext.Uid, WorkContext.Sid, WorkContext.NickName, WorkContext.IP, WorkContext.RegionId);
            //更新PV统计
            if (WorkContext.ShopConfig.UpdatePVStatTimespan != 0)
                Asyn.UpdatePVStat(WorkContext.Uid, WorkContext.RegionId, WebHelper.GetBrowserType(), WebHelper.GetOSType());
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            ShopUtils.WriteLogFile(filterContext.Exception);
            if (WorkContext.IsHttpAjax)
                filterContext.Result = AjaxResult("error", "系统错误,请联系管理员");
            else
                filterContext.Result = new ViewResult() { ViewName = "error" };
        }

        /// <summary>
        /// 获得路由中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        protected string GetRouteString(string key, string defaultValue)
        {
            object value = RouteData.Values[key];
            if (value != null)
                return value.ToString();
            else
                return defaultValue;
        }

        /// <summary>
        /// 获得路由中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        protected string GetRouteString(string key)
        {
            return GetRouteString(key, "");
        }

        /// <summary>
        /// 获得路由中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        protected int GetRouteInt(string key, int defaultValue)
        {
            return TypeHelper.ObjectToInt(RouteData.Values[key], defaultValue);
        }

        /// <summary>
        /// 获得路由中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        protected int GetRouteInt(string key)
        {
            return GetRouteInt(key, 0);
        }

        /// <summary>
        /// 提示信息视图
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        protected ViewResult PromptView(string message)
        {
            return View("prompt", new PromptModel(message));
        }

        /// <summary>
        /// 提示信息视图
        /// </summary>
        /// <param name="backUrl">返回地址</param>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        protected ViewResult PromptView(string backUrl, string message)
        {
            return View("prompt", new PromptModel(backUrl, message));
        }

        /// <summary>
        /// ajax请求结果
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        protected ActionResult AjaxResult(string state, string content)
        {
            return AjaxResult(state, content, false);
        }

        /// <summary>
        /// ajax请求结果
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="content">内容</param>
        /// <param name="isObject">是否为对象</param>
        /// <returns></returns>
        protected ActionResult AjaxResult(string state, string content, bool isObject)
        {
            return Content(string.Format("{0}\"state\":\"{1}\",\"content\":{2}{3}{4}{5}", "{", state, isObject ? "" : "\"", content, isObject ? "" : "\"", "}"));
        }
    }
}
