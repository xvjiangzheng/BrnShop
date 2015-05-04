using System;
using System.Data;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 积分操作管理类
    /// </summary>
    public partial class Credits
    {
        private static object _locker = new object();//锁对象
        private static CreditConfigInfo _creditconfiginfo = null;//积分配置信息

        static Credits()
        {
            _creditconfiginfo = BSPConfig.CreditConfig;
        }

        /// <summary>
        /// 重置积分配置信息
        /// </summary>
        public static void ResetCreditConfig()
        {
            lock (_locker)
            {
                _creditconfiginfo = BSPConfig.CreditConfig;
            }
        }

        /// <summary>
        /// 支付积分名称
        /// </summary>
        public static string PayCreditName
        {
            get { return _creditconfiginfo.PayCreditName; }
        }

        /// <summary>
        /// 等级积分名称
        /// </summary>
        public static string RankCreditName
        {
            get { return _creditconfiginfo.RankCreditName; }
        }

        /// <summary>
        /// 每天最大支付积分发放数量
        /// </summary>
        public static int DayMaxSendPayCredits
        {
            get { return _creditconfiginfo.DayMaxSendPayCredits; }
        }

        /// <summary>
        /// 每笔订单最大使用支付积分
        /// </summary>
        public static int OrderMaxUsePayCredits
        {
            get { return _creditconfiginfo.OrderMaxUsePayCredits; }
        }

        /// <summary>
        /// 发放登陆积分
        /// </summary>
        /// <param name="partUserInfo">用户信息</param>
        /// <param name="loginTime">登陆时间</param>
        public static void SendLoginCredits(ref PartUserInfo partUserInfo, DateTime loginTime)
        {
            if (_creditconfiginfo.LoginPayCredits > 0 || _creditconfiginfo.LoginRankCredits > 0)
            {
                DateTime slcTime = TypeHelper.StringToDateTime(WebHelper.UrlDecode(ShopUtils.GetBSPCookie("slctime")), loginTime.Date.AddDays(-2));
                if (loginTime.Date <= slcTime.Date)
                    return;

                if (!IsSendTodayLoginCredit(partUserInfo.Uid, DateTime.Now))
                {
                    ShopUtils.SetBSPCookie("slctime", WebHelper.UrlEncode(loginTime.ToString()));

                    int surplusPayCredits = GetDaySurplusPayCredits(partUserInfo.Uid, loginTime.Date);
                    int surplusRankCredits = GetDaySurplusRankCredits(partUserInfo.Uid, loginTime.Date);
                    if (surplusPayCredits == 0 && surplusRankCredits == 0)
                        return;

                    int payCredits = 0;
                    int rankCredits = 0;
                    if (surplusPayCredits > 0)
                        payCredits = surplusPayCredits < _creditconfiginfo.LoginPayCredits ? surplusPayCredits : _creditconfiginfo.LoginPayCredits;
                    else if (surplusPayCredits == -1)
                        payCredits = _creditconfiginfo.LoginPayCredits;
                    if (surplusRankCredits > 0)
                        rankCredits = surplusRankCredits < _creditconfiginfo.LoginRankCredits ? surplusRankCredits : _creditconfiginfo.LoginRankCredits;
                    else if (surplusRankCredits == -1)
                        rankCredits = _creditconfiginfo.LoginRankCredits;

                    partUserInfo.PayCredits += payCredits;
                    partUserInfo.RankCredits += rankCredits;

                    int userRid = UserRanks.GetUserRankByCredits(partUserInfo.RankCredits).UserRid;
                    if (userRid != partUserInfo.UserRid)
                        partUserInfo.UserRid = userRid;
                    else
                        userRid = 0;

                    CreditLogInfo creditLogInfo = new CreditLogInfo();
                    creditLogInfo.Uid = partUserInfo.Uid;
                    creditLogInfo.PayCredits = payCredits;
                    creditLogInfo.RankCredits = rankCredits;
                    creditLogInfo.Action = (int)CreditAction.Login;
                    creditLogInfo.ActionCode = 0;
                    creditLogInfo.ActionTime = loginTime;
                    creditLogInfo.ActionDes = "登陆赠送积分";
                    creditLogInfo.Operator = 0;

                    SendCredits(userRid, creditLogInfo);
                }
            }
        }

        /// <summary>
        /// 发放注册积分
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="verifyTime">注册时间</param>
        public static void SendRegisterCredits(ref UserInfo userInfo, DateTime registerTime)
        {
            if (_creditconfiginfo.RegisterPayCredits > 0 || _creditconfiginfo.RegisterRankCredits > 0)
            {
                int surplusPayCredits = GetDaySurplusPayCredits(userInfo.Uid, registerTime.Date);
                int surplusRankCredits = GetDaySurplusRankCredits(userInfo.Uid, registerTime.Date);
                if (surplusPayCredits == 0 && surplusRankCredits == 0)
                    return;

                int payCredits = 0;
                int rankCredits = 0;
                if (surplusPayCredits > 0)
                    payCredits = surplusPayCredits < _creditconfiginfo.RegisterPayCredits ? surplusPayCredits : _creditconfiginfo.RegisterPayCredits;
                else if (surplusPayCredits == -1)
                    payCredits = _creditconfiginfo.RegisterPayCredits;
                if (surplusRankCredits > 0)
                    rankCredits = surplusRankCredits < _creditconfiginfo.RegisterRankCredits ? surplusRankCredits : _creditconfiginfo.RegisterRankCredits;
                else if (surplusRankCredits == -1)
                    rankCredits = _creditconfiginfo.RegisterRankCredits;

                userInfo.PayCredits += payCredits;
                userInfo.RankCredits += rankCredits;

                int userRid = UserRanks.GetUserRankByCredits(userInfo.RankCredits).UserRid;
                if (userRid != userInfo.UserRid)
                    userInfo.UserRid = userRid;
                else
                    userRid = 0;

                CreditLogInfo creditLogInfo = new CreditLogInfo();
                creditLogInfo.Uid = userInfo.Uid;
                creditLogInfo.PayCredits = payCredits;
                creditLogInfo.RankCredits = rankCredits;
                creditLogInfo.Action = (int)CreditAction.Register;
                creditLogInfo.ActionCode = 0;
                creditLogInfo.ActionTime = registerTime;
                creditLogInfo.ActionDes = "注册赠送积分";
                creditLogInfo.Operator = 0;

                SendCredits(userRid, creditLogInfo);
            }
        }

        /// <summary>
        /// 发放验证邮箱积分
        /// </summary>
        /// <param name="partUserInfo">用户信息</param>
        /// <param name="verifyTime">验证时间</param>
        public static void SendVerifyEmailCredits(ref PartUserInfo partUserInfo, DateTime verifyTime)
        {
            if (partUserInfo.VerifyEmail == 0 && (_creditconfiginfo.VerifyEmailPayCredits > 0 || _creditconfiginfo.VerifyEmailRankCredits > 0))
            {
                int surplusPayCredits = GetDaySurplusPayCredits(partUserInfo.Uid, verifyTime.Date);
                int surplusRankCredits = GetDaySurplusRankCredits(partUserInfo.Uid, verifyTime.Date);
                if (surplusPayCredits == 0 && surplusRankCredits == 0)
                    return;

                int payCredits = 0;
                int rankCredits = 0;
                if (surplusPayCredits > 0)
                    payCredits = surplusPayCredits < _creditconfiginfo.VerifyEmailPayCredits ? surplusPayCredits : _creditconfiginfo.VerifyEmailPayCredits;
                else if (surplusPayCredits == -1)
                    payCredits = _creditconfiginfo.VerifyEmailPayCredits;
                if (surplusRankCredits > 0)
                    rankCredits = surplusRankCredits < _creditconfiginfo.VerifyEmailRankCredits ? surplusRankCredits : _creditconfiginfo.VerifyEmailRankCredits;
                else if (surplusRankCredits == -1)
                    rankCredits = _creditconfiginfo.VerifyEmailRankCredits;

                partUserInfo.PayCredits += payCredits;
                partUserInfo.RankCredits += rankCredits;

                int userRid = UserRanks.GetUserRankByCredits(partUserInfo.RankCredits).UserRid;
                if (userRid != partUserInfo.UserRid)
                    partUserInfo.UserRid = userRid;
                else
                    userRid = 0;

                CreditLogInfo creditLogInfo = new CreditLogInfo();
                creditLogInfo.Uid = partUserInfo.Uid;
                creditLogInfo.PayCredits = payCredits;
                creditLogInfo.RankCredits = rankCredits;
                creditLogInfo.Action = (int)CreditAction.VerifyEmail;
                creditLogInfo.ActionCode = 0;
                creditLogInfo.ActionTime = verifyTime;
                creditLogInfo.ActionDes = "验证用户邮箱";
                creditLogInfo.Operator = 0;

                SendCredits(userRid, creditLogInfo);
            }
        }

        /// <summary>
        /// 发放验证手机积分
        /// </summary>
        /// <param name="partUserInfo">用户信息</param>
        /// <param name="verifyTime">验证时间</param>
        public static void SendVerifyMobileCredits(ref PartUserInfo partUserInfo, DateTime verifyTime)
        {
            if (partUserInfo.VerifyMobile == 0 && (_creditconfiginfo.VerifyMobilePayCredits > 0 || _creditconfiginfo.VerifyMobileRankCredits > 0))
            {
                int surplusPayCredits = GetDaySurplusPayCredits(partUserInfo.Uid, verifyTime.Date);
                int surplusRankCredits = GetDaySurplusRankCredits(partUserInfo.Uid, verifyTime.Date);
                if (surplusPayCredits == 0 && surplusRankCredits == 0)
                    return;

                int payCredits = 0;
                int rankCredits = 0;
                if (surplusPayCredits > 0)
                    payCredits = surplusPayCredits < _creditconfiginfo.VerifyMobilePayCredits ? surplusPayCredits : _creditconfiginfo.VerifyMobilePayCredits;
                else if (surplusPayCredits == -1)
                    payCredits = _creditconfiginfo.VerifyMobilePayCredits;
                if (surplusRankCredits > 0)
                    rankCredits = surplusRankCredits < _creditconfiginfo.VerifyMobileRankCredits ? surplusRankCredits : _creditconfiginfo.VerifyMobileRankCredits;
                else if (surplusRankCredits == -1)
                    rankCredits = _creditconfiginfo.VerifyMobileRankCredits;

                partUserInfo.PayCredits += payCredits;
                partUserInfo.RankCredits += rankCredits;

                int userRid = UserRanks.GetUserRankByCredits(partUserInfo.RankCredits).UserRid;
                if (userRid != partUserInfo.UserRid)
                    partUserInfo.UserRid = userRid;
                else
                    userRid = 0;

                CreditLogInfo creditLogInfo = new CreditLogInfo();
                creditLogInfo.Uid = partUserInfo.Uid;
                creditLogInfo.PayCredits = payCredits;
                creditLogInfo.RankCredits = rankCredits;
                creditLogInfo.Action = (int)CreditAction.VerifyMobile;
                creditLogInfo.ActionCode = 0;
                creditLogInfo.ActionTime = verifyTime;
                creditLogInfo.ActionDes = "验证用户手机";
                creditLogInfo.Operator = 0;

                SendCredits(userRid, creditLogInfo);
            }
        }

        /// <summary>
        /// 发放完善用户信息积分
        /// </summary>
        /// <param name="partUserInfo">用户信息</param>
        /// <param name="completeTime">完成时间</param>
        public static void SendCompleteUserInfoCredits(ref PartUserInfo partUserInfo, DateTime completeTime)
        {
            if ((_creditconfiginfo.CompleteUserInfoPayCredits > 0 || _creditconfiginfo.CompleteUserInfoRankCredits > 0) && !IsSendCompleteUserInfoCredit(partUserInfo.Uid))
            {
                int surplusPayCredits = GetDaySurplusPayCredits(partUserInfo.Uid, completeTime.Date);
                int surplusRankCredits = GetDaySurplusRankCredits(partUserInfo.Uid, completeTime.Date);
                if (surplusPayCredits == 0 && surplusRankCredits == 0)
                    return;

                int payCredits = 0;
                int rankCredits = 0;
                if (surplusPayCredits > 0)
                    payCredits = surplusPayCredits < _creditconfiginfo.CompleteUserInfoPayCredits ? surplusPayCredits : _creditconfiginfo.CompleteUserInfoPayCredits;
                else if (surplusPayCredits == -1)
                    payCredits = _creditconfiginfo.CompleteUserInfoPayCredits;
                if (surplusRankCredits > 0)
                    rankCredits = surplusRankCredits < _creditconfiginfo.CompleteUserInfoRankCredits ? surplusRankCredits : _creditconfiginfo.CompleteUserInfoRankCredits;
                else if (surplusRankCredits == -1)
                    rankCredits = _creditconfiginfo.CompleteUserInfoRankCredits;

                partUserInfo.PayCredits += payCredits;
                partUserInfo.RankCredits += rankCredits;

                int userRid = UserRanks.GetUserRankByCredits(partUserInfo.RankCredits).UserRid;
                if (userRid != partUserInfo.UserRid)
                    partUserInfo.UserRid = userRid;
                else
                    userRid = 0;

                CreditLogInfo creditLogInfo = new CreditLogInfo();
                creditLogInfo.Uid = partUserInfo.Uid;
                creditLogInfo.PayCredits = payCredits;
                creditLogInfo.RankCredits = rankCredits;
                creditLogInfo.Action = (int)CreditAction.CompleteUserInfo;
                creditLogInfo.ActionCode = 0;
                creditLogInfo.ActionTime = completeTime;
                creditLogInfo.ActionDes = "完善用户信息";
                creditLogInfo.Operator = 0;

                SendCredits(userRid, creditLogInfo);
            }
        }

        /// <summary>
        /// 发放完成订单积分
        /// </summary>
        /// <param name="partUserInfo">用户信息</param>
        /// <param name="orderInfo">订单信息</param>
        /// <param name="orderProductList">订单商品列表</param>
        /// <param name="completeTime">完成时间</param>
        public static void SendCompleteOrderCredits(ref PartUserInfo partUserInfo, OrderInfo orderInfo, List<OrderProductInfo> orderProductList, DateTime completeTime)
        {
            if (_creditconfiginfo.CompleteOrderPayCredits > 0 || _creditconfiginfo.CompleteOrderRankCredits > 0)
            {
                int surplusPayCredits = GetDaySurplusPayCredits(partUserInfo.Uid, completeTime.Date);
                int surplusRankCredits = GetDaySurplusRankCredits(partUserInfo.Uid, completeTime.Date);
                if (surplusPayCredits == 0 && surplusRankCredits == 0)
                    return;

                int payCredits = 0;
                int rankCredits = 0;
                int tempPayCredits = (int)Math.Floor(orderInfo.OrderAmount * _creditconfiginfo.CompleteOrderPayCredits / 100);
                int tempRankCredits = (int)Math.Floor(orderInfo.OrderAmount * _creditconfiginfo.CompleteOrderRankCredits / 100);
                if (surplusPayCredits > 0)
                    payCredits = surplusPayCredits < tempPayCredits ? surplusPayCredits : tempPayCredits;
                else if (surplusPayCredits == -1)
                    payCredits = tempPayCredits;
                if (surplusRankCredits > 0)
                    rankCredits = surplusRankCredits < tempRankCredits ? surplusRankCredits : tempRankCredits;
                else if (surplusRankCredits == -1)
                    rankCredits = tempRankCredits;

                partUserInfo.PayCredits += payCredits;
                partUserInfo.RankCredits += rankCredits;

                int userRid = UserRanks.GetUserRankByCredits(partUserInfo.RankCredits).UserRid;
                if (userRid != partUserInfo.UserRid)
                    partUserInfo.UserRid = userRid;
                else
                    userRid = 0;

                CreditLogInfo creditLogInfo = new CreditLogInfo();
                creditLogInfo.Uid = partUserInfo.Uid;
                creditLogInfo.PayCredits = payCredits;
                creditLogInfo.RankCredits = rankCredits;
                creditLogInfo.Action = (int)CreditAction.CompleteOrder;
                creditLogInfo.ActionCode = orderInfo.Oid;
                creditLogInfo.ActionTime = completeTime;
                creditLogInfo.ActionDes = "完成订单:" + orderInfo.OSN;
                creditLogInfo.Operator = 0;

                SendCredits(userRid, creditLogInfo);
            }
        }

        /// <summary>
        /// 发放评价商品积分
        /// </summary>
        /// <param name="partUserInfo">用户信息</param>
        /// <param name="orderProductInfo">订单商品</param>
        /// <param name="reviewTime">评价时间</param>
        public static int SendReviewProductCredits(ref PartUserInfo partUserInfo, OrderProductInfo orderProductInfo, DateTime reviewTime)
        {
            if (_creditconfiginfo.ReviewProductPayCredits > 0 || _creditconfiginfo.ReviewProductRankCredits > 0)
            {
                int surplusPayCredits = GetDaySurplusPayCredits(partUserInfo.Uid, reviewTime.Date);
                int surplusRankCredits = GetDaySurplusRankCredits(partUserInfo.Uid, reviewTime.Date);
                if (surplusPayCredits == 0 && surplusRankCredits == 0)
                    return 0;

                int payCredits = 0;
                int rankCredits = 0;
                if (surplusPayCredits > 0)
                    payCredits = surplusPayCredits < _creditconfiginfo.ReviewProductPayCredits ? surplusPayCredits : _creditconfiginfo.ReviewProductPayCredits;
                else if (surplusPayCredits == -1)
                    payCredits = _creditconfiginfo.ReviewProductPayCredits;
                if (surplusRankCredits > 0)
                    rankCredits = surplusRankCredits < _creditconfiginfo.ReviewProductRankCredits ? surplusRankCredits : _creditconfiginfo.ReviewProductRankCredits;
                else if (surplusRankCredits == -1)
                    rankCredits = _creditconfiginfo.ReviewProductRankCredits;

                partUserInfo.PayCredits += payCredits;
                partUserInfo.RankCredits += rankCredits;

                int userRid = UserRanks.GetUserRankByCredits(partUserInfo.RankCredits).UserRid;
                if (userRid != partUserInfo.UserRid)
                    partUserInfo.UserRid = userRid;
                else
                    userRid = 0;

                CreditLogInfo creditLogInfo = new CreditLogInfo();
                creditLogInfo.Uid = partUserInfo.Uid;
                creditLogInfo.PayCredits = payCredits;
                creditLogInfo.RankCredits = rankCredits;
                creditLogInfo.Action = (int)CreditAction.ReviewProduct;
                creditLogInfo.ActionCode = orderProductInfo.Oid;
                creditLogInfo.ActionTime = reviewTime;
                creditLogInfo.ActionDes = "评价商品:" + orderProductInfo.Name;
                creditLogInfo.Operator = 0;

                SendCredits(userRid, creditLogInfo);

                return payCredits;
            }
            return 0;
        }

        /// <summary>
        /// 发放单品促销活动积分
        /// </summary>
        /// <param name="partUserInfo">用户信息</param>
        /// <param name="orderInfo">订单信息</param>
        /// <param name="credits">积分</param>
        /// <param name="sendTime">发放时间</param>
        public static void SendSinglePromotionCredits(ref PartUserInfo partUserInfo, OrderInfo orderInfo, int credits, DateTime sendTime)
        {
            int surplusPayCredits = GetDaySurplusPayCredits(partUserInfo.Uid, sendTime.Date);
            if (surplusPayCredits != 0)
            {
                int creditCount = credits;
                if (surplusPayCredits > 0 && surplusPayCredits < creditCount)
                    creditCount = surplusPayCredits;

                partUserInfo.PayCredits += creditCount;

                CreditLogInfo creditLogInfo = new CreditLogInfo();
                creditLogInfo.Uid = partUserInfo.Uid;
                creditLogInfo.PayCredits = creditCount;
                creditLogInfo.RankCredits = 0;
                creditLogInfo.Action = (int)CreditAction.SinglePromotion;
                creditLogInfo.ActionCode = orderInfo.Oid;
                creditLogInfo.ActionTime = sendTime;
                creditLogInfo.ActionDes = "单品促销活动";
                creditLogInfo.Operator = 0;

                SendCredits(0, creditLogInfo);
            }
        }

        /// <summary>
        /// 支付订单
        /// </summary>
        /// <param name="partUserInfo">用户信息</param>
        /// <param name="orderInfo">订单信息</param>
        /// <param name="credits">积分数量</param>
        /// <param name="payTime">支付时间</param>
        public static void PayOrder(ref PartUserInfo partUserInfo, OrderInfo orderInfo, int credits, DateTime payTime)
        {
            if (credits > 0)
            {
                partUserInfo.PayCredits -= credits;

                CreditLogInfo creditLogInfo = new CreditLogInfo();
                creditLogInfo.Uid = partUserInfo.Uid;
                creditLogInfo.PayCredits = -1 * credits;
                creditLogInfo.RankCredits = 0;
                creditLogInfo.Action = (int)CreditAction.PayOrder;
                creditLogInfo.ActionCode = orderInfo.Oid;
                creditLogInfo.ActionTime = payTime;
                creditLogInfo.ActionDes = "支付订单:" + orderInfo.OSN;
                creditLogInfo.Operator = partUserInfo.Uid;

                SendCredits(0, creditLogInfo);
            }
        }

        /// <summary>
        /// 退回用户订单使用的积分
        /// </summary>
        /// <param name="partUserInfo">用户信息</param>
        /// <param name="orderInfo">订单信息</param>
        /// <param name="operatorId">操作人</param>
        /// <param name="returnTime">退回时间</param>
        public static void ReturnUserOrderUseCredits(ref PartUserInfo partUserInfo, OrderInfo orderInfo, int operatorId, DateTime returnTime)
        {
            partUserInfo.PayCredits += orderInfo.PayCreditCount;

            CreditLogInfo creditLogInfo = new CreditLogInfo();
            creditLogInfo.Uid = orderInfo.Uid;
            creditLogInfo.PayCredits = orderInfo.PayCreditCount;
            creditLogInfo.RankCredits = 0;
            creditLogInfo.Action = (int)CreditAction.ReturnOrderUse;
            creditLogInfo.ActionCode = orderInfo.Oid;
            creditLogInfo.ActionTime = returnTime;
            creditLogInfo.ActionDes = "退回用户订单使用的积分:" + orderInfo.OSN;
            creditLogInfo.Operator = operatorId;

            SendCredits(0, creditLogInfo);
        }

        /// <summary>
        /// 退回用户订单发放的积分
        /// </summary>
        /// <param name="partUserInfo">用户信息</param>
        /// <param name="orderInfo">订单信息</param>
        /// <param name="payCredits">支付积分</param>
        /// <param name="rankCredits">等级积分</param>
        /// <param name="operatorId">操作人</param>
        /// <param name="returnTime">退回时间</param>
        public static void ReturnUserOrderSendCredits(ref PartUserInfo partUserInfo, OrderInfo orderInfo, int payCredits, int rankCredits, int operatorId, DateTime returnTime)
        {
            partUserInfo.PayCredits -= payCredits;
            partUserInfo.RankCredits -= rankCredits;

            int userRid = UserRanks.GetUserRankByCredits(partUserInfo.RankCredits).UserRid;
            if (userRid != partUserInfo.UserRid)
                partUserInfo.UserRid = userRid;
            else
                userRid = 0;

            CreditLogInfo creditLogInfo = new CreditLogInfo();
            creditLogInfo.Uid = orderInfo.Uid;
            creditLogInfo.PayCredits = -1 * payCredits;
            creditLogInfo.RankCredits = -1 * rankCredits;
            creditLogInfo.Action = (int)CreditAction.ReturnOrderSend;
            creditLogInfo.ActionCode = orderInfo.Oid;
            creditLogInfo.ActionTime = returnTime;
            creditLogInfo.ActionDes = "收回订单发放的积分:" + orderInfo.OSN;
            creditLogInfo.Operator = operatorId;

            SendCredits(userRid, creditLogInfo);
        }

        /// <summary>
        /// 发放积分
        /// </summary>
        /// <param name="userRid">用户等级id</param>
        /// <param name="creditLogInfo">积分日志信息</param>
        public static void SendCredits(int userRid, CreditLogInfo creditLogInfo)
        {
            BrnShop.Data.Credits.SendCredits(userRid, creditLogInfo);
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
            return BrnShop.Data.Credits.GetPayCreditLogList(uid, type, pageSize, pageNumber);
        }

        /// <summary>
        /// 获得支付积分日志数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="type">类型(2代表全部类型，0代表收入，1代表支出)</param>
        /// <returns></returns>
        public static int GetPayCreditLogCount(int uid, int type)
        {
            return BrnShop.Data.Credits.GetPayCreditLogCount(uid, type);
        }

        /// <summary>
        /// 获得今天发放的支付积分
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="today">今天日期</param>
        /// <returns></returns>
        public static int GetTodaySendPayCredits(int uid, DateTime today)
        {
            return BrnShop.Data.Credits.GetTodaySendPayCredits(uid, today);
        }

        /// <summary>
        /// 获得今天发放的等级积分
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="today">今天日期</param>
        /// <returns></returns>
        public static int GetTodaySendRankCredits(int uid, DateTime today)
        {
            return BrnShop.Data.Credits.GetTodaySendRankCredits(uid, today);
        }

        /// <summary>
        /// 获得今天剩余的积分发放数
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="today">今天日期</param>
        /// <returns></returns>
        public static int GetDaySurplusPayCredits(int uid, DateTime today)
        {
            if (_creditconfiginfo.DayMaxSendPayCredits > 0)
            {
                int todaySendCredits = GetTodaySendPayCredits(uid, today);
                if (todaySendCredits < _creditconfiginfo.DayMaxSendPayCredits)
                    return _creditconfiginfo.DayMaxSendPayCredits - todaySendCredits;
                else
                    return 0;
            }
            return -1;
        }

        /// <summary>
        /// 获得今天剩余的积分发放数
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="today">今天日期</param>
        /// <returns></returns>
        public static int GetDaySurplusRankCredits(int uid, DateTime today)
        {
            if (_creditconfiginfo.DayMaxSendRankCredits > 0)
            {
                int todaySendCredits = GetTodaySendRankCredits(uid, today);
                if (todaySendCredits < _creditconfiginfo.DayMaxSendRankCredits)
                    return _creditconfiginfo.DayMaxSendRankCredits - todaySendCredits;
                else
                    return 0;
            }
            return -1;
        }

        /// <summary>
        /// 是否发放了今天的登陆积分
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="today">今天日期</param>
        /// <returns></returns>
        public static bool IsSendTodayLoginCredit(int uid, DateTime today)
        {
            return BrnShop.Data.Credits.IsSendTodayLoginCredit(uid, today);
        }

        /// <summary>
        /// 是否发放了完成用户信息积分
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static bool IsSendCompleteUserInfoCredit(int uid)
        {
            return BrnShop.Data.Credits.IsSendCompleteUserInfoCredit(uid);
        }

        /// <summary>
        /// 获得订单最大使用支付积分
        /// </summary>
        /// <param name="payCredits">支付积分</param>
        /// <returns></returns>
        public static int GetOrderMaxUsePayCredits(int payCredits)
        {
            if (_creditconfiginfo.OrderMaxUsePayCredits > 0)
                return payCredits < _creditconfiginfo.OrderMaxUsePayCredits ? payCredits : _creditconfiginfo.OrderMaxUsePayCredits;
            return payCredits;
        }

        /// <summary>
        /// 将支付积分转换为金钱
        /// </summary>
        /// <param name="payCredits">支付积分</param>
        /// <returns></returns>
        public static decimal PayCreditsToMoney(int payCredits)
        {
            if (payCredits <= 0)
                return 0M;
            return (decimal)payCredits / 100 * _creditconfiginfo.PayCreditPrice;
        }

        /// <summary>
        /// 将金钱转换为支付积分
        /// </summary>
        /// <param name="money">金钱</param>
        /// <returns></returns>
        public static int MoneyToPayCredits(decimal money)
        {
            if (money <= 0M)
                return 0;
            return (int)(money * 100 / _creditconfiginfo.PayCreditPrice);
        }

        /// <summary>
        /// 获得用户订单发放的积分
        /// </summary>
        /// <param name="oid">订单id</param>
        /// <returns></returns>
        public static DataTable GetUserOrderSendCredits(int oid)
        {
            return BrnShop.Data.Credits.GetUserOrderSendCredits(oid);
        }
    }
}
