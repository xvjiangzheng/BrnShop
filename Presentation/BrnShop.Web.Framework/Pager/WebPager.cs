using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Specialized;

namespace BrnShop.Web.Framework
{
    /// <summary>
    /// 前台分页类
    /// </summary>
    public class WebPager : Pager
    {
        private string _routename = null;//路由名
        private string _pageparamname = "page";//页参数名
        private ViewContext _viewcontext = null;//视图上下文
        private RouteValueDictionary _routevalues = new RouteValueDictionary();//路由值集合

        public WebPager(PageModel pageModel, ViewContext viewContext)
            : base(pageModel)
        {
            _viewcontext = viewContext;

            NameValueCollection queryString = _viewcontext.RequestContext.HttpContext.Request.QueryString;
            foreach (string key in queryString.AllKeys)
            {
                if (key != null)
                    _routevalues.Add(key, queryString[key]);
            }

            if (!_routevalues.ContainsKey(_pageparamname))
                _routevalues.Add(_pageparamname, 1);
        }

        /// <summary>
        /// 设置路由名
        /// </summary>
        /// <param name="name">路由名称</param>
        /// <returns></returns>
        public Pager RouteName(string name)
        {
            _routename = name;
            return this;
        }

        /// <summary>
        /// 设置页参数名
        /// </summary>
        /// <param name="name">页参数名称</param>
        /// <returns></returns>
        public Pager PageParamName(string name)
        {
            _pageparamname = name;
            return this;
        }

        /// <summary>
        /// 设置路由值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public Pager RouteValues(string key, string value)
        {
            if (_routevalues.ContainsKey(key))
                _routevalues[key] = value;
            else
                _routevalues.Add(key, value);
            return this;
        }

        public sealed override string ToString()
        {
            if (_pagemodel.TotalCount == 0 || _pagemodel.TotalCount <= _pagemodel.PageSize)
                return null;

            StringBuilder html = new StringBuilder("<div class=\"page\">");

            if (_showsummary)
            {
                html.AppendFormat("当前{0}/{1}页&nbsp;共{2}条记录 ", _pagemodel.PageNumber, _pagemodel.TotalPages, _pagemodel.TotalCount);
            }

            if (_showfirst)
            {
                if (_pagemodel.IsFirstPage)
                    html.Append("<a href=\"javascript:void(0)\">首页</a> ");
                else
                    html.AppendFormat("<a href=\"{0}\"> 首页</a> ", CreateUrl(1));
            }

            if (_showpre)
            {
                if (_pagemodel.HasPrePage)
                    html.AppendFormat("<a href=\"{0}\">上一页</a> ", CreateUrl(_pagemodel.PrePageNumber));
                else
                    html.Append("<a href=\"javascript:void(0)\">上一页</a> ");
            }

            if (_showitems)
            {
                int startPageNumber = GetStartPageNumber();
                int endPageNumber = GetEndPageNumber();
                for (int i = startPageNumber; i <= endPageNumber; i++)
                {
                    if (_pagemodel.PageNumber == i)
                        html.AppendFormat("<a href=\"javascript:void(0)\" class=\"hot\">{0}</a> ", i);
                    else
                        html.AppendFormat("<a href=\"{1}\">{0}</a> ", i, CreateUrl(i));
                }
            }

            if (_shownext)
            {
                if (_pagemodel.HasNextPage)
                    html.AppendFormat("<a href=\"{0}\">下一页</a> ", CreateUrl(_pagemodel.NextPageNumber));
                else
                    html.Append("<a href=\"javascript:void(0)\">下一页</a> ");
            }

            if (_showlast)
            {
                if (_pagemodel.IsLastPage)
                    html.Append("<a href=\"javascript:void(0)\">末页</a>");
                else
                    html.AppendFormat("<a href=\"{0}\">末页</a>", CreateUrl(_pagemodel.TotalPages));
            }

            html.Append("</div>");
            return html.ToString();
        }

        /// <summary>
        /// 生成链接地址
        /// </summary>
        /// <param name="pageNumber">页数</param>
        /// <returns></returns>
        private string CreateUrl(int pageNumber)
        {
            _routevalues[_pageparamname] = pageNumber;
            return UrlHelper.GenerateUrl(_routename, null, null, _routevalues, RouteTable.Routes, _viewcontext.RequestContext, true);
        }
    }
}
