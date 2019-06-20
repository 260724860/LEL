using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LEL.App_Start
{
    public class UserStatusActionfilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //var claimIdentity = (ClaimsIdentity)User.Identity;
            //Convert.ToInt32(claimIdentity.FindFirstValue("Status"));
            base.OnActionExecuting(actionContext);
        }

    }
}