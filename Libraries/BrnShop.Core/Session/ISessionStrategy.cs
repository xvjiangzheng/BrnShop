using System;
using System.Collections.Generic;

namespace BrnShop.Core
{
    /// <summary>
    /// 会话状态策略接口
    /// </summary>
    public partial interface ISessionStrategy
    {
        /// <summary>
        /// 过期时间(单位为秒)
        /// </summary>
        int Timeout { get; }

        /// <summary>
        /// 获得用户会话状态数据
        /// </summary>
        /// <param name="sid">sid</param>
        /// <returns>Dictionary<string,object>类型</returns>
        Dictionary<string,object> GetSession(string sid);

        /// <summary>
        /// 移除用户会话状态数据
        /// </summary>
        /// <param name="sid">sid</param>
        void RemoverSession(string sid);

        /// <summary>
        /// 获得用户会话状态数据的数据项的值
        /// </summary>
        /// <param name="sid">sid</param>
        /// <param name="key">键</param>
        /// <returns>当前键值不存在时返回null</returns>
        object GetValue(string sid, string key);

        /// <summary>
        /// 设置用户会话状态数据的数据项
        /// </summary>
        /// <param name="sid">sid</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        void SetItem(string sid, string key, object value);

        /// <summary>
        /// 移除用户会话状态数据的数据项
        /// </summary>
        /// <param name="sid">sid</param>
        /// <param name="key">键</param>
        void RemoveItem(string sid, string key);
    }
}
