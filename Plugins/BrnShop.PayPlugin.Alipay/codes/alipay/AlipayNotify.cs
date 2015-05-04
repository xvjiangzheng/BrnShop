using System;
using System.IO;
using System.Web;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace BrnShop.PayPlugin.Alipay
{
    /// <summary>
    /// 支付宝通知处理类,处理支付宝各接口通知返回
    /// 调试通知返回时，可查看或改写log日志的写入TXT里的数据，来检查通知返回是否正常 
    /// </summary>
    public class AlipayNotify
    {
        /// <summary>
        ///  验证消息是否是支付宝发出的合法消息
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="notifyId">通知验证ID</param>
        /// <param name="sign">支付宝生成的签名结果</param>
        /// <param name="signType">签名方式</param>
        /// <param name="key">交易安全校验码</param>
        /// <param name="code">字符编码格式</param>
        /// <returns>验证结果</returns>
        public static bool Verify(SortedDictionary<string, string> inputPara, string notifyId, string sign, string signType, string key, Encoding code, string veryfyUrl, string partner)
        {
            //获取返回时的签名验证结果
            bool isSign = GetSignVeryfy(inputPara, sign, signType, key, code);
            //获取是否是支付宝服务器发来的请求的验证结果
            string responseTxt = "true";
            if (notifyId != null && notifyId != "") { responseTxt = GetResponseTxt(notifyId, veryfyUrl, partner); }

            //写日志记录（若要调试，请取消下面两行注释）
            //string sWord = "responseTxt=" + responseTxt + "\n isSign=" + isSign.ToString() + "\n 返回回来的参数：" + GetPreSignStr(inputPara) + "\n ";
            //Core.LogResult(sWord);

            //判断responsetTxt是否为true，isSign是否为true
            //responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
            //isSign不是true，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关
            if (responseTxt == "true" && isSign)//验证成功
            {
                return true;
            }
            else//验证失败
            {
                return false;
            }
        }

        /// <summary>
        /// 获取待签名字符串（调试用）
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <returns>待签名字符串</returns>
        private static string GetPreSignStr(SortedDictionary<string, string> inputPara)
        {
            Dictionary<string, string> sPara = new Dictionary<string, string>();

            //过滤空值、sign与sign_type参数
            sPara = AlipayCore.FilterPara(inputPara);

            //获取待签名字符串
            string preSignStr = AlipayCore.CreateLinkString(sPara);

            return preSignStr;
        }

        /// <summary>
        /// 获取返回时的签名验证结果
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="sign">对比的签名结果</param>
        /// <param name="signType">签名方式</param>
        /// <param name="key">交易安全校验码</param>
        /// <param name="code">字符编码格式</param>
        /// <returns>签名验证结果</returns>
        private static bool GetSignVeryfy(SortedDictionary<string, string> inputPara, string sign, string signType, string key, Encoding code)
        {
            Dictionary<string, string> sPara = new Dictionary<string, string>();

            //过滤空值、sign与sign_type参数
            sPara = AlipayCore.FilterPara(inputPara);

            //获取待签名字符串
            string preSignStr = AlipayCore.CreateLinkString(sPara);

            //获得签名验证结果
            bool isSgin = false;
            if (sign != null && sign != "")
            {
                switch (signType)
                {
                    case "MD5":
                        isSgin = AlipayMD5.Verify(preSignStr, sign, key, code);
                        break;
                    default:
                        break;
                }
            }

            return isSgin;
        }

        /// <summary>
        /// 获取是否是支付宝服务器发来的请求的验证结果
        /// </summary>
        /// <param name="notifyId">通知验证ID</param>
        /// <param name="veryfyUrl">支付宝消息验证地址</param>
        /// <param name="partner">收款支付宝帐户ID</param>
        /// <returns>验证结果</returns>
        private static string GetResponseTxt(string notifyId, string veryfyUrl, string partner)
        {
            string veryfy_url = string.Format("{0}partner={1}&notify_id={2}", veryfyUrl, partner, notifyId);

            //获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
            string responseTxt = AlipayCore.Get_Http(veryfy_url, 120000);

            return responseTxt;
        }


    }
}