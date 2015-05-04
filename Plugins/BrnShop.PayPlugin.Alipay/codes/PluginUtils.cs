using System;

using BrnShop.Core;

namespace BrnShop.PayPlugin.Alipay
{
    /// <summary>
    /// 插件工具类
    /// </summary>
    public class PluginUtils
    {
        private static object _locker = new object();//锁对象
        private static PluginSetInfo _pluginsetinfo = null;//插件设置信息
        private static string _dbfilepath = "/plugins/BrnShop.PayPlugin.Alipay/db.config";//数据文件路径

        /// <summary>
        ///获得插件设置
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
        /// 保存插件设置
        /// </summary>
        public static void SavePluginSet(PluginSetInfo pluginSetInfo)
        {
            lock (_locker)
            {
                IOHelper.SerializeToXml(pluginSetInfo, IOHelper.GetMapPath(_dbfilepath));
                _pluginsetinfo = null;
                AlipayConfig.ReSet();
            }
        }
    }

    /// <summary>
    /// 插件设置信息类
    /// </summary>
    public class PluginSetInfo
    {
        private string _partner;//合作者身份ID
        private string _key;//交易安全检验码
        private string _seller;//收款支付宝帐户
        private decimal _payfee;//支付手续费
        private decimal _freemoney;//免费金额

        /// <summary>
        /// 合作者身份ID
        /// </summary>
        public string Partner
        {
            get { return _partner; }
            set { _partner = value; }
        }
        /// <summary>
        /// 交易安全检验码
        /// </summary>
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }
        /// <summary>
        /// 收款支付宝帐户
        /// </summary>
        public string Seller
        {
            get { return _seller; }
            set { _seller = value; }
        }
        /// <summary>
        /// 支付手续费
        /// </summary>
        public decimal PayFee
        {
            get { return _payfee; }
            set { _payfee = value; }
        }
        /// <summary>
        /// 免费金额
        /// </summary>
        public decimal FreeMoney
        {
            get { return _freemoney; }
            set { _freemoney = value; }
        }
    }
}
