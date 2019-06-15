using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    /// <summary>
    /// 短信接口发送
    /// </summary>
    public class SmsSendHelper
    {
        private const string TMP_SEND_CONTENT_JSON_APIKEY = "\"apikey\":\"{0}\",\"mobile\":\"{1}\",\"content\":\"{2}\",\"timestamp\":\"{3}\",\"svrtype\":\"{4}\",\"exno\":\"{5}\",\"custid\":\"{6}\",\"exdata\":\"{7}\"";
        /// <summary>
        /// 个性发送报文JSON， APIKEY鉴权模板 
        /// </summary>
        public const string TMP_MULT_CONTENT_JSON_APIKEY = "\"apikey\":\"{0}\",\"timestamp\":\"{1}\",\"multimt\":{2}";

        private const string APIKEY = "55bba72e44c3d1bc307467165aef1991";

        public delegate string MyDelegate(object paras);

        /// <summary>
        /// 发送单个短信通知
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="msg"></param>
        public void SendSingleSms(string mobile,string content, out Result msg)
        {
            Message box = new Message();
            box.apikey = APIKEY;
            box.mobile = mobile;
            box.content = content;
            string SmsJsonStr = getSingleContentString(box);
            
            SortedList<string, string> paras = new SortedList<string, string>();
            paras.Add("contents", SmsJsonStr);
            paras.Add("url", SmsSendUrl("U0001"));

            MyDelegate dele = new MyDelegate(httpPost);
            IAsyncResult Ir = dele.BeginInvoke(paras, null, null);
            //msg = dele.EndInvoke(Ir);
            msg = JsonConvert.DeserializeObject<Result>(dele.EndInvoke(Ir));
        }

        /// <summary>
        /// 发送个性短信群发通知
        /// </summary>
        /// <param name="MultimtList"></param>
        /// <param name="msg"></param>
        public void SendMultiSms(List<Multimt> MultimtList,out object msg)
        {
            msg = "";
            Message box = new Message();
            box.apikey = APIKEY;
            box.multimt = JsonConvert.SerializeObject(MultimtList);
            string SmsJsonStr = getMultiMtContentString(box);

            SortedList<string, string> paras = new SortedList<string, string>();
            paras.Add("contents", SmsJsonStr);
            paras.Add("url", SmsSendUrl("U0002"));

            MyDelegate dele = new MyDelegate(httpPost);
            IAsyncResult Ir = dele.BeginInvoke(paras, null, null);
            msg = dele.EndInvoke(Ir);

        }

        /// <summary>
        /// 单发短信 Content
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string getSingleContentString(Message message)
        {
            string contents = null;
            contents = string.Format(TMP_SEND_CONTENT_JSON_APIKEY,
                       message.apikey,
                       message.mobile,
                       HttpUtility.UrlEncode(message.content, Encoding.GetEncoding("GBK")),
                       message.TimeStamp,
                        (string.IsNullOrEmpty(message.svrtype) ? string.Empty : message.svrtype),
                        (string.IsNullOrEmpty(message.exno) ? string.Empty : message.exno),
                        (string.IsNullOrEmpty(message.custid) ? string.Empty : message.custid),
                        (string.IsNullOrEmpty(message.exdata) ? string.Empty : message.exdata));
            //两端加中括号
            contents = "{" + contents + "}";
            return contents;
        }

        /// <summary>
        /// 个性化群发 Content
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string getMultiMtContentString(Message message)
        {
            string contents = null;

            contents = string.Format(TMP_MULT_CONTENT_JSON_APIKEY,
                          message.apikey,
                          message.TimeStamp,
                          message.multimt);
            //两端加中括号
            contents = "{" + contents + "}";
            return contents;
        }

        public class Message
        {
            /// <summary>
            /// 用户唯一标识：32位长度，必填
            /// </summary>
            public string apikey { get; set; }

            /// <summary>
            /// 短信接收的手机号，必填
            /// </summary>
            public string mobile { get; set; }

            /// <summary>
            /// 短信内容：最大支持350个字，一个字母或一个汉字都视为一个字
            /// 编码方式：urlencode（GBK明文），必填
            /// </summary>
            public string content { get; set; }

            /// <summary>
            /// 业务类型，非必填
            /// </summary>
            public string svrtype { get; set; }
            /// <summary>
            /// 时间戳，非必填
            /// </summary>
            public string TimeStamp { get; set; }
            /// <summary>
            /// 非必填
            /// </summary>
            public string exno { get; set; }
            /// <summary>
            /// 非必填
            /// </summary>
            public string custid { get; set; }
            /// <summary>
            /// 非必填
            /// </summary>
            public string exdata { get; set; }

            /// <summary>
            /// 个性化群发时 必填
            /// </summary>
            public string multimt { get; set; }
        }

        public class Multimt
        {
            /// <summary>
            /// 手机号，必填
            /// </summary>
            public string mobile { get; set; }
            /// <summary>
            /// 短信内容，必填
            /// </summary>
            public string content { get; set; }
            public string svrtype { get; set; }
            public string exno { get; set; }
            public string custid { get; set; }
            public string exdata { get; set; }
        }

        public class Result {
            public int result { get; set; }
            public string msgid { get; set; }
            public string custid { get; set; }
        }

        /// HTTP提交
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="contents"></param>
        /// <param name="bKeepAlive"></param>
        /// <returns></returns>
        private string httpPost(object paras)
        {
            SortedList<string, string> par = paras as SortedList<string, string>;

            var contents = par["contents"].ToString();
            var url = par["url"].ToString();

            //创建一个Http请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //设置请求的方法。
            request.Method = "POST";
            //设置 Content-typeHTTP 标头的值。 
            request.ContentType = "application/json";

            //请求所包含的 Connection HTTP 标头带有 Keep-alive 这一值，则为 true；否则为 false。默认值为  true。
            request.KeepAlive = true;
            //转为UTF-8
            byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(contents);
            //设置 Content-lengthHTTP 标头。
            request.ContentLength = bytes.Length;
            // 获取用于写入请求数据的 System.IO.Stream 对象。
            Stream os = request.GetRequestStream();
            os.Write(bytes, 0, bytes.Length);
            os.Close();
            //提供来自统一资源标识符 (URI) 的响应       
            System.Net.WebResponse response = request.GetResponse();
            if (response == null)
                return String.Empty;

            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                //从流的当前位置到末尾读取流。
                return sr.ReadToEnd().Trim();
            }
        }

        /// <summary>
        /// 短信模板
        /// </summary>
        /// <param name="TemplateID"></param>
        /// <returns></returns>
        public string SmsTemplate(string TemplateID)
        {
            switch (TemplateID)
            {
                case "T0001":
                    return "您的验证码为:{0}，5分钟内有效，为保障账户安全，请勿向他人泄露验证码信息";
                    break;

                case "T0002":
                    return "您的商品已送达到指定地址，请及时前去领取。感谢您选择本平台，我们将会继续竭诚为您服务";
                    break;

                case "T0003":
                    return "单号：{0}的订单已审核，需要您的处理";
                    break;

                default:
                    return "";
                    break;
            }
        }

        public string SmsSendUrl(string UrlID)
        {
            switch (UrlID)
            {
                case "U0001"://单条发送
                    return "http://api01.monyun.cn:7901/sms/v2/std/single_send";
                    break;

                case "U0002"://个性化群发
                    return "http://api01.monyun.cn:7901/sms/v2/std/multi_send";
                    break;

                default:
                    return "";
                    break;
            }
        }

        /// <summary>
        /// 生成随机6位数短信验证码
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
