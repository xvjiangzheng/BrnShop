using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 商品sku项信息类
    /// </summary>
    public class ProductSKUItemInfo
    {
        private int _recordid;//记录id
        private int _skugid;//sku组id
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
        /// sku组id
        /// </summary>
        public int SKUGid
        {
            get { return _skugid; }
            set { _skugid = value; }
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
    /// 扩展商品sku项信息类
    /// </summary>
    public class ExtProductSKUItemInfo : ProductSKUItemInfo
    {
        private string _attrvalue = "";//属性值
        private int _isinput = 0;//是否为输入值
        private string _attrname = "";//属性名称
        private int _attrshowtype;//属性展示类型(0代表文字,1代表图片)
        private string _showimg = "";//商品展示图片

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
        /// 属性展示类型(0代表文字,1代表图片)
        /// </summary>
        public int AttrShowType
        {
            get { return _attrshowtype; }
            set { _attrshowtype = value; }
        }
        /// <summary>
        /// 商品展示图片
        /// </summary>
        public string ShowImg
        {
            set { _showimg = value; }
            get { return _showimg; }
        }
    }
}
