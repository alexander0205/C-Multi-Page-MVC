using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PageWebMic.Areas.Languajes;
using PageWebMic.Areas.Admin.Models;
using System.Web.Script.Serialization;
using System.Net;
using System.Data.Entity;

namespace PageWebMic.Areas.Admin.Controllers
{
    [Authorize]
    public abstract class AuthorizeBaseController : System.Web.Mvc.Controller
    {
        private paginaMICEntities2 db = new paginaMICEntities2();
        private string nameAction,nameController;

        List<articulosContenidoUrl> listArticulo;

        public AuthorizeBaseController()
        {
            createVieBagNames();
        }
       

        private void createVieBagNames()
        {
            System.Resources.ResourceSet resourceSet = Areas.Languajes.BaseAdmin.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.CurrentUICulture, true, true);
            foreach (System.Collections.DictionaryEntry item in resourceSet)
            {
                string resourceKey = (string)item.Key;
                string resource = (string)item.Value;
                ViewData[resourceKey] = resource;
            }
            ViewBag.menuDirecciones = db.articulos_contenido.Where(x => x.id_title == "direcciones").First().articulos_contenido1;
            ViewBag.menuViceministerios = db.articulos_contenido.Where(x => x.id_title == "Viceministerios").First().articulos_contenido1;
            // ViewBag.actionName = ControllerContext.RouteData.Values["action"].ToString();
            // ViewBag.controllerName =ControllerContext.RouteData.Values["controller"].ToString();
        }

        private void configurationActiveUrl() {

        }



        /*   protected List<articulosContenidoUrl> getUrlsTable(int? idGetUrl)
           {
               int count = db.articulos_contenido.OrderBy(x => x.parent_id).Count();
               this.listArticulo = new List<articulosContenidoUrl>(count);

               getUrls(idGetUrl);

                int i =0;
                int  positionId =1;

                List<articulosContenidoUrl> listArticulo = new List<articulosContenidoUrl>();

                string[] data = new string[count];
                string[] dataParent = new string[count];
                int[] idInt = new int[count];
                Nullable<int> valorAgregado = 0;
                int valorAsignado = 0;
                foreach (var item in this.listArticulo)
                {

                    idInt[i] = item.id;

                    if (item.id_parent == null) {
                        data[i] = positionId.ToString();
                    } else {
                        int index2 = Array.IndexOf(idInt, item.id_parent);
                        if (valorAgregado == item.id_parent)
                        {
                            valorAsignado++;
                        }
                        else {
                            valorAsignado = 0;
                            valorAsignado++;
                        }

                        data[i] += data[index2] + "." + valorAsignado.ToString();
                        dataParent[i] += data[index2];
                        valorAgregado = item.id_parent;
                    }

                articulosContenidoUrl articulosContenido = new articulosContenidoUrl()
                    {
                        id = item.id,
                        principalParent = item.principalParent,
                        id_parent = item.id_parent,
                        data = data[i],
                        dataParent = dataParent[i],
                        text = item.text,
                        type = item.type,
                        href = Url.Action("Index", "articulos_contenido", new { id = item.id, padrePrincipalId = item.principalParent}),
                    };
                    listArticulo.Add(articulosContenido);
                    i ++;
                    positionId++;

                }

               return listArticulo;
           }
           */
        
        protected List<articulosContenidoUrl> getUrls(int? idGetUrl)
        {
            ViewData["idSelectNode"] = idGetUrl;
            nameAction = "index";
            nameController = "articulos_contenido";
           return getListArticulosResults(db.articulos_contenido.Where(x => x.parent_id == null));

        }

        protected List<articulosContenidoUrl> getUrls(int? idGetUrl, string PActionName,string PControllerName)
        {
           string controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            ViewData["idSelectNode"] = idGetUrl;
            IQueryable<articulos_contenido>   dbResults = db.articulos_contenido.Where(x => x.id_title == PActionName).Where(x => x.articulos_contenido2.id_title == controllerName);
            nameAction = PActionName;
            nameController = PControllerName;
            return getListArticulosResults(dbResults);

        }

        protected List<articulosContenidoUrl> getListArticulosResults(IQueryable<articulos_contenido> data) {
            List<articulosContenidoUrl> artLista = new List<articulosContenidoUrl>();
            foreach (var item in data)
            {
                string clas;
                if (item.articulos_contenido1.Count > 0)
                {
                    clas = "default";

                }
                else {
                    clas = "file";
                };
                articulosContenidoUrl articulosContenido = new articulosContenidoUrl()
                {
                    id = item.id,
                    principalParent = item.id,
                    id_text = item.id_title,
                    id_parent = item.parent_id,
                    text = item.titulo,
                    type = clas,
                    href = urlString( new { id = item.id, padrePrincipalId = item.id }),
                };
                // listArticulo.Add(articulosContenido);
                articulosContenido = foreachArticulosContenido(item.parent_id, articulosContenido, item.articulos_contenido1);
                artLista.Add(articulosContenido);
            }


            return artLista;
        }

        protected articulosContenidoUrl foreachArticulosContenido(int? principalParent,articulosContenidoUrl articulosContenido, ICollection<articulos_contenido> articulos_contenido)
        {
            foreach (var item in articulos_contenido)
            {
                string clas;
                if (item.articulos_contenido1.Count > 0)
                {
                    clas = "default";
                }
                else {
                    clas = "file";
                };
                articulosContenidoUrl articulosContenidos = new articulosContenidoUrl()
                {
                    id = item.id,
                    id_text = item.id_title,
                    principalParent = principalParent,
                    id_parent = item.parent_id,
                    text = item.titulo,
                    type= clas,
                    href = urlString(new { id = item.id, padrePrincipalId = principalParent }),
                };
               // listArticulo.Add(articulosContenidos);
                articulosContenidos = foreachArticulosContenido(principalParent,articulosContenidos, item.articulos_contenido1);
                articulosContenido.children.Add(articulosContenidos);


            }
            return articulosContenido;
        }

        protected string urlString(object objeto) {
            return Url.Action(nameAction.Replace("-",""), nameController, objeto);
        }


        private articulos_contenido instanceContenido;

        #region Functions ActionsManagers
        protected void findArticulo(string direction, int? idp)
        {

            instanceContenido = db.articulos_contenido.Where(x => x.id_title == direction).First();
            ViewBag.IdInstance = instanceContenido.id;
            if (idp == 0)
            {
                idp = instanceContenido.id;
            }
            //  ViewBag.menuContenido = getUrlsTable(id);
            ViewBag.selectFilesUrl = Url.Action("List", "files", new { area = string.Empty, id = instanceContenido.id });
            var selet = db.articulos_contenido.Find(idp);

            ViewBag.selectFiles = "[]";
            if (selet != null)
            {
                var mediaSelect = selet.articulos_contenido_media.Select(x => x.media_id);
                ViewBag.selectFiles = new JavaScriptSerializer().Serialize(mediaSelect);
            }


            ViewBag.urlDetails = Url.Action(direction.Replace("-","") + "Details", new { id = idp, padrePrincipalId = instanceContenido.parent_id });

            ViewBag.urlCreate = Url.Action(direction.Replace("-", "") + "Create", new { id = idp });

            ViewBag.urlEdit = Url.Action(direction.Replace("-", "") + "Edit", new { id = idp });

            ViewBag.urlDeleteNode = Url.Action(direction.Replace("-", "") + "Delete", new { idd = idp });

            ViewBag.urlRedirectNode = direction;

            ViewBag.urlMedia = Url.Action("contenido", "Home", new { @area = "media", id = instanceContenido.id });

            ViewBag.backList = direction;

            ViewBag.idParents = idp;
        }
        articulos_contenido crearArtiMedia(List<int> list, articulos_contenido articulos_contenido)
        {
            foreach (int item in list)
            {
                articulos_contenido_media articulosContenidoMedia = new articulos_contenido_media();
                articulosContenidoMedia.articulos_contenido_id = articulos_contenido.id;
                articulosContenidoMedia.media_id = item;
                db.articulos_contenido_media.Add(articulosContenidoMedia);
            }
            return articulos_contenido;
        }




        public ViewResult View(string contenidoId, int? id, int? padrePrincipalId)
        {

            ViewBag.modelIdValidate = true;
            var getUr = getUrls(id, contenidoId, nameController);
            ViewBag.menuContenido = new JavaScriptSerializer().Serialize(getUr);
            if (padrePrincipalId != null)
            {
                ViewData["padrePrincipalId"] = padrePrincipalId;
            }
            else
            {

                ViewData["padrePrincipalId"] = id;
            }
            ViewBag.idParents = id;

            if (id == 0 || id == null)
            {
                findArticulo(contenidoId, 0);
                ViewBag.modelIdValidate = false;
                return View("View", instanceContenido);
            }
            else {
                findArticulo(contenidoId, id);
                ViewBag.modelIdValidate = true;
                ViewBag.modelo = db.articulos_contenido.Find(id);
                return View("View", db.articulos_contenido.Find(id));
            }
        }
        public redirectAndActionValidate DBCreate(string contenidoId, int? id, articulos_contenido articulos_contenido, List<int> image, List<int> pdf, List<int> video) {
            redirectAndActionValidate classRedirect = new redirectAndActionValidate();
            findArticulo(contenidoId, id);
            articulos_contenido.id = 0;
            articulos_contenido.fecha = DateTime.Now;
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
                classRedirect.validateRedirect = true;
                classRedirect.redirect = RedirectToAction(contenidoId.Replace("-",""), new { id = articulos_contenido.id });
                return classRedirect;
            }
            classRedirect.view =  View("Create", articulos_contenido);
            return classRedirect;
        }
        public redirectAndActionValidate DBeditIndex(string contenidoId, int? id) {
            redirectAndActionValidate classRedirect = new redirectAndActionValidate();
            findArticulo(contenidoId, id);

            if (id == null)
            {
                classRedirect.validatehttpStatus = true;
                classRedirect.httpStatus =   new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            articulos_contenido articulos_contenido = db.articulos_contenido.Find(id);

            if (articulos_contenido == null)
            {
                classRedirect.validateHttpNoresults = true;
                classRedirect.httpNoResults =  HttpNotFound();
            }
            classRedirect.view = View("Edit", articulos_contenido);
            return classRedirect;
        }
        public redirectAndActionValidate DBEdit(string contenidoId, articulos_contenido articulos_conteni, List<int> image, List<int> pdf, List<int> video)
        {
            redirectAndActionValidate classRedirect = new redirectAndActionValidate();
            articulos_contenido articulos_contenido = db.articulos_contenido.Find(articulos_conteni.id);
            articulos_contenido.id_title = articulos_conteni.id_title;
            articulos_contenido.titulo = articulos_conteni.titulo;
            articulos_contenido.subtitulo = articulos_conteni.subtitulo;
            articulos_contenido.contenido = articulos_conteni.contenido;
            articulos_contenido.parent_id = articulos_conteni.parent_id;
            articulos_contenido.tags = articulos_conteni.tags;
            articulos_contenido.image_presentation_1 = articulos_conteni.image_presentation_1;
            articulos_contenido.image_presentation_2 = articulos_conteni.image_presentation_2;

            findArticulo(contenidoId, articulos_contenido.id);

            if (ModelState.IsValid)
            {
                db.articulos_contenido_media.RemoveRange(db.articulos_contenido_media.Where(x => x.articulos_contenido_id == articulos_contenido.id));

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
                classRedirect.validateRedirect = true;
                classRedirect.redirect = RedirectToAction(contenidoId.Replace("-", ""), new { id = articulos_contenido.id });
                return classRedirect;
            }
            classRedirect.view = View("Create", articulos_contenido);
            return classRedirect;
        }

        public JsonResult DBDelete(int? id)
        {
            var Resultado = new { mensaje="" , validate = false };

            if (id == null)
            {
                 Resultado = new { mensaje = "No se encuentra ID de  la Pagina", validate = false };
                return Json(Resultado);
            }
            articulos_contenido articulos_contenido = db.articulos_contenido.Find(id);
            if (articulos_contenido == null)
            {
                Resultado = new { mensaje = "No se encuentra la Pagina", validate = false };
                return Json(Resultado);
            }
           // db.articulos_contenido.Remove(articulos_contenido);
            db.Entry(articulos_contenido).State = EntityState.Deleted;
            if (db.SaveChanges() > 0) {
                Resultado = new { mensaje = "Eliminado", validate = true };
                return Json(Resultado);
            };
            return Json(new  { });
        }
        #endregion

    }


}