using System;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 后台用户等级操作管理类
    /// </summary>
    public partial class AdminUserRanks : UserRanks
    {
        /// <summary>
        /// 创建用户等级
        /// </summary>
        public static void CreateUserRank(UserRankInfo userRankInfo)
        {
            BrnShop.Data.UserRanks.CreateUserRank(userRankInfo);
            BSPCache.Remove(CacheKeys.SHOP_USERRANK_LIST);
        }

        /// <summary>
        /// 删除用户等级
        /// </summary>
        /// <param name="userRid">用户等级id</param>
        /// <returns>-2代表系统等级不能删除，-1代表此等级下还有用户未删除，0代表此用户等级不存在，1代表删除成功</returns>
        public static int DeleteUserRankById(int userRid)
        {
            UserRankInfo userRankInfo = GetUserRankById(userRid);
            if (userRankInfo != null)
            {
                if (userRankInfo.System == 1)
                    return -2;

                if (AdminUsers.GetUserCountByUserRid(userRid) > 0)
                    return -1;

                BrnShop.Data.UserRanks.DeleteUserRankById(userRid);
                BSPCache.Remove(CacheKeys.SHOP_USERRANK_LIST);
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// 更新用户等级
        /// </summary>
        public static void UpdateUserRank(UserRankInfo userRankInfo)
        {
            BrnShop.Data.UserRanks.UpdateUserRank(userRankInfo);
            BSPCache.Remove(CacheKeys.SHOP_USERRANK_LIST);
        }
    }
}
