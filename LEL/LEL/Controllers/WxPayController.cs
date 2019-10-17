using Senparc.Weixin.TenPay.V3;
using Senparc.Weixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP;
using Common;
using Senparc.Weixin.TenPay.V2;
using ResponseHandler = Senparc.Weixin.TenPay.V3.ResponseHandler;
using Senparc.CO2NET.Utilities;
using System.IO;
using System.Text;
using Senparc.Weixin.Exceptions;
using Service;
using DTO.ShopOrder;

namespace LEL.Controllers
{
    [RoutePrefix("api/WxPay")]
    [Authorize]
    public class WxPayController : ApiController
    {
        private static TenPayV3Info _tenPayV3Info;

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
        }

        public static TenPayV3Info TenPayV3Info
        {
            get
            {
                if (_tenPayV3Info == null)
                {
                    var key = TenPayV3InfoCollection.GetKey(Config.SenparcWeixinSetting);

                    _tenPayV3Info =
                        TenPayV3InfoCollection.Data[key];
                }
                return _tenPayV3Info;
            }
        }

        /// <summary>
        /// 获取授权链接
        /// </summary>
        /// <param name="url"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        //[HttpGet, Route("GetAuthorizeUrl")]

        //public IHttpActionResult  GetAuthorizeUrl(string url,string state)
        //{
        //    var reuslt = OAuthApi.GetAuthorizeUrl(TenPayV3Info.AppId, url, state, OAuthScope.snsapi_userinfo);
            
        //    return Json(JRpcHelper.AjaxResult(0, "SUCCESS", reuslt));

        //}
        /// <summary>
        /// 获取微信授权资料
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        //[HttpGet, Route("OAuthCallback")]
        //public IHttpActionResult OAuthCallback(string code, string state)
        //{
        //    if (string.IsNullOrEmpty(code))
        //    {
        //        return Json(JRpcHelper.AjaxResult(1, "你拒绝了授权", null));
        //    }
        //    //通过，用code换取access_token
        //    var openIdResult = OAuthApi.GetAccessToken(TenPayV3Info.AppId, TenPayV3Info.AppSecret, code);
        //    if (openIdResult.errcode != ReturnCode.请求成功)
        //    {
        //        return Json(JRpcHelper.AjaxResult(0, "错误："+openIdResult.errmsg, null));
        //        //return Content("错误：" + openIdResult.errmsg);
        //    }
        //    return Json(JRpcHelper.AjaxResult(0, "SUCCESS", openIdResult));
        //}
        
        /// <summary>
        /// JsApi发起支付
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="hc"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("JsApiPay")]
        public IHttpActionResult JsApiPay(OrderSaveParams Data)
        {
            try
            {
                var OrderResult = new ShopOrderService().OrderSave(Data, 5000, "测试", out string Msg,out string OutTradeNo, 0);               
                if(OrderResult<=0||string.IsNullOrEmpty(OutTradeNo))
                {
                    return  Json(JRpcHelper.AjaxResult(1, Msg, Msg));
                }
                
                string sp_billno  = OutTradeNo;
               
                var timeStamp = TenPayV3Util.GetTimestamp();
                var nonceStr = TenPayV3Util.GetNoncestr();

                var body = "测试";
                var price = 100;
                var xmlDataInfo = new TenPayV3UnifiedorderRequestData(TenPayV3Info.AppId, TenPayV3Info.MchId, body, sp_billno, price,"192.168.121.10", TenPayV3Info.TenPayV3Notify,Senparc.Weixin.TenPay.TenPayV3Type.JSAPI, Data.OpenID, TenPayV3Info.Key, nonceStr);
                var result = TenPayV3.Unifiedorder(xmlDataInfo);//调用统一订单接口

              //JsSdkUiPackage jsPackage = new JsSdkUiPackage(TenPayV3Info.AppId, timeStamp, nonceStr,);

                var package = string.Format("prepay_id={0}", result.prepay_id);
                JsApiPayDto JsApiPayDto = new JsApiPayDto();

                JsApiPayDto.appId = TenPayV3Info.AppId;
                JsApiPayDto.timeStamp = timeStamp;
                JsApiPayDto.nonceStr = nonceStr;
                JsApiPayDto.package = package;
                JsApiPayDto.paySign = TenPayV3.GetJsPaySign(TenPayV3Info.AppId, timeStamp, nonceStr, package, TenPayV3Info.Key);

                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", JsApiPayDto));
               
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                msg += "<br>" + ex.StackTrace;
                msg += "<br>==Source==<br>" + ex.Source;

                if (ex.InnerException != null)
                {
                    msg += "<br>===InnerException===<br>" + ex.InnerException.Message;
                }
                return Json(JRpcHelper.AjaxResult(0, ex.Message, ex));
            }
        }

        /// <summary>
        /// 重新支付
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("AgainPay")]
        public IHttpActionResult AgainPay(string OutTradeNo,string OpenID)
        {
            var IsTure = new ShopOrderService().ExitNopayOrderByOrderNo(OutTradeNo);
            if(IsTure)
            {
                return Json(JRpcHelper.AjaxResult(1, "不存在的订单", null));
            }
            var timeStamp = TenPayV3Util.GetTimestamp();
            var nonceStr = TenPayV3Util.GetNoncestr();

            var body = "测试";
            var price = 100;
            var xmlDataInfo = new TenPayV3UnifiedorderRequestData(TenPayV3Info.AppId, TenPayV3Info.MchId, body, OutTradeNo, price, "192.168.121.10", TenPayV3Info.TenPayV3Notify, Senparc.Weixin.TenPay.TenPayV3Type.JSAPI, OpenID, TenPayV3Info.Key, nonceStr);
            var result = TenPayV3.Unifiedorder(xmlDataInfo);//调用统一订单接口
           
            var package = string.Format("prepay_id={0}", result.prepay_id);
            JsApiPayDto JsApiPayDto = new JsApiPayDto();

            JsApiPayDto.appId = TenPayV3Info.AppId;
            JsApiPayDto.timeStamp = timeStamp;
            JsApiPayDto.nonceStr = nonceStr;
            JsApiPayDto.package = package;
            JsApiPayDto.paySign = TenPayV3.GetJsPaySign(TenPayV3Info.AppId, timeStamp, nonceStr, package, TenPayV3Info.Key);

            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", JsApiPayDto));
        }
        /// <summary>
        /// JS-SDK支付回调地址（在统一下单接口中设置notify_url）
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult PayNotifyUrl(bool isWxOpenPay = false)//注意：统一下单接口中不能带参数！
        {
            WeixinTrace.SendCustomLog("微信支付回调", "来源：" + (isWxOpenPay ? "微信支付" : "小程序支付"));
            try
            {
                ResponseHandler resHandler = new ResponseHandler(null);
                string return_code = resHandler.GetParameter("return_code");
                string return_msg = resHandler.GetParameter("return_msg");
                bool paySuccess = false;

                resHandler.SetKey(TenPayV3Info.Key);
                //验证请求是否从微信发过来（安全）
                if (resHandler.IsTenpaySign() && return_code.ToUpper() == "SUCCESS")
                {
                    paySuccess = true;//正确的订单处理
                    //直到这里，才能认为交易真正成功了，可以进行数据库操作，但是别忘了返回规定格式的消息！
                    string OrderNo = resHandler.GetParameter("out_trade_no");

                    using (Entities ctx=new Entities())
                    {
                        var OrderResult= ctx.le_orders_head.Where(s => s.OutTradeNo == OrderNo).FirstOrDefault();
                        if(OrderResult!=null)
                        {
                            if(OrderResult.pay_status==0)
                            {
                                OrderResult.pay_status = 1;
                                ctx.Entry<le_orders_head>(OrderResult).State = System.Data.Entity.EntityState.Modified;
                                if(ctx.SaveChanges()>0)
                                {
                                    
                                }else
                                {
                                    paySuccess = false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    paySuccess = false;//错误的订单处理
                }

                if (paySuccess)
                {
                    /* 这里可以进行订单处理的逻辑 */

                    //发送支付成功的模板消息
                    try
                    {
                        string appId = Config.SenparcWeixinSetting.WeixinAppId;//与微信公众账号后台的AppId设置保持一致，区分大小写。
                        string openId = resHandler.GetParameter("openid");
                       
                    }
                    catch (Exception ex)
                    {
                        // WeixinTrace.WeixinExceptionLog(new WeixinException("支付成功模板消息异常", ex));
                        //WeixinTrace.SendCustomLog("支付成功模板消息", ex.ToString());
                    }

                    WeixinTrace.SendCustomLog("PayNotifyUrl回调", "支付成功");

                }
                else
                {
                    Senparc.Weixin.WeixinTrace.SendCustomLog("PayNotifyUrl回调", "支付失败");
                }



                #region 记录日志

                var logDir = ServerUtility.ContentRootMapPath(string.Format("~/App_Data/TenPayNotify/{0}", SystemTime.Now.ToString("yyyyMMdd")));
                if (!Directory.Exists(logDir))
                {
                    Directory.CreateDirectory(logDir);
                }

                var logPath = Path.Combine(logDir, string.Format("{0}-{1}-{2}.txt", SystemTime.Now.ToString("yyyyMMdd"), SystemTime.Now.ToString("HHmmss"), Guid.NewGuid().ToString("n").Substring(0, 8)));

                using (var fileStream = System.IO.File.OpenWrite(logPath))
                {
                    var notifyXml = resHandler.ParseXML();
                    //fileStream.Write(Encoding.Default.GetBytes(res), 0, Encoding.Default.GetByteCount(res));

                    fileStream.Write(Encoding.Default.GetBytes(notifyXml), 0, Encoding.Default.GetByteCount(notifyXml));
                    fileStream.Close();
                }

                #endregion


                string xml = string.Format(@"<xml>
<return_code><![CDATA[{0}]]></return_code>
<return_msg><![CDATA[{1}]]></return_msg>
</xml>", return_code, return_msg);
                //return Content(xml, "text/xml");
                return Json(xml);
            }
            catch (Exception ex)
            {
                WeixinTrace.WeixinExceptionLog(new WeixinException(ex.Message, ex));
                throw;
            }
        }
      
        private class JsApiPayDto
        {
           public string appId { get; set; }
           public string timeStamp { get; set; }
            public string nonceStr { get; set; }
            public string package { get; set; }
            public string paySign { get; set; }
        }


    }
}
