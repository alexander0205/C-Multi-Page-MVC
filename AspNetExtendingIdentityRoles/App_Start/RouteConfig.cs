using PageWebMic.Areas.Media.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PageWebMic
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            string parametros = "{params1}/{params2}/{params3}/{params4}/{params5}/{params6}";
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Direcciones",
                url: "{controller}/"+ parametros,
                defaults: new {  action = "ProcessCommand", params1 = UrlParameter.Optional, params2 = UrlParameter.Optional, params3 = UrlParameter.Optional, params4 = UrlParameter.Optional, params5 = UrlParameter.Optional, params6 = UrlParameter.Optional },
                namespaces: new[] { "PageWebMic.Controllers" }

            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "PageWebMic.Controllers" }

            );
         


            routes.MapRoute(
             name: "Get Json File List",
             url: "Files",
             defaults: new { controller = "Files", action = "List" },
             constraints: new { httpMethod = new HttpMethodConstraint("GET") },
              namespaces: new[] { "PageWebMic.Controllers" }
         );

            routes.MapRoute(
                name: "Post Files",
                url: "Files",
                defaults: new { controller = "Files", action = "Uploads" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") },
                    namespaces: new[] { "PageWebMic.Controllers" }
            );

            routes.MapRoute(
                name: "Delete File",
                url: "Files/{id}/{filename}",
                defaults: new { controller = "Files", action = "Delete" },
                constraints: new { httpMethod = new HttpMethodConstraint("DELETE") },
                  namespaces: new[] { "PageWebMic.Controllers" }
            );



        }
    }
}
