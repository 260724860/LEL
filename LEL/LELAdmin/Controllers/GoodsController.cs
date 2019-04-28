using DTO.Goods;
using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LELAdmin.Controllers
{
    /// <summary>
    /// 后台商品 API
    /// </summary>
    [RoutePrefix("api/Goods")]
    public class GoodsController : ApiController
    {
        private GoodsService GService = new GoodsService();

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="options">查询参数</param>
        /// <returns></returns>
        [Route("GetGoodsList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetGoodsList(GoodsSeachOptions options)
        {
            //options.IsShelves = 1;
            //options.Offset = 0;
            //options.Rows = 10;
            //options.KeyWords = "7";
            //options.SortKey = GoodsSeachOrderByType.CreateTimeDesc;

            try {
                var result = await GService.GetGoodsListAsync(options);
                return Json(new { code = 0, msg = "SUCCESS", content = result });
            } catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
            
        }

        /// <summary>
        /// 获取所有的商品分类
        /// </summary>
        /// <param name="KeyWords"></param>
        /// <returns></returns>
        [Route("GetGoodsGroupList")]
        [HttpGet]
        public IHttpActionResult GetGoodsGroupList(string KeyWords = "")
        {
            try
            {
                var result = GService.GetGoodsGroupList(KeyWords);
                return Json(new { code = 0, msg = "SUCCESS", content = result });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 图片上传测试接口
        /// </summary>
        /// <returns></returns>
        [Route("AttachmentUrl")]
        [HttpGet]
        public String AttachmentUrl()
        {
            //var filebyte = GetPictureData("E:/LeerleWebServer/LELAdmin/Image/1.jpg");
            var filebyte = GetPictureData("C:/Users/wuyou/Desktop/乐尔乐项目/LeerleCode/LEL_Server/LEL/LELAdmin/Image/1.jpg");

            return GService.AttachmentUrl(filebyte);
        }

        /// <summary>  
        /// 图片转二进制  
        /// </summary>  
        /// <param name="imagepath">图片地址</param>  
        /// <returns>二进制</returns>  
        public static byte[] GetPictureData(string imagepath)
        {
            //根据图片文件的路径使用文件流打开，并保存为byte[]   
            FileStream fs = new FileStream(imagepath, FileMode.Open);//可以是其他重载方法   
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();
            return byData;
        }
    }
}
