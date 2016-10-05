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
    public class media_albumController : AuthorizeBaseController
    {
        private paginaMICEntities2 db = new paginaMICEntities2();

        // GET: Admin/media_album
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var media_album = db.media_album.Include(m => m.media_tipo);
            return View(media_album.ToList());
        }

        // GET: Admin/media_album/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            media_album media_album = db.media_album.Find(id);
            if (media_album == null)
            {
                return HttpNotFound();
            }
            return View(media_album);
        }

        // GET: Admin/media_album/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.media_tipo_id = new SelectList(db.media_tipo, "id", "tipo");
            return View();
        }

        // POST: Admin/media_album/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "id,id_titulo,titulo,width,height,media_tipo_id")] media_album media_album)
        {
            if (ModelState.IsValid)
            {
                db.media_album.Add(media_album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.media_tipo_id = new SelectList(db.media_tipo, "id", "tipo", media_album.media_tipo_id);
            return View(media_album);
        }

        // GET: Admin/media_album/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            media_album media_album = db.media_album.Find(id);
            if (media_album == null)
            {
                return HttpNotFound();
            }
            ViewBag.media_tipo_id = new SelectList(db.media_tipo, "id", "tipo", media_album.media_tipo_id);
            return View(media_album);
        }

        // POST: Admin/media_album/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "id,id_titulo,titulo,width,height,media_tipo_id")] media_album media_album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(media_album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.media_tipo_id = new SelectList(db.media_tipo, "id", "tipo", media_album.media_tipo_id);
            return View(media_album);
        }

        // GET: Admin/media_album/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            media_album media_album = db.media_album.Find(id);
            if (media_album == null)
            {
                return HttpNotFound();
            }
            return View(media_album);
        }

        // POST: Admin/media_album/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            media_album media_album = db.media_album.Find(id);
            db.media_album.Remove(media_album);
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
