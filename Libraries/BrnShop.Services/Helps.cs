using System;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 帮助操作管理类
    /// </summary>
    public partial class Helps
    {
        /// <summary>
        /// 获得帮助列表
        /// </summary>
        /// <returns></returns>
        public static List<HelpInfo> GetHelpList()
        {
            List<HelpInfo> helpTree = BrnShop.Core.BSPCache.Get(CacheKeys.SHOP_HELP_LIST) as List<HelpInfo>;

            if (helpTree == null)
            {
                helpTree = new List<HelpInfo>();
                List<HelpInfo> helpList = BrnShop.Data.Helps.GetHelpList();

                CreateHelpTree(helpList, helpTree, 0);
                BrnShop.Core.BSPCache.Insert(CacheKeys.SHOP_HELP_LIST, helpTree);
            }
            return helpTree;
        }

        /// <summary>
        /// 创建帮助列表树
        /// </summary>
        private static void CreateHelpTree(List<HelpInfo> sourceHelpList, List<HelpInfo> resultHelpList, int id)
        {
            foreach (HelpInfo helpInfo in sourceHelpList)
            {
                if (helpInfo.Pid == id)
                {
                    resultHelpList.Add(helpInfo);
                    CreateHelpTree(sourceHelpList, resultHelpList, helpInfo.Id);
                }
            }
        }

        /// <summary>
        /// 获得帮助分类列表
        /// </summary>
        /// <returns></returns>
        public static List<HelpInfo> GetHelpCategoryList()
        {
            List<HelpInfo> helpCategoryList = new List<HelpInfo>();

            foreach (HelpInfo helpInfo in GetHelpList())
            {
                if (helpInfo.Pid == 0)
                    helpCategoryList.Add(helpInfo);
            }

            return helpCategoryList;
        }

        /// <summary>
        /// 获得帮助分类下帮助列表
        /// </summary>
        /// <returns></returns>
        public static List<HelpInfo> GetSameCategoryHelpList(int pid)
        {
            List<HelpInfo> helpList = new List<HelpInfo>();

            foreach (HelpInfo helpInfo in GetHelpList())
            {
                if (helpInfo.Pid == pid)
                    helpList.Add(helpInfo);
            }

            return helpList;
        }

        /// <summary>
        /// 获得帮助分类下帮助数量
        /// </summary>
        /// <returns></returns>
        public static int GetChildHelpCount(int pid)
        {
            int count = 0;
            foreach (HelpInfo helpInfo in GetHelpList())
            {
                if (helpInfo.Pid == pid)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// 获得帮助
        /// </summary>
        /// <param name="id">帮助id</param>
        /// <returns></returns>
        public static HelpInfo GetHelpById(int id)
        {
            if (id > 0)
            {
                foreach (HelpInfo helpInfo in GetHelpList())
                {
                    if (helpInfo.Id == id)
                        return helpInfo;
                }
            }
            return null;
        }
    }
}
