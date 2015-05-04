using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 商品属性信息类
    /// </summary>
    public class ProductAttributeInfo
    {
        private int _recordid;//记录id
        private int _pid;//商品id
        private int _attrid;//属性id
        private int _attrvalueid;//属性值id
        private string _inputvalue;//输入值

        /// <summary>
        /// 记录id
        /// </summary>
        public int RecordId
        {
            get { return _recordid; }
            set { _recordid = value; }
        }
        /// <summary>
        /// 商品id
        /// </summary>
        public int Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }
        /// <summary>
        /// 属性id
        /// </summary>
        public int AttrId
        {
            get { return _attrid; }
            set { _attrid = value; }
        }
        /// <summary>
        /// 属性值id
        /// </summary>
        public int AttrValueId
        {
            get { return _attrvalueid; }
            set { _attrvalueid = value; }
        }
        /// <summary>
        /// 输入值
        /// </summary>
        public string InputValue
        {
            get { return _inputvalue; }
            set { _inputvalue = value; }
        }
    }

    /// <summary>
    /// 扩展商品属性信息类
    /// </summary>
    public class ExtProductAttributeInfo : ProductAttributeInfo
    {
        private string _attrvalue = "";//属性值
        private int _isinput = 0;//是否为输入值
        private string _attrname = "";//属性名称
        private int _attrgroupid;//属性分组id
        private string _attrgroupname = "";//属性分组名称

        /// <summary>
        /// 属性值
        /// </summary>
        public string AttrValue
        {
            get { return _attrvalue; }
            set { _attrvalue = value.TrimEnd(); }
        }
        /// <summary>
        /// 是否为输入值
        /// </summary>
        public int IsInput
        {
            get { return _isinput; }
            set { _isinput = value; }
        }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string AttrName
        {
            get { return _attrname; }
            set { _attrname = value.TrimEnd(); }
        }
        /// <summary>
        /// 属性分组id
        /// </summary>
        public int AttrGroupId
        {
            get { return _attrgroupid; }
            set { _attrgroupid = value; }
        }
        /// <summary>
        /// 属性分组名称
        /// </summary>
        public string AttrGroupName
        {
            get { return _attrgroupname; }
            set { _attrgroupname = value.TrimEnd(); }
        }
    }
}
