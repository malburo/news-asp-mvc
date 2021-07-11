using Microsoft.AspNet.Identity;
using NewsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsApplication.Controllers
{
    [AllowAnonymous]
    public class PostController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult showPost(int id)
        {
            Post dbpost = context.Posts.FirstOrDefault(p => p.PostId == id);
            return View(dbpost);
        }

        [ChildActionOnly]
        public ActionResult rate3PostPartial(int id)
        {
            var posts = (from p in context.Posts orderby p.PostId descending select p).Take(3).ToList();
            ViewBag.posts = posts;
            return PartialView("_rate3postPartial");

        }
        [ChildActionOnly]
        public ActionResult new3PostPartial(int id)
        {
            var posts = (from p in context.Posts orderby p.CreatedAt descending select p).Take(3).ToList();
            ViewBag.post = posts;
            return PartialView("_new3PostPartial");
        }

        [ChildActionOnly]
        public ActionResult relatedPostPartial(int id)
        {
            var listsub = context.Posts.Find(id).SubCategory.First();
            var posts = from p in context.Posts
                        where p.SubCategory.Any(s => s.SubCategoryId == listsub.CategoryId)
                        select p;
            ViewBag.Posts = posts.OrderByDescending(p => p.CreatedAt).Take(3).ToList();
            return PartialView("_relatedPostPartial");
        }

        public ActionResult Search(string searchString)
        {
            var search = context.Posts.SqlQuery("Select * from Post Where Title like '%" + searchString + "%'").ToList();
            ViewBag.Search = search;
            return View();
        }
    }
}