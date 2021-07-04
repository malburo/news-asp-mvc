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
        // GET: Post
        public ActionResult showPost( int id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            Post dbpost = context.Posts.FirstOrDefault(p => p.PostId==id);
            return View(dbpost);
        }

        public ActionResult relatePost()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            
            return View();
        }

    }
}