using System.Web.Mvc;

namespace LELAdmin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new LELExceptionFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
