using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 广告信息类
    /// </summary>
    public class AdvertInfo
    {
        private int _adid;//广告id
        private int _clickcount;//点击数
        private int _adposid;//广告位置id
        private int _state;//状态
        private DateTime _starttime;//开始时间
        private DateTime _endtime;//结束时间
        private int _displayorder;//排序
        private int _type;//类型(0代表文字，1代表图片，2代表flash，3代表代码)
        private string _title;//标题
        private string _url;//网址
        private string _body;//主体
        private string _extfield1;//扩展字段1
        private string _extfield2;//扩展字段2
        private string _extfield3;//扩展字段3
        private string _extfield4;//扩展字段4
        private string _extfield5;//扩展字段5

        /// <summary>
        /// 广告id
        /// </summary>
        public int AdId
        {
            get { return _adid; }
            set { _adid = value; }
        }
        /// <summary>
        /// 点击数
        /// </summary>
        public int ClickCount
        {
            get { return _clickcount; }
            set { _clickcount = value; }
        }
        /// <summary>
        /// 广告位置id
        /// </summary>
        public int AdPosId
        {
            get { return _adposid; }
            set { _adposid = value; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int State
        {
            get { return _state; }
            set { _state = value; }
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
        /// 排序
        /// </summary>
        public int DisplayOrder
        {
            get { return _displayorder; }
            set { _displayorder = value; }
        }
        /// <summary>
        /// 类型(0代表文字，1代表图片，2代表flash，3代表代码)
        /// </summary>
        public int Type
        {
            get { return _type; }
            set { _type = value; }
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
        /// 网址
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        /// <summary>
        /// 主体
        /// </summary>
        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }
        /// <summary>
        /// 扩展字段1
        /// </summary>
        public string ExtField1
        {
            get { return _extfield1; }
            set { _extfield1 = value; }
        }
        /// <summary>
        /// 扩展字段2
        /// </summary>
        public string ExtField2
        {
            get { return _extfield2; }
            set { _extfield2 = value; }
        }
        /// <summary>
        /// 扩展字段3
        /// </summary>
        public string ExtField3
        {
            get { return _extfield3; }
            set { _extfield3 = value; }
        }
        /// <summary>
        /// 扩展字段4
        /// </summary>
        public string ExtField4
        {
            get { return _extfield4; }
            set { _extfield4 = value; }
        }
        /// <summary>
        /// 扩展字段5
        /// </summary>
        public string ExtField5
        {
            get { return _extfield5; }
            set { _extfield5 = value; }
        }
    }
}
