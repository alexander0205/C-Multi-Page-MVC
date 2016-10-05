using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PageWebMic.include
{
    public class AdditionalHelpers
    {
        public static string Boolean(Nullable<bool> target)
        {
     
            string span = "<span class='glyphicon glyphicon-{0}' aria-hidden='true'></span>";
            if (target == true)
                return String.Format(span, "ok");
            else 
                return String.Format(span, "remove");
        }
    }
}