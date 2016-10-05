using PageWebMic.Areas.Admin.Controllers;
using PageWebMic.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PageWebMic.Areas.Media.Controllers
{
    public class HomeController : AuthorizeBaseController
    {
        private paginaMICEntities2 db = new paginaMICEntities2();
        // GET: Media/Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Media/Home

        public ActionResult Contenido(int id)
        {
            ViewBag.id = id;
            return View("index");
        }

        public ActionResult Crear(int id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public ActionResult GuardarUrl(int id, FormCollection  frm) {
            string name = Request.Form["name"];
            string type = Request.Form["type"];
            string url = Request.Form["url"];
            string videoInline = Request.Form["inlineRadioVide"];
            string thumb = url;
            if (type == "video") {
                if (videoInline.IndexOf("youtube") > 0)
                {
                    thumb = String.Format("http://img.youtube.com/vi/{0}/mqdefault.jpg", url);
                }
                else {
                    thumb = String.Format("https://i.vimeocdn.com/video/{0}_340.jpg", url);
                }
                url = videoInline+url;

            }

            var fileName = Path.GetFileName(url);
            media mediaModel = new media();
            mediaModel.articulo_contenido_id = id;
            mediaModel.name = name;
            mediaModel.type_name = type;
            mediaModel.url = url;
            mediaModel.articulo_contenido_id = id;
            mediaModel.type = type;
            mediaModel.isServer = false;
            mediaModel.size = 0;
            mediaModel.thumb = thumb;
            db.media.Add(mediaModel);
            db.SaveChanges();
            bool valuE = false;
            if (1 > 0) {
                valuE = true;
            }

            return Json(valuE);


        }
    }
}