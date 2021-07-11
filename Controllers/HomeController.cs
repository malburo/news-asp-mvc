using NewsApplication.Models;
using NewsApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsApplication.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var postList = db.Posts.ToList();
            return View(postList);
        }

        public ActionResult Categories(int id)
        {
            Category category = db.Categories.FirstOrDefault(p => p.CategoryId == id);
            if (category == null)
            {
                HttpNotFound();
            }
            List<SubCategory> subCategories = db.SubCategories.Where(p => p.CategoryId == id).ToList();
            List<Post> categoryPosts = (from p in db.Posts
                                        where p.SubCategory.Any(s => s.CategoryId == id)
                                        orderby p.CreatedAt descending
                                        select p).Take(6).ToList();
            List<Post> subCategoryPosts = (from p in db.Posts
                                           where p.SubCategory.Any(s => s.CategoryId == id)
                                           orderby p.CreatedAt descending
                                           select p).ToList();
            ViewBag.SubCategories = subCategories;
            ViewBag.CategoryPosts = categoryPosts;
            ViewBag.SubCategoryPosts = subCategoryPosts;
            return View(category);
        }

        [ChildActionOnly]
        public ActionResult RenderTop3Posts(int id)
        {
            List<Post> Posts = (from p in db.Posts
                                where p.SubCategory.Any(s => s.SubCategoryId == id)
                                orderby p.CreatedAt descending
                                select p).Take(3).ToList();
            ViewBag.Posts = Posts;
            return PartialView("_Top_3_Posts");
        }

        [ChildActionOnly]
        public ActionResult RenderTop3PostsRE(int id)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            List<Post> posts = context.Posts.Where(p => p.SubCategory.Any(s => s.Posts.Any(p1 => p1.PostId == id))).ToList();
            posts = posts.OrderByDescending(p => p.CreatedAt).Take(3).ToList();
            ViewBag.Posts = posts;
            return PartialView("_Top_3_PostsRE");
        }
        
        public ActionResult DetailsCategory(int id)
        {
            SubCategory subCategory = db.SubCategories.FirstOrDefault(s => s.SubCategoryId == id);
            if (subCategory == null)
            {
                HttpNotFound();
            }
            List<Post> Posts = (from p in db.Posts
                                where p.SubCategory.Any(s => s.SubCategoryId == id)
                                orderby p.CreatedAt descending
                                select p).ToList();
            ViewBag.Posts = Posts;
            return View(subCategory);
        }
        public ActionResult Details()
        {
            return View();
        }
    }
}