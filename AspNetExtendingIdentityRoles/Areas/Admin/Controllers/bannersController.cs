using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PageWebMic.Areas.Admin.Models;
using System.IO;
using System.Configuration;

namespace PageWebMic.Areas.Admin.Controllers
{
    [Authorize(Roles = "banners")]
    public class bannersController : AuthorizeBaseController
    {
        private paginaMICEntities2 db = new paginaMICEntities2();
        private string _StorageRoot;
        private string StorageRoot
        {
            get { return _StorageRoot; }
        }
        public bannersController()
        {
            _StorageRoot = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), ConfigurationManager.AppSettings["DIR_FILE_UPLOADS"]);
        }
        // GET: Admin/banners
        [Authorize(Roles = "banners")]
        public ActionResult Index()
        {
            return View(db.banners.ToList());
        }

        // GET: Admin/banners/Details/5
        [Authorize(Roles = "banners")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            banners banners = db.banners.Find(id);
            if (banners == null)
            {
                return HttpNotFound();
            }
            return View(banners);
        }

        // GET: Admin/banners/Create
        [Authorize(Roles = "banners")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/banners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "banners")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,url,image_url,active")] banners banners)
        {
            if (ModelState.IsValid)
            {


                if (Request.Files.Count > 0)
                {
                    var file = Request.Files["image_url"];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileId = Media.Models.IDGen.NewID();
                        var fileName = Path.GetFileName(file.FileName);
                        var directoryPatch = Path.Combine(StorageRoot, "media_banners");
                        var fullPath = Path.Combine(directoryPatch, fileId + "-" + fileName);

                        if (!Directory.Exists(directoryPatch))
                        {
                            Directory.CreateDirectory(directoryPatch);
                        }
                        string url = Url.Action("FindImages", "Files", new { Area = string.Empty, folname = "media_banners", filename = fileId + "-" + fileName });
                        banners.image_url = url;
                        file.SaveAs(fullPath);

                    }
                }


                db.banners.Add(banners);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(banners);
        }

        // GET: Admin/banners/Edit/5
        [Authorize(Roles = "banners")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            banners banners = db.banners.Find(id);
            if (banners == null)
            {
                return HttpNotFound();
            }
            return View(banners);
        }

        // POST: Admin/banners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "banners")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,url,image_url,active")] banners banners)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files["image_url"];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileId = Media.Models.IDGen.NewID();
                        var fileName = Path.GetFileName(file.FileName);
                        var directoryPatch = Path.Combine(StorageRoot, "media_banners");
                        var fullPath = Path.Combine(directoryPatch, fileId + "-" + fileName);

                        if (!Directory.Exists(directoryPatch))
                        {
                            Directory.CreateDirectory(directoryPatch);
                        }
                        string url = Url.Action("FindImages", "Files", new { Area = string.Empty, folname = "media_banners", filename = fileId + "-" + fileName });
                        banners.image_url = url;
                        file.SaveAs(fullPath);

                    }
                }

                db.Entry(banners).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(banners);
        }

        // GET: Admin/banners/Delete/5
        [Authorize(Roles = "banners")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            banners banners = db.banners.Find(id);
            if (banners == null)
            {
                return HttpNotFound();
            }
            return View(banners);
        }

        // POST: Admin/banners/Delete/5
        [Authorize(Roles = "banners")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            banners banners = db.banners.Find(id);
            db.banners.Remove(banners);
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
