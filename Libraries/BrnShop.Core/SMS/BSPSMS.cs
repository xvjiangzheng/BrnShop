using System;
using System.IO;

namespace BrnShop.Core
{
    /// <summary>
    /// BrnShop短信管理类
    /// </summary>
    public class BSPSMS
    {
        private static ISMSStrategy _ismsstrategy = null;//短信策略

        static BSPSMS()
        {
            try
            {
                string[] fileNameList = Directory.GetFiles(System.Web.HttpRuntime.BinDirectory, "BrnShop.SMSStrategy.*.dll", SearchOption.TopDirectoryOnly);
                _ismsstrategy = (ISMSStrategy)Activator.CreateInstance(Type.GetType(string.Format("BrnShop.SMSStrategy.{0}.SMSStrategy, BrnShop.SMSStrategy.{0}", fileNameList[0].Substring(fileNameList[0].IndexOf("SMSStrategy.") + 12).Replace(".dll", "")),
                                                                                   false,
                                                                                   true));
            }
            catch
            {
                throw new BSPException("创建'短信策略对象'失败,可能存在的原因:未将'短信策略程序集'添加到bin目录中;'短信策略程序集'文件名不符合'BrnShop.SMSStrategy.{策略名称}.dll'格式");
            }
        }

        /// <summary>
        /// 短信策略实例
        /// </summary>
        public static ISMSStrategy Instance
        {
            get { return _ismsstrategy; }
        }
    }
}
