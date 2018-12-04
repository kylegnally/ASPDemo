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
            // This is the ViewBag. It can be used to store values that we want
            // to send over to the view from the controller.
            // ViewBag is a dynamic object where we can just add properties on
            // the fly to it, and then access them in the view.
            ViewBag.Message = "Your contact page.";

            // View is one more ActionResult that will look into the Views folder
            // for a view that matches the controller and method.
            // Inside the Views folder it will look for a Home folder. Then it
            // will look for a Contact.cshtml page inside that folder.
            return View();
        }

        public ActionResult Foobar()
        {
            // Content is one of the ActionResult return types that can be
            // returned. Content just returns a string, but is good for testing
            // to ensure that a url returns the correct controller method.
            // Once that is established, you may want to change it to on of the other
            // types.
            return Content("The foobar page");

        }
    }
}