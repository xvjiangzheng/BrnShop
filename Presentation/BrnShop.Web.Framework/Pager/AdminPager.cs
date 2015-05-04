using System;
using System.Text;

namespace BrnShop.Web.Framework
{
    /// <summary>
    /// 后台分页类
    /// </summary>
    public class AdminPager : Pager
    {
        public AdminPager(PageModel pageModel)
            : base(pageModel)
        {
        }

        public sealed override string ToString()
        {
            if (_pagemodel.TotalCount == 0 || _pagemodel.TotalCount <= _pagemodel.PageSize)
                return null;

            StringBuilder html = new StringBuilder();

            if (_showsummary)
            {
                html.Append(string.Format("<div class=\"summary\">当前{2}/{1}页&nbsp;共{0}条记录</div>", _pagemodel.TotalCount, _pagemodel.TotalPages, _pagemodel.PageNumber));
                html.Append("&nbsp;");
            }

            if (_showpagesize)
            {
                html.AppendFormat("每页:<input type=\"text\" value=\"{0}\" id=\"pageSize\" name=\"pageSize\" size=\"1\"/>", _pagemodel.PageSize);
            }

            if (_showfirst)
            {
                if (_pagemodel.IsFirstPage)
                    html.Append("<a href=\"#\">首页</a>");
                else
                    html.Append("<a href=\"#\" page=\"1\" class=\"bt\">首页</a>");
            }

            if (_showpre)
            {
                if (_pagemodel.HasPrePage)
                    html.AppendFormat("<a href=\"#\" page=\"{0}\" class=\"bt\">上一页</a>", _pagemodel.PageNumber - 1);
                else
                    html.Append("<a href=\"#\">上一页</a>");
            }

            if (_showitems)
            {
                int startPageNumber = GetStartPageNumber();
                int endPageNumber = GetEndPageNumber();
                for (int i = startPageNumber; i <= endPageNumber; i++)
                {
                    if (_pagemodel.PageNumber != i)
                        html.AppendFormat("<a href=\"#\" page=\"{0}\" class=\"bt\">{0}</a>", i);
                    else
                        html.AppendFormat("<a href=\"\" class=\"hot\">{0}</a>", i);
                }
            }

            if (_shownext)
            {
                if (_pagemodel.HasNextPage)
                    html.AppendFormat("<a href=\"#\" page=\"{0}\" class=\"bt\">下一页</a>", _pagemodel.PageNumber + 1);
                else
                    html.Append("<a href=\"#\">下一页</a>");
            }

            if (_showlast)
            {
                if (_pagemodel.IsLastPage)
                    html.Append("<a href=\"#\">末页</a>");
                else
                    html.AppendFormat("<a href=\"#\" page=\"{0}\" class=\"bt\">末页</a>", _pagemodel.TotalPages);
            }

            if (_showgopage)
            {
                html.AppendFormat("跳转到:<input type=\"text\" value=\"{0}\" id=\"pageNumber\" totalPages=\"{1}\" name=\"pageNumber\" size=\"1\"/>页", _pagemodel.PageNumber, _pagemodel.TotalPages);
            }

            return html.ToString();
        }
    }
}
