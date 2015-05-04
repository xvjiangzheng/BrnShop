using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 单品促销活动信息类
    /// </summary>
    public class SinglePromotionInfo
    {
        private int _pmid;//促销活动id
        private int _pid;//商品id
        private DateTime _starttime1;//开始时间1
        private DateTime _endtime1;//结束时间1
        private DateTime _starttime2;//开始时间2
        private DateTime _endtime2;//结束时间2
        private DateTime _starttime3;//开始时间1
        private DateTime _endtime3;//结束时间2
        private int _userranklower;//用户等级下限
        private int _state;//状态
        private string _name;//活动名称
        private string _slogan;//活动广告语
        private int _discounttype;//折扣类型,0代表折扣，1代表直降，2代表折后价
        private int _discountvalue;//折扣值
        private int _coupontypeid;//优惠劵类型id
        private int _paycredits;//支付积分
        private int _isstock;//是否限制库存
        private int _stock;//库存
        private int _quotalower;//订单配额上限
        private int _quotaupper;//订单配额下限
        private int _allowbuycount;//最大购买数量


        /// <summary>
        /// 促销活动id
        /// </summary>
        public int PmId
        {
            get { return _pmid; }
            set { _pmid = value; }
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
        /// 开始时间1
        /// </summary>
        public DateTime StartTime1
        {
            get { return _starttime1; }
            set { _starttime1 = value; }
        }
        /// <summary>
        /// 结束时间1
        /// </summary>
        public DateTime EndTime1
        {
            get { return _endtime1; }
            set { _endtime1 = value; }
        }
        /// <summary>
        /// 开始时间2
        /// </summary>
        public DateTime StartTime2
        {
            get { return _starttime2; }
            set { _starttime2 = value; }
        }
        /// <summary>
        /// 结束时间2
        /// </summary>
        public DateTime EndTime2
        {
            get { return _endtime2; }
            set { _endtime2 = value; }
        }
        /// <summary>
        /// 开始时间3
        /// </summary>
        public DateTime StartTime3
        {
            get { return _starttime3; }
            set { _starttime3 = value; }
        }
        /// <summary>
        /// 结束时间3
        /// </summary>
        public DateTime EndTime3
        {
            get { return _endtime3; }
            set { _endtime3 = value; }
        }
        /// <summary>
        /// 用户等级下限
        /// </summary>
        public int UserRankLower
        {
            get { return _userranklower; }
            set { _userranklower = value; }
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
        /// 活动名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 活动广告语
        /// </summary>
        public string Slogan
        {
            get { return _slogan; }
            set { _slogan = value; }
        }
        /// <summary>
        /// 折扣类型,0代表折扣，1代表直降，2代表折后价
        /// </summary>
        public int DiscountType
        {
            get { return _discounttype; }
            set { _discounttype = value; }
        }
        /// <summary>
        /// 折扣值
        /// </summary>
        public int DiscountValue
        {
            get { return _discountvalue; }
            set { _discountvalue = value; }
        }
        /// <summary>
        /// 优惠劵类型id
        /// </summary>
        public int CouponTypeId
        {
            get { return _coupontypeid; }
            set { _coupontypeid = value; }
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
        /// 是否限制库存
        /// </summary>
        public int IsStock
        {
            get { return _isstock; }
            set { _isstock = value; }
        }
        /// <summary>
        /// 库存
        /// </summary>
        public int Stock
        {
            get { return _stock; }
            set { _stock = value; }
        }
        /// <summary>
        /// 订单配额上限
        /// </summary>
        public int QuotaLower
        {
            get { return _quotalower; }
            set { _quotalower = value; }
        }
        /// <summary>
        /// 订单配额下限
        /// </summary>
        public int QuotaUpper
        {
            get { return _quotaupper; }
            set { _quotaupper = value; }
        }
        /// <summary>
        /// 最大购买数量
        /// </summary>
        public int AllowBuyCount
        {
            get { return _allowbuycount; }
            set { _allowbuycount = value; }
        }
    }
}
