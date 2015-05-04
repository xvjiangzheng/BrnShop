using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 定时商品信息类
    /// </summary>
    public class TimeProductInfo
    {
        private int _recordid;//记录id
        private int _pid;//商品id
        private int _onsalestate;//上架状态(0代表无需上架,1代表等待执行上架,2代表已经执行上架)
        private int _outsalestate;//下架状态(0代表无需下架,1代表等待执行下架,2代表已经执行下架)
        private DateTime _onsaletime;//上架时间
        private DateTime _outsaletime;//下架时间

        /// <summary>
        /// 记录id
        /// </summary>
        public int RecordId
        {
            set { _recordid = value; }
            get { return _recordid; }
        }
        /// <summary>
        /// 商品id
        /// </summary>
        public int Pid
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 上架状态(0代表无需上架,1代表等待执行上架,2代表已经执行上架)
        /// </summary>
        public int OnSaleState
        {
            set { _onsalestate = value; }
            get { return _onsalestate; }
        }
        /// <summary>
        /// 下架状态(0代表无需下架,1代表等待执行下架,2代表已经执行下架)
        /// </summary>
        public int OutSaleState
        {
            set { _outsalestate = value; }
            get { return _outsalestate; }
        }
        /// <summary>
        /// 上架时间
        /// </summary>
        public DateTime OnSaleTime
        {
            set { _onsaletime = value; }
            get { return _onsaletime; }
        }
        /// <summary>
        /// 下架时间
        /// </summary>
        public DateTime OutSaleTime
        {
            set { _outsaletime = value; }
            get { return _outsaletime; }
        }
    }
}
