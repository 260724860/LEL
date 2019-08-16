
#if NET45
using System.Web;
#else
using Microsoft.AspNetCore.Http;
#endif

namespace Senparc.Weixin.MP.Sample.CommonService.Utilities
{
    public static class Server
    {
        public static HttpContext HttpContext
        {
            get
            {
#if NET45
                HttpContext context = HttpContext.Current;
                if (context == null)
                {
                    HttpRequest request = new HttpRequest("Default.aspx", "https://sdk.weixin.senparc.com/default.aspx", null);
                    StringWriter sw = new StringWriter();
                    HttpResponse response = new HttpResponse(sw);
                    context = new HttpContext(request, response);
                }
#else
                HttpContext context = new DefaultHttpContext();
#endif
                return context;
            }
        }
    }
}
