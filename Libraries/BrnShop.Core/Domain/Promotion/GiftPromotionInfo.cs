using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 赠品促销活动信息类
    /// </summary>
    public class GiftPromotionInfo
    {
        private int _pmid;//活动id
        private int _pid;//商品id
        private DateTime _starttime1;//活动开始时间1
        private DateTime _endtime1;//活动结束时间1
        private DateTime _starttime2;//活动开始时间1
        private DateTime _endtime2;//活动结束时间2
        private DateTime _starttime3;//活动开始时间3
        private DateTime _endtime3;//活动结束时间4
        private int _userranklower;//用户等级下限
        private int _state;//状态
        private string _name;//活动名称
        private int _quotaupper;//活动上限


        /// <summary>
        /// 活动id
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
        /// 活动开始时间1
        /// </summary>
        public DateTime StartTime1
        {
            get { return _starttime1; }
            set { _starttime1 = value; }
        }
        /// <summary>
        /// 活动结束时间1
        /// </summary>
        public DateTime EndTime1
        {
            get { return _endtime1; }
            set { _endtime1 = value; }
        }
        /// <summary>
        /// 活动开始时间2
        /// </summary>
        public DateTime StartTime2
        {
            get { return _starttime2; }
            set { _starttime2 = value; }
        }
        /// <summary>
        /// 活动结束时间2
        /// </summary>
        public DateTime EndTime2
        {
            get { return _endtime2; }
            set { _endtime2 = value; }
        }
        /// <summary>
        /// 活动开始时间3
        /// </summary>
        public DateTime StartTime3
        {
            get { return _starttime3; }
            set { _starttime3 = value; }
        }
        /// <summary>
        /// 活动结束时间3
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
        /// 活动上限
        /// </summary>
        public int QuotaUpper
        {
            get { return _quotaupper; }
            set { _quotaupper = value; }
        }

    }
}
