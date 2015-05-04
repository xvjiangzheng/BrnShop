using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 更新商品统计要使用的信息类
    /// </summary>
    [Serializable]
    public class UpdateProductStatState
    {
        private int _pid;//商品id
        private int _regionid;//区域id
        private DateTime _time;//时间

        public UpdateProductStatState(int pid, int regionId, DateTime time)
        {
            _pid = pid;
            _regionid = regionId;
            _time = time;
        }

        /// <summary>
        /// 商品id
        /// </summary>
        public int Pid
        {
            get { return _pid; }
            set { _pid = value; }
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
        /// 时间
        /// </summary>
        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }
    }
}
