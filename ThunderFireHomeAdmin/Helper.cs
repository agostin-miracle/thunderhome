using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThunderFireHomeAdmin
{
    public class AdminSite
    {

        /// <summary>
        /// Efetua o redirecionamento de processo
        /// </summary>
        /// <param name="page">Nome da Página para qual será redirecionadk</param>
        /// <param name="finalize">Define se o response será finalizado ou não</param>
        public static void RedirectTo(string page, bool finalize = false)
        {
            if (page != "")
            {
                HttpContext.Current.Response.Redirect(page, finalize);
            }
        }

        /// <summary>
        /// Obtêm o valor de um Request
        /// </summary>
        /// <param name="name">Nome do Request</param>
        /// <returns>string</returns>
        public static string GetRequest(string name)
        {
            string s = "";
            if (HttpContext.Current.Request[name] != null)
                s = HttpContext.Current.Request[name].ToString().Trim().ToUpper();
            return s;
        }

        public static string GetRequestValue(string name)
        {
            string s = "";
            if (HttpContext.Current.Request[name] != null)
                s = HttpContext.Current.Request[name].ToString().Trim();
            return s;
        }

    }
}