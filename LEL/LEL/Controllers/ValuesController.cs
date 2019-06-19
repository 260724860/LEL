using System.Web.Http;

namespace LEL.Controllers
{
    public class ValuesController : ApiController
    {
        /// <summary>
        /// 修改订单行状态，拆单
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="OrdersLinesID"></param>
        /// <param name="Notes"></param>
        /// <param name="AdminID"></param>
        /// <param name="SuppliersID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        //[HttpGet, Route("api/OrderHandle/UpdateOrderLineStatus/")]
        //public IHttpActionResult UpdateOrderLineStatus(int Status, int OrdersLinesID,string OrderNo, int Admin, string Notes = "", int SuppliersID = 0)
        //{
        //    var result = new ShopOrderService().UpdateOrderLineStatus(Status, OrdersLinesID, OrderNo, Notes, out string Msg, Admin, SuppliersID);
        //    if (result)
        //    {
        //        return Json(JRpcHelper.AjaxResult(0, "SUCCESS", Msg));
        //    }
        //    else
        //    {
        //        return Json(JRpcHelper.AjaxResult(1, "ERROR", Msg));
        //    }
        //}


        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
