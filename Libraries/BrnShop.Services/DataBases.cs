using System;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 数据库操作管理类
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
            return BrnShop.Data.DataBases.RunSql(sql);
        }

        /// <summary>
        /// 获得数据库名称
        /// </summary>
        /// <returns></returns>
        public static string GetDBName()
        {
            string[] itemList = StringHelper.SplitString(RDBSHelper.ConnectionString, ";");
            foreach (string item in itemList)
            {
                if (item.ToLower().Contains("initial catalog") || item.ToLower().Contains("database"))
                    return StringHelper.SplitString(item, "=")[1].Trim();
            }
            return "";
        }
    }
}
