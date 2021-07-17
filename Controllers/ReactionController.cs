using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NewsApplication.Models;

namespace NewsApplication.Controllers
{
    [Authorize]
    public class ReactionController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        
        public Reaction Check(int Id){
            string userId = User.Identity.GetUserId();
            var react = context.Reactions.FirstOrDefault(r => r.PostId == Id && r.UserId == userId);
            if(react == null){
                var newReact =new Reaction();
                newReact.PostId = Id;
                newReact.UserId = userId;
                newReact.Like = false;
                newReact.Dislike = false;
                newReact.CreatedAt = DateTime.Now;
                newReact.UpdatedAt = DateTime.Now;
                context.Reactions.Add(newReact);
                context.SaveChanges();
                return newReact;
            }else{
                return react;
            }
        }

        [HttpPost]
        public JsonResult Like(int postId)
        {
            var reaction = Check(postId);
            if (reaction.Like == true)
            {
                reaction.Like = false;
                context.SaveChanges();
                return Json("unlike", JsonRequestBehavior.AllowGet);
            }
            else {
                reaction.Like = true;
                reaction.Dislike = false;
                context.SaveChanges();
                return Json("like", JsonRequestBehavior.AllowGet);
            }   
        }   
        public JsonResult Dislike(int postId)
        {
            var reaction = Check(postId);
            if (reaction.Dislike == true)
            {
                reaction.Dislike = false;
                context.SaveChanges();
                return Json("undislike", JsonRequestBehavior.AllowGet);
            }
            else {
                reaction.Dislike = true;
                reaction.Like = false;
                context.SaveChanges();
                return Json("dislike", JsonRequestBehavior.AllowGet);
            }   
        }   
    }   
}