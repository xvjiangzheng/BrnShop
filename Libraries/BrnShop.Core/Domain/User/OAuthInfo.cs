using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 开放授权信息类
    /// </summary>
    public class OAuthInfo
    {
        private int _id;//自增id
        private int _uid;//用户id
        private string _openid;//开放id
        private string _server;//服务商

        /// <summary>
        /// 自增id
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户id
        /// </summary>
        public int Uid
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 开放id
        /// </summary>
        public string OpenId
        {
            set { _openid = value.TrimEnd(); }
            get { return _openid; }
        }
        /// <summary>
        /// 服务商
        /// </summary>
        public string Server
        {
            set { _server = value.TrimEnd(); }
            get { return _server; }
        }
    }
}
