using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaptchaWebAPI.Controllers
{
    /// <summary>
    /// Home Controller for Home Page
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Default action method.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
