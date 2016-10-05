using System.Web.Mvc;
using System.Web.Routing;

namespace PageWebMic.Areas.Media
{
    public class MediaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Media";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Media_default",
                "Media/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

          
        }
    }
}