using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 属性分组信息类
    /// </summary>
    public class AttributeGroupInfo
    {
        private int _attrgroupid;//属性分组id
        private int _cateid;//分类id
        private string _name = "";//分组名称
        private int _displayorder;//分组排序

        /// <summary>
        /// 属性分组id
        /// </summary>
        public int AttrGroupId
        {
            set { _attrgroupid = value; }
            get { return _attrgroupid; }
        }

        /// <summary>
        /// 分类id
        /// </summary>
        public int CateId
        {
            set { _cateid = value; }
            get { return _cateid; }
        }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string Name
        {
            set { _name = value.TrimEnd(); }
            get { return _name; }
        }
        /// <summary>
        /// 分组排序
        /// </summary>
        public int DisplayOrder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }
    }
}
