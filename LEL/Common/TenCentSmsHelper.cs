using qcloudsms_csharp;
using qcloudsms_csharp.httpclient;
using qcloudsms_csharp.json;
using System;
using System.Collections.Generic;

namespace Common
{
    public class TenCentSmsHelper
    {
        private static int appid = 1400210680;
        private static string appkey = "8192b9d6aa84d70601f8ef2af4fd0e99";

        /// <summary>
        /// 测试
        /// </summary>
        //public void test()
        //{
        //    TenCentSmsHelper sms = new TenCentSmsHelper();
        //    string[] parm = new string[] { "123456" };

        //    var result = sms.SmsSingleSender("13789308189", 334320, parm, "蘑菇侠");
        //}

        /// <summary>
        /// 单个短信发送
        /// </summary>
        /// <param name="phoneNumbers"></param>
        /// <param name="templateId"></param>
        /// <param name="parm"></param>
        /// <param name="smsSign"></param>
        /// <returns></returns>
        public static SmsSingleSenderResult SmsSingleSender(string phoneNumbers,int templateId,string[] parm,string smsSign)
        {
            try
            {
                SmsSingleSenderResult result = null;
                SmsSingleSender ssender = new SmsSingleSender(appid, appkey);
                result = ssender.sendWithParam("86", phoneNumbers,
                    templateId, parm, smsSign, "", "");  // 签名参数未提供或者为空时，会使用默认签名发送短信
                Console.WriteLine(result);
                return result;
            }
            catch (JSONException e)
            {
                Console.WriteLine(e);
                return null;
            }
            catch (HTTPException e)
            {
                Console.WriteLine(e);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// 批量短信发送
        /// </summary>
        /// <param name="phoneNumbers"></param>
        /// <param name="templateId"></param>
        /// <param name="parm"></param>
        /// <param name="smsSign"></param>
        /// <returns></returns>
        public static SmsMultiSenderResult SmsMultiSender(List<string> phoneNumbers, int templateId,List<string> parm, string smsSign)
        {
            try
            {
                SmsMultiSenderResult result = null; 
                SmsMultiSender msender = new SmsMultiSender(appid, appkey);
                result = msender.sendWithParam("86", phoneNumbers, templateId,
                    parm, smsSign, "", "");  // 签名参数未提供或者为空时，会使用默认签名发送短信
                Console.WriteLine(result);
                return result;
            }
            catch (JSONException e)
            {
                Console.WriteLine(e);
                return null;
            }
            catch (HTTPException e)
            {
                Console.WriteLine(e);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// 生成随机短信验证码
        /// </summary>
        public static string RandomCode()
        {
            Random rd = new Random();
            string str = "0123456789";
            string result = "";

            for (int j = 0; j < 6; j++)
            {
                result += str[rd.Next(str.Length)];
            }

            return result;
        }
    }
}
