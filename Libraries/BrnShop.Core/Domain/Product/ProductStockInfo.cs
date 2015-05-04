using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 商品库存信息类
    /// </summary>
    public class ProductStockInfo
    {
        private int _pid;//商品id
        private int _number;//商品数量
        private int _limit;//商品库存警戒线

        /// <summary>
        /// 商品id
        /// </summary>
        public int Pid
        {
            get { return _pid; }
            set { _pid = value; }
        }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }
        /// <summary>
        /// 商品库存警戒线
        /// </summary>
        public int Limit
        {
            get { return _limit; }
            set { _limit = value; }
        }
    }
}
