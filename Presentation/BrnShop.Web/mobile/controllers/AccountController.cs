using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Mobile.Models;

namespace BrnShop.Web.Mobile.Controllers
{
    /// <summary>
    /// 账号控制器类
    /// </summary>
    public partial class AccountController : BaseMobileController
    {
        /// <summary>
        /// 登录
        /// </summary>
        public ActionResult Login()
        {
            string returnUrl = WebHelper.GetQueryString("returnUrl");
            if (returnUrl.Length == 0)
                returnUrl = Url.Action("index", "home");

            if (WorkContext.ShopConfig.LoginType == "")
                return PromptView(returnUrl, "商城目前已经关闭登陆功能!");
            if (WorkContext.Uid > 0)
                return PromptView(returnUrl, "您已经登录，无须重复登录!");
            if (WorkContext.ShopConfig.LoginFailTimes != 0 && LoginFailLogs.GetLoginFailTimesByIp(WorkContext.IP) >= WorkContext.ShopConfig.LoginFailTimes)
                return PromptView(returnUrl, "您已经输入错误" + WorkContext.ShopConfig.LoginFailTimes + "次密码，请15分钟后再登陆!");

            //get请求
            if (WebHelper.IsGet())
            {
                LoginModel model = new LoginModel();

                model.ReturnUrl = returnUrl;
                model.ShadowName = WorkContext.ShopConfig.ShadowName;
                model.IsRemember = WorkContext.ShopConfig.IsRemember == 1;
                model.IsVerifyCode = CommonHelper.IsInArray(WorkContext.PageKey, WorkContext.ShopConfig.VerifyPages);
                model.OAuthPluginList = Plugins.GetOAuthPluginList();

                return View(model);
            }

            //ajax请求
            string accountName = WebHelper.GetFormString(WorkContext.ShopConfig.ShadowName);
            string password = WebHelper.GetFormString("password");
            string verifyCode = WebHelper.GetFormString("verifyCode");
            int isRemember = WebHelper.GetFormInt("isRemember");

            StringBuilder errorList = new StringBuilder("[");
            //验证账户名
            if (string.IsNullOrWhiteSpace(accountName))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "账户名不能为空", "}");
            }
            else if (accountName.Length < 4 || accountName.Length > 50)
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "账户名必须大于3且不大于50个字符", "}");
            }
            else if ((!SecureHelper.IsSafeSqlString(accountName, false)))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "账户名不存在", "}");
            }

            //验证密码
            if (string.IsNullOrWhiteSpace(password))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "password", "密码不能为空", "}");
            }
            else if (password.Length < 4 || password.Length > 32)
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "password", "密码必须大于3且不大于32个字符", "}");
            }

            //验证验证码
            if (CommonHelper.IsInArray(WorkContext.PageKey, WorkContext.ShopConfig.VerifyPages))
            {
                if (string.IsNullOrWhiteSpace(verifyCode))
                {
                    errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "verifyCode", "验证码不能为空", "}");
                }
                else if (verifyCode.ToLower() != Sessions.GetValueString(WorkContext.Sid, "verifyCode"))
                {
                    errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "verifyCode", "验证码不正确", "}");
                }
            }

            //当以上验证全部通过时
            PartUserInfo partUserInfo = null;
            if (errorList.Length == 1)
            {
                if (BSPConfig.ShopConfig.LoginType.Contains("2") && ValidateHelper.IsEmail(accountName))//邮箱登陆
                {
                    partUserInfo = Users.GetPartUserByEmail(accountName);
                    if (partUserInfo == null)
                        errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "邮箱不存在", "}");
                }
                else if (BSPConfig.ShopConfig.LoginType.Contains("3") && ValidateHelper.IsMobile(accountName))//手机登陆
                {
                    partUserInfo = Users.GetPartUserByMobile(accountName);
                    if (partUserInfo == null)
                        errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "手机不存在", "}");
                }
                else if (BSPConfig.ShopConfig.LoginType.Contains("1"))//用户名登陆
                {
                    partUserInfo = Users.GetPartUserByName(accountName);
                    if (partUserInfo == null)
                        errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "用户名不存在", "}");
                }
                //判断密码是否正确
                if (partUserInfo != null && Users.CreateUserPassword(password, partUserInfo.Salt) != partUserInfo.Password)
                {
                    LoginFailLogs.AddLoginFailTimes(WorkContext.IP, DateTime.Now);//增加登陆失败次数
                    errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "password", "密码不正确", "}");
                }
            }
            if (errorList.Length > 1)//验证失败时
            {
                return AjaxResult("error", errorList.Remove(errorList.Length - 1, 1).Append("]").ToString(), true);
            }
            else//验证成功时
            {
                //当用户等级是禁止访问等级时
                if (partUserInfo.UserRid == 1)
                    return AjaxResult("lockuser", "您的账号当前被锁定,不能访问");

                //删除登陆失败日志
                LoginFailLogs.DeleteLoginFailLogByIP(WorkContext.IP);
                //更新用户最后访问
                Users.UpdateUserLastVisit(partUserInfo.Uid, DateTime.Now, WorkContext.IP, WorkContext.RegionId);
                //更新购物车中用户id
                Carts.UpdateCartUidBySid(partUserInfo.Uid, WorkContext.Sid);
                //将用户信息写入cookie中
                ShopUtils.SetUserCookie(partUserInfo, (WorkContext.ShopConfig.IsRemember == 1 && isRemember == 1) ? 30 : -1);

                return AjaxResult("success", "登录成功");
            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        public ActionResult Register()
        {
            string returnUrl = WebHelper.GetQueryString("returnUrl");
            if (returnUrl.Length == 0)
                returnUrl = Url.Action("index", "home");

            if (WorkContext.ShopConfig.RegType.Length == 0)
                return PromptView(returnUrl, "商城目前已经关闭注册功能!");
            if (WorkContext.Uid > 0)
                return PromptView(returnUrl, "你已经是本商城的注册用户，无需再注册!");
            if (WorkContext.ShopConfig.RegTimeSpan > 0)
            {
                DateTime registerTime = Users.GetRegisterTimeByRegisterIP(WorkContext.IP);
                if ((DateTime.Now - registerTime).Minutes <= WorkContext.ShopConfig.RegTimeSpan)
                    return PromptView(returnUrl, "你注册太频繁，请间隔一定时间后再注册!");
            }

            //get请求
            if (WebHelper.IsGet())
            {
                RegisterModel model = new RegisterModel();

                model.ReturnUrl = returnUrl;
                model.ShadowName = WorkContext.ShopConfig.ShadowName;
                model.IsVerifyCode = CommonHelper.IsInArray(WorkContext.PageKey, WorkContext.ShopConfig.VerifyPages);

                return View(model);
            }

            //ajax请求
            string accountName = WebHelper.GetFormString(WorkContext.ShopConfig.ShadowName).Trim().ToLower();
            string password = WebHelper.GetFormString("password");
            string confirmPwd = WebHelper.GetFormString("confirmPwd");
            string verifyCode = WebHelper.GetFormString("verifyCode");

            StringBuilder errorList = new StringBuilder("[");
            #region 验证

            //账号验证
            if (string.IsNullOrWhiteSpace(accountName))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "账户名不能为空", "}");
            }
            else if (accountName.Length < 4 || accountName.Length > 50)
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "账户名必须大于3且不大于50个字符", "}");
            }
            else if (accountName.Contains(" "))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "账户名中不允许包含空格", "}");
            }
            else if (accountName.Contains(":"))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "账户名中不允许包含冒号", "}");
            }
            else if (accountName.Contains("<"))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "账户名中不允许包含'<'符号", "}");
            }
            else if (accountName.Contains(">"))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "账户名中不允许包含'>'符号", "}");
            }
            else if ((!SecureHelper.IsSafeSqlString(accountName)))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "账户名已经存在", "}");
            }
            else if (CommonHelper.IsInArray(accountName, WorkContext.ShopConfig.ReservedName, "\n"))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "账户名已经存在", "}");
            }
            else if (FilterWords.IsContainWords(accountName))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "账户名包含禁止单词", "}");
            }

            //密码验证
            if (string.IsNullOrWhiteSpace(password))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "password", "密码不能为空", "}");
            }
            else if (password.Length < 4 || password.Length > 32)
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "password", "密码必须大于3且不大于32个字符", "}");
            }
            else if (password != confirmPwd)
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "password", "两次输入的密码不一样", "}");
            }

            //验证码验证
            if (CommonHelper.IsInArray(WorkContext.PageKey, WorkContext.ShopConfig.VerifyPages))
            {
                if (string.IsNullOrWhiteSpace(verifyCode))
                {
                    errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "verifyCode", "验证码不能为空", "}");
                }
                else if (verifyCode.ToLower() != Sessions.GetValueString(WorkContext.Sid, "verifyCode"))
                {
                    errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "verifyCode", "验证码不正确", "}");
                }
            }

            //其它验证
            int gender = WebHelper.GetFormInt("gender");
            if (gender < 0 || gender > 2)
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "gender", "请选择正确的性别", "}");

            string nickName = WebHelper.GetFormString("nickName");
            if (nickName.Length > 10)
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "nickName", "昵称的长度不能大于10", "}");
            }
            else if (FilterWords.IsContainWords(nickName))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "nickName", "昵称中包含禁止单词", "}");
            }

            if (WebHelper.GetFormString("realName").Length > 5)
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "realName", "真实姓名的长度不能大于5", "}");
            }

            string bday = WebHelper.GetFormString("bday");
            if (bday.Length == 0)
            {
                string bdayY = WebHelper.GetFormString("bdayY");
                string bdayM = WebHelper.GetFormString("bdayM");
                string bdayD = WebHelper.GetFormString("bdayD");
                bday = string.Format("{0}-{1}-{2}", bdayY, bdayM, bdayD);
            }
            if (bday.Length > 0 && bday != "--" && !ValidateHelper.IsDate(bday))
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "bday", "请选择正确的日期", "}");

            string idCard = WebHelper.GetFormString("idCard");
            if (idCard.Length > 0 && !ValidateHelper.IsIdCard(idCard))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "idCard", "请输入正确的身份证号", "}");
            }

            int regionId = WebHelper.GetFormInt("regionId");
            if (regionId > 0)
            {
                if (Regions.GetRegionById(regionId) == null)
                    errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "regionId", "请选择正确的地址", "}");
                if (WebHelper.GetFormString("address").Length > 75)
                {
                    errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "address", "详细地址的长度不能大于75", "}");
                }
            }

            if (WebHelper.GetFormString("bio").Length > 150)
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "bio", "简介的长度不能大于150", "}");
            }

            //当以上验证都通过时
            UserInfo userInfo = null;
            if (errorList.Length == 1)
            {
                if (WorkContext.ShopConfig.RegType.Contains("2") && ValidateHelper.IsEmail(accountName))//验证邮箱
                {
                    string emailProvider = CommonHelper.GetEmailProvider(accountName);
                    if (WorkContext.ShopConfig.AllowEmailProvider.Length != 0 && (!CommonHelper.IsInArray(emailProvider, WorkContext.ShopConfig.AllowEmailProvider, "\n")))
                    {
                        errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "不能使用'" + emailProvider + "'类型的邮箱", "}");
                    }
                    else if (CommonHelper.IsInArray(emailProvider, WorkContext.ShopConfig.BanEmailProvider, "\n"))
                    {
                        errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "不能使用'" + emailProvider + "'类型的邮箱", "}");
                    }
                    else if (Users.IsExistEmail(accountName))
                    {
                        errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "邮箱已经存在", "}");
                    }
                    else
                    {
                        userInfo = new UserInfo();
                        userInfo.UserName = string.Empty;
                        userInfo.Email = accountName;
                        userInfo.Mobile = string.Empty;
                    }
                }
                else if (WorkContext.ShopConfig.RegType.Contains("3") && ValidateHelper.IsMobile(accountName))//验证手机
                {
                    if (Users.IsExistMobile(accountName))
                    {
                        errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "手机号已经存在", "}");
                    }
                    else
                    {
                        userInfo = new UserInfo();
                        userInfo.UserName = string.Empty;
                        userInfo.Email = string.Empty;
                        userInfo.Mobile = accountName;
                    }
                }
                else if (WorkContext.ShopConfig.RegType.Contains("1"))//验证用户名
                {
                    if (BrnShop.Services.Users.IsExistUserName(accountName))
                    {
                        errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "用户名已经存在", "}");
                    }
                    else
                    {
                        userInfo = new UserInfo();
                        userInfo.UserName = accountName;
                        userInfo.Email = string.Empty;
                        userInfo.Mobile = string.Empty;
                    }
                }
            }

            #endregion

            if (errorList.Length > 1)//验证失败
            {
                return AjaxResult("error", errorList.Remove(errorList.Length - 1, 1).Append("]").ToString(), true);
            }
            else//验证成功
            {
                #region 绑定用户信息

                userInfo.Salt = Randoms.CreateRandomValue(6);
                userInfo.Password = Users.CreateUserPassword(password, userInfo.Salt);
                userInfo.UserRid = UserRanks.GetLowestUserRank().UserRid;
                userInfo.AdminGid = 1;//非管理员组
                if (nickName.Length > 0)
                    userInfo.NickName = WebHelper.HtmlEncode(nickName);
                else
                    userInfo.NickName = "bsp" + Randoms.CreateRandomValue(7);
                userInfo.Avatar = "";
                userInfo.PayCredits = 0;
                userInfo.RankCredits = 0;
                userInfo.VerifyEmail = 0;
                userInfo.VerifyMobile = 0;

                userInfo.LastVisitIP = WorkContext.IP;
                userInfo.LastVisitRgId = WorkContext.RegionId;
                userInfo.LastVisitTime = DateTime.Now;
                userInfo.RegisterIP = WorkContext.IP;
                userInfo.RegisterRgId = WorkContext.RegionId;
                userInfo.RegisterTime = DateTime.Now;

                userInfo.Gender = WebHelper.GetFormInt("gender");
                userInfo.RealName = WebHelper.HtmlEncode(WebHelper.GetFormString("realName"));
                userInfo.Bday = bday.Length > 0 ? TypeHelper.StringToDateTime(bday) : new DateTime(1900, 1, 1);
                userInfo.IdCard = WebHelper.GetFormString("idCard");
                userInfo.RegionId = WebHelper.GetFormInt("regionId");
                userInfo.Address = WebHelper.HtmlEncode(WebHelper.GetFormString("address"));
                userInfo.Bio = WebHelper.HtmlEncode(WebHelper.GetFormString("bio"));

                #endregion

                //创建用户
                userInfo.Uid = Users.CreateUser(userInfo);

                //添加用户失败
                if (userInfo.Uid < 1)
                    return AjaxResult("exception", "创建用户失败,请联系管理员");

                //发放注册积分
                Credits.SendRegisterCredits(ref userInfo, DateTime.Now);
                //更新购物车中用户id
                Carts.UpdateCartUidBySid(userInfo.Uid, WorkContext.Sid);
                //将用户信息写入cookie
                ShopUtils.SetUserCookie(userInfo, 0);

                //发送注册欢迎信息
                if (WorkContext.ShopConfig.IsWebcomeMsg == 1)
                {
                    if (userInfo.Email.Length > 0)
                        Emails.SendWebcomeEmail(userInfo.Email);
                    if (userInfo.Mobile.Length > 0)
                        SMSes.SendWebcomeSMS(userInfo.Mobile);
                }

                //同步上下午
                WorkContext.Uid = userInfo.Uid;
                WorkContext.UserName = userInfo.UserName;
                WorkContext.UserEmail = userInfo.Email;
                WorkContext.UserMobile = userInfo.Mobile;
                WorkContext.NickName = userInfo.NickName;

                return AjaxResult("success", "注册成功");
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        public ActionResult Logout()
        {
            if (WorkContext.Uid > 0)
            {
                WebHelper.DeleteCookie("bsp");
                Sessions.RemoverSession(WorkContext.Sid);
                OnlineUsers.DeleteOnlineUserBySid(WorkContext.Sid);
            }
            return Redirect(Url.Action("index", "home"));
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        public ActionResult FindPwd()
        {
            //get请求
            if (WebHelper.IsGet())
            {
                FindPwdModel model = new FindPwdModel();

                model.ShadowName = WorkContext.ShopConfig.ShadowName;
                model.IsVerifyCode = CommonHelper.IsInArray(WorkContext.PageKey, WorkContext.ShopConfig.VerifyPages);

                return View(model);
            }

            //ajax请求
            string accountName = WebHelper.GetFormString(WorkContext.ShopConfig.ShadowName);
            string verifyCode = WebHelper.GetFormString("verifyCode");

            StringBuilder errorList = new StringBuilder("[");
            //账号验证
            if (string.IsNullOrWhiteSpace(accountName))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "账户名不能为空", "}");
            }
            else if (accountName.Length < 4 || accountName.Length > 50)
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "账户名必须大于3且不大于50个字符", "}");
            }
            else if ((!SecureHelper.IsSafeSqlString(accountName)))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "账户名不存在", "}");
            }

            //验证码验证
            if (CommonHelper.IsInArray(WorkContext.PageKey, WorkContext.ShopConfig.VerifyPages))
            {
                if (string.IsNullOrWhiteSpace(verifyCode))
                {
                    errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "verifyCode", "验证码不能为空", "}");
                }
                else if (verifyCode.ToLower() != Sessions.GetValueString(WorkContext.Sid, "verifyCode"))
                {
                    errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "verifyCode", "验证码不正确", "}");
                }
            }

            //当以上验证都通过时
            PartUserInfo partUserInfo = null;
            if (ModelState.IsValid)
            {
                if (ValidateHelper.IsEmail(accountName))//验证邮箱
                {
                    partUserInfo = Users.GetPartUserByEmail(accountName);
                    if (partUserInfo == null)
                        errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "邮箱不存在", "}");
                }
                else if (ValidateHelper.IsMobile(accountName))//验证手机
                {
                    partUserInfo = Users.GetPartUserByMobile(accountName);
                    if (partUserInfo == null)
                        errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "手机号不存在", "}");
                }
                else//验证用户名
                {
                    partUserInfo = Users.GetPartUserByName(accountName);
                    if (partUserInfo == null)
                        errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "accountName", "用户名不存在", "}");
                }
            }

            if (errorList.Length == 1)
            {
                if (partUserInfo.Email.Length == 0 && partUserInfo.Mobile.Length == 0)
                    return AjaxResult("nocanfind", "由于您没有设置邮箱和手机，所以不能找回此账号的密码");

                return AjaxResult("success", Url.Action("selectfindpwdtype", new RouteValueDictionary { { "uid", partUserInfo.Uid } }));
            }
            else
            {
                return AjaxResult("error", errorList.Remove(errorList.Length - 1, 1).Append("]").ToString(), true);
            }
        }

        /// <summary>
        /// 选择找回密码方式
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectFindPwdType()
        {
            int uid = WebHelper.GetQueryInt("uid");
            PartUserInfo partUserInfo = Users.GetPartUserById(uid);
            if (partUserInfo == null)
                return PromptView("用户不存在");

            SelectFindPwdTypeModel model = new SelectFindPwdTypeModel();
            model.PartUserInfo = partUserInfo;
            return View(model);
        }

        /// <summary>
        /// 发送找回密码邮件
        /// </summary>
        public ActionResult SendFindPwdEmail()
        {
            int uid = WebHelper.GetQueryInt("uid");

            PartUserInfo partUserInfo = Users.GetPartUserById(uid);
            if (partUserInfo == null)
                return AjaxResult("nouser", "用户不存在");
            if (partUserInfo.Email.Length == 0)
                return AjaxResult("nocanfind", "由于您没有设置邮箱，所以不能通过邮箱找回此账号的密码");

            //发送找回密码邮件
            string v = ShopUtils.AESEncrypt(string.Format("{0},{1},{2}", partUserInfo.Uid, DateTime.Now, Randoms.CreateRandomValue(6)));
            string url = string.Format("http://{0}{1}", Request.Url.Authority, Url.Action("resetpwd", new RouteValueDictionary { { "v", v } }));
            Emails.SendFindPwdEmail(partUserInfo.Email, partUserInfo.UserName, url);
            return AjaxResult("success", "邮件已发送,请查收");
        }

        /// <summary>
        /// 发送找回密码短信
        /// </summary>
        public ActionResult SendFindPwdMobile()
        {
            int uid = WebHelper.GetQueryInt("uid");

            PartUserInfo partUserInfo = Users.GetPartUserById(uid);
            if (partUserInfo == null)
                return AjaxResult("nouser", "用户不存在");
            if (partUserInfo.Mobile.Length == 0)
                return AjaxResult("nocanfind", "由于您没有设置手机，所以不能通过手机找回此账号的密码");

            //发送找回密码短信
            string moibleCode = Randoms.CreateRandomValue(6);
            Sessions.SetItem(WorkContext.Sid, "findPwdMoibleCode", moibleCode);
            SMSes.SendFindPwdMobile(partUserInfo.Mobile, moibleCode);
            return AjaxResult("success", "验证码已发送,请查收");
        }

        /// <summary>
        /// 验证找回密码手机
        /// </summary>
        public ActionResult VerifyFindPwdMobile()
        {
            int uid = WebHelper.GetQueryInt("uid");
            string mobileCode = WebHelper.GetFormString("mobileCode");

            PartUserInfo partUserInfo = Users.GetPartUserById(uid);
            if (partUserInfo == null)
                return AjaxResult("nouser", "用户不存在");
            if (partUserInfo.Mobile.Length == 0)
                return AjaxResult("nocanfind", "由于您没有设置手机，所以不能通过手机找回此账号的密码");

            //检查手机码
            if (string.IsNullOrWhiteSpace(mobileCode))
            {
                return AjaxResult("emptymobilecode", "手机验证码不能为空");
            }
            else if (Sessions.GetValueString(WorkContext.Sid, "findPwdMoibleCode") != mobileCode)
            {
                return AjaxResult("wrongmobilecode", "手机验证码不正确");
            }

            string v = ShopUtils.AESEncrypt(string.Format("{0},{1},{2}", partUserInfo.Uid, DateTime.Now, Randoms.CreateRandomValue(6)));
            string url = string.Format("http://{0}{1}", Request.Url.Authority, Url.Action("resetpwd", new RouteValueDictionary { { "v", v } }));
            return AjaxResult("success", url);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        public ActionResult ResetPwd()
        {
            string v = WebHelper.GetQueryString("v");
            //解密字符串
            string realV = SecureHelper.AESDecrypt(v, WorkContext.ShopConfig.SecretKey);

            //数组第一项为uid，第二项为验证时间,第三项为随机值
            string[] result = StringHelper.SplitString(realV);
            if (result.Length != 3)
                return HttpNotFound();

            int uid = TypeHelper.StringToInt(result[0]);
            DateTime time = TypeHelper.StringToDateTime(result[1]);

            PartUserInfo partUserInfo = Users.GetPartUserById(uid);
            if (partUserInfo == null)
                return PromptView("用户不存在");
            //判断验证时间是否过时
            if (DateTime.Now.AddMinutes(-30) > time)
                return PromptView("此链接已经失效，请重新验证");

            //get请求
            if (WebHelper.IsGet())
            {
                ResetPwdModel model = new ResetPwdModel();
                model.V = v;
                return View(model);
            }

            //ajax请求
            string password = WebHelper.GetFormString("password");
            string confirmPwd = WebHelper.GetFormString("confirmPwd");

            StringBuilder errorList = new StringBuilder("[");
            //验证
            if (string.IsNullOrWhiteSpace(password))
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "password", "密码不能为空", "}");
            }
            else if (password.Length < 4 || password.Length > 32)
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "password", "密码必须大于3且不大于32个字符", "}");
            }
            else if (password != confirmPwd)
            {
                errorList.AppendFormat("{0}\"key\":\"{1}\",\"msg\":\"{2}\"{3},", "{", "confirmPwd", "两次输入的密码不一样", "}");
            }

            if (errorList.Length == 1)
            {
                //生成用户新密码
                string p = Users.CreateUserPassword(password, partUserInfo.Salt);
                //设置用户新密码
                Users.UpdateUserPasswordByUid(uid, p);
                //清空当前用户信息
                WebHelper.DeleteCookie("bsp");
                Sessions.RemoverSession(WorkContext.Sid);
                OnlineUsers.DeleteOnlineUserBySid(WorkContext.Sid);

                return AjaxResult("success", Url.Action("login"));
            }
            else
            {
                return AjaxResult("error", errorList.Remove(errorList.Length - 1, 1).Append("]").ToString(), true);
            }
        }
    }
}
