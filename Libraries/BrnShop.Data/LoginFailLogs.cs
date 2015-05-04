using System;
using System.Data;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 登陆失败日志数据访问类
    /// </summary>
    public partial class LoginFailLogs
    {
        /// <summary>
        /// 获得登陆失败日志
        /// </summary>
        /// <param name="loginIP">登陆IP</param>
        /// <returns></returns>
        public static LoginFailLogInfo GetLoginFailLogByIP(long loginIP)
        {
            LoginFailLogInfo loginFailLogInfo = null;
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetLoginFailLogByIP(loginIP);
            if (reader.Read())
            {
                loginFailLogInfo = new LoginFailLogInfo();
                loginFailLogInfo.Id = TypeHelper.ObjectToInt(reader["id"]);
                loginFailLogInfo.LoginIP = Convert.ToInt64(reader["loginip"]);
                loginFailLogInfo.FailTimes = TypeHelper.ObjectToInt(reader["failtimes"]);
                loginFailLogInfo.LastLoginTime = TypeHelper.ObjectToDateTime(reader["lastlogintime"]);
            }
            reader.Close();
            return loginFailLogInfo;
        }

        /// <summary>
        /// 增加登陆失败次数
        /// </summary>
        /// <param name="loginIP">登陆IP</param>
        /// <param name="loginTime">登陆时间</param>
        public static void AddLoginFailTimes(long loginIP, DateTime loginTime)
        {
            BrnShop.Core.BSPData.RDBS.AddLoginFailTimes(loginIP, loginTime);
        }

        /// <summary>
        /// 删除登陆失败日志
        /// </summary>
        /// <param name="loginIP">登陆IP</param>
        public static void DeleteLoginFailLogByIP(long loginIP)
        {
            BrnShop.Core.BSPData.RDBS.DeleteLoginFailLogByIP(loginIP);
        }
    }
}
