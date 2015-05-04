using System;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// banner操作管理类
    /// </summary>
    public partial class Banners
    {
        /// <summary>
        /// 获得首页banner列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static BannerInfo[] GetHomeBannerList(int type)
        {
            BannerInfo[] bannerList = BrnShop.Core.BSPCache.Get(CacheKeys.SHOP_BANNER_HOMELIST + type) as BannerInfo[];
            if (bannerList == null)
            {
                bannerList = BrnShop.Data.Banners.GetHomeBannerList(type, DateTime.Now);
                BrnShop.Core.BSPCache.Insert(CacheKeys.SHOP_BANNER_HOMELIST + type, bannerList);
            }
            return bannerList;
        }
    }
}
