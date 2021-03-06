﻿using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dev_1400.Models;
using System.IO;
using PagedList;

namespace Dev_1400
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Posts
        public ActionResult Posts(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;

            }

            ViewBag.CurrentFilter = searchString;

            var postList = from s in db.Posts
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                postList = postList.Where(s => s.Title.Contains(searchString)
                                       || s.BodyText.Contains(searchString)
                                       || s.Comments.Any(c => c.BodyText.Contains(searchString))
                                       || s.Comments.Any(c => c.Author.UserName.Contains(searchString)));
            }
            else
            {
                Console.WriteLine("No results");
            }


            switch (sortOrder)
            {

                case "Date":
                    postList = postList.OrderBy(s => s.CreationDate);
                    break;
                case "date_desc":
                    postList = postList.OrderByDescending(s => s.CreationDate);
                    break;
                default:  // Name ascending 
                    postList = postList.OrderByDescending(s => s.CreationDate);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(postList.ToPagedList(pageNumber, pageSize));
        }


        // GET: Posts/Details/5
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

        // GET: Posts/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Post post, HttpPostedFileBase fileUpload)
        {

            DateTime timeUtc = DateTime.UtcNow;
            TimeZoneInfo kstZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime kstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, kstZone);

            post.CreationDate = kstTime;

            if (ModelState.IsValid)
            {
                // restricting the valid file formats to images only
                if (Post.ImageUploadValidator.IsWebFriendlyImage(fileUpload))
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    fileUpload.SaveAs(Path.Combine(Server.MapPath("~/assets/img/post/"), fileName));
                    post.MediaUrl = "~/assets/img/post/" + fileName;

                }

                //post.CreationDate = new DateTimeOffset(DateTime.Now);
                post.CreationDate = kstTime;

                db.Posts.Add(post); //add the object
                db.SaveChanges(); //creates a sql statement and sends it out
                return RedirectToAction("Posts");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "Admin")]
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
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Post post, HttpPostedFileBase fileUpload)
        {

            //post.UpdateDate = new DateTimeOffset(DateTime.Now);
            DateTime timeUtc = DateTime.UtcNow;
            TimeZoneInfo kstZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime kstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, kstZone);

            post.UpdateDate = kstTime;


            if (ModelState.IsValid)
            {
                var fetched = db.Posts.Find(post.Id);
                fetched.Title = post.Title;
                fetched.BodyText = post.BodyText;
                fetched.MediaUrl = post.MediaUrl;
                fetched.UpdateDate = post.UpdateDate;

                // restricting the valid file formats to images only
                if (Post.ImageUploadValidator.IsWebFriendlyImage(fileUpload))
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    fileUpload.SaveAs(Path.Combine(Server.MapPath("~/assets/img/post"), fileName));
                    fetched.MediaUrl = "~/assets/img/post/" + fileName;

                }

                //post.UpdateDate = new DateTimeOffset(DateTime.Now);
                post.UpdateDate = kstTime;
                db.Entry(fetched).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Posts");
            }


            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Admin")]
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

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {

            Post post = db.Posts.Find(id);
            post.CreationDate = new DateTimeOffset(DateTime.Now);
            post.UpdateDate = new DateTimeOffset(DateTime.Now);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Posts");
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
