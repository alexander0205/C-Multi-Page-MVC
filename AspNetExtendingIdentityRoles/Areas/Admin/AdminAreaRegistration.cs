using PageWebMic.Areas.Admin.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PageWebMic.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public static string UrlContexto;
        
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "PageWebMic.Areas.Admin.Controllers" }
            );
            
        }



    }
}