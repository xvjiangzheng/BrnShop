using System;
using System.Text;
using System.Security.Cryptography;

namespace BrnShop.PayPlugin.Alipay
{
    /// <summary>
    /// MD5加密
    /// </summary>
    public sealed class AlipayMD5
    {
        /// <summary>
        /// 签名字符串
        /// </summary>
        /// <param name="prestr">需要签名的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="code">编码格式</param>
        /// <returns>签名结果</returns>
        public static string Sign(string prestr, string key, Encoding code)
        {
            StringBuilder sb = new StringBuilder(32);

            prestr = string.Format("{0}{1}", prestr, key);

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(code.GetBytes(prestr));
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="prestr">需要签名的字符串</param>
        /// <param name="sign">签名结果</param>
        /// <param name="key">密钥</param>
        /// <param name="code">编码格式</param>
        /// <returns>验证结果</returns>
        public static bool Verify(string prestr, string sign, string key, Encoding code)
        {
            string mysign = Sign(prestr, key, code);
            if (mysign == sign)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}