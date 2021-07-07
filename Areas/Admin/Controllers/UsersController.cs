using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewsApplication.Models;

namespace NewsApplication.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Users
        public ActionResult Index()
        {
            var users = db.Users;
            return View(users);
        }

        [ChildActionOnly]
        public ActionResult RenderRoles(String id)
        {
            var roles = from role in db.Roles
                        where role.Users.Any(r => r.UserId == id)
                        select role;
            ViewBag.UserRoles = roles;
            return PartialView("RenderRoles");
        }
    }
}