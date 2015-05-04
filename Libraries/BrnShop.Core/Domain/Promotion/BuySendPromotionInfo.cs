using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 买送促销活动信息类
    /// </summary>
    public class BuySendPromotionInfo : IComparable<BuySendPromotionInfo>
    {
        private int _pmid;//活动id
        private DateTime _starttime;//开始时间
        private DateTime _endtime;//结束时间
        private int _userranklower;//用户等级下限
        private int _state;//状态
        private string _name;//名称
        private int _type;//类型(0代表全场参加，1代表部分商品参加，2代表部分商品不参加)
        private int _buycount;//购买数量
        private int _sendcount;//赠送数量

        /// <summary>
        /// 活动id
        /// </summary>
        public int PmId
        {
            get { return _pmid; }
            set { _pmid = value; }
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
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 类型(0代表全场参加，1代表部分商品参加，2代表部分商品不参加)
        /// </summary>
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int BuyCount
        {
            get { return _buycount; }
            set { _buycount = value; }
        }
        /// <summary>
        /// 赠送数量
        /// </summary>
        public int SendCount
        {
            get { return _sendcount; }
            set { _sendcount = value; }
        }

        public int CompareTo(BuySendPromotionInfo other)
        {
            if (this.BuyCount > other.BuyCount)
                return 1;
            if (this.BuyCount < other.BuyCount)
                return -1;
            return 0;
        }
    }
}
