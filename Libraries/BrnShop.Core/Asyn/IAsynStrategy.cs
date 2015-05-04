using System;

namespace BrnShop.Core
{
    /// <summary>
    /// BrnShop异步策略接口
    /// </summary>
    public partial interface IAsynStrategy
    {
        /// <summary>
        /// 更新在线用户
        /// </summary>
        /// <param name="state">state</param>
        void UpdateOnlineUser(UpdateOnlineUserState state);

        /// <summary>
        /// 更新浏览历史
        /// </summary>
        /// <param name="state">state</param>
        void UpdateBrowseHistory(UpdateBrowseHistoryState state);

        /// <summary>
        /// 更新搜索历史
        /// </summary>
        /// <param name="state">state</param>
        void UpdateSearchHistory(UpdateSearchHistoryState state);

        /// <summary>
        /// 更新商品统计
        /// </summary>
        /// <param name="state">state</param>
        void UpdateProductStat(UpdateProductStatState state);

        /// <summary>
        /// 更新PV统计
        /// </summary>
        /// <param name="state">state</param>
        void UpdatePVStat(UpdatePVStatState state);
    }
}
