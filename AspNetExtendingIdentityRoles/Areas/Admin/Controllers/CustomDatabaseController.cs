using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PageWebMic.Areas.Admin.Models;
using Backload.Contracts.Context;
using Backload.Contracts.FileHandler;
using Backload.Demo.Models;
using Backload.Helper;
using System.Threading.Tasks;

namespace PageWebMic.Areas.Admin.Controllers
{
    public class CustomDatabaseController : Controller
    {
        private paginaMICEntities2 db = new paginaMICEntities2();
        /// <summary>
        /// Custom database handler. 
        /// To access it in an Javascript ajax request use: <code>var url = "/CustomDatabase/DataHandler";</code>.
        /// </summary>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post | HttpVerbs.Put | HttpVerbs.Delete | HttpVerbs.Options)]
        public async Task<ActionResult> DataHandler()
        {

            try
            {

                using (var context = new paginaMICEntities2())
                {

                    // Create and initialize the handler
                    IFileHandler handler = Backload.FileHandler.Create();
                    handler.Init(HttpContext.Request, context);


                    // Call the execution pipeline and get the result
                    IBackloadResult result = await handler.Execute();


                    // Save changes to database
                    await context.SaveChangesAsync();


                    // Helper to create an ActionResult object from the IBackloadResult instance
                    return ResultCreator.Create(result);
                }

            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

        }
    }
}
