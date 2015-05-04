using System;
using System.IO;

namespace BrnShop.Core
{
    /// <summary>
    /// BrnShop会话状态管理类
    /// </summary>
    public class BSPSession
    {
        private static ISessionStrategy _isessionstrategy = null;//会话状态策略

        static BSPSession()
        {
            try
            {
                string[] fileNameList = Directory.GetFiles(System.Web.HttpRuntime.BinDirectory, "BrnShop.SessionStrategy.*.dll", SearchOption.TopDirectoryOnly);
                _isessionstrategy = (ISessionStrategy)Activator.CreateInstance(Type.GetType(string.Format("BrnShop.SessionStrategy.{0}.SessionStrategy, BrnShop.SessionStrategy.{0}", fileNameList[0].Substring(fileNameList[0].IndexOf("SessionStrategy.") + 16).Replace(".dll", "")),
                                                                                           false,
                                                                                           true));
            }
            catch
            {
                throw new BSPException("创建'会话状态策略对象'失败,可能存在的原因:未将'会话状态策略程序集'添加到bin目录中;'会话状态策略程序集'文件名不符合'BrnShop.SessionStrategy.{策略名称}.dll'格式");
            }
        }

        /// <summary>
        /// 会话状态策略实例
        /// </summary>
        public static ISessionStrategy Instance
        {
            get { return _isessionstrategy; }
        }
    }
}
