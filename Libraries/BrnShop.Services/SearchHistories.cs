using System;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 搜索历史操作管理类
    /// </summary>
    public partial class SearchHistories
    {
        /// <summary>
        /// 更新搜索历史
        /// </summary>
        /// <param name="state">UpdateSearchHistoryState类型对象</param>
        /// <returns></returns>
        public static void UpdateSearchHistory(object state)
        {
            UpdateSearchHistoryState updateSearchHistoryState = (UpdateSearchHistoryState)state;
            BrnShop.Data.SearchHistories.UpdateSearchHistory(updateSearchHistoryState.Uid, updateSearchHistoryState.Word, updateSearchHistoryState.UpdateTime);
        }

        /// <summary>
        /// 清空过期搜索历史
        /// </summary>
        public static void ClearExpiredSearchHistory()
        {
            BrnShop.Data.SearchHistories.ClearExpiredSearchHistory();
        }
    }
}
