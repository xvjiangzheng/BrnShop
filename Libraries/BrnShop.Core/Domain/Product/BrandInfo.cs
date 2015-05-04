using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 品牌信息类
    /// </summary>
    public class BrandInfo
    {
        private int _brandid;//品牌id
        private int _displayorder = 0;//品牌排序
        private string _name = "";//品牌名称
        private string _logo = "";//品牌logo

        /// <summary>
        /// 品牌id
        /// </summary>
        public int BrandId
        {
            set { _brandid = value; }
            get { return _brandid; }
        }
        /// <summary>
        /// 品牌排序
        /// </summary>
        public int DisplayOrder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string Name
        {
            set { _name = value.TrimEnd(); }
            get { return _name; }
        }
        /// <summary>
        /// 品牌logo
        /// </summary>
        public string Logo
        {
            set { _logo = value.TrimEnd(); }
            get { return _logo; }
        }
    }
}

