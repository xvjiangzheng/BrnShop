using System;
using System.Xml.Serialization;

namespace BrnShop.Core
{
    /// <summary>
    /// 插件信息类
    /// </summary>
    public class PluginInfo : IComparable
    {
        private string _systemname = "";//插件系统名称(必须具有唯一性)
        private string _friendlyname = "";//插件友好名称
        private string _classfullname = "";//插件控制器
        private string _folder = "";//插件目录
        private string _description = "";//插件描述
        private int _type = 0;//插件类型(0代表开放授权插件，1代表支付插件，2代表配送插件)
        private string _author = "";//插件作者
        private string _version = "";//插件版本
        private string _supversion = "";//插件支持的BrnShop版本
        private int _displayOrder = 0;//插件顺序
        private int _isdefault = 0;//是否是默认插件

        /// <summary>
        /// 插件系统名称
        /// </summary>
        public string SystemName
        {
            get { return _systemname; }
            set { _systemname = value; }
        }

        /// <summary>
        /// 插件友好名称
        /// </summary>
        public string FriendlyName
        {
            get { return _friendlyname; }
            set { _friendlyname = value; }
        }

        /// <summary>
        /// 插件类型名称
        /// </summary>
        public string ClassFullName
        {
            get { return _classfullname; }
            set { _classfullname = value; }
        }

        /// <summary>
        /// 插件目录
        /// </summary>
        public string Folder
        {
            get { return _folder; }
            set { _folder = value; }
        }

        /// <summary>
        /// 插件描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// 插件类型(0代表开放授权插件，1代表支付插件，2代表配送插件)
        /// </summary>
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// 插件作者
        /// </summary>
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        /// <summary>
        /// 插件版本
        /// </summary>
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        /// <summary>
        /// 插件支持的BrnShop版本
        /// </summary>
        public string SupVersion
        {
            get { return _supversion; }
            set { _supversion = value; }
        }

        /// <summary>
        /// 插件顺序
        /// </summary>
        public int DisplayOrder
        {
            get { return _displayOrder; }
            set { _displayOrder = value; }
        }

        /// <summary>
        /// 是否是默认插件
        /// </summary>
        public int IsDefault
        {
            get { return _isdefault; }
            set { _isdefault = value; }
        }


        /// <summary>
        /// 插件实例
        /// </summary>
        private IPlugin _instance = null;

        /// <summary>
        /// 插件实例
        /// </summary>
        [XmlIgnoreAttribute]
        public IPlugin Instance
        {
            get
            {
                if (_instance == null)
                {
                    try
                    {
                        _instance = (IPlugin)Activator.CreateInstance(System.Type.GetType(ClassFullName, false, true));
                    }
                    catch (Exception ex)
                    {
                        throw new BSPException("创建插件:" + _classfullname + "的实例失败", ex);
                    }
                }
                return _instance;
            }
        }

        public int CompareTo(object obj)
        {
            PluginInfo info = (PluginInfo)obj;

            if (this.DisplayOrder > info.DisplayOrder)
                return 1;
            else if (this.DisplayOrder < info.DisplayOrder)
                return -1;
            else
                return 0;
        }

    }
}
