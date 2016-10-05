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
    [Authorize(Roles = "Direcciones")]
    public class DireccionesController : AuthorizeBaseController
    {





        // GET: Admin/Direcciones

        #region Hidrocarburos
        [Authorize(Roles = "Hidrocarburos")]
        public ActionResult Hidrocarburos(int? id, int? padrePrincipalId)
        {
            return View("Hidrocarburos", id, padrePrincipalId);
        }

        [Authorize(Roles = "Hidrocarburos")]
        public ActionResult HidrocarburosCreate(int? id)
        {
            findArticulo("Hidrocarburos", id);
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "Hidrocarburos")]
        public ActionResult HidrocarburosCreate(int? id, articulos_contenido articulos_contenido, List<int> image, List<int> pdf, List<int> video)
        {
            redirectAndActionValidate classRedirect = DBCreate("Hidrocarburos", id, articulos_contenido, image, pdf, video);
            if (classRedirect.validateRedirect)
            {
                return classRedirect.redirect;
            }
            return classRedirect.view;

        }

        [Authorize(Roles = "Hidrocarburos")]
        public ActionResult HidrocarburosEdit(int? id)
        {
            redirectAndActionValidate rediClass = DBeditIndex("Hidrocarburos", id);
            if (rediClass.validatehttpStatus)
            {
                return rediClass.httpStatus;
            }

            if (rediClass.validateHttpNoresults)
            {
                return rediClass.httpNoResults;
            }

            return rediClass.view;

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "Hidrocarburos")]
        public ActionResult HidrocarburosEdit(articulos_contenido articulos_conteni, List<int> image, List<int> pdf, List<int> video)
        {
            redirectAndActionValidate classRedirect = DBEdit("Hidrocarburos", articulos_conteni, image, pdf, video);
            if (classRedirect.validateRedirect)
            {
                return classRedirect.redirect;
            }
            return classRedirect.view;

        }

        [HttpPost]
        [Authorize(Roles = "HidrocarburosDelete")]
        public JsonResult HidrocarburosDelete(int? id, int? idd) {
            return DBDelete(id);
        }

        #endregion

        #region Comunicaciones
        [Authorize(Roles = "Comunicaciones")]
        public ActionResult Comunicaciones(int? id, int? padrePrincipalId)
        {
            return View("Comunicaciones", id, padrePrincipalId);
        }

        [Authorize(Roles = "Comunicaciones")]
        public ActionResult ComunicacionesCreate(int? id)
        {
            findArticulo("Comunicaciones", id);
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "Comunicaciones")]
        public ActionResult ComunicacionesCreate(int? id, articulos_contenido articulos_contenido, List<int> image, List<int> pdf, List<int> video)
        {
            redirectAndActionValidate classRedirect = DBCreate("Comunicaciones", id, articulos_contenido, image, pdf, video);
            if (classRedirect.validateRedirect)
            {
                return classRedirect.redirect;
            }
            return classRedirect.view;

        }

        [Authorize(Roles = "Comunicaciones")]
        public ActionResult ComunicacionesEdit(int? id)
        {
            redirectAndActionValidate rediClass = DBeditIndex("Comunicaciones", id);
            if (rediClass.validatehttpStatus)
            {
                return rediClass.httpStatus;
            }

            if (rediClass.validateHttpNoresults)
            {
                return rediClass.httpNoResults;
            }

            return rediClass.view;

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "Comunicaciones")]
        public ActionResult ComunicacionesEdit(articulos_contenido articulos_conteni, List<int> image, List<int> pdf, List<int> video)
        {
            redirectAndActionValidate classRedirect = DBEdit("Comunicaciones", articulos_conteni, image, pdf, video);
            if (classRedirect.validateRedirect)
            {
                return classRedirect.redirect;
            }
            return classRedirect.view;


        }
        [HttpPost]
        [Authorize(Roles = "ComunicacionesDelete")]
        public JsonResult ComunicacionesDelete(int? id, int? idd)
        {
            return DBDelete(id);
        }
        #endregion

        #region ComercioExterior
        [Authorize(Roles = "ComercioExterior")]
        public ActionResult ComercioExterior(int? id, int? padrePrincipalId)
        {
            return View("Comercio-Exterior", id, padrePrincipalId);
        }

        [Authorize(Roles = "ComercioExterior")]
        public ActionResult ComercioExteriorCreate(int? id)
        {
            findArticulo("Comercio-Exterior", id);
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "ComercioExterior")]
        public ActionResult ComercioExteriorCreate(int? id, articulos_contenido articulos_contenido, List<int> image, List<int> pdf, List<int> video)
        {
            redirectAndActionValidate classRedirect = DBCreate("Comercio-Exterior", id, articulos_contenido, image, pdf, video);
            if (classRedirect.validateRedirect)
            {
                return classRedirect.redirect;
            }
            return classRedirect.view;

        }

        [Authorize(Roles = "ComercioExterior")]
        public ActionResult ComercioExteriorEdit(int? id)
        {
            redirectAndActionValidate rediClass = DBeditIndex("Comercio-Exterior", id);
            if (rediClass.validatehttpStatus)
            {
                return rediClass.httpStatus;
            }

            if (rediClass.validateHttpNoresults)
            {
                return rediClass.httpNoResults;
            }

            return rediClass.view;

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "ComercioExterior")]
        public ActionResult ComercioExteriorEdit(articulos_contenido articulos_conteni, List<int> image, List<int> pdf, List<int> video)
        {
            redirectAndActionValidate classRedirect = DBEdit("Comercio-Exterior", articulos_conteni, image, pdf, video);
            if (classRedirect.validateRedirect)
            {
                return classRedirect.redirect;
            }
            return classRedirect.view;


        }

        [HttpPost]
        [Authorize(Roles = "ComercioExteriorDelete")]
        public JsonResult ComercioExteriorDelete(int? id, int? idd)
        {
            return DBDelete(id);
        }

        #endregion

        #region ComercioInterno
        [Authorize(Roles = "ComercioInterno")]
        public ActionResult ComercioInterno(int? id, int? padrePrincipalId)
        {
            return View("Comercio-Interno", id, padrePrincipalId);
        }

        [Authorize(Roles = "ComercioInterno")]
        public ActionResult ComercioInternoCreate(int? id)
        {
            findArticulo("Comercio-Interno", id);
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "ComercioInterno")]
        public ActionResult ComercioInternoCreate(int? id, articulos_contenido articulos_contenido, List<int> image, List<int> pdf, List<int> video)
        {
            redirectAndActionValidate classRedirect = DBCreate("Comercio-Interno", id, articulos_contenido, image, pdf, video);
            if (classRedirect.validateRedirect)
            {
                return classRedirect.redirect;
            }
            return classRedirect.view;

        }

        [Authorize(Roles = "ComercioInterno")]
        public ActionResult ComercioInternoEdit(int? id)
        {
            redirectAndActionValidate rediClass = DBeditIndex("Comercio-Interno", id);
            if (rediClass.validatehttpStatus)
            {
                return rediClass.httpStatus;
            }

            if (rediClass.validateHttpNoresults)
            {
                return rediClass.httpNoResults;
            }

            return rediClass.view;

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "ComercioInterno")]
        public ActionResult ComercioInternoEdit(articulos_contenido articulos_conteni, List<int> image, List<int> pdf, List<int> video)
        {
            redirectAndActionValidate classRedirect = DBEdit("Comercio-Interno", articulos_conteni, image, pdf, video);
            if (classRedirect.validateRedirect)
            {
                return classRedirect.redirect;
            }
            return classRedirect.view;


        }

        [HttpPost]
        [Authorize(Roles = "ComercioInternoDelete")]
        public JsonResult ComercioInternoDelete(int? id, int? idd)
        {
            return DBDelete(id);
        }

        #endregion

        #region AnalisisEconomico
        [Authorize(Roles = "AnalisisEconomico")]
        public ActionResult AnalisisEconomico(int? id, int? padrePrincipalId)
        {
            return View("Analisis-Economico", id, padrePrincipalId);
        }

        [Authorize(Roles = "AnalisisEconomico")]
        public ActionResult AnalisisEconomicoCreate(int? id)
        {
            findArticulo("Analisis-Economico", id);
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "AnalisisEconomico")]
        public ActionResult AnalisisEconomicoCreate(int? id, articulos_contenido articulos_contenido, List<int> image, List<int> pdf, List<int> video)
        {
            redirectAndActionValidate classRedirect = DBCreate("Analisis-Economico", id, articulos_contenido, image, pdf, video);
            if (classRedirect.validateRedirect)
            {
                return classRedirect.redirect;
            }
            return classRedirect.view;

        }

        [Authorize(Roles = "AnalisisEconomico")]
        public ActionResult AnalisisEconomicoEdit(int? id)
        {
            redirectAndActionValidate rediClass = DBeditIndex("Analisis-Economico", id);
            if (rediClass.validatehttpStatus)
            {
                return rediClass.httpStatus;
            }

            if (rediClass.validateHttpNoresults)
            {
                return rediClass.httpNoResults;
            }

            return rediClass.view;

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "AnalisisEconomico")]
        public ActionResult AnalisisEconomicoEdit(articulos_contenido articulos_conteni, List<int> image, List<int> pdf, List<int> video)
        {
            redirectAndActionValidate classRedirect = DBEdit("Analisis-Economico", articulos_conteni, image, pdf, video);
            if (classRedirect.validateRedirect)
            {
                return classRedirect.redirect;
            }
            return classRedirect.view;


        }

        [HttpPost]
        [Authorize(Roles = "AnalisisEconomicoDelete")]
        public JsonResult AnalisisEconomicoDelete(int? id, int? idd)
        {
            return DBDelete(id);
        }

        #endregion

        #region CombustiblesnoConvencionales
        [Authorize(Roles = "CombustiblesnoConvencionales")]
        public ActionResult CombustiblesnoConvencionales(int? id, int? padrePrincipalId)
        {
            return View("Combustibles-no-Convencionales", id, padrePrincipalId);
        }

        [Authorize(Roles = "CombustiblesnoConvencionales")]
        public ActionResult CombustiblesnoConvencionalesCreate(int? id)
        {
            findArticulo("Combustibles-no-Convencionales", id);
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "CombustiblesnoConvencionales")]
        public ActionResult CombustiblesnoConvencionalesCreate(int? id, articulos_contenido articulos_contenido, List<int> image, List<int> pdf, List<int> video)
        {
            redirectAndActionValidate classRedirect = DBCreate("Combustibles-no-Convencionales", id, articulos_contenido, image, pdf, video);
            if (classRedirect.validateRedirect)
            {
                return classRedirect.redirect;
            }
            return classRedirect.view;

        }

        [Authorize(Roles = "CombustiblesnoConvencionales")]
        public ActionResult CombustiblesnoConvencionalesEdit(int? id)
        {
            redirectAndActionValidate rediClass = DBeditIndex("Combustibles-no-Convencionales", id);
            if (rediClass.validatehttpStatus)
            {
                return rediClass.httpStatus;
            }

            if (rediClass.validateHttpNoresults)
            {
                return rediClass.httpNoResults;
            }

            return rediClass.view;

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "CombustiblesnoConvencionales")]
        public ActionResult CombustiblesnoConvencionalesEdit(articulos_contenido articulos_conteni, List<int> image, List<int> pdf, List<int> video)
        {
            redirectAndActionValidate classRedirect = DBEdit("Combustibles-no-Convencionales", articulos_conteni, image, pdf, video);
            if (classRedirect.validateRedirect)
            {
                return classRedirect.redirect;
            }
            return classRedirect.view;


        }

        [HttpPost]
        [Authorize(Roles = "CombustiblesDelete")]
        public JsonResult CombustiblesDelete(int? id, int? idd)
        {
            return DBDelete(id);
        }


        #endregion


    }

    public class redirectAndActionValidate {
        public ViewResult view;
        public RedirectToRouteResult redirect;
        public bool validateRedirect = false;
        public HttpNotFoundResult httpNoResults;
        public bool validateHttpNoresults = false;
        public HttpStatusCodeResult httpStatus;
        public bool validatehttpStatus = false;
    }
}