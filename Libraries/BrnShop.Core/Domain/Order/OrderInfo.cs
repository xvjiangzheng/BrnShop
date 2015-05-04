using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 订单信息类
    /// </summary>
    public class OrderInfo
    {
        private int _oid;//订单id
        private string _osn;//订单编号
        private int _uid;//用户id

        private int _orderstate;//订单状态

        private decimal _productamount;//商品合计
        private decimal _orderamount;//订单合计
        private decimal _surplusmoney;//剩余金钱

        private int _parentid;//父id
        private int _isreview;//是否评价
        private DateTime _addtime;//添加时间
        private string _shipsn;//配送单号
        private string _shipsystemname;//配送方式系统名
        private string _shipfriendname;//配送方式昵称
        private DateTime _shiptime;//配送时间
        private string _paysn;//支付单号
        private string _paysystemname;//支付方式系统名
        private string _payfriendname;//支付方式昵称
        private int _paymode;//支付方式(0代表货到付款，1代表在线付款，2代表线下付款)
        private DateTime _paytime;//支付时间

        private int _regionid;//配送区域id
        private string _consignee;//收货人
        private string _mobile;//手机号
        private string _phone;//固话号
        private string _email;//邮箱
        private string _zipcode;//邮政编码
        private string _address;//详细地址
        private DateTime _besttime;//最佳送货时间

        private decimal _shipfee;//配送费用
        private decimal _payfee;//支付费用
        private int _fullcut;//满减
        private decimal _discount;//折扣
        private int _paycreditcount;//支付积分数量
        private decimal _paycreditmoney;//支付积分金额
        private int _couponmoney;//优惠劵金额
        private int _weight;//重量

        private string _buyerremark;//买家备注
        private string _ip;//ip地址

        public OrderInfo()
        {
            DateTime time = new DateTime(1900, 1, 1);
            _addtime = DateTime.Now;
            _shipsn = "";
            _paysn = "";
            _besttime = time;
            _discount = 0M;
            _buyerremark = "";
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
        /// 订单编号
        /// </summary>
        public string OSN
        {
            get { return _osn; }
            set { _osn = value.TrimEnd(); }
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
        /// 订单状态
        /// </summary>
        public int OrderState
        {
            get { return _orderstate; }
            set { _orderstate = value; }
        }

        /// <summary>
        /// 商品合计
        /// </summary>
        public decimal ProductAmount
        {
            get { return _productamount; }
            set { _productamount = value; }
        }
        /// <summary>
        /// 订单合计
        /// </summary>
        public decimal OrderAmount
        {
            get { return _orderamount; }
            set { _orderamount = value; }
        }
        /// <summary>
        /// 剩余金钱
        /// </summary>
        public decimal SurplusMoney
        {
            get { return _surplusmoney; }
            set { _surplusmoney = value; }
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
        /// 是否评价
        /// </summary>
        public int IsReview
        {
            get { return _isreview; }
            set { _isreview = value; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        /// <summary>
        /// 配送单号
        /// </summary>
        public string ShipSN
        {
            get { return _shipsn; }
            set { _shipsn = value.TrimEnd(); }
        }
        /// <summary>
        /// 配送方式系统名
        /// </summary>
        public string ShipSystemName
        {
            get { return _shipsystemname; }
            set { _shipsystemname = value.TrimEnd(); }
        }
        /// <summary>
        /// 配送方式昵称
        /// </summary>
        public string ShipFriendName
        {
            get { return _shipfriendname; }
            set { _shipfriendname = value.TrimEnd(); }
        }
        /// <summary>
        /// 配送时间
        /// </summary>
        public DateTime ShipTime
        {
            get { return _shiptime; }
            set { _shiptime = value; }
        }
        /// <summary>
        /// 支付单号
        /// </summary>
        public string PaySN
        {
            get { return _paysn; }
            set { _paysn = value.TrimEnd(); }
        }
        /// <summary>
        /// 支付方式系统名
        /// </summary>
        public string PaySystemName
        {
            get { return _paysystemname; }
            set { _paysystemname = value.TrimEnd(); }
        }
        /// <summary>
        /// 支付方式昵称
        /// </summary>
        public string PayFriendName
        {
            get { return _payfriendname; }
            set { _payfriendname = value.TrimEnd(); }
        }
        /// <summary>
        /// 支付方式(0代表货到付款，1代表在线付款，2代表线下付款)
        /// </summary>
        public int PayMode
        {
            get { return _paymode; }
            set { _paymode = value; }
        }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PayTime
        {
            get { return _paytime; }
            set { _paytime = value; }
        }

        /// <summary>
        /// 配送区域id
        /// </summary>
        public int RegionId
        {
            get { return _regionid; }
            set { _regionid = value; }
        }
        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee
        {
            get { return _consignee; }
            set { _consignee = value; }
        }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }
        /// <summary>
        /// 固话号
        /// </summary>
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string ZipCode
        {
            get { return _zipcode; }
            set { _zipcode = value.TrimEnd(); }
        }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        /// <summary>
        /// 最佳送货时间
        /// </summary>
        public DateTime BestTime
        {
            get { return _besttime; }
            set { _besttime = value; }
        }

        /// <summary>
        /// 配送费用
        /// </summary>
        public decimal ShipFee
        {
            get { return _shipfee; }
            set { _shipfee = value; }
        }
        /// <summary>
        /// 支付费用
        /// </summary>
        public decimal PayFee
        {
            get { return _payfee; }
            set { _payfee = value; }
        }
        /// <summary>
        /// 满减
        /// </summary>
        public int FullCut
        {
            get { return _fullcut; }
            set { _fullcut = value; }
        }
        /// <summary>
        /// 折扣
        /// </summary>
        public decimal Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }
        /// <summary>
        /// 支付积分数量
        /// </summary>
        public int PayCreditCount
        {
            get { return _paycreditcount; }
            set { _paycreditcount = value; }
        }
        /// <summary>
        /// 支付积分金额
        /// </summary>
        public decimal PayCreditMoney
        {
            get { return _paycreditmoney; }
            set { _paycreditmoney = value; }
        }
        /// <summary>
        /// 优惠劵金额
        /// </summary>
        public int CouponMoney
        {
            get { return _couponmoney; }
            set { _couponmoney = value; }
        }
        /// <summary>
        /// 重量
        /// </summary>
        public int Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        /// <summary>
        /// 买家备注
        /// </summary>
        public string BuyerRemark
        {
            get { return _buyerremark; }
            set { _buyerremark = value; }
        }
        /// <summary>
        /// ip地址
        /// </summary>
        public string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }
    }
}

