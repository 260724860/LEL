﻿using Common;
using DTO.SupplierUser;
using DTO.User;
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
    /// 商户管理
    /// </summary>
    [RoutePrefix("api/UserManage")]
    public class UserManageController :BaseController
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
                var result = SlService.EditSupplierUser(model, out string msg);
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
        /// 获取用户列表
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
                var result = StoreSevice.GetUserList(options, out int Count);
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
        /// 修改用户资料
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
                var result = SlService.UpdateUserInfo(model, out string msg);
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
        /// 获取加盟店基本信息
        /// </summary>
        /// <param name="dTO"></param>
        /// <param name="oneself"></param>
        /// <returns></returns>
        [Route("GetBaseStoreUserList")]
        [HttpPost]
        public IHttpActionResult GetBaseStoreUserList(string KeyWords)
        {
            var result = StoreSevice.GetBaseStoreUserList(KeyWords,GetLoginInfo().UserID);
            return Json(new { code = 0, msg = "SUCCESS", content = result });
        }


        #endregion
    }
}
