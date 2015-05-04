using System;
using System.Data.Common;

namespace BrnShop.Core
{
    /// <summary>
    /// BrnShop关系数据库策略之数据库分部接口
    /// </summary>
    public partial interface IRDBSStrategy
    {
        /// <summary>
        /// 获得数据库提供程序工厂
        /// </summary>
        DbProviderFactory GetDbProviderFactory();

        /// <summary>
        /// 运行SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        string RunSql(string sql);
    }
}
