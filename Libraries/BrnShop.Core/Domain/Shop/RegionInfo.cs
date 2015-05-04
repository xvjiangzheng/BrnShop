using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 区域信息类
    /// </summary>
    public class RegionInfo
    {
        private int _regionid;//区域id
        private string _name;//名称
        private string _spell;//拼写
        private string _shortspell;//简拼
        private int _displayorder;//排序
        private int _parentid;//父id
        private int _layer;//级别
        private int _provinceid;//省id
        private string _provincename;//省名称
        private int _cityid;//市id
        private string _cityname;//市名称

        /// <summary>
        /// 区域id
        /// </summary>
        public int RegionId
        {
            get { return _regionid; }
            set { _regionid = value; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value.TrimEnd(); }
        }
        /// <summary>
        /// 拼写
        /// </summary>
        public string Spell
        {
            get { return _spell; }
            set { _spell = value.TrimEnd(); }
        }
        /// <summary>
        /// 简拼
        /// </summary>
        public string ShortSpell
        {
            get { return _shortspell; }
            set { _shortspell = value.TrimEnd(); }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder
        {
            get { return _displayorder; }
            set { _displayorder = value; }
        }
        /// <summary>
        /// 父id
        /// </summary>
        public int ParentId
        {
            get { return _parentid; }
            set { _parentid = value; }
        }
        /// <summary>
        /// 级别
        /// </summary>
        public int Layer
        {
            get { return _layer; }
            set { _layer = value; }
        }
        /// <summary>
        /// 省id
        /// </summary>
        public int ProvinceId
        {
            get { return _provinceid; }
            set { _provinceid = value; }
        }
        /// <summary>
        /// 省名称
        /// </summary>
        public string ProvinceName
        {
            get { return _provincename; }
            set { _provincename = value.TrimEnd(); }
        }
        /// <summary>
        /// 市id
        /// </summary>
        public int CityId
        {
            get { return _cityid; }
            set { _cityid = value; }
        }
        /// <summary>
        /// 市名称
        /// </summary>
        public string CityName
        {
            get { return _cityname; }
            set { _cityname = value.TrimEnd(); }
        }
    }
}
