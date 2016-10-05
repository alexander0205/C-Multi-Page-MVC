using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITCloud.Web.Routing;
namespace PageWebMic.Controllers
{
    public class DireccionesController : Controller
    {
        public ActionResult ProcessCommand(string params1, string params2 , string params3, string params4 , string params5, string params6)
        {
            
               return View("view");
        }
        //
        //  UrlRoute Name property is optional, it can be used when generating
        //  outbound links (e.g. via Html.RouteLink).
        //
        //  UrlRoute Path property is required, and contains the URL that
        //  will be routed to the action method.  It must not begin with "/".
        //
        [UrlRoute(Name = "Home", Path = "")]
        public ActionResult Index()
        {
            return View("view");
        }

        //
        //  Route paths containing parameters (e.g. "title" below) are
        //  mapped to action method parameters of the same name.
        //
        //  Optional UrlRouteParameterDefault attribute allows you to
        //  specify a default value for a parameter if it is omitted in
        //  the incoming URL.
        //
        [UrlRoute(Path = "direcciones/{title}")]
        [UrlRouteParameterDefault(Name = "title", Value = "latest-post")]
        public ActionResult DireccionesgEntryByTitle(string title)
        {
            // omitted code to retrieve blog based on title

            return View();
        }

        //
        //  The UrlRouteParameterContraint attribute specified below
        //  ensures that an incoming URL routes to this method only if
        //  the postDate parameter matches the date format YYYY-MM-DD
        //  (using the RegEx).  E.g. blogs/2009-07-15
        //
        //  The UrlRoute Order property specifies the order in which the
        //  route is evaluated relative to other routes.  The default Order
        //  value if not specified is 0.  If multiple routes have the same
        //  Order, then the order in which they are evaluated relative to
        //  each other is undefined and should not be relied upon.
        //
        //  In the route below, it is necessary to specify a negative Order
        //  value to ensure that the route is evaluated before the
        //  BlogEntryByTitle route specified above.  Otherwise, even if the
        //  incoming URL contains a postDate in YYYY-MM-DD format, the other
        //  route could potentially be evaluated first, which would match
        //  but would not be the desired outcome in this case.
        //
        [UrlRoute(Path = "direcciones/{postDate}", Order = -10)]
        [UrlRouteParameterConstraint(Name = "postDate", Regex = @"^\d\d\d\d-\d\d-\d\d$")]
        public ActionResult DireccionesEntriesByDate(DateTime postDate)
        {
            // omitted code to retrieve blogs that were posted on postDate

            return View();
        }




    }
}
