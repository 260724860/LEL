using Common;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class DataHandlingController : ApiController
    {
        public IHttpActionResult CleanUpDuplication()
        {
            new DataHandlingService().CleanUpDuplication();
            return Json(JRpcHelper.AjaxResult(0, "SUCCESS", null));
        }
    }
}
