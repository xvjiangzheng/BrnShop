using System;
using System.Collections.Generic;

namespace BrnShop.Core
{
    /// <summary>
    /// RabbitMQ订单配置信息类
    /// </summary>
    [Serializable]
    public class RabbitMQOrderConfigInfo : IConfigInfo
    {
        private string _server = "";//服务器
        private string _virtualhost = "";//虚拟主机
        private string _username = "";//用户名
        private string _password = "";//密码
        private string _exchange = "";//交换器
        private string _exchangetype = "";//交换器类型
        private string _queue = "";//队列
        private string _routingkey = "";//路由键
        private RabbitMQOrderRedisConfigInfo _rabbitmqorderredisconfiginfo;//redis配置

        /// <summary>
        /// 服务器
        /// </summary>
        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }
        /// <summary>
        /// 虚拟主机
        /// </summary>
        public string VirtualHost
        {
            get { return _virtualhost; }
            set { _virtualhost = value; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        /// <summary>
        /// 交换器
        /// </summary>
        public string Exchange
        {
            get { return _exchange; }
            set { _exchange = value; }
        }
        /// <summary>
        /// 交换器类型
        /// </summary>
        public string ExchangeType
        {
            get { return _exchangetype; }
            set { _exchangetype = value; }
        }
        /// <summary>
        /// 队列
        /// </summary>
        public string Queue
        {
            get { return _queue; }
            set { _queue = value; }
        }
        /// <summary>
        /// 路由键
        /// </summary>
        public string RoutingKey
        {
            get { return _routingkey; }
            set { _routingkey = value; }
        }
        /// <summary>
        /// redis配置
        /// </summary>
        public RabbitMQOrderRedisConfigInfo RabbitMQOrderRedisConfigInfo
        {
            get { return _rabbitmqorderredisconfiginfo; }
            set { _rabbitmqorderredisconfiginfo = value; }
        }
    }

    /// <summary>
    /// RabbitMQ订单Redis配置信息类
    /// </summary>
    public class RabbitMQOrderRedisConfigInfo
    {
        private string _readwritehost;//读写主机
        private int _maxreadpoolsize;//最大读池数
        private int _maxwritepoolsize;//最大写池数
        private int _initaldb;//数据库初始化大小

        /// <summary>
        /// 读写主机
        /// </summary>
        public string ReadWriteHost
        {
            get { return _readwritehost; }
            set { _readwritehost = value; }
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

    /// <summary>
    /// RabbitMQ订单消息类
    /// </summary>
    [Serializable]
    public class RabbitMQOrderMessage
    {
        private OrderInfo _orderinfo;//订单信息
        private List<OrderProductInfo> _orderproductlist;//订单商品列表

        /// <summary>
        /// 订单信息
        /// </summary>
        public OrderInfo OrderInfo
        {
            get { return _orderinfo; }
            set { _orderinfo = value; }
        }
        /// <summary>
        /// 订单商品列表
        /// </summary>
        public List<OrderProductInfo> OrderProductList
        {
            get { return _orderproductlist; }
            set { _orderproductlist = value == null ? new List<OrderProductInfo>() : value; }
        }
    }
}
