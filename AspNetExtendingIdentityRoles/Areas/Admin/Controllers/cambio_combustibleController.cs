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
    public class cambio_combustibleController : Controller
    {
        private paginaMICEntities2 db = new paginaMICEntities2();

        // GET: Admin/cambio_combustible
        public ActionResult Index()
        {
            var cambio_combustible = db.cambio_combustible.Include(c => c.AspNetUsers);
            return View(cambio_combustible.ToList());
        }

        // GET: Admin/cambio_combustible/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cambio_combustible cambio_combustible = db.cambio_combustible.Find(id);
            if (cambio_combustible == null)
            {
                return HttpNotFound();
            }
            return View(cambio_combustible);
        }

        // GET: Admin/cambio_combustible/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "UserName");
            return View();
        }

        // POST: Admin/cambio_combustible/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,user_id,fecha,titulo,tasa_cambio")] cambio_combustible cambio_combustible)
        {
            if (ModelState.IsValid)
            {
                db.cambio_combustible.Add(cambio_combustible);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "UserName", cambio_combustible.user_id);
            return View(cambio_combustible);
        }

        // GET: Admin/cambio_combustible/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cambio_combustible cambio_combustible = db.cambio_combustible.Find(id);
            if (cambio_combustible == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "UserName", cambio_combustible.user_id);
            return View(cambio_combustible);
        }

        // POST: Admin/cambio_combustible/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,user_id,fecha,titulo,tasa_cambio")] cambio_combustible cambio_combustible)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cambio_combustible).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.AspNetUsers, "Id", "UserName", cambio_combustible.user_id);
            return View(cambio_combustible);
        }

        // GET: Admin/cambio_combustible/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cambio_combustible cambio_combustible = db.cambio_combustible.Find(id);
            if (cambio_combustible == null)
            {
                return HttpNotFound();
            }
            return View(cambio_combustible);
        }

        // POST: Admin/cambio_combustible/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cambio_combustible cambio_combustible = db.cambio_combustible.Find(id);
            db.cambio_combustible.Remove(cambio_combustible);
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
