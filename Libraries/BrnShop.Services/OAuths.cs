using System;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 开放授权操作管理类
    /// </summary>
    public partial class OAuths
    {
        /// <summary>
        /// 创建开放授权用户
        /// </summary>
        /// <param name="oUserName">开放用户名</param>
        /// <param name="uNamePrefix">用户名前缀</param>
        /// <param name="openId">开放id</param>
        /// <param name="server">服务商</param>
        /// <param name="regionId">区域id</param>
        /// <returns></returns>
        public static UserInfo CreateOAuthUser(string oUserName, string uNamePrefix, string openId, string server, int regionId)
        {
            UserInfo userInfo = InitUser(oUserName, uNamePrefix, regionId);
            int uid = Users.CreateUser(userInfo);
            if (uid > 0)
            {
                OAuthInfo oauthInfo = new OAuthInfo()
                {
                    Uid = uid,
                    OpenId = openId,
                    Server = server
                };
                if (BrnShop.Data.OAuths.CreateOAuthUser(oauthInfo))
                {
                    userInfo.Uid = uid;
                }
                else
                {
                    userInfo = null;
                    Users.DeleteUserById(new int[] { uid });
                }
            }
            else
            {
                userInfo = null;
            }
            return userInfo;
        }

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="openId">开放id</param>
        /// <param name="server">服务商</param>
        /// <returns></returns>
        public static int GetUidByOpenIdAndServer(string openId, string server)
        {
            if (string.IsNullOrWhiteSpace(openId) || string.IsNullOrWhiteSpace(server))
                return -1;
            return BrnShop.Data.OAuths.GetUidByOpenIdAndServer(openId, server);
        }

        /// <summary>
        /// 获得开放授权用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static OAuthInfo GetOAuthUserByUid(int uid)
        {
            return BrnShop.Data.OAuths.GetOAuthUserByUid(uid);
        }

        /// <summary>
        /// 获得开放授权用户列表
        /// </summary>
        /// <param name="uidList">用户id列表</param>
        /// <returns></returns>
        public static List<OAuthInfo> GetOAuthUserList(int[] uidList)
        {
            return BrnShop.Data.OAuths.GetOAuthUserList(CommonHelper.IntArrayToString(uidList));
        }

        /// <summary>
        /// 获得有效的用户名
        /// </summary>
        /// <param name="oUserName">开放用户名</param>
        /// <param name="uNamePrefix">用户名前缀</param>
        /// <returns></returns>
        private static string GetValidUserName(string oUserName, string uNamePrefix)
        {
            if (string.IsNullOrWhiteSpace(uNamePrefix))
                uNamePrefix = "bsp";

            string validUserName = uNamePrefix + oUserName;
            int length = StringHelper.GetStringLength(validUserName);
            if (length > 20)
                validUserName = validUserName.Substring(0, 20);

            if (!Users.IsExistUserName(validUserName))
                return validUserName;

            validUserName = StringHelper.SubString(validUserName, 14);
            string temp = validUserName + Randoms.CreateRandomValue(6);
            while (Users.IsExistUserName(temp))
            {
                temp = validUserName + Randoms.CreateRandomValue(6);
            }
            return temp;
        }

        /// <summary>
        /// 初始化用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="uNamePrefix">用户名前缀</param>
        /// <param name="regionId">区域id</param>
        /// <returns></returns>
        private static UserInfo InitUser(string userName, string uNamePrefix, int regionId)
        {
            UserInfo userInfo = new UserInfo();

            userInfo.Salt = Randoms.CreateRandomValue(6);
            userInfo.Password = Users.CreateUserPassword(Randoms.CreateRandomValue(32, false), userInfo.Salt);
            userInfo.AdminGid = 1;//非管理员组
            userInfo.UserName = GetValidUserName(userName, uNamePrefix);
            userInfo.Email = string.Empty;
            userInfo.Mobile = string.Empty;
            userInfo.NickName = StringHelper.SubString(userName, 20);
            userInfo.Avatar = "";
            userInfo.PayCredits = 0;
            userInfo.RankCredits = 0;
            userInfo.VerifyEmail = 0;
            userInfo.VerifyMobile = 0;
            userInfo.UserRid = UserRanks.GetUserRankByCredits(userInfo.PayCredits).UserRid;//根据积分判读用户等级
            userInfo.LiftBanTime = new DateTime(1900, 1, 1);
            userInfo.LastVisitTime = DateTime.Now;
            userInfo.LastVisitIP = WebHelper.GetIP();
            userInfo.LastVisitRgId = regionId;
            userInfo.RegisterTime = DateTime.Now;
            userInfo.RegisterIP = WebHelper.GetIP();
            userInfo.RegisterRgId = regionId;
            userInfo.Gender = 0;
            userInfo.RealName = string.Empty;
            userInfo.Bday = new DateTime(1900, 1, 1);
            userInfo.IdCard = string.Empty;
            userInfo.RegionId = 0;
            userInfo.Address = string.Empty;
            userInfo.Bio = string.Empty;

            return userInfo;
        }
    }
}
