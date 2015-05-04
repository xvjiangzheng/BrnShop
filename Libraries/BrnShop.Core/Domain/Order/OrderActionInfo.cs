using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 订单处理信息类
    /// </summary>
    public class OrderActionInfo
    {
        private int _aid;//处理id
        private int _oid;//订单id
        private int _uid;//用户id
        private string _realname;//真实名称
        private int _admingid;//管理员组id
        private string _admingtitle;//管理员组标题
        private int _actiontype;//处理类型
        private DateTime _actiontime;//处理时间
        private string _actiondes = "";//处理描述

        /// <summary>
        /// 处理id
        /// </summary>
        public int Aid
        {
            get { return _aid; }
            set { _aid = value; }
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
        /// 用户id
        /// </summary>
        public int Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }
        /// <summary>
        /// 真实名称
        /// </summary>
        public string RealName
        {
            get { return _realname; }
            set { _realname = value; }
        }
        /// <summary>
        /// 管理员组id
        /// </summary>
        public int AdminGid
        {
            get { return _admingid; }
            set { _admingid = value; }
        }
        /// <summary>
        /// 管理员组标题
        /// </summary>
        public string AdminGTitle
        {
            get { return _admingtitle; }
            set { _admingtitle = value; }
        }
        /// <summary>
        /// 处理类型
        /// </summary>
        public int ActionType
        {
            get { return _actiontype; }
            set { _actiontype = value; }
        }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime ActionTime
        {
            get { return _actiontime; }
            set { _actiontime = value; }
        }
        /// <summary>
        /// 处理描述
        /// </summary>
        public string ActionDes
        {
            get { return _actiondes; }
            set { _actiondes = value; }
        }
    }
}
