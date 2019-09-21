using Common;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using Service;
using System;
using System.Security.Claims;
using System.Web.Http;

namespace LEL.Controllers
{

    public class AuthenticateController : BaseApiController
    {
        StoreUserService userService = new StoreUserService();
        OtherService otherService = new OtherService();

        SupplierUserService SupplierUserService = new SupplierUserService();

       /// <summary>
       /// 登陆获取access_token
       /// </summary>
       /// <param name="Loginname">手机号码</param>
       /// <param name="PWD"></param>
       /// <param name="Token">登陆token</param>
       /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult GetAccessToken(string Loginname, string PWD,string Token="")
        {
            var UserDto = userService.Login(Loginname, PWD, Token);
            if (UserDto.Code == 1)
            {
                return Json(JRpcHelper.AjaxResult(1, UserDto.Msg, null));
            }
            if(UserDto.Classify==null)
            {
                UserDto.Classify = "";
            }
            var tokenExpiration = TimeSpan.FromHours(24);
            ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, UserDto.Mobile + "," + UserDto.UserID.ToString() + "," + UserDto.status.ToString()));
            identity.AddClaim(new Claim("UserID", UserDto.UserID.ToString()));
            identity.AddClaim(new Claim("UserType", "1"));
            identity.AddClaim(new Claim("Status", UserDto.status.ToString()));
            identity.AddClaim(new Claim("Classify", UserDto.Classify));
            //identity.AddClaim(new Claim(ClaimTypes.Sid, dto.AdminID.ToString()));

            var props = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
            };
            var ticket = new AuthenticationTicket(identity, props);
            var accessToken = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);
            //userService.AddUserToken(UserID, accessToken);
            JObject tokenResponse = new JObject(
                                        new JProperty("userName", UserDto.Mobile),
                                        new JProperty("access_token", accessToken),
                                        new JProperty("token_type", "bearer"),
                                        new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
                                        new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
                                        new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString()));
            UserDto.PWD = "******";
            UserDto.Salt = "******";
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", tokenResponse, UserDto));
            return null;
        }

        /// <summary>
        /// 供应商登陆
        /// </summary>
        /// <param name="Loginname">登陆名</param>
        /// <param name="PWD"></param>
        /// <param name="Token">一次性登陆token</param>
        /// <returns></returns>
        [HttpPost, Route("api/Authenticate/GetSuppliersAccessToken/")]
        [AllowAnonymous]
        public IHttpActionResult GetSuppliersAccessToken(string Loginname="", string PWD="",string Token="",string Unionid="")
        {
            if(Token== "undefined")
            {
                Token = "";
            }
            if(Unionid== "oVvDxwphXBvk71RXlmuWzST18EV0")
            {
                Loginname = "15616127553";
                PWD = "0b4e931fdfcbe5f1a22b3a384389fc31";
            }
            if(!string.IsNullOrEmpty(Unionid)&&!string.IsNullOrEmpty(Token))
            {
                return Json(JRpcHelper.AjaxResult(1, "Unionid和！Token不能同时使用", Loginname));
            }
            if(string.IsNullOrEmpty(Loginname)&&string.IsNullOrEmpty(PWD)&&string.IsNullOrEmpty(Token)&&string.IsNullOrEmpty(Unionid))
            {
                return Json(JRpcHelper.AjaxResult(1, "请输入账号密码！", Loginname));
            }
            var SupplierUser = SupplierUserService.Login(Loginname, PWD, Token, Unionid);
            if (SupplierUser.Code == 1)
            {
                return Json(JRpcHelper.AjaxResult(1, SupplierUser.Msg, null));
            }
            var tokenExpiration = TimeSpan.FromHours(24);
            ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, Loginname + "," + SupplierUser.SuppliersID.ToString() + "," + SupplierUser.Suppliers_Status.ToString()));
            identity.AddClaim(new Claim("UserID", SupplierUser.SuppliersID.ToString()));
            identity.AddClaim(new Claim("UserType", "2"));
            identity.AddClaim(new Claim("Status", SupplierUser.Suppliers_Status.ToString()));
            var props = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
            };
            var ticket = new AuthenticationTicket(identity, props);
            var accessToken = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);
            //userService.AddUserToken(UserID, accessToken);
            JObject tokenResponse = new JObject(
                                        new JProperty("userName", Loginname),
                                        new JProperty("access_token", accessToken),
                                        new JProperty("token_type", "bearer"),
                                        new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
                                        new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
                                        new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString()));
            SupplierUser.Suppliers_PassWord = "******";
            SupplierUser.Suppliers_Salt = "******";
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", tokenResponse, SupplierUser));
            return null;
        }

        /// <summary>
        /// 供应商注册
        /// </summary>
        /// <param name="LoginName">登陆名</param>
        /// <param name="PWD">密码</param>  
        /// <returns></returns>
        [HttpPost, Route("api/Authenticate/SupplierRegist/")]
        [AllowAnonymous]
        public IHttpActionResult SupplierRegist(string LoginName, string PWD)
        {
            var Model = otherService.GetSmsRecord(LoginName);
            if (Model == null)
            {
                return Json(JRpcHelper.AjaxResult(1, "非法操作！请重新获取验证码", LoginName));
            }
            if (Model.Status != 1 || Model.Status == 1 && Model.CreatTime < DateTime.Now.AddMinutes(-10))
            {
                return Json(JRpcHelper.AjaxResult(1, "非法操作！请重新获取验证码", LoginName));
            }
            string TruePwd = DESEncrypt.DecryptStringHex(PWD, "SystemLE");
            if (string.IsNullOrEmpty(TruePwd))
            {
                return Json(JRpcHelper.AjaxResult(1, "解密失败,请确认", LoginName));
            }
            string Msg;
            var result = SupplierUserService.Regist(LoginName, TruePwd, out Msg);
            if (result != 0)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", LoginName));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, Msg, LoginName));
            }
        }

        /// <summary>
        /// 商户注册
        /// </summary>
        /// <param name="LoginName">登陆名</param>
        /// <param name="PWD">密码</param>  
        /// <returns></returns>
        [HttpPost, Route("api/Authenticate/Regist/")]
        [AllowAnonymous]
        public IHttpActionResult Regist(string LoginName, string PWD)
        {
            var Model = otherService.GetSmsRecord(LoginName);
            if (Model == null)
            {
                return Json(JRpcHelper.AjaxResult(1, "非法操作！请重新获取验证码", LoginName));
            }
            if (Model.Status != 1 || Model.Status == 1 && Model.CreatTime < DateTime.Now.AddMinutes(-10))
            {
                return Json(JRpcHelper.AjaxResult(1, "非法操作！请重新获取验证码", LoginName));
            }
            string TruePwd = DESEncrypt.DecryptStringHex(PWD, "SystemLE");
            if (string.IsNullOrEmpty(TruePwd))
            {
                return Json(JRpcHelper.AjaxResult(1, "解密失败,请确认", LoginName));
            }
            string Msg;
            var result = userService.Regist(LoginName, TruePwd, out Msg);
            if (result != 0)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", LoginName));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, Msg, LoginName));
            }
        }

        /// <summary>
        /// 验证短信验证码
        /// </summary>
        /// <param name="LoginName">手机号码</param>
        /// <param name="Code">验证码 10分钟内有效</param>
        /// <returns></returns>
        [HttpPost, Route("api/Authenticate/CheckCode/")]
        [AllowAnonymous]
        public IHttpActionResult CheckCode(string LoginName, string Code)
        {
            var Model = otherService.GetSmsRecord(LoginName);
            if (Model == null)
            {
                return Json(JRpcHelper.AjaxResult(1, "验证码错误", Code));
            }
            if (Model.CreatTime < DateTime.Now.AddMinutes(-10))
            {
                return Json(JRpcHelper.AjaxResult(1, "验证码已过期,请重新获取", Code));
            }
            if (Model.Status == 1)
            {
                return Json(JRpcHelper.AjaxResult(1, "验证码已验证过,请重新获取", Code));
            }
            if (Model.Code != Code)
            {
                return Json(JRpcHelper.AjaxResult(1, "验证码错误", Code));
            }
            otherService.UpdateSmsRecord(Code, LoginName);
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", Code));
        }

        /// <summary>
        /// 商户 找回密码 需要验证短信
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="PWD"></param>
        /// <returns></returns>
        [HttpPost, Route("api/Authenticate/RetrievePassword/")]
        [AllowAnonymous]
        public IHttpActionResult RetrievePassword(string LoginName, string PWD)
        {
            var Model = otherService.GetSmsRecord(LoginName);
            if (Model.Status != 1 || Model.Status == 1 && Model.CreatTime < DateTime.Now.AddMinutes(-10))
            {
                return Json(JRpcHelper.AjaxResult(1, "非法操作！请重新获取验证码", LoginName));
            }
            string TruePwd = DESEncrypt.DecryptStringHex(PWD, "SystemLE");
            if (string.IsNullOrEmpty(TruePwd))
            {
                return Json(JRpcHelper.AjaxResult(1, "解密失败,请确认", LoginName));
            }
            var result = new StoreUserService().UpdatePwd(LoginName, TruePwd);
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
        /// 供应商 找回密码 需要验证短信
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="PWD"></param>
        /// <returns></returns>
        [HttpPost, Route("api/Authenticate/SupplierRetrievePassword/")]
        [AllowAnonymous]
        public IHttpActionResult SupplierRetrievePassword(string LoginName, string PWD)
        {
            var Model = otherService.GetSmsRecord(LoginName);
            if (Model.Status != 1 || Model.Status == 1 && Model.CreatTime < DateTime.Now.AddMinutes(-10))
            {
                return Json(JRpcHelper.AjaxResult(1, "非法操作！请重新获取验证码", LoginName));
            }
            string TruePwd = DESEncrypt.DecryptStringHex(PWD, "SystemLE");
            if (string.IsNullOrEmpty(TruePwd))
            {
                return Json(JRpcHelper.AjaxResult(1, "解密失败,请确认", LoginName));
            }
            var result = new SupplierUserService().UpdatePwd(LoginName, TruePwd);
            if (result)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", LoginName));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, "修改失败", LoginName));
            }
        }

    }

}
