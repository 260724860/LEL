using DTO.User;
using Microsoft.AspNet.Identity;
using System;
using System.Security.Claims;
using System.Web.Http;

namespace LEL.Controllers
{
    [Authorize]
    public class BaseApiController : ApiController
    {
        protected LoginInfo GetLoginInfo()
        {
            LoginInfo info = new LoginInfo();
            var claimIdentity = (ClaimsIdentity)User.Identity;
            info.LoginName = User.Identity.Name.Split(',')[0];
            info.Status = Convert.ToInt32(claimIdentity.FindFirstValue("Status"));
            info.UserType = Convert.ToInt32(claimIdentity.FindFirstValue("UserType"));
            info.UserID = Convert.ToInt32(claimIdentity.FindFirstValue("UserID"));

            if (info.Status != 1 && info.UserType == 1)
            {
                // return Json(JRpcHelper.AjaxResult(10000, "账号未通过审核或已禁用", info.UserID));
                throw new Exception("账号未通过审核或已禁用");
            }
            return info;
        }
    }

}
