using Common;
using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using static Common.SmsSendHelper;

namespace LEL.Controllers
{
    public class CommonController : ApiController
    {
        OtherService OtherService = new OtherService();
        [HttpPost]
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

                    string fileName = System.Guid.NewGuid().ToString() + Path.GetExtension(hpf.FileName);

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
            var result = OtherService.GetAdList(0, 5, "", 1, out Count);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }
        /// <summary>
        /// 发送短信验证码。间隔时间120秒
        /// </summary>
        /// <param name="Phone"></param>
        /// <returns></returns>
        [HttpGet, Route("api/Common/SendSms/")]
        public IHttpActionResult SendSms(string Phone)
        {
            SmsSendHelper ssh = new SmsSendHelper();
            string Code = RandomUtils.RandomCode(6);
            var returnmsg = new ReturnMsg();
            var CookieStr = HttpUtils.GetCookie("SMS");
            if(string.IsNullOrEmpty(CookieStr))
            {
                CookieStr = Guid.NewGuid().ToString("N");
                HttpUtils.WriteCookie("SMS", CookieStr, 2);
            }            
            var Model = OtherService.GetSmsRecordforNoce(CookieStr);
            if (Model!=null&&Model.CreatTime > DateTime.Now.AddMinutes(-2))
            {
                System.TimeSpan t3 = DateTime.Now.AddMinutes(-2) - Model.CreatTime;
                return Json(JRpcHelper.AjaxResult(0, "两分钟内只能发送一条短信", t3.TotalSeconds));
            }
            var content = string.Format(@"" + ssh.SmsTemplate("T0001"),Code);                
            ssh.SendSingleSms(Phone, content, out returnmsg);
            if(returnmsg.result==0)
            {
                OtherService.AddSmsRecord(Code, Phone, CookieStr);
                
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(0, "请输入正确的手机号码", returnmsg.result));
            }
            return  Json(JRpcHelper.AjaxResult(0, "SUCCESS", returnmsg.result));
        }
    }
}
