using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BrnShop.Core
{
    /// <summary>
    /// 基于正则的缓存管理
    /// </summary>
    public partial class CacheByRegex : ICacheManager
    {
        private Hashtable _cachekeys = new Hashtable();//缓存键列表

        /// <summary>
        /// 保存缓存键到_cachekeys中
        /// </summary>
        /// <param name="key">缓存键</param>
        private void SaveKeyToCacheKeys(string key)
        {
            if (!_cachekeys.ContainsKey(key))
            {
                _cachekeys.Add(key, DateTime.Now.ToString());
            }
        }

        /// <summary>
        /// 生成获得时的缓存键
        /// </summary>
        /// <param name="key">原缓存键</param>
        /// <returns>新缓存键</returns>
        public string GenerateGetKey(string key)
        {
            return key;
        }

        /// <summary>
        /// 生成添加时的缓存键
        /// </summary>
        /// <param name="key">原缓存键</param>
        /// <returns>新缓存键</returns>
        public string GenerateInsertKey(string key)
        {
            SaveKeyToCacheKeys(key);
            return key;
        }

        /// <summary>
        /// 生成要移除的缓存键列表
        /// </summary>
        /// <returns></returns>
        public List<string> GenerateRemoveKey(string key)
        {
            List<string> matchedKeyList = new List<string>();
            Regex regex = new Regex(key, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            foreach (string k in _cachekeys.Keys)
            {
                if (regex.IsMatch(k))
                    matchedKeyList.Add(k);
            }
            return matchedKeyList;
        }
    }

}
