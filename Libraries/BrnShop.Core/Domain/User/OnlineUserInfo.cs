using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 在线用户信息类
    /// </summary>
    public class OnlineUserInfo
    {
        private int _olid;//在线用户编号
        private int _uid;//在线用户id
        private string _sid;//在线用户sid
        private string _nickname;//用户昵称
        private string _ip;//在线用户ip
        private int _regionid = -1;//区域id
        private DateTime _updatetime;//更新时间

        /// <summary>
        /// 在线用户编号
        /// </summary>
        public int OlId
        {
            set { _olid = value; }
            get { return _olid; }
        }
        /// <summary>
        /// 在线用户id
        /// </summary>
        public int Uid
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 在线用户sid
        /// </summary>
        public string Sid
        {
            set { _sid = value.TrimEnd(); }
            get { return _sid; }
        }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName
        {
            set { _nickname = value.TrimEnd(); }
            get { return _nickname; }
        }
        /// <summary>
        /// 在线用户ip
        /// </summary>
        public string IP
        {
            set { _ip = value.TrimEnd(); }
            get { return _ip; }
        }
        /// <summary>
        /// 区域id
        /// </summary>
        public int RegionId
        {
            set { _regionid = value; }
            get { return _regionid; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
    }
}
