using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace LEL.Oauth
{
    public class HandleUnauthorizedRequestS: AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
        }
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
          //  actionContext.
            return base.IsAuthorized(actionContext);
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
        }
    }
}