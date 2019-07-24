using Common;
using DTO.SupplierUser;
using DTO.User;
using Service;
using System;
using System.Data.Entity.Validation;
using System.Web.Http;

namespace LELAdmin.Controllers
{
    /// <summary>
    /// 商户管理
    /// </summary>
    [RoutePrefix("api/UserManage")]
    public class UserManageController : BaseController
    {
        StoreUserService StoreSevice = new StoreUserService();
        private SuppliersService SlService = new SuppliersService();
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="options"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        [HttpPost, Route("api/UserManage/GetUserList/")]
        public IHttpActionResult GetStoreUserList(UserSeachOptions options, out int Count)
        {
            var reuslt = StoreSevice.GetUserList(options, out Count);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", reuslt));
        }

        /// <summary>
        /// 修改商户用户资料 审核
        /// </summary>
        /// <param name="dTO"></param>
        /// <param name="oneself"></param>
        /// <returns></returns>
        [HttpPost, Route("api/UserManage/Update/")]
        public IHttpActionResult Update(UserDTO dTO)
        {
            var result = StoreSevice.Update(dTO, false);
            if (result)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, "FAIL", result));
            }
        }

        #region 门店管理相关
        /// <summary>
        /// 获取供货商列表
        /// </summary>
        /// <param name="options"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        [Route("GetSupplierUserList")]
        [HttpPost]
        public IHttpActionResult GetSupplierUserList(UserSeachOptions options)
        {
            try
            {
                var result = SlService.GetSupplierUserList(options, out int Count);
                if (result.Count > 0)
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = result, Count = Count });
                }
                return Json(new { code = 1, msg = "ERROR", content = "" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 修改供货商信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [Route("EditSupplierUser")]
        [HttpPost]
        public IHttpActionResult EditSupplierUser(SupplierUserDto model)
        {
            try
            {
                var result = new SupplierUserService().Update(model, false);
                if (result)
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = "" });
                }
                return Json(new { code = 1, msg = "ERROR", content = "" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 获取门店用户列表
        /// </summary>
        /// <param name="options"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        [Route("GetUserList")]
        [HttpPost]
        public IHttpActionResult GetUserList(UserSeachOptions options)
        { 
            try
            {
                var result = StoreSevice.GetUserList(options, out int Count,GetLoginInfo().UserID);
                if (result.Count > 0)
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = result, Count = Count });
                }
                return Json(new { code = 1, msg = "ERROR", content = "" });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 修改加盟店用户资料
        /// </summary>
        /// <param name="dTO"></param>
        /// <param name="oneself"></param>
        /// <returns></returns>
        [Route("UpdateUserInfo")]
        [HttpPost]
        public IHttpActionResult UpdateUserInfo(UserDTO model)
        {
            try
            {
                var result = new StoreUserService().Update(model, false);
                if (result)
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = "修改成功" });
                }
                return Json(new { code = 1, msg = "ERROR", content = "修改失败" });
            }
            catch (DbEntityValidationException ex)
            {
                string kk = "";
                foreach (var index in ex.EntityValidationErrors)
                {
                    kk += index.ValidationErrors;
                }
                
               
                return Json(new { code = 1, msg = "数据类型错误:" + kk, content = ex.ToString() });

            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }
        /// <summary>
        /// 获取加盟店基本信息
        /// </summary>
        /// <param name="dTO"></param>
        /// <param name="oneself"></param>
        /// <returns></returns>
        [Route("GetBaseStoreUserList")]
        [HttpPost]
        public IHttpActionResult GetBaseStoreUserList(string KeyWords)
        {
            var result = StoreSevice.GetBaseStoreUserList(KeyWords, GetLoginInfo().UserID);
            return Json(new { code = 0, msg = "SUCCESS", content = result });
        }


        #endregion
    }
}
