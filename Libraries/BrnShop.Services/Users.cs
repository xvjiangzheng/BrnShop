using System;
using System.Data;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 用户操作管理类
    /// </summary>
    public partial class Users
    {
        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static PartUserInfo GetPartUserById(int uid)
        {
            if (uid > 0)
                return BrnShop.Data.Users.GetPartUserById(uid);

            return null;
        }

        /// <summary>
        /// 获得用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static UserInfo GetUserById(int uid)
        {
            if (uid > 0)
                return BrnShop.Data.Users.GetUserById(uid);

            return null;
        }

        /// <summary>
        /// 获得用户细节
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static UserDetailInfo GetUserDetailById(int uid)
        {
            if (uid > 0)
                return BrnShop.Data.Users.GetUserDetailById(uid);

            return null;
        }

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static PartUserInfo GetPartUserByUidAndPwd(int uid, string password)
        {
            PartUserInfo partUserInfo = GetPartUserById(uid);
            if (partUserInfo != null && partUserInfo.Password == password)
                return partUserInfo;
            return null;
        }

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static PartUserInfo GetPartUserByName(string userName)
        {
            return BrnShop.Data.Users.GetPartUserByName(userName);
        }

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <returns></returns>
        public static PartUserInfo GetPartUserByEmail(string email)
        {
            return BrnShop.Data.Users.GetPartUserByEmail(email);
        }

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="mobile">用户手机</param>
        /// <returns></returns>
        public static PartUserInfo GetPartUserByMobile(string mobile)
        {
            return BrnShop.Data.Users.GetPartUserByMobile(mobile);
        }

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static int GetUidByUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return -1;

            return BrnShop.Data.Users.GetUidByUserName(userName);
        }

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="email">用户邮箱</param>
        /// <returns></returns>
        public static int GetUidByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return -1;

            return BrnShop.Data.Users.GetUidByEmail(email);
        }

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="mobile">用户手机</param>
        /// <returns></returns>
        public static int GetUidByMobile(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
                return -1;

            return BrnShop.Data.Users.GetUidByMobile(mobile);
        }

        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="accountName">账户名</param>
        /// <returns></returns>
        public static int GetUidByAccountName(string accountName)
        {
            if (string.IsNullOrWhiteSpace(accountName))
                return -1;

            if (ValidateHelper.IsEmail(accountName))//邮箱
            {
                return GetUidByEmail(accountName);
            }
            else if (ValidateHelper.IsMobile(accountName))//手机
            {
                return GetUidByMobile(accountName);
            }
            else//用户名
            {
                return GetUidByUserName(accountName);
            }
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public static int CreateUser(UserInfo userInfo)
        {
            return BrnShop.Data.Users.CreateUser(userInfo);
        }

        /// <summary>
        /// 创建部分用户
        /// </summary>
        /// <returns></returns>
        public static PartUserInfo CreatePartGuest()
        {
            return new PartUserInfo
            {
                Uid = -1,
                UserName = "guest",
                Email = "",
                Mobile = "",
                Password = "",
                UserRid = 6,
                AdminGid = 1,
                NickName = "游客",
                Avatar = "",
                PayCredits = 0,
                RankCredits = 0,
                VerifyEmail = 0,
                VerifyMobile = 0,
                LiftBanTime = new DateTime(1900, 1, 1),
                Salt = ""
            };
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <returns></returns>
        public static void UpdateUser(UserInfo userInfo)
        {
            BrnShop.Data.Users.UpdateUser(userInfo);
        }

        /// <summary>
        /// 更新部分用户
        /// </summary>
        /// <returns></returns>
        public static void UpdatePartUser(PartUserInfo partUserInfo)
        {
            BrnShop.Data.Users.UpdatePartUser(partUserInfo);
        }

        /// <summary>
        /// 更新用户细节
        /// </summary>
        /// <returns></returns>
        public static void UpdateUserDetail(UserDetailInfo userDetailInfo)
        {
            BrnShop.Data.Users.UpdateUserDetail(userDetailInfo);
        }

        /// <summary>
        /// 更新用户最后访问
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="visitTime">访问时间</param>
        /// <param name="ip">ip</param>
        /// <param name="regionId">区域id</param>
        public static void UpdateUserLastVisit(int uid, DateTime visitTime, string ip, int regionId)
        {
            BrnShop.Data.Users.UpdateUserLastVisit(uid, visitTime, ip, regionId);
        }

        /// <summary>
        /// 用户名是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        public static bool IsExistUserName(string userName)
        {
            return GetUidByUserName(userName) > 0 ? true : false;
        }

        /// <summary>
        /// 邮箱是否存在
        /// </summary>
        /// <param name="userName">邮箱</param>
        public static bool IsExistEmail(string email)
        {
            return GetUidByEmail(email) > 0 ? true : false;
        }

        /// <summary>
        /// 手机是否存在
        /// </summary>
        /// <param name="userName">手机</param>
        public static bool IsExistMobile(string mobile)
        {
            return GetUidByMobile(mobile) > 0 ? true : false;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="uidList">用户id</param>
        public static void DeleteUserById(int[] uidList)
        {
            //此方法需要补充
            if (uidList != null && uidList.Length > 0)
                BrnShop.Data.Users.DeleteUserById(CommonHelper.IntArrayToString(uidList));
        }

        /// <summary>
        /// 创建用户密码
        /// </summary>
        /// <param name="password">真实密码</param>
        /// <param name="salt">散列盐值</param>
        /// <returns></returns>
        public static string CreateUserPassword(string password, string salt)
        {
            return SecureHelper.MD5(password + salt);
        }

        /// <summary>
        /// 获得用户等级下用户的数量
        /// </summary>
        /// <param name="userRid">用户等级id</param>
        /// <returns></returns>
        public static int GetUserCountByUserRid(int userRid)
        {
            return BrnShop.Data.Users.GetUserCountByUserRid(userRid);
        }

        /// <summary>
        /// 获得管理员组下用户的数量
        /// </summary>
        /// <param name="adminGid">管理员组id</param>
        /// <returns></returns>
        public static int GetUserCountByAdminGid(int adminGid)
        {
            return BrnShop.Data.Users.GetUserCountByAdminGid(adminGid);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="userName">用户名</param>
        /// <param name="nickName">昵称</param>
        /// <param name="avatar">头像</param>
        /// <param name="gender">性别</param>
        /// <param name="realName">真实名称</param>
        /// <param name="bday">出生日期</param>
        /// <param name="idCard">The id card.</param>
        /// <param name="regionId">区域id</param>
        /// <param name="address">所在地</param>
        /// <param name="bio">简介</param>
        /// <returns></returns>
        public static bool UpdateUser(int uid, string userName, string nickName, string avatar, int gender, string realName, DateTime bday, string idCard, int regionId, string address, string bio)
        {
            return BrnShop.Data.Users.UpdateUser(uid, userName, nickName, avatar, gender, realName, bday, idCard, regionId, address, bio);
        }

        /// <summary>
        /// 更新用户邮箱
        /// </summary>
        /// <param name="uid">用户id.</param>
        /// <param name="email">邮箱</param>
        public static void UpdateUserEmailByUid(int uid, string email)
        {
            BrnShop.Data.Users.UpdateUserEmailByUid(uid, email);
        }

        /// <summary>
        /// 更新用户手机
        /// </summary>
        /// <param name="uid">用户id.</param>
        /// <param name="mobile">手机</param>
        public static void UpdateUserMobileByUid(int uid, string mobile)
        {
            BrnShop.Data.Users.UpdateUserMobileByUid(uid, mobile);
        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="uid">用户id.</param>
        /// <param name="password">密码</param>
        public static void UpdateUserPasswordByUid(int uid, string password)
        {
            BrnShop.Data.Users.UpdateUserPasswordByUid(uid, password);
        }

        /// <summary>
        /// 生成用户盐值
        /// </summary>
        /// <returns></returns>
        public static string GenerateUserSalt()
        {
            return Randoms.CreateRandomValue(6);
        }

        /// <summary>
        /// 更新用户解禁时间
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="liftBanTime">解禁时间</param>
        public static void UpdateUserLiftBanTimeByUid(int uid, DateTime liftBanTime)
        {
            BrnShop.Data.Users.UpdateUserLiftBanTimeByUid(uid, liftBanTime);
        }

        /// <summary>
        /// 更新用户解禁时间
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="userRankInfo">用户等级</param>
        public static void UpdateUserLiftBanTimeByUid(int uid, UserRankInfo userRankInfo)
        {
            UpdateUserLiftBanTimeByUid(uid, DateTime.Now.AddDays(userRankInfo.LimitDays));
        }

        /// <summary>
        /// 更新用户等级
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="userRid">用户等级id</param>
        public static void UpdateUserRankByUid(int uid, int userRid)
        {
            BrnShop.Data.Users.UpdateUserRankByUid(uid, userRid);
        }

        /// <summary>
        /// 更新用户在线时间
        /// </summary>
        /// <param name="uid">用户id</param>
        public static void UpdateUserOnlineTime(int uid)
        {
            int updateOnlineTimeSpan = BSPConfig.ShopConfig.UpdateOnlineTimeSpan;
            if (updateOnlineTimeSpan == 0)
                return;

            int lastUpdateTime = TypeHelper.StringToInt(WebHelper.GetCookie("oltime"));
            if (lastUpdateTime > 0 && lastUpdateTime < (Environment.TickCount - updateOnlineTimeSpan * 60 * 1000))
            {
                BrnShop.Data.Users.UpdateUserOnlineTime(uid, updateOnlineTimeSpan, DateTime.Now);
                WebHelper.SetCookie("oltime", Environment.TickCount.ToString());
            }
            else if (lastUpdateTime == 0)
            {
                WebHelper.SetCookie("oltime", Environment.TickCount.ToString());
            }
        }

        /// <summary>
        /// 通过注册ip获得注册时间
        /// </summary>
        /// <param name="registerIP">注册ip</param>
        /// <returns></returns>
        public static DateTime GetRegisterTimeByRegisterIP(string registerIP)
        {
            return BrnShop.Data.Users.GetRegisterTimeByRegisterIP(registerIP);
        }

        /// <summary>
        /// 获得用户最后访问时间
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static DateTime GetUserLastVisitTimeByUid(int uid)
        {
            return BrnShop.Data.Users.GetUserLastVisitTimeByUid(uid);
        }
    }
}
