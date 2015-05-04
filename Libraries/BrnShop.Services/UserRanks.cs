using System;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 用户等级操作管理类
    /// </summary>
    public partial class UserRanks
    {
        /// <summary>
        /// 获得用户等级列表
        /// </summary>
        /// <returns></returns>
        public static List<UserRankInfo> GetUserRankList()
        {
            List<UserRankInfo> userRankList = BrnShop.Core.BSPCache.Get(CacheKeys.SHOP_USERRANK_LIST) as List<UserRankInfo>;
            if (userRankList == null)
            {
                userRankList = BrnShop.Data.UserRanks.GetUserRankList();
                BrnShop.Core.BSPCache.Insert(CacheKeys.SHOP_USERRANK_LIST, userRankList);
            }
            return userRankList;
        }

        /// <summary>
        /// 返回系统级用户等级列表
        /// </summary>
        /// <returns></returns>
        public static List<UserRankInfo> GetSystemUserRankList()
        {
            List<UserRankInfo> userRankList = new List<UserRankInfo>();
            foreach (UserRankInfo userRankInfo in GetUserRankList())
            {
                if (userRankInfo.System == 1)
                    userRankList.Add(userRankInfo);
            }

            return userRankList;
        }

        /// <summary>
        /// 返回用户级用户等级列表
        /// </summary>
        /// <returns></returns>
        public static List<UserRankInfo> GetCustomerUserRankList()
        {
            List<UserRankInfo> userRankList = new List<UserRankInfo>();
            foreach (UserRankInfo userRankInfo in GetUserRankList())
            {
                if (userRankInfo.System == 0)
                    userRankList.Add(userRankInfo);
            }

            return userRankList;
        }

        /// <summary>
        /// 获得用户等级
        /// </summary>
        /// <param name="userRid">用户等级id</param>
        /// <returns></returns>
        public static UserRankInfo GetUserRankById(int userRid)
        {
            if (userRid > 0)
            {
                foreach (UserRankInfo userRankInfo in GetUserRankList())
                {
                    if (userRid == userRankInfo.UserRid)
                        return userRankInfo;
                }
            }
            return null;
        }

        /// <summary>
        /// 获得用户等级id
        /// </summary>
        /// <param name="title">用户等级标题</param>
        /// <returns></returns>
        public static int GetUserRidByTitle(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                foreach (UserRankInfo userRankInfo in GetUserRankList())
                {
                    if (userRankInfo.Title == title)
                        return userRankInfo.UserRid;
                }
            }
            return -1;
        }

        /// <summary>
        /// 判断用户等级是否为被禁用等级
        /// </summary>
        /// <param name="userRid">用户等级id</param>
        /// <returns></returns>
        public static bool IsBanUserRank(int userRid)
        {
            return userRid > 5 ? false : true;
        }

        /// <summary>
        /// 获得积分对应的用户等级
        /// </summary>
        /// <param name="credits">积分</param>
        /// <returns></returns>
        public static UserRankInfo GetUserRankByCredits(int credits)
        {
            foreach (UserRankInfo item in GetUserRankList())
            {
                if (item.System == 0 && item.CreditsLower <= credits && (item.CreditsUpper > credits || item.CreditsUpper == -1))
                    return item;
            }
            return null;
        }

        /// <summary>
        /// 获得最低用户等级
        /// </summary>
        /// <returns></returns>
        public static UserRankInfo GetLowestUserRank()
        {
            foreach (UserRankInfo userRankInfo in GetUserRankList())
            {
                if (userRankInfo.System == 0 && userRankInfo.CreditsLower == 0)
                    return userRankInfo;
            }
            return null;
        }
    }
}
