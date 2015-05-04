using System;
using System.Data;

namespace BrnShop.Services
{
    public partial class AdminSearchHistories : SearchHistories
    {
        /// <summary>
        /// 获得搜索词统计列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="word">搜索词</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable GetSearchWordStatList(int pageSize, int pageNumber, string word, string sort)
        {
            return BrnShop.Data.SearchHistories.GetSearchWordStatList(pageSize, pageNumber, word, sort);
        }

        /// <summary>
        /// 获得搜索词统计列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string GetSearchWordStatListSort(string sortColumn, string sortDirection)
        {
            return BrnShop.Data.SearchHistories.GetSearchWordStatListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 获得搜索词统计数量
        /// </summary>
        /// <param name="word">搜索词</param>
        /// <returns></returns>
        public static int GetSearchWordStatCount(string word)
        {
            return BrnShop.Data.SearchHistories.GetSearchWordStatCount(word);
        }
    }
}
