using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using BrnShop.Core;

namespace BrnShop.RandomStrategy.BrnShop
{
    /// <summary>
    /// BrnShop团队实现的随机性策略
    /// </summary>
    public partial class RandomStrategy : IRandomStrategy
    {
        private Random _random = new Random();//随机发生器
        private char[] _randomlibrary = new char[0];//随机库

        /// <summary>
        /// 随机库
        /// </summary>
        public char[] RandomLibrary
        {
            get { return _randomlibrary; }
            set { _randomlibrary = value; }
        }

        /// <summary>
        /// 创建随机值
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="onlyNumber">是否只包含数字</param>
        /// <returns>随机值</returns>
        public string CreateRandomValue(int length, bool onlyNumber)
        {
            int index;
            StringBuilder randomValue = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                if (onlyNumber)
                    index = _random.Next(0, 9);
                else
                    index = _random.Next(0, _randomlibrary.Length);

                randomValue.Append(_randomlibrary[index]);
            }

            return randomValue.ToString();
        }

        /// <summary>
        /// 创建随机对
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="onlyNumber">是否只包含数字</param>
        /// <param name="randomKey">随机键</param>
        /// <param name="randomValue">随机值</param>
        public void CreateRandomPair(int length, bool onlyNumber, out string randomKey, out string randomValue)
        {
            StringBuilder randomKeySB = new StringBuilder();
            StringBuilder randomValueSB = new StringBuilder();

            int index1;
            int index2;
            for (int i = 0; i < length; i++)
            {
                if (onlyNumber)
                {
                    index1 = _random.Next(0, 10);
                    index2 = _random.Next(0, 10);
                }
                else
                {
                    index1 = _random.Next(0, _randomlibrary.Length);
                    index2 = _random.Next(0, _randomlibrary.Length);
                }

                randomKeySB.Append(_randomlibrary[index1]);
                randomValueSB.Append(_randomlibrary[index2]);
            }
            randomKey = randomKeySB.ToString();
            randomValue = randomValueSB.ToString();
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
        public RandomImage CreateRandomImage(string value, int imageWidth, int imageHeight, Color imageBGColor, Color imageTextColor1, Color imageTextColor2)
        {
            Bitmap image = new Bitmap(imageWidth, imageHeight);
            Graphics g = Graphics.FromImage(image);
            //保存图片数据
            MemoryStream stream = new MemoryStream();
            try
            {
                //生成随机生成器 
                //Random random = new Random();

                //清空图片背景色 
                g.Clear(imageBGColor);

                //画图片的背景噪音线 
                for (int i = 0; i < 5; i++)
                {
                    int x1 = _random.Next(image.Width);
                    int x2 = _random.Next(image.Width);
                    int y1 = _random.Next(image.Height);
                    int y2 = _random.Next(image.Height);

                    g.DrawLine(new Pen(Color.FromArgb(_random.Next(255), _random.Next(255), _random.Next(255))), x1, y1, x2, y2);
                }

                Font font = new Font("Arial", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                                                                    imageTextColor1,
                                                                    imageTextColor2,
                                                                    1.2f,
                                                                    true);
                g.DrawString(value, font, brush, 2, 2);

                //画图片的前景噪音点 
                for (int i = 0; i < 80; i++)
                {
                    int x = _random.Next(image.Width);
                    int y = _random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(_random.Next()));
                }

                //画图片的边框线 
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                RandomImage verifyImage = new RandomImage();
                image.Save(stream, ImageFormat.Jpeg);
                verifyImage.Image = stream.ToArray();
                verifyImage.ContentType = "image/jpeg";

                return verifyImage;
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
                if (g != null)
                    g.Dispose();
                if (image != null)
                    image.Dispose();
            }
        }
    }
}
