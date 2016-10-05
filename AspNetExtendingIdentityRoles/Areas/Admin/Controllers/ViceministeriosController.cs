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
    [Authorize(Roles = "Viceministerios")]
    public class ViceministeriosController : AuthorizeBaseController
    {





        // GET: Admin/Viceministerios

       

        #region ComercioExterior
        [Authorize(Roles = "VComercioExterior")]
        public ActionResult ComercioExterior(int? id, int? padrePrincipalId)
        {
            return View("Comercio-Exterior", id, padrePrincipalId);
        }

        [Authorize(Roles = "VComercioExterior")]
        public ActionResult ComercioExteriorCreate(int? id)
        {
            findArticulo("Comercio-Exterior", id);
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "VComercioExterior")]
        public ActionResult ComercioExteriorCreate(int? id, articulos_contenido articulos_contenido, List<int> image, List<int> pdf, List<int> video)
        {
            redirectAndActionValidate classRedirect = DBCreate("Comercio-Exterior", id, articulos_contenido, image, pdf, video);
            if (classRedirect.validateRedirect)
            {
                return classRedirect.redirect;
            }
            return classRedirect.view;

        }

        [Authorize(Roles = "VComercioExterior")]
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
        [Authorize(Roles = "VComercioExterior")]
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
        [Authorize(Roles = "VComercioExteriorDelete")]
        public JsonResult ComercioExteriorDelete(int? id, int? idd)
        {
            return DBDelete(id);
        }

        #endregion

        #region ComercioInterno
        [Authorize(Roles = "VComercioInterno")]
        public ActionResult ComercioInterno(int? id, int? padrePrincipalId)
        {
            return View("Comercio-Interno", id, padrePrincipalId);
        }

        [Authorize(Roles = "VComercioInterno")]
        public ActionResult ComercioInternoCreate(int? id)
        {
            findArticulo("Comercio-Interno", id);
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "VComercioInterno")]
        public ActionResult ComercioInternoCreate(int? id, articulos_contenido articulos_contenido, List<int> image, List<int> pdf, List<int> video)
        {
            redirectAndActionValidate classRedirect = DBCreate("Comercio-Interno", id, articulos_contenido, image, pdf, video);
            if (classRedirect.validateRedirect)
            {
                return classRedirect.redirect;
            }
            return classRedirect.view;

        }

        [Authorize(Roles = "VComercioInterno")]
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
        [Authorize(Roles = "VComercioInterno")]
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
        [Authorize(Roles = "VComercioInternoDelete")]
        public JsonResult ComercioInternoDelete(int? id, int? idd)
        {
            return DBDelete(id);
        }

        #endregion

        #region DesarrolloIndustrial
        [Authorize(Roles = "VDesarrolloIndustrial")]
        public ActionResult DesarrolloIndustrial(int? id, int? padrePrincipalId)
        {
            return View("Desarrollo-Industrial", id, padrePrincipalId);
        }

        [Authorize(Roles = "VDesarrolloIndustrial")]
        public ActionResult DesarrolloIndustrialCreate(int? id)
        {
            findArticulo("Desarrollo-Industrial", id);
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "VDesarrolloIndustrial")]
        public ActionResult DesarrolloIndustrialCreate(int? id, articulos_contenido articulos_contenido, List<int> image, List<int> pdf, List<int> video)
        {
            redirectAndActionValidate classRedirect = DBCreate("Desarrollo-Industrial", id, articulos_contenido, image, pdf, video);
            if (classRedirect.validateRedirect)
            {
                return classRedirect.redirect;
            }
            return classRedirect.view;

        }

        [Authorize(Roles = "VDesarrolloIndustrial")]
        public ActionResult DesarrolloIndustrialEdit(int? id)
        {
            redirectAndActionValidate rediClass = DBeditIndex("Desarrollo-Industrial", id);
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
        [Authorize(Roles = "VDesarrolloIndustrial")]
        public ActionResult DesarrolloIndustrialEdit(articulos_contenido articulos_conteni, List<int> image, List<int> pdf, List<int> video)
        {
            redirectAndActionValidate classRedirect = DBEdit("Desarrollo-Industrial", articulos_conteni, image, pdf, video);
            if (classRedirect.validateRedirect)
            {
                return classRedirect.redirect;
            }
            return classRedirect.view;


        }

        [HttpPost]
        [Authorize(Roles = "VDesarrolloIndustrialDelete")]
        public JsonResult DesarrolloIndustrialDelete(int? id, int? idd)
        {
            return DBDelete(id);
        }

        #endregion

        #region FomentoalaPYMES
        [Authorize(Roles = "VFomentoalaPYMES")]
        public ActionResult FomentoalaPYMES(int? id, int? padrePrincipalId)
        {
            return View("Fomento-a-la-PYMES", id, padrePrincipalId);
        }

        [Authorize(Roles = "VFomentoalaPYMES")]
        public ActionResult FomentoalaPYMESCreate(int? id)
        {
            findArticulo("Fomento-a-la-PYMES", id);
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "VFomentoalaPYMES")]
        public ActionResult FomentoalaPYMESCreate(int? id, articulos_contenido articulos_contenido, List<int> image, List<int> pdf, List<int> video)
        {
            redirectAndActionValidate classRedirect = DBCreate("Fomento-a-la-PYMES", id, articulos_contenido, image, pdf, video);
            if (classRedirect.validateRedirect)
            {
                return classRedirect.redirect;
            }
            return classRedirect.view;

        }

        [Authorize(Roles = "VFomentoalaPYMES")]
        public ActionResult FomentoalaPYMESEdit(int? id)
        {
            redirectAndActionValidate rediClass = DBeditIndex("Fomento-a-la-PYMES", id);
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
        [Authorize(Roles = "VFomentoalaPYMES")]
        public ActionResult FomentoalaPYMESEdit(articulos_contenido articulos_conteni, List<int> image, List<int> pdf, List<int> video)
        {
            redirectAndActionValidate classRedirect = DBEdit("Fomento-a-la-PYMES", articulos_conteni, image, pdf, video);
            if (classRedirect.validateRedirect)
            {
                return classRedirect.redirect;
            }
            return classRedirect.view;


        }

        [HttpPost]
        [Authorize(Roles = "VFomentoalaPYMESDelete")]
        public JsonResult FomentoalaPYMESDelete(int? id, int? idd)
        {
            return DBDelete(id);
        }

        #endregion

        #region ZonasFrancas
        [Authorize(Roles = "VZonasFrancas")]
        public ActionResult ZonasFrancas(int? id, int? padrePrincipalId)
        {
            return View("Zonas-Francas", id, padrePrincipalId);
        }

        [Authorize(Roles = "VZonasFrancas")]
        public ActionResult ZonasFrancasCreate(int? id)
        {
            findArticulo("Zonas-Francas", id);
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "VZonasFrancas")]
        public ActionResult ZonasFrancasCreate(int? id, articulos_contenido articulos_contenido, List<int> image, List<int> pdf, List<int> video)
        {
            redirectAndActionValidate classRedirect = DBCreate("Zonas-Francas", id, articulos_contenido, image, pdf, video);
            if (classRedirect.validateRedirect)
            {
                return classRedirect.redirect;
            }
            return classRedirect.view;

        }

        [Authorize(Roles = "VZonasFrancas")]
        public ActionResult ZonasFrancasEdit(int? id)
        {
            redirectAndActionValidate rediClass = DBeditIndex("Zonas-Francas", id);
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
        [Authorize(Roles = "VZonasFrancas")]
        public ActionResult ZonasFrancasEdit(articulos_contenido articulos_conteni, List<int> image, List<int> pdf, List<int> video)
        {
            redirectAndActionValidate classRedirect = DBEdit("Zonas-Francas", articulos_conteni, image, pdf, video);
            if (classRedirect.validateRedirect)
            {
                return classRedirect.redirect;
            }
            return classRedirect.view;


        }
        [HttpPost]
        [Authorize(Roles = "VZonasFrancasDelete")]
        public JsonResult ZonasFrancasDelete(int? id, int? idd)
        {
            return DBDelete(id);
        }
        #endregion


    }


}