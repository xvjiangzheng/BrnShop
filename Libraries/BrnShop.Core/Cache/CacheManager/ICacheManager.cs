using System;
using System.Collections.Generic;

namespace BrnShop.Core
{
    /// <summary>
    /// 缓存管理接口
    /// </summary>
    public partial interface ICacheManager
    {
        /// <summary>
        /// 生成获得时的缓存键
        /// </summary>
        /// <param name="key">原缓存键</param>
        /// <returns>新缓存键</returns>
        string GenerateGetKey(string key);

        /// <summary>
        /// 生成添加时的缓存键
        /// </summary>
        /// <param name="key">原缓存键</param>
        /// <returns>新缓存键</returns>
        string GenerateInsertKey(string key);

        /// <summary>
        /// 生成要移除的缓存键列表
        /// </summary>
        /// <returns></returns>
        List<string> GenerateRemoveKey(string key);
    }
}
