using System;
using System.IO;

namespace BrnShop.Core
{
    /// <summary>
    /// BrnShop购物车管理类
    /// </summary>
    public class BSPCart
    {
        private static ICartStrategy _icartstrategy = null;//购物车策略

        static BSPCart()
        {
            try
            {
                string[] fileNameList = Directory.GetFiles(System.Web.HttpRuntime.BinDirectory, "BrnShop.CartStrategy.*.dll", SearchOption.TopDirectoryOnly);
                _icartstrategy = (ICartStrategy)Activator.CreateInstance(Type.GetType(string.Format("BrnShop.CartStrategy.{0}.CartStrategy, BrnShop.CartStrategy.{0}", fileNameList[0].Substring(fileNameList[0].IndexOf("CartStrategy.") + 13).Replace(".dll", "")),
                                                                                      false,
                                                                                      true));
            }
            catch
            {
                throw new BSPException("创建'购物车策略对象'失败,可能存在的原因:未将'购物车策略程序集'添加到bin目录中;'购物车策略程序集'文件名不符合'BrnShop.CartStrategy.{策略名称}.dll'格式");
            }
        }

        /// <summary>
        /// 购物车策略实例
        /// </summary>
        public static ICartStrategy Instance
        {
            get { return _icartstrategy; }
        }
    }
}
