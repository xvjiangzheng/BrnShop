using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 商品评价信息类
    /// </summary>
    public class ProductReviewInfo
    {
        private int _reviewid;//商品评价id
        private int _pid;//商品id
        private int _uid;//用户id
        private int _oprid;//订单商品记录id
        private int _oid;//订单id
        private int _parentid;//父id
        private int _state;//状态
        private int _star;//星星
        private int _quality;//评价质量
        private string _message;//评价信息
        private DateTime _reviewtime;//评价时间
        private int _paycredits;//支付积分
        private string _pname;//商品名称
        private string _pshowimg;//商品展示图片
        private DateTime _buytime;//购买时间
        private string _ip;//ip


        /// <summary>
        /// 商品评价id
        /// </summary>
        public int ReviewId
        {
            get { return _reviewid; }
            set { _reviewid = value; }
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
        /// 用户id
        /// </summary>
        public int Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }
        /// <summary>
        /// 订单商品记录id
        /// </summary>
        public int OPRId
        {
            get { return _oprid; }
            set { _oprid = value; }
        }
        /// <summary>
        /// 订单id
        /// </summary>
        public int Oid
        {
            get { return _oid; }
            set { _oid = value; }
        }
        /// <summary>
        /// 父id
        /// </summary>
        public int ParentId
        {
            get { return _parentid; }
            set { _parentid = value; }
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
        /// 星星
        /// </summary>
        public int Star
        {
            get { return _star; }
            set { _star = value; }
        }
        /// <summary>
        /// 评价质量
        /// </summary>
        public int Quality
        {
            get { return _quality; }
            set { _quality = value; }
        }
        /// <summary>
        /// 评价信息
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        /// <summary>
        /// 评价时间
        /// </summary>
        public DateTime ReviewTime
        {
            get { return _reviewtime; }
            set { _reviewtime = value; }
        }
        /// <summary>
        /// 支付积分
        /// </summary>
        public int PayCredits
        {
            get { return _paycredits; }
            set { _paycredits = value; }
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
        /// 商品展示图片
        /// </summary>
        public string PShowImg
        {
            get { return _pshowimg; }
            set { _pshowimg = value; }
        }
        /// <summary>
        /// 购买时间
        /// </summary>
        public DateTime BuyTime
        {
            get { return _buytime; }
            set { _buytime = value; }
        }
        /// <summary>
        /// ip
        /// </summary>
        public string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }
    }
}
