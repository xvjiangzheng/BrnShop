using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 用户配送地址信息类
    /// </summary>
    public class ShipAddressInfo
    {
        private int _said;//配送地址id
        private int _uid;//用户id
        private int _regionid;//区域id
        private int _isdefault;//是否是默认地址
        private string _alias;//别名
        private string _consignee;//收货人
        private string _mobile;//收货人手机
        private string _phone;//收货人固定电话
        private string _email;//收货人邮箱
        private string _zipcode;//邮政编码
        private string _address;//地址

        /// <summary>
        /// 配送地址id
        /// </summary>
        public int SAId
        {
            get { return _said; }
            set { _said = value; }
        }
        /// <summary>
        /// 用户id
        /// </summary>
        public int Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }
        /// <summary>
        /// 区域id
        /// </summary>
        public int RegionId
        {
            get { return _regionid; }
            set { _regionid = value; }
        }
        /// <summary>
        /// 是否是默认地址
        /// </summary>
        public int IsDefault
        {
            get { return _isdefault; }
            set { _isdefault = value; }
        }
        /// <summary>
        /// 别名
        /// </summary>
        public string Alias
        {
            get { return _alias; }
            set { _alias = value; }
        }
        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee
        {
            get { return _consignee; }
            set { _consignee = value; }
        }
        /// <summary>
        /// 收货人手机
        /// </summary>
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }
        /// <summary>
        /// 收货人固定电话
        /// </summary>
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        /// <summary>
        /// 收货人邮箱
        /// </summary>
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string ZipCode
        {
            get { return _zipcode; }
            set { _zipcode = value.TrimEnd(); }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
    }


    /// <summary>
    /// 完整用户配送地址信息类
    /// </summary>
    public class FullShipAddressInfo : ShipAddressInfo
    {
        private int _provinceid;//省id
        private string _provincename;//省名称
        private int _cityid;//市id
        private string _cityname;//市名称
        private int _countyid;//县或区id
        private string _countyname;//县或区名称


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
        /// <summary>
        /// 县或区id
        /// </summary>
        public int CountyId
        {
            get { return _countyid; }
            set { _countyid = value; }
        }
        /// <summary>
        /// 县或区名称
        /// </summary>
        public string CountyName
        {
            get { return _countyname; }
            set { _countyname = value.TrimEnd(); }
        }
    }

}
