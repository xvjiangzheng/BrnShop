using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 更新在线用户要使用的信息类
    /// </summary>
    [Serializable]
    public class UpdateOnlineUserState
    {
        private int _uid;//用户id
        private string _sid;//sessionId
        private string _nickname;//用户昵称
        private string _ip;//ip地址
        private int _regionid;//区域id
        private DateTime _updatetime;//更新时间

        public UpdateOnlineUserState(int uid, string sid, string nickName, string ip, int regionId, DateTime updateTime)
        {
            _uid = uid;
            _sid = sid;
            _nickname = nickName;
            _ip = ip;
            _regionid = regionId;
            _updatetime = updateTime;
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
        /// sessionId
        /// </summary>
        public string Sid
        {
            get { return _sid; }
            set { _sid = value; }
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
        /// ip地址
        /// </summary>
        public string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }
        /// <summary>
        /// 区域id
        /// </summary>
        public int RegionId
        {
            get { return _regionid; }
            set { _regionid = value; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            get { return _updatetime; }
            set { _updatetime = value; }
        }
    }
}
