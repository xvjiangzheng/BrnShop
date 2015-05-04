using System;

using BrnShop.Core;

namespace BrnShop.Data
{
    /// <summary>
    /// 数据库数据访问类
    /// </summary>
    public partial class DataBases
    {
        /// <summary>
        /// 运行SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public static string RunSql(string sql)
        {
            return BrnShop.Core.BSPData.RDBS.RunSql(sql);
        }
    }
}
