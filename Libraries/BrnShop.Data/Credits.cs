using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 积分数据访问类
    /// </summary>
    public partial class Credits
    {
        #region 辅助方法

        /// <summary>
        /// 通过IDataReader创建CreditLogInfo信息
        /// </summary>
        public static CreditLogInfo BuildCreditLogFromReader(IDataReader reader)
        {
            CreditLogInfo creditLogInfo = new CreditLogInfo();

            creditLogInfo.LogId = TypeHelper.ObjectToInt(reader["logid"]);
            creditLogInfo.Uid = TypeHelper.ObjectToInt(reader["uid"]);
            creditLogInfo.PayCredits = TypeHelper.ObjectToInt(reader["paycredits"]);
            creditLogInfo.RankCredits = TypeHelper.ObjectToInt(reader["rankcredits"]);
            creditLogInfo.Action = TypeHelper.ObjectToInt(reader["action"]);
            creditLogInfo.ActionCode = TypeHelper.ObjectToInt(reader["actioncode"]);
            creditLogInfo.ActionTime = TypeHelper.ObjectToDateTime(reader["actiontime"]);
            creditLogInfo.ActionDes = reader["actiondes"].ToString();
            creditLogInfo.Operator = TypeHelper.ObjectToInt(reader["operator"]);

            return creditLogInfo;
        }

        #endregion

        /// <summary>
        /// 后台获得积分日志列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static DataTable AdminGetCreditLogList(int pageSize, int pageNumber, string condition)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetCreditLogList(pageSize, pageNumber, condition);
        }

        /// <summary>
        /// 后台获得积分日志列表条件
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static string AdminGetCreditLogListCondition(int uid, string startTime, string endTime)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetCreditLogListCondition(uid, startTime, endTime);
        }

        /// <summary>
        /// 后台获得积分日志数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetCreditLogCount(string condition)
        {
            return BrnShop.Core.BSPData.RDBS.AdminGetCreditLogCount(condition);
        }

        /// <summary>
        /// 发放积分
        /// </summary>
        /// <param name="userRid">用户等级id</param>
        /// <param name="creditLogInfo">积分日志信息</param>
        public static void SendCredits(int userRid, CreditLogInfo creditLogInfo)
        {
            BrnShop.Core.BSPData.RDBS.SendCredits(userRid, creditLogInfo);
        }

        /// <summary>
        /// 获得今天发放的支付积分
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="today">今天日期</param>
        /// <returns></returns>
        public static int GetTodaySendPayCredits(int uid, DateTime today)
        {
            return BrnShop.Core.BSPData.RDBS.GetTodaySendPayCredits(uid, today);
        }

        /// <summary>
        /// 获得今天发放的等级积分
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="today">今天日期</param>
        /// <returns></returns>
        public static int GetTodaySendRankCredits(int uid, DateTime today)
        {
            return BrnShop.Core.BSPData.RDBS.GetTodaySendRankCredits(uid, today);
        }

        /// <summary>
        /// 是否发放了今天的登陆积分
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="today">今天日期</param>
        /// <returns></returns>
        public static bool IsSendTodayLoginCredit(int uid, DateTime today)
        {
            return BrnShop.Core.BSPData.RDBS.IsSendTodayLoginCredit(uid, today);
        }

        /// <summary>
        /// 是否发放了完成用户信息积分
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static bool IsSendCompleteUserInfoCredit(int uid)
        {
            return BrnShop.Core.BSPData.RDBS.IsSendCompleteUserInfoCredit(uid);
        }

        /// <summary>
        /// 获得支付积分日志列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="type">类型(2代表全部类型，0代表收入，1代表支出)</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        public static List<CreditLogInfo> GetPayCreditLogList(int uid, int type, int pageSize, int pageNumber)
        {
            List<CreditLogInfo> creditLogList = new List<CreditLogInfo>();
            IDataReader reader = BrnShop.Core.BSPData.RDBS.GetPayCreditLogList(uid, type, pageSize, pageNumber);
            while (reader.Read())
            {
                CreditLogInfo creditLogInfo = BuildCreditLogFromReader(reader);
                creditLogList.Add(creditLogInfo);
            }
            reader.Close();
            return creditLogList;
        }

        /// <summary>
        /// 获得支付积分日志数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="type">类型(2代表全部类型，0代表收入，1代表支出)</param>
        /// <returns></returns>
        public static int GetPayCreditLogCount(int uid, int type)
        {
            return BrnShop.Core.BSPData.RDBS.GetPayCreditLogCount(uid, type);
        }

        /// <summary>
        /// 获得用户订单发放的积分
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static DataTable GetUserOrderSendCredits(int oid)
        {
            return BrnShop.Core.BSPData.RDBS.GetUserOrderSendCredits(oid);
        }
    }
}
