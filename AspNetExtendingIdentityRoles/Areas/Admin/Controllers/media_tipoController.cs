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
    public class media_tipoController : AuthorizeBaseController
    {
        private paginaMICEntities2 db = new paginaMICEntities2();

        // GET: Admin/media_tipo
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.media_tipo.ToList());
        }

        // GET: Admin/media_tipo/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            media_tipo media_tipo = db.media_tipo.Find(id);
            if (media_tipo == null)
            {
                return HttpNotFound();
            }
            return View(media_tipo);
        }

        // GET: Admin/media_tipo/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/media_tipo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "id,tipo")] media_tipo media_tipo)
        {
            if (ModelState.IsValid)
            {
                db.media_tipo.Add(media_tipo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(media_tipo);
        }

        // GET: Admin/media_tipo/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            media_tipo media_tipo = db.media_tipo.Find(id);
            if (media_tipo == null)
            {
                return HttpNotFound();
            }
            return View(media_tipo);
        }

        // POST: Admin/media_tipo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "id,tipo")] media_tipo media_tipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(media_tipo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(media_tipo);
        }

        // GET: Admin/media_tipo/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            media_tipo media_tipo = db.media_tipo.Find(id);
            if (media_tipo == null)
            {
                return HttpNotFound();
            }
            return View(media_tipo);
        }

        // POST: Admin/media_tipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            media_tipo media_tipo = db.media_tipo.Find(id);
            db.media_tipo.Remove(media_tipo);
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
