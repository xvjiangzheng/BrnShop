using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 登陆失败日志信息类
    /// </summary>
    public class LoginFailLogInfo
    {
        private int _id;
        private long _loginip;//登陆ip
        private int _failtimes;//失败次数
        private DateTime _lastlogintime;//最后登陆时间


        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 登陆ip
        /// </summary>
        public long LoginIP
        {
            get { return _loginip; }
            set { _loginip = value; }
        }
        /// <summary>
        /// 失败次数
        /// </summary>
        public int FailTimes
        {
            get { return _failtimes; }
            set { _failtimes = value; }
        }
        /// <summary>
        /// 最后登陆时间
        /// </summary>
        public DateTime LastLoginTime
        {
            get { return _lastlogintime; }
            set { _lastlogintime = value; }
        }
    }
}
