using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 新闻类型信息类
    /// </summary>
    public class NewsTypeInfo
    {
        private int _newstypeid;//新闻类型id
        private string _name;//名称
        private int _displayorder = 0;//排序

        /// <summary>
        /// 新闻类型id
        /// </summary>
        public int NewsTypeId
        {
            set { _newstypeid = value; }
            get { return _newstypeid; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            set { _name = value.TrimEnd(); }
            get { return _name; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }
    }
}
