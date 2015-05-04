using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 分类信息类
    /// </summary>
	public class CategoryInfo
	{
		private int _cateid;//分类id
        private int _displayorder = 0;//分类排序
        private string _name = "";//分类名称
        private string _pricerange = "";//分类价格范围
        private int _parentid = 0;//父id
        private int _layer = 0;//层级
        private int _haschild = 0;//是否有子节点
        private string _path;//分类路径

		/// <summary>
        /// 分类id
		/// </summary>
        public int CateId
		{
            set { _cateid = value; }
            get { return _cateid; }
		}
        /// <summary>
        /// 分类排序
        /// </summary>
        public int DisplayOrder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }
		/// <summary>
        /// 分类名称
		/// </summary>
		public string Name
		{
            set { _name = value.TrimEnd(); }
            get { return _name; }
		}
        /// <summary>
        /// 分类价格范围
        /// </summary>
        public string PriceRange
        {
            set { _pricerange = value.TrimEnd(); }
            get { return _pricerange; }
        }
        /// <summary>
        /// 父id
        /// </summary>
        public int ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 层级
        /// </summary>
        public int Layer
        {
            set { _layer = value; }
            get { return _layer; }
        }
        /// <summary>
        /// 是否有子节点
        /// </summary>
        public int HasChild
        {
            set { _haschild = value; }
            get { return _haschild; }
        }
        /// <summary>
        /// 分类路径
        /// </summary>
        public string Path
        {
            set { _path = value.TrimEnd(); }
            get { return _path; }
        }
	}
}

