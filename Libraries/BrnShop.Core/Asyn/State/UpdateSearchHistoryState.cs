using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 更新搜索历史要使用的信息类
    /// </summary>
    [Serializable]
    public class UpdateSearchHistoryState
    {
        private int _uid;//用户id
        private string _word;//搜索词
        private DateTime _updatetime;//更新时间

        public UpdateSearchHistoryState(int uid, string word, DateTime updateTime)
        {
            _uid = uid;
            _word = word;
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
        /// 搜索词
        /// </summary>
        public string Word
        {
            get { return _word; }
            set { _word = value; }
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
