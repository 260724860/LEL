using Common;
using DTO.Goods;
using LELAdmin.Models;
using Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace LELAdmin.Controllers
{
    /// <summary>
    /// 后台商品 API
    /// </summary>
    [RoutePrefix("api/Goods")]
    [Authorize]
    public class GoodsController : BaseController
    {
        private GoodsService GService = new GoodsService();
        private SuppliersService SlService = new SuppliersService();
        private StoreUserService StoreUserBLL = new StoreUserService();
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="options">查询参数</param>
        /// <returns></returns>
        [Route("GetGoodsList")]
        [HttpPost]
        public async Task<IHttpActionResult> GetGoodsList(GoodsSeachOptions options)
        {
            //options.IsShelves = 1;
            //options.Offset = 0;
            //options.Rows = 10;
            //options.KeyWords = "7";
            //options.SortKey = GoodsSeachOrderByType.CreateTimeDesc;

            try
            {
                var result = await GService.GetGoodsListAsync(options, GetLoginInfo().UserID);
                return Json(new { code = 0, msg = "SUCCESS", content = result });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }

        }

        /// <summary>
        /// 获取所有的商品分类
        /// </summary>
        /// <param name="KeyWords"></param>
        /// <returns></returns>
        [Route("GetGoodsGroupList")]
        [HttpGet]
        public IHttpActionResult GetGoodsGroupList(string KeyWords = "")
        {
            try
            {
                var result = GService.GetGoodsGroupList(KeyWords);
                return Json(new { code = 0, msg = "SUCCESS", content = result });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 获取商品详细
        /// </summary>
        /// <param name="GoodsID"></param>
        /// <returns></returns>
        [Route("GetGoodDetailedAync")]
        [HttpGet]
        public async Task<IHttpActionResult> GetGoodDetailedAync(int GoodsID)
        {
            try
            {
                var result = await GService.GetGoodDetailedAync(GoodsID);
                return Json(new { code = 0, msg = "SUCCESS", content = result });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("AddGoods")]
        [HttpPost]
        public IHttpActionResult AddGoods(AddGoodsDto dto)
        {
            try
            {
                if (dto.MinimumPurchase <= 0)
                {
                    return Json(JRpcHelper.AjaxResult(1, "最小起配数应该大于零", null));
                }
                var result = GService.AddGoods(dto, out string msg);
                if (result != 0)
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = result });
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
        /// 商品下架
        /// </summary>
        /// <param name="GoodsID"></param>
        /// <returns></returns>
        [Route("UnShelvesGoods")]
        [HttpPost]
        public IHttpActionResult UnShelvesGoods(int GoodsID)
        {
            try
            {
                string Msg;
                var result = GService.UnShelvesGoods(GoodsID, out Msg);
                if (Msg.Equals("SUCCESS") && result)
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = Msg });
                }
                return Json(new { code = 1, msg = "ERROR", content = Msg });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 修改商品信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route("EditGoods")]
        [HttpPost]
        public IHttpActionResult EditGoods(AddGoodsDto dto)
        {
            try
            {
                string Msg;
                if(dto.MinimumPurchase<=0)
                {
                    return Json(JRpcHelper.AjaxResult(1,"最小起配数应该大于零",null));
                }
                var result = GService.EditGoods(dto, GetLoginInfo(), out Msg);
                if (Msg.Equals("SUCCESS") && result)
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = Msg });
                }
                return Json(new { code = 1, msg = "ERROR", content = Msg });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 删除商品图片
        /// </summary>
        /// <param name="ListImgID"></param>
        /// <param name="GoodsID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [Route("DeleteGoodsImage")]
        [HttpPost]
        public IHttpActionResult DeleteGoodsImage(List<int> ListImgID)
        {
            try
            {
                var result = GService.DeleteGoodsImg(ListImgID, out string Msg);
                if (Msg.Equals("SUCCESS") && result)
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = Msg });
                }
                return Json(new { code = 1, msg = "ERROR", content = Msg });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 添加商品图片
        /// </summary>
        /// <param name="List"></param>
        /// <returns></returns>
        [Route("AddGoodsImage")]
        [HttpPost]
        public IHttpActionResult AddGoodsImage(List<AddGoodsAttachImg1to> List)
        {
            try
            {
                if (List.Count <= 0)
                {
                    return Json(new { code = 1, msg = "ERROR", content = "List参数为NULL" });
                }
                var result = GService.AddGoodsImage(List, out string Msg);
                if (Msg.Equals("SUCCESS") && result != 0)
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = result });
                }
                return Json(new { code = 1, msg = "ERROR", content = Msg });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 删除商品属性
        /// </summary>
        /// <param name="List">GetGoodDetailedAync 返回的GoodsValueID</param>
        /// <returns></returns>
        [Route("DeleteGoodsValue")]
        [HttpPost]
        public IHttpActionResult DeleteGoodsValue(List<int> List)
        {
            try
            {
                var result = GService.DeleteGoodsValue(List, out string Msg);
                if (Msg.Equals("SUCCESS") && result)
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = Msg });
                }
                return Json(new { code = 1, msg = "ERROR", content = Msg });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 删除商品分类 (硬删除)
        /// </summary>
        /// <param name="GoodsGroupID"></param>
        /// <returns></returns>
        [HttpDelete, Route("DeleteGoodsGroup")]
        public IHttpActionResult DeleteGoodsGroup(int GoodsGroupID)
        {
            var result = GService.DeleteGoodsGroup(GoodsGroupID);
            if (result)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", GoodsGroupID));
            }
            return Json(JRpcHelper.AjaxResult(1, "删除失败,尚有商品使用该分类", GoodsGroupID));
        }
        /// <summary>
        /// 增加商品属性
        /// </summary>
        /// <param name = "Name" ></ param >
        /// < param name="Price"></param>
        /// <returns></returns>
        [Route("AddGoodsValue")]
        [HttpPost]
        public IHttpActionResult AddGoodsValue(List<GoodsValues> List, int IsBulkCargo)
        {
            try
            {
                if (List.Count <= 0)
                {
                    return Json(new { code = 1, msg = "ERROR", content = "List参数为NULL" });
                }
                var result = GService.AddGoodsValueList(List, IsBulkCargo, out string Msg);
                if (result != null)
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = result, result = Msg });
                }
                return Json(new { code = 1, msg = "ERROR", content = "添加失败" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="KeyWords"></param>
        /// <returns></returns>
        [Route("GetSupplierList")]
        [HttpGet]
        public IHttpActionResult GetSupplierList(string KeyWords)
        {
            try
            {
                var result = SlService.GetSupplierList(KeyWords);
                if (result.Count > 0)
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = result });
                }
                return Json(new { code = 1, msg = "ERROR", content = "" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 添加商品分类
        /// </summary>
        /// <param name="model"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [Route("AddGoodsGroupList")]
        [HttpPost]
        public IHttpActionResult AddGoodsGroupList(GoodsGroupDto model)
        {
            try
            {
                var result = GService.AddGoodsGroupList(model, out string msg);
                if (result)
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
        /// 修改商品分类
        /// </summary>
        /// <param name="model"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [Route("EditGoodsGroupList")]
        [HttpPost]
        public IHttpActionResult EditGoodsGroupList(GoodsGroupDto model)
        {
            try
            {
                var result = GService.EditGoodsGroupList(model, out string msg);
                if (result)
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
        /// 获取供应商价格
        /// </summary>
        /// <param name="KeyWords"></param>
        /// <param name="Offset"></param>
        /// <param name="Rows"></param>
        /// <param name="GoodsID"></param>
        /// <param name="SuppliersID"></param>
        /// <returns></returns>
        [Route("GetSupplierGoodsPriceList")]
        [HttpPost]
        public IHttpActionResult GetSupplierGoodsPriceList(string KeyWords, int Offset, int Rows, int? GoodsID, int? SuppliersID)
        {
            int Count;
            var result = GService.GetSupplierGoodsPriceList(KeyWords, Offset, Rows, GoodsID, SuppliersID, out Count);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }


        /// <summary>
        /// 生成条码 散货生成五位数,否则六位数条码
        /// </summary>
        /// <param name="IsBulkCargo"></param>
        /// <returns></returns>
        [Route("BarcodeGeneration")]
        [HttpGet]
        public IHttpActionResult BarcodeGeneration(int IsBulkCargo)
        {
            string result;

            result = GService.BarcodeGeneration(IsBulkCargo);

            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }

        /// <summary>
        /// 检查商品条码是否已经存在
        /// </summary>
        /// <param name="SerialNumber"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("IsSerialNumberExit")]
        [HttpGet]
        public IHttpActionResult IsSerialNumberExit(string SerialNumber)
        {
            var Exit = GService.IsSerialNumberExit(SerialNumber);
            if (Exit)
            {
                return Json(JRpcHelper.AjaxResult(100, "已存在相同的条码", SerialNumber));
            }
            return Json(JRpcHelper.AjaxResult(200, "当前条码不存在", SerialNumber));
        }
        /// <summary>
        /// 获取当前商品中最大排序数，自动加一
        /// </summary>
        [AllowAnonymous]
        [Route("GetGoodsMaxSort")]
        [HttpGet]
        public IHttpActionResult GetGoodsMaxSort()
        {

            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", GService.GetGoodsMaxSort()));
        }
        /// <summary>
        /// 判断当前序号是否存在,返回当前商品中最大排序数，自动加一
        /// </summary>
        [AllowAnonymous]
        [Route("IsExitGetGoodsSort")]
        [HttpGet]
        public IHttpActionResult IsExitGetGoodsSort(int Sort)
        {
            var IsExit = GService.IsExitGetGoodsSort(Sort);
            int MaxSort=  GService.GetGoodsMaxSort();
            if (IsExit)
            {
                return Json(JRpcHelper.AjaxResult(100, "已存在相同的排序", MaxSort));
            }
            return Json(JRpcHelper.AjaxResult(200, "当前排序不存在", MaxSort));
        }


        /// <summary>
        /// 获取商品日志
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [Route("GetGoodsLogList")]
        [HttpPost]
        public IHttpActionResult GetGoodsLogList(GoodsLogParam param)
        {
            var result = new LogService().GetGoodsLogList(param.SeachOptions, param.AdminID, param.GoodsID, out int Count);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result, Count));
        }
       
    }
}
