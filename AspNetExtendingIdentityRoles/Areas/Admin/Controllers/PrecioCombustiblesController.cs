using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PageWebMic.Areas.Admin.Models;
using Microsoft.AspNet.Identity;
using PageWebMic.Areas.Media.Models;
using System.IO;
using System.Configuration;

namespace PageWebMic.Areas.Admin.Controllers
{
    public class PrecioCombustiblesController : AuthorizeBaseController
    {
        private paginaMICEntities2 db = new paginaMICEntities2();
        private string _StorageRoot;

        public  PrecioCombustiblesController() {

            _StorageRoot = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), ConfigurationManager.AppSettings["DIR_FILE_UPLOADS"]);
        }
        private string StorageRoot
        {
            get { return _StorageRoot; }
        }
        // GET: Admin/PrecioCombustibles
        [Authorize(Roles = "precio_combustible")]
        public ActionResult Index()
        {
            var cambio_combustible = db.cambio_combustible.OrderBy(c=> c.id).ThenBy(c => c.id).ToList();

            return View(cambio_combustible);
        }

        // GET: Admin/PrecioCombustibles/Details/5
        [Authorize(Roles = "precio_combustible")]
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

        // GET: Admin/PrecioCombustibles/Create
        [Authorize(Roles = "precio_combustible")]
        public ActionResult Create()
        {
              cambio_combustible modelCambioCombustible = new cambio_combustible();
              var combut = db.combustible.ToList();
              List<cambio_combustible_precio> listCombustible = new List<cambio_combustible_precio>();
              foreach (var item in combut)
              {
                  cambio_combustible_precio modeloCCP = new cambio_combustible_precio();
                  modeloCCP.id_combustible = item.id;
                  modeloCCP.combustible = item;
                  listCombustible.Add(modeloCCP);
              }
              modelCambioCombustible.cambio_combustible_precio_lista = listCombustible;
            return View(modelCambioCombustible);
        }

        // POST: Admin/PrecioCombustibles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "precio_combustible")]
        public ActionResult Create( cambio_combustible cambio_combustible)
        {

            cambio_combustible.user_id = User.Identity.GetUserId();
            cambio_combustible.fecha = DateTime.Now;
           if (ModelState.IsValid)
             {

                foreach (var item in cambio_combustible.cambio_combustible_precio_lista)
                {
                    item.cambio_combustible = cambio_combustible;
                    db.cambio_combustible_precio.Add(item);
                }


                if (Request.Files.Count > 0)
                {
                    var file = Request.Files["file_combustible"];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileId = IDGen.NewID();
                        var fileName = Path.GetFileName(file.FileName);
                        var directoryPatch = Path.Combine(StorageRoot, "media_combustible");
                        var fullPath = Path.Combine(directoryPatch, fileId + "-" + fileName);

                        if (!Directory.Exists(directoryPatch))
                        {
                            Directory.CreateDirectory(directoryPatch);
                        }
                        string url = Url.Action("FindImages", "Files", new { Area = string.Empty, folname = "media_combustible", filename = fileId + "-" + fileName });
                     
                        cambio_combustible.file_combustible = url;
                        file.SaveAs(fullPath);

                    }
                     file = Request.Files["file_gas_natural"];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileId = IDGen.NewID();
                        var fileName = Path.GetFileName(file.FileName);
                        var directoryPatch = Path.Combine(StorageRoot, "media_combustible");
                        var fullPath = Path.Combine(directoryPatch, fileId + "-" + fileName);

                        if (!Directory.Exists(directoryPatch))
                        {
                            Directory.CreateDirectory(directoryPatch);
                        }
                        string url = Url.Action("FindImages", "Files", new { Area = string.Empty, folname = "media_combustible", filename = fileId + "-" + fileName });

                        cambio_combustible.file_gas_natural = url;
                        file.SaveAs(fullPath);

                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
             }

            return View(cambio_combustible);
        }

        // GET: Admin/PrecioCombustibles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cambio_combustible cambio_combustible_precio = db.cambio_combustible.Find(id);
            cambio_combustible_precio.cambio_combustible_precio_lista = cambio_combustible_precio.cambio_combustible_precio.ToList();
            if (cambio_combustible_precio == null)
            {
                return HttpNotFound();
            }
            return View(cambio_combustible_precio);
        }

        // POST: Admin/PrecioCombustibles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(cambio_combustible cambio_combustible)
        {
            cambio_combustible.user_id = User.Identity.GetUserId();
            cambio_combustible.fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
              
                foreach (var item in cambio_combustible.cambio_combustible_precio_lista)
                {
                    item.cambio_combustible = cambio_combustible;
                    db.cambio_combustible_precio.Add(item);
                    db.Entry(item).State = EntityState.Modified;
                }

                if (Request.Files.Count > 0)
                {
                    var file = Request.Files["file_combustible"];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileId = IDGen.NewID();
                        var fileName = Path.GetFileName(file.FileName);
                        var directoryPatch = Path.Combine(StorageRoot, "media_combustible");
                        string fileNameUrl = fileId + "-" + fileName;
                        var fullPath = Path.Combine(directoryPatch, fileNameUrl);


                        if (!Directory.Exists(directoryPatch))
                        {
                            Directory.CreateDirectory(directoryPatch);
                        }
                        string url = Url.Action("buscarCombustible", "Files", new { filename = fileNameUrl });
                        cambio_combustible.file_combustible = url;
                        file.SaveAs(fullPath);

                    }
                }

                db.Entry(cambio_combustible).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
            db.media_combustible.RemoveRange(cambio_combustible.media_combustible);
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
