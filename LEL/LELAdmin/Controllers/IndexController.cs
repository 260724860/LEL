using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using static Common.SmsSendHelper;

namespace LELAdmin.Controllers
{
    /// <summary>
    /// 后台首页 API
    /// </summary>
    [RoutePrefix("api/Index")]
    public class IndexController : ApiController
    {
        private Service.IndexService IdService = new Service.IndexService();
        SmsSendHelper ssh = new SmsSendHelper();
        /// <summary>
        /// 查询后台首页业绩统计
        /// </summary>
        /// <returns></returns>
        [Route("GetSalesDTO")]
        [HttpGet]
        public IHttpActionResult GetSalesDTO()
        {
            try {
                var dto = IdService.GetSalesDTO();
                return Json(new { code = 0, msg = "SUCCESS", content = dto });
            } catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 查询商品总览
        /// </summary>
        /// <returns></returns>
        [Route("GetGoodsStaticDTO")]
        [HttpGet]
        public IHttpActionResult GetGoodsStaticDTO()
        {
            try
            {
                var dto = IdService.GetGoodsStaticDTO();
                return Json(new { code = 0, msg = "SUCCESS", content = dto });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 查询选择时间段内销售订单数据
        /// </summary>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        [Route("GetSalesChartDTO")]
        [HttpGet]
        public IHttpActionResult GetSalesChartDTO(string StartTime, string EndTime)
        {
            if (Convert.ToDateTime(StartTime) >= Convert.ToDateTime(EndTime))
            {
                return Json(new { code = 1, msg = "ERROR", content = "时间选择有误" });
            }

            try
            {
                var dto = IdService.GetSalesChartDTO(StartTime, EndTime);
                return Json(new { code = 0, msg = "SUCCESS", content = dto });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 查询代办事项
        /// </summary>
        /// <returns></returns>
        [Route("GetPendingTransDTO")]
        [HttpGet]
        public IHttpActionResult GetPendingTransDTO()
        {
            try
            {
                var dto = IdService.GetPendingTransDTO();
                return Json(new { code = 0, msg = "SUCCESS", content = dto });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 发送短信测试接口
        /// </summary>
        /// <returns></returns>
        [Route("SendSingleSms")]
        [HttpGet]
        public IHttpActionResult SendSingleSms()
        {
            var mobile = "18569553462";
            var content = string.Format(@"" + ssh.SmsTemplate("T0001"), RandomCode());
            ReturnMsg result = new ReturnMsg();
            ssh.SendSingleSms(mobile, content, out result);

            //List<Multimt> List = new List<Multimt>();
            
            //Multimt group1 = new Multimt();
            //group1.mobile = "15107330889";
            //group1.content = HttpUtility.UrlEncode(string.Format(@ssh.SmsTemplate("T0001"),"260001"), Encoding.GetEncoding("GBK"));
            //List.Add(group1);

            //Multimt group2 = new Multimt();
            //group2.mobile = "18569553462";
            //group2.content = HttpUtility.UrlEncode(string.Format(@ssh.SmsTemplate("T0001"), "260002"), Encoding.GetEncoding("GBK"));
            //List.Add(group2);

            //ReturnMsg result = new ReturnMsg();

            //ssh.SendMultiSms(List,out result);

            return Json(new { code = 0, msg = "SUCCESS", content = result });
        }
    }
}
