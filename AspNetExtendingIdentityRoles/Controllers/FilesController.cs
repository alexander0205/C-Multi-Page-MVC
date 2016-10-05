using PageWebMic.Areas.Admin.Models;
using PageWebMic.Areas.Media.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PageWebMic.Controllers
{
    public class FilesController : Controller
    {
        private paginaMICEntities2 db = new paginaMICEntities2();
        private string _StorageRoot;
        private string StorageRoot
        {
            get { return _StorageRoot; }
        }

        public string generateUrlFind(string id,string fileId, string fileNameEncoded) {
            return  Url.Action("find", "Files", new { id = id, filename = fileId + "-" + fileNameEncoded });
        }
        public string generateUrlDelete(string id, string fileId, string fileNameEncoded)
        {
            return Url.Action("Delete", "Files", new { id = id, filename =  fileNameEncoded });
        }


        public FilesController()
        {
            _StorageRoot = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), ConfigurationManager.AppSettings["DIR_FILE_UPLOADS"]);
        }

        [HttpGet]
        public ActionResult ListJson(int id)
        {

            var fileData = new List<ViewDataUploadFilesResult>();
            var StorageRootFile = Path.Combine(StorageRoot, id.ToString());
            DirectoryInfo dir = new DirectoryInfo(StorageRootFile);
            if (dir.Exists)
            {
                string[] extensions = MimeTypes.ImageMimeTypes.Keys.ToArray();

                FileInfo[] files = dir.EnumerateFiles()
                         .Where(f => extensions.Contains(f.Extension.ToLower()))
                         .ToArray();

                if (files.Length > 0)
                {
                    foreach (FileInfo file in files)
                    {
                        var fileId = file.Name.Substring(0, 20);
                        var fileNameEncoded = HttpUtility.HtmlEncode(file.Name.Substring(21));
                        var relativePath = generateUrlFind(id.ToString(), fileId, fileNameEncoded);

                        fileData.Add(new ViewDataUploadFilesResult()
                        {
                            image = relativePath,
                            thumb = relativePath, //@"data:image/png;base64," + EncodeFile(fullName),
                            folder = id.ToString(),
                            name = fileNameEncoded,
                            type = MimeTypes.ImageMimeTypes[file.Extension],
                            size = Convert.ToInt32(file.Length),
                            delete_url = generateUrlDelete(id.ToString(), fileId, fileNameEncoded),
                        });
                    }
                }
            }

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;

            var result = new ContentResult
            {
                Content =  serializer.Serialize(fileData) ,
            };
            return result;
        }


        [HttpGet]
        public ActionResult List(int id,string type = null )
        {

            var fileData = new List<ViewDataUploadFilesResult>();
            var StorageRootFile = Path.Combine(StorageRoot, id.ToString());
            DirectoryInfo dir = new DirectoryInfo(StorageRootFile);
            if (dir.Exists)
            {
                string[] extensions = MimeTypes.ImageMimeTypes.Keys.ToArray();

                FileInfo[] files = dir.EnumerateFiles()
                         .Where(f => extensions.Contains(f.Extension.ToLower()))
                         .ToArray();
                var dbMedia = db.media.Where(x => x.articulo_contenido_id == id).ToList();

                if (type != null)
                {
                     dbMedia = db.media.Where(x => x.articulo_contenido_id == id).Where(x => x.type_name == type).ToList();
                }
                

                if (dbMedia.Count > 0)
                {
                    foreach (var file in dbMedia)
                    {
                        var fileNameEncoded = file.delete_url;
                        var relativePath = file.url;

                        fileData.Add(new ViewDataUploadFilesResult()
                        {
                            id = file.id,
                            url = relativePath,
                            thumb = file.thumb,
                            folder = "",
                            image = relativePath,
                            thumbnail_url = file.thumb, //@"data:image/png;base64," + EncodeFile(fullName),
                            name = file.name,
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

        [HttpGet]
        public ActionResult FindImages(string folname, string filename)
        {
            if (folname == null || filename == null)
            {
                return HttpNotFound();
            }
            try
            {
                var filePath = Path.Combine(_StorageRoot, folname, filename);
                //FileStreamResult result = new FileStreamResult(new System.IO.FileStream(filePath, System.IO.FileMode.Open), GetMimeType(filePath));
                //  result.FileDownloadName = filename;
                return base.File(filePath, GetMimeType(filePath));
            }
            catch (Exception)
            {
                var filePath = Path.Combine(_StorageRoot,"delete.png");
                //FileStreamResult result = new FileStreamResult(new System.IO.FileStream(filePath, System.IO.FileMode.Open), GetMimeType(filePath));
               // result.FileDownloadName = filename;
                return base.File(filePath, GetMimeType(filePath));
            }
           

           
        }


        public ActionResult Find(string id, string filename)
        {
            if (id == null || filename == null)
            {
                return HttpNotFound();
            }
            try
            {
                var filePath = Path.Combine(_StorageRoot, id + "/" + filename);

                FileStreamResult result = new FileStreamResult(new System.IO.FileStream(filePath, System.IO.FileMode.Open), GetMimeType(filePath));
                result.FileDownloadName = filename;

                return result;
            }
            catch (Exception )
            {

                var filePath = Path.Combine(_StorageRoot, "delete.png");

                FileStreamResult result = new FileStreamResult(new System.IO.FileStream(filePath, System.IO.FileMode.Open), GetMimeType(filePath));
                result.FileDownloadName = filename;

                return result;
            }

           
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Uploads(int ID)
        {
            var fileData = new List<ViewDataUploadFilesResult>();

            foreach (string file in Request.Files)
            {
                UploadWholeFile(Request, fileData, ID);
            }

            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;

            var result = new ContentResult
            {
                Content = "{\"files\":" + serializer.Serialize(fileData) + "}",
            };
            return result;
        }

        private void UploadWholeFile(HttpRequestBase request, List<ViewDataUploadFilesResult> statuses,int id)
        {
            for (int i = 0; i < request.Files.Count; i++)
            {
                HttpPostedFileBase file = request.Files[i];

                var fileId = IDGen.NewID();
                var fileName = Path.GetFileName(file.FileName);
                var fileNameEncoded = HttpUtility.HtmlEncode(fileName);
                var directoryPatch = Path.Combine(StorageRoot,id.ToString());
                string thumb;
               
                var fullPath = Path.Combine(directoryPatch, fileId + "-" + fileName);

                if (!Directory.Exists(directoryPatch))
                {
                    Directory.CreateDirectory(directoryPatch);
                }
                file.SaveAs(fullPath);
                try
                {
                    string url = generateUrlFind(id.ToString(), fileId, fileNameEncoded);
                    thumb = url;
                    string typo = "image";
                    if (file.ContentType == "application/pdf")
                    {
                        typo = "file";
                        thumb = "/Content/images/iconPdf.png";
                    } else if (file.ContentType == "application/docx" || file.ContentType == "application/msword" || file.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                    {
                        typo = "document";
                        thumb = "/Content/images/doxc.png";
                    }
                    file.SaveAs(fullPath);
                   
                  
                    media mediaModel = new media();
                    mediaModel.articulo_contenido_id = id;
                    mediaModel.name = fileNameEncoded;
                    mediaModel.url = url;
                    mediaModel.type_name = typo;
                    mediaModel.type = file.ContentType;
                    mediaModel.isServer = true;
                    mediaModel.size = file.ContentLength;
                    mediaModel.delete_url = generateUrlDelete(id.ToString(), fileId, fileNameEncoded);
                    mediaModel.thumb = thumb;
                    db.media.Add(mediaModel);
                    db.SaveChanges();

                    statuses.Add(new ViewDataUploadFilesResult()
                    {
                        url = url,
                        thumbnail_url = url, //@"data:image/png;base64," + EncodeFile(fullName),
                        name = fileNameEncoded,
                        type = file.ContentType,
                        size = file.ContentLength,
                        delete_url = generateUrlDelete(id.ToString(), fileId, fileNameEncoded),
                        delete_type = "DELETE"
                    });
                }
                catch (Exception ex)
                {
                  
                    throw ex;
                }
               
            }
        }

        [AcceptVerbs(HttpVerbs.Delete)]
        public ActionResult Delete(int id, string filename)
        {

            if (id == 0 || filename == null)
            {
                return HttpNotFound();
            }

          
            var employer = db.media.Where(x => x.name == filename && x.articulo_contenido_id == id).First();
            var filePath = Path.Combine(_StorageRoot, id.ToString(), employer.name+"-"+filename);
            db.media.Attach(employer);
            db.media.Remove(employer);
            db.SaveChanges();
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            return RedirectToAction("Index", "Home");
        }

        private string EncodeFile(string fileName)
        {
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName));
        }

        private string GetMimeType(string filePath)
        {
            return GetMimeType(new FileInfo(filePath));
        }
        private string GetMimeType(FileInfo file)
        {
            return MimeTypes.ImageMimeTypes[file.Extension];
        }
    }
}