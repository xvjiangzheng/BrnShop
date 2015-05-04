using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 更新浏览历史要使用的信息类
    /// </summary>
    [Serializable]
    public class UpdateBrowseHistoryState
    {
        private int _uid;//用户id
        private int _pid;//商品id
        private DateTime _updatetime;//更新时间

        public UpdateBrowseHistoryState(int uid, int pid, DateTime updateTime)
        {
            _uid = uid;
            _pid = pid;
            _updatetime = updateTime;
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
        /// 商品id
        /// </summary>
        public int Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            get { return _updatetime; }
            set { _updatetime = value; }
        }
    }
}
