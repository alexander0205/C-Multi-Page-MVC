using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PageWebMic.Areas.Admin.Controllers
{
    public class articulosContenidoUrl
    {
        public int id;
        public int? principalParent;
        public Nullable<int> id_parent;
        public string id_text;
        public string text;
        public string href;
        public Boolean expanded = true;
       
        public string type { get;  set; }
        public string data { get;  set; }
        public string dataParent { get; set; }

        public List<articulosContenidoUrl> children = new List<articulosContenidoUrl>();
    }
}