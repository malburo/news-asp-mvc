using Microsoft.AspNet.Identity;
using NewsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsApplication.Controllers
{
    [Authorize]
    public class BookmarkController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        // GET: Bookmark
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var bookmarkList = context.Bookmarks.Where(b => b.UserId == userId).ToList();
            return View(bookmarkList);
        }
        [HttpPost]
        public JsonResult Bookmark(int Id)
        {
            string userId = User.Identity.GetUserId();
            if (context.Bookmarks.Any(i => i.PostId == Id && i.UserId == userId))
            {
                return Json("failed", JsonRequestBehavior.DenyGet);
            }
            Bookmark newBookmark = new Bookmark();
            newBookmark.PostId = Id;
            newBookmark.UserId = userId;
            newBookmark.CreateAt = DateTime.Now;
            context.Bookmarks.Add(newBookmark);
            context.SaveChanges();
            return Json("thanh cong", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteBookmark(int Id)
        {
            string userId = User.Identity.GetUserId();
            var currentBookmark = context.Bookmarks.FirstOrDefault(i => i.PostId == Id && i.UserId == userId);
            context.Bookmarks.Remove(currentBookmark);
            context.SaveChanges();
            return Json("thanh cong", JsonRequestBehavior.AllowGet);
        }
    }
}