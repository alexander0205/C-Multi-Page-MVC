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
    public class MediaController : AuthorizeBaseController
    {
        private paginaMICEntities2 db = new paginaMICEntities2();

        // GET: Admin/Media
        public ActionResult Index()
        {
            var media_album = db.media_album.Include(m => m.media_tipo);
            return View(media_album.ToList());
        }

        
        public ActionResult Images()
        {
            return View();
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
