using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 浏览历史数据访问类
    /// </summary>
    public partial class BrowseHistories
    {
        /// <summary>
        /// 获得用户浏览商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static List<PartProductInfo> GetUserBrowseProductList(int pageSize, int pageNumber, int uid)
        {
            List<PartProductInfo> partProductList = new List<PartProductInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetUserBrowseProductList(pageSize, pageNumber, uid);
            while (reader.Read())
            {
                PartProductInfo partProductInfo = Products.BuildPartProductFromReader(reader);
                partProductList.Add(partProductInfo);
            }

            reader.Close();
            return partProductList;
        }

        /// <summary>
        /// 获得用户浏览商品数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static int GetUserBrowseProductCount(int uid)
        {
            return BrnShop.Core.BSPData.RDBS.GetUserBrowseProductCount(uid);
        }

        /// <summary>
        /// 更新浏览历史
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pid">商品id</param>
        /// <param name="updateTime">更新时间</param>
        public static void UpdateBrowseHistory(int uid, int pid, DateTime updateTime)
        {
            BrnShop.Core.BSPData.RDBS.UpdateBrowseHistory(uid, pid, updateTime);
        }

        /// <summary>
        /// 清空过期浏览历史
        /// </summary>
        public static void ClearExpiredBrowseHistory()
        {
            BrnShop.Core.BSPData.RDBS.ClearExpiredBrowseHistory();
        }
    }
}
