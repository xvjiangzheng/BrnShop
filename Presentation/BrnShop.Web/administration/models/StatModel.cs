using System;
using System.Data;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;

namespace BrnShop.Web.Admin.Models
{
    /// <summary>
    /// 在线用户列表模型类
    /// </summary>
    public class OnlineUserListModel
    {
        public PageModel PageModel { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public List<OnlineUserInfo> OnlineUserList { get; set; }
        /// <summary>
        /// 省id
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 市id
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 区/县id
        /// </summary>
        public int RegionId { get; set; }
    }

    /// <summary>
    /// 在线用户趋势模型类
    /// </summary>
    public class OnlineUserTrendModel
    {
        public List<PVStatInfo> PVStatList { get; set; }
    }

    /// <summary>
    /// 搜索词统计列表模型类
    /// </summary>
    public class SearchWordStatListModel
    {
        public PageModel PageModel { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public DataTable SearchWordStatList { get; set; }
        /// <summary>
        /// 搜索词
        /// </summary>
        public string Word { get; set; }
    }

    /// <summary>
    /// 客户端统计模型类
    /// </summary>
    public class ClientStatModel
    {
        /// <summary>
        /// 浏览器统计
        /// </summary>
        public List<PVStatInfo> BrowserStat { get; set; }
        /// <summary>
        /// 操作系统统计
        /// </summary>
        public List<PVStatInfo> OSStat { get; set; }
    }

    /// <summary>
    /// 地区统计模型类
    /// </summary>
    public class RegionStatModel
    {
        /// <summary>
        /// 地区统计
        /// </summary>
        public DataTable RegionStat { get; set; }
    }

    /// <summary>
    /// 商品统计模型类
    /// </summary>
    public class ProductStatModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 商品列表
        /// </summary>
        public DataTable ProductList { get; set; }
        /// <summary>
        /// 排序列
        /// </summary>
        public string SortColumn { get; set; }
        /// <summary>
        /// 排序方向
        /// </summary>
        public string SortDirection { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 分类id
        /// </summary>
        public int CateId { get; set; }
        /// <summary>
        /// 品牌id
        /// </summary>
        public int BrandId { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }
    }

    /// <summary>
    /// 销售明细模型类
    /// </summary>
    public class SaleListModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 销售商品列表
        /// </summary>
        public DataTable SaleProductList { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
    }

    /// <summary>
    /// 销售趋势模型类
    /// </summary>
    public class SaleTrendModel
    {
        /// <summary>
        /// 趋势类型(0代表订单数，1代表订单合计)
        /// </summary>
        public int TrendType { get; set; }
        /// <summary>
        /// 时间类型(0代表小时，1代表天，2代表月，3代表年)
        /// </summary>
        public int TimeType { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 趋势项列表
        /// </summary>
        public DataTable TrendItemList { get; set; }
    }
}
