using System;

namespace BrnShop.Web.Framework
{
    /// <summary>
    /// 分页模型
    /// </summary>
    public class PageModel
    {
        private int _pageindex;//当前页索引
        private int _pagenumber;//当前页数
        private int _prepagenumber;//上一页数
        private int _nextpagenumber;//下一页数
        private int _pagesize;//每页数
        private int _totalcount;//总项数
        private int _totalpages;//总页数
        private bool _hasprepage;//是否有上一页
        private bool _hasnextpage;//是否有下一页
        private bool _isfirstpage;//是否是第一页
        private bool _islastpage;//是否是最后一页

        public PageModel(int pageSize, int pageNumber, int totalCount)
        {
            if (pageSize > 0)
                _pagesize = pageSize;
            else
                _pagesize = 1;

            if (pageNumber > 0)
                _pagenumber = pageNumber;
            else
                _pagenumber = 1;

            if (totalCount > 0)
                _totalcount = totalCount;
            else
                _totalcount = 0;

            _pageindex = _pagenumber - 1;

            _totalpages = _totalcount / _pagesize;
            if (_totalcount % _pagesize > 0)
                _totalpages++;

            _hasprepage = _pagenumber > 1;
            _hasnextpage = _pagenumber < _totalpages;

            _isfirstpage = _pagenumber == 1;
            _islastpage = _pagenumber == _totalpages;

            _prepagenumber = _pagenumber < 2 ? 1 : _pagenumber - 1;
            _nextpagenumber = _pagenumber < _totalpages ? _pagenumber + 1 : _totalpages;
        }

        /// <summary>
        /// 当前页索引
        /// </summary>
        public int PageIndex
        {
            get { return _pageindex; }
            set { _pageindex = value; }
        }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageNumber
        {
            get { return _pagenumber; }
            set { _pagenumber = value; }
        }
        /// <summary>
        /// 上一页数
        /// </summary>
        public int PrePageNumber
        {
            get { return _prepagenumber; }
            set { _prepagenumber = value; }
        }
        /// <summary>
        /// 下一页数
        /// </summary>
        public int NextPageNumber
        {
            get { return _nextpagenumber; }
            set { _nextpagenumber = value; }
        }
        /// <summary>
        /// 每页数
        /// </summary>
        public int PageSize
        {
            get { return _pagesize; }
            set { _pagesize = value; }
        }
        /// <summary>
        /// 总项数
        /// </summary>
        public int TotalCount
        {
            get { return _totalcount; }
            set { _totalcount = value; }
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages
        {
            get { return _totalpages; }
            set { _totalpages = value; }
        }
        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPrePage
        {
            get { return _hasprepage; }
            set { _hasprepage = value; }
        }
        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPage
        {
            get { return _hasnextpage; }
            set { _hasnextpage = value; }
        }
        /// <summary>
        /// 是否是第一页
        /// </summary>
        public bool IsFirstPage
        {
            get { return _isfirstpage; }
            set { _isfirstpage = value; }
        }
        /// <summary>
        /// 是否是最后一页
        /// </summary>
        public bool IsLastPage
        {
            get { return _islastpage; }
            set { _islastpage = value; }
        }
    }
}