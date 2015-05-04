using System;
using System.Net;
using System.Text;
using System.Net.Mail;

using BrnShop.Core;

namespace BrnShop.EmailStrategy.DotNet
{
    /// <summary>
    /// 基于.Net自带的邮件框架的策略
    /// </summary>
    public partial class EmailStrategy : IEmailStrategy
    {
        private string _host;
        private int _port;
        private string _username;
        private string _password;
        private string _from;
        private string _fromname;
        private Encoding _bodyencoding = Encoding.GetEncoding("utf-8");
        private bool _isbodyhtml = true;

        /// <summary>
        /// 邮件服务器地址
        /// </summary>
        public string Host
        {
            get { return _host; }
            set { _host = value; }
        }

        /// <summary>
        /// 邮件服务器端口
        /// </summary>
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        /// <summary>
        /// 发送邮件的账号
        /// </summary>
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        /// <summary>
        /// 发送邮件的密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        public string From
        {
            get { return _from; }
            set { _from = value; }
        }

        /// <summary>
        /// 发送邮件的昵称
        /// </summary>
        public string FromName
        {
            get { return _fromname; }
            set { _fromname = value; }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">接收邮件</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <returns>是否发送成功</returns>
        public bool Send(string to, string subject, string body)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            if (_port != 25)
                smtp.EnableSsl = true;

            smtp.Host = _host;
            smtp.Port = _port;
            smtp.Credentials = new NetworkCredential(_username, _password);

            MailMessage mm = new MailMessage();
            mm.Priority = MailPriority.Normal;
            mm.From = new MailAddress(_from, subject, _bodyencoding);
            mm.To.Add(to);
            mm.Subject = subject;
            mm.Body = body;
            mm.BodyEncoding = _bodyencoding;
            mm.IsBodyHtml = _isbodyhtml;

            try
            {
                smtp.Send(mm);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">接收邮件</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="bodyEncoding">邮件内容编码</param>
        /// <param name="isBodyHtml">邮件内容是否html化</param>
        /// <returns>是否发送成功</returns>
        public bool Send(string to, string subject, string body, Encoding bodyEncoding, bool isBodyHtml)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            if (_port != 25)
                smtp.EnableSsl = true;

            smtp.Host = _host;
            smtp.Port = _port;
            smtp.Credentials = new NetworkCredential(_username, _password);

            MailMessage mm = new MailMessage();
            mm.Priority = MailPriority.Normal;
            mm.From = new MailAddress(_from, subject, bodyEncoding);
            mm.To.Add(to);
            mm.Subject = subject;
            mm.Body = body;
            mm.BodyEncoding = bodyEncoding;
            mm.IsBodyHtml = isBodyHtml;

            try
            {
                smtp.Send(mm);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
