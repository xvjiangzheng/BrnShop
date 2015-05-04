using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.PayPlugin.Alipay;

namespace BrnShop.Web.Controllers
{
    /// <summary>
    /// 前台支付宝控制器类
    /// </summary>
    public class AlipayController : BaseWebController
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

            //支付类型，必填，不能修改
            string paymentType = "1";

            //服务器异步通知页面路径，需http://格式的完整路径，不能加?id=123这类自定义参数
            string notifyUrl = string.Format("http://{0}/Alipay/Notify", BSPConfig.ShopConfig.SiteUrl);
            //页面跳转同步通知页面路径，需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/
            string returnUrl = string.Format("http://{0}/Alipay/Return", BSPConfig.ShopConfig.SiteUrl);

            //收款支付宝帐户
            string sellerEmail = AlipayConfig.Seller;
            //合作者身份ID
            string partner = AlipayConfig.Partner;
            //交易安全检验码
            string key = AlipayConfig.Key;

            //商户订单号
            string outTradeNo = orderInfo.Oid.ToString() + Randoms.CreateRandomValue(10, false);
            //订单名称
            string subject = BSPConfig.ShopConfig.SiteTitle + "购物";
            //付款金额
            string totalFee = orderInfo.SurplusMoney.ToString();
            //订单描述
            string body = "";

            //防钓鱼时间戳,若要使用请调用类文件submit中的query_timestamp函数
            string antiPhishingKey = "";
            //客户端的IP地址,非局域网的外网IP地址，如：221.0.0.1
            string exterInvokeIP = "";

            //把请求参数打包成数组
            SortedDictionary<string, string> parms = new SortedDictionary<string, string>();
            parms.Add("partner", partner);
            parms.Add("_input_charset", AlipayConfig.InputCharset);
            parms.Add("service", "create_direct_pay_by_user");
            parms.Add("payment_type", paymentType);
            parms.Add("notify_url", notifyUrl);
            parms.Add("return_url", returnUrl);
            parms.Add("seller_email", sellerEmail);
            parms.Add("out_trade_no", outTradeNo);
            parms.Add("subject", subject);
            parms.Add("total_fee", totalFee);
            parms.Add("body", body);
            parms.Add("show_url", "");
            parms.Add("anti_phishing_key", antiPhishingKey);
            parms.Add("exter_invoke_ip", exterInvokeIP);

            //建立请求
            string sHtmlText = AlipaySubmit.BuildRequest(parms, AlipayConfig.SignType, AlipayConfig.Key, AlipayConfig.Code, AlipayConfig.Gateway, AlipayConfig.InputCharset, "get", "确认");
            return Content(sHtmlText);
        }

        /// <summary>
        /// 返回调用
        /// </summary>
        public ActionResult Return()
        {
            SortedDictionary<string, string> paras = AlipayCore.GetRequestGet();

            if (paras.Count > 0)//判断是否有带返回参数
            {
                bool verifyResult = AlipayNotify.Verify(paras, Request.QueryString["notify_id"], Request.QueryString["sign"], AlipayConfig.SignType, AlipayConfig.Key, AlipayConfig.Code, AlipayConfig.VeryfyUrl, AlipayConfig.Partner);
                if (verifyResult && (Request.QueryString["trade_status"] == "TRADE_FINISHED" || Request.QueryString["trade_status"] == "TRADE_SUCCESS"))//验证成功
                {
                    string out_trade_no = Request.QueryString["out_trade_no"];//商户订单号
                    string tradeSN = Request.QueryString["trade_no"];//支付宝交易号
                    decimal tradeMoney = TypeHelper.StringToDecimal(Request.QueryString["total_fee"]);//交易金额
                    DateTime tradeTime = TypeHelper.StringToDateTime(Request.QueryString["notify_time"]);//交易时间

                    int oid = TypeHelper.StringToInt(StringHelper.SubString(out_trade_no, out_trade_no.Length - 10));
                    OrderInfo orderInfo = Orders.GetOrderByOid(oid);
                    if (orderInfo != null && orderInfo.PayMode == 1 && orderInfo.PaySN.Length == 0 && orderInfo.SurplusMoney > 0 && orderInfo.SurplusMoney <= tradeMoney)
                    {
                        Orders.PayOrder(oid, OrderState.Confirming, tradeSN, DateTime.Now);
                        OrderActions.CreateOrderAction(new OrderActionInfo()
                        {
                            Oid = oid,
                            Uid = orderInfo.Uid,
                            RealName = "本人",
                            AdminGid = 1,
                            AdminGTitle = "非管理员",
                            ActionType = (int)OrderActionType.Pay,
                            ActionTime = tradeTime,
                            ActionDes = "你使用支付宝支付订单成功，支付宝交易号为:" + tradeSN
                        });
                    }

                    return RedirectToAction("payresult", "order", new RouteValueDictionary { { "oid", orderInfo.Oid } });
                }
                else//验证失败
                {
                    return new EmptyResult();
                }
            }
            else
            {
                return new EmptyResult();
            }
        }

        /// <summary>
        /// 通知调用
        /// </summary>
        public ActionResult Notify()
        {
            SortedDictionary<string, string> paras = AlipayCore.GetRequestPost();

            if (paras.Count > 0)//判断是否有带返回参数
            {
                bool verifyResult = AlipayNotify.Verify(paras, Request.Form["notify_id"], Request.Form["sign"], AlipayConfig.SignType, AlipayConfig.Key, AlipayConfig.Code, AlipayConfig.VeryfyUrl, AlipayConfig.Partner);
                if (verifyResult && (Request.Form["trade_status"] == "TRADE_FINISHED" || Request.Form["trade_status"] == "TRADE_SUCCESS"))//验证成功
                {
                    string out_trade_no = Request.Form["out_trade_no"];//商户订单号
                    string tradeSN = Request.QueryString["trade_no"];//支付宝交易号
                    decimal tradeMoney = TypeHelper.StringToDecimal(Request.QueryString["total_fee"]);//交易金额
                    DateTime tradeTime = TypeHelper.StringToDateTime(Request.QueryString["gmt_payment"]);//交易时间

                    int oid = TypeHelper.StringToInt(StringHelper.SubString(out_trade_no, out_trade_no.Length - 10));
                    OrderInfo orderInfo = Orders.GetOrderByOid(oid);
                    if (orderInfo != null && orderInfo.PayMode == 1 && orderInfo.PaySN.Length == 0 && orderInfo.SurplusMoney > 0 && orderInfo.SurplusMoney <= tradeMoney)
                    {
                        Orders.PayOrder(oid, OrderState.Confirming, tradeSN, DateTime.Now);
                        OrderActions.CreateOrderAction(new OrderActionInfo()
                        {
                            Oid = oid,
                            Uid = orderInfo.Uid,
                            RealName = "本人",
                            AdminGid = 1,
                            AdminGTitle = "非管理员",
                            ActionType = (int)OrderActionType.Pay,
                            ActionTime = tradeTime,
                            ActionDes = "你使用支付宝支付订单成功，支付宝交易号为:" + tradeSN
                        });
                    }

                    return Content("success");
                }
                else//验证失败
                {
                    return Content("fail");
                }
            }
            else
            {
                return Content("无通知参数");
            }
        }
    }
}
