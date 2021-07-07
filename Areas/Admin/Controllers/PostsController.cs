using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NewsApplication.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;

using System.Web;
using System.Web.Mvc;

namespace NewsApplication.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Posts
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.Author);
            return View(posts.ToList());
        }

        // GET: Admin/Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Posts/Create
        public ActionResult Create()
        {
            ViewBag.subcategory = db.SubCategories.ToList();
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Content,ImageUrl,Views")] Post post, int[] sub_category_id)
        {
            ViewBag.subcategory = db.SubCategories.ToList();
            if (sub_category_id == null)
            {
                ViewBag.error = "Category is not empty";
                return View();
            }
            ApplicationUser user = System.Web.HttpContext.Current
                   .GetOwinContext()
                   .GetUserManager<ApplicationUserManager>()
                   .FindById(System.Web.HttpContext.Current.User.Identity
                   .GetUserId());
            post.AuthorId = user.Id;
            post.Views = 0;
            post.CreatedAt = DateTime.Now;
            post.UpdatedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                foreach (var sub in sub_category_id)
                {
                    SubCategory sub_category = db.SubCategories.Find(sub);
                    sub_category.Posts.Add(post);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        [ValidateInput(false)]
        // GET: Admin/Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.subcategory_post = post.SubCategory.ToList();
            ViewBag.subcategory = db.SubCategories.ToList();
            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, int[] SubCategory)
        {
            
            Post post = db.Posts.Find(id);
            if (SubCategory == null)
            {
                ViewBag.subcategory = db.SubCategories.ToList();
                ViewBag.subcategory_post = post.SubCategory.ToList();
                ViewBag.error = "Category is not empty";
                return View(post);
            }
            var listSub = db.SubCategories.ToList();
            if (ModelState.IsValid)
            {
                post.ImageUrl = Request["ImageUrl"];
                post.Title = Request["Title"];
                post.UpdatedAt = DateTime.Now;
                post.Content = Request["Content"];
                post.UpdatedAt = DateTime.Now;

                foreach (var sub_cate in listSub)
                {
                    int i = 0;
                    foreach (var sub_child in SubCategory)
                    {
                        if(sub_cate.SubCategoryId == sub_child)
                        {
                            SubCategory sub_category = db.SubCategories.Find(sub_child);
                            sub_category.Posts.Add(post);
                            i++;
                            break;
                        }
                    }
                    if(i == 0)
                    {
                        sub_cate.Posts.Remove(post);
                    }
                }
                ViewBag.subcategory = db.SubCategories.ToList();
                ViewBag.subcategory_post = post.SubCategory.ToList();
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.subcategory = db.SubCategories.ToList();
            ViewBag.subcategory_post = post.SubCategory.ToList();
            return View(post);
        }

        // GET: Admin/Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
