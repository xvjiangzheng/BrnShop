using System;
using System.Web;
using System.Data;
using System.Text;
using System.Web.Mvc;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Admin.Models;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台报表统计控制器类
    /// </summary>
    public partial class StatController : BaseAdminController
    {
        /// <summary>
        /// 在线用户列表
        /// </summary>
        /// <param name="provinceId">省id</param>
        /// <param name="cityId">市id</param>
        /// <param name="regionId">区/县id</param>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">每页数</param>
        /// <returns></returns>
        public ActionResult OnlineUserList(string sortColumn, string sortDirection, int provinceId = -1, int cityId = -1, int regionId = -1, int pageNumber = 1, int pageSize = 15)
        {
            int locationType = 0, locationId = 0;
            if (regionId > 0)
            {
                locationType = 2;
                locationId = regionId;
            }
            else if (cityId > 0)
            {
                locationType = 1;
                locationId = cityId;
            }
            else if (provinceId > 0)
            {
                locationType = 0;
                locationId = provinceId;
            }

            string sort = OnlineUsers.GetOnlineUserListSort(sortColumn, sortDirection);
            PageModel pageModel = new PageModel(pageSize, pageNumber, OnlineUsers.GetOnlineUserCount(locationType, locationId));

            OnlineUserListModel model = new OnlineUserListModel()
            {
                OnlineUserList = OnlineUsers.GetOnlineUserList(pageModel.PageSize, pageModel.PageNumber, locationType, locationId, sort),
                PageModel = pageModel,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                ProvinceId = provinceId,
                CityId = cityId,
                RegionId = regionId
            };

            ShopUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&sortColumn={3}&sortDirection={4}&provinceId={5}&cityId={6}&regionId={7}",
                                                          Url.Action("onlineuserlist"),
                                                          pageModel.PageNumber, pageModel.PageSize,
                                                          sortColumn, sortDirection,
                                                          provinceId, cityId, regionId));
            return View(model);
        }

        /// <summary>
        /// 在线用户趋势
        /// </summary>
        /// <returns></returns>
        public ActionResult OnlineUserTrend()
        {
            OnlineUserTrendModel model = new OnlineUserTrendModel();

            model.PVStatList = PVStats.GetTodayHourPVStatList();
            ViewData["referer"] = ShopUtils.GetAdminRefererCookie();

            return View(model);
        }

        /// <summary>
        /// 搜索词统计列表
        /// </summary>
        /// <param name="word">搜索词</param>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">每页数</param>
        /// <returns></returns>
        public ActionResult SearchWordStatList(string word, string sortColumn, string sortDirection, int pageNumber = 1, int pageSize = 15)
        {
            string sort = AdminSearchHistories.GetSearchWordStatListSort(sortColumn, sortDirection);
            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminSearchHistories.GetSearchWordStatCount(word));

            SearchWordStatListModel model = new SearchWordStatListModel()
            {
                SearchWordStatList = AdminSearchHistories.GetSearchWordStatList(pageModel.PageSize, pageModel.PageNumber, word, sort),
                PageModel = pageModel,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                Word = word
            };
            return View(model);
        }

        /// <summary>
        /// 客户端统计
        /// </summary>
        /// <returns></returns>
        public ActionResult ClientStat()
        {
            ClientStatModel model = new ClientStatModel();

            model.BrowserStat = PVStats.GetBrowserStat();
            model.OSStat = PVStats.GetOSStat();

            return View(model);
        }

        /// <summary>
        /// 地区统计
        /// </summary>
        /// <returns></returns>
        public ActionResult RegionStat()
        {
            RegionStatModel model = new RegionStatModel();

            model.RegionStat = PVStats.GetProvinceRegionStat();

            return View(model);
        }

        /// <summary>
        /// 商品统计
        /// </summary>
        /// <param name="productName">商品名称</param>
        /// <param name="categoryName">分类名称</param>
        /// <param name="brandName">品牌名称</param>
        /// <param name="sortOptions">排序</param>
        /// <param name="cateId">分类id</param>
        /// <param name="brandId">品牌id</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">每页数</param>
        /// <returns></returns>
        public ActionResult ProductStat(string productName, string categoryName, string brandName, string sortColumn, string sortDirection, int cateId = -1, int brandId = -1, int pageNumber = 1, int pageSize = 15)
        {
            string condition = AdminProducts.AdminGetProductListCondition(productName, cateId, brandId, -1);
            string sort = AdminProducts.AdminGetProductListSort(sortColumn, sortDirection);

            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminProducts.AdminGetProductCount(condition));

            DataTable productList = AdminProducts.AdminGetProductList(pageModel.PageSize, pageModel.PageNumber, condition, sort);
            StringBuilder pidList = new StringBuilder();
            foreach (DataRow row in productList.Rows)
            {
                pidList.AppendFormat("{0},", row["pid"]);
            }

            ProductStatModel model = new ProductStatModel()
            {
                ProductList = pidList.Length > 0 ? AdminProducts.GetProductSummaryList(pidList.Remove(pidList.Length - 1, 1).ToString()) : new DataTable(),
                PageModel = pageModel,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                ProductName = productName,
                CateId = cateId,
                BrandId = brandId,
                CategoryName = string.IsNullOrWhiteSpace(categoryName) ? "全部分类" : categoryName,
                BrandName = string.IsNullOrWhiteSpace(brandName) ? "全部品牌" : brandName
            };
            return View(model);
        }

        /// <summary>
        /// 销售明细
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">每页数</param>
        /// <returns></returns>
        public ActionResult SaleList(string startTime, string endTime, int pageNumber = 1, int pageSize = 15)
        {
            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminOrders.GetSaleProductCount(startTime, endTime));

            SaleListModel model = new SaleListModel()
            {
                SaleProductList = AdminOrders.GetSaleProductList(pageModel.PageSize, pageModel.PageNumber, startTime, endTime),
                PageModel = pageModel,
                StartTime = startTime,
                EndTime = endTime
            };
            return View(model);
        }

        /// <summary>
        /// 销售趋势
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="trendType">趋势类型(0代表订单数，1代表订单合计)</param>
        /// <param name="timeType">时间类型(0代表小时，1代表天，2代表月，3代表年)</param>
        /// <returns></returns>
        public ActionResult SaleTrend(string startTime = "0", string endTime = "23", int trendType = 0, int timeType = 0)
        {
            if (string.IsNullOrWhiteSpace(startTime) || string.IsNullOrWhiteSpace(endTime))
                return PromptView(Url.Action("saletrend"), "请输入筛选时间");

            SaleTrendModel model = new SaleTrendModel();

            model.StartTime = startTime;
            model.EndTime = endTime;

            trendType = trendType == 0 ? 0 : 1;
            model.TrendType = trendType;

            if (timeType == 3)//按年筛选
            {
                string startYear = new DateTime(TypeHelper.StringToInt(startTime, DateTime.Now.Year), 1, 1).ToString();
                string endYear = new DateTime((TypeHelper.StringToInt(endTime, DateTime.Now.Year) + 1), 1, 1).ToString();
                model.TrendItemList = AdminOrders.GetSaleTrend(trendType, 3, startYear, endYear);
                model.TimeType = 3;
            }
            else if (timeType == 2)//按月筛选
            {
                string startMonth = TypeHelper.StringToDateTime(startTime).ToString();
                string endMonth = (TypeHelper.StringToDateTime(endTime).AddMonths(1)).ToString();
                model.TrendItemList = AdminOrders.GetSaleTrend(trendType, 2, startMonth, endMonth);
                model.TimeType = 2;
            }
            else if (timeType == 1)//按天筛选
            {
                string startDay = TypeHelper.StringToDateTime(startTime).ToString();
                string endDay = (TypeHelper.StringToDateTime(endTime).AddDays(1)).ToString();
                model.TrendItemList = AdminOrders.GetSaleTrend(trendType, 1, startDay, endDay);
                model.TimeType = 1;
            }
            else//按小时筛选
            {
                int startHour = TypeHelper.StringToInt(startTime, -1);
                int endHour = TypeHelper.StringToInt(endTime, -1);

                if (startHour < 0 || startHour > 23)
                    startHour = 0;
                if (endHour < 0 || endHour > 23)
                    endHour = 23;

                startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, startHour, 0, 0).ToString();
                endTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, endHour, 59, 59).ToString();
                model.TrendItemList = AdminOrders.GetSaleTrend(trendType, 0, startTime, endTime);
                model.TimeType = 0;
            }

            return View(model);
        }
    }
}
