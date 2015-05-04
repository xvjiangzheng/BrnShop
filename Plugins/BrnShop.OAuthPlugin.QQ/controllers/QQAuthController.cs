using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.OAuthPlugin.QQ;

using fastJSON;

namespace BrnShop.Web.Controllers
{
    /// <summary>
    /// 前台QQ开放授权控制器类
    /// </summary>
    public class QQOAuthController : BaseWebController
    {
        /// <summary>
        /// 登陆
        /// </summary>
        public ActionResult Login()
        {
            //返回url
            string returnUrl = WebHelper.GetQueryString("returnUrl");
            if (returnUrl.Length == 0)
                returnUrl = "/";

            if (WorkContext.ShopConfig.LoginType == "")
                return PromptView(returnUrl, "商城目前已经关闭登陆功能!");
            if (WorkContext.Uid > 0)
                return PromptView(returnUrl, "您已经登录，无须重复登录!");

            PluginSetInfo pluginSetInfo = PluginUtils.GetPluginSet();
            //生成随机值，防止CSRF攻击
            string salt = Randoms.CreateRandomValue(16);
            //获取Authorization Code地址
            string url = string.Format("{0}/oauth2.0/authorize?client_id={1}&response_type=code&redirect_uri=http://{2}{3}&state={4}",
                                        pluginSetInfo.AuthUrl, pluginSetInfo.AppKey, BSPConfig.ShopConfig.SiteUrl, Url.Action("CallBack"), salt);
            Sessions.SetItem(WorkContext.Sid, "qqAuthLoginSalt", salt);
            return Redirect(url);
        }

        /// <summary>
        /// 回调
        /// </summary>
        public ActionResult CallBack()
        {
            //返回url
            string returnUrl = WebHelper.GetQueryString("returnUrl");
            if (returnUrl.Length == 0)
                returnUrl = "/";

            if (WorkContext.ShopConfig.LoginType == "")
                return PromptView(returnUrl, "商城目前已经关闭登陆功能!");
            if (WorkContext.Uid > 0)
                return PromptView(returnUrl, "您已经登录，无须重复登录!");

            //返回的随机值
            string backSalt = WebHelper.GetQueryString("state");
            //Authorization Code
            string code = WebHelper.GetQueryString("code");
            //保存在session中随机值
            string salt = Sessions.GetValueString(WorkContext.Sid, "qqAuthLoginSalt");

            if (backSalt.Length > 0 && code.Length > 0 && salt.Length > 0 && backSalt == salt)
            {
                //清空session中随机值
                Sessions.SetItem(WorkContext.Sid, "qqAuthLoginSalt", null);

                PluginSetInfo pluginSetInfo = PluginUtils.GetPluginSet();

                //构建获取Access Token的参数
                string postData = string.Format("grant_type=authorization_code&code={0}&client_id={1}&client_secret={2}&redirect_uri=http://{3}{4}",
                                                 code, pluginSetInfo.AppKey, pluginSetInfo.AppSecret, BSPConfig.ShopConfig.SiteUrl, Url.Action("CallBack"));
                //发送获得Access Token的请求
                string result = WebHelper.GetRequestData(pluginSetInfo.AuthUrl + "/oauth2.0/token", postData);
                //将返回结果解析成参数列表
                NameValueCollection parmList = WebHelper.GetParmList(result);
                //Access Token值
                string access_token = parmList["access_token"];

                //通过上一步获取的Access Token，构建获得对应用户身份的OpenID的url
                string url = string.Format("{0}/oauth2.0/me?access_token={1}", pluginSetInfo.AuthUrl, access_token);
                //发送获得OpenID的请求
                result = WebHelper.GetRequestData(url, "get", null);
                //移除返回结果开头的“callback(”和结尾的“);”字符串
                string json = StringHelper.TrimEnd(StringHelper.TrimStart(result, "callback("), ");");
                //OpenID值
                string openId = JSON.ToObject<PartOAuthUser>(json).OpenId;


                //判断此用户是否已经存在
                int uid = OAuths.GetUidByOpenIdAndServer(openId, pluginSetInfo.Server);
                if (uid > 0)//存在时
                {
                    PartUserInfo partUserInfo = Users.GetPartUserById(uid);
                    //更新用户最后访问
                    Users.UpdateUserLastVisit(partUserInfo.Uid, DateTime.Now, WorkContext.IP, WorkContext.RegionId);
                    //更新购物车中用户id
                    Carts.UpdateCartUidBySid(partUserInfo.Uid, WorkContext.Sid);
                    ShopUtils.SetUserCookie(partUserInfo, -1);

                    return Redirect("/");
                }
                else
                {
                    //获取用户信息的url
                    url = string.Format("{0}/user/get_user_info?access_token={1}&oauth_consumer_key={2}&openid={3}",
                                         pluginSetInfo.AuthUrl, access_token, pluginSetInfo.AppKey, openId);
                    //发送获取用户信息的请求
                    result = WebHelper.GetRequestData(url, "get", null);
                    //将返回结果序列化为对象
                    OAuthUser oAuthUser = JSON.ToObject<OAuthUser>(result);
                    if (oAuthUser.Ret == 0)//当没有错误时
                    {
                        UserInfo userInfo = OAuths.CreateOAuthUser(oAuthUser.Nickname, pluginSetInfo.UNamePrefix, openId, pluginSetInfo.Server, WorkContext.RegionId);
                        if (userInfo != null)
                        {
                            //发放注册积分
                            Credits.SendRegisterCredits(ref userInfo, DateTime.Now);
                            //更新购物车中用户id
                            Carts.UpdateCartUidBySid(userInfo.Uid, WorkContext.Sid);
                            ShopUtils.SetUserCookie(userInfo, -1);
                            return Redirect("/");
                        }
                        else
                        {
                            return PartialView("用户创建失败");
                        }
                    }
                    else
                    {
                        return PartialView("QQ授权登陆失败");
                    }
                }
            }
            else
            {
                return Redirect("/");
            }
        }
    }

    public class PartOAuthUser
    {
        private string _openid = "";//开放id

        /// <summary>
        /// 开放id
        /// </summary>
        public string OpenId
        {
            get { return _openid; }
            set { _openid = value; }
        }
    }

    public class OAuthUser
    {
        private int _ret = -1;//状态码
        private string _msg = "";//错误信息
        private string _nickname = "";//用户昵称

        /// <summary>
        /// 状态码
        /// </summary>
        public int Ret
        {
            get { return _ret; }
            set { _ret = value; }
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Msg
        {
            get { return _msg; }
            set { _msg = value; }
        }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string Nickname
        {
            get { return _nickname; }
            set { _nickname = value; }
        }
    }
}
