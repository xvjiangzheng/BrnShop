using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 筛选词信息类
    /// </summary>
    public class FilterWordInfo
    {
        private int _id;
        private string _match;//匹配词
        private string _replace;//替换词

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 匹配词
        /// </summary>
        public string Match
        {
            get { return _match; }
            set { _match = value; }
        }
        /// <summary>
        /// 替换词
        /// </summary>
        public string Replace
        {
            get { return _replace; }
            set { _replace = value; }
        }
    }
}
