using System;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.Web.Admin.Models;

namespace BrnShop.Web.Admin.Controllers
{
    /// <summary>
    /// 后台日志控制器类
    /// </summary>
    public partial class LogController : BaseAdminController
    {
        /// <summary>
        /// 管理员操作日志列表
        /// </summary>
        /// <param name="accountName">操作人</param>
        /// <param name="operation">操作动作</param>
        /// <param name="startTime">操作开始时间</param>
        /// <param name="endTime">操作结束时间</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">每页数</param>
        /// <returns></returns>
        public ActionResult AdminOperateLogList(string accountName, string operation, string startTime, string endTime, int pageNumber = 1, int pageSize = 15)
        {
            int uid = AdminUsers.GetUidByAccountName(accountName);

            string condition = AdminOperateLogs.GetAdminOperateLogListCondition(uid, operation, startTime, endTime);

            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminOperateLogs.GetAdminOperateLogCount(condition));

            AdminOperateLogListModel model = new AdminOperateLogListModel()
            {
                AdminOperateLogList = AdminOperateLogs.GetAdminOperateLogList(pageModel.PageSize, pageModel.PageNumber, condition),
                PageModel = pageModel,
                AccountName = accountName,
                Operation = operation,
                StartTime = startTime,
                EndTime = endTime
            };
            ShopUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&accountName={3}&operation={4}&startTime={5}&endTime={6}",
                                                          Url.Action("adminoperateloglist"),
                                                          pageModel.PageNumber,
                                                          pageModel.PageSize,
                                                          accountName, operation, startTime, endTime));
            return View(model);
        }

        /// <summary>
        /// 删除管理员操作日志
        /// </summary>
        public ActionResult DelAdminOperateLog(int[] logIdList)
        {
            AdminOperateLogs.DeleteAdminOperateLogById(logIdList);
            AddAdminOperateLog("删除管理员操作日志", "删除管理员操作日志,日志ID为:" + CommonHelper.IntArrayToString(logIdList));
            return PromptView("管理员操作日志删除成功");
        }

        /// <summary>
        /// 积分日志列表
        /// </summary>
        /// <param name="accountName">账户名</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">每页数</param>
        /// <returns></returns>
        public ActionResult CreditLogList(string accountName, string startTime, string endTime, int pageNumber = 1, int pageSize = 15)
        {
            int uid = AdminUsers.GetUidByAccountName(accountName);

            string condition = AdminCredits.AdminGetCreditLogListCondition(uid, startTime, endTime);

            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminCredits.AdminGetCreditLogCount(condition));

            CreditLogListModel model = new CreditLogListModel()
            {
                CreditLogList = AdminCredits.AdminGetCreditLogList(pageModel.PageSize, pageModel.PageNumber, condition),
                PageModel = pageModel,
                AccountName = accountName,
                StartTime = startTime,
                EndTime = endTime
            };
            ShopUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&accountName={3}&startTime={4}&endTime={5}",
                                                          Url.Action("creditloglist"),
                                                          pageModel.PageNumber,
                                                          pageModel.PageSize,
                                                          accountName, startTime, endTime));
            return View(model);
        }

        /// <summary>
        /// 发放积分
        /// </summary>
        /// <param name="rUserName">接收用户名</param>
        /// <param name="payCredits">支付积分</param>
        /// <param name="rankCredits">等级积分</param>
        /// <returns></returns>
        public ActionResult SendCredits(string rUserName, int payCredits = 0, int rankCredits = 0)
        {
            PartUserInfo partUserInfo = AdminUsers.GetPartUserByName(rUserName);
            if (partUserInfo == null)
                return PromptView("请输入正确的用户名");

            AdminCredits.AdminSendCredits(partUserInfo, payCredits, rankCredits, WorkContext.Uid, DateTime.Now);
            AddAdminOperateLog("发放积分", "支付积分为:" + payCredits + ",等级积分为:" + rankCredits);
            return PromptView("积分发放成功");
        }
    }
}
