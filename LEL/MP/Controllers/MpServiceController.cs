using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senparc.CO2NET.Extensions;
using Senparc.NeuChar.App.AppStore;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin;
using OAuthAccessTokenResult = Senparc.Weixin.MP.AdvancedAPIs.OAuth.OAuthAccessTokenResult;

namespace MP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MpServiceController : Controller
    {
        //下面换成账号对应的信息，也可以放入web.config等地方方便配置和更换
        public readonly string appId = Senparc.Weixin.Config.SenparcWeixinSetting.WeixinAppId;//与微信公众账号后台的AppId设置保持一致，区分大小写。
        private readonly string appSecret = Senparc.Weixin.Config.SenparcWeixinSetting.WeixinAppSecret;//与微信公众账号后台的AppId设置保持一致，区分大小写。
        
        /// <summary>
        /// 获取Url授权链接
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetAuthorizeUrl")]
        public ActionResult GetAuthorizeUrl(string appid, string url, string state)
        {
            if (string.IsNullOrEmpty(appid))
            {
                appid = appId;
            }
            string result = OAuthApi.GetAuthorizeUrl(appid,
               url,
               state, OAuthScope.snsapi_userinfo);

             return Json(new { code = 0, msg = "SUCCESS", content = result });
        }

        /// <summary>
        /// 获取用户信息授权回调获取
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet,Route("UserInfoCallback")]
        public ActionResult UserInfoCallback(string appid, string code, string state, string returnUrl)
        {
            if (string.IsNullOrEmpty(code))
            {
                // return Content("您拒绝了授权！");
                return Json(new { code = 0, msg = "您拒绝了授权", content = "" });
            }
            //if (state != HttpContext.Session.GetString("State"))
            //{
                //这里的state其实是会暴露给客户端的，验证能力很弱，这里只是演示一下，
                //建议用完之后就清空，将其一次性使用
                //实际上可以存任何想传递的数据，比如用户ID，并且需要结合例如下面的Session["OAuthAccessToken"]进行验证
                //return Content("验证失败！请从正规途径进入！");
            //}

           OAuthAccessTokenResult result = null;

            //通过，用code换取access_token
            try
            {
                result = OAuthApi.GetAccessToken(appId, appSecret, code);
            }
            catch (Exception ex)
            {
                return Json(new { code = 0, msg = ex.Message, content = ex });
                //return Content(ex.Message);
            }
            if (result.errcode != ReturnCode.请求成功)
            {
                return Json(new { code = 0, msg = result.errcode, content = result });
               // return Content("错误：" + result.errmsg);
            }
            //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
            //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的

            //HttpContext.Session.SetString("OAuthAccessTokenStartTime", SystemTime.Now.ToString());
            //HttpContext.Session.SetString("OAuthAccessToken", result.ToJson());

            //因为第一步选择的是OAuthScope.snsapi_userinfo，这里可以进一步获取用户详细信息
            try
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                OAuthUserInfo userInfo = OAuthApi.GetUserInfo(result.access_token, result.openid);
                //return View(userInfo);
                return Json(new { code = 0, msg = "SUCCESS", content = userInfo , accattach = appId });
            }
            catch (ErrorJsonResultException ex)
            {
                //return Content(ex.Message);
                return Json(new { code = 0, msg = ex.Message, content = ex });
            }
        }
    }
}