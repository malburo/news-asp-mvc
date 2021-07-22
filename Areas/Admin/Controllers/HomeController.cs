using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsApplication.Models;

namespace NewsApplication.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator, Staff")]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            ViewBag.CountPosts = db.Posts.Count();
            ViewBag.CountCategories = db.Categories.Count();
            ViewBag.CountAccounts = db.Users.Count();
            ViewBag.CountSubmenu = db.SubCategories.Count();
            return View();
        }
    }
}