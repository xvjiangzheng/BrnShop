using System;

using BrnShop.Core;

namespace BrnShop.OAuthPlugin.QQ
{
    /// <summary>
    /// 插件工具类
    /// </summary>
    public class PluginUtils
    {
        private static object _locker = new object();//锁对象
        private static PluginSetInfo _pluginsetinfo = null;//插件设置信息
        private static string _dbfilepath = "/plugins/BrnShop.OAuthPlugin.QQ/db.config";//数据文件路径

        /// <summary>
        ///获得插件设置信息
        /// </summary>
        /// <returns></returns>
        public static PluginSetInfo GetPluginSet()
        {
            if (_pluginsetinfo == null)
            {
                lock (_locker)
                {
                    if (_pluginsetinfo == null)
                    {
                        _pluginsetinfo = (PluginSetInfo)IOHelper.DeserializeFromXML(typeof(PluginSetInfo), IOHelper.GetMapPath(_dbfilepath));
                    }
                }
            }
            return _pluginsetinfo;
        }

        /// <summary>
        /// 保存插件设置信息
        /// </summary>
        public static void SavePluginSet(PluginSetInfo pluginSetInfo)
        {
            lock (_locker)
            {
                IOHelper.SerializeToXml(pluginSetInfo, IOHelper.GetMapPath(_dbfilepath));
                _pluginsetinfo = null;
            }
        }
    }

    /// <summary>
    /// 插件设置信息
    /// </summary>
    public class PluginSetInfo
    {
        private string _authurl = "";//验证Url
        private string _appkey = "";//应用程序key
        private string _appsecret = "";//应用程序密钥
        private string _server = "";//服务商
        private string _unameprefix = "";//用户名前缀

        /// <summary>
        /// 验证Url
        /// </summary>
        public string AuthUrl
        {
            get { return _authurl; }
            set { _authurl = value; }
        }
        /// <summary>
        /// 应用程序key
        /// </summary>
        public string AppKey
        {
            get { return _appkey; }
            set { _appkey = value; }
        }
        /// <summary>
        /// 应用程序密钥
        /// </summary>
        public string AppSecret
        {
            get { return _appsecret; }
            set { _appsecret = value; }
        }
        /// <summary>
        /// 服务商
        /// </summary>
        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }
        /// <summary>
        /// 用户名前缀
        /// </summary>
        public string UNamePrefix
        {
            get { return _unameprefix; }
            set { _unameprefix = value; }
        }

    }
}
