using System;
using System.Drawing;

using BrnShop.Core;

namespace BrnShop.Services
{
    /// <summary>
    /// 随机性操作管理类
    /// </summary>
    public partial class Randoms
    {
        private static object _locker = new object();//锁对象
        private static IRandomStrategy _irandomstrategy = null;//随机性策略

        static Randoms()
        {
            _irandomstrategy = BSPRandom.Instance;
            if (!string.IsNullOrWhiteSpace(BSPConfig.ShopConfig.RandomLibrary))
            {
                _irandomstrategy.RandomLibrary = BSPConfig.ShopConfig.RandomLibrary.ToCharArray();
            }
        }

        /// <summary>
        /// 重置随机库
        /// </summary>
        public static void ResetRandomLibrary()
        {
            lock (_locker)
            {
                if (!string.IsNullOrWhiteSpace(BSPConfig.ShopConfig.RandomLibrary))
                {
                    _irandomstrategy.RandomLibrary = BSPConfig.ShopConfig.RandomLibrary.ToCharArray();
                }
            }
        }

        /// <summary>
        /// 创建随机值
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="onlyNumber">是否只包含数字</param>
        /// <returns>随机值</returns>
        public static string CreateRandomValue(int length, bool onlyNumber)
        {
            return _irandomstrategy.CreateRandomValue(length, onlyNumber);
        }

        /// <summary>
        /// 创建数字随机值
        /// </summary>
        /// <param name="length">随机值长度</param>
        /// <returns>随机值</returns>
        public static string CreateRandomValue(int length)
        {
            return CreateRandomValue(length, true);
        }

        /// <summary>
        /// 创建随机对
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="onlyNumber">是否只包含数字</param>
        /// <param name="randomKey">随机键</param>
        /// <param name="randomValue">随机值</param>
        public static void CreateRandomPair(int length, bool onlyNumber, out string randomKey, out string randomValue)
        {
            _irandomstrategy.CreateRandomPair(length, onlyNumber, out randomKey, out randomValue);
        }

        /// <summary>
        /// 创建只包含数字的随机对
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="randomKey">随机键</param>
        /// <param name="randomValue">随机值</param>
        public static void CreateRandomPair(int length, out string randomKey, out string randomValue)
        {
            CreateRandomPair(length, true, out randomKey, out randomValue);
        }

        /// <summary>
        /// 创建随机图片
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="imageWidth">图片宽度</param>
        /// <param name="imageHeight">图片高度</param>
        /// <param name="imageBGColor">图片背景颜色</param>
        /// <param name="imageTextColor1">图片文字颜色</param>
        /// <param name="imageTextColor2">图片文字颜色</param>
        /// <returns>随机图片</returns>
        public static RandomImage CreateRandomImage(string value, int imageWidth, int imageHeight, Color imageBGColor, Color imageTextColor1, Color imageTextColor2)
        {
            return _irandomstrategy.CreateRandomImage(value, imageWidth, imageHeight, imageBGColor, imageTextColor1, imageTextColor2);
        }
    }
}
