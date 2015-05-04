using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 积分日志信息类
    /// </summary>
    public class CreditLogInfo
    {
        private int _logid;//日志id
        private int _uid;//用户id
        private int _paycredits;//支付积分
        private int _rankcredits;//等级积分
        private int _action;//动作类型
        private int _actioncode;//动作代码
        private DateTime _actiontime;//动作时间
        private string _actiondes;//动作描述
        private int _operator;//操作人

        /// <summary>
        /// 日志id
        /// </summary>
        public int LogId
        {
            get { return _logid; }
            set { _logid = value; }
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
        /// 支付积分
        /// </summary>
        public int PayCredits
        {
            get { return _paycredits; }
            set { _paycredits = value; }
        }
        /// <summary>
        /// 等级积分
        /// </summary>
        public int RankCredits
        {
            get { return _rankcredits; }
            set { _rankcredits = value; }
        }
        /// <summary>
        /// 动作类型
        /// </summary>
        public int Action
        {
            get { return _action; }
            set { _action = value; }
        }
        /// <summary>
        /// 动作代码
        /// </summary>
        public int ActionCode
        {
            get { return _actioncode; }
            set { _actioncode = value; }
        }
        /// <summary>
        /// 动作时间
        /// </summary>
        public DateTime ActionTime
        {
            get { return _actiontime; }
            set { _actiontime = value; }
        }
        /// <summary>
        /// 动作描述
        /// </summary>
        public string ActionDes
        {
            get { return _actiondes; }
            set { _actiondes = value; }
        }
        /// <summary>
        /// 操作人
        /// </summary>
        public int Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }
    }
}
