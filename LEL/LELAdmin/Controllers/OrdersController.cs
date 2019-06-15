using DTO.ShopOrder;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LELAdmin.Controllers
{
    /// <summary>
    /// 订单API
    /// </summary>
    [RoutePrefix("api/Orders")]
    public class OrdersController : ApiController
    {
        private ShopOrderService soService = new ShopOrderService();

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="seachParams"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        [Route("GetListOrder")]
        [HttpPost]
        public IHttpActionResult GetListOrder(OrderSeachParams seachParams)
        {
            try
            {
                var dto = soService.GetListOrder(seachParams,out int Count);
                return Json(new { code = 0, msg = "SUCCESS", content = dto, count = Count });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 获取订单详细
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        [Route("GetOrderDetails")]
        [HttpGet]
        public IHttpActionResult GetOrderDetails(string OrderNo)
        {
            try
            {
                int Count;
                var dto = soService.GetOrderDetails(OrderNo);
                return Json(new { code = 0, msg = "SUCCESS", content = dto });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 修改订单收货信息
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [Route("EditReceiptInfo")]
        [HttpPost]
        public IHttpActionResult EditReceiptInfo(EditReceiptInfo Info)
        {
            try
            {
                string msg;
                var bol = soService.EditReceiptInfo(Info, out msg);
                if (bol)
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = msg });
                }
                return Json(new { code = 1, msg = "ERROR", content = msg });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="Out_Trade_No"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        [Route("UpdateOrderStatus")]
        [HttpPost]
        public IHttpActionResult UpdateOrderStatus(string Out_Trade_No, int Status)
        {
            try
            {
                string msg;
                var bol = soService.UpdateOrderStatus(Out_Trade_No, Status, out msg);
                if (bol)
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = msg });
                }
                return Json(new { code = 1, msg = "ERROR", content = msg });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 修改订单备注信息
        /// </summary>
        /// <param name="Out_Trade_No"></param>
        /// <param name="Head_Notes"></param>
        /// <returns></returns>
        [Route("EditOrderHead_Notes")]
        [HttpPost]
        public IHttpActionResult EditOrderHead_Notes(string Out_Trade_No, string Head_Notes)
        {
            try
            {
                string msg;
                var bol = soService.EditOrderHead_Notes(Out_Trade_No, Head_Notes, out msg);
                if (bol)
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = msg });
                }
                return Json(new { code = 1, msg = "ERROR", content = msg });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }


        /// <summary>
        /// 修改订单行供货商/商品数量信息
        /// </summary>
        /// <param name="Orders_Lines_ID"></param>
        /// <param name="SuppliersID"></param>
        /// <param name="GoodsCount"></param>
        /// <returns></returns>
        [Route("EditLineSuppliersInfo")]
        [HttpPost]
        public IHttpActionResult EditLineSuppliersInfo(int Orders_Lines_ID, int SuppliersID, int GoodsCount)
        {
            try
            {
                string msg;
                var bol = soService.EditLineSuppliersInfo(Orders_Lines_ID, SuppliersID,GoodsCount,out msg);
                if (bol)
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = msg });
                }
                return Json(new { code = 1, msg = "ERROR", content = msg });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 查询商品的供货商信息
        /// </summary>
        /// <param name="GoodsID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [Route("GetGoodsSuppliersList")]
        [HttpGet]
        public IHttpActionResult GetGoodsSuppliersList(int GoodsID)
        {
            try
            {
                string msg;
                var List = soService.GetGoodsSuppliersList(GoodsID, out msg);
                if (msg.Equals("SUCCESS"))
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = List });
                }
                return Json(new { code = 1, msg = "ERROR", content = msg });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }
    }
}
