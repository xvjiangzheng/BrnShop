using System;

using BrnShop.Core;

namespace BrnShop.Web.Framework
{
    /// <summary>
    /// PC后台工作上下文类
    /// </summary>
    public class AdminWorkContext
    {
        public ShopConfigInfo ShopConfig = BSPConfig.ShopConfig;//商城配置信息

        public bool IsHttpAjax;//当前请求是否为ajax请求

        public string IP;//用户ip

        public RegionInfo RegionInfo;//区域信息

        public int RegionId;//区域id

        public string Url;//当前url

        public string UrlReferrer;//上一次访问的url

        public string Sid;//用户sid

        public int Uid = -1;//用户id

        public string UserName;//用户名

        public string UserEmail;//用户邮箱

        public string UserMobile;//用户手机号

        public string NickName;//用户昵称

        public string Avatar;//用户头像

        public string Password;//用户密码

        public string EncryptPwd;//加密密码

        public PartUserInfo PartUserInfo;//用户信息

        public int UserRid = -1;//用户等级id

        public UserRankInfo UserRankInfo;//用户等级信息

        public string UserRTitle;//用户等级标题

        public int AdminGid = -1;//用户管理员组id

        public AdminGroupInfo AdminGroupInfo;//用户管理员组信息

        public string AdminGTitle;//管理员组标题

        public string Controller;//控制器

        public string Action;//动作方法

        public string PageKey;//页面标示符
    }
}
