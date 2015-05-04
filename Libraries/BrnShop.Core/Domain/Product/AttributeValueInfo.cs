using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 属性值信息类
    /// </summary>
    public class AttributeValueInfo
    {
        private int _attrvalueid = 0;//属性值id
        private string _attrvalue = "";//属性值
        private int _isinput = 0;//是否为输入值
        private string _attrname = "";//属性名称
        private int _attrdisplayorder = 0;//属性排序
        private int _attrshowtype;//属性展示类型(0代表文字,1代表图片)
        private int _attrvaluedisplayorder = 0;//属性值排序
        private int _attrgroupid;//属性分组id
        private string _attrgroupname = "";//属性分组名称
        private int _attrgroupdisplayorder;///属性分组排序
        private int _attrid = 0;//属性id

        /// <summary>
        /// 属性值id
        /// </summary>
        public int AttrValueId
        {
            get { return _attrvalueid; }

            set { _attrvalueid = value; }
        }
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
        /// 属性排序
        /// </summary>
        public int AttrDisplayOrder
        {
            get { return _attrdisplayorder; }
            set { _attrdisplayorder = value; }
        }
        /// <summary>
        /// 属性展示类型(0代表文字,1代表图片)
        /// </summary>
        public int AttrShowType
        {
            get { return _attrshowtype; }
            set { _attrshowtype = value; }
        }
        /// <summary>
        /// 属性值排序
        /// </summary>
        public int AttrValueDisplayOrder
        {
            get { return _attrvaluedisplayorder; }
            set { _attrvaluedisplayorder = value; }
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
        /// <summary>
        /// 属性分组排序
        /// </summary>
        public int AttrGroupDisplayOrder
        {
            get { return _attrgroupdisplayorder; }
            set { _attrgroupdisplayorder = value; }
        }
        /// <summary>
        /// 属性id
        /// </summary>
        public int AttrId
        {
            get { return _attrid; }
            set { _attrid = value; }
        }
    }
}
