using NewsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Categories()
        {

            return View();
        }

        public ActionResult DetailsCategory()
        {

            return View();
        }
        public ActionResult Details()
        {

            return View();
        }
    }
}