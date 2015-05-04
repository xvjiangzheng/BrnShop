using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 新闻信息类
    /// </summary>
    public class NewsInfo
    {
        private int _newsid;//新闻id
        private int _newstypeid = 0;//新闻类型id
        private int _isshow = 0;//是否显示
        private int _istop = 0;//是否置顶
        private int _ishome = 0;//是否置首
        private int _displayorder = 0;//排序
        private DateTime _addtime;//添加时间
        private string _title = "";//标题
        private string _url = "";//网址
        private string _body = "";//内容


        /// <summary>
        /// 新闻id
        /// </summary>
        public int NewsId
        {
            set { _newsid = value; }
            get { return _newsid; }
        }
        /// <summary>
        /// 新闻类型id
        /// </summary>
        public int NewsTypeId
        {
            set { _newstypeid = value; }
            get { return _newstypeid; }
        }
        /// <summary>
        /// 是否显示
        /// </summary>
        public int IsShow
        {
            set { _isshow = value; }
            get { return _isshow; }
        }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public int IsTop
        {
            set { _istop = value; }
            get { return _istop; }
        }
        /// <summary>
        /// 是否置首
        /// </summary>
        public int IsHome
        {
            set { _ishome = value; }
            get { return _ishome; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 网址
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Body
        {
            set { _body = value; }
            get { return _body; }
        }
    }
}
