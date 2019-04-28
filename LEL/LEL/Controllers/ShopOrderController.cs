using Common;
using DTO.Common;
using DTO.ShopOrder;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEL.Controllers
{
    /// <summary>
    /// 商城订单
    /// </summary>
    [Authorize]
    public class ShopOrderController : ApiController
    {
        ShopOrderService shopOrderService = new ShopOrderService();
        OtherService OtherService = new OtherService();
        private int UserID { get; set; }
        
        private int GetUserID()
        {
            UserID = Convert.ToInt32(User.Identity.Name.Split(',')[1]);
            return UserID;
        }
        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="GoodsID">商品ID</param>
        /// <param name="GoodValueID">商品属性ID</param>
        /// <param name="GoodsCount">商品数量</param>
        /// <returns></returns>
        [HttpPost, Route("api/ShopOrder/AddCart/")]
        public IHttpActionResult AddCart(int GoodsID,int GoodValueID,int GoodsCount)
         {            
             string Msg;
             var result= shopOrderService.AddCart(GoodsID, GoodValueID, GoodsCount,GetUserID(), out Msg);
            if (result>0)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            return Json(JRpcHelper.AjaxResult(1, "Fail", result));
        }

        /// <summary>
        /// 获取购物车
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("api/ShopOrder/GetCartList/")]
        public IHttpActionResult GetCartList()
        {
             var result = shopOrderService.GetCartList(GetUserID());
             return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }

        /// <summary>
        /// 保存订单
        /// </summary>
        /// <param name="orderGoods"></param>
        /// <returns></returns>
        [HttpPost, Route("api/ShopOrder/OrderSave/")]
        public IHttpActionResult OrderSave(List<OrderGoodsList> orderGoodsList,int AddressID)
        {
            string msg;
            var result= shopOrderService.OrderSave(orderGoodsList, GetUserID(), AddressID, out msg);
            //using (Entities ctx = new Entities())
            //{
            //    var GoodsIDArry = orderGoodsList.Select(k => k.GoodsID).ToArray();
            //    var GoodsValueIDArry = orderGoodsList.Select(k => k.GoodsValueID).ToArray();
            //    var GoodsList = ctx.le_goods_value_mapping.Where(s => GoodsIDArry.Contains(s.GoodsID)&& GoodsValueIDArry.Contains(s.GoodsValueID))
            //        .Select(s=>new {s.GoodsID,s.GoodsValueID,s.le_goods.SpecialOffer })
            //        .ToList();
            // .Select(s => new { s.GoodsID,s.le_goods.SpecialOffer ,s.GoodsValueID}).ToList();


            //foreach (var model in orderGoodsList)
            //{
            //    le_orders_lines linesModel = new le_orders_lines();
            //    linesModel.CreateTime = DateTime.Now;
            //    linesModel.GoodsCount = model.GoodsCount;
            //    linesModel.GoodsValueID = model.GoodsValueID;
            //    linesModel.
            //}
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            return null;

        }

        /// <summary>
        /// 新增收货地址
        /// </summary>
        /// <param name="ReceiveName">收货人</param>
        /// <param name="ReceivePhone">电话</param>
        /// <param name="ReceiveArea">收货地区</param>
        /// <param name="ReceiveAddress">收货地址详细</param>
        /// <param name="DefaultAddr">1默认地址</param>
        /// <returns></returns>
        [HttpPost, Route("api/ShopOrder/AddAddress/")]
        public IHttpActionResult AddAddress(string ReceiveName, string ReceivePhone, string ReceiveArea, string ReceiveAddress, int DefaultAddr)
        {
            var result= OtherService.AddAddress(GetUserID(), ReceiveName, ReceivePhone, ReceiveArea, ReceiveAddress, DefaultAddr);
            if(result>0)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, "新增收货地址失败，请稍后再试", result));
            }
        }
        /// <summary>
        /// 修改收货地址
        /// </summary>
        /// <param name="AddressID">收货地址ID</param>
        /// <param name="ReceiveName">收货人</param>
        /// <param name="ReceivePhone">电话</param>
        /// <param name="ReceiveArea">收货地区</param>
        /// <param name="ReceiveAddress">收货地址详细</param>
        /// <param name="DefaultAddr">1默认地址</param>
        /// <param name="Status">状态 0 删除 1 启用</param>
        /// <returns></returns>
        [HttpPost, Route("api/ShopOrder/UpdateAddress/")]
        public IHttpActionResult  UpdateAddress(int AddressID, string ReceiveName, string ReceivePhone, string ReceiveArea, string ReceiveAddress, int DefaultAddr, int Status)
        {
            var result = OtherService.UpdateAddress(GetUserID(), ReceiveName, ReceivePhone, ReceiveArea, ReceiveAddress, DefaultAddr, Status);
            if (result)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, "修改收货地址失败，请稍后再试", result));
            }
        }
      
        /// <summary>
        /// 获取收货地址
        /// </summary>
        /// <param name="options">查询参数</param>
        /// <returns></returns>
        [HttpPost, Route("api/ShopOrder/GetAddressList/")]
        public IHttpActionResult GetAddressList(SeachOptions options)
        {
            int Count;
            var result = OtherService.GetAddressList(options, GetUserID(),out Count);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost, Route("api/ShopOrder/GetListOrder/")]
        public IHttpActionResult GetListOrder(OrderSeachParams options )
        {
            options.UserID = GetUserID();
            var result = shopOrderService.GetListOrder(options, out int Count);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }
        /// <summary>
        /// 获取订单详细
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost, Route("api/ShopOrder/GetOrderDetails/")]
        public IHttpActionResult GetOrderDetails(string OrderNO)
        { 
            var result = shopOrderService.GetOrderDetails(OrderNO);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }
    }
}
