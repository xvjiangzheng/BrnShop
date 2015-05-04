using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using BrnShop.Core;

namespace BrnShop.RDBSStrategy.SqlServer
{
    /// <summary>
    /// SqlServer策略之关系数据库分部类
    /// </summary>
    public partial class RDBSStrategy : IRDBSStrategy
    {
        #region  辅助方法

        /// <summary>
        /// 生成输入参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="sqlDbType">参数类型</param>
        /// <param name="size">类型大小</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        private DbParameter GenerateInParam(string paramName, SqlDbType sqlDbType, int size, object value)
        {
            return GenerateParam(paramName, sqlDbType, size, ParameterDirection.Input, value);
        }

        /// <summary>
        /// 生成输出参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="sqlDbType">参数类型</param>
        /// <param name="size">类型大小</param>
        /// <returns></returns>
        private DbParameter GenerateOutParam(string paramName, SqlDbType sqlDbType, int size)
        {
            return GenerateParam(paramName, sqlDbType, size, ParameterDirection.Output, null);
        }

        /// <summary>
        /// 生成返回参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="sqlDbType">参数类型</param>
        /// <param name="size">类型大小</param>
        /// <returns></returns>
        private DbParameter GenerateReturnParam(string paramName, SqlDbType sqlDbType, int size)
        {
            return GenerateParam(paramName, sqlDbType, size, ParameterDirection.ReturnValue, null);
        }

        /// <summary>
        /// 生成参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="sqlDbType">参数类型</param>
        /// <param name="size">类型大小</param>
        /// <param name="direction">方向</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        private DbParameter GenerateParam(string paramName, SqlDbType sqlDbType, int size, ParameterDirection direction, object value)
        {
            SqlParameter param = new SqlParameter(paramName, sqlDbType, size);
            param.Direction = direction;
            if (direction == ParameterDirection.Input && value != null)
                param.Value = value;
            return param;
        }

        #endregion

        /// <summary>
        /// 获得数据库提供程序工厂
        /// </summary>
        public DbProviderFactory GetDbProviderFactory()
        {
            return SqlClientFactory.Instance;
        }

        /// <summary>
        /// 运行SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public string RunSql(string sql)
        {
            if (!string.IsNullOrWhiteSpace(sql))
            {
                SqlConnection conn = new SqlConnection(RDBSHelper.ConnectionString);
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    string[] sqlList = StringHelper.SplitString(sql, "-sqlseparator-");
                    foreach (string item in sqlList)
                    {
                        if (!string.IsNullOrWhiteSpace(item))
                        {
                            try
                            {
                                RDBSHelper.ExecuteNonQuery(CommandType.Text, item);
                                trans.Commit();
                            }
                            catch (Exception ex)
                            {
                                trans.Rollback();
                                return ex.Message;
                            }
                        }
                    }
                }
                conn.Close();
            }
            return string.Empty;
        }
    }
}
