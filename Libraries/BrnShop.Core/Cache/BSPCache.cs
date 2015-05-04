using System;
using System.IO;

namespace BrnShop.Core
{
    /// <summary>
    /// BrnShop缓存管理类
    /// </summary>
    public static partial class BSPCache
    {
        private static object _locker = new object();//锁对象
        private static ICacheStrategy _icachestrategy = null;//缓存策略
        private static ICacheManager _icachemanager = null;//缓存管理

        static BSPCache()
        {
            try
            {
                string[] fileNameList = Directory.GetFiles(System.Web.HttpRuntime.BinDirectory, "BrnShop.CacheStrategy.*.dll", SearchOption.TopDirectoryOnly);
                _icachestrategy = (ICacheStrategy)Activator.CreateInstance(Type.GetType(string.Format("BrnShop.CacheStrategy.{0}.CacheStrategy, BrnShop.CacheStrategy.{0}", fileNameList[0].Substring(fileNameList[0].IndexOf("CacheStrategy.") + 14).Replace(".dll", "")),
                                                                                       false,
                                                                                       true));
            }
            catch
            {
                throw new BSPException("创建'缓存策略对象'失败,可能存在的原因:未将'缓存策略程序集'添加到bin目录中;'缓存策略程序集'文件名不符合'BrnShop.CacheStrategy.{策略名称}.dll'格式");
            }
            _icachemanager = new CacheByRegex();
        }

        /// <summary>
        /// 缓存过期时间
        /// </summary>
        public static int TimeOut
        {
            get
            {
                return _icachestrategy.TimeOut;
            }
            set
            {
                lock (_locker)
                {
                    _icachestrategy.TimeOut = value;
                }
            }
        }

        /// <summary>
        /// 获得指定键的缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>缓存值</returns>
        public static object Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return null;
            return _icachestrategy.Get(_icachemanager.GenerateGetKey(key));
        }

        /// <summary>
        /// 将指定键的对象添加到缓存中
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        public static void Insert(string key, object data)
        {
            if (string.IsNullOrWhiteSpace(key) || data == null)
                return;
            lock (_locker)
            {
                _icachestrategy.Insert(_icachemanager.GenerateInsertKey(key), data);
            }
        }

        /// <summary>
        /// 将指定键的对象添加到缓存中,并指定过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间</param>
        public static void Insert(string key, object data, int cacheTime)
        {
            if (string.IsNullOrWhiteSpace(key) || data == null)
                return;
            lock (_locker)
            {
                _icachestrategy.Insert(_icachemanager.GenerateInsertKey(key), data, cacheTime);
            }
        }

        /// <summary>
        /// 从缓存中移除指定键的缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        public static void Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return;
            lock (_locker)
            {
                foreach (string k in _icachemanager.GenerateRemoveKey(key))
                    _icachestrategy.Remove(k);
            }
        }

        /// <summary>
        /// 清空缓存所有对象
        /// </summary>
        public static void Clear()
        {
            lock (_locker)
            {
                _icachestrategy.Clear();
            }
        }

    }
}
