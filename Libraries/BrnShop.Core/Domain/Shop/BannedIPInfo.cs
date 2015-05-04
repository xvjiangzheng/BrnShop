using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 禁止IP信息类
    /// </summary>
    public class BannedIPInfo
    {
        private int _id;//id
        private string _ip;//ip地址
        private DateTime _liftbantime;//解禁时间

        /// <summary>
        /// id
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// ip地址
        /// </summary>
        public string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }
        /// <summary>
        /// 解禁时间
        /// </summary>
        public DateTime LiftBanTime
        {
            get { return _liftbantime; }
            set { _liftbantime = value; }
        }
    }
}
