using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 管理员操作日志信息类
    /// </summary>
    public class AdminOperateLogInfo
    {
        private int _logid;//日志id
        private int _uid;//用户id
        private string _nickname;//用户昵称
        private int _admingid;//管理员组id
        private string _admingtitle;//管理员组标题
        private string _operation;//操作动作
        private string _description;//操作描述
        private string _ip;//ip
        private DateTime operatetime;//操作时间

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
        /// 用户昵称
        /// </summary>
        public string NickName
        {
            get { return _nickname; }
            set { _nickname = value; }
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
        /// 操作动作
        /// </summary>
        public string Operation
        {
            get { return _operation; }
            set { _operation = value; }
        }
        /// <summary>
        /// 操作描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        /// <summary>
        /// ip
        /// </summary>
        public string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateTime
        {
            get { return operatetime; }
            set { operatetime = value; }
        }
    }
}
