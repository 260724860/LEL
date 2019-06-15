using Common;
using DTO.User;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace LELAdmin.Controllers
{
    [Authorize]
    public class BaseController : ApiController
    {
        //public UserInfo userInfo { get; set; }

        protected LoginInfo GetLoginInfo()
        {
            LoginInfo info = new LoginInfo();
            var claimIdentity = (ClaimsIdentity)User.Identity;
            info.LoginName = claimIdentity.Name.ToString();
            info.Status = Convert.ToInt32(claimIdentity.FindFirstValue("Status"));
            info.UserType = Convert.ToInt32(claimIdentity.FindFirstValue("UserType"));
            info.UserID = Convert.ToInt32(claimIdentity.FindFirstValue("UserID"));
           
            if (info.Status != 1)
            {
                // return Json(JRpcHelper.AjaxResult(10000, "账号未通过审核或已禁用", info.UserID));
                throw new Exception("账号未通过审核或已禁用");
            }
            return info;
        }
    }
}
