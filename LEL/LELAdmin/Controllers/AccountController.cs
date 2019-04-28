
using Common;
using DTO.Admin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace LELAdmin.Controllers
{
    /// <summary>
    /// 账号管理 API
    /// </summary>
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private Service.AdminService AdService = new Service.AdminService();

        #region 后台用户管理
        /// <summary>
        /// 后台用户登录
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="PWD"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpGet]
        public IHttpActionResult Login(string LoginName, string PWD)
        {
            try
            {
                var dto = AdService.Login(LoginName, PWD);
                if (dto.code != 0)
                {
                    return Json(new { code = 0, msg = "ERROR", content = dto.msg });
                }
                var tokenExpiration = TimeSpan.FromHours(2);
                ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, LoginName + "," + dto.AdminID.ToString()));
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
                return Json(new { code = 0, msg = "ERROR", content = ex.ToString() });
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
        public IHttpActionResult AddAdmin (AdminDTO model)
        {
            try
            {
                string message;

                var bol = AdService.AddAdmin(model, out message);

                if (message.Equals("SUCCESS"))
                {
                    return Json(new { code = 0, msg = "SUCCESS", content = message });
                }
                else {
                    return Json(new { code = 1, msg = "ERROR", content = message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = "ERROR", content = ex.ToString() });
            }
        }
        #endregion
    }
}
