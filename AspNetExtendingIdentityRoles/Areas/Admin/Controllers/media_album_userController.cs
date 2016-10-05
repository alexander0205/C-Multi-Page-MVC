using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PageWebMic.Areas.Admin.Models;

namespace PageWebMic.Areas.Admin.Controllers
{
    public class media_album_userController : AuthorizeBaseController
    {
        private paginaMICEntities2 db = new paginaMICEntities2();

        // GET: Admin/media_album_user
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var media_album_user = db.media_album_user.Include(m => m.AspNetUsers).Include(m => m.media_album);
            return View(media_album_user.ToList());
        }

        // GET: Admin/media_album_user/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            media_album_user media_album_user = db.media_album_user.Find(id);
            if (media_album_user == null)
            {
                return HttpNotFound();
            }
            return View(media_album_user);
        }

        // GET: Admin/media_album_user/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.usuario_id = new SelectList(db.AspNetUsers, "Id", "UserName");
            ViewBag.media_album_id = new SelectList(db.media_album, "id", "id_titulo");
            return View();
        }

        // POST: Admin/media_album_user/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "id,media_album_id,usuario_id")] media_album_user media_album_user)
        {
            if (ModelState.IsValid)
            {
                db.media_album_user.Add(media_album_user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.usuario_id = new SelectList(db.AspNetUsers, "Id", "UserName", media_album_user.usuario_id);
            ViewBag.media_album_id = new SelectList(db.media_album, "id", "id_titulo", media_album_user.media_album_id);
            return View(media_album_user);
        }

        // GET: Admin/media_album_user/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            media_album_user media_album_user = db.media_album_user.Find(id);
            if (media_album_user == null)
            {
                return HttpNotFound();
            }
            ViewBag.usuario_id = new SelectList(db.AspNetUsers, "Id", "UserName", media_album_user.usuario_id);
            ViewBag.media_album_id = new SelectList(db.media_album, "id", "id_titulo", media_album_user.media_album_id);
            return View(media_album_user);
        }

        // POST: Admin/media_album_user/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "id,media_album_id,usuario_id")] media_album_user media_album_user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(media_album_user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.usuario_id = new SelectList(db.AspNetUsers, "Id", "UserName", media_album_user.usuario_id);
            ViewBag.media_album_id = new SelectList(db.media_album, "id", "id_titulo", media_album_user.media_album_id);
            return View(media_album_user);
        }

        // GET: Admin/media_album_user/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            media_album_user media_album_user = db.media_album_user.Find(id);
            if (media_album_user == null)
            {
                return HttpNotFound();
            }
            return View(media_album_user);
        }

        // POST: Admin/media_album_user/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            media_album_user media_album_user = db.media_album_user.Find(id);
            db.media_album_user.Remove(media_album_user);
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
