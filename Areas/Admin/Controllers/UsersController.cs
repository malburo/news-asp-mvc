using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NewsApplication.Models;

namespace NewsApplication.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
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

        public ActionResult Edit(String id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_role = user.Roles.ToList();
            ViewBag.roles = db.Roles.ToList();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, string[] roles)
        {
            Microsoft.AspNet.Identity.EntityFramework.IdentityUser user = db.Users.Find(id);
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = db.Users.Find(id);
            if (roles == null)
            {
                ViewBag.user_role = user.Roles.ToList();
                ViewBag.roles = db.Roles.ToList();
                ViewBag.error = "Permission is not empty";
                return View(user);
            }
            var listRoles = db.Roles.ToList();
            if (ModelState.IsValid)
            {
                foreach (var role in listRoles)
                {
                    int i = 0;
                    foreach (var child_role in roles)
                    {
                        if (role.Id == child_role)
                        {
                            UserManager.AddToRole(users.Id, role.Name);
                            i++;
                            break;
                        }
                    }
                    if (i == 0)
                    {
                        UserManager.RemoveFromRole(users.Id, role.Name);
                    }
                }
                ViewBag.user_role = user.Roles.ToList();
                ViewBag.roles = db.Roles.ToList();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_role = user.Roles.ToList();
            ViewBag.roles = db.Roles.ToList();
            return View(user);
        }
    }
}