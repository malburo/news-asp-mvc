using Microsoft.AspNet.Identity;
using NewsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsApplication.Controllers
{
    public class PostController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        // GET: Post
        public ActionResult showPost(int id)
        {
            Post dbpost = context.Posts.FirstOrDefault(p => p.PostId == id);
            return View(dbpost);
        }

        public ActionResult relatePost()
        {
            return View();
        }
    }
}