using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 满减促销活动信息类
    /// </summary>
    public class FullCutPromotionInfo
    {
        private int _pmid;//活动id
        private int _type;//活动类型,0代表全场商品满减，1代表部分商品满减，2代表部分商品不满减
        private DateTime _starttime;//开始时间
        private DateTime _endtime;//结束时间
        private int _userranklower;//用户等级下限
        private int _state;//状态
        private string _name;//名称
        private int _limitmoney1;//限制金额1
        private int _cutmoney1;//减小金额1
        private int _limitmoney2;//限制金额2
        private int _cutmoney2;//减小金额2
        private int _limitmoney3;//限制金额3
        private int _cutmoney3;//减小金额3


        /// <summary>
        /// 活动id
        /// </summary>
        public int PmId
        {
            get { return _pmid; }
            set { _pmid = value; }
        }
        /// <summary>
        /// 活动类型,0代表全场商品满减，1代表部分商品满减，2代表部分商品不满减
        /// </summary>
        public int Type
        {
            get { return _type; }
            set { _type = value; }
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
        /// 限制金额1
        /// </summary>
        public int LimitMoney1
        {
            get { return _limitmoney1; }
            set { _limitmoney1 = value; }
        }
        /// <summary>
        /// 减小金额1
        /// </summary>
        public int CutMoney1
        {
            get { return _cutmoney1; }
            set { _cutmoney1 = value; }
        }
        /// <summary>
        /// 限制金额2
        /// </summary>
        public int LimitMoney2
        {
            get { return _limitmoney2; }
            set { _limitmoney2 = value; }
        }
        /// <summary>
        /// 减小金额2
        /// </summary>
        public int CutMoney2
        {
            get { return _cutmoney2; }
            set { _cutmoney2 = value; }
        }
        /// <summary>
        /// 限制金额3
        /// </summary>
        public int LimitMoney3
        {
            get { return _limitmoney3; }
            set { _limitmoney3 = value; }
        }
        /// <summary>
        /// 减小金额3
        /// </summary>
        public int CutMoney3
        {
            get { return _cutmoney3; }
            set { _cutmoney3 = value; }
        }
    }
}
