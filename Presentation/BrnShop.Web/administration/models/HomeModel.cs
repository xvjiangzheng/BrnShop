using System;

namespace BrnShop.Web.Admin.Models
{
    /// <summary>
    /// 商城运行信息模型类
    /// </summary>
    public class ShopRunInfoModel
    {
        /// <summary>
        /// 待确认订单数
        /// </summary>
        public int WaitConfirmCount { get; set; }
        /// <summary>
        /// 待备货订单数
        /// </summary>
        public int WaitPreProductCount { get; set; }
        /// <summary>
        /// 待发货订单数
        /// </summary>
        public int WaitSendCount { get; set; }
        /// <summary>
        /// 待支付订单数
        /// </summary>
        public int WaitPayCount { get; set; }

        /// <summary>
        /// 在线用户数量
        /// </summary>
        public int OnlineUserCount { get; set; }
        /// <summary>
        /// 在线游客数量
        /// </summary>
        public int OnlineGuestCount { get; set; }
        /// <summary>
        /// 在线会员数量
        /// </summary>
        public int OnlineMemberCount { get; set; }

        /// <summary>
        /// 商城版本
        /// </summary>
        public string ShopVersion { get; set; }
        /// <summary>
        /// .net版本
        /// </summary>
        public string NetVersion { get; set; }
        /// <summary>
        /// 操作系统版本
        /// </summary>
        public string OSVersion { get; set; }
        /// <summary>
        /// 服务器运行时间
        /// </summary>
        public string TickCount { get; set; }
        /// <summary>
        /// CPU数量
        /// </summary>
        public string ProcessorCount { get; set; }
        /// <summary>
        /// 内存数
        /// </summary>
        public string WorkingSet { get; set; }
    }
}
