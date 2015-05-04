using System;
using System.Collections.Generic;

namespace BrnShop.Core
{
    /// <summary>
    /// Redis非关系型数据库配置信息类
    /// </summary>
    [Serializable]
    public class RedisNOSQLConfigInfo : IConfigInfo
    {
        private RedisConfigInfo _user = null;//用户redis配置信息
        private RedisConfigInfo _product = null;//商品redis配置信息
        private RedisConfigInfo _promotion = null;//促销活动redis配置信息
        private RedisConfigInfo _order = null;//订单redis配置信息

        /// <summary>
        /// 用户redis配置信息
        /// </summary>
        public RedisConfigInfo User
        {
            get { return _user; }
            set { _user = value; }
        }

        /// <summary>
        /// 商品redis配置信息
        /// </summary>
        public RedisConfigInfo Product
        {
            get { return _product; }
            set { _product = value; }
        }

        /// <summary>
        /// 促销活动redis配置信息
        /// </summary>
        public RedisConfigInfo Promotion
        {
            get { return _promotion; }
            set { _promotion = value; }
        }

        /// <summary>
        /// 订单redis配置信息
        /// </summary>
        public RedisConfigInfo Order
        {
            get { return _order; }
            set { _order = value; }
        }

    }

    /// <summary>
    /// Redis配置信息类
    /// </summary>
    public class RedisConfigInfo
    {
        private int _enabled;//是否启用
        private List<string> _readwritehostlist;//读写主机列表
        private List<string> _readonlyhostlist;//只读主机列表
        private int _maxreadpoolsize;//最大读池数
        private int _maxwritepoolsize;//最大写池数
        private int _initaldb;//数据库初始化大小

        /// <summary>
        /// 是否启用
        /// </summary>
        public int Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
        /// <summary>
        /// 读写主机列表
        /// </summary>
        public List<string> ReadWriteHostList
        {
            get { return _readwritehostlist; }
            set { _readwritehostlist = value; }
        }
        /// <summary>
        /// 只读主机列表
        /// </summary>
        public List<string> ReadOnlyHostList
        {
            get { return _readonlyhostlist; }
            set { _readonlyhostlist = value; }
        }
        /// <summary>
        /// 最大读池数
        /// </summary>
        public int MaxReadPoolSize
        {
            get { return _maxreadpoolsize; }
            set { _maxreadpoolsize = value > 0 ? value : 5; }
        }
        /// <summary>
        /// 最大写池数
        /// </summary>
        public int MaxWritePoolSize
        {
            get { return _maxwritepoolsize; }
            set { _maxwritepoolsize = value > 0 ? value : 5; }
        }
        /// <summary>
        /// 数据库初始化大小
        /// </summary>
        public int InitalDB
        {
            get { return _initaldb; }
            set { _initaldb = value > 0 ? value : 0; }
        }
    }
}
