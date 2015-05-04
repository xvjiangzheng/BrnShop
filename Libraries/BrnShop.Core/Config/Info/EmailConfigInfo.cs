using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 邮件配置信息类
    /// </summary>
    [Serializable]
    public class EmailConfigInfo : IConfigInfo
    {
        private string _host;//服务器地址
        private int _port;//服务器端口
        private string _username;//邮箱账号
        private string _password;//邮箱密码
        private string _from;//发送邮箱
        private string _fromname;//发送邮箱的昵称
        private string _findpwdbody;//找回密码内容
        private string _scverifybody;//安全中心验证邮箱内容
        private string _scupdatebody;//安全中心确认更新邮箱内容
        private string _webcomebody;//注册欢迎信息

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Host
        {
            get { return _host; }
            set { _host = value; }
        }

        /// <summary>
        /// 服务器端口
        /// </summary>
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        /// <summary>
        /// 邮箱账号
        /// </summary>
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        /// 发送邮箱
        /// </summary>
        public string From
        {
            get { return _from; }
            set { _from = value; }
        }

        /// <summary>
        /// 发送邮箱的昵称
        /// </summary>
        public string FromName
        {
            get { return _fromname; }
            set { _fromname = value; }
        }

        /// <summary>
        /// 找回密码内容
        /// </summary>
        public string FindPwdBody
        {
            get { return _findpwdbody; }
            set { _findpwdbody = value; }
        }

        /// <summary>
        /// 安全中心验证邮箱内容
        /// </summary>
        public string SCVerifyBody
        {
            get { return _scverifybody; }
            set { _scverifybody = value; }
        }

        /// <summary>
        /// 安全中心确认更新邮箱内容
        /// </summary>
        public string SCUpdateBody
        {
            get { return _scupdatebody; }
            set { _scupdatebody = value; }
        }

        /// <summary>
        /// 注册欢迎信息
        /// </summary>
        public string WebcomeBody
        {
            get { return _webcomebody; }
            set { _webcomebody = value; }
        }
    }
}
