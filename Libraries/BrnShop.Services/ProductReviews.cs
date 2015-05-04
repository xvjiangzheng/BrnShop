using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 商品评价操作管理类
    /// </summary>
    public partial class ProductReviews
    {
        /// <summary>
        /// 获得商品评价
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <returns></returns>
        public static ProductReviewInfo GetProductReviewById(int reviewId)
        {
            return BrnShop.Data.ProductReviews.GetProductReviewById(reviewId);
        }

        /// <summary>
        /// 评价商品
        /// </summary>
        public static void ReviewProduct(ProductReviewInfo productReviewInfo)
        {
            BrnShop.Data.ProductReviews.ReviewProduct(productReviewInfo);
        }

        /// <summary>
        /// 对商品评价投票
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <param name="uid">用户id</param>
        /// <param name="voteTime">投票时间</param>
        public static void VoteProductReview(int reviewId, int uid, DateTime voteTime)
        {
            BrnShop.Data.ProductReviews.VoteProductReview(reviewId, uid, voteTime);
        }

        /// <summary>
        /// 是否对商品评价投过票
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <param name="uid">用户id</param>
        public static bool IsVoteProductReview(int reviewId, int uid)
        {
            return BrnShop.Data.ProductReviews.IsVoteProductReview(reviewId, uid);
        }

        /// <summary>
        /// 更改商品评价状态
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <param name="state">评价状态</param>
        /// <returns></returns>
        public static bool ChangeProductReviewState(int reviewId, int state)
        {
            if (reviewId > 0 && (state == 0 || state == 1))
                return BrnShop.Data.ProductReviews.ChangeProductReviewState(reviewId, state);
            return false;
        }

        /// <summary>
        /// 获得用户商品评价列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static List<ProductReviewInfo> GetUserProductReviewList(int uid, int pageSize, int pageNumber)
        {
            return BrnShop.Data.ProductReviews.GetUserProductReviewList(uid, pageSize, pageNumber);
        }

        /// <summary>
        /// 获得用户商品评价数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static int GetUserProductReviewCount(int uid)
        {
            return BrnShop.Data.ProductReviews.GetUserProductReviewCount(uid);
        }

        /// <summary>
        /// 获得商品评价列表
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="type">类型(0代表全部评价,1代表好评,2代表中评,3代表差评)</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        public static DataTable GetProductReviewList(int pid, int type, int pageSize, int pageNumber)
        {
            return BrnShop.Data.ProductReviews.GetProductReviewList(pid, type, pageSize, pageNumber);
        }

        /// <summary>
        /// 获得商品评价数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="type">类型(0代表全部评价,1代表好评,2代表中评,3代表差评)</param>
        /// <returns></returns>
        public static int GetProductReviewCount(int pid, int type)
        {
            return BrnShop.Data.ProductReviews.GetProductReviewCount(pid, type);
        }

        /// <summary>
        /// 获得商品评价及其回复
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <returns></returns>
        public static DataTable GetProductReviewWithReplyById(int reviewId)
        {
            return BrnShop.Data.ProductReviews.GetProductReviewWithReplyById(reviewId);
        }

        /// <summary>
        /// 获得商品评价列表
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static DataTable GetProductReviewList(DateTime startTime, DateTime endTime)
        {
            return BrnShop.Data.ProductReviews.GetProductReviewList(startTime, endTime);
        }
    }
}
