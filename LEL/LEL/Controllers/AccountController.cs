using Common;
using DTO.User;
using LEL.App_Start;
using Microsoft.AspNet.Identity;
using Service;
using System;
using System.Security.Claims;
using System.Web.Http;
namespace LEL.Controllers
{

    [Authorize]
    public class AccountController : ApiController
    {
        private StoreUserService StoreUserService = new StoreUserService();
        private OtherService OtherService = new OtherService();

        private int UserID { get; set; }
        private int Status { get; set; }
        private int GetUserID()
        {
            UserID = Convert.ToInt32(User.Identity.Name.Split(',')[1]);
            Status = Convert.ToInt32(User.Identity.Name.Split(',')[2]);

            var claimIdentity = (ClaimsIdentity)User.Identity;
            var HspUID = claimIdentity.FindFirstValue("RoleID");
            if (Status == 0)
            {
                return -1;
            }
            return UserID;
        }


        /// <summary>
        /// 加盟店上传资料
        /// </summary>
        /// <param name="dTO"></param>
        /// <returns></returns>
        [UserStatusActionfilter]
        [HttpPost, Route("api/Account/Update/")]
        public IHttpActionResult Update(UserDTO dTO)
        {
            if (dTO == null)
            {
                return Json(JRpcHelper.AjaxResult(1, "参数错误请检查", null));
            }

            dTO.UserID = Convert.ToInt32(User.Identity.Name.Split(',')[1]);
            var result = StoreUserService.Update(dTO, true);
            if (result)
            {
                return Json(JRpcHelper.AjaxResult(0, "SUCCESS", result));
            }
            else
            {
                return Json(JRpcHelper.AjaxResult(1, "修改失败！", result));
            }
            return null;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="LoginName">账号</param>
        /// <param name="NewPWD">新密码</param>
        /// <param name="OriginalPWD">旧密码</param>
        /// <returns></returns>
        [HttpPost, Route("api/Account/UpdatePwd/")]
        public IHttpActionResult UpdatePwd(string LoginName, string NewPWD, string OriginalPWD)
        {
            //var Model = OtherService.GetSmsRecord(LoginName);
            //if (Model.Status != 1 || Model.Status == 1 && Model.CreatTime < DateTime.Now.AddMinutes(-10))
            //{
            //    return Json(JRpcHelper.AjaxResult(1, "非法操作！请重新获取验证码", LoginName));
            //}
            var UserDto = new StoreUserService().Login(LoginName, OriginalPWD);
            if (UserDto.Code == 1)
            {
                return Json(JRpcHelper.AjaxResult(1, UserDto.Msg, null));
            }
            string TruePwd = DESEncrypt.DecryptStringHex(NewPWD, "SystemLE");
            if (string.IsNullOrEmpty(TruePwd))
            {
                return Json(JRpcHelper.AjaxResult(1, "解密失败,请确认", LoginName));
            }
            var result = StoreUserService.UpdatePwd(LoginName, TruePwd);
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
