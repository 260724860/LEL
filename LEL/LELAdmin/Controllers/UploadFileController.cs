using Common;
using System;
using System.IO;
using System.Web.Http;

namespace LELAdmin.Controllers
{
    /// <summary>
    /// 文件上传
    /// </summary>
    public class UploadFileController : ApiController
    {
        /// <summary>
        /// 图片上传 过滤 .gif|.jpg|.bmp|.jpeg|.png 大小限制16240000B
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("api/UploadFile/UploadWithStream/")]
        public IHttpActionResult UploadWithStream()
        {
            try
            {

                System.Web.HttpContext context = System.Web.HttpContext.Current;
                string serverPath = ""; //System.Configuration.ConfigurationSettings.AppSettings["serverPath"];
                string localPath = "";//System.Configuration.ConfigurationSettings.AppSettings["localPath"];
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
                        string folderPath = "GoodImg\\";
                        string serverFolderPath = "/GoodImg/";
                        if (Directory.Exists(localPath + folderPath) == false)
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
            catch (Exception ex)
            {
                return Json(JRpcHelper.AjaxResult(1, ex.Message, ex));
            }
        }

    }
}
