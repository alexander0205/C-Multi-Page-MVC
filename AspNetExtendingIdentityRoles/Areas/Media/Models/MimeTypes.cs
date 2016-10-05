using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PageWebMic.Areas.Media.Models
{
    public static class MimeTypes
    {
        public static Dictionary<string, string> ImageMimeTypes = new Dictionary<string, string>
        {
            { ".gif", "image/gif" },
            { ".jpeg", "image/jpeg" },
            { ".jpg", "image/jpeg" },
            { ".png", "image/png" },
            { ".PNG", "image/PNG" },
            { ".PDF", "application/PDF" },
            { ".pdf", "application/pdf" },
            { ".doc", "application/msword" },
            { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
        };
    }

}