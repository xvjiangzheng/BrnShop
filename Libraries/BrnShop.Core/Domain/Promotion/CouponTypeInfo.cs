using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 优惠劵类型信息类
    /// </summary>
    public class CouponTypeInfo
    {
        private int _coupontypeid;//类型id
        private int _state;//状态
        private string _name;//优惠劵类型名称
        private int _money;//金额
        private int _count;//数量
        private int _sendmode;//发放方式(0代表免费领取,1代表手动发放,2代表随活动发放)
        private int _getmode;//领取方式(当且仅当发放方式为免费领取时有效.0代表无限制,1代表限领一张,2代表每天限领一张)
        private int _usemode;//使用方式(0代表可以叠加,1代表不可以叠加)
        private int _userranklower;//最低用户级别
        private int _orderamountlower;//订单总计下限
        private int _limitcateid;//限制分类id
        private int _limitbrandid;//限制品牌id
        private int _limitproduct;//限制商品
        private DateTime _sendstarttime;//发放开始时间
        private DateTime _sendendtime;//发放结束时间
        private int _useexpiretime;//使用过期时间
        private DateTime _usestarttime;//使用开始时间
        private DateTime _useendtime;//使用结束时间

        /// <summary>
        /// 类型id
        /// </summary>		
        public int CouponTypeId
        {
            get { return _coupontypeid; }
            set { _coupontypeid = value; }
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
        /// 优惠劵类型名称
        /// </summary>		
        public string Name
        {
            get { return _name; }
            set { _name = value; }
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
        /// 数量
        /// </summary>
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }
        /// <summary>
        /// 发放方式(0代表免费领取,1代表手动发放,2代表随活动发放)
        /// </summary>
        public int SendMode
        {
            get { return _sendmode; }
            set { _sendmode = value; }
        }
        /// <summary>
        /// 领取方式(当且仅当发放方式为免费领取时有效.0代表无限制,1代表限领一张,2代表每天限领一张)
        /// </summary>
        public int GetMode
        {
            get { return _getmode; }
            set { _getmode = value; }
        }
        /// <summary>
        /// 使用方式(0代表可以叠加,1代表不可以叠加)
        /// </summary>
        public int UseMode
        {
            get { return _usemode; }
            set { _usemode = value; }
        }
        /// <summary>
        /// 最低用户级别
        /// </summary>
        public int UserRankLower
        {
            get { return _userranklower; }
            set { _userranklower = value; }
        }
        /// <summary>
        /// 订单总计下限
        /// </summary>		
        public int OrderAmountLower
        {
            get { return _orderamountlower; }
            set { _orderamountlower = value; }
        }
        /// <summary>
        /// 限制分类id
        /// </summary>
        public int LimitCateId
        {
            get { return _limitcateid; }
            set { _limitcateid = value; }
        }
        /// <summary>
        /// 限制品牌id
        /// </summary>
        public int LimitBrandId
        {
            get { return _limitbrandid; }
            set { _limitbrandid = value; }
        }
        /// <summary>
        /// 限制商品
        /// </summary>
        public int LimitProduct
        {
            get { return _limitproduct; }
            set { _limitproduct = value; }
        }
        /// <summary>
        /// 发放开始时间
        /// </summary>		
        public DateTime SendStartTime
        {
            get { return _sendstarttime; }
            set { _sendstarttime = value; }
        }
        /// <summary>
        /// 发放结束时间
        /// </summary>		
        public DateTime SendEndTime
        {
            get { return _sendendtime; }
            set { _sendendtime = value; }
        }
        /// <summary>
        /// 使用过期时间
        /// </summary>
        public int UseExpireTime
        {
            get { return _useexpiretime; }
            set { _useexpiretime = value; }
        }
        /// <summary>
        /// 使用开始时间
        /// </summary>		
        public DateTime UseStartTime
        {
            get { return _usestarttime; }
            set { _usestarttime = value; }
        }
        /// <summary>
        /// 使用结束时间
        /// </summary>		
        public DateTime UseEndTime
        {
            get { return _useendtime; }
            set { _useendtime = value; }
        }
    }
}

