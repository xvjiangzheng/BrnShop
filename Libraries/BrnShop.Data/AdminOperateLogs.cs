using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 管理员操作日志数据访问类
    /// </summary>
    public partial class AdminOperateLogs
    {
        /// <summary>
        /// 创建管理员操作日志
        /// </summary>
        public static void CreateAdminOperateLog(AdminOperateLogInfo adminOperateLogInfo)
        {
            BrnShop.Core.BSPData.RDBS.CreateAdminOperateLog(adminOperateLogInfo);
        }

        /// <summary>
        /// 获得管理员操作日志列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static List<AdminOperateLogInfo> GetAdminOperateLogList(int pageSize, int pageNumber, string condition)
        {
            List<AdminOperateLogInfo> adminOperateLogList = new List<AdminOperateLogInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetAdminOperateLogList(pageSize, pageNumber, condition);
            while (reader.Read())
            {
                AdminOperateLogInfo adminOperateLogInfo = new AdminOperateLogInfo();
                adminOperateLogInfo.LogId = TypeHelper.ObjectToInt(reader["logid"]);
                adminOperateLogInfo.Uid = TypeHelper.ObjectToInt(reader["uid"]);
                adminOperateLogInfo.NickName = reader["nickname"].ToString();
                adminOperateLogInfo.AdminGid = TypeHelper.ObjectToInt(reader["admingid"]);
                adminOperateLogInfo.AdminGTitle = reader["admingtitle"].ToString();
                adminOperateLogInfo.Operation = reader["operation"].ToString();
                adminOperateLogInfo.Description = reader["description"].ToString();
                adminOperateLogInfo.IP = reader["ip"].ToString();
                adminOperateLogInfo.OperateTime = TypeHelper.ObjectToDateTime(reader["operatetime"]);
                adminOperateLogList.Add(adminOperateLogInfo);
            }
            reader.Close();
            return adminOperateLogList;
        }

        /// <summary>
        /// 获得管理员操作日志列表条件
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="operation">操作行为</param>
        /// <param name="startTime">操作开始时间</param>
        /// <param name="endTime">操作结束时间</param>
        /// <returns></returns>
        public static string GetAdminOperateLogListCondition(int uid, string operation, string startTime, string endTime)
        {
            return BrnShop.Core.BSPData.RDBS.GetAdminOperateLogListCondition(uid, operation, startTime, endTime);
        }

        /// <summary>
        /// 获得管理员操作日志数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int GetAdminOperateLogCount(string condition)
        {
            return BrnShop.Core.BSPData.RDBS.GetAdminOperateLogCount(condition);
        }

        /// <summary>
        /// 删除管理员操作日志
        /// </summary>
        /// <param name="logIdList">日志id</param>
        public static void DeleteAdminOperateLogById(string logIdList)
        {
            BrnShop.Core.BSPData.RDBS.DeleteAdminOperateLogById(logIdList);
        }

    }
}
