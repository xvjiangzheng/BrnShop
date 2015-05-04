using System;
using System.Text;

namespace BrnShop.Core
{
    /// <summary>
    /// 邮件策略接口
    /// </summary>
    public partial interface IEmailStrategy
    {
        /// <summary>
        /// 邮件服务器地址
        /// </summary>
        string Host { get; set; }

        /// <summary>
        /// 邮件服务器端口
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// 发送邮件的账号
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// 发送邮件的密码
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// 发送邮件
        /// </summary>
        string From { get; set; }

        /// <summary>
        /// 发送邮件的昵称
        /// </summary>
        string FromName { get; set; }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">接收邮件</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <returns>是否发送成功</returns>
        bool Send(string to, string subject, string body);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">接收邮件</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="bodyEncoding">邮件内容编码</param>
        /// <param name="isBodyHtml">邮件内容是否html化</param>
        /// <returns>是否发送成功</returns>
        bool Send(string to, string subject, string body, Encoding bodyEncoding, bool isBodyHtml);
    }
}
