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
using PageWebMic.Areas.Media.Models;
using System.Web.Script.Serialization;

namespace PageWebMic.Areas.Admin.Controllers
{
    public class slideshowsController : AuthorizeBaseController
    {
        private paginaMICEntities2 db = new paginaMICEntities2();
        private string _StorageRoot;

        private string StorageRoot
        {
            get { return _StorageRoot; }
        }
        public slideshowsController()
        {
            _StorageRoot = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), ConfigurationManager.AppSettings["DIR_FILE_UPLOADS"]);
        }
        // GET: Admin/slideshows
        public ActionResult Index()
        {
          
            return View();
        }

        // GET: Admin/slideshows/Details/5
        /* public ActionResult Details(int? id)
         {
             if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             slideshow slideshow = db.slideshow.Find(id);
             if (slideshow == null)
             {
                 return HttpNotFound();
             }
             return View(slideshow);
         }

         // GET: Admin/slideshows/Create
         public ActionResult Create()
         {
             ViewBag.id_articulo = new SelectList(db.articulos_contenido.Where(x=> x.id_title == "Noticias").First().articulos_contenido1.OrderBy(x=>x.fecha), "id", "id_title");
             return View();
         }

         // POST: Admin/slideshows/Create
         // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         [ValidateInput(false)]
         public ActionResult Create([Bind(Include = "id,url_image,text_html,id_articulo,active")] slideshow slideshow)
         {
             if (ModelState.IsValid)
             {
                 if (Request.Files.Count > 0)
                 {
                     var file = Request.Files["url_image"];

                     if (file != null && file.ContentLength > 0)
                     {
                         var fileId = Media.Models.IDGen.NewID();
                         var fileName = Path.GetFileName(file.FileName);
                         var directoryPatch = Path.Combine(StorageRoot, "media_slideshow");
                         var fullPath = Path.Combine(directoryPatch, fileId + "-" + fileName);

                         if (!Directory.Exists(directoryPatch))
                         {
                             Directory.CreateDirectory(directoryPatch);
                         }
                         string url = Url.Action("FindImages", "Files", new { Area = string.Empty, folname = "media_slideshow", filename = fileId + "-" + fileName });
                         slideshow.url_image = url;
                         file.SaveAs(fullPath);

                     }
                 }

                 db.slideshow.Add(slideshow);
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }

             ViewBag.id_articulo = new SelectList(db.articulos_contenido, "id", "id_title", slideshow.id_articulo);
             return View(slideshow);
         }

         // GET: Admin/slideshows/Edit/5
         public ActionResult Edit(int? id)
         {
             if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             slideshow slideshow = db.slideshow.Find(id);
             if (slideshow == null)
             {
                 return HttpNotFound();
             }
             ViewBag.id_articulo = new SelectList(db.articulos_contenido, "id", "id_title", slideshow.id_articulo);
             return View(slideshow);
         }

         // POST: Admin/slideshows/Edit/5
         // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         [ValidateInput(false)]
         public ActionResult Edit([Bind(Include = "id,url_image,text_html,id_articulo,active")] slideshow slideshow)
         {
             if (ModelState.IsValid)
             {
                 var file = Request.Files["url_image"];

                 if (file != null && file.ContentLength > 0)
                 {
                     var fileId = Media.Models.IDGen.NewID();
                     var fileName = Path.GetFileName(file.FileName);
                     var directoryPatch = Path.Combine(StorageRoot, "media_slideshow");
                     var fullPath = Path.Combine(directoryPatch, fileId + "-" + fileName);

                     if (!Directory.Exists(directoryPatch))
                     {
                         Directory.CreateDirectory(directoryPatch);
                     }
                     string url = Url.Action("FindImages", "Files", new { Area = string.Empty, folname = "media_slideshow", filename = fileId + "-" + fileName });
                     slideshow.url_image = url;
                     file.SaveAs(fullPath);

                 }
                 db.Entry(slideshow).State = EntityState.Modified;
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }
             ViewBag.id_articulo = new SelectList(db.articulos_contenido, "id", "id_title", slideshow.id_articulo);
             return View(slideshow);
         }
         */
        [HttpPost]
        public ActionResult GuardarUrl(FormCollection frm)
        {
            string name = Request.Form["name"];
            string type = Request.Form["type"];
            string url = Request.Form["url"];
            string videoInline = Request.Form["inlineRadioVide"];
            string thumb = url;
            if (type == "video")
            {
                if (videoInline.IndexOf("youtube") > 0)
                {
                    thumb = String.Format("http://img.youtube.com/vi/{0}/mqdefault.jpg", url);
                    url = videoInline + url + "?rel=0&amp;controls=0&amp;showinfo=0";
                }
                else {
                    thumb = String.Format("https://i.vimeocdn.com/video/{0}_340.jpg", url);
                    url = videoInline + url + "?color=ffffff&title=0&byline=0&portrait=0";
                }

            }

            var fileName = Path.GetFileName(url);
            slideshow mediaModel = new slideshow();
           // mediaModel.id_articulo = id;
          //  mediaModel.name = name;
            mediaModel.type_name = type;
            mediaModel.url_image = url;
            mediaModel.type = type;
            mediaModel.isServer = false;
            mediaModel.size = 0;
            mediaModel.thumb = thumb;
            db.slideshow.Add(mediaModel);
            db.SaveChanges();
            bool valuE = false;
            if (1 > 0)
            {
                valuE = true;
            }

            return Json(valuE);


        }


        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "slideshow")]
        public ActionResult Uploads()
        {
            var fileData = new List<ViewDataUploadFilesResult>();

            foreach (string file in Request.Files)
            {
                UploadWholeFile(Request, fileData);
            }

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;

            var result = new ContentResult
            {
                Content = "{\"files\":" + serializer.Serialize(fileData) + "}",
            };
            return result;
        }

        private void UploadWholeFile(HttpRequestBase request, List<ViewDataUploadFilesResult> statuses)
        {
            for (int i = 0; i < request.Files.Count; i++)
            {
                HttpPostedFileBase file = request.Files[i];

                var fileId = IDGen.NewID();
                var fileName = Path.GetFileName(file.FileName);
                var fileNameEncoded = HttpUtility.HtmlEncode(fileName);
                var directoryPatch = Path.Combine(StorageRoot, "media_slideshow");
                string thumb;

                var fullPath = Path.Combine(directoryPatch, fileId + "-" + fileName);

                if (!Directory.Exists(directoryPatch))
                {
                    Directory.CreateDirectory(directoryPatch);
                }
                file.SaveAs(fullPath);
                try
                {
                    string url = generateUrlFind(fileId, fileNameEncoded);
                    thumb = url;
                    string typo = "image";
                   
                    file.SaveAs(fullPath);

                    slideshow mediaModel = new slideshow();
                   // mediaModel.articulo_contenido_id = id;
                   // mediaModel.name = fileNameEncoded;
                    mediaModel.url_image = url;
                    mediaModel.type_name = typo;
                    mediaModel.type = file.ContentType;
                    mediaModel.isServer = true;
                    mediaModel.size = file.ContentLength;
                  //  mediaModel.delete_url = generateUrlDelete(id.ToString(), fileId, fileNameEncoded);
                    mediaModel.thumb = thumb;
                    db.slideshow.Add(mediaModel);
                    db.SaveChanges();

                    statuses.Add(new ViewDataUploadFilesResult()
                    {
                        url = url,
                        thumbnail_url = url, //@"data:image/png;base64," + EncodeFile(fullName),
                        name = fileNameEncoded,
                        type = file.ContentType,
                        size = file.ContentLength,
                        delete_url = "delete",
                        delete_type = "DELETE"
                    });
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }

        public string generateUrlFind( string fileId, string fileNameEncoded)
        {
            return Url.Action("FindImages", "Files", new { Area = string.Empty, folname = "media_slideshow", filename = fileId + "-" + fileNameEncoded });
           // return Url.Action("find", "Files", new { id = id, filename = fileId + "-" + fileNameEncoded });
        }
        [HttpGet]
        [Authorize(Roles = "slideshow")]
        public ActionResult List()
        {

            var fileData = new List<ViewDataUploadFilesResult>();
            var StorageRootFile = Path.Combine(StorageRoot);
            DirectoryInfo dir = new DirectoryInfo(StorageRootFile);
            if (dir.Exists)
            {
                string[] extensions = MimeTypes.ImageMimeTypes.Keys.ToArray();

                FileInfo[] files = dir.EnumerateFiles()
                         .Where(f => extensions.Contains(f.Extension.ToLower()))
                         .ToArray();
                var dbMedia = db.slideshow.ToList();

                if (dbMedia.Count > 0)
                {
                    foreach (var file in dbMedia)
                    {
                        var fileNameEncoded = Url.Action("delete", new { id = file.id});
                        var relativePath = file.url_image;

                        fileData.Add(new ViewDataUploadFilesResult()
                        {
                            id = file.id,
                            url = relativePath,
                            thumb = file.thumb,
                            folder = "",
                            image = relativePath,
                            thumbnail_url = file.thumb, //@"data:image/png;base64," + EncodeFile(fullName),
                            name = "",
                            type = file.type,
                            size = Convert.ToInt32(file.size),
                            delete_url = fileNameEncoded,
                            delete_type = "DELETE"
                        });
                    }
                }
            }

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;

            var result = new ContentResult
            {
                Content = serializer.Serialize(fileData),
            };
            return result;
        }

  
        [Authorize(Roles = "slideshow")]
        [HttpPost]
        public JsonResult Delete(int? id)
        {
            slideshow slideshow = db.slideshow.Find(id);
            db.slideshow.Remove(slideshow);
            db.SaveChanges();
            return Json(true);
        }
        // GET: Admin/slideshows/Delete/5
        /*  public ActionResult Delete(int? id)
          {
              if (id == null)
              {
                  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
              }
              slideshow slideshow = db.slideshow.Find(id);
              if (slideshow == null)
              {
                  return HttpNotFound();
              }
              return View(slideshow);
          }
          */
        // POST: Admin/slideshows/Delete/5
       /* [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            slideshow slideshow = db.slideshow.Find(id);
            db.slideshow.Remove(slideshow);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        */
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
