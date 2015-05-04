using System;
using System.Web;
using System.Web.Caching;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.SessionStrategy.AspNetCache
{
    /// <summary>
    /// 基于Asp.Net缓存的会话状态策略
    /// </summary>
    public partial class SessionStrategy : ISessionStrategy
    {
        private Cache _cache;//Asp.Net缓存
        private int _timeout = 600;//过期时间(单位为秒)

        public SessionStrategy()
        {
            _cache = HttpRuntime.Cache;
        }

        /// <summary>
        /// 过期时间(单位为秒)
        /// </summary>
        /// <value></value>
        public int Timeout
        {
            get { return _timeout; }
        }

        /// <summary>
        /// 获得用户会话状态数据
        /// </summary>
        /// <param name="sid">sid</param>
        /// <returns>Dictionary<string,object>类型</returns>
        public Dictionary<string, object> GetSession(string sid)
        {
            object session = _cache.Get(sid);
            if (session != null)
                return (Dictionary<string, object>)session;

            Dictionary<string, object> s = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            _cache.Insert(sid, s, null, DateTime.Now.AddSeconds(_timeout), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            return s;
        }

        /// <summary>
        /// 移除用户会话状态数据
        /// </summary>
        /// <param name="sid">sid</param>
        public void RemoverSession(string sid)
        {
            _cache.Remove(sid);
        }

        /// <summary>
        /// 获得用户会话状态数据的数据项的值
        /// </summary>
        /// <param name="sid">sid</param>
        /// <param name="key">键</param>
        /// <returns>当前键值不存在时返回null</returns>
        public object GetValue(string sid, string key)
        {
            object session = _cache.Get(sid);
            if (session == null)
            {
                return null;
            }
            else
            {
                Dictionary<string, object> s = (Dictionary<string, object>)session;
                object value = null;
                s.TryGetValue(key, out value);
                return value;
            }
        }

        /// <summary>
        /// 设置用户会话状态数据的数据项
        /// </summary>
        /// <param name="sid">sid</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void SetItem(string sid, string key, object value)
        {
            Dictionary<string, object> session = GetSession(sid);
            if (session.ContainsKey(key))
                session[key] = value;
            else
                session.Add(key, value);
        }

        /// <summary>
        /// 移除用户会话状态数据的数据项
        /// </summary>
        /// <param name="sid">sid</param>
        /// <param name="key">键</param>
        public void RemoveItem(string sid, string key)
        {
            object session = _cache.Get(sid);
            if (session != null)
            {
                Dictionary<string, object> s = (Dictionary<string, object>)session;
                s.Remove(key);
            }
        }
    }
}
