using DTO.ShopOrder;
using Service;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace LELAdmin.Controllers
{
    /// <summary>
    /// 后台订单管理API
    /// </summary>
    [RoutePrefix("api/Orders")]
    [Authorize]
    public class OrderController : BaseController
    {
        private AdminOrderService aoService = new AdminOrderService();
        private ShopOrderService ShopBLL = new ShopOrderService();

        #region 订单头列表操作
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="seachParams"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("GetListOrder")]
        [HttpPost]
        public IHttpActionResult GetListOrder(OrderSeachParams seachParams)
        {
            try
            {
                var list = ShopBLL.GetListOrder(seachParams, out int Count);

                return Json(new { code = 0, msg = "SUCCESS", content = list, count = Count });
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
                var list = ShopBLL.GetOrderDetails(OrderNo);

                return Json(new { code = 0, msg = "SUCCESS", content = list });
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
        public IHttpActionResult EditReceiptInfo(EditReceiptInfo dto)
        {
            try
            {
                var bol = aoService.EditReceiptInfo(dto, out string msg);

                if (bol)
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = msg });
                }
                else
                {
                    return Json(new { code = 1, msg = "ERROR", content = msg });
                }

            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }



        /// <summary>
        /// 更新订单头状态
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
                var bol = ShopBLL.UpdateOrderStatus(Out_Trade_No, Status, GetLoginInfo(), out string msg);

                if (bol)
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = msg });
                }
                else
                {
                    return Json(new { code = 1, msg = "ERROR", content = msg });
                }

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
        /// <param name="msg"></param>
        /// <returns></returns>
        [Route("EditOrderHead_Notes")]
        [HttpPost]
        public IHttpActionResult EditOrderHead_Notes(string Out_Trade_No, string Head_Notes)
        {
            try
            {
                var bol = aoService.EditOrderHead_Notes(Out_Trade_No, Head_Notes, out string msg);

                if (bol)
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = msg });
                }
                else
                {
                    return Json(new { code = 1, msg = "ERROR", content = msg });
                }

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
                var list = aoService.GetGoodsSuppliersList(GoodsID, out string msg);

                return Json(new { code = 0, msg = msg, content = list });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 批量修改订单行供货商/商品数量
        /// </summary>
        /// <param name="List"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [Route("BatchEditLinesInfo")]
        [HttpPost]
        public IHttpActionResult BatchEditLinesInfo(List<EditLinesInfo> List)
        {
            try
            {
                var bol = ShopBLL.BatchEditLinesInfo(List, GetLoginInfo(), out string msg);
                if (bol)
                {
                    return Json(new { code = 0, msg = msg, content = "修改成功" });
                }
                else
                {
                    return Json(new { code = 1, msg = "ERROR", content = msg });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }


        #endregion

        #region 订单行列表操作 (暂弃)

        /// <summary>
        /// 查询订单行列表
        /// </summary>
        /// <param name="seachParams"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        //[Route("GetOrderLineList")]
        //[HttpPost]
        //public IHttpActionResult GetOrderLineList(OrderSeachParams seachParams)
        //{
        //    try
        //    {
        //        var list = aoService.GetOrderLineList(seachParams, out int Count);

        //        return Json(new { code = 0, msg = "SUCCESS", content = list, count = Count });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
        //    }
        //}

        /// <summary>
        /// 查询订单行明细
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        //[Route("GetOrderLineDetail")]
        //[HttpGet]
        //public IHttpActionResult GetOrderLineDetail(int ID)
        //{
        //    try
        //    {
        //        var model = aoService.GetOrderLineDetail(ID, out string msg);
        //        if (msg.Equals("SUCCESS"))
        //        {
        //            return Json(new { code = 0, msg = "SUCCESS", content = model});
        //        }
        //        return Json(new { code = 1, msg = "ERROR", content = model});
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
        //    }
        //}
        #endregion
    }
}
