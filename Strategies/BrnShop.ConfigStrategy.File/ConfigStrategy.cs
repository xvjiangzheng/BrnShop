using System;

using BrnShop.Core;

namespace BrnShop.ConfigStrategy.File
{
    /// <summary>
    /// 基于文件的配置策略
    /// </summary>
    public partial class ConfigStrategy : IConfigStrategy
    {
        #region 私有字段

        private readonly string _rdbsconfigfilepath = "/App_Data/rdbs.config";//关系数据库配置信息文件路径
        private readonly string _shopconfigfilepath = "/App_Data/shop.config";//商城基本配置信息文件路径
        private readonly string _emailconfigfilepath = "/App_Data/email.config";//邮件配置信息文件路径
        private readonly string _smsconfigfilepath = "/App_Data/sms.config";//短信配置信息文件路径
        private readonly string _creditconfigfilepath = "/App_Data/credit.config";//积分配置信息文件路径
        private readonly string _eventconfigfilepath = "/App_Data/event.config";//事件配置信息文件路径
        private readonly string _redisnosqlconfigfilepath = "/App_Data/redisnosql.config";//redis非关系型数据库配置信息文件路径
        private readonly string _memcachedcacheconfigfilepath = "/App_Data/memcachedcache.config";//Memcached缓存配置信息文件路径
        private readonly string _memcachedsessionconfigfilepath = "/App_Data/memcachedsession.config";//Memcached会话状态配置信息文件路径
        private readonly string _memcachedcartconfigfilepath = "/App_Data/memcachedcart.config";//Memcached购物车配置信息文件路径
        private readonly string _rabbitmqorderconfigfilepath = "/App_Data/rabbitmqorder.config";//RabbitMQ订单配置信息文件路径

        #endregion

        #region 帮助方法

        /// <summary>
        /// 从文件中加载配置信息
        /// </summary>
        /// <param name="configInfoType">配置信息类型</param>
        /// <param name="configInfoFile">配置信息文件路径</param>
        /// <returns>配置信息</returns>
        private IConfigInfo LoadConfigInfo(Type configInfoType, string configInfoFile)
        {
            return (IConfigInfo)IOHelper.DeserializeFromXML(configInfoType, configInfoFile);
        }

        /// <summary>
        /// 将配置信息保存到文件中
        /// </summary>
        /// <param name="configInfo">配置信息</param>
        /// <param name="configInfoFile">保存路径</param>
        /// <returns>是否保存成功</returns>
        private bool SaveConfigInfo(IConfigInfo configInfo, string configInfoFile)
        {
            return IOHelper.SerializeToXml(configInfo, configInfoFile);
        }

        #endregion

        /// <summary>
        /// 获得关系数据库配置
        /// </summary>
        public RDBSConfigInfo GetRDBSConfig()
        {
            return (RDBSConfigInfo)LoadConfigInfo(typeof(RDBSConfigInfo), IOHelper.GetMapPath(_rdbsconfigfilepath));
        }

        /// <summary>
        /// 保存商城基本配置
        /// </summary>
        /// <param name="configInfo">商城基本配置信息</param>
        /// <returns>是否保存结果</returns>
        public bool SaveShopConfig(ShopConfigInfo configInfo)
        {
            return SaveConfigInfo(configInfo, IOHelper.GetMapPath(_shopconfigfilepath));
        }

        /// <summary>
        /// 获得商城基本配置
        /// </summary>
        public ShopConfigInfo GetShopConfig()
        {
            return (ShopConfigInfo)LoadConfigInfo(typeof(ShopConfigInfo), IOHelper.GetMapPath(_shopconfigfilepath));
        }

        /// <summary>
        /// 保存邮件配置
        /// </summary>
        /// <param name="configInfo">邮件配置信息</param>
        /// <returns>是否保存结果</returns>
        public bool SaveEmailConfig(EmailConfigInfo configInfo)
        {
            return SaveConfigInfo(configInfo, IOHelper.GetMapPath(_emailconfigfilepath));
        }

        /// <summary>
        /// 获得邮件配置
        /// </summary>
        public EmailConfigInfo GetEmailConfig()
        {
            return (EmailConfigInfo)LoadConfigInfo(typeof(EmailConfigInfo), IOHelper.GetMapPath(_emailconfigfilepath));
        }

        /// <summary>
        /// 保存短信配置
        /// </summary>
        /// <param name="configInfo">短信配置信息</param>
        /// <returns>是否保存结果</returns>
        public bool SaveSMSConfig(SMSConfigInfo configInfo)
        {
            return SaveConfigInfo(configInfo, IOHelper.GetMapPath(_smsconfigfilepath));
        }

        /// <summary>
        /// 获得短信配置
        /// </summary>
        public SMSConfigInfo GetSMSConfig()
        {
            return (SMSConfigInfo)LoadConfigInfo(typeof(SMSConfigInfo), IOHelper.GetMapPath(_smsconfigfilepath));
        }

        /// <summary>
        /// 保存积分配置
        /// </summary>
        /// <param name="configInfo">积分配置信息</param>
        /// <returns>是否保存结果</returns>
        public bool SaveCreditConfig(CreditConfigInfo configInfo)
        {
            return SaveConfigInfo(configInfo, IOHelper.GetMapPath(_creditconfigfilepath));
        }

        /// <summary>
        /// 获得积分配置
        /// </summary>
        /// <returns></returns>
        public CreditConfigInfo GetCreditConfig()
        {
            return (CreditConfigInfo)LoadConfigInfo(typeof(CreditConfigInfo), IOHelper.GetMapPath(_creditconfigfilepath));
        }

        /// <summary>
        /// 保存事件配置
        /// </summary>
        /// <param name="configInfo">事件配置信息</param>
        /// <returns>是否保存成功</returns>
        public bool SaveEventConfig(EventConfigInfo configInfo)
        {
            return SaveConfigInfo(configInfo, IOHelper.GetMapPath(_eventconfigfilepath));
        }

        /// <summary>
        /// 获得事件配置
        /// </summary>
        /// <returns></returns>
        public EventConfigInfo GetEventConfig()
        {
            return (EventConfigInfo)LoadConfigInfo(typeof(EventConfigInfo), IOHelper.GetMapPath(_eventconfigfilepath));
        }

        /// <summary>
        /// 获得Redis非关系型数据库配置
        /// </summary>
        public RedisNOSQLConfigInfo GetRedisNOSQLConfig()
        {
            return (RedisNOSQLConfigInfo)LoadConfigInfo(typeof(RedisNOSQLConfigInfo), IOHelper.GetMapPath(_redisnosqlconfigfilepath));
        }

        /// <summary>
        /// 获得Memcached缓存配置
        /// </summary>
        public MemcachedCacheConfigInfo GetMemcachedCacheConfig()
        {
            return (MemcachedCacheConfigInfo)LoadConfigInfo(typeof(MemcachedCacheConfigInfo), IOHelper.GetMapPath(_memcachedcacheconfigfilepath));
        }

        /// <summary>
        /// 获得Memcached会话状态配置
        /// </summary>
        public MemcachedSessionConfigInfo GetMemcachedSessionConfig()
        {
            return (MemcachedSessionConfigInfo)LoadConfigInfo(typeof(MemcachedSessionConfigInfo), IOHelper.GetMapPath(_memcachedsessionconfigfilepath));
        }

        /// <summary>
        /// 获得Memcached购物车配置
        /// </summary>
        public MemcachedCartConfigInfo GetMemcachedCartConfig()
        {
            return (MemcachedCartConfigInfo)LoadConfigInfo(typeof(MemcachedCartConfigInfo), IOHelper.GetMapPath(_memcachedcartconfigfilepath));
        }

        /// <summary>
        /// 获得RabbitMQ订单配置
        /// </summary>
        public RabbitMQOrderConfigInfo GetRabbitMQOrderConfig()
        {
            return (RabbitMQOrderConfigInfo)LoadConfigInfo(typeof(RabbitMQOrderConfigInfo), IOHelper.GetMapPath(_rabbitmqorderconfigfilepath));
        }
    }
}
