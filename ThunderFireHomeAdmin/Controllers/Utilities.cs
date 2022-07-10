using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThunderFireHomeAdmin.Controllers
{
    public class Utilities
    {
        public static string GetRequest(string qname, string defaultValue)
        {
            try
            {
                var r = HttpContext.Current.Request[qname].ToString();
                return r;
            }
            catch {

                return defaultValue;
            }

        }

    }
}