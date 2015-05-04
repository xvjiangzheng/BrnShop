using System;
using System.IO;

namespace BrnShop.Core
{
    /// <summary>
    /// BrnShop配置管理类
    /// </summary>
    public partial class BSPConfig
    {
        private static object _locker = new object();//锁对象

        private static IConfigStrategy _iconfigstrategy = null;//配置策略

        private static RDBSConfigInfo _rdbsconfiginfo = null;//关系数据库配置信息
        private static ShopConfigInfo _shopconfiginfo = null;//商城基本配置信息
        private static EmailConfigInfo _emailconfiginfo = null;//邮件配置信息
        private static SMSConfigInfo _smsconfiginfo = null;//短信配置信息
        private static CreditConfigInfo _creditconfiginfo = null;//积分配置信息
        private static EventConfigInfo _eventconfiginfo = null;//事件配置信息
        private static RedisNOSQLConfigInfo _redisnosqlconfiginfo = null;//redis非关系数据库配置信息
        private static MemcachedCacheConfigInfo _memcachedcacheconfiginfo = null;//Memcached缓存配置信息
        private static MemcachedSessionConfigInfo _memcachedsessionconfiginfo = null;//Memcached会话状态配置信息
        private static MemcachedCartConfigInfo _memcachedcartconfiginfo = null;//Memcached购物车配置信息
        private static RabbitMQOrderConfigInfo _rabbitmqorderconfiginfo = null;//RabbitMQ订单配置信息

        static BSPConfig()
        {
            try
            {
                string[] fileNameList = Directory.GetFiles(System.Web.HttpRuntime.BinDirectory, "BrnShop.ConfigStrategy.*.dll", SearchOption.TopDirectoryOnly);
                _iconfigstrategy = (IConfigStrategy)Activator.CreateInstance(Type.GetType(string.Format("BrnShop.ConfigStrategy.{0}.ConfigStrategy, BrnShop.ConfigStrategy.{0}", fileNameList[0].Substring(fileNameList[0].LastIndexOf("ConfigStrategy.") + 15).Replace(".dll", "")),
                                                                                         false,
                                                                                         true));
            }
            catch
            {
                throw new BSPException("创建'配置策略对象'失败,可能存在的原因:未将'配置策略程序集'添加到bin目录中;'配置策略程序集'文件名不符合'BrnShop.ConfigStrategy.{策略名称}.dll'格式");
            }
            _rdbsconfiginfo = _iconfigstrategy.GetRDBSConfig();
            _shopconfiginfo = _iconfigstrategy.GetShopConfig();
        }

        /// <summary>
        /// 关系数据库配置信息
        /// </summary>
        public static RDBSConfigInfo RDBSConfig
        {
            get { return _rdbsconfiginfo; }
        }

        /// <summary>
        /// 商城基本配置信息
        /// </summary>
        public static ShopConfigInfo ShopConfig
        {
            get { return _shopconfiginfo; }
        }

        /// <summary>
        /// 邮件配置信息
        /// </summary>
        public static EmailConfigInfo EmailConfig
        {
            get
            {
                if (_emailconfiginfo == null)
                {
                    lock (_locker)
                    {
                        if (_emailconfiginfo == null)
                        {
                            _emailconfiginfo = _iconfigstrategy.GetEmailConfig();
                        }
                    }
                }

                return _emailconfiginfo;
            }
        }

        /// <summary>
        /// 短息配置信息
        /// </summary>
        public static SMSConfigInfo SMSConfig
        {
            get
            {
                if (_smsconfiginfo == null)
                {
                    lock (_locker)
                    {
                        if (_smsconfiginfo == null)
                        {
                            _smsconfiginfo = _iconfigstrategy.GetSMSConfig();
                        }
                    }
                }
                return _smsconfiginfo;
            }
        }

        /// <summary>
        /// 积分配置信息
        /// </summary>
        public static CreditConfigInfo CreditConfig
        {
            get
            {
                if (_creditconfiginfo == null)
                {
                    lock (_locker)
                    {
                        if (_creditconfiginfo == null)
                        {
                            _creditconfiginfo = _iconfigstrategy.GetCreditConfig();
                        }
                    }
                }
                return _creditconfiginfo;
            }
        }

        /// <summary>
        /// 事件配置信息
        /// </summary>
        public static EventConfigInfo EventConfig
        {
            get
            {
                if (_eventconfiginfo == null)
                {
                    lock (_locker)
                    {
                        if (_eventconfiginfo == null)
                        {
                            _eventconfiginfo = _iconfigstrategy.GetEventConfig();
                        }
                    }
                }
                return _eventconfiginfo;
            }
        }

        /// <summary>
        /// Redis非关系型数据库配置信息
        /// </summary>
        public static RedisNOSQLConfigInfo RedisNOSQLConfig
        {
            get
            {
                if (_redisnosqlconfiginfo == null)
                {
                    lock (_locker)
                    {
                        if (_redisnosqlconfiginfo == null)
                        {
                            _redisnosqlconfiginfo = _iconfigstrategy.GetRedisNOSQLConfig();
                        }
                    }
                }
                return _redisnosqlconfiginfo;
            }
        }

        /// <summary>
        /// Memcached缓存配置信息
        /// </summary>
        public static MemcachedCacheConfigInfo MemcachedCacheConfig
        {
            get
            {
                if (_memcachedcacheconfiginfo == null)
                {
                    lock (_locker)
                    {
                        if (_memcachedcacheconfiginfo == null)
                        {
                            _memcachedcacheconfiginfo = _iconfigstrategy.GetMemcachedCacheConfig();
                        }
                    }
                }
                return _memcachedcacheconfiginfo;
            }
        }

        /// <summary>
        /// Memcached会话状态配置信息
        /// </summary>
        public static MemcachedSessionConfigInfo MemcachedSessionConfig
        {
            get
            {
                if (_memcachedsessionconfiginfo == null)
                {
                    lock (_locker)
                    {
                        if (_memcachedsessionconfiginfo == null)
                        {
                            _memcachedsessionconfiginfo = _iconfigstrategy.GetMemcachedSessionConfig();
                        }
                    }
                }
                return _memcachedsessionconfiginfo;
            }
        }

        /// <summary>
        /// Memcached购物车配置信息
        /// </summary>
        public static MemcachedCartConfigInfo MemcachedCartConfig
        {
            get
            {
                if (_memcachedcartconfiginfo == null)
                {
                    lock (_locker)
                    {
                        if (_memcachedcartconfiginfo == null)
                        {
                            _memcachedcartconfiginfo = _iconfigstrategy.GetMemcachedCartConfig();
                        }
                    }
                }
                return _memcachedcartconfiginfo;
            }
        }

        /// <summary>
        /// RabbitMQ订单配置信息
        /// </summary>
        public static RabbitMQOrderConfigInfo RabbitMQOrderConfig
        {
            get
            {
                if (_rabbitmqorderconfiginfo == null)
                {
                    lock (_locker)
                    {
                        if (_rabbitmqorderconfiginfo == null)
                        {
                            _rabbitmqorderconfiginfo = _iconfigstrategy.GetRabbitMQOrderConfig();
                        }
                    }
                }
                return _rabbitmqorderconfiginfo;
            }
        }



        /// <summary>
        /// 保存商城配置信息
        /// </summary>
        public static void SaveShopConfig(ShopConfigInfo shopConfigInfo)
        {
            lock (_locker)
            {
                if (_iconfigstrategy.SaveShopConfig(shopConfigInfo))
                    _shopconfiginfo = shopConfigInfo;
            }
        }

        /// <summary>
        /// 保存邮件配置信息
        /// </summary>
        public static void SaveEmailConfig(EmailConfigInfo emailConfigInfo)
        {
            lock (_locker)
            {
                if (_iconfigstrategy.SaveEmailConfig(emailConfigInfo))
                    _emailconfiginfo = null;
            }
        }

        /// <summary>
        /// 保存短信配置信息
        /// </summary>
        public static void SaveSMSConfig(SMSConfigInfo smsConfigInfo)
        {
            lock (_locker)
            {
                if (_iconfigstrategy.SaveSMSConfig(smsConfigInfo))
                    _smsconfiginfo = null;
            }
        }

        /// <summary>
        /// 保存积分配置信息
        /// </summary>
        public static void SaveCreditConfig(CreditConfigInfo creditConfigInfo)
        {
            lock (_locker)
            {
                if (_iconfigstrategy.SaveCreditConfig(creditConfigInfo))
                    _creditconfiginfo = null;
            }
        }

        /// <summary>
        /// 保存事件配置信息
        /// </summary>
        public static void SaveEventConfig(EventConfigInfo eventConfigInfo)
        {
            lock (_locker)
            {
                if (_iconfigstrategy.SaveEventConfig(eventConfigInfo))
                    _eventconfiginfo = null;
            }
        }
    }
}
