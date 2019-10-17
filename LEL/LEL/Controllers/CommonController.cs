using Common;
using DTO.Common;
using Service;
using System;
using System.IO;
using System.Web.Http;
using static Common.SmsSendHelper;

namespace LEL.Controllers
{
    public class CommonController : ApiController
    {
        OtherService OtherService = new OtherService();
        /// <summary>
        /// 图片上传 过滤 .gif|.jpg|.bmp|.jpeg|.png 大小限制16240000B
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("api/Common/UploadWithStream/")]
        public IHttpActionResult UploadWithStream()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string serverPath = System.Configuration.ConfigurationSettings.AppSettings["serverPath"];
            string localPath = System.Configuration.ConfigurationSettings.AppSettings["localPath"];
            int cout = context.Request.Files.Count;
            if (cout > 0)
            {
                System.Web.HttpPostedFile hpf = context.Request.Files[0];
                if (hpf != null)
                {
                    string fileExt = Path.GetExtension(hpf.FileName).ToLower();
                    //只能上传文件，过滤不可上传的文件类型
                    string fileFilt = ".gif|.jpg|.bmp|.jpeg|.png";
                    if (fileFilt.IndexOf(fileExt) <= -1)
                    {
                        return Json(JRpcHelper.AjaxResult(1, "上传文件类型错误", null));
                    }
                    //判断文件大小     
                    int length = hpf.ContentLength;
                    if (length > 16240000)
                    {
                        return Json(JRpcHelper.AjaxResult(1, "文件大小超出限制", null));
                    }
                    if (localPath.Trim().Length == 0)
                    {
                        localPath = System.Web.HttpContext.Current.Server.MapPath("/");
                    }

                    DateTime dt = DateTime.Now;
                    string folderPath = "Upload\\";
                    string serverFolderPath = "/Upload/";
                    if (Directory.Exists(localPath + folderPath) == false)//如果不存在就创建file文件夹
                    {
                        Directory.CreateDirectory(localPath + folderPath);
                    }

                    string fileName = RandomUtils.GenerateOutTradeNo("IMG") + Path.GetExtension(hpf.FileName);

                    string filePath = localPath + folderPath + fileName;
                    hpf.SaveAs(filePath);
                    return Json(JRpcHelper.AjaxResult(0, "上传成功", serverPath + serverFolderPath + fileName));

                }
            }
            return Json(JRpcHelper.AjaxResult(1, "没有获取到文件", null));
        }

        /// <summary>
        /// 获取广告列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("api/Common/GetADList/")]
        public IHttpActionResult GetADList()
        {
            int Count;
            var result = OtherService.GetAdList("", 1, out Count);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }
        /// <summary>
        /// 发送短信验证码。间隔时间120秒
        /// </summary>
        /// <param name="Phone"></param>
        /// <returns></returns>
        //[HttpGet, Route("api/Common/SendSms/")]
        //public IHttpActionResult SendSms(string Phone)
        //{
        //    SmsSendHelper ssh = new SmsSendHelper();
        //    string Code = RandomUtils.RandomCode(6);
        //    var returnmsg = new Result();
        //    var CookieStr = HttpUtils.GetCookie("SMS");
        //    if (string.IsNullOrEmpty(CookieStr))
        //    {
        //        CookieStr = Guid.NewGuid().ToString("N");
        //        HttpUtils.WriteCookie("SMS", CookieStr, 2);
        //    }
        //    var Model = OtherService.GetSmsRecordforNoce(CookieStr);
        //    System.TimeSpan t3 = DateTime.Now.AddMinutes(-2) - DateTime.Now;
        //    if (Model != null && Model.CreatTime > DateTime.Now.AddMinutes(-2))
        //    {
        //        t3 = DateTime.Now.AddMinutes(-1) - Model.CreatTime;
        //        return Json(JRpcHelper.AjaxResult(1, "1分钟内只能发送一条短信", t3.TotalSeconds));
        //    }
        //    var content = string.Format(@"" + ssh.SmsTemplate("T0001"), Code);
        //    ssh.SendSingleSms(Phone, content, out returnmsg);
        //    if (returnmsg.result == 0)
        //    {
        //        OtherService.AddSmsRecord(Code, Phone, CookieStr);

        //    }
        //    else
        //    {
        //        return Json(JRpcHelper.AjaxResult(1, "请输入正确的手机号码", returnmsg.result));
        //    }
        //    return Json(JRpcHelper.AjaxResult(0, "倒计时," + t3.TotalSeconds.ToString() + "", returnmsg.result));
        //    return null;
        //}

        [HttpGet, Route("api/Common/SendSms/")]
        public IHttpActionResult SendSms(string Phone)
        {
            //SmsSendHelper ssh = new SmsSendHelper();
            //string Code = RandomUtils.RandomCode(6);
            var returnmsg = new Result();
            var CookieStr = HttpUtils.GetCookie("SMS");
            if (string.IsNullOrEmpty(CookieStr))
            {
                CookieStr = Guid.NewGuid().ToString("N");
                HttpUtils.WriteCookie("SMS", CookieStr, 2);
            }
            var Model = OtherService.GetSmsRecordforNoce(CookieStr);
            System.TimeSpan t3 = DateTime.Now.AddMinutes(-1) - DateTime.Now;
            var str = TenCentSmsHelper.RandomCode();
            if (Model != null && Model.CreatTime > DateTime.Now.AddMinutes(-1))
            {
                t3 = DateTime.Now.AddMinutes(-1) - Model.CreatTime;
                return Json(JRpcHelper.AjaxResult(1, "1分钟内只能发送一条短信", t3.TotalSeconds));

            }
            if(Model!=null)
            {
                if(Model.CreatTime > DateTime.Now.AddMinutes(-10))
                {
                    str = Model.Code;
                }
            }
            
            string[] parm = new string[] { str };
            //  TenCentSmsHelper.SmsSingleSender(Phone, 334320, parm, "蘑菇侠");
            TenCentSmsHelper.SmsSingleSender(Phone, 334320, parm, "乐尔乐");
            if (returnmsg.result == 0)
            {
                OtherService.AddSmsRecord(parm[0], Phone, CookieStr);

            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, "请输入正确的手机号码", returnmsg.result));
            }
            return Json(JRpcHelper.AjaxResult(0, "倒计时," + t3.TotalSeconds.ToString() + "", returnmsg.result));
            return null;
        }
        /// <summary>
        /// 加密测试 返回16进制
        /// </summary>
        /// <param name="PWD"></param>
        /// <returns></returns>
        [HttpGet, Route("api/Common/DesHepler16/")]
        public IHttpActionResult DesHepler16(string PWD)
        {
            string TruePwd = DESEncrypt.EncryptStringHex(PWD, "SystemLE");

            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", TruePwd));
        }
        /// <summary>
        /// 加密测试 返回Base64
        /// </summary>
        /// <param name="PWD"></param>
        /// <returns></returns>
        [HttpGet, Route("api/Common/DesHeplerBase64/")]
        public IHttpActionResult DesHeplerBase64(string PWD)
        {
            string TruePwd = DESEncrypt.DESEncryptStringBase64(PWD, "SystemLE");

            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", TruePwd));
        }
        /// <summary>
        /// 解密测试 返回16进制
        /// </summary>
        /// <param name="PWD"></param>
        /// <returns></returns>
        [HttpGet, Route("api/Common/DecryptString16/")]
        public IHttpActionResult DecryptString16(string PWD)
        {
            string TruePwd = DESEncrypt.DecryptStringHex(PWD, "SystemLE");

            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", TruePwd));
        }

        /// <summary>
        /// 获取系统自提地址
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost, Route("api/Common/GetSysAddressList/")]
        public IHttpActionResult GetSysAddressList(SeachOptions options)
        {
            int Count;
            options.Status = 1;
            var result = OtherService.GetSysAddressList(options, out Count);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result, Count));
        }

        /// <summary>
        /// 获取最新订单推送消息
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="UserType">用户类型 1商户 2供货商</param>
        /// <returns></returns>
        [HttpPost, Route("api/Common/GetPushMsg/")]
        public IHttpActionResult GetPushMsg(int UserID, int UserType)
        {
            var result = OtherService.GetPushMsg(UserID, UserType);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }
        /// <summary>
        /// 获取系统运行参数
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("api/Other/GetSysConfig/")]
        public IHttpActionResult GetSysConfig()
        {
            var result = SysConfig.Get().values;
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }

        /// <summary>
        /// 强制刷新系统运行参数（可能会引发系统异常）
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("api/Other/RefreshSysConfig/")]
        public IHttpActionResult RefreshSysConfig()
        {
            var result = SysConfig.RefreshSysConfig();
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }

        public IHttpActionResult GetHours()
        {
            string start = DateTime.Now.ToString("yyyy-MM-dd HH") + ":00:00";
            string end = DateTime.Now.ToString("yyyy-MM-dd ") + "23:59:59";
            var result = Convert.ToDateTime(start);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }

        /// <summary>
        /// 日志测试
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("LogTest")]
        [AllowAnonymous]
        public IHttpActionResult LogTest()
        {
            new ShopOrderService().LogTest();
            return Json(JRpcHelper.AjaxResult(0, "1111", ""));
        }

        /// <summary>
        /// 获取购物车优化版本
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="IsBackgroundAddition"></param>
        /// <returns></returns>
        [HttpGet, Route("GetCartListByshort")]
        [AllowAnonymous]
        public IHttpActionResult GetCartListByshort(int UserID, int IsBackgroundAddition = 0)
        {
           var reuslt=  new ShopOrderService().GetCartListByshort(UserID, IsBackgroundAddition);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", reuslt));
        }
    }
}
