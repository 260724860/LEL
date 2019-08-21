using System;
using System.Web.Mvc;

namespace LEL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult Result()
        {
            string msg = "59.9";
            Convert.ToInt32(msg);

            return Json(msg);
        }
    }
}
