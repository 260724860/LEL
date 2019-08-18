using Common;
using DTO.Common;
using DTO.Others;
using DTO.ShopOrder;
using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using static Service.GoodsService;

namespace LELAdmin.Controllers
{
    [Authorize]
    public class OtherController : BaseController
    {
        OtherService OtherBLL = new OtherService();
        [AllowAnonymous]
        [HttpGet, Route("api/Other/AddSysAddress/")]
        public IHttpActionResult AddSysAddress(string ReceiveName, string ReceivePhone, string ReceiveArea, string ReceiveAddress, int Sort, double? Longitude, double? Latitude)
        {
            var result = OtherBLL.AddSysAddress(ReceiveName, ReceivePhone, ReceiveArea, ReceiveAddress, Sort, Longitude, Latitude);
            if (result > 0)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, "FAIL", result));
            }
        }

   
        /// <summary>
        /// 新增门店散货
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost, Route("api/Other/AddStoreBulkCargo/")]
        public IHttpActionResult AddStoreBulkCargo(StoreBulkCargoDto dto)
        {
            try
            {
                var result = new StoreBulkCargoService().AddStoreBulkCargo(dto);

                if (result)
                {
                    return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
                }
                else
                {
                    return Json(JRpcHelper.AjaxResult(1, "FAIL", result));
                }
            }
            catch (Exception ex)
            {
                return Json(JRpcHelper.AjaxResult(1, ex.Message, ex));
            }
           
        }
        /// <summary>
        /// 查询门店散货表
        /// </summary>
        /// <param name="Barcode"></param>
        /// <param name="Name"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpPost, Route("api/Other/GetStoreBulkCargoDtos/")]
        public IHttpActionResult GetStoreBulkCargoDtos(string Barcode, string Name, int? UserID)
        {
            var result = new StoreBulkCargoService(). GetStoreBulkCargoDtos(Barcode, Name, UserID);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }

        /// <summary>
        /// 修改系统收获地址
        /// </summary>
        /// <param name="AddressID"></param>
        /// <param name="ReceiveName"></param>
        /// <param name="ReceivePhone"></param>
        /// <param name="ReceiveArea"></param>
        /// <param name="ReceiveAddress"></param>
        /// <param name="DefaultAddr"></param>
        /// <returns></returns>
        [HttpGet, Route("api/Other/UpdateSysAddress/")]

        public IHttpActionResult UpdateSysAddress(int AddressID, string ReceiveName, string ReceivePhone, string ReceiveArea, string ReceiveAddress, int Sort, int Status, double? Longitude, double? Latitude)
        {
            var result = OtherBLL.UpdateSysAddress(AddressID, ReceiveName, ReceivePhone, ReceiveArea, ReceiveAddress, Sort, Status, Longitude, Latitude);
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
        /// 获取系统地址列表
        /// </summary>
        /// <param name="options"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpPost, Route("api/Other/GetSysAddressList/")]
        [AllowAnonymous]
        public IHttpActionResult GetSysAddressList(SeachOptions options)
        {
            int Count;
            var result = OtherBLL.GetSysAddressList(options, out Count);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result, Count));
        }
        /// <summary>
        /// 添加广告
        /// </summary>
        /// <param name="Img"></param>
        /// <param name="Link"></param>
        /// <param name="Sort"></param>
        /// <param name="AdName"></param>
        /// <returns></returns>
        [HttpGet, Route("api/Other/AddAd/")]
        [AllowAnonymous]
        public IHttpActionResult AddAd(string Img, string Link, int Sort, string AdName)
        {
            var result = OtherBLL.AddAd(Img, Link, Sort, AdName);
            if (result > 0)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, "FAIL", result));
            }
        }
        /// <summary>
        /// 修改广告
        /// </summary>
        /// <param name="AdId"></param>
        /// <param name="Img"></param>
        /// <param name="Link"></param>
        /// <param name="Sort"></param>
        /// <param name="AdName"></param>
        /// <param name="falag"></param>
        /// <returns></returns>
        [HttpGet, Route("api/Other/UpdateAd/")]
        [AllowAnonymous]
        public IHttpActionResult UpdateAd(int AdId, string Img, string Link, int Sort, string AdName, int falag)
        {
            var result = OtherBLL.UpdateAd(AdId, Img, Link, Sort, AdName, falag);
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
        /// 删除广告（硬删除）
        /// </summary>
        /// <param name="AdId"></param>
        /// <returns></returns>
        [HttpDelete, Route("api/Other/DeleteAd/")]
        [AllowAnonymous]
        public IHttpActionResult DeleteAd(int AdId)
        {
            var result = OtherBLL.DeleteAd(AdId);
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
        /// 删除系统自提地址（硬删除）
        /// </summary>
        /// <param name="AddressID"></param>
        /// <returns></returns>
        [HttpDelete, Route("api/Other/DeleteSysAddress/")]
        [AllowAnonymous]
        public IHttpActionResult DeleteSysAddress(int AddressID)
        {
            var result = OtherBLL.DeleteSysAddress(AddressID);
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
        /// 获取广告列表
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="rows"></param>
        /// <param name="Keywords"></param>
        /// <param name="Flag"></param>
        /// <returns></returns>
        [HttpGet, Route("api/Other/GetAdList/")]
        [AllowAnonymous]
        public IHttpActionResult GetAdList(int offset, int rows, string Keywords, int Flag)
        {
            int Count;
            var result = OtherBLL.GetAdList(Keywords, Flag, out Count);

            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result, Count));

        }
        /// <summary>
        /// 获取系统运行参数
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("api/Other/GetSysConfig/")]
        [AllowAnonymous]
        public IHttpActionResult GetSysConfig()
        {
            var result = SysConfig.Get().values;
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }
        /// <summary>
        /// 强制刷新系统运行参数（可能会引发系统异常）
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("api/Other/RefreshSysConfig/")]
        public IHttpActionResult RefreshSysConfig()
        {
            var result = SysConfig.RefreshSysConfig();
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }

        /// <summary>
        /// 修改系统配置参数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("api/Other/UpdateSysConfig/")]
        public IHttpActionResult UpdateSysConfig(le_sysconfig model)
        {
            var result = new SysConfigServie().UpdateSysConfig(model);
            if (result)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, "FAIL", model));
            }
        }
        /// <summary>
        /// 解密密码
        /// </summary>
        /// <param name="EncrypPwd"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        [HttpGet, Route("api/Other/GetPWD/")]
        [AllowAnonymous]
        public IHttpActionResult GetPWD(string EncrypPwd, string salt)
        {
            var DbPwd = DESEncrypt.Decrypt(EncrypPwd, salt);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", DbPwd));
        }
        /// <summary>
        /// 系统后门,获取用户账号密码. 正式上线请务必删除此接口！！！
        /// </summary>
        /// <param name="EncrypPwd"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        [HttpGet, Route("api/Other/GetUserPWD/")]
        [AllowAnonymous]
        public IHttpActionResult GetUserPWD(string Loginname,int UserType)
        {
            return Json(JRpcHelper.AjaxResult(1, "接口废除", Loginname));
            //if(UserType==1)
            //{

            //    return Json(JRpcHelper.AjaxResult(0, "SUCCESS", DbPwd));
            //}
        }

        /// <summary>
        /// 创建或修改当前系统版本
        /// </summary>
        /// <param name="id">为0或者空时新增否则修改</param>
        /// <param name="MajorVersion">主要版本号</param>
        /// <param name="ViversionNumber">次版本号</param>
        /// <param name="Description">说明</param>
        /// <param name="LeftoverBug">遗留问题</param>
        /// <param name="Remarks">备注</param>
        /// <returns></returns>
        [HttpGet, Route("api/Other/CreateOrUpdateSysSversion/")]
        public IHttpActionResult CreateOrUpdateSysSversion(int? id, string MajorVersion, string ViversionNumber, string Description, string LeftoverBug, string Remarks )
        {
            var result = new SysSversionService().CreateOrUpdate(id,  MajorVersion,  ViversionNumber,  Description,  LeftoverBug,  Remarks, out string msg);

            if(result)
            {
                return   Json(JRpcHelper.AjaxResult(0, msg, ""));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, msg, ""));
            }
        }

        /// <summary>
        /// 获取当前版本更新记录
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("api/Other/GetSysVersionList/")]
        public IHttpActionResult GetSysVersionList()
        {
            var result = new SysSversionService().GetSysVersionList();

          
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
           
        }

        /// <summary>
        /// 获取下单时间限制
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("api/Other/GetOrdersTimeLimitList/")]
        public IHttpActionResult GetOrdersTimeLimitList()
        {
            var result=  new OrdersTimeLimitService().GetOrdersTimeLimitList();

            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }

      /// <summary>
      /// 设置或者修改下单时间设置
      /// </summary>
      /// <param name="dto">ID未空或者0表示新增否则修改</param>
      /// <returns></returns>
        [HttpPost, Route("api/Other/CreateOrUpdate/")]
        public IHttpActionResult CreateOrUpdate(OrdersTimeLimitEditDto dto)
        {
           
            var result = new OrdersTimeLimitService().CreateOrUpdate(GetLoginInfo().UserID,dto,out string msg);
            if (result)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, msg, result));

            }
        }

        /// <summary>
        /// 获取当前时间段内下单数
        /// </summary>
        /// <param name="TimeSlot"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, Route("api/ShopOrder/GetOrderLimitByTimeSlot/")]
        public async Task<IHttpActionResult> GetOrderLimitByTimeSlot(DateTime? TimeSlot)
        {
            DateTime newDate;
            if (TimeSlot==null)
            {
                newDate = DateTime.Now;
            }
            else
            {
                newDate = TimeSlot.Value;
            }
          
            var result = await new OrdersTimeLimitService().GetOrderLimitForTimeSlot(newDate);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }

        /// <summary>
        /// 检查商品图片是否存在
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet,Route("FindGoodsImg")]
        public IHttpActionResult FindGoodsImg(int offset,int rows,bool IsShop0Jpg)
        {
            var GetAllGoodsImg = new GoodsService().GetAllGoodsImg(offset,rows);
            var   localPath = System.Web.HttpContext.Current.Server.MapPath("/");
            List<FindGoodsImg> result = new List<FindGoodsImg>();
            foreach (var item in GetAllGoodsImg)
            {
                if (!System.IO.File.Exists(localPath+ item.Img))
                {
                    result.Add(item);
                }
                if(item.Img== "/GoodImg/0.jpg"&& IsShop0Jpg==true)
                {
                    result.Add(item);
                }
            }

            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result,result.Count));
        }
    }
}
