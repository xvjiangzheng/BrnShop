using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.PayPlugin.ChinaBank;

namespace BrnShop.Web.Controllers
{
    /// <summary>
    /// 前台网银在线控制器类
    /// </summary>
    public class ChinaBankController : BaseWebController
    {
        /// <summary>
        /// 支付
        /// </summary>
        public ActionResult Pay()
        {
            //订单id
            int oid = WebHelper.GetQueryInt("oid");

            //订单信息
            OrderInfo orderInfo = Orders.GetOrderByOid(oid);
            if (orderInfo == null || orderInfo.Uid != WorkContext.Uid || orderInfo.OrderState != (int)OrderState.WaitPaying || orderInfo.PayMode != 1)
                return Redirect("/");

            PluginSetInfo pluginSetInfo = PluginUtils.GetPluginSet();
            string v_mid = pluginSetInfo.Mid; //商户号
            string key = pluginSetInfo.Key;

            string v_url = string.Format("http://{0}/ChinaBank/Notify", BSPConfig.ShopConfig.SiteUrl);  //返回接收支付结果的页面
            string remark2 = string.Format("[url:=http://{0}/ChinaBank/Notify]", BSPConfig.ShopConfig.SiteUrl);//服务器异步通知的接收地址

            string v_oid = oid.ToString();
            string v_amount = orderInfo.SurplusMoney.ToString();

            string v_moneytype = "CNY";

            string text = v_amount + v_moneytype + v_oid + v_mid + v_url + key; // 拼凑加密串

            string v_md5info = FormsAuthentication.HashPasswordForStoringInConfigFile(text, "md5").ToUpper();

            StringBuilder sbHtml = new StringBuilder();

            sbHtml.Append("<form action=\"https://pay3.chinabank.com.cn/PayGate?encoding=UTF-8\"  method=\"post\" name=\"E_FORM\">");
            sbHtml.AppendFormat("<input type=\"hidden\" name=\"v_md5info\" value=\"{0}\" size=\"100\" />", v_md5info);
            sbHtml.AppendFormat("<input type=\"hidden\" name=\"v_mid\" value=\"{0}\" />", v_mid);
            sbHtml.AppendFormat("<input type=\"hidden\" name=\"v_oid\" value=\"{0}\" />", v_oid);
            sbHtml.AppendFormat("<input type=\"hidden\" name=\"v_amount\" value=\"{0}\" />", v_amount);
            sbHtml.AppendFormat("<input type=\"hidden\" name=\"v_moneytype\" value=\"{0}\" />", v_moneytype);
            sbHtml.AppendFormat("<input type=\"hidden\" name=\"v_url\" value=\"{0}\" />", v_url);

            //<!--以下几项项为网上支付完成后，随支付反馈信息一同传给信息接收页-->
            sbHtml.Append("<input type=\"hidden\"  name=\"remark1\" value=\"\" />");
            sbHtml.AppendFormat("<input type=\"hidden\"  name=\"remark2\" value=\"{0}\" />", remark2);
            sbHtml.Append("<input type=\"submit\" value=\"网银在线支付\"/>");
            sbHtml.Append("</form>");
            sbHtml.Append("<script>document.forms['E_FORM'].submit();</script>");
            return Content(sbHtml.ToString());
        }

        /// <summary>
        /// 返回调用
        /// </summary>
        public ActionResult Return()
        {
            PluginSetInfo pluginSetInfo = PluginUtils.GetPluginSet();

            // 如果您还没有设置MD5密钥请登陆我们为您提供商户后台，地址：https://merchant3.chinabank.com.cn/
            // 登陆后在上面的导航栏里可能找到“B2C”，在二级导航栏里有“MD5密钥设置”
            // 建议您设置一个16位以上的密钥或更高，密钥最多64位，但设置16位已经足够了
            string key = pluginSetInfo.Key;

            int v_oid = WebHelper.GetRequestInt("v_oid");
            string v_pstatus = WebHelper.GetRequestString("v_pstatus");
            string v_pstring = WebHelper.GetRequestString("v_pstring");
            string v_pmode = WebHelper.GetRequestString("v_pmode");
            string v_md5str = WebHelper.GetRequestString("v_md5str");
            decimal v_amount = TypeHelper.StringToDecimal(WebHelper.GetRequestString("v_amount"));
            string v_moneytype = WebHelper.GetRequestString("v_moneytype");
            string remark1 = WebHelper.GetRequestString("remark1");
            string remark2 = WebHelper.GetRequestString("remark2");

            string str = v_oid + v_pstatus + v_amount + v_moneytype + key;

            str = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5").ToUpper();

            if (str == v_md5str)
            {
                if (v_pstatus.Equals("20"))
                {
                    //支付成功
                    OrderInfo orderInfo = Orders.GetOrderByOid(v_oid);
                    if (orderInfo != null && orderInfo.PayMode == 1 && orderInfo.PaySN.Length == 0 && orderInfo.SurplusMoney > 0 && orderInfo.SurplusMoney <= v_amount)
                    {
                        Orders.PayOrder(v_oid, OrderState.Confirming, "", DateTime.Now);
                        OrderActions.CreateOrderAction(new OrderActionInfo()
                        {
                            Oid = v_oid,
                            Uid = orderInfo.Uid,
                            RealName = "本人",
                            AdminGid = 1,
                            AdminGTitle = "非管理员",
                            ActionType = (int)OrderActionType.Pay,
                            ActionTime = DateTime.Now,
                            ActionDes = "你使用网银在线支付订单成功，支付银行为:" + v_pmode
                        });
                    }

                    return RedirectToAction("payresult", "order", new RouteValueDictionary { { "oid", orderInfo.Oid } });
                }
                else
                {
                    return Content("支付失败");
                }
            }
            else
            {
                return Content("校验失败，数据可疑");
            }
        }

        /// <summary>
        /// 通知调用
        /// </summary>
        public ActionResult Notify()
        {
            PluginSetInfo pluginSetInfo = PluginUtils.GetPluginSet();

            // 如果您还没有设置MD5密钥请登陆我们为您提供商户后台，地址：https://merchant3.chinabank.com.cn/
            // 登陆后在上面的导航栏里可能找到“B2C”，在二级导航栏里有“MD5密钥设置”
            // 建议您设置一个16位以上的密钥或更高，密钥最多64位，但设置16位已经足够了
            string key = pluginSetInfo.Key;

            int v_oid = WebHelper.GetRequestInt("v_oid");
            string v_pstatus = WebHelper.GetRequestString("v_pstatus");
            string v_pstring = WebHelper.GetRequestString("v_pstring");
            string v_pmode = WebHelper.GetRequestString("v_pmode");
            string v_md5str = WebHelper.GetRequestString("v_md5str");
            decimal v_amount = TypeHelper.StringToDecimal(WebHelper.GetRequestString("v_amount"));
            string v_moneytype = WebHelper.GetRequestString("v_moneytype");
            string remark1 = WebHelper.GetRequestString("remark1");
            string remark2 = WebHelper.GetRequestString("remark2");

            string str = v_oid + v_pstatus + v_amount + v_moneytype + key;

            str = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5").ToUpper();

            if (str == v_md5str)
            {
                if (v_pstatus.Equals("20"))
                {
                    //支付成功
                    OrderInfo orderInfo = Orders.GetOrderByOid(v_oid);
                    if (orderInfo != null && orderInfo.PayMode == 1 && orderInfo.PaySN.Length == 0 && orderInfo.SurplusMoney > 0 && orderInfo.SurplusMoney <= v_amount)
                    {
                        Orders.PayOrder(v_oid, OrderState.Confirming, "", DateTime.Now);
                        OrderActions.CreateOrderAction(new OrderActionInfo()
                        {
                            Oid = v_oid,
                            Uid = orderInfo.Uid,
                            RealName = "本人",
                            AdminGid = 1,
                            AdminGTitle = "非管理员",
                            ActionType = (int)OrderActionType.Pay,
                            ActionTime = DateTime.Now,
                            ActionDes = "你使用网银在线支付订单成功，支付银行为:" + v_pmode
                        });
                    }

                }
                return Content("ok");
            }
            else
            {
                return Content("error");
            }
        }
    }
}
