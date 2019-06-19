using Common;
using DTO.Common;
using DTO.Report;
using Service;
using System;
using System.Collections.Generic;
using System.Web.Http;
using static DTO.Common.Enum;

namespace LEL.Controllers
{
    public class ReportController : BaseApiController
    {
        ReportService ReportBLL = new ReportService();

        /// <summary>
        /// 商品销量报表
        /// </summary>
        /// <param name="options"></param>
        /// <param name="GoodsID"></param>
        /// <param name="GoodsGroupsID"></param>
        /// <param name="SupplierID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpPost, Route("api/Report/GetGoodsSalesReport/")]
        public IHttpActionResult GetGoodsSalesReport(SeachDateTimeOptions options, GoodsSalesReportOrderByType orderType, int? GoodsID, int? GoodsGroupsID)
        {
            try
            {
                List<GoodsSaleDTO> result = new List<GoodsSaleDTO>();
                if (GetLoginInfo().UserType == 1)
                {
                    result = ReportBLL.GetGoodsSalesReport(options, orderType, GoodsID, GoodsGroupsID, null, GetLoginInfo().UserID, null);
                }
                if (GetLoginInfo().UserType == 2)
                {
                    result = ReportBLL.GetGoodsSalesReport(options, orderType, GoodsID, GoodsGroupsID, GetLoginInfo().UserID, null, null);
                }
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            catch (Exception ex)
            {
                return Json(JRpcHelper.AjaxResult(1, ex.Message, ex));
            }
        }

        /// <summary>
        /// 商户销量统计报表
        /// </summary>
        /// <param name="options"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpPost, Route("api/Report/GetStoreSaleReport/")]
        public IHttpActionResult GetStoreSaleReport(SeachDateTimeOptions options, int? UserID, GoodsSalesReportOrderByType orderType)
        {
            try
            {
                var result = ReportBLL.GetStoreSaleReport(options, UserID, orderType, null);

                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            catch (Exception ex)
            {
                return Json(JRpcHelper.AjaxResult(1, ex.Message, ex));
            }
        }
        /// <summary>
        /// 供应商统计报表
        /// </summary>
        /// <param name="options"></param>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        [HttpPost, Route("api/Report/GetSupplierSaleReport/")]
        public IHttpActionResult GetSupplierSaleReport(SeachDateTimeOptions options, int? SupplierID, GoodsSalesReportOrderByType orderType)
        {
            try
            {
                var result = ReportBLL.GetSupplierSaleReport(options, SupplierID, orderType);

                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            catch (Exception ex)
            {
                return Json(JRpcHelper.AjaxResult(1, ex.Message, ex));
            }
        }
    }
}
