using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 满赠促销活动信息类
    /// </summary>
    public class FullSendPromotionInfo
    {
        private int _pmid;//活动id
        private DateTime _starttime;//开始时间
        private DateTime _endtime;//结束时间
        private int _userranklower;//用户等级下限
        private int _state;//状态
        private string _name;//名称
        private int _limitmoney;//限制金额
        private int _addmoney;//添加金额



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
        /// 限制金额
        /// </summary>
        public int LimitMoney
        {
            get { return _limitmoney; }
            set { _limitmoney = value; }
        }
        /// <summary>
        /// 添加金额
        /// </summary>
        public int AddMoney
        {
            get { return _addmoney; }
            set { _addmoney = value; }
        }


    }
}
