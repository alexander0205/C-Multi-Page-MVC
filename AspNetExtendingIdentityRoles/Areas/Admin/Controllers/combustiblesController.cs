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
    [Authorize(Roles = "Admin")]
    public class combustiblesController : AuthorizeBaseController
    {
        private paginaMICEntities2 db = new paginaMICEntities2();

        // GET: Admin/combustibles
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.combustible.ToList());
        }

        // GET: Admin/combustibles/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            combustible combustible = db.combustible.Find(id);
            if (combustible == null)
            {
                return HttpNotFound();
            }
            return View(combustible);
        }

        // GET: Admin/combustibles/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/combustibles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "id,nombre,descripcion,position,activo")] combustible combustible)
        {
            if (ModelState.IsValid)
            {
                db.combustible.Add(combustible);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(combustible);
        }

        // GET: Admin/combustibles/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            combustible combustible = db.combustible.Find(id);
            if (combustible == null)
            {
                return HttpNotFound();
            }
            return View(combustible);
        }

        // POST: Admin/combustibles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "id,nombre,descripcion,position,activo")] combustible combustible)
        {
            if (ModelState.IsValid)
            {
                db.Entry(combustible).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(combustible);
        }

        // GET: Admin/combustibles/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            combustible combustible = db.combustible.Find(id);
            if (combustible == null)
            {
                return HttpNotFound();
            }
            return View(combustible);
        }

        // POST: Admin/combustibles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(string id)
        {
            combustible combustible = db.combustible.Find(id);
            db.combustible.Remove(combustible);
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
