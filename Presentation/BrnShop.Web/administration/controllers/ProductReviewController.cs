using System;
using System.Data;
using System.Web.Mvc;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Admin.Models;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台商品评价控制器类
    /// </summary>
    public partial class ProductReviewController : BaseAdminController
    {
        /// <summary>
        /// 商品评价列表
        /// </summary>
        public ActionResult ProductReviewList(string productName, string message, string rateStartTime, string rateEndTime, string sortColumn, string sortDirection, int pid = -1, int pageNumber = 1, int pageSize = 15)
        {
            string condition = AdminProductReviews.AdminGetProductReviewListCondition(pid, message, rateStartTime, rateEndTime);
            string sort = AdminProductReviews.AdminGetProductReviewListSort(sortColumn, sortDirection);

            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminProductReviews.AdminGetProductReviewCount(condition));

            ProductReviewListModel model = new ProductReviewListModel()
            {
                PageModel = pageModel,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                ProductReviewList = AdminProductReviews.AdminGetProductReviewList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                Pid = pid,
                ProductName = pid > 0 ? productName : "选择商品",
                Message = message,
                StartTime = rateStartTime,
                EndTime = rateEndTime
            };
            ShopUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&sortColumn={3}&sortDirection={4}&message={5}&pid={6}&productName={7}&startTime={8}&endTime={9}",
                                                            Url.Action("productreviewlist"),
                                                            pageModel.PageNumber, pageModel.PageSize,
                                                            sortColumn, sortDirection,
                                                            message,
                                                            pid, productName,
                                                            rateStartTime, rateEndTime));
            return View(model);
        }

        /// <summary>
        /// 商品评价回复列表
        /// </summary>
        public ActionResult ProductReviewReplyList(int reviewId = -1)
        {
            ProductReviewInfo productReviewInfo = AdminProductReviews.AdminGetProductReviewById(reviewId);
            if (productReviewInfo == null)
                return PromptView("商品评价不存在");

            ProductReviewReplyListModel model = new ProductReviewReplyListModel()
            {
                ProductReviewReplyList = AdminProductReviews.AdminGetProductReviewReplyList(reviewId),
            };
            ShopUtils.SetAdminRefererCookie(string.Format("{0}?reviewId={1}", Url.Action("productreviewreplylist"), reviewId));
            return View(model);
        }

        /// <summary>
        /// 改变商品评价的状态
        /// </summary>
        public ActionResult ChangeProductReviewState(int reviewId = -1, int state = -1)
        {
            bool result = AdminProductReviews.ChangeProductReviewState(reviewId, state);
            if (result)
            {
                AddAdminOperateLog("修改商品评价状态", "修改商品评价状态,商品评价ID和状态为:" + reviewId + "_" + state);
                return Content("1");
            }
            else
            {
                return Content("0");
            }
        }

        /// <summary>
        /// 删除商品评价
        /// </summary>
        public ActionResult DelProductReview(int reviewId)
        {
            AdminProductReviews.DeleteProductReviewById(reviewId);
            AddAdminOperateLog("删除商品评价", "删除商品评价,商品评价ID为:" + reviewId);
            return PromptView("商品评价删除成功");
        }
    }
}
