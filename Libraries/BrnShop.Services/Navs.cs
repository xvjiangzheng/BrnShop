using System;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 导航栏操作管理类
    /// </summary>
    public partial class Navs
    {
        /// <summary>
        /// 获得导航栏列表
        /// </summary>
        /// <returns></returns>
        public static List<NavInfo> GetNavList()
        {
            List<NavInfo> navTree = BrnShop.Core.BSPCache.Get(CacheKeys.SHOP_NAV_LIST) as List<NavInfo>;

            if (navTree == null)
            {
                navTree = new List<NavInfo>();
                List<NavInfo> navList = BrnShop.Data.Navs.GetNavList();

                CreateNavTree(navList, navTree, 0);
                BrnShop.Core.BSPCache.Insert(CacheKeys.SHOP_NAV_LIST, navTree);
            }
            return navTree;
        }

        /// <summary>
        /// 创建导航列表树
        /// </summary>
        private static void CreateNavTree(List<NavInfo> sourceNavList, List<NavInfo> resultNavList, int id)
        {
            foreach (NavInfo navInfo in sourceNavList)
            {
                if (navInfo.Pid == id)
                {
                    resultNavList.Add(navInfo);
                    CreateNavTree(sourceNavList, resultNavList, navInfo.Id);
                }
            }
        }

        /// <summary>
        /// 获得主导航栏列表
        /// </summary>
        /// <returns></returns>
        public static List<NavInfo> GetMainNavList()
        {
            List<NavInfo> navList = BrnShop.Core.BSPCache.Get(CacheKeys.SHOP_NAV_MAINLIST) as List<NavInfo>;

            if (navList == null)
            {
                navList = new List<NavInfo>();
                foreach (NavInfo navInfo in BrnShop.Data.Navs.GetNavList())
                {
                    if (navInfo.Pid == 0)
                        navList.Add(navInfo);
                }
                BrnShop.Core.BSPCache.Insert(CacheKeys.SHOP_NAV_MAINLIST, navList);
            }
            return navList;
        }

        /// <summary>
        /// 获得子导航栏列表
        /// </summary>
        /// <param name="id">导航id</param>
        /// <returns></returns>
        public static List<NavInfo> GetSubNavList(int id)
        {
            return GetSubNavList(id, true);
        }

        /// <summary>
        /// 获得子导航栏列表
        /// </summary>
        /// <param name="id">导航id</param>
        /// <param name="isAllChildren">是否包括全部子节点</param>
        /// <returns></returns>
        public static List<NavInfo> GetSubNavList(int id, bool isAllChildren)
        {
            List<NavInfo> navList = new List<NavInfo>();

            if (id > 0)
            {
                int flag = 0;
                if (isAllChildren)
                {
                    foreach (NavInfo navInfo in GetNavList())
                    {
                        if (navInfo.Pid == id || navInfo.Layer > 2)
                        {
                            flag = 1;
                            navList.Add(navInfo);
                        }
                        else if (flag == 1 && navInfo.Layer == 1)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    foreach (NavInfo navInfo in GetNavList())
                    {
                        if (navInfo.Pid == id && navInfo.Layer == 2)
                        {
                            flag = 1;
                            navList.Add(navInfo);
                        }
                        else if (flag == 1 && navInfo.Layer == 1)
                        {
                            break;
                        }
                    }
                }
            }
            return navList;
        }

        /// <summary>
        /// 获得导航栏
        /// </summary>
        /// <param name="id">导航栏id</param>
        /// <returns></returns>
        public static NavInfo GetNavById(int id)
        {
            if (id < 1)
                return null;

            foreach (NavInfo navInfo in GetNavList())
            {
                if (navInfo.Id == id)
                    return navInfo;
            }

            return null;
        }
    }
}
