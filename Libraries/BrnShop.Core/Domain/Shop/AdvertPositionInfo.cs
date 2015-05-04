using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 广告位置信息类
    /// </summary>
    public class AdvertPositionInfo
    {
        private int _adposid;//广告位置id
        private string _title;//广告位置标题
        private string _description;//广告位置描述

        /// <summary>
        /// 广告位置id
        /// </summary>
        public int AdPosId
        {
            get { return _adposid; }
            set { _adposid = value; }
        }
        /// <summary>
        /// 广告位置标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value.TrimEnd(); }
        }
        /// <summary>
        /// 广告位置描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value.TrimEnd(); }
        }
    }
}
