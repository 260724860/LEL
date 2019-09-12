using Newtonsoft.Json;
using System.Collections.Generic;

namespace Common
{
    public static class JRpcHelper
    {
        public static Dictionary<string, object> MakeResulte(int code, string msg, object obj)
        {
            Dictionary<string, object> dt = new Dictionary<string, object>();
            dt.Add("code", code);
            dt.Add("msg", msg);
            dt.Add("content", obj);
            return dt;
        }

        public static Dictionary<string, object> MakeResulte(int code, string msg, object obj, object obj2)
        {
            Dictionary<string, object> dt = new Dictionary<string, object>();
            dt.Add("code", code);
            dt.Add("msg", msg);
            dt.Add("content", obj);
            dt.Add("ext", obj2);
            return dt;

        }

        public static string JsonResultSuccess(int code = 0, string msg = "SUCCESS", object obj = null)
        {
            Dictionary<string, object> dt = new Dictionary<string, object>();
            dt.Add("code", code);
            dt.Add("msg", msg);
            dt.Add("content", obj);
            return JsonConvert.SerializeObject(dt);
        }
        public static string JsonResultFail(int code = 0, string msg = "FAIL", object obj = null)
        {
            Dictionary<string, object> dt = new Dictionary<string, object>();
            dt.Add("code", code);
            dt.Add("msg", msg);
            dt.Add("content", obj);
            return JsonConvert.SerializeObject(dt);
        }
        public static string JsonResult(int code, string msg, object obj, object obj2)
        {
            Dictionary<string, object> dt = new Dictionary<string, object>();
            dt.Add("code", code);
            dt.Add("msg", msg);
            dt.Add("content", obj);
            dt.Add("ext", obj2);

            return JsonConvert.SerializeObject(dt);
        }

        public static string JsonResult(int code, string msg, object obj, object obj2, object obj3)
        {
            Dictionary<string, object> dt = new Dictionary<string, object>();
            dt.Add("code", code);
            dt.Add("msg", msg);
            dt.Add("content", obj);
            dt.Add("ext", obj2);
            dt.Add("ext2", obj3);
            return JsonConvert.SerializeObject(dt);
        }

        public static string JsonResult(int code, string msg, object obj, object obj2, object obj3, object obj4)
        {
            Dictionary<string, object> dt = new Dictionary<string, object>();
            dt.Add("code", code);
            dt.Add("msg", msg);
            dt.Add("content", obj);
            dt.Add("ext", obj2);
            dt.Add("ext2", obj3);
            dt.Add("ext3", obj4);
            return JsonConvert.SerializeObject(dt);
        }
        public static Dictionary<int, string> Errcode(int code)
        {
            Dictionary<int, string> dt = new Dictionary<int, string>();

            dt.Add(0, "密码错误");
            dt.Add(1, "登录成功");
            dt.Add(3, "尚未登录");
            dt.Add(101, "商户编码不能为空");
            dt.Add(102, "条码编码不能为空");
            dt.Add(1000, "无效URL参数,barcode不能为空.");
            dt.Add(1001, "无效的条码.");

            return dt;
        }

        public static AjaxResult AjaxResult(int code, string msg, object obj)
        {
            return new AjaxResult { code = code, msg = msg, content = obj };
        }
        public static AjaxResult AjaxResult(int code, string msg, object obj, object accattach)
        {
            return new AjaxResult { code = code, msg = msg, content = obj, accattach = accattach };
        }
        public static AjaxResult2 AjaxResult2(int code, string msg, object obj, object accattach, object accattach2)
        {
            return new AjaxResult2 { code = code, msg = msg, content = obj, accattach = accattach, accattach2 = accattach2 };
        }
    }

    public class AjaxResult
    {
        /// <summary>
        /// 执行的结果
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 执行返回的数据
        /// </summary>
        public object content { get; set; }

        public object accattach { get; set; }
    }
    public class AjaxResult2
    {
        /// <summary>
        /// 执行的结果
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 执行返回的数据
        /// </summary>
        public object content { get; set; }

        public object accattach { get; set; }
        public object accattach2 { get; set; }
    }
}
