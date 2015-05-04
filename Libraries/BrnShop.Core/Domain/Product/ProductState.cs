using System;

namespace BrnShop.Core
{
    /// <summary>
    /// 商品状态枚举
    /// </summary>
    public enum ProductState
    {
        /// <summary>
        /// 上架
        /// </summary>
        OnSale = 0,
        /// <summary>
        /// 下架
        /// </summary>
        OutSale = 1,
        /// <summary>
        /// 回收站
        /// </summary>
        RecycleBin = 2
    }
}
