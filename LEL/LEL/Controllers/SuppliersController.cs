using Common;
using DTO.Goods;
using DTO.ShopOrder;
using DTO.Suppliers;
using DTO.SupplierUser;
using DTO.WeixinDto;
using Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using static DTO.Common.Enum;

namespace LEL.Controllers
{
    [Authorize]
    public class SuppliersController : BaseApiController
    {
        OtherService otherService = new OtherService();
        SupplierUserService SupplierUserService = new SupplierUserService();
        ShopOrderService ShopOrderBLL = new ShopOrderService();
        GoodsService GoodService = new GoodsService();
        SupplierIndex SupplierIndexBLL = new SupplierIndex();
        ReportService ReportBLL = new ReportService();
        private int UserID { get; set; }
        private int Status { get; set; }
        private int GetUserID()
        {
            UserID = Convert.ToInt32(User.Identity.Name.Split(',')[1]);
            Status = Convert.ToInt32(User.Identity.Name.Split(',')[2]);
            if (Status == 0)
            {
                return -1;
            }
            return UserID;
        }

        /// <summary>
        /// 供应商上传资料
        /// </summary>
        /// <param name="dTO"></param>
        /// <returns></returns>
        [HttpPost, Route("api/Suppliers/Update/")]
        public IHttpActionResult Update(SupplierUserDto dTO)
        {
            if (dTO == null)
            {
                return Json(JRpcHelper.AjaxResult(1, "参数错误请检查", null));
            }
            dTO.SuppliersID = GetLoginInfo().UserID;
            var result = SupplierUserService.Update(dTO, true,out string msg);
            if (result)
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
        /// 获取供应商价格
        /// </summary>
        /// <param name="KeyWords"></param>
        /// <param name="Offset"></param>
        /// <param name="Rows"></param>
        /// <param name="GoodsID"></param>
        /// <param name="SuppliersID"></param>
        /// <returns></returns>
        [HttpPost, Route("api/Suppliers/GetSupplierGoodsPric/")]
        public IHttpActionResult GetSupplierGoodsPric(string KeyWords, int Offset, int Rows, int? GoodsID, int? SuppliersID)
        {
            var result = GoodService.GetSupplierGoodsPriceList(KeyWords, Offset, Rows, GoodsID, SuppliersID, out int Count);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result, Count));

        }
        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="OrderLineID"></param>
        /// <returns></returns>
        [HttpPost, Route("api/Suppliers/UpdateOrderLineStatus/")]
        public IHttpActionResult UpdateOrderLineStatus(List<UpdateOrderLineParamas> lineParamas)
        {
            int[] StatusAttr = { 2, 3 };
            //if (!StatusAttr.Contains(index.Status) || index.OrderLineID <= 0)
            //{
            //    return Json(JRpcHelper.AjaxResult(1, "参数错误", index.OrderLineID));
            //}
          
            var result = ShopOrderBLL.UpdateOrderLineStatus(lineParamas, out string Msg, 0, GetUserID());
            if (!result)
            {
                return Json(JRpcHelper.AjaxResult(1, Msg, null));
            }

            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", lineParamas));
        }

        /// <summary>
        /// 获取订单行列表
        /// </summary>
        /// <param name="SeachOptions"></param>
        /// <returns></returns>
        [HttpPost, Route("api/Suppliers/GetOrderlineList/")]
        public IHttpActionResult GetOrderlineList(OrderLineSeachParames SeachOptions)
        {
            SeachOptions.SuppliersID = GetUserID();
            var result = ShopOrderBLL.GetOrderlineList(SeachOptions, out int Count);
            
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result, Count));
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        //[HttpPost, Route("api/Suppliers/GetListOrderforSuppier/")]
        //public IHttpActionResult GetListOrderforSuppier(OrderSeachParams options)
        //{
        //    if (GetUserID() == -1)
        //    {
        //        return Json(JRpcHelper.AjaxResult(1, "未通过审核", GetUserID()));
        //    }
        //    options.SupplierID = GetUserID();
        //    var result = ShopOrderBLL.GetListOrderforSuppier(options, out int Count);
        //    return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result, Count));
        //}

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="LoginName">账号</param>
        /// <param name="NewPWD">新密码</param>
        /// <param name="OriginalPWD">旧密码</param>
        /// <returns></returns>
        [HttpPost, Route("api/Suppliers/UpdatePwd/")]
        public IHttpActionResult UpdatePwd(string LoginName, string NewPWD, string OriginalPWD)
        {
            //var Model = OtherService.GetSmsRecord(LoginName);
            //if (Model.Status != 1 || Model.Status == 1 && Model.CreatTime < DateTime.Now.AddMinutes(-10))
            //{
            //    return Json(JRpcHelper.AjaxResult(1, "非法操作！请重新获取验证码", LoginName));
            //}
            var UserDto = SupplierUserService.Login(LoginName, OriginalPWD);
            if (UserDto.Code == 1)
            {
                return Json(JRpcHelper.AjaxResult(1, UserDto.Msg, null));
            }
            string TruePwd = DESEncrypt.DecryptStringHex(NewPWD, "SystemLE");
            if (string.IsNullOrEmpty(TruePwd))
            {
                return Json(JRpcHelper.AjaxResult(1, "解密失败,请确认", LoginName));
            }
            var result = SupplierUserService.UpdatePwd(LoginName, TruePwd);
            if (result)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", LoginName));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, "修改失败", LoginName));
            }
        }

        /// <summary>
        /// 获取订单详细
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        
        [HttpPost, Route("api/Suppliers/GetOrderDetails/")]
        public IHttpActionResult GetOrderDetails(string OrderNO)
        {
            if (GetUserID() == -1)
            {
                return Json(JRpcHelper.AjaxResult(1, "未通过审核", GetUserID()));
            }
            var result = ShopOrderBLL.GetOrderDetails(OrderNO, GetUserID());
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
        /// 获取订单详细 分页
        /// </summary>
        /// <param name="Params"></param>
        /// <returns></returns>
        [HttpPost, Route("api/Suppliers/GetOrderDetailsPage/")]
        public IHttpActionResult GetOrderDeatilsPage(OrderDeatilsParams Params)
        {
            Params.SupplierIDs = new List<int?>{ GetUserID() };
            var result = ShopOrderBLL.GetOrderDeatils(Params, out int Count);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result, Count));
        }
        /// <summary>
        /// 获取供应商价格
        /// </summary>
        /// <param name="KeyWords">搜索关键字</param>
        /// <param name="Offset"></param>
        /// <param name="Rows"></param>
        /// <param name="GoodsID">商品ID 可为空</param>
        /// <returns></returns>
        [HttpGet, Route("api/Suppliers/GetSupplierGoodsPric/")]
        public IHttpActionResult GetSupplierGoodsPric(int Offset, int Rows, int? GoodsID = 0, string KeyWords = "")
        {
            var result = GoodService.GetSupplierGoodsPriceList(KeyWords, Offset, Rows, GoodsID, GetUserID(), out int Count);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="options">查询参数</param>
        /// <returns></returns>
        [HttpPost, Route("api/Suppliers/GetGoodsList/")]
        public async Task<IHttpActionResult> GetGoodsList([FromBody]GoodsSeachOptions options)
        {
            if (options == null)
            {
                return Json(JRpcHelper.AjaxResult(1, "未接收到有效参数", options));
            }

            options.SupplierID = GetUserID();
            var result = await GoodService.GetGoodsListAsync(options);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
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
                var bol = ShopOrderBLL.BatchEditLinesInfo(List, GetLoginInfo(), out string msg);
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

        /// <summary>
        /// 查询后台首页业绩统计
        /// </summary>
        /// <returns></returns>
        [Route("GetSalesDTO")]
        [HttpGet]
        public IHttpActionResult GetSalesDTO()
        {
            try
            {
                var dto = SupplierIndexBLL.GetSalesDTO(GetUserID());
                return Json(new { code = 0, msg = "SUCCESS", content = dto });
            }
            catch (Exception ex)
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
                var dto = SupplierIndexBLL.GetGoodsStaticDTO(GetUserID());
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
                var dto = SupplierIndexBLL.GetSalesChartDTO(StartTime, EndTime, GetUserID());
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
            var result = SupplierIndexBLL.GetPendingTransDTO(GetUserID());
            return Json(new { code = 0, msg = "SUCCESS", content = result });
        }


        /// <summary>
        /// 设置订单打包数 重复设置覆盖
        /// </summary>
        /// <param name="OrderHeadID"></param>
        /// <param name="PacetCount"></param>
        /// <returns></returns>
        [Route("SetOrderPackCount")]
        [HttpPost]
        public IHttpActionResult SetOrderPackCount(int OrderHeadID, int PacetCount)
        {
            var reuslt = ShopOrderBLL.SetOrderPackCount(OrderHeadID, GetUserID(), PacetCount);
            if (reuslt)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", reuslt));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, "Fail", reuslt));
            }
        }

        /// <summary>
        /// 获取订单打包数
        /// </summary>
        /// <param name="OrderHeadID"></param>
        /// <returns></returns>
        [Route("GetOrderPackCountsList")]
        [HttpPost]
        public IHttpActionResult GetOrderPackCountsList(int OrderHeadID)
        {
            var reuslt = ShopOrderBLL.GetOrderPackCountsList(OrderHeadID, GetUserID());
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", reuslt));
        }

        /// <summary>
        /// 微信消息绑定
        /// </summary>
        /// <param name="OrderHeadID"></param>
        /// <returns></returns>
        // [AllowAnonymous]
        [Route("BindWeixinUser")]
        [HttpPost]
        public IHttpActionResult BindWeixinUser(BindWxUserDto dto )
        {
            if(dto==null)
            {
                return Json(JRpcHelper.AjaxResult(1, "接受到了空得参数", dto));
            }
            var result=  new WeixinUserService().BindWeixinUser(dto.WeixinInfo, 2, GetUserID(), out string Msg);
            if(result)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", dto));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, Msg, dto));
            }
            return null;
        }
        
        /// <summary>
        /// 修改供应商订单行（接单/取消/结算/发货中）
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost, Route("api/OrderHandle/UpdateOrderLineStatusBySupplier/")]
        public IHttpActionResult UpdateOrderLineStatusBySupplier(UpdateOrderLineStatusBySupplierDto param )
        {
            //int AdminID = GetLoginInfo().UserID;
            int[] limit = { 100, 2, 3,6,10 };
            if (!((IList)limit).Contains(param.Status))
            {
                return Json(JRpcHelper.AjaxResult(1, "请输入正常的状态码限定范围[100, 2,3,6]", param.Status));
            }

            var result = ShopOrderBLL.UpdateOrderLineStatusBySupplier(param.OrderNO, null, param.SuppliersID, param.Status, out string Msg);
            if (!result)
            {

                return Json(JRpcHelper.AjaxResult(1, Msg, param.OrderNO));
            }
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", param.OrderNO));
        }
        
        /// <summary>
        /// 获取绑定用户列表
        /// </summary>
        /// <returns></returns>
        [Route("GetWeixinUserList")]
        [HttpGet]
        public IHttpActionResult GetWeixinUserList()
        {
            var result = new WeixinUserService().GetWeixinUserList(2, GetUserID());
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }
        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("api/OrderHandle/GetOrderPrintList/")]
        public IHttpActionResult GetOrderPrintList(string OutNo="")
        {
            PrintingService bll = new PrintingService();
            var result = bll.GetLinesMappingList(OutNo, GetUserID());
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }
        /// <summary>
        /// 新建或更新打印列表
        /// </summary>
        /// <param name="OutNo"></param>
        /// <param name="SupplierID"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="Remarks"></param>
        /// <param name="Types"></param>
        /// <returns></returns>
        [HttpPost, Route("api/OrderHandle/CreateOrUpdate/")]
        public IHttpActionResult CreateOrUpdate(string OutNo,  string A, string B, string C, string Remarks, string Types)
        {
            PrintingService bll = new PrintingService();
            var result = bll.CreateOrUpdate( OutNo, GetUserID() ,A,  B,  C,  Remarks,  Types);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }
        /// <summary>
        /// 更新供应商客户端信息
        /// </summary>
        /// <param name="ClientVersion"></param>
        /// <param name="IsLoginClient"></param>
        /// <param name="LoginClientTime"></param>
        /// <returns></returns>
        [HttpPost, Route("api/Suppliers/UpdateSupplierVision")]
        public IHttpActionResult UpdateSupplierVision( string ClientVersion="", int? IsLoginClient=null, DateTime? LoginClientTime=null)
        {
             var result = SupplierUserService.UpdateSupplierVision(GetUserID(),ClientVersion, IsLoginClient, LoginClientTime);
            if(result)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, "没有更新到数据", result));
            }
        }

        /// <summary>
        /// 上传质检报告
        /// </summary>
        /// <param name="createOrEditDto"></param>
        /// <returns></returns>
        [HttpPost, Route("api/Suppliers/QualityTestingCreateOrEdit")]
        public IHttpActionResult QualityTestingCreateOrEdit(QualityTestingCreateOrEditDto createOrEditDto)
        {
            createOrEditDto.SupplierID = GetUserID();
            var result = GoodService.QualityTestingCreateOrEdit(createOrEditDto);
            if (result)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, "没有更新到数据", result));
            }
        }

        /// <summary>
        /// 获取质检报告
        /// </summary>
        /// <param name="GoodsID"></param>
        /// <returns></returns>
        [HttpPost, Route("api/Suppliers/GetQualityTestingList")]
        public IHttpActionResult GetQualityTestingList(int  GoodsID)
        {
            var result = GoodService.GetQualityTestingList(GoodsID);
           
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
          
        }


        /// <summary>
        /// 修改供应商价格
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Price"></param>
        /// <param name="IsDeleted"></param>
        /// <returns></returns>
        //[HttpGet, Route("api/Suppliers/UpdateSupplierPrice/")]
        //public IHttpActionResult UpdateSupplierPrice(int ID, int Price, int IsDeleted)
        //{
        //    var result = GoodService.UpdateSupplierPrice(ID,  Price,  IsDeleted);
        //    if (result)
        //    {
        //        return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        //    }
        //    else
        //    {
        //        return Json(JRpcHelper.AjaxResult(1, "FAIL", result));
        //    }
        //}


        ///// <summary>
        ///// 商品销量报表
        ///// </summary>
        ///// <param name="options">基础参数</param>
        ///// <param name="GoodsID">商品ID</param>
        ///// <param name="GoodsGroupsID">商品分类ID</param>
        ///// <param name="SupplierID">供应商ID</param>
        ///// <param name="UserID">加盟店ID</param>
        ///// <returns></returns>
        //[HttpPost, Route("GetGoodsSalesReport")]
        //public IHttpActionResult GetGoodsSalesReport(SeachDateTimeOptions options, GoodsSalesReportOrderByType orderType, int? GoodsID, int? GoodsGroupsID, int? SupplierID, int? UserID)
        //{
        //    try
        //    {
        //        var result = ReportBLL.GetGoodsSalesReport(options, orderType, GoodsID, GoodsGroupsID, SupplierID, UserID);

        //        return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(JRpcHelper.AjaxResult(1, ex.Message, ex));
        //    }
        //}


    }
}
