using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 订单退款信息类
    /// </summary>
    public class OrderRefundInfo
    {
        private int _refundid;//退款id
        private int _oid;//订单id
        private string _osn;//订单编号
        private int _uid;//用户id
        private int _state = 0;//状态(0代表未退款,1代表已退款)
        private DateTime _applytime;//申请时间
        private decimal _paymoney;//支付金额
        private decimal _refundmoney;//退款金额
        private string _refundsn = "";//退款单号
        private string _refundsystemname = "";//退款方式系统名
        private string _refundfriendname = "";//退款方式昵称
        private DateTime _refundtime = new DateTime(1900, 1, 1);//退款时间
        private string _paysn;//支付单号
        private string _paysystemname;//支付方式系统名
        private string _payfriendname;//支付方式昵称

        /// <summary>
        /// 退款id
        /// </summary>
        public int RefundId
        {
            get { return _refundid; }
            set { _refundid = value; }
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
            set { _osn = value; }
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
        /// 状态(0代表未退款,1代表已退款)
        /// </summary>
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime
        {
            get { return _applytime; }
            set { _applytime = value; }
        }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal PayMoney
        {
            get { return _paymoney; }
            set { _paymoney = value; }
        }
        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal RefundMoney
        {
            get { return _refundmoney; }
            set { _refundmoney = value; }
        }
        /// <summary>
        /// 退款单号
        /// </summary>
        public string RefundSN
        {
            get { return _refundsn; }
            set { _refundsn = value; }
        }
        /// <summary>
        /// 退款方式系统名
        /// </summary>
        public string RefundSystemName
        {
            get { return _refundsystemname; }
            set { _refundsystemname = value; }
        }
        /// <summary>
        /// 退款方式昵称
        /// </summary>
        public string RefundFriendName
        {
            get { return _refundfriendname; }
            set { _refundfriendname = value; }
        }
        /// <summary>
        /// 退款时间
        /// </summary>
        public DateTime RefundTime
        {
            get { return _refundtime; }
            set { _refundtime = value; }
        }
        /// <summary>
        /// 支付单号
        /// </summary>
        public string PaySN
        {
            get { return _paysn; }
            set { _paysn = value; }
        }
        /// <summary>
        /// 支付方式系统名
        /// </summary>
        public string PaySystemName
        {
            get { return _paysystemname; }
            set { _paysystemname = value; }
        }
        /// <summary>
        /// 支付方式昵称
        /// </summary>
        public string PayFriendName
        {
            get { return _payfriendname; }
            set { _payfriendname = value; }
        }
    }
}
