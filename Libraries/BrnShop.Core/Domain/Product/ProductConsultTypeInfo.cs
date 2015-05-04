using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 商品咨询类型信息类
    /// </summary>
    public class ProductConsultTypeInfo
    {
        private int _consulttypeid;//咨询类型id
        private string _title;//咨询类型标题
        private int _displayorder;//排序

        /// <summary>
        /// 咨询类型id
        /// </summary>
        public int ConsultTypeId
        {
            get { return _consulttypeid; }
            set { _consulttypeid = value; }
        }
        /// <summary>
        /// 咨询类型标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value.TrimEnd(); }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder
        {
            get { return _displayorder; }
            set { _displayorder = value; }
        }
    }
}
