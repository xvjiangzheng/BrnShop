using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 商品评价数据访问类
    /// </summary>
    public partial class ProductReviews
    {
        private static IOrderNOSQLStrategy _ordernosql = BSPData.OrderNOSQL;//订单非关系型数据库

        #region 辅助方法

        /// <summary>
        /// 从IDataReader创建ProductReviewInfo
        /// </summary>
        public static ProductReviewInfo BuildProductReviewFromReader(IDataReader reader)
        {
            ProductReviewInfo productReviewInfo = new ProductReviewInfo();

            productReviewInfo.ReviewId = TypeHelper.ObjectToInt(reader["reviewid"]);
            productReviewInfo.Pid = TypeHelper.ObjectToInt(reader["pid"]);
            productReviewInfo.Uid = TypeHelper.ObjectToInt(reader["uid"]);
            productReviewInfo.OPRId = TypeHelper.ObjectToInt(reader["oprid"]);
            productReviewInfo.Oid = TypeHelper.ObjectToInt(reader["oid"]);
            productReviewInfo.ParentId = TypeHelper.ObjectToInt(reader["parentid"]);
            productReviewInfo.State = TypeHelper.ObjectToInt(reader["state"]);
            productReviewInfo.Star = TypeHelper.ObjectToInt(reader["star"]);
            productReviewInfo.Quality = TypeHelper.ObjectToInt(reader["quality"]);
            productReviewInfo.Message = reader["message"].ToString();
            productReviewInfo.ReviewTime = TypeHelper.ObjectToDateTime(reader["reviewtime"]);
            productReviewInfo.PayCredits = TypeHelper.ObjectToInt(reader["paycredits"]);
            productReviewInfo.PName = reader["pname"].ToString();
            productReviewInfo.PShowImg = reader["pshowimg"].ToString();
            productReviewInfo.BuyTime = TypeHelper.ObjectToDateTime(reader["buytime"]);
            productReviewInfo.IP = reader["ip"].ToString();

            return productReviewInfo;
        }

        #endregion

        /// <summary>
        /// 获得商品评价
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <returns></returns>
        public static ProductReviewInfo GetProductReviewById(int reviewId)
        {
            ProductReviewInfo productReviewInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetProductReviewById(reviewId);
            if (reader.Read())
            {
                productReviewInfo = BuildProductReviewFromReader(reader);
            }
            reader.Close();
            return productReviewInfo;
        }

        /// <summary>
        /// 后台获得商品评价
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <returns></returns>
        public static ProductReviewInfo AdminGetProductReviewById(int reviewId)
        {
            ProductReviewInfo productReviewInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.AdminGetProductReviewById(reviewId);
            if (reader.Read())
            {
                productReviewInfo = BuildProductReviewFromReader(reader);
            }
            reader.Close();
            return productReviewInfo;
        }

        /// <summary>
        /// 评价商品
        /// </summary>
        public static void ReviewProduct(ProductReviewInfo productReviewInfo)
        {
            BrnShop.Core.BSPData.RDBS.ReviewProduct(productReviewInfo);
            if (_ordernosql != null)
                _ordernosql.ReviewProduct(productReviewInfo.Oid, productReviewInfo.ReviewId);
        }

        /// <summary>
        /// 删除商品评价
        /// </summary>
        /// <param name="reviewId">评价id</param>
        public static void DeleteProductReviewById(int reviewId)
        {
            BrnShop.Core.BSPData.RDBS.DeleteProductReviewById(reviewId);
        }

        /// <summary>
        /// 后台获得商品评价列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable AdminGetProductReviewList(int pageSize, int pageNumber, string condition, string sort)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetProductReviewList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 后台获得商品评价列表条件
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="message">评价内容</param>
        /// <param name="startTime">评价开始时间</param>
        /// <param name="endTime">评价结束时间</param>
        /// <returns></returns>
        public static string AdminGetProductReviewListCondition(int pid, string message, string startTime, string endTime)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetProductReviewListCondition(pid, message, startTime, endTime);
        }

        /// <summary>
        /// 后台获得商品评价列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetProductReviewListSort(string sortColumn, string sortDirection)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetProductReviewListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得商品评价数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetProductReviewCount(string condition)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetProductReviewCount(condition);
        }

        /// <summary>
        /// 后台获得商品评价回复列表
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <returns></returns>
        public static DataTable AdminGetProductReviewReplyList(int reviewId)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetProductReviewReplyList(reviewId);
        }

        /// <summary>
        /// 对商品评价投票
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <param name="uid">用户id</param>
        /// <param name="voteTime">投票时间</param>
        public static void VoteProductReview(int reviewId, int uid, DateTime voteTime)
        {
            BrnShop.Core.BSPData.RDBS.VoteProductReview(reviewId, uid, voteTime);
        }

        /// <summary>
        /// 是否对商品评价投过票
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <param name="uid">用户id</param>
        public static bool IsVoteProductReview(int reviewId, int uid)
        {
            return BrnShop.Core.BSPData.RDBS.IsVoteProductReview(reviewId, uid);
        }

        /// <summary>
        /// 更改商品评价状态
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <param name="state">评价状态</param>
        /// <returns></returns>
        public static bool ChangeProductReviewState(int reviewId, int state)
        {
            return BrnShop.Core.BSPData.RDBS.ChangeProductReviewState(reviewId, state);
        }

        /// <summary>
        /// 获得用户商品评价列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static List<ProductReviewInfo> GetUserProductReviewList(int uid, int pageSize, int pageNumber)
        {
            List<ProductReviewInfo> productReviewList = new List<ProductReviewInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetUserProductReviewList(uid, pageSize, pageNumber);
            while (reader.Read())
            {
                ProductReviewInfo productReviewInfo = BuildProductReviewFromReader(reader);
                productReviewList.Add(productReviewInfo);
            }
            reader.Close();
            return productReviewList;
        }

        /// <summary>
        /// 获得用户商品评价数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static int GetUserProductReviewCount(int uid)
        {
            return BrnShop.Core.BSPData.RDBS.GetUserProductReviewCount(uid);
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
            return BrnShop.Core.BSPData.RDBS.GetProductReviewList(pid, type, pageSize, pageNumber);
        }

        /// <summary>
        /// 获得商品评价数量
        /// </summary>
        /// <param name="pid">商品id</param>
        /// <param name="type">类型(0代表全部评价,1代表好评,2代表中评,3代表差评)</param>
        /// <returns></returns>
        public static int GetProductReviewCount(int pid, int type)
        {
            return BrnShop.Core.BSPData.RDBS.GetProductReviewCount(pid, type);
        }

        /// <summary>
        /// 获得商品评价及其回复
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <returns></returns>
        public static DataTable GetProductReviewWithReplyById(int reviewId)
        {
            return BrnShop.Core.BSPData.RDBS.GetProductReviewWithReplyById(reviewId);
        }

        /// <summary>
        /// 获得商品评价列表
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static DataTable GetProductReviewList(DateTime startTime, DateTime endTime)
        {
            return BrnShop.Core.BSPData.RDBS.GetProductReviewList(startTime, endTime);
        }
    }
}
