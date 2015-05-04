using System;
using System.Web.Mvc;
using System.Web.Routing;

using BrnShop.Core;
using BrnShop.Services;
using BrnShop.Web.Framework;
using BrnShop.PayPlugin.Tenpay;

namespace BrnShop.Web.Controllers
{
    /// <summary>
    /// 前台财付通控制器类
    /// </summary>
    public class TenpayController : BaseWebController
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

            string sp_billno = oid.ToString();
            string product_name = "";
            string remarkexplain = "";
            decimal money = orderInfo.SurplusMoney;

            System.Web.HttpContext Context = System.Web.HttpContext.Current;

            //创建RequestHandler实例
            RequestHandler reqHandler = new RequestHandler(Context);
            //初始化
            reqHandler.init();
            //设置密钥
            reqHandler.setKey(TenpayUtil.tenpay_key);
            reqHandler.setGateUrl("https://gw.tenpay.com/gateway/pay.htm");




            //-----------------------------
            //设置支付参数
            //-----------------------------
            reqHandler.setParameter("partner", TenpayUtil.bargainor_id);		        //商户号
            reqHandler.setParameter("out_trade_no", sp_billno);		//商家订单号
            reqHandler.setParameter("total_fee", (money * 100).ToString().Replace(".00", ""));			        //商品金额,以分为单位
            reqHandler.setParameter("return_url", TenpayUtil.tenpay_return);		    //交易完成后跳转的URL
            reqHandler.setParameter("notify_url", TenpayUtil.tenpay_notify);		    //接收财付通通知的URL
            reqHandler.setParameter("body", "商品描述");	                    //商品描述
            reqHandler.setParameter("bank_type", "DEFAULT");		    //银行类型(中介担保时此参数无效)
            reqHandler.setParameter("spbill_create_ip", Context.Request.UserHostAddress);   //用户的公网ip，不是商户服务器IP
            reqHandler.setParameter("fee_type", "1");                    //币种，1人民币
            reqHandler.setParameter("subject", "商品名称");              //商品名称(中介交易时必填)


            //系统可选参数
            reqHandler.setParameter("sign_type", "MD5");
            reqHandler.setParameter("service_version", "1.0");
            reqHandler.setParameter("input_charset", "UTF-8");
            reqHandler.setParameter("sign_key_index", "1");

            //业务可选参数

            reqHandler.setParameter("attach", "");                      //附加数据，原样返回
            reqHandler.setParameter("product_fee", "0");                 //商品费用，必须保证transport_fee + product_fee=total_fee
            reqHandler.setParameter("transport_fee", "0");               //物流费用，必须保证transport_fee + product_fee=total_fee
            reqHandler.setParameter("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));            //订单生成时间，格式为yyyymmddhhmmss
            reqHandler.setParameter("time_expire", "");                 //订单失效时间，格式为yyyymmddhhmmss
            reqHandler.setParameter("buyer_id", "");                    //买方财付通账号
            reqHandler.setParameter("goods_tag", "");                   //商品标记
            reqHandler.setParameter("trade_mode", "1");                 //交易模式，1即时到账(默认)，2中介担保，3后台选择（买家进支付中心列表选择）
            reqHandler.setParameter("transport_desc", "");              //物流说明
            reqHandler.setParameter("trans_type", "1");                  //交易类型，1实物交易，2虚拟交易
            reqHandler.setParameter("agentid", "");                     //平台ID
            reqHandler.setParameter("agent_type", "");                  //代理模式，0无代理(默认)，1表示卡易售模式，2表示网店模式
            reqHandler.setParameter("seller_id", "");                   //卖家商户号，为空则等同于partner



            //获取请求带参数的url
            return Redirect(reqHandler.getRequestURL());
        }

        /// <summary>
        /// 返回调用
        /// </summary>
        public ActionResult Return()
        {
            System.Web.HttpContext Context = System.Web.HttpContext.Current;

            //创建ResponseHandler实例
            ResponseHandler resHandler = new ResponseHandler(Context);
            resHandler.setKey(TenpayUtil.tenpay_key);

            //判断签名
            if (resHandler.isTenpaySign())
            {
                //支付结果
                string trade_state = resHandler.getParameter("trade_state");
                //交易模式，1即时到账，2中介担保
                string trade_mode = resHandler.getParameter("trade_mode");

                if ("1".Equals(trade_mode) && "0".Equals(trade_state))
                {
                    int oid = TypeHelper.StringToInt(resHandler.getParameter("out_trade_no"));//商户订单号
                    string tradeSN = resHandler.getParameter("transaction_id");//财付通订单号
                    decimal tradeMoney = TypeHelper.StringToDecimal(resHandler.getParameter("total_fee")) / 100;//金额,以分为单位
                    DateTime tradeTime = DateTime.Now;//交易时间

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
                            ActionDes = "你使用财付通支付订单成功，财付通交易号为:" + tradeSN
                        });
                    }

                    return RedirectToAction("payresult", "order", new RouteValueDictionary { { "oid", orderInfo.Oid } });
                }
                else
                {
                    return Content("财付通支付失败");
                }
            }
            else
            {
                return Content("认证签名失败");
            }
        }

        /// <summary>
        /// 通知调用
        /// </summary>
        public ActionResult Notify()
        {
            System.Web.HttpContext Context = System.Web.HttpContext.Current;

            //创建ResponseHandler实例
            ResponseHandler resHandler = new ResponseHandler(Context);
            resHandler.setKey(TenpayUtil.tenpay_key);

            //判断签名
            if (resHandler.isTenpaySign())
            {
                ///通知id
                string notify_id = resHandler.getParameter("notify_id");
                //通过通知ID查询，确保通知来至财付通
                //创建查询请求
                RequestHandler queryReq = new RequestHandler(Context);
                queryReq.init();
                queryReq.setKey(TenpayUtil.tenpay_key);
                queryReq.setGateUrl("https://gw.tenpay.com/gateway/simpleverifynotifyid.xml");
                queryReq.setParameter("partner", TenpayUtil.bargainor_id);
                queryReq.setParameter("notify_id", notify_id);

                //通信对象
                TenpayHttpClient httpClient = new TenpayHttpClient();
                httpClient.setTimeOut(5);
                //设置请求内容
                httpClient.setReqContent(queryReq.getRequestURL());
                //后台调用
                if (httpClient.call())
                {
                    //设置结果参数
                    ClientResponseHandler queryRes = new ClientResponseHandler();
                    queryRes.setContent(httpClient.getResContent());
                    queryRes.setKey(TenpayUtil.tenpay_key);
                    //判断签名及结果
                    //只有签名正确,retcode为0，trade_state为0才是支付成功
                    if (queryRes.isTenpaySign())
                    {
                        //支付结果
                        string trade_state = resHandler.getParameter("trade_state");
                        //交易模式，1即时到帐 2中介担保
                        string trade_mode = resHandler.getParameter("trade_mode");
                        #region
                        //判断签名及结果
                        if ("0".Equals(queryRes.getParameter("retcode")))
                        {
                            if ("1".Equals(trade_mode) && "0".Equals(trade_state))
                            {
                                int oid = TypeHelper.StringToInt(queryRes.getParameter("out_trade_no"));//商户订单号
                                string tradeSN = queryRes.getParameter("transaction_id");//财付通订单号
                                decimal tradeMoney = TypeHelper.StringToDecimal(queryRes.getParameter("total_fee"));//金额,以分为单位
                                DateTime tradeTime = DateTime.Now;//交易时间

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
                                        ActionDes = "你使用财付通支付订单成功，财付通交易号为:" + tradeSN
                                    });
                                }

                                return new EmptyResult();
                            }
                            else
                            {
                                return Content("交易失败");
                            }
                        }
                        else
                        {
                            return Content("查询验证签名失败或id验证失败");
                        }
                        #endregion
                    }
                    else
                    {
                        return Content("通知ID查询签名验证失败");
                    }
                }
                else
                {
                    return Content("后台调用通信失败");
                }
            }
            else
            {
                return Content("签名验证失败");
            }
        }
    }
}
