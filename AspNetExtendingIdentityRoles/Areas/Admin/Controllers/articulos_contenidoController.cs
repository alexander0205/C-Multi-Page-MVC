using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PageWebMic.Areas.Admin.Models;
using System.Web.Script.Serialization;

namespace PageWebMic.Areas.Admin.Controllers
{
    public class articulos_contenidoController : AuthorizeBaseController
    {
        private paginaMICEntities2 db = new paginaMICEntities2();
        [Authorize(Roles = "Admin")]
        public ActionResult Admin(int? id, int? padrePrincipalId)
        {
            var getUr = getUrls(id);
            ViewBag.menuContenido = new JavaScriptSerializer().Serialize(getUr);

            return View();
        }

        // GET: Admin/articulos_contenido
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? id, int? padrePrincipalId)
        {
            var getUr = getUrls(id);
            ViewBag.menuContenido = new JavaScriptSerializer().Serialize(getUr);

            if (padrePrincipalId != null) {
                ViewData["padrePrincipalId"] = padrePrincipalId;
            }
            else
            {
                ViewData["padrePrincipalId"] = id;
            }
            ViewBag.idParents = id;
            if (id == 0 || id == null) {
                ViewBag.modelIdValidate = false;
                return View();
            }
            else {
                ViewBag.modelIdValidate = true;
                var modelo = db.articulos_contenido.Find(id);
                ViewBag.modelo = modelo;
                return View(modelo);
            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult LoadAjax(int? id, int? padrePrincipalId) {
            ViewBag.menuContenido = getUrls(id);
            ViewBag.idParents = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            articulos_contenido articulos_contenido = db.articulos_contenido.Find(id);
            if (articulos_contenido == null)
            {
                return HttpNotFound();
            }
            return View(articulos_contenido);
        }

        // GET: Admin/articulos_contenido/Details/5
        public ActionResult Details(int? id, int? padrePrincipalId)
        {
            ViewBag.menuContenido = getUrls(id);
            ViewBag.idParents = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            articulos_contenido articulos_contenido = db.articulos_contenido.Find(id);
            if (articulos_contenido == null)
            {
                return HttpNotFound();
            }
            return View(articulos_contenido);
        }

        // GET: Admin/articulos_contenido/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create(int? id, int? padrePrincipalId)
        {
            if (padrePrincipalId != null)
            {
                ViewData["padrePrincipalId"] = padrePrincipalId;
                ViewBag.selectFiles = new JavaScriptSerializer().Serialize(db.articulos_contenido.Find((int)ViewData["padrePrincipalId"]).articulos_contenido_media.Select(x => (int)ViewData["padrePrincipalId"]));

            }
            else if (id != null)
            {
                ViewData["padrePrincipalId"] = id;

                ViewBag.selectFiles = new JavaScriptSerializer().Serialize(db.articulos_contenido.Find((int)ViewData["padrePrincipalId"]).articulos_contenido_media.Select(x => (int)ViewData["padrePrincipalId"]));

            }
            else {
                ViewData["padrePrincipalId"] = 0;
            }
          //  ViewBag.menuContenido = getUrlsTable(id);
            ViewBag.selectFilesUrl = Url.Action("List", "files", new { area = string.Empty, id = ViewData["padrePrincipalId"] });
            if (id == null)
            {
                ViewBag.idParents = null;
            }
            else
            {
                ViewBag.idParents = id;
            }
            return View();
        }

        // POST: Admin/articulos_contenido/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(articulos_contenido articulos_contenido, List<int> image, List<int> pdf, List<int> video, int? padrePrincipalId)
        {
            ViewBag.selectFiles = null;
            if (padrePrincipalId != null)
            {
                ViewData["padrePrincipalId"] = padrePrincipalId;
                ViewBag.selectFiles = new JavaScriptSerializer().Serialize(db.articulos_contenido.Find((int)ViewData["padrePrincipalId"]).articulos_contenido_media.Select(x => (int)ViewData["padrePrincipalId"]));

            }
            else
            {
                ViewData["padrePrincipalId"] = 0;
            }
            ViewBag.selectFilesUrl = Url.Action("List", "files", new { area = string.Empty, id = (int)ViewData["padrePrincipalId"] });

            ViewBag.menuContenido = getUrls(articulos_contenido.parent_id);
            if (articulos_contenido.parent_id == 0)
                articulos_contenido.parent_id = null;
            if (ModelState.IsValid)
            {
                List<int> allValues = new List<int>();

                if (image != null)
                    allValues = allValues.Union(image).ToList();
                if (pdf != null)
                    allValues = allValues.Union(pdf).ToList();
                if (video != null)
                    allValues = allValues.Union(video).ToList();
                foreach (int item in allValues)
                {
                    articulos_contenido_media articulosContenidoMedia = new articulos_contenido_media();
                    articulosContenidoMedia.media_id = item;
                    articulos_contenido.articulos_contenido_media.Add(articulosContenidoMedia);
                }
         
                db.articulos_contenido.Add(articulos_contenido);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = articulos_contenido.id });
            }

            return View(articulos_contenido);
        }

        // GET: Admin/articulos_contenido/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id, int? padrePrincipalId)
        {
            if (padrePrincipalId != null)
            {
                ViewData["padrePrincipalId"] = padrePrincipalId;
            }
            else
            {
                ViewData["padrePrincipalId"] = id;
            }
            ViewBag.selectFilesUrl = Url.Action("List", "files", new { area = string.Empty, id = (int)ViewData["padrePrincipalId"] });

            ViewBag.menuContenido = getUrls(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            articulos_contenido articulos_contenido = db.articulos_contenido.Find(id);
            ViewBag.selectFiles = new JavaScriptSerializer().Serialize(articulos_contenido.articulos_contenido_media.Select(x => x.media_id));
            if (articulos_contenido == null)
            {
                return HttpNotFound();
            }
            return View(articulos_contenido);
        }

        List<articulos_contenido_media> articulosConteniMediaLista = new List<articulos_contenido_media>();
        // POST: Admin/articulos_contenido/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(articulos_contenido articulos_contenido, List<int> image, List<int> pdf, List<int> video, int? padrePrincipalId)
        {
           
            if (padrePrincipalId != null)
            {
                ViewData["padrePrincipalId"] = padrePrincipalId;
            }
            else
            {
                ViewData["padrePrincipalId"] = articulos_contenido.id;
            }
            ViewBag.selectFilesUrl = Url.Action("List", "files", new { area = string.Empty, id = (int)ViewData["padrePrincipalId"] });
            ViewBag.menuContenido = getUrls(articulos_contenido.parent_id);
     
            if (articulos_contenido.parent_id == 0)
                articulos_contenido.parent_id = null;
            ViewBag.selectFiles = new JavaScriptSerializer().Serialize(articulos_contenido.articulos_contenido_media.Select(x => x.media_id));

            if (ModelState.IsValid)
            {
                db.articulos_contenido_media.RemoveRange(db.articulos_contenido_media.Where(x=>x.articulos_contenido_id== articulos_contenido.id));

                List<int> allValues = new List<int>();

                if (image != null)
                    allValues = allValues.Union(image).ToList();
                if (pdf != null)
                    allValues = allValues.Union(pdf).ToList();
                if (video != null)
                    allValues = allValues.Union(video).ToList();

                articulos_contenido = crearArtiMedia(allValues, articulos_contenido);

           
               
                db.Entry(articulos_contenido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = articulos_contenido.id });
            }
            return View(articulos_contenido);
        }

        articulos_contenido crearArtiMedia(List<int> list, articulos_contenido articulos_contenido) {
            foreach (int item in list)
            {
                articulos_contenido_media articulosContenidoMedia = new articulos_contenido_media();
                articulosContenidoMedia.articulos_contenido_id = articulos_contenido.id;
                articulosContenidoMedia.media_id = item;
                db.articulos_contenido_media.Add(articulosContenidoMedia);
            }
            return articulos_contenido;
        }
        // GET: Admin/articulos_contenido/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            articulos_contenido articulos_contenido = db.articulos_contenido.Find(id);
            if (articulos_contenido == null)
            {
                return HttpNotFound();
            }
            return View(articulos_contenido);
        }

        // POST: Admin/articulos_contenido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            articulos_contenido articulos_contenido = db.articulos_contenido.Find(id);
            db.articulos_contenido.Remove(articulos_contenido);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       /* public  List<menuArticulos> getUrls()
        {
            List<menuArticulos> menuAr = new List<menuArticulos>();
            foreach (var item in db.articulos_contenido)
            {

            }

            return menuAr;

        }*/

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }

    class menuArticulos {
        string text { get; set; }
        string selectable { get; set; }
        string selectedIcon { get; set; }
        string href { get; set; }
    }
}
