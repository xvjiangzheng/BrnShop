using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 积分配置信息类
    /// </summary>
    [Serializable]
    public class CreditConfigInfo : IConfigInfo
    {
        private string _paycreditname;//支付积分名称
        private int _paycreditprice;//支付积分价格(单位为100个)
        private int _daymaxsendpaycredits;//每天最大发放支付积分
        private int _ordermaxusepaycredits;//每笔订单最大使用支付积分
        private int _registerpaycredits;//注册支付积分
        private int _loginpaycredits;//每天登陆支付积分
        private int _verifyemailpaycredits;//验证邮箱支付积分
        private int _verifymobilepaycredits;//验证手机支付积分
        private int _completeuserinfopaycredits;//完善用户信息支付积分
        private int _completeorderpaycredits;//完成订单支付积分(以订单金额的百分比计算)
        private int _reviewproductpaycredits;//评价商品支付积分

        private string _rankcreditname;//等级积分名称
        private int _daymaxsendrankcredits;//每天最大发放等级积分
        private int _registerrankcredits;//注册等级积分
        private int _loginrankcredits;//每天登陆等级积分
        private int _verifyemailrankcredits;//验证邮箱等级积分
        private int _verifymobilerankcredits;//验证手机等级积分
        private int _completeuserinforankcredits;//完善用户信息等级积分
        private int _completeorderrankcredits;//完成订单等级积分(以订单金额的百分比计算)
        private int _reviewproductrankcredits;//评价商品等级积分


        /// <summary>
        /// 支付积分名称
        /// </summary>
        public string PayCreditName
        {
            get { return _paycreditname; }
            set { _paycreditname = value; }
        }
        /// <summary>
        /// 支付积分价格(单位为100个)
        /// </summary>
        public int PayCreditPrice
        {
            get { return _paycreditprice; }
            set { _paycreditprice = value; }
        }
        /// <summary>
        /// 每天最大发放支付积分
        /// </summary>
        public int DayMaxSendPayCredits
        {
            get { return _daymaxsendpaycredits; }
            set { _daymaxsendpaycredits = value; }
        }
        /// <summary>
        /// 每笔订单最大使用支付积分
        /// </summary>
        public int OrderMaxUsePayCredits
        {
            get { return _ordermaxusepaycredits; }
            set { _ordermaxusepaycredits = value; }
        }
        /// <summary>
        /// 注册支付积分
        /// </summary>
        public int RegisterPayCredits
        {
            get { return _registerpaycredits; }
            set { _registerpaycredits = value; }
        }
        /// <summary>
        /// 每天登陆支付积分
        /// </summary>
        public int LoginPayCredits
        {
            get { return _loginpaycredits; }
            set { _loginpaycredits = value; }
        }
        /// <summary>
        /// 验证邮箱支付积分
        /// </summary>
        public int VerifyEmailPayCredits
        {
            get { return _verifyemailpaycredits; }
            set { _verifyemailpaycredits = value; }
        }
        /// <summary>
        /// 验证手机支付积分
        /// </summary>
        public int VerifyMobilePayCredits
        {
            get { return _verifymobilepaycredits; }
            set { _verifymobilepaycredits = value; }
        }
        /// <summary>
        /// 完善用户信息支付积分
        /// </summary>
        public int CompleteUserInfoPayCredits
        {
            get { return _completeuserinfopaycredits; }
            set { _completeuserinfopaycredits = value; }
        }
        /// <summary>
        /// 完成订单支付积分(以订单金额的百分比计算)
        /// </summary>
        public int CompleteOrderPayCredits
        {
            get { return _completeorderpaycredits; }
            set { _completeorderpaycredits = value; }
        }
        /// <summary>
        /// 评价商品支付积分
        /// </summary>
        public int ReviewProductPayCredits
        {
            get { return _reviewproductpaycredits; }
            set { _reviewproductpaycredits = value; }
        }

        /// <summary>
        /// 等级积分名称
        /// </summary>
        public string RankCreditName
        {
            get { return _rankcreditname; }
            set { _rankcreditname = value; }
        }
        /// <summary>
        /// 每天最大发放等级积分
        /// </summary>
        public int DayMaxSendRankCredits
        {
            get { return _daymaxsendrankcredits; }
            set { _daymaxsendrankcredits = value; }
        }
        /// <summary>
        /// 注册等级积分
        /// </summary>
        public int RegisterRankCredits
        {
            get { return _registerrankcredits; }
            set { _registerrankcredits = value; }
        }
        /// <summary>
        /// 每天登陆等级积分
        /// </summary>
        public int LoginRankCredits
        {
            get { return _loginrankcredits; }
            set { _loginrankcredits = value; }
        }
        /// <summary>
        /// 验证邮箱等级积分
        /// </summary>
        public int VerifyEmailRankCredits
        {
            get { return _verifyemailrankcredits; }
            set { _verifyemailrankcredits = value; }
        }
        /// <summary>
        /// 验证手机等级积分
        /// </summary>
        public int VerifyMobileRankCredits
        {
            get { return _verifymobilerankcredits; }
            set { _verifymobilerankcredits = value; }
        }
        /// <summary>
        /// 完善用户信息等级积分
        /// </summary>
        public int CompleteUserInfoRankCredits
        {
            get { return _completeuserinforankcredits; }
            set { _completeuserinforankcredits = value; }
        }
        /// <summary>
        /// 完成订单等级积分(以订单金额的百分比计算)
        /// </summary>
        public int CompleteOrderRankCredits
        {
            get { return _completeorderrankcredits; }
            set { _completeorderrankcredits = value; }
        }
        /// <summary>
        /// 评价商品等级积分
        /// </summary>
        public int ReviewProductRankCredits
        {
            get { return _reviewproductrankcredits; }
            set { _reviewproductrankcredits = value; }
        }
    }
}
