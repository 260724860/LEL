using Common;
using DTO.Admin;
using LELAdmin.App_Start;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;

namespace LELAdmin.Controllers
{
    /// <summary>
    /// 账号管理 API
    /// </summary>
    [RoutePrefix("api/Account")]
    public class AccountController : BaseController
    {
        private Service.AdminService AdService = new Service.AdminService();


        /// <summary>
        /// 后台用户登录
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="PWD"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Login(string LoginName, string PWD)
        {
            try
            {
                var dto = AdService.Login(LoginName, PWD);
                if (dto.code != 0)
                {
                    return Json(new { code = 1, msg = "ERROR", content = "" });
                }
                var tokenExpiration = TimeSpan.FromHours(2);
                ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, LoginName));
                identity.AddClaim(new Claim("UserID", dto.AdminID.ToString()));
                identity.AddClaim(new Claim("UserType", "3"));
                identity.AddClaim(new Claim("Status", dto.status.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Sid, dto.AdminID.ToString()));
                var props = new AuthenticationProperties()
                {
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
                };
                var ticket = new AuthenticationTicket(identity, props);
                var accessToken = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);

                JObject tokenResponse = new JObject(
                                            new JProperty("result", JsonConvert.SerializeObject(dto)),
                                            new JProperty("access_token", accessToken),
                                            new JProperty("token_type", "bearer"),
                                            new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
                                            new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
                                            new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString()));

                return Json(new { code = 0, msg = "SUCCESS", content = tokenResponse });

            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 添加后台用户
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [Route("AddAdmin")]
        [HttpPost]
        public IHttpActionResult AddAdmin(AdminDTO model)
        {
            try
            {
                string message;

                var bol = AdService.AddAdmin(model, out message);

                if (message.Equals("SUCCESS"))
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = message });
                }
                else
                {
                    return Json(new { code = 1, msg = "ERROR", content = message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 删除后台用户
        /// </summary>
        /// <param name="AdminID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [Route("DeleteAdmin")]
        [HttpPost]
        public IHttpActionResult DeleteAdmin(int AdminID)
        {
            try
            {
                string message;

                var bol = AdService.DeleteAdmin(AdminID, out message);

                if (message.Equals("SUCCESS"))
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = message });
                }
                else
                {
                    return Json(new { code = 1, msg = "ERROR", content = message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 编辑后台用户
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [Route("UpdateAdmin")]
        [HttpPost]
        public IHttpActionResult UpdateAdmin(AdminDTO dto)
        {
            try
            {
                string message;

                var bol = AdService.UpdateAdmin(dto, out message);

                if (message.Equals("SUCCESS"))
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = message });
                }
                else
                {
                    return Json(new { code = 1, msg = "ERROR", content = message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 查询后台用户
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="KeyWords"></param>
        /// <returns></returns>
        [Route("GetAdminList")]
        [HttpGet]
        public IHttpActionResult GetAdminList(string KeyWords = "")
        {
            try
            {
                string message;

                var bol = AdService.GetAdminList(out message, KeyWords);

                if (message.Equals("SUCCESS"))
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = bol });
                }
                else
                {
                    return Json(new { code = 1, msg = "ERROR", content = message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 修改后台用户密码
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [Route("UpdateAdminPassWord")]
        [HttpPost]
        public IHttpActionResult UpdateAdminPassWord(UpdatePassWordDTO dto)
        {
            try
            {
                string message;

                var bol = AdService.UpdateAdminPassWord(dto, out message);

                if (message.Equals("SUCCESS"))
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = message });
                }
                else
                {
                    return Json(new { code = 1, msg = "ERROR", content = message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 查询角色权限
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [Route("GetAdminRole")]
        [HttpGet]
        public IHttpActionResult GetAdminRole(int ID = 0)
        {
            try
            {
                var bol = AdService.GetAdminRole(ID = 0);

                if (bol.Count > 0)
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = bol });
                }
                else
                {
                    return Json(new { code = 1, msg = "ERROR", content = "" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="editAdmin"></param>
        /// <returns></returns>
        [Route("EditAdminUserInfo")]
        [HttpPost]
        public IHttpActionResult EditAdminUserInfo(EditAdminUserDto editAdmin)
        {

            if (!string.IsNullOrEmpty(editAdmin.Password))
            {
                string TruePwd = DESEncrypt.DecryptStringHex(editAdmin.Password, "SystemLE");
                if (string.IsNullOrEmpty(TruePwd))
                {
                    return Json(JRpcHelper.AjaxResult(1, "解密失败,请确认", editAdmin));
                }
                editAdmin.Password = TruePwd;
            }

            var success = AdService.EditAdminUserInfo(editAdmin, out string Msg);

            if (success)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", null));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, "SUCCESS", Msg));

            }
        }

        /// <summary>
        /// 设置门店和总部关联关系
        /// </summary>
        /// <param name="editAdmin"></param>
        /// <returns></returns>
        [Route("SetAdminReUsers")]
        [HttpPost]
        public IHttpActionResult SetAdminReUsers(List<int> UserListID, int AdminID)
        {
            if (UserListID == null || UserListID.Count == 0)
            {
                return Json(JRpcHelper.AjaxResult(1, "参数错误【UserListID】错误", UserListID));
            }
            var result = AdService.SetAdminReUsers(UserListID, AdminID);
            if (result)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", UserListID));
            }
            return Json(JRpcHelper.AjaxResult(1, "修改错误，请稍后再试", UserListID));
        }

        /// <summary>
        /// 获取门店权限关系
        /// </summary>
        /// <param name="AdminID">管理员ID</param>
        /// <returns></returns>
        [Route("GetAdminReUsers")]
        [HttpPost]
        public IHttpActionResult GetAdminReUsers(int AdminID)
        {
            var result = AdService.GetAdminReUsers(AdminID);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }

        /// <summary>
        /// 设置总部管理员和供应商权限关系
        /// </summary>
        /// <param name="editAdmin"></param>
        /// <returns></returns>
        [Route("SetAdminReSupplier")]
        [HttpPost]
        public IHttpActionResult SetAdminReSupplier(List<int> UserListID, int AdminID)
        {
            if (UserListID == null || UserListID.Count == 0)
            {
                return Json(JRpcHelper.AjaxResult(1, "参数错误【UserListID】错误", UserListID));
            }
            var result = AdService.SetAdminReSupplier(UserListID, AdminID);
            if (result)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", UserListID));
            }
            return Json(JRpcHelper.AjaxResult(1, "修改错误，请稍后再试", UserListID));
        }

        /// <summary>
        /// 获取总部管理员和供应商权限关系
        /// </summary>
        /// <param name="AdminID">管理员ID</param>
        /// <returns></returns>
        [Route("GetAdminReSupplier")]
        [HttpPost]
        public IHttpActionResult GetAdminReSupplier(int AdminID)
        {
            var result = AdService.GetAdminReSupplier(AdminID);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
        }
    }
}
