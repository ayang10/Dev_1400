using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Dev_1400.Models;

namespace Dev_1400
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        public ActionResult Posts()
        {
            var comments = db.Comments.Include(c => c.Author).Include(c => c.Editor).Include(c => c.ParentComment).Include(c => c.Post);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create(int id)
        {
            Comment model = new Comment { PostId = id };

            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PostId,AuthorId,EditorId,BodyText,Created,Updated,ParentCommentId")] Comment comment)
        {
            DateTime timeUtc = DateTime.UtcNow;
            TimeZoneInfo kstZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime kstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, kstZone);

            comment.Created = kstTime;

            if (ModelState.IsValid)
            {
                //comment.Created = new DateTimeOffset(DateTime.Now);
                comment.Created = kstTime;
                comment.AuthorId = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Id; //grab the id of the current login user, assign that to the AuthorId
                
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", "Posts", new { id = comment.PostId }); //redirect this create to Posts view Details page.
            }

            //ViewBag.AuthorId = new SelectList(db.ApplicationUsers, "Id", "Email", comment.AuthorId);
            //ViewBag.EditorId = new SelectList(db.ApplicationUsers, "Id", "Email", comment.EditorId);
            //ViewBag.ParentCommentId = new SelectList(db.Comments, "Id", "AuthorId", comment.ParentCommentId);
            //ViewBag.PostId = new SelectList(db.Posts, "Id", "Title", comment.PostId);

            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.Name != comment.Author.UserName)
            {
                return RedirectToAction("Posts");
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FirstName", comment.AuthorId);
            ViewBag.EditorId = new SelectList(db.Users, "Id", "FirstName", comment.EditorId);
            ViewBag.ParentCommentId = new SelectList(db.Comments, "Id", "AuthorId", comment.ParentCommentId);
            ViewBag.PostId = new SelectList(db.Posts, "Id", "title", comment.PostId);

            return View(comment);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PostId,AuthorId,EditorId,BodyText,Created,Updated,ParentCommentId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Posts");
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "FirstName", comment.AuthorId);
            ViewBag.EditorId = new SelectList(db.Users, "Id", "FirstName", comment.EditorId);
            ViewBag.ParentCommentId = new SelectList(db.Comments, "Id", "AuthorId", comment.ParentCommentId);
            ViewBag.PostId = new SelectList(db.Posts, "Id", "title", comment.PostId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id, int postId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int postId)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Details", "Posts", new { id = postId });
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
