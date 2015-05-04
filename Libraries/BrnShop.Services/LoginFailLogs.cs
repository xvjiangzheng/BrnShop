using System;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 登陆失败日志操作管理类
    /// </summary>
    public partial class LoginFailLogs
    {
        /// <summary>
        /// 获得登陆失败次数
        /// </summary>
        /// <param name="loginIP">登陆IP</param>
        /// <returns></returns>
        public static int GetLoginFailTimesByIp(string loginIP)
        {
            LoginFailLogInfo loginFailLogInfo = BrnShop.Data.LoginFailLogs.GetLoginFailLogByIP(CommonHelper.ConvertIPToLong(loginIP));
            if (loginFailLogInfo == null)
                return 0;
            if (loginFailLogInfo.LastLoginTime.AddMinutes(15) < DateTime.Now)
                return 0;

            return loginFailLogInfo.FailTimes;
        }

        /// <summary>
        /// 增加登陆失败次数
        /// </summary>
        /// <param name="loginIP">登陆IP</param>
        /// <param name="loginTime">登陆时间</param>
        public static void AddLoginFailTimes(string loginIP, DateTime loginTime)
        {
            BrnShop.Data.LoginFailLogs.AddLoginFailTimes(CommonHelper.ConvertIPToLong(loginIP), loginTime);
        }

        /// <summary>
        /// 删除登陆失败日志
        /// </summary>
        /// <param name="loginIP">登陆IP</param>
        public static void DeleteLoginFailLogByIP(string loginIP)
        {
            BrnShop.Data.LoginFailLogs.DeleteLoginFailLogByIP(CommonHelper.ConvertIPToLong(loginIP));
        }
    }
}
