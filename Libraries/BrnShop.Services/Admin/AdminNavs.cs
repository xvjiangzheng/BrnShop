using System;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 后台导航栏操作管理类
    /// </summary>
    public partial class AdminNavs : Navs
    {
        /// <summary>
        /// 创建导航栏
        /// </summary>
        public static void CreateNav(NavInfo navInfo)
        {
            if (navInfo.Pid > 0)
            {
                List<NavInfo> navList = BrnShop.Data.Navs.GetNavList();
                NavInfo parentNavInfo = navList.Find(x => x.Id == navInfo.Pid);//父导航
                navInfo.Layer = parentNavInfo.Layer + 1;
            }
            else
            {
                navInfo.Layer = 1;
            }
            BrnShop.Data.Navs.CreateNav(navInfo);
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_NAV_LIST);
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_NAV_MAINLIST);
        }

        /// <summary>
        /// 删除导航栏
        /// </summary>
        /// <param name="id">导航栏id</param>
        /// <returns></returns>
        public static int DeleteNavById(int id)
        {
            if (GetSubNavList(id, false).Count > 0)
                return 0;

            BrnShop.Data.Navs.DeleteNavById(id);
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_NAV_LIST);
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_NAV_MAINLIST);
            return 1;
        }

        /// <summary>
        /// 更新导航栏
        /// </summary>
        public static void UpdateNav(NavInfo navInfo, int oldPid)
        {
            if (navInfo.Pid != oldPid)
            {
                int changeLayer = 0;
                List<NavInfo> navList = BrnShop.Data.Navs.GetNavList();
                NavInfo oldParentNavInfo = navList.Find(x => x.Id == oldPid);
                if (navInfo.Pid > 0)
                {
                    NavInfo newParentNavInfo = navList.Find(x => x.Id == navInfo.Pid);//新的父导航
                    if (oldParentNavInfo == null)
                    {
                        changeLayer = newParentNavInfo.Layer - 1;
                    }
                    else
                    {
                        changeLayer = newParentNavInfo.Layer - oldParentNavInfo.Layer;
                    }
                    navInfo.Layer = newParentNavInfo.Layer + 1;
                }
                else
                {
                    changeLayer = 1 - oldParentNavInfo.Layer;
                    navInfo.Layer = 1;
                }

                foreach (NavInfo info in navList.FindAll(x => x.Pid == navInfo.Id))
                {
                    UpdateChildNavLayer(navList, info, changeLayer);
                }
            }

            BrnShop.Data.Navs.UpdateNav(navInfo);

            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_NAV_LIST);
            BrnShop.Core.BSPCache.Remove(CacheKeys.SHOP_NAV_MAINLIST);
        }

        /// <summary>
        /// 递归更新导航及其子导航的级别
        /// </summary>
        /// <param name="navList">导航列表</param>
        /// <param name="navInfo">导航信息</param>
        /// <param name="changeLayer">变化的级别</param>
        private static void UpdateChildNavLayer(List<NavInfo> navList, NavInfo navInfo, int changeLayer)
        {
            navInfo.Layer = navInfo.Layer + changeLayer;
            BrnShop.Data.Navs.UpdateNav(navInfo);
            foreach (NavInfo info in navList.FindAll(x => x.Pid == navInfo.Id))
            {
                UpdateChildNavLayer(navList, info, changeLayer);
            }
        }
    }
}
