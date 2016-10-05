using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PageWebMic.Areas.Media.Models
{
    public class ViewDataUploadFilesResult
    {
        public int id { get; set; }
        public string url { get; set; }
        public string image { get; set; }
        public string folder { get; set; }
        public string thumb { get; set; }
        public string thumbnail_url { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int size { get; set; }
        public string delete_url { get; set; }
        public string delete_type { get; set; }
    }
}