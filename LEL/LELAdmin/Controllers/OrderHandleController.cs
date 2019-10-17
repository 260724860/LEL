using Common;
using DTO.Goods;
using DTO.ShopOrder;
using DTO.Suppliers;
using LELAdmin.Models;
using Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using static DTO.Common.Enum;

namespace LELAdmin.Controllers
{

    public class OrderHandleController : BaseController
    {
        private GoodsService GoodsBLL = new GoodsService();
        private ShopOrderService ShopOrderBLL = new ShopOrderService();
        /// <summary>
        /// 添加供应商价格
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <param name="GoodsID"></param>
        /// <param name="Price"></param>
        /// <returns></returns>
        [HttpPost, Route("api/OrderHandle/AddSupplierGoodsPrice/")]
        public IHttpActionResult AddSupplierGoodsPrice(int SupplierID, int GoodsID, decimal Price, int IsDefalut)
        {
            var result = GoodsBLL.AddSupplierGoodsPrice(SupplierID, GoodsID, Price, IsDefalut, out string Msg);
            if (result > 0)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", Msg));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, "ERROR", Msg));

            }
        }

        /// <summary>
        /// 获取供应商价格
        /// </summary>
        /// <param name="KeyWords"></param>
        /// <param name="Offset"></param>
        /// <param name="Rows"></param>
        /// <param name="GoodsID"></param>
        /// <param name="SuppliersID"></param>
        /// <returns></returns>
        [HttpPost, Route("api/OrderHandle/GetSupplierGoodsPric/")]
        public IHttpActionResult GetSupplierGoodsPric(string KeyWords, int Offset, int Rows, int? GoodsID, int? SuppliersID)
        {
            var result = GoodsBLL.GetSupplierGoodsPriceList(KeyWords, Offset, Rows, GoodsID, SuppliersID, out int Count);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result, Count));

        }

        /// <summary>
        /// 修改供应商价格
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Price"></param>
        /// <param name="IsDeleted"></param>
        /// <param name="IsDefault"></param>
        /// <returns></returns>
        [HttpPost, Route("api/OrderHandle/UpdateSupplierPrice/")]
        public IHttpActionResult UpdateSupplierPrice(int ID, decimal Price, int IsDeleted, int IsDefault)
        {

            var result = GoodsBLL.UpdateSupplierPrice(ID, Price, IsDeleted, IsDefault, GetLoginInfo().UserID);
            if (result)
            {
                return Json(new { code = 0, msg = "SUCCESS", content = result });
            }
            else
            {
                return Json(new { code = 1, msg = "操作失败或权限不足", content = result });
            }
        }

        /// <summary>
        /// 修改订单行状态，拆单 待优化
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="OrdersLinesID"></param>
        /// <param name="Notes"></param>
        /// <param name="AdminID"></param>
        /// <param name="SuppliersID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpPost, Route("api/OrderHandle/UpdateOrderLineStatus/")]
        public IHttpActionResult UpdateOrderLineStatus(List<UpdateOrderLineParamas> UpdateParams)
        {
            
            int AdminID = GetLoginInfo().UserID;
        
            var result = ShopOrderBLL.UpdateOrderLineStatus(UpdateParams, out string Msg, AdminID, 0);
            if (!result)
            {
                  
                return Json(JRpcHelper.AjaxResult(1, Msg, UpdateParams));
            }

     
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", UpdateParams));
        }

     
       /// <summary>
       /// 修改供应商订单行
       /// </summary>
       /// <param name="param"></param>
       /// <returns></returns>
        [HttpPost, Route("api/OrderHandle/UpdateOrderLineStatusBySupplier/")]
        public IHttpActionResult UpdateOrderLineStatusBySupplier(UpdateOrderLineStatusBySupplierDto param)
        {
            int AdminID = GetLoginInfo().UserID;
            int[] limit = { 100, 2,3 };
            if (!((IList)limit).Contains(param.Status))
            {
                return Json(JRpcHelper.AjaxResult(1, "请输入正常的状态码限定范围[100, 2,3]", param.Status));
            }
            var result = ShopOrderBLL.UpdateOrderLineStatusBySupplier(param.OrderNO, AdminID, param.SuppliersID, param.Status, out string Msg);
            if (!result)
            {
               
                return Json(JRpcHelper.AjaxResult(1, Msg, param.OrderNO));
            }
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", param.OrderNO));
        }
        /// <summary>
        /// 修改订单信息
        /// </summary>
        /// <param name="orderEditInfo"></param>
        /// <returns></returns>
        [HttpPost, Route("api/OrderHandle/UpdateOrderInfo/")]
        public IHttpActionResult UpdateOrderInfo(OrderEditInfo orderEditInfo)
        {
            int AdminID = GetLoginInfo().UserID;
            if (orderEditInfo == null)
            {
                return Json(JRpcHelper.AjaxResult(1, "参数错误", orderEditInfo));
            }
            var result = ShopOrderBLL.UpdateOrderInfo(orderEditInfo, AdminID);
            if (result)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, "FAIL", result));
            }
        }

        /// <summary>
        /// 获取最新订单推送消息
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="UserType">用户类型 1商户 2供货商 3总部</param>
        /// <returns></returns>
        [HttpPost, Route("api/OrderHandle/GetPushMsg/")]
        public IHttpActionResult GetPushMsg(int UserID, int UserType)
        {
            //var result = new OtherService().GetPushMsg(UserID, UserType);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", 1));
        }

        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="Supperid"></param>
        /// <returns></returns>
        [HttpGet, Route("api/OrderHandle/AddPulshMsg/")]
        public IHttpActionResult AddPulshMsg(int Supperid, string OrderNO, int MsgType)
        {
            var result = new OtherService().UpdatePushMsg(Supperid, OrderNO, 2, MsgType);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }

        /// <summary>
        /// 订单行修改记录
        /// </summary>
        /// <param name="logParams"></param>
        /// <returns></returns>
        [HttpPost, Route("api/OrderHandle/GetOrderLineLogList/")]
        public IHttpActionResult GetOrderLineLogList(OrderLineLogParams logParams)
        {
            //  , out int Count
            var result = new LogService().GetOrderLineLogList(logParams.SeachOptions, logParams.AdminID, logParams.LinesRecordID, out int Count);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result, Count));
        }

        /// <summary>
        ///  订单头修改记录
        /// </summary>
        /// <param name="logParams"></param>
        /// <returns></returns>
        [HttpPost, Route("api/OrderHandle/GetOrderHeadLogList/")]
        public IHttpActionResult GetOrderHeadLogList(OrderHeadLogParams logParams)
        {
            var result = new LogService().GetOrderHeadLogList(logParams.SeachOptions, logParams.AdminID, logParams.OrderHeadID, out int Count);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result, Count));
        }

        /// <summary>
        /// 获取订单打包数
        /// </summary>
        /// <param name="OrderHeadID"></param>
        /// <param name="PacetCount"></param>
        /// <returns></returns>
        [Route("GetOrderPackCountsList")]
        [HttpPost]
        public IHttpActionResult GetOrderPackCountsList(int OrderHeadID)
        {
            var reuslt = ShopOrderBLL.GetOrderPackCountsList(OrderHeadID, null);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", reuslt));
        }

        /// <summary>
        /// 保存订单 接口
        /// </summary>

        /// <returns></returns>
        [HttpPost, Route("api/ShopOrder/OrderSaveInterface/")]
        public IHttpActionResult OrderSaveInterface(OrderSaveParamesExtend ParamasData)
        {
            if (ParamasData == null)
            {
                return Json(JRpcHelper.AjaxResult(0, "参数错误", ParamasData));
            }
            int[] OrderTypes = { 1, 2, 3 }; int[] ExpressTypes = { 1, 2 };
            //if (GetUserID() == -1)
            //{
            //    return Json(JRpcHelper.AjaxResult(1, "未通过审核", GetUserID()));
            //}
            if (!OrderTypes.Contains(ParamasData.OrderInfo.OrderType) || !ExpressTypes.Contains(ParamasData.OrderInfo.ExpressType))
            {
                return Json(JRpcHelper.AjaxResult(1, "参数错误,请检查", ParamasData.OrderInfo.OrderType.ToString() + "," + ParamasData.OrderInfo.ExpressType.ToString()));
            }
            if (ParamasData.OrderInfo.ExpressType == 2)
            {
                if (!ParamasData.OrderInfo.PickupTime.HasValue)
                {
                    try
                    {
                        ParamasData.OrderInfo.PickupTime = Convert.ToDateTime(ParamasData.OrderInfo.PickupTimeStr);
                    }
                    catch (Exception ex)
                    {
                        return Json(JRpcHelper.AjaxResult(1, ex.Message + "前端传入时间：【" + ParamasData.OrderInfo.PickupTimeStr + "】", ex));
                    }
                }
            }
           // ParamasData.OrderInfo.UserID = GetUserID();

            string msg;
            List<ShopCartDto> FailCartList;
            var result = new ShopOrderService().OrderSave(ParamasData, out msg);
            if (result.Substring(0, 3) != "LEL")
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, msg, result));
            }
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        
            return null;

        }

        /// <summary>
        /// 获取订单详细内供应商状态
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        [HttpPost, Route("api/ShopOrder/GetOrderSupplierList/")]
        public IHttpActionResult GetOrderSupplierList(OrderSupplierListParams Params)
        {
            var result=  ShopOrderBLL.GetOrderSupplierList(Params,out int Count);
            if(result == null||result.Count<=0)
            {
                return Json(JRpcHelper.AjaxResult(1, "请输入正确得单号", result));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result, Count));
            }
        }
        /// <summary>
        /// 获取订单详细
        /// </summary>
        /// <param name="Params"></param>
        /// <returns></returns>
        [HttpPost, Route("api/ShopOrder/GetOrderDeatils/")]
        public IHttpActionResult GetOrderDeatils(OrderDeatilsParams Params)
        {
            var result = ShopOrderBLL.GetOrderDeatils(Params, out int Count);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result, Count));
        }

      /// <summary>
      /// 新增缺货列表
      /// </summary>
      /// <param name="dto"></param>
      /// <returns></returns>
        [AllowAnonymous]
        [Route("api/ShopOrder/AddBackOrder")]
        [HttpPost]
        public IHttpActionResult AddBackOrder(AddBackOrderDto dto
            )
        {
           // return Json(JRpcHelper.AjaxResult(0, "新增成功", dto.Barcode));

            if (dto.GoodsName == null) dto.GoodsName = "";
            if (dto.PurchasePrice == null) dto.PurchasePrice = "";
            if (dto.SellingPrice == null) dto.SellingPrice = "";
            if (dto.Specifications == null) dto.Specifications = "";
            if (dto.Merchant == null) dto.Merchant = "";
            if (dto.MerchantCode == null) dto.MerchantCode = "";
            if (dto.Classify == null) dto.Classify = "";
            if (dto.ClassifyCode == null) dto.ClassifyCode = "";
            if (dto.Flag == null) dto.Flag = "";
            if (dto.Remark == null) dto.Remark = "";
            var result = new BackOrderService().AddBackOrder(dto.Barcode, dto.GoodsName, dto.PurchasePrice, dto.SellingPrice,
             dto.Specifications, dto.GoodsCount, dto.Merchant, dto.MerchantCode, dto.Classify, dto.ClassifyCode
            , dto.UsersID, dto.Flag, dto.Remark, dto.InStock,dto.IsDeleted, dto.ID ,out string Msg);
            if (result)
            {
                return Json(JRpcHelper.AjaxResult(0, "新增成功", null));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, Msg, null));
            }
        }

        /// <summary>
        /// 获取半小时未结单订单
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("api/ShopOrder/GetWDJOrderList")]
        [HttpPost]
        public async Task< IHttpActionResult> GetWDJOrderList()
        {
            var result = await ShopOrderBLL.GetWDJOrderList();
            return Json(JRpcHelper.AjaxResult(0, "新增成功", result));
        }

        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="GoodsID">商品ID</param>
        /// <param name="GoodValueID">商品属性ID</param>
        /// <param name="GoodsCount">商品数量</param>
        /// <param name="cumulation">商品数量是否累加</param>
        /// <param name="ReturnCount">购物车退货数量</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("api/OrderHandle/AddCart/")]
        public IHttpActionResult AddCart(int GoodsID, List<AddGoodsValues> GoodValueID, int GoodsCount, bool cumulation,int UserID, int? ReturnCount = 0)
        {           
            if (GoodsCount <= 0&& ReturnCount==0)
            {
                return Json(JRpcHelper.AjaxResult(1, "GoodsCount或者 ReturnCount参数错误", GoodsCount));
            }
            string Msg;
            var result =new  ShopOrderService().AddCart(GoodsID, GoodValueID, GoodsCount, UserID, cumulation, out Msg, ReturnCount,1);
            if (result > 0)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            return Json(JRpcHelper.AjaxResult(1, Msg, result));
        }

        /// <summary>
        /// 保存订单 （从购物车获取）后台提交
        /// </summary>     
        /// <returns></returns>
        [HttpPost, Route("api/OrderHandle/OrderSave/")]
        [AllowAnonymous]
        public IHttpActionResult OrderSave(OrderSaveParams Data)
        {
            int[] OrderTypes = { 1, 2, 3 }; int[] ExpressTypes = { 1, 2 };
            //if (GetUserID() == -1)
            //{
            //    return Json(JRpcHelper.AjaxResult(1, "未通过审核", GetUserID()));
            //}
            if (!OrderTypes.Contains(Data.OrderType) || !ExpressTypes.Contains(Data.ExpressType))
            {
                return Json(JRpcHelper.AjaxResult(1, "参数错误,请检查", Data.OrderType.ToString() + "," + Data.ExpressType.ToString()));
            }

            if (Data.ExpressType == 2)
            {
                if (!Data.PickupTime.HasValue&&!string.IsNullOrEmpty(Data.PickupTimeStr))
                {
                    try
                    {
                        Data.PickupTime = Convert.ToDateTime(Data.PickupTimeStr);
                    }
                    catch (Exception ex)
                    {
                        return Json(JRpcHelper.AjaxResult(1, ex.Message + "前端传入时间：【" + Data.PickupTimeStr + "】", ex));
                    }
                }
            }
            //if (Data.PickupTime > new DateTime(2019, 09, 17))
            //{
            //    return Json(JRpcHelper.AjaxResult(1, "取货时间格式错误", GetUserID()));
            //}
            //if (Data.PickupTime > DateTime.Now.AddDays(3))
            //{
            //    return Json(JRpcHelper.AjaxResult(1, "取货时间格式错误,取货时间限制48小时内", GetUserID()));
            //}
            //if(Data.PickupTime <new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day+1))
            //{
            //    return Json(JRpcHelper.AjaxResult(1, "取货时间错误，请重新选择", GetUserID()));
            //}

           // Data.UserID = GetUserID();
           
            string msg;
            List<ShopCartDto> FailCartList;
            var result =new ShopOrderService().OrderSave(Data, out msg, out FailCartList,1);
            if (result != 0)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, msg, result));
            }
            return null;

        }

        /// <summary>
        /// 获取购物车
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("api/ShopOrder/GetCartList/")]
        public IHttpActionResult GetCartList(int UserID)
        {
            //if (GetUserID() == -1)
            //{
            //    return Json(JRpcHelper.AjaxResult(1, "未通过审核", GetUserID()));
            //}
            var result =new ShopOrderService().GetCartList(UserID,1);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }
        /// <summary>
        ///  删除购物车
        /// </summary>
        /// <param name="CartIDList"></param>
        /// <returns></returns>
        [HttpPost, Route("api/ShopOrder/DeleteCart/")]
        public IHttpActionResult DeleteCart(List<int> CartIDList)
        {
            //if (GetUserID() == -1)
            //{
            //    return Json(JRpcHelper.AjaxResult(1, "未通过审核", GetUserID()));
            //}
            if (CartIDList == null)
            {
                return Json(JRpcHelper.AjaxResult(1, "参数错误,请检查", CartIDList));
            }
            var result = new ShopOrderService().DeleteCart(CartIDList, null);
            if (result)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            return Json(JRpcHelper.AjaxResult(1, "Fail", result));
        }
    }
}
