using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 新闻数据访问类
    /// </summary>
    public partial class News
    {
        #region 辅助方法

        /// <summary>
        /// 通过IDataReader创建NewsTypeInfo
        /// </summary>
        public static NewsTypeInfo BuildNewsTypeFromReader(IDataReader reader)
        {
            NewsTypeInfo newsTypeInfo = new NewsTypeInfo();

            newsTypeInfo.NewsTypeId = TypeHelper.ObjectToInt(reader["newstypeid"]);
            newsTypeInfo.Name = reader["name"].ToString();
            newsTypeInfo.DisplayOrder = TypeHelper.ObjectToInt(reader["displayorder"]);

            return newsTypeInfo;
        }

        /// <summary>
        /// 通过IDataReader创建NewsInfo
        /// </summary>
        public static NewsInfo BuildNewsFromReader(IDataReader reader)
        {
            NewsInfo newsInfo = new NewsInfo();

            newsInfo.NewsId = TypeHelper.ObjectToInt(reader["newsid"]);
            newsInfo.NewsTypeId = TypeHelper.ObjectToInt(reader["newstypeid"]);
            newsInfo.IsShow = TypeHelper.ObjectToInt(reader["isshow"]);
            newsInfo.IsTop = TypeHelper.ObjectToInt(reader["istop"]);
            newsInfo.IsHome = TypeHelper.ObjectToInt(reader["ishome"]);
            newsInfo.DisplayOrder = TypeHelper.ObjectToInt(reader["displayorder"]);
            newsInfo.AddTime = TypeHelper.ObjectToDateTime(reader["addtime"]);
            newsInfo.Title = reader["title"].ToString();
            newsInfo.Url = reader["url"].ToString();
            newsInfo.Body = reader["body"].ToString();

            return newsInfo;
        }

        #endregion

        /// <summary>
        /// 创建新闻类型
        /// </summary>
        public static void CreateNewsType(NewsTypeInfo newsTypeInfo)
        {
            BrnShop.Core.BSPData.RDBS.CreateNewsType(newsTypeInfo);
        }

        /// <summary>
        /// 删除新闻类型
        /// </summary>
        /// <param name="newsTypeId">新闻类型id</param>
        public static void DeleteNewsTypeById(int newsTypeId)
        {
            BrnShop.Core.BSPData.RDBS.DeleteNewsTypeById(newsTypeId);
        }

        /// <summary>
        /// 更新新闻类型
        /// </summary>
        public static void UpdateNewsType(NewsTypeInfo newsTypeInfo)
        {
            BrnShop.Core.BSPData.RDBS.UpdateNewsType(newsTypeInfo);
        }

        /// <summary>
        /// 获得新闻类型列表
        /// </summary>
        /// <returns></returns>
        public static List<NewsTypeInfo> GetNewsTypeList()
        {
            List<NewsTypeInfo> newsTypeList = new List<NewsTypeInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetNewsTypeList();
            while (reader.Read())
            {
                NewsTypeInfo newsTypeInfo = BuildNewsTypeFromReader(reader);
                newsTypeList.Add(newsTypeInfo);
            }

            reader.Close();
            return newsTypeList;
        }




        /// <summary>
        /// 创建新闻
        /// </summary>
        public static void CreateNews(NewsInfo newsInfo)
        {
            BrnShop.Core.BSPData.RDBS.CreateNews(newsInfo);
        }

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="newsIdList">新闻id列表</param>
        public static void DeleteNewsById(string newsIdList)
        {
            BrnShop.Core.BSPData.RDBS.DeleteNewsById(newsIdList);
        }

        /// <summary>
        /// 更新新闻
        /// </summary>
        public static void UpdateNews(NewsInfo newsInfo)
        {
            BrnShop.Core.BSPData.RDBS.UpdateNews(newsInfo);
        }

        /// <summary>
        /// 获得新闻
        /// </summary>
        /// <param name="newsId">新闻id</param>
        /// <returns></returns>
        public static NewsInfo GetNewsById(int newsId)
        {
            NewsInfo newsInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetNewsById(newsId);
            if (reader.Read())
            {
                newsInfo = BuildNewsFromReader(reader);
            }

            reader.Close();
            return newsInfo;
        }

        /// <summary>
        /// 后台获得新闻
        /// </summary>
        /// <param name="newsId">新闻id</param>
        /// <returns></returns>
        public static NewsInfo AdminGetNewsById(int newsId)
        {
            NewsInfo newsInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.AdminGetNewsById(newsId);
            if (reader.Read())
            {
                newsInfo = BuildNewsFromReader(reader);
            }

            reader.Close();
            return newsInfo;
        }

        /// <summary>
        /// 后台获得新闻列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable AdminGetNewsList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetNewsList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 后台获得新闻列表搜索条件
        /// </summary>
        /// <param name="newsTypeId">新闻类型id</param>
        /// <param name="title">新闻标题</param>
        /// <returns></returns>
        public static string AdminGetNewsListCondition(int newsTypeId, string title)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetNewsListCondition(newsTypeId, title);
        }

        /// <summary>
        /// 后台获得新闻列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetNewsListSort(string sortColumn, string sortDirection)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetNewsListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得新闻数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetNewsCount(string condition)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetNewsCount(condition);
        }

        /// <summary>
        /// 后台根据新闻标题得到新闻id
        /// </summary>
        /// <param name="title">新闻标题</param>
        /// <returns></returns>
        public static int AdminGetNewsIdByTitle(string title)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetNewsIdByTitle(title);
        }

        /// <summary>
        /// 获得置首的新闻列表
        /// </summary>
        /// <param name="newsTypeId">新闻类型id(0代表全部类型)</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        public static DataTable GetHomeNewsList(int newsTypeId, int count)
        {
            return BrnShop.Core.BSPData.RDBS.GetHomeNewsList(newsTypeId, count);
        }

        /// <summary>
        /// 获得新闻列表条件
        /// </summary>
        /// <param name="newsTypeId">新闻类型id(0代表全部类型)</param>
        /// <param name="title">新闻标题</param>
        /// <returns></returns>
        public static string GetNewsListCondition(int newsTypeId, string title)
        {
            return BrnShop.Core.BSPData.RDBS.GetNewsListCondition(newsTypeId, title);
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
            return BrnShop.Core.BSPData.RDBS.GetNewsList(pageSize, pageNumber, condition);
        }

        /// <summary>
        /// 获得新闻数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int GetNewsCount(string condition)
        {
            return BrnShop.Core.BSPData.RDBS.GetNewsCount(condition);
        }
    }
}
