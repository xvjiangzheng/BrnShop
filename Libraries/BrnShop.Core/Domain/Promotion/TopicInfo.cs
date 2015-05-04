using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 活动专题信息类
    /// </summary>
    public class TopicInfo
    {
        private int _topicid;//专题id
        private DateTime _starttime;//开始时间
        private DateTime _endtime;//结束时间
        private int _isshow;//是否显示
        private string _sn;//编号
        private string _title;//标题
        private string _headhtml;//头部html
        private string _bodyhtml;//主体html

        /// <summary>
        /// 专题id
        /// </summary>
        public int TopicId
        {
            get { return _topicid; }
            set { _topicid = value; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get { return _starttime; }
            set { _starttime = value; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get { return _endtime; }
            set { _endtime = value; }
        }
        /// <summary>
        /// 是否显示
        /// </summary>
        public int IsShow
        {
            get { return _isshow; }
            set { _isshow = value; }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string SN
        {
            get { return _sn; }
            set { _sn = value; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// 头部html
        /// </summary>
        public string HeadHtml
        {
            get { return _headhtml; }
            set { _headhtml = value; }
        }
        /// <summary>
        /// 主体html
        /// </summary>
        public string BodyHtml
        {
            get { return _bodyhtml; }
            set { _bodyhtml = value; }
        }
    }
}
