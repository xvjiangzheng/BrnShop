using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 短信配置信息类
    /// </summary>
    [Serializable]
    public class SMSConfigInfo : IConfigInfo
    {
        private string _url;//短信服务器地址
        private string _username;//短信账号
        private string _password;//短信密码
        private string _findpwdbody;//找回密码内容
        private string _scverifybody;//安全中心验证手机内容
        private string _scupdatebody;//安全中心确认更新手机内容
        private string _webcomebody;//注册欢迎信息

        /// <summary>
        /// 短信服务器地址
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        /// <summary>
        /// 短信账号
        /// </summary>
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        /// <summary>
        /// 短信密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
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
        /// 安全中心验证手机内容
        /// </summary>
        public string SCVerifyBody
        {
            get { return _scverifybody; }
            set { _scverifybody = value; }
        }

        /// <summary>
        /// 安全中心确认更新手机内容
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
