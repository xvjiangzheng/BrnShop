using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 新闻操作管理类
    /// </summary>
    public partial class News
    {
        /// <summary>
        /// 获得新闻类型列表
        /// </summary>
        /// <returns></returns>
        public static List<NewsTypeInfo> GetNewsTypeList()
        {
            List<NewsTypeInfo> newsTypeList = BrnShop.Core.BSPCache.Get(CacheKeys.SHOP_NEWSTYPE_LIST) as List<NewsTypeInfo>;
            if (newsTypeList == null)
            {
                newsTypeList = BrnShop.Data.News.GetNewsTypeList();
                BrnShop.Core.BSPCache.Insert(CacheKeys.SHOP_NEWSTYPE_LIST, newsTypeList);
            }
            return newsTypeList;
        }

        /// <summary>
        /// 获得新闻类型
        /// </summary>
        /// <param name="newsTypeId">新闻类型id</param>
        /// <returns></returns>
        public static NewsTypeInfo GetNewsTypeById(int newsTypeId)
        {
            if (newsTypeId > 0)
            {
                foreach (NewsTypeInfo newsTypeInfo in GetNewsTypeList())
                {
                    if (newsTypeInfo.NewsTypeId == newsTypeId)
                        return newsTypeInfo;
                }
            }
            return null;
        }

        /// <summary>
        /// 获得新闻类型
        /// </summary>
        /// <param name="name">新闻类型名称</param>
        /// <returns></returns>
        public static NewsTypeInfo GetNewsTypeByName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                foreach (NewsTypeInfo newsTypeInfo in GetNewsTypeList())
                {
                    if (newsTypeInfo.Name == name)
                        return newsTypeInfo;
                }
            }
            return null;
        }




        /// <summary>
        /// 获得新闻
        /// </summary>
        /// <param name="newsId">新闻id</param>
        /// <returns></returns>
        public static NewsInfo GetNewsById(int newsId)
        {
            if (newsId > 0)
                return BrnShop.Data.News.GetNewsById(newsId);
            return null;
        }

        /// <summary>
        /// 获得置首的新闻列表
        /// </summary>
        /// <param name="newsTypeId">新闻类型id(0代表全部类型)</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        public static DataTable GetHomeNewsList(int newsTypeId, int count)
        {
            DataTable newsList = BrnShop.Core.BSPCache.Get(CacheKeys.SHOP_NEWS_HOMELIST + newsTypeId) as DataTable;
            if (newsList == null)
            {
                newsList = BrnShop.Data.News.GetHomeNewsList(newsTypeId, count);
                BrnShop.Core.BSPCache.Insert(CacheKeys.SHOP_NEWS_HOMELIST + newsTypeId, newsList);
            }
            return newsList;
        }

        /// <summary>
        /// 获得新闻列表条件
        /// </summary>
        /// <param name="newsTypeId">新闻类型id(0代表全部类型)</param>
        /// <param name="title">新闻标题</param>
        /// <returns></returns>
        public static string GetNewsListCondition(int newsTypeId, string title)
        {
            return BrnShop.Data.News.GetNewsListCondition(newsTypeId, title);
        }

        /// <summary>
        /// 获得新闻列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static DataTable GetNewsList(int pageSize, int pageNumber, string condition)
        {
            return BrnShop.Data.News.GetNewsList(pageSize, pageNumber, condition);
        }

        /// <summary>
        /// 获得新闻数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int GetNewsCount(string condition)
        {
            return BrnShop.Data.News.GetNewsCount(condition);
        }
    }
}
