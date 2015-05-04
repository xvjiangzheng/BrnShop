using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 用户等级信息类
    /// </summary>
    public class UserRankInfo
    {
        private int _userrid;//用户等级id
        private int _system;//是否是系统等级
        private string _title;//用户等级标题
        private string _avatar;//用户等级头像
        private int _creditsupper;//用户等级积分上限
        private int _creditslower;//用户等级积分下限
        private int _limitdays;//限制天数

        ///<summary>
        ///用户等级id
        ///</summary>
        public int UserRid
        {
            get { return _userrid; }
            set { _userrid = value; }
        }
        ///<summary>
        ///是否是系统等级
        ///</summary>
        public int System
        {
            get { return _system; }
            set { _system = value; }
        }
        ///<summary>
        ///用户等级标题
        ///</summary>
        public string Title
        {
            get { return _title; }
            set { _title = value.TrimEnd(); }
        }
        /// <summary>
        /// 用户等级头像
        /// </summary>
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value.TrimEnd(); }
        }
        ///<summary>
        ///用户等级积分上限
        ///</summary>
        public int CreditsUpper
        {
            get { return _creditsupper; }
            set { _creditsupper = value; }
        }
        ///<summary>
        ///用户等级积分下限
        ///</summary>
        public int CreditsLower
        {
            get { return _creditslower; }
            set { _creditslower = value; }
        }
        /// <summary>
        /// 限制天数
        /// </summary>
        public int LimitDays
        {
            get { return _limitdays; }
            set { _limitdays = value; }
        }
    }
}
