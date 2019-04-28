using Common;
using DTO.User;
using LEL;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace LEL.Controllers
{
    
    public class AuthenticateController : ApiController
    {
        StoreUserService userService = new StoreUserService();
        OtherService otherService = new OtherService();
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="Loginname">登陆名</param>
        /// <param name="PWD"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult GetAccessToken(string Loginname, string PWD)
        {          
            var UserID = userService.Login(Loginname, PWD,out string Msg);
            if(UserID<=0)
            {
                return Json( JRpcHelper.AjaxResult(0,"账号或者密码错误",null));
            }
            var tokenExpiration = TimeSpan.FromHours(2);
            ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, Loginname+","+ UserID.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Sid, UserID.ToString()));
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

            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", tokenResponse));
            return null;
        }

        /// <summary>
        /// 商户注册
        /// </summary>
        /// <param name="LoginName">登陆名</param>
        /// <param name="PWD">密码</param>
        /// <param name="Code">验证码 10分钟内有效</param>
        /// <returns></returns>
        [HttpPost, Route("api/Account/Regist/")]
        public IHttpActionResult Regist(string LoginName,string PWD,string Code)
        {
            var Model=  otherService.GetSmsRecord(LoginName);
            if(Model==null)
            {
                return Json(JRpcHelper.AjaxResult(1, "验证码错误", Code));
            }
            if(Model.CreatTime<DateTime.Now.AddMinutes(-10))
            {
                return Json(JRpcHelper.AjaxResult(1, "验证码已过期,请重新获取", Code));
            }
            if(Model.Status==1)
            {
                return Json(JRpcHelper.AjaxResult(1, "验证码已验证过,请重新获取", Code));
            }
            otherService.UpdateSmsRecord(Code, LoginName);
           

            string TruePwd = DESEncrypt.Decrypt(PWD, "SystemLEL");
            if(string.IsNullOrEmpty(TruePwd))
            {
                return Json(JRpcHelper.AjaxResult(1, "解密失败,请确认", Code));
            }
            string Msg;
            var result= userService.Regist(LoginName, TruePwd, out Msg);
            if(result!=0)
            {
                return  Json(JRpcHelper.AjaxResult(0, "SUCCESS", Code));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, Msg, LoginName));
            }
        }

    }

}
