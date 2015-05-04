using System;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 管理员操作日志操作管理类
    /// </summary>
    public partial class AdminOperateLogs
    {
        /// <summary>
        /// 创建管理员操作日志
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="nickName">用户昵称</param>
        /// <param name="adminGid">管理员组id</param>
        /// <param name="adminGTitle">管理员组标题</param>
        /// <param name="ip">ip</param>
        /// <param name="operation">操作行为</param>
        /// <param name="description">操作描述</param>
        public static void CreateAdminOperateLog(int uid, string nickName, int adminGid, string adminGTitle, string ip, string operation, string description)
        {
            AdminOperateLogInfo adminOperateLogInfo = new AdminOperateLogInfo
            {
                Uid = uid,
                NickName = nickName,
                AdminGid = adminGid,
                AdminGTitle = adminGTitle,
                IP = ip,
                OperateTime = DateTime.Now,
                Operation = operation,
                Description = description
            };
            BrnShop.Data.AdminOperateLogs.CreateAdminOperateLog(adminOperateLogInfo);
        }

        /// <summary>
        /// 创建管理员操作日志
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="nickName">用户昵称</param>
        /// <param name="adminGid">管理员组id</param>
        /// <param name="adminGTitle">管理员组标题</param>
        /// <param name="ip">ip</param>
        /// <param name="operation">操作行为</param>
        public static void CreateAdminOperateLog(int uid, string nickName, int adminGid, string adminGTitle, string ip, string operation)
        {
            CreateAdminOperateLog(uid, nickName, adminGid, adminGTitle, ip, operation, "");
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
            return BrnShop.Data.AdminOperateLogs.GetAdminOperateLogList(pageSize, pageNumber, condition);
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
            return BrnShop.Data.AdminOperateLogs.GetAdminOperateLogListCondition(uid, operation, startTime, endTime);
        }

        /// <summary>
        /// 获得管理员操作日志数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int GetAdminOperateLogCount(string condition)
        {
            return BrnShop.Data.AdminOperateLogs.GetAdminOperateLogCount(condition);
        }

        /// <summary>
        /// 删除管理员操作日志
        /// </summary>
        /// <param name="logIdList">日志id</param>
        public static void DeleteAdminOperateLogById(int[] logIdList)
        {
            if (logIdList != null && logIdList.Length > 0)
                BrnShop.Data.AdminOperateLogs.DeleteAdminOperateLogById(CommonHelper.IntArrayToString(logIdList));
        }
    }
}
