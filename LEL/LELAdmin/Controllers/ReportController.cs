using Common;
using DTO.Common;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static DTO.Common.Enum;

namespace LELAdmin.Controllers
{
    [Authorize]
    public class ReportController : BaseController
    {       
            ReportService ReportBLL = new ReportService();
            /// <summary>
            /// 商品销量报表
            /// </summary>
            /// <param name="options">基础参数</param>
            /// <param name="GoodsID">商品ID</param>
            /// <param name="GoodsGroupsID">商品分类ID</param>
            /// <param name="SupplierID">供应商ID</param>
            /// <param name="UserID">加盟店ID</param>
            /// <returns></returns>
            [HttpPost, Route("api/Report/GetGoodsSalesReport/")]
            public IHttpActionResult GetGoodsSalesReport(SeachDateTimeOptions options, GoodsSalesReportOrderByType orderType ,int? GoodsID, int? GoodsGroupsID, int? SupplierID, int? UserID)
            {
                try
                {
                    var result = ReportBLL.GetGoodsSalesReport(options, orderType, GoodsID, GoodsGroupsID, SupplierID,  UserID,GetLoginInfo().UserID);

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
            public IHttpActionResult GetStoreSaleReport(SeachDateTimeOptions options, GoodsSalesReportOrderByType orderType, int? UserID)
            {
                try
                {
                    var result = ReportBLL.GetStoreSaleReport(options ,UserID, orderType, GetLoginInfo().UserID);

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
