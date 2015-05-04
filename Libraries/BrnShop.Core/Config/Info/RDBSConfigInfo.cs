using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 关系型数据库配置信息
    /// </summary>
    [Serializable]
    public class RDBSConfigInfo : IConfigInfo
    {
        private string _rdbsconnectstring;//关系数据库连接字符串
        private string _rdbstablepre;//关系数据库对象前缀

        /// <summary>
        /// 关系数据库连接字符串
        /// </summary>
        public string RDBSConnectString
        {
            get { return _rdbsconnectstring; }
            set { _rdbsconnectstring = value; }
        }

        /// <summary>
        /// 关系数据库对象前缀
        /// </summary>
        public string RDBSTablePre
        {
            get { return _rdbstablepre; }
            set { _rdbstablepre = value; }
        }

    }
}
