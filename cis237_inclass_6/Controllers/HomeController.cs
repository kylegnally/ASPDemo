using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cis237_inclass_6.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // This tells the framework to go find the View with the
            // Name Index inside the Home folder of the Views folder.
            // It is using the convention of controller name and
            // action name to find the correct view.
            // For this one it will be Views->Home->Index.cshtml
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}