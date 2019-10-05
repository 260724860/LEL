using Common;
using DTO.Common;
using DTO.Goods;
using DTO.ShopOrder;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using static DTO.Common.Enum;

namespace LEL.Controllers
{
    /// <summary>
    /// 商城订单
    /// </summary>
    [Authorize]
    public class ShopOrderController : BaseApiController
    {
        ShopOrderService shopOrderService = new ShopOrderService();
        OtherService OtherService = new OtherService();
        private int UserID { get; set; }

        private int GetUserID()
        {
            UserID = Convert.ToInt32(User.Identity.Name.Split(',')[1]);
            int Status = Convert.ToInt32(User.Identity.Name.Split(',')[2]);
            if (Status == 0)
            {
                return -1;
            }
            return UserID;
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
        [HttpPost, Route("api/ShopOrder/AddCart/")]
        public IHttpActionResult AddCart(int GoodsID, List<AddGoodsValues> GoodValueID, int GoodsCount, bool cumulation,int? ReturnCount=1)
        {
            if (GetUserID() == -1)
            {
                return Json(JRpcHelper.AjaxResult(1, "未通过审核", GetUserID()));
            }
            if (GoodsCount <= 0)
            {
                return Json(JRpcHelper.AjaxResult(1, "GoodsCount 参数错误", GoodsCount));
            }
            string Msg;
            var result = shopOrderService.AddCart(GoodsID, GoodValueID, GoodsCount, GetUserID(), cumulation, out Msg, ReturnCount);
            if (result > 0)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            return Json(JRpcHelper.AjaxResult(1, Msg, result));
        }



        /// <summary>
        ///  删除购物车
        /// </summary>
        /// <param name="CartIDList"></param>
        /// <returns></returns>
        [HttpPost, Route("api/ShopOrder/DeleteCart/")]
        public IHttpActionResult DeleteCart(List<int> CartIDList)
        {
            if (GetUserID() == -1)
            {
                return Json(JRpcHelper.AjaxResult(1, "未通过审核", GetUserID()));
            }
            if (CartIDList == null)
            {
                return Json(JRpcHelper.AjaxResult(1, "参数错误,请检查", CartIDList));
            }
            var result = shopOrderService.DeleteCart(CartIDList, GetUserID());
            if (result)
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
            //if (GetUserID() == -1)
            //{
            //    return Json(JRpcHelper.AjaxResult(1, "未通过审核", GetUserID()));
            //}
            var result = shopOrderService.GetCartList(GetUserID());
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }

        /// <summary>
        /// 保存订单 （从购物车获取）
        /// </summary>     
        /// <returns></returns>
        [HttpPost, Route("api/ShopOrder/OrderSave/")]
        public IHttpActionResult OrderSave(OrderSaveParams Data)
        {
            int[] OrderTypes = { 1, 2, 3 }; int[] ExpressTypes = { 1, 2 };
            if (GetUserID() == -1)
            {
                return Json(JRpcHelper.AjaxResult(1, "未通过审核", GetUserID()));
            }
            if (!OrderTypes.Contains(Data.OrderType) || !ExpressTypes.Contains(Data.ExpressType))
            {
                return Json(JRpcHelper.AjaxResult(1, "参数错误,请检查", Data.OrderType.ToString() + "," + Data.ExpressType.ToString()));
            }

            if (Data.ExpressType == 2)
            {
                if (!Data.PickupTime.HasValue)
                {
                    try
                    {
                        Data.PickupTime = Convert.ToDateTime(Data.PickupTimeStr);
                    }
                    catch (Exception ex)
                    {
                        return Json(JRpcHelper.AjaxResult(1, ex.Message+"前端传入时间：【"+ Data.PickupTimeStr + "】", ex));
                    }
                }
            }
            //if (Data.PickupTime > new DateTime(2019, 09, 17))
            //{
            //    return Json(JRpcHelper.AjaxResult(1, "取货时间格式错误", GetUserID()));
            //}
            if (Data.PickupTime > DateTime.Now.AddDays(3))
            {
                return Json(JRpcHelper.AjaxResult(1, "取货时间格式错误,取货时间限制48小时内", GetUserID()));
            }
            if (Data.PickupTime < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1))
            {
                return Json(JRpcHelper.AjaxResult(1, "取货时间错误，请重新选择", GetUserID()));
            }

            Data.UserID = GetUserID();
            using (Entities ctx=new Entities())
            {
                string Classify = ctx.le_users.Where(s => s.UsersID == UserID).Select(s=>s.Classify).FirstOrDefault();
                string Environment = "";
                string url = Request.RequestUri.Host.ToString();
                var SubdomainArrty = url.Split('.');
                if (SubdomainArrty.Length > 0)
                {
                    Environment = SubdomainArrty[0];
                }
                //if(Classify=="lelshoptest"&& Environment== "lelshoptest")
                //{
                //    return Json(JRpcHelper.AjaxResult(1, "下单失败，只能从lelshoptest2.muguxia.cn/Content网址下单", Classify));
                //}

            }
            string msg;
            List<ShopCartDto> FailCartList;
            var result = shopOrderService.OrderSave(Data, out msg, out FailCartList);
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
        /// 保存订单 接口
        /// </summary>

        /// <returns></returns>
        //[HttpPost, Route("api/ShopOrder/OrderSaveInterface/")]
        //public IHttpActionResult OrderSaveInterface(OrderSaveParamesExtend ParamasData)
        //{
        //    int[] OrderTypes = { 1, 2, 3 }; int[] ExpressTypes = { 1, 2 };
        //    if (GetUserID() == -1)
        //    {
        //        return Json(JRpcHelper.AjaxResult(1, "未通过审核", GetUserID()));
        //    }
        //    if (!OrderTypes.Contains(ParamasData.OrderInfo.OrderType) || !ExpressTypes.Contains(ParamasData.OrderInfo.ExpressType))
        //    {
        //        return Json(JRpcHelper.AjaxResult(1, "参数错误,请检查", ParamasData.OrderInfo.OrderType.ToString() + "," + ParamasData.OrderInfo.ExpressType.ToString()));
        //    }
        //    if (ParamasData.OrderInfo.ExpressType == 2)
        //    {
        //        if (!ParamasData.OrderInfo.PickupTime.HasValue)
        //        {
        //            try
        //            {
        //                ParamasData.OrderInfo.PickupTime = Convert.ToDateTime(ParamasData.OrderInfo.PickupTimeStr);
        //            }
        //            catch (Exception ex)
        //            {
        //                return Json(JRpcHelper.AjaxResult(1, ex.Message + "前端传入时间：【" + ParamasData.OrderInfo.PickupTimeStr + "】", ex));
        //            }

        //        }
        //    }
        //    ParamasData.OrderInfo.UserID = GetUserID();
            
        //    string msg;
        //    List<ShopCartDto> FailCartList;
        //    var result = shopOrderService.OrderSave(ParamasData, out msg);
        //    if (result.Substring(0,3)!= "LEL")
        //    {
        //        return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        //    }
        //    else
        //    {
        //        return Json(JRpcHelper.AjaxResult(1, msg, result));
        //    }
        //    return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        //    return null;

        //}
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
            if (GetUserID() == -1)
            {
                return Json(JRpcHelper.AjaxResult(1, "未通过审核", GetUserID()));
            }
            var result = OtherService.AddAddress(GetUserID(), ReceiveName, ReceivePhone, ReceiveArea, ReceiveAddress, DefaultAddr);
            if (result > 0)
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
        public IHttpActionResult UpdateAddress(int AddressID, string ReceiveName, string ReceivePhone, string ReceiveArea, string ReceiveAddress, int DefaultAddr, int Status)
        {
            if (GetUserID() == -1)
            {
                return Json(JRpcHelper.AjaxResult(1, "未通过审核", GetUserID()));
            }
            var result = OtherService.UpdateAddress(AddressID, ReceiveName, ReceivePhone, ReceiveArea, ReceiveAddress, DefaultAddr, Status);
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
            if (GetUserID() == -1)
            {
                return Json(JRpcHelper.AjaxResult(1, "未通过审核", GetUserID()));
            }
            int Count;
            var result = OtherService.GetAddressList(options, GetUserID(), out Count);
            result = result.Where(s => s.Status == 1).ToList();
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost, Route("api/ShopOrder/GetListOrder/")]
        public IHttpActionResult GetListOrder(OrderSeachParams options)
        {
            //if (GetUserID() == -1)
            //{
            //    return Json(JRpcHelper.AjaxResult(1, "未通过审核", GetUserID()));
            //}
            options.UserID = GetUserID();
            var result = shopOrderService.GetListOrder(options, out int Count);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result, Count));
        }

        /// <summary>
        /// 获取订单详细
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost, Route("api/ShopOrder/GetOrderDetails/")]
        public IHttpActionResult GetOrderDetails(string OrderNO)
        {
            //if (GetUserID() == -1)
            //{
            //    return Json(JRpcHelper.AjaxResult(1, "未通过审核", GetUserID()));
            //}
            var result = shopOrderService.GetOrderDetails(OrderNO);
            var filter = new List<OrderDetail>();
            foreach (var model in result)
            {
                model.SuppliersName = "******";
                model.DefultSupplier = "******";
                filter.Add(model);
            }
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", filter));
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="OrderNO">订单号码</param>
        /// <returns></returns>
        [HttpPost, Route("api/ShopOrder/CancelOrder/")]
        public IHttpActionResult CancelOrder(string OrderNO)
        {
            if (GetUserID() == -1)
            {
                return Json(JRpcHelper.AjaxResult(1, "未通过审核", GetUserID()));
            }
            var result = shopOrderService.UpdateOrderStatus(OrderNO, 5, GetLoginInfo(), out string msg);
            if (result)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", msg));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, "取消订单失败", msg));
            }
        }


        /// <summary>
        /// 获取当前时间段内下单数
        /// </summary>
        /// <param name="TimeSlot"></param>
        /// <returns></returns>
         [HttpPost, Route("api/ShopOrder/GetOrderLimitByTimeSlot/")]
        public async Task<IHttpActionResult> GetOrderLimitByTimeSlot(DateTime TimeSlot)
        {
            var result= await new OrdersTimeLimitService().GetOrderLimitForTimeSlot(TimeSlot);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
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
            , dto.UsersID, dto.Flag, dto.Remark, dto.InStock, dto.ID,dto.IsDeleted, out string Msg);
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
        /// 获取自采列表
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="IsExport">是否导出Excel</param>
        /// <param name="OrderByType">排序</param> 
        /// <param name="Flag">标志</param>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="MerchantCode">供应商编码</param>
        /// <param name="InStock">有货/无货</param>
        /// <param name="OrderByType"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("api/ShopOrder/GetBackOrderList")]
        [HttpPost]
        public IHttpActionResult GetBackOrderList(BackOrderListParas dto )
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("/") + "UploadFile/商品自采导出格式.xlsx";
            string FileName = dto.UserID.ToString()+"自采列表"+ DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
            string SavPath = System.Web.HttpContext.Current.Server.MapPath("/") + @"UploadFile\export\" + FileName;

            var result = new BackOrderService().GetbackOrderList(dto.UserID, dto.Flag, dto.BeginTime, dto.EndTime, dto.MerchantCode, dto.OrderByType, dto.InStock);

            if (dto.IsExport)
            {
                var dt = DataTableToListHelper.ToDataTable(result);
                dt.Columns["BarCode"].ColumnName = "条码";
                dt.Columns["GoodsName"].ColumnName = "商品名";
                dt.Columns["Specifications"].ColumnName = "规格";
                dt.Columns["Merchant"].ColumnName = "货商";
                dt.Columns["GoodsCount"].ColumnName = "数量";

                dt.Columns.Remove("Classify");
                dt.Columns.Remove("ClassifyCode");
                dt.Columns.Remove("Remark");
                dt.Columns.Remove("InStock");
                dt.Columns.Remove("MerchantCode");
                dt.Columns.Remove("CreateTime");
                dt.Columns.Remove("UpdateTime");
                dt.Columns.Remove("Flag");
                dt.Columns.Remove("UsersID");
                dt.Columns.Remove("PurchasePrice");
                dt.Columns.Remove("SellingPrice");
                dt.Columns.Remove("ID");
                dt.Columns.Remove("IsDeleted");
                List<DtoImportExcel> DtList = new List<DtoImportExcel>();

                DtList.Add(new DtoImportExcel { dt = dt, SheetNmae = "自采商品列表" });

                ExcelHelper.DataTableToExcel("", DtList, true, SavPath);
                string returnpath = "/UploadFile/export/" + FileName;

                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result, returnpath));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
                   
        }

        /// <summary>
        /// 删除自采列表
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("api/ShopOrder/DeleteBackOrderlist")]
        [HttpPost]
        public IHttpActionResult DeleteBackOrderlist(int UserID,string Barcode)
        {
            var result= new BackOrderService().DeleteBackOrderlist(UserID, Barcode);
            if(result)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, "更新失败", result));
            }
        }
    }
}
