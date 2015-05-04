using System;

using BrnShop.Core;

namespace BrnShop.SMSStrategy.BrnShop
{
    /// <summary>
    /// 简单短信策略
    /// </summary>
    public partial class SMSStrategy : ISMSStrategy
    {
        private string _url;
        private string _username;
        private string _password;

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
        /// 发送短信
        /// </summary>
        /// <param name="to">接收人号码</param>
        /// <param name="body">短信内容</param>
        /// <returns>是否发送成功</returns>
        public bool Send(string to, string body)
        {
            string postData = string.Format("OperID={2}&OperPass={3}&DesMobile={0}&Content={1}&ContentType=15", to, body, _username, _password);
            string content = WebHelper.GetRequestData(_url, postData);

            //以下各种情况的判断要根据不同平台具体调整
            if (content.Contains("<code>03</code>"))
            {
                return true;
            }
            else
            {
                if (content.Substring(0, 1) == "2") //余额不足
                {
                    //"手机短信余额不足";
                    //TODO
                }
                else
                {
                    //短信发送失败的其他原因
                    //TODO
                }
                return false;
            }
        }
    }
}
