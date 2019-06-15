using Common;
using Service;
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
    [Authorize]
    public class IndexController : BaseController
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
                return Json(new { code = 0, msg = "ERROR", content = ex.ToString() });
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
                return Json(new { code = 0, msg = "ERROR", content = ex.ToString() });
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
                return Json(new { code = 0, msg = "ERROR", content = "时间选择有误" });
            }

            try
            {
                var dto = IdService.GetSalesChartDTO(StartTime, EndTime);
                return Json(new { code = 0, msg = "SUCCESS", content = dto });
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, msg = "ERROR", content = ex.ToString() });
            }
        }


        [Route("GetPendingTransDTO")]
        [HttpGet]
        public IHttpActionResult GetPendingTransDTO()
        {
            var result= IdService.GetPendingTransDTO();
            return  Json(new { code = 0, msg = "SUCCESS", content = result });
        }
        /// <summary>
        /// 发送短信测试接口
        /// </summary>
        /// <returns></returns>
        [Route("SendSingleSms")]
        [HttpGet]
        public IHttpActionResult SendSingleSms()
        {
            var mobile = "15107330889";
            var content = string.Format(@"" + ssh.SmsTemplate("T0001"), RandomCode());
            return Json("a");
           // ssh.SendSingleSms(mobile, content, out object result);

            //List<Multimt> List = new List<Multimt>();

            //Multimt group1 = new Multimt();
            //group1.mobile = "15107330889";
            //group1.content = HttpUtility.UrlEncode(string.Format(@ssh.SmsTemplate("T0001"),"260001"), Encoding.GetEncoding("GBK"));
            //List.Add(group1);

            //Multimt group2 = new Multimt();
            //group2.mobile = "18569553462";
            //group2.content = HttpUtility.UrlEncode(string.Format(@ssh.SmsTemplate("T0001"), "260002"), Encoding.GetEncoding("GBK"));
            //List.Add(group2);

            //string result;

            //ssh.SendMultiSms(List,out result);

         //   return Json(new { code = 0, msg = "ERROR", content = result });
        }


        /// <summary>
        /// 修改系统配置参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("api/Other/UpdateSysConfig/")]
        public IHttpActionResult UpdateSysConfig(le_sysconfig model)
        {
            var result = new SysConfigServie().UpdateSysConfig(model);
            if (result)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, "FAIL", model));
            }
        }
    }
}
