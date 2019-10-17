using Senparc.Weixin.MP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.HttpUtility;

using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;
using Senparc.Weixin.WxOpen.Containers;
using Senparc.Weixin.WxOpen.Entities;
using Senparc.Weixin.WxOpen.Entities.Request;
using Senparc.Weixin.WxOpen.Helpers;
using System;
using System.IO;
using Senparc.Weixin.TenPay.V3;
using Senparc.CO2NET.Utilities;
using Senparc.Weixin.MP;
using Senparc.Weixin;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.Exceptions;

namespace LEL.Controllers
{
    /// <summary>
    /// 微信和小程序接口
    /// </summary>
    [RoutePrefix("api/MpAndOpen")]
    public class MpAndOpenController : ApiController
    {
        //下面换成账号对应的信息，也可以放入web.config等地方方便配置和更换
        public readonly string appId = Senparc.Weixin.Config.SenparcWeixinSetting.WeixinAppId;//与微信公众账号后台的AppId设置保持一致，区分大小写。
        private readonly string appSecret = Senparc.Weixin.Config.SenparcWeixinSetting.WeixinAppSecret;//与微信公众账号后台的AppId设置保持一致，区分大小写。
        public static readonly string WxOpenAppId = Config.SenparcWeixinSetting.WxOpenAppId;//与微信小程序后台的AppId设置保持一致，区分大小写。
        public static readonly string WxOpenAppSecret = Config.SenparcWeixinSetting.WxOpenAppSecret;//与微信小程序账号后台的AppId设置保持一致，区分大小写。

        /// <summary>
        /// 获取给前端UI使用的JSSDK信息包（扫一扫/分享等功能)
        /// </summary>
        /// <param name="Url">当前页面Url</param>
        /// <returns></returns>
        [HttpGet, Route("GetJsSdkUiPackage")]
        public IHttpActionResult GetJsSdkUiPackage(string Url)
        {
            try
            {
                var jssdkUiPackage = JSSDKHelper.GetJsSdkUiPackage(appId, appSecret, Url);
                return Json(new { code = 0, msg = "SUCCESS", content = jssdkUiPackage });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = ex.Message, content = ex });
            }

        }
        /// <summary>
        /// 获取Url授权链接
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetAuthorizeUrl")]
        public IHttpActionResult GetAuthorizeUrl(string appid, string url, string state)
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
        /// <param name="appid">公众号appid默认</param>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet, Route("UserInfoCallback")]
        public IHttpActionResult UserInfoCallback(string appid, string code, string state, string returnUrl)
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
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
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


                OAuthUserInfo userInfo = OAuthApi.GetUserInfo(result.access_token, result.openid);
                //return View(userInfo);
                return Json(new { code = 0, msg = "SUCCESS", content = userInfo, accattach = appId });
            }
            catch (ErrorJsonResultException ex)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                //return Content(ex.Message);
                return Json(new { code = 0, msg = ex.Message, content = ex });
            }
        }
        /// <summary>
        /// wx.login登陆成功之后发送的请求
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost, Route("OnLogin")]
        public IHttpActionResult OnLogin(string code)
        {
            try
            {
                var jsonResult = SnsApi.JsCode2Json(WxOpenAppId, WxOpenAppSecret, code);
                if (jsonResult.errcode == ReturnCode.请求成功)
                {
                    //Session["WxOpenUser"] = jsonResult;//使用Session保存登陆信息（不推荐）
                    //使用SessionContainer管理登录信息（推荐）
                    var unionId = jsonResult.unionid;
                    var sessionBag = SessionContainer.UpdateSession(null, jsonResult.openid, jsonResult.session_key, unionId);

                    //注意：生产环境下SessionKey属于敏感信息，不能进行传输！
                    // return Json(new { success = true, msg = "OK", sessionId = sessionBag.Key, sessionKey = sessionBag.SessionKey });
                    return Json(new { code = 0, msg = "OK.生产环境下SessionKey属于敏感信息，不能进行传输", content = sessionBag, unionId = unionId });
                }
                else
                {
                    return Json(new { code = 1, msg = jsonResult.errmsg });
                }
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = ex.Message, content = ex });
            }

        }
        /// <summary>
        /// 签名校验
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="rawData"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        [HttpPost, Route("CheckWxOpenSignature")]
        public IHttpActionResult CheckWxOpenSignature(string sessionId, string rawData, string signature)
        {
            try
            {
                var checkSuccess = Senparc.Weixin.WxOpen.Helpers.EncryptHelper.CheckSignature(sessionId, rawData, signature);
                return Json(new { success = checkSuccess, msg = checkSuccess ? "签名校验成功" : "签名校验失败" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sessionId"></param>
        /// <param name="encryptedData"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        [HttpPost, Route("DecodeEncryptedData")]
        public IHttpActionResult DecodeEncryptedData(string type, string sessionId, string encryptedData, string iv)
        {
            DecodeEntityBase decodedEntity = null;
            switch (type.ToUpper())
            {
                case "USERINFO"://wx.getUserInfo()
                    decodedEntity = Senparc.Weixin.WxOpen.Helpers.EncryptHelper.DecodeUserInfoBySessionId(
                        sessionId,
                        encryptedData, iv);
                    break;
                default:
                    break;
            }

            //检验水印
            var checkWartmark = false;
            if (decodedEntity != null)
            {
                checkWartmark = decodedEntity.CheckWatermark(WxOpenAppId);
            }

            //注意：此处仅为演示，敏感信息请勿传递到客户端！
            return Json(new
            {
                success = checkWartmark,
                //decodedEntity = decodedEntity,
                msg = string.Format("水印验证：{0}",
                        checkWartmark ? "通过" : "不通过")
            });
        }
    }
}
