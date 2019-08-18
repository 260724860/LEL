using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;

namespace MP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MpServiceController : ControllerBase
    {

        /// <summary>
        /// 获取Url授权链接
        /// </summary>
        /// <returns></returns>
        //[HttpGet,Route("GetAuthorizeUrl")]
        //public ActionResult GetAuthorizeUrl(string Appid,string url,string state)
        //{
        //   string result= OAuthApi.GetAuthorizeUrl(Appid,
        //      "http://sdk.weixin.senparc.com/oauth2/UserInfoCallback?returnUrl=" + url.UrlEncode(),
        //      state, OAuthScope.snsapi_userinfo);

        //   // return Content(new { code = 1, msg = ex.Message, content = ex });
        //}
      
    }
}