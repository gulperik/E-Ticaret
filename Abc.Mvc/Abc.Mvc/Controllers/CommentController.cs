using Abc.Mvc.Entity;
using Abc.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abc.Mvc.Controllers
{
    public class CommentController : Controller
    {
        DataContext _context = new DataContext();
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult AddedComment()
        {
            
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddedComment(string UserComment)
        {
            if (string.IsNullOrWhiteSpace(UserComment))
            {
                TempData["ErrorMessageComment"] = "Bu alan boş bırakılamaz!";
                return View();
            }


            else
            {
                var comment = new Comment()
                {
                    UserId = Session["UserId"] as string,
                    ProductId = Convert.ToInt32(Session["id"]),
                    UserComment = UserComment       
                };
                var productId = comment.ProductId;
                _context.Comments.Add(comment);
                _context.SaveChanges();
                return RedirectToAction("Details", "Home", new { id = productId });

            }
            return View();
        }

        public ActionResult DeleteComment(int commentId)
        {
            var comment = _context.Comments.FirstOrDefault(i => i.Id == commentId);
            var productId = comment.ProductId;
            _context.Comments.Remove(comment);
            _context.SaveChanges();

          

            return RedirectToAction("Details","Home", new { id = productId });
        }



    }
}