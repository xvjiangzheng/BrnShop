using System;

namespace BrnShop.Core
{
    /// <summary>
    /// PV统计信息类
    /// </summary>
    public class PVStatInfo
    {
        private string _category;//类别
        private string _value;//值
        private int _count;//数量

        /// <summary>
        /// 类别
        /// </summary>
        public string Category
        {
            get { return _category; }
            set { _category = value.TrimEnd(); }
        }
        /// <summary>
        /// 值
        /// </summary>
        public string Value
        {
            get { return _value; }
            set { _value = value.TrimEnd(); }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }
    }
}
