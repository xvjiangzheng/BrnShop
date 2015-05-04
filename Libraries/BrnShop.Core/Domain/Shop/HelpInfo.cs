using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 帮助信息类
    /// </summary>
    public class HelpInfo
    {
        private int _id;//编号
        private int _pid = 0;//父编号
        private string _title = "";//标题
        private string _url = "";//网址
        private string _description = "";//描述
        private int _displayorder = 0;//排序
        /// <summary>
        /// 编号
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 父编号
        /// </summary>
        public int Pid
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            set { _title = value.TrimEnd(); }
            get { return _title; }
        }
        /// <summary>
        /// 网址
        /// </summary>
        public string Url
        {
            set { _url = value.TrimEnd(); }
            get { return _url; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }
    }
}

