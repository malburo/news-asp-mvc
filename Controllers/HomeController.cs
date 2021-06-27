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

        public ActionResult Test()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var listUser = context.Users.ToList();
            return View(listUser);
        }

        public ActionResult Test2()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var listUser = context.Roles.ToList();
            return View(listUser);
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