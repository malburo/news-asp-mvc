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
    public class CommentController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        // GET: Comment
        [ChildActionOnly]
        public ActionResult Index(int Id)
        {
            var commentList = context.Comments.Where(item => item.PostId == Id).ToList();
            return PartialView("_CommentPartial", commentList);
        }
        [HttpPost]
        public JsonResult CreateComment(int postId, string content)
        {
            string userId = User.Identity.GetUserId();
            Comment newComment = new Comment
            {
                PostId = postId,
                UserId = userId,
                Content = content,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            context.Comments.Add(newComment);
            context.SaveChanges();

            var response = context.Comments.Find(newComment.CommentId);
            return Json(new { message = "Success", data = response }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CreateSubComment(int commentId, string content)
        {
            string userId = User.Identity.GetUserId();
            SubComment newSubComment = new SubComment
            {
                CommentId = commentId,
                UserId = userId,
                Content = content,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            context.SubComments.Add(newSubComment);
            context.SaveChanges();
            return Json(new { message = "Success", data = newSubComment }, JsonRequestBehavior.AllowGet);
        }
    }
}