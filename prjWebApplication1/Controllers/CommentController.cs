using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.ViewModels;
using System.Collections.Generic;
using System.Linq;
using PJ_MSIT143_team02.Models;

namespace PJ_MSIT143_team02.Controllers
{
    public class CommentController : Controller
    {
        public IActionResult List()
        {
            var data = queryAll();
            return View(data);
        }
        [HttpPost]
        public IActionResult List(CommentViewModel model)
        {
            IEnumerable<Comment> data;
            if (string.IsNullOrEmpty(model.txtKey))
                data = queryAll();
            else
                data = from c in (new MingSuContext()).Comments
                       where (c.CommentId.ToString().Contains(model.txtKey) ||
                       c.CommentDetail.Contains(model.txtKey) ||
                       c.CommentPoint.ToString().Contains(model.txtKey) ||
                       c.RoomId.ToString().Contains(model.txtKey))
                       select c;
            return View(data);
        }

        public IActionResult Delete(int? Id)
        {
            MingSuContext db = new MingSuContext();
            Comment c = db.Comments.FirstOrDefault(c => c.CommentId == Id);
            if (c != null)
            {
                db.Comments.Remove(c);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public IActionResult Edit(int? Id)
        {
            if (Id != null)
            {
                MingSuContext db = new MingSuContext();
                Comment c = db.Comments.FirstOrDefault(c => c.CommentId == Id);
                if (c != null)
                    return View(c);
            }
            return RedirectPermanent("List");
        }
        [HttpPost]
        public IActionResult Edit(CommentViewModel input)
        {
            MingSuContext db = new MingSuContext();
            Comment c = db.Comments.FirstOrDefault(d => d.CommentId == input.Id);
            if (c != null)
            {
                c.CommentId = input.Id; 
                c.RoomId = input.RoomId;
                c.CommentDetail = input.CommentDetail;
                c.CommentPoint = input.CommentPoint;
                c.CommentStatus = input.CommentStatus;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        public IEnumerable<Comment> queryAll()
        {
            var data = from d in (new MingSuContext()).Comments
                       select d;
            return data;
        }
    }
}
