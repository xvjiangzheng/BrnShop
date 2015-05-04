using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 更新PV统计要使用的信息类
    /// </summary>
    [Serializable]
    public class UpdatePVStatState
    {
        private bool _ismember;//是否为会员
        private int _regionid;//区域id
        private string _browser;//浏览器
        private string _os;//操作系统

        public UpdatePVStatState(bool isMember, int regionId, string browser, string os)
        {
            _ismember = isMember;
            _regionid = regionId;
            _browser = browser;
            _os = os;
        }

        /// <summary>
        /// 是否为会员
        /// </summary>
        public bool IsMember
        {
            get { return _ismember; }
            set { _ismember = value; }
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
        /// 浏览器
        /// </summary>
        public string Browser
        {
            get { return _browser; }
            set { _browser = value; }
        }
        /// <summary>
        /// 操作系统
        /// </summary>
        public string OS
        {
            get { return _os; }
            set { _os = value; }
        }
    }
}
