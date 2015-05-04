using System;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    public partial class AdminActions
    {
        /// <summary>
        /// 获得后台操作列表
        /// </summary>
        /// <returns></returns>
        public static List<AdminActionInfo> GetAdminActionList()
        {
            return BrnShop.Data.AdminActions.GetAdminActionList();
        }

        /// <summary>
        /// 获得后台操作树
        /// </summary>
        /// <returns></returns>
        public static List<AdminActionInfo> GetAdminActionTree()
        {
            List<AdminActionInfo> adminActionTree = new List<AdminActionInfo>();
            List<AdminActionInfo> adminActionList = GetAdminActionList();
            CreateAdminActionTree(adminActionList, adminActionTree, 0);
            return adminActionTree;
        }

        /// <summary>
        /// 递归创建后台操作树
        /// </summary>
        private static void CreateAdminActionTree(List<AdminActionInfo> adminActionList, List<AdminActionInfo> adminActionTree, int parentId)
        {
            foreach (AdminActionInfo adminActionInfo in adminActionList)
            {
                if (adminActionInfo.ParentId == parentId)
                {
                    adminActionTree.Add(adminActionInfo);
                    CreateAdminActionTree(adminActionList, adminActionTree, adminActionInfo.AdminAid);
                }
            }
        }

        /// <summary>
        /// 获得后台操作HashSet
        /// </summary>
        /// <returns></returns>
        public static HashSet<string> GetAdminActionHashSet()
        {
            HashSet<string> actionHashSet = BrnShop.Core.BSPCache.Get(CacheKeys.SHOP_ADMINACTION_HASHSET) as HashSet<string>;
            if (actionHashSet == null)
            {
                actionHashSet = new HashSet<string>();
                List<AdminActionInfo> adminActionList = GetAdminActionList();
                foreach (AdminActionInfo adminActionInfo in adminActionList)
                {
                    if (adminActionInfo.ParentId != 0 && adminActionInfo.Action != string.Empty)
                        actionHashSet.Add(adminActionInfo.Action);
                }
                BrnShop.Core.BSPCache.Insert(CacheKeys.SHOP_ADMINACTION_HASHSET, actionHashSet);
            }
            return actionHashSet;
        }
    }
}
