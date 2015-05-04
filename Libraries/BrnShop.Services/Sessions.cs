using System;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 会话状态操作管理类
    /// </summary>
    public partial class Sessions
    {
        private static ISessionStrategy _isessionstrategy = null;//会话状态策略

        static Sessions()
        {
            _isessionstrategy = BSPSession.Instance;
        }

        /// <summary>
        /// 生成sessionid
        /// </summary>
        /// <returns></returns>
        public static string GenerateSid()
        {
            long i = 1;
            byte[] byteArray = Guid.NewGuid().ToByteArray();
            foreach (byte b in byteArray)
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        /// <summary>
        /// 过期时间(单位为秒)
        /// </summary>
        /// <value></value>
        public static int Timeout
        {
            get { return _isessionstrategy.Timeout; }
        }

        /// <summary>
        /// 获得用户会话状态数据
        /// </summary>
        /// <param name="sid">sid</param>
        /// <returns>Dictionary<string,object>类型</returns>
        public static Dictionary<string, object> GetSession(string sid)
        {
            return _isessionstrategy.GetSession(sid);
        }

        /// <summary>
        /// 移除用户会话状态数据
        /// </summary>
        /// <param name="sid">sid</param>
        public static void RemoverSession(string sid)
        {
            _isessionstrategy.RemoverSession(sid);
        }

        /// <summary>
        /// 获得用户会话状态数据的数据项的值
        /// </summary>
        /// <param name="sid">sid</param>
        /// <param name="key">键</param>
        /// <returns>当前键值不存在时返回null</returns>
        public static object GetValue(string sid, string key)
        {
            if (!string.IsNullOrEmpty(key))
                return _isessionstrategy.GetValue(sid, key);
            else
                return null;
        }

        /// <summary>
        /// 获得用户会话状态数据的数据项的值
        /// </summary>
        /// <param name="sid">sid</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static int GetValueInt(string sid, string key)
        {
            return TypeHelper.ObjectToInt(GetValue(sid, key));
        }

        /// <summary>
        /// 获得用户会话状态数据的数据项的值
        /// </summary>
        /// <param name="sid">sid</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetValueString(string sid, string key)
        {
            object value = GetValue(sid, key);
            if (value != null)
                return value.ToString();
            else
                return string.Empty;
        }

        /// <summary>
        /// 设置用户会话状态数据的数据项
        /// </summary>
        /// <param name="sid">sid</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetItem(string sid, string key, object value)
        {
            if (!string.IsNullOrEmpty(key))
                _isessionstrategy.SetItem(sid, key, value);
        }

        /// <summary>
        /// 移除用户会话状态数据的数据项
        /// </summary>
        /// <param name="sid">sid</param>
        /// <param name="key">键</param>
        public static void RemoveItem(string sid, string key)
        {
            if (!string.IsNullOrEmpty(key))
                _isessionstrategy.RemoveItem(sid, key);
        }
    }


    /// <summary>
    /// session键类
    /// </summary>
    public partial class SessionKey
    {
        /// <summary>
        /// 图片验证码
        /// </summary>
        public const string VERIFYIMAGEVALUE = "verifyImageValue";
        /// <summary>
        /// 安全中心手机验证值
        /// </summary>
        public const string SAFEVERIFYMOBILEVALUE = "safeVerifyMoibleValue";
        /// <summary>
        /// 安全中心更新手机值
        /// </summary>
        public const string UPDATEMOBILEVALUE = "updateMoibleValue";
        /// <summary>
        /// 安全中心更新手机
        /// </summary>
        public const string UPDATEMOBILE = "updateMoible";
        /// <summary>
        /// 安全中心验证密码动作
        /// </summary>
        public const string VALIDATEPASSWORD = "validatePassword";
        /// <summary>
        /// 安全中心验证手机动作
        /// </summary>
        public const string VALIDATEMOBILE = "validateMobile";
        /// <summary>
        /// 安全中心验证邮箱动作
        /// </summary>
        public const string VALIDATEEMAIL = "validateEmail";
    }
}
