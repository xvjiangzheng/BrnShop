using System;
using System.Data;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 收藏夹数据访问类
    /// </summary>
    public partial class Favorites
    {
        /// <summary>
        /// 将商品添加到收藏夹
        /// </summary>
        /// <returns></returns>
        public static bool AddToFavorite(int uid, int pid, int state, DateTime addTime)
        {
            return BrnShop.Core.BSPData.RDBS.AddToFavorite(uid, pid, state, addTime);
        }

        /// <summary>
        /// 删除收藏夹的商品
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pid">商品id</param>
        /// <returns></returns>
        public static bool DeleteFavoriteProductByUidAndPid(int uid, int pid)
        {
            return BrnShop.Core.BSPData.RDBS.DeleteFavoriteProductByUidAndPid(uid, pid);
        }

        /// <summary>
        /// 商品是否已经收藏
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pid">商品id</param>
        public static bool IsExistFavoriteProduct(int uid, int pid)
        {
            return BrnShop.Core.BSPData.RDBS.IsExistFavoriteProduct(uid, pid);
        }

        /// <summary>
        /// 获得收藏夹商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="uid">用户id</param>
        /// <param name="productName">商品名称</param>
        /// <returns></returns>
        public static DataTable GetFavoriteProductList(int pageSize, int pageNumber, int uid, string productName)
        {
            return BrnShop.Core.BSPData.RDBS.GetFavoriteProductList(pageSize, pageNumber, uid, productName);
        }

        /// <summary>
        /// 获得收藏夹商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static DataTable GetFavoriteProductList(int pageSize, int pageNumber, int uid)
        {
            return BrnShop.Core.BSPData.RDBS.GetFavoriteProductList(pageSize, pageNumber, uid);
        }

        /// <summary>
        /// 获得收藏夹商品数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="productName">商品名称</param>
        /// <returns></returns>
        public static int GetFavoriteProductCount(int uid, string productName)
        {
            return BrnShop.Core.BSPData.RDBS.GetFavoriteProductCount(uid, productName);
        }

        /// <summary>
        /// 获得收藏夹商品数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static int GetFavoriteProductCount(int uid)
        {
            return BrnShop.Core.BSPData.RDBS.GetFavoriteProductCount(uid);
        }

        /// <summary>
        /// 设置收藏夹商品状态
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="pid">商品id</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public static bool SetFavoriteProductState(int uid, int pid, int state)
        {
            return BrnShop.Core.BSPData.RDBS.SetFavoriteProductState(uid, pid, state);
        }
    }
}
