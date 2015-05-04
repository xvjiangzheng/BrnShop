using System;
using System.Data;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 后台积分操作管理类
    /// </summary>
    public partial class AdminCredits : Credits
    {
        /// <summary>
        /// 后台获得积分日志列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static DataTable AdminGetCreditLogList(int pageSize, int pageNumber, string condition)
        {
            return BrnShop.Data.Credits.AdminGetCreditLogList(pageSize, pageNumber, condition);
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
            return BrnShop.Data.Credits.AdminGetCreditLogListCondition(uid, startTime, endTime);
        }

        /// <summary>
        /// 后台获得积分日志数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetCreditLogCount(string condition)
        {
            return BrnShop.Data.Credits.AdminGetCreditLogCount(condition);
        }

        /// <summary>
        /// 管理员发放积分
        /// </summary>
        /// <param name="partUserInfo">用户信息</param>
        /// <param name="payCredits">支付积分</param>
        /// <param name="rankCredits">等级积分</param>
        /// <param name="sendUid">发放用户id</param>
        /// <param name="sendTime">发放时间</param>
        public static void AdminSendCredits(PartUserInfo partUserInfo, int payCredits, int rankCredits, int sendUid, DateTime sendTime)
        {
            int userRid = UserRanks.GetUserRankByCredits(partUserInfo.RankCredits + rankCredits).UserRid;
            if (userRid == partUserInfo.UserRid)
                userRid = 0;

            CreditLogInfo creditLogInfo = new CreditLogInfo();

            creditLogInfo.Uid = partUserInfo.Uid;
            creditLogInfo.PayCredits = payCredits;
            creditLogInfo.RankCredits = rankCredits;
            creditLogInfo.Action = (int)CreditAction.AdminSend;
            creditLogInfo.ActionTime = sendTime;
            creditLogInfo.ActionCode = 0;
            creditLogInfo.ActionDes = "管理员发放";
            creditLogInfo.Operator = sendUid;

            SendCredits(userRid, creditLogInfo);
        }
    }
}
