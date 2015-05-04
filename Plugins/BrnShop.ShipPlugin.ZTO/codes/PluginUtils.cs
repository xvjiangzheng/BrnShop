using System;
using System.Collections.Generic;

using BrnShop.Core;

namespace BrnShop.ShipPlugin.ZTO
{
    /// <summary>
    /// 插件工具类
    /// </summary>
    public class PluginUtils
    {
        private static object _locker = new object();//锁对象
        private static List<ShipRuleInfo> _shiprulelist = null;//配送规则列表
        private static string _dbfilepath = "/plugins/BrnShop.ShipPlugin.ZTO/db.config";//数据文件路径

        /// <summary>
        /// 获得配送规则列表
        /// </summary>
        /// <returns></returns>
        public static List<ShipRuleInfo> GetShipRuleList()
        {
            if (_shiprulelist == null)
            {
                lock (_locker)
                {
                    if (_shiprulelist == null)
                    {
                        _shiprulelist = (List<ShipRuleInfo>)IOHelper.DeserializeFromXML(typeof(List<ShipRuleInfo>), IOHelper.GetMapPath(_dbfilepath));
                    }
                }
            }
            return _shiprulelist;
        }

        /// <summary>
        /// 保存配送规则列表
        /// </summary>
        public static void SaveShipRuleList(List<ShipRuleInfo> shipRuleList)
        {
            lock (_locker)
            {
                IOHelper.SerializeToXml(shipRuleList, IOHelper.GetMapPath(_dbfilepath));
                _shiprulelist = null;
            }
        }
    }

    /// <summary>
    /// 配送规则信息类
    /// </summary>
    public class ShipRuleInfo
    {
        private string _name;//名称
        private int _regionid;//区域id
        private string _regiontitle;//区域标题
        private int _type;//类型(0代表按重量计算,1代表按商品件数计算)
        private decimal _extcode1;//扩展1
        private decimal _extcode2;//扩展2
        private decimal _freemoney;//免费金额
        private decimal _codpayfee;//货到付款支付手续费

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 区域id
        /// </summary>
        public int RegionId
        {
            get { return _regionid; }
            set { _regionid = value; }
        }
        /// <summary>
        /// 区域标题
        /// </summary>
        public string RegionTitle
        {
            get { return _regiontitle; }
            set { _regiontitle = value; }
        }
        /// <summary>
        /// 类型(0代表按重量计算,1代表按商品件数计算)
        /// </summary>
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }
        /// <summary>
        /// 扩展1
        /// </summary>
        public decimal ExtCode1
        {
            get { return _extcode1; }
            set { _extcode1 = value; }
        }
        /// <summary>
        /// 扩展2
        /// </summary>
        public decimal ExtCode2
        {
            get { return _extcode2; }
            set { _extcode2 = value; }
        }
        /// <summary>
        /// 免费金额
        /// </summary>
        public decimal FreeMoney
        {
            get { return _freemoney; }
            set { _freemoney = value; }
        }
        /// <summary>
        /// 货到付款支付手续费
        /// </summary>
        public decimal CODPayFee
        {
            get { return _codpayfee; }
            set { _codpayfee = value; }
        }
    }
}
