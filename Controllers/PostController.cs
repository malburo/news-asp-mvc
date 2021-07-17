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

        public Reaction Check(int Id)
        {
            string userId = User.Identity.GetUserId();
            var react = context.Reactions.FirstOrDefault(r => r.PostId == Id && r.UserId == userId);
            if (react == null)
            {
                var newReact = new Reaction();
                newReact.PostId = Id;
                newReact.UserId = userId;
                newReact.Like = false;
                newReact.Dislike = false;
                newReact.CreatedAt = DateTime.Now;
                newReact.UpdatedAt = DateTime.Now;
                context.Reactions.Add(newReact);
                context.SaveChanges();
                return newReact;
            }
            else
            {
                return react;
            }
        }

        public ActionResult showPost(int id)
        {
            string userId = User.Identity.GetUserId();
            Post dbpost = context.Posts.FirstOrDefault(p => p.PostId == id);
            var reaction = Check(id);
            ViewBag.Like = "";
            if (reaction.Like == true)
            {
                ViewBag.Like = "fas";
            }
            else
            {
                ViewBag.Like = "far";
            }
            if (reaction.Dislike == true)
            {
                ViewBag.dislike = "fas";
            }
            else
            {
                ViewBag.dislike = "far";
            }
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