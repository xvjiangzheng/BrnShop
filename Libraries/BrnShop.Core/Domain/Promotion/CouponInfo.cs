using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 优惠劵信息类
    /// </summary>
    public class CouponInfo
    {
        private int _couponid;//优惠劵id
        private string _couponsn;//优惠劵编号
        private int _uid;//用户id
        private int _coupontypeid;//优惠劵类型id
        private int _oid;//订单id
        private DateTime _usetime;//使用时间
        private string _useip;//使用ip
        private int _money;//金额
        private DateTime _activatetime;//激活时间
        private string _activateip;//激活ip
        private int _createuid;//创建用户id
        private int _createoid;//创建订单id
        private DateTime _createtime;//创建时间
        private string _createip;//创建ip

        /// <summary>
        /// 优惠劵id
        /// </summary>
        public int CouponId
        {
            get { return _couponid; }
            set { _couponid = value; }
        }
        /// <summary>
        /// 优惠劵编号
        /// </summary>
        public string CouponSN
        {
            get { return _couponsn; }
            set { _couponsn = value.TrimEnd(); }
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
        /// 优惠劵类型id
        /// </summary>
        public int CouponTypeId
        {
            get { return _coupontypeid; }
            set { _coupontypeid = value; }
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
        /// 使用时间
        /// </summary>
        public DateTime UseTime
        {
            get { return _usetime; }
            set { _usetime = value; }
        }
        /// <summary>
        /// 使用ip
        /// </summary>
        public string UseIP
        {
            get { return _useip; }
            set { _useip = value.TrimEnd(); }
        }
        /// <summary>
        /// 金额
        /// </summary>		
        public int Money
        {
            get { return _money; }
            set { _money = value; }
        }
        /// <summary>
        /// 激活时间
        /// </summary>
        public DateTime ActivateTime
        {
            get { return _activatetime; }
            set { _activatetime = value; }
        }
        /// <summary>
        /// 激活ip
        /// </summary>
        public string ActivateIP
        {
            get { return _activateip; }
            set { _activateip = value.TrimEnd(); }
        }
        /// <summary>
        /// 创建用户id
        /// </summary>
        public int CreateUid
        {
            get { return _createuid; }
            set { _createuid = value; }
        }
        /// <summary>
        /// 创建订单id
        /// </summary>
        public int CreateOid
        {
            get { return _createoid; }
            set { _createoid = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return _createtime; }
            set { _createtime = value; }
        }
        /// <summary>
        /// 创建ip
        /// </summary>
        public string CreateIP
        {
            get { return _createip; }
            set { _createip = value.TrimEnd(); }
        }
    }
}
