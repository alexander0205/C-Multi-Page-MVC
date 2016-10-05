using Newtonsoft.Json;
using PageWebMic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
namespace PageWebMic.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            viewModelFront model = new  viewModelFront();
            ViewData["viewModelFront"] = model;
            return View(model);
        }
        /*
        public  string imagenes() {

            string json = get_web_content("http://localhost:18499/CustomDataProvider/FileHandlerJpg/");

            dynamic array = JsonConvert.DeserializeObject(json);
            Response.Write("Code: " + array.Code + "<br>");
            Response.Write("Message: " + array.Message + "<br>");
            dynamic Data = array;
            dynamic DeviceList = Data;

            List<imagenResultado> resultados = new List<imagenResultado>();
            foreach (var item in DeviceList["files"])
            {
                imagenResultado listas = new imagenResultado();
                listas.image = item["type"];
                listas.thumb = item["type"];
                listas.thumb = "";
                resultados.Add(listas);
            }


            JavaScriptSerializer js = new JavaScriptSerializer();
            string dt = js.Serialize(resultados);

        /* return new JsonResult()
            {
                Data = resultados,
                ContentType = "application/javascript",
                ContentEncoding = System.Text.Encoding.UTF8,
                MaxJsonLength = Int32.MaxValue,
                RecursionLimit = null,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };*/

            //  return Json(resultados, "application/json", JsonRequestBehavior.AllowGet);

      
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string get_web_content(string url)
        {
            Uri uri = new Uri(url);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = WebRequestMethods.Http.Get;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string output = reader.ReadToEnd();
            response.Close();

            return output;
        }
    }
    public class imagenResultado {
        public string image;
        public string thumb;
        public string folder;

    }
    
  
}