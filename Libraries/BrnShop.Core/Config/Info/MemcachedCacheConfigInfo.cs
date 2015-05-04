using System;
using System.Collections.Generic;

namespace BrnShop.Core
{
    /// <summary>
    /// Memcached缓存配置信息类
    /// </summary>
    [Serializable]
    public class MemcachedCacheConfigInfo : IConfigInfo
    {
        private List<string> _serverlist;//服务器列表
        private int _minpoolsize;//连接池最小连接数
        private int _maxpoolsize;//连接池最大连接数
        private int _connectiontimeout;//连接超时时间(单位为秒)
        private int _queuetimeout;//查询超时时间(单位为秒)
        private int _receivetimeout;//接收超时时间(单位为秒)
        private int _deadtimeout;//宕机服务器检查时间(单位为秒)
        private int _cachetimeout;//缓存超时时间(单位为秒)

        /// <summary>
        /// 服务器列表
        /// </summary>
        public List<string> ServerList
        {
            get { return _serverlist; }
            set { _serverlist = value; }
        }
        /// <summary>
        /// 连接池最小连接数
        /// </summary>
        public int MinPoolSize
        {
            get { return _minpoolsize; }
            set { _minpoolsize = value > 0 ? value : 10; }
        }
        /// <summary>
        /// 连接池最大连接数
        /// </summary>
        public int MaxPoolSize
        {
            get { return _maxpoolsize; }
            set { _maxpoolsize = value > 0 ? value : 20; }
        }
        /// <summary>
        /// 连接超时时间(单位为秒)
        /// </summary>
        public int ConnectionTimeOut
        {
            get { return _connectiontimeout; }
            set { _connectiontimeout = value > 0 ? value : 10; }
        }
        /// <summary>
        /// 查询超时时间(单位为秒)
        /// </summary>
        public int QueueTimeOut
        {
            get { return _queuetimeout; }
            set { _queuetimeout = value > 0 ? value : 10; }
        }
        /// <summary>
        /// 接收超时时间(单位为秒)
        /// </summary>
        public int ReceiveTimeOut
        {
            get { return _receivetimeout; }
            set { _receivetimeout = value > 0 ? value : 10; }
        }
        /// <summary>
        /// 宕机服务器检查时间(单位为秒)
        /// </summary>
        public int DeadTimeOut
        {
            get { return _deadtimeout; }
            set { _deadtimeout = value > 0 ? value : 100; }
        }
        /// <summary>
        /// 缓存超时时间(单位为秒)
        /// </summary>
        public int CacheTimeOut
        {
            get { return _cachetimeout; }
            set { _cachetimeout = value > 0 ? value : 3600; }
        }
    }
}
