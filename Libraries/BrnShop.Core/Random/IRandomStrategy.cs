using System;
using System.Drawing;

namespace BrnShop.Core
{
    /// <summary>
    /// 随机性策略接口
    /// </summary>
    public partial interface IRandomStrategy
    {
        /// <summary>
        /// 随机库
        /// </summary>
        char[] RandomLibrary { get; set; }

        /// <summary>
        /// 创建随机值
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="onlyNumber">是否只包含数字</param>
        /// <returns>随机值</returns>
        string CreateRandomValue(int length, bool onlyNumber);

        /// <summary>
        /// 创建随机对
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="onlyNumber">是否只包含数字</param>
        /// <param name="randomKey">随机键</param>
        /// <param name="randomValue">随机值</param>
        void CreateRandomPair(int length, bool onlyNumber, out string randomKey, out string randomValue);

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
        RandomImage CreateRandomImage(string value, int imageWidth, int imageHeight, Color imageBGColor, Color imageTextColor1, Color imageTextColor2);
    }
}
