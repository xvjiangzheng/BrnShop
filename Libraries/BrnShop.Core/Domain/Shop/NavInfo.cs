using System;

namespace BrnShop.Core
{
    /// <summary>
    ///  导航栏信息类
    /// </summary>
    public class NavInfo
    {
        private int _id;//编号
        private int _pid;//父编号
        private int _layer;//级别
        private string _name;//名称
        private string _title;//标题
        private string _url;//网址
        private int _target;//打开目标
        private int _displayorder;//排序

        /// <summary>
        /// 编号
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 父编号
        /// </summary>
        public int Pid
        {
            get { return _pid; }
            set { _pid = value; }
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
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value.TrimEnd(); }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value.TrimEnd(); }
        }
        /// <summary>
        /// 网址
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value.TrimEnd(); }
        }
        /// <summary>
        /// 打开目标
        /// </summary>
        public int Target
        {
            get { return _target; }
            set { _target = value; }
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
