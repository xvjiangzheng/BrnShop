using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 商品咨询信息类
    /// </summary>
    public class ProductConsultInfo
    {
        private int _consultid;//咨询id
        private int _pid;//商品id
        private int _consulttypeid;//咨询类型id
        private int _state;//状态(0代表显示，1代表屏蔽)
        private int _consultuid;//咨询用户id
        private int _replyuid;//回复用户id
        private DateTime _consulttime;//咨询时间
        private DateTime _replytime;//回复时间
        private string _consultmessage;//咨询内容
        private string _replymessage;//回复内容
        private string _consultnickname;//咨询昵称
        private string _replynickname;//回复昵称
        private string _pname;//商品名称
        private string _pshowimg;//商品图片
        private string _consultip;//咨询ip
        private string _replyip;//回复ip

        /// <summary>
        /// 咨询id
        /// </summary>
        public int ConsultId
        {
            get { return _consultid; }
            set { _consultid = value; }
        }
        /// <summary>
        /// 商品id
        /// </summary>
        public int Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }
        /// <summary>
        /// 咨询类型id
        /// </summary>
        public int ConsultTypeId
        {
            get { return _consulttypeid; }
            set { _consulttypeid = value; }
        }
        /// <summary>
        /// 状态(0代表显示，1代表屏蔽)
        /// </summary>
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }
        /// <summary>
        /// 咨询用户id
        /// </summary>
        public int ConsultUid
        {
            get { return _consultuid; }
            set { _consultuid = value; }
        }
        /// <summary>
        /// 回复用户id
        /// </summary>
        public int ReplyUid
        {
            get { return _replyuid; }
            set { _replyuid = value; }
        }
        /// <summary>
        /// 咨询时间
        /// </summary>
        public DateTime ConsultTime
        {
            get { return _consulttime; }
            set { _consulttime = value; }
        }
        /// <summary>
        /// 回复时间
        /// </summary>
        public DateTime ReplyTime
        {
            get { return _replytime; }
            set { _replytime = value; }
        }
        /// <summary>
        /// 咨询内容
        /// </summary>
        public string ConsultMessage
        {
            get { return _consultmessage; }
            set { _consultmessage = value; }
        }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string ReplyMessage
        {
            get { return _replymessage; }
            set { _replymessage = value; }
        }
        /// <summary>
        /// 咨询昵称
        /// </summary>
        public string ConsultNickName
        {
            get { return _consultnickname; }
            set { _consultnickname = value; }
        }
        /// <summary>
        /// 回复昵称
        /// </summary>
        public string ReplyNickName
        {
            get { return _replynickname; }
            set { _replynickname = value; }
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string PName
        {
            get { return _pname; }
            set { _pname = value; }
        }
        /// <summary>
        /// 商品图片
        /// </summary>
        public string PShowImg
        {
            get { return _pshowimg; }
            set { _pshowimg = value; }
        }
        /// <summary>
        /// 咨询ip
        /// </summary>
        public string ConsultIP
        {
            get { return _consultip; }
            set { _consultip = value; }
        }
        /// <summary>
        /// 回复ip
        /// </summary>
        public string ReplyIP
        {
            get { return _replyip; }
            set { _replyip = value; }
        }
    }
}
