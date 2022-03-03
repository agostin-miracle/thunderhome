using System.Net.Http;
using System.Web;

namespace ThunderFire
{
    /// <summary>
    /// Funcionalidades do Http
    /// </summary>
    public class Http
    {
        /// <summary>
        /// Retorna o Ip do Cliente
        /// </summary>
        /// <param name="request">HttpRequestMessage</param>
        /// <returns>string</returns>
        public static string GetClientIp(HttpRequestMessage request)
        {
            string ip = string.Empty;
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                HttpContextBase context = (HttpContextBase)request.Properties["MS_HttpContext"];
                if (context.Request.ServerVariables["HTTP_VIA"] != null)
                    ip = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                else
                    ip = context.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            return ip;
        }
    }
}
