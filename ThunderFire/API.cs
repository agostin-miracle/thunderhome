using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire
{
    /// <summary>
    /// Classe para consumo de API
    /// </summary>
    public class API
    {
        /// <summary>
        /// Parãmetros de Envio de Requisição
        /// </summary>
        public enum ContenType
        {
            /// <summary>
            /// Json
            /// </summary>
            JSON = 1,
            /// <summary>
            /// "application/x-www-form-urlencoded
            /// </summary>
            URLENCODED = 2
        }

        /// <summary>
        /// Retorna true se a requisição ou processo foi bem sucedido
        /// </summary>
        public static bool HasError { get; internal set; }


        /// <summary>
        /// Captura de Erros
        /// </summary>
        public static TrapError TrappedError = new TrapError();

        /// <summary>
        /// String dos dados postados
        /// </summary>
        public static string DataPosted { get; set; } = "";


        /// <summary>
        /// Efetua uma chamada do Tipo Post 
        /// </summary>
        /// <typeparam name="R">Response Object</typeparam>
        /// <typeparam name="T">Request Object</typeparam>
        /// <param name="EndPoint">Localização da API</param>
        /// <param name="action">Método</param>
        /// <param name="data">Body</param>
        /// <param name="sendform">Content-type</param>
        /// <returns>String</returns>
        public static R POST<R, T>(string EndPoint, string action, T data, ContenType sendform = ContenType.JSON)
        {
            if (sendform == ContenType.URLENCODED)
                DataPosted = Objects.CreateContentString(data);
            else
                DataPosted = ThunderFire.JsonUtil.Serialize<T>(data);
            return PostOrPut<R, T>("POST", EndPoint, action, sendform);
        }
        /// <summary>
        /// Efetua uma chamada do Tipo Put 
        /// </summary>
        /// <typeparam name="R">Response Object</typeparam>
        /// <typeparam name="T">Request Object</typeparam>
        /// <param name="EndPoint">Localização da API</param>
        /// <param name="action">Método</param>
        /// <param name="data">Body</param>
        /// <param name="sendform">Content-type</param>
        /// <returns>Void</returns>
        public static R PUT<R,T>(string EndPoint, string action, T data, ContenType sendform = ContenType.JSON)
        {
            return PostOrPut<R, T>("PUT", EndPoint, action, sendform);
        }

        private static R PostOrPut<R,T>(string method, string EndPoint, string action, ContenType sendform = ContenType.JSON)
        {
            R RETURN_VALUE = (R)Activator.CreateInstance(typeof(R));
            if (EndPoint.EndsWith("/"))
                EndPoint = EndPoint + action;
            else
                EndPoint = EndPoint + "/" + action;
            TrappedError.AddMessage(string.Format("Conectando {0}", EndPoint));
       
            bool go = true;

            if (EndPoint != "")
            {
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(EndPoint);
                request.Method = method;
                request.Headers.Add("Cache-Control", "no-store");
                request.Accept = "application/json";
                if (sendform == ContenType.URLENCODED)
                    request.ContentType = "application/x-www-form-urlencoded";
                else
                    request.ContentType = "application/json";

                UTF8Encoding encoding = new System.Text.UTF8Encoding();
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(DataPosted.ToString());
                request.ContentLength = bytes.Length;

                TrappedError.AddMessage(String.Format("Posting = Length({0}):{1}", DataPosted.Length, DataPosted));

                try
                {
                    using (System.IO.Stream writeStream = request.GetRequestStream())
                    {
                        writeStream.Write(bytes, 0, bytes.Length);
                    }
                }
                catch (System.Net.WebException Error)
                {
                    TrappedError.SetError("RESOURCEFAILREAD");
                    TrappedError.ErrorMessage = Error.Message;
                    TrappedError.ErrorObject = Error;
                    go = false;
                }

                if (go)
                {
                    try
                    {
                        System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                        TrappedError.StatusCode = (int)response.StatusCode;
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string responseValue = string.Empty;
                            using (System.IO.Stream responseStream = response.GetResponseStream())
                            {
                                if (responseStream != null)
                                {
                                    using (System.IO.StreamReader reader = new System.IO.StreamReader(responseStream))
                                    {
                                        responseValue = reader.ReadToEnd();
                                    }
                                }
                            }
                            TrappedError.StatusDescription = response.StatusCode.ToString();
                            TrappedError.ResponseBody = responseValue;

                            //if (!string.IsNullOrEmpty(TrappedError.ResponseBody))
                            //    RETURN_VALUE = JsonConvert.DeserializeObject<R>(TrappedError.ResponseBody);

                        }
                    }
                    catch (System.Net.WebException Error)
                    {
                        /*
                         * API Lança um erro
                         */
                        TreatError(Error);

                        /*
                         *  Monta response Body
                         */
                 
                    }
                }

                if (!string.IsNullOrEmpty(TrappedError.ResponseBody))
                    RETURN_VALUE = JsonConvert.DeserializeObject<R>(TrappedError.ResponseBody);
            }
            return RETURN_VALUE;
        }

        /// <summary>
        /// Efetua uma chamada do Tipo Get 
        /// </summary>
        /// <param name="EndPoint">Localização da API</param>
        /// <param name="action">Action Method</param>
        /// <param name="data">Dados em formato string</param>
        /// <param name="sendform">Forma de Envio</param>
        /// <remarks>
        /// O conteúdo de resposta pode ser obtido acessando o membro ThunderFire.TrappedError.ResponseBody
        /// </remarks>
        /// <returns>void</returns>
        public static void GET(string EndPoint, string action, string data="", ContenType sendform = ContenType.URLENCODED)
        {
            DataPosted = data;
            GET(EndPoint, action, sendform);
        }

        /// <summary>
        /// Efetua uma chamada do Tipo Get 
        /// </summary>
        /// <param name="EndPoint">Localização da API</param>
        /// <param name="action">Action Method</param>
        /// <param name="data">Object</param>
        /// <param name="sendform">Forma de Envio</param>
        /// <remarks>
        /// O conteúdo de resposta pode ser obtido acessando o membro ThunderFire.TrappedError.ResponseBody
        /// </remarks>
        /// <example>
        /// <code>
        ///   var param =new   
        ///   {
        ///     pINDLCT = Livre.Base.Constants.INDICADOR_CONTA_CORRENTE,
        ///     pCODUSU = 54,
        ///     pDATMOV1 = DateTime.Now.AddDays(-700).ToString("yyyyMMdd"),
        ///     pDATMOV2 = DateTime.Now.AddDays(1).ToString("yyyyMMdd"),
        ///     pARGSEL = "",
        ///     pTIPSEL = (string)null,
        ///     pTOPREC = (int?)null,
        ///     pNUMDAY = (int?)null
        ///     };
        ///     
        ///     ThunderFire.API.GET("http://localhost:19274/", "controlecc/getcurrentaccountmovement", param);
        ///     if (ThunderFire.API.TrappedError.StatusCode == 200)
        ///     {
        ///         DataTable result = ThunderFire.JsonUtil.JToDataTable(ThunderFire.API.TrappedError.ResponseBody);
        ///     }
        /// </code>
        /// </example>
        /// <returns>void</returns>
        public static void GET(string EndPoint, string action, object data=null, ContenType sendform = ContenType.URLENCODED)
        {
            if (data != null)
                DataPosted = Objects.CreateContentString(data);
            else
                DataPosted = "";

            GET(EndPoint, action, sendform);
        }

        ///// <summary>
        ///// Efetua uma chamada do Tipo Get 
        ///// </summary>
        ///// <typeparam name="T">Request Object</typeparam>
        ///// <param name="EndPoint">EndPoint</param>
        ///// <param name="action">Action Method</param>
        ///// <param name="data">Dados em T</param>
        ///// <param name="sendform">Forma de Envio</param>
        ///// <remarks>
        ///// O conteúdo de resposta pode ser obtido acessando o membro ThunderFire.TrappedError.ResponseBody
        ///// </remarks>
        ///// <returns>void</returns>
        //public static void GET<T>(string EndPoint, string action, T data, ContenType sendform = ContenType.URLENCODED)
        //{
        //    if (data != null)
        //    {
        //        if (sendform == ContenType.URLENCODED)
        //            DataPosted = CreateContentString(data);
        //        else
        //            DataPosted = ThunderFire.JsonUtil.Serialize<T>(data);
        //    }
        //    else
        //        DataPosted = "";
        //    GET(EndPoint, action,sendform);
        //}
        /// <summary>
        /// Efetua uma chamada do Tipo Get 
        /// </summary>
        /// <typeparam name="R">Response Object</typeparam>
        /// <typeparam name="T">Request Object</typeparam>
        /// <param name="EndPoint">EndPoint</param>
        /// <param name="action">Action Method</param>
        /// <param name="data">Dados</param>
        /// <param name="sendform">Forma de Envio</param>
        /// <remarks>
        /// O conteúdo de resposta pode ser obtido acessando o membro ThunderFire.TrappedError.ResponseBody
        /// </remarks>
        /// <returns>Response Object</returns>
        public static R GET<R, T>(string EndPoint, string action, T data, ContenType sendform = ContenType.URLENCODED)
        {
            if (data != null)
            {
                if (sendform == ContenType.URLENCODED)
                    DataPosted = Objects.CreateContentString(data);
                else
                    DataPosted = ThunderFire.JsonUtil.Serialize<T>(data);
            }
            else
                DataPosted = "";

            return GetWithObject<R>(EndPoint, action, sendform);
        }

        /// <summary>
        /// Efetua uma chamada do Tipo Get com retorno de objeto
        /// </summary>
        /// <typeparam name="R">Response Object</typeparam>
        /// <param name="EndPoint">EndPoint</param>
        /// <param name="action">Action Method</param>
        /// <param name="data">Dados</param>
        /// <param name="sendform">Forma de Envio</param>
        /// <example>
        /// <code>
        ///    var param1 = new
        ///    {
        ///        pCODUSU = 54,
        ///        pTIPVAL = "2",
        ///        pINDLCT = 2
        ///    };
        ///    ResponseService rsp = ThunderFire.API.GET&lt;ResponseService&gt;("http://localhost:19274/", "controlecc/GetSaldoContaCorrente", param1);
        ///    Log.Write(Log.LogPropertyValues(rsp));
        /// </code>
        /// </example>
        /// <returns>Response Object</returns>
        public static R GET<R>(string EndPoint, string action, object data, ContenType sendform = ContenType.URLENCODED)
        {
            if (data != null)
            {
                DataPosted = Objects.CreateContentString(data);
            }
            else
                DataPosted = "";

            return GetWithObject<R>(EndPoint, action, sendform);
        }


        /// <summary>
        /// Efetua uma chamada do Tipo Get 
        /// </summary>
        /// <param name="EndPoint">Localização da API</param>
        /// <param name="action">Action Method</param>
        /// <param name="sendform">Forma de Envio</param>
        /// <remarks>
        /// O conteúdo de resposta pode ser obtido acessando o membro ThunderFire.TrappedError.ResponseBody
        /// </remarks>
        /// <returns>void</returns>
        private static void GET(string EndPoint, string action, ContenType sendform)
        {
            if (EndPoint.EndsWith("/"))
                EndPoint = EndPoint + action;
            else
                EndPoint = EndPoint + "/" + action;
            TrappedError.AddMessage(string.Format("Conectando {0}", EndPoint));
            if (!String.IsNullOrWhiteSpace(DataPosted))
                EndPoint += "?" + DataPosted;
            TrappedError.AddMessage(String.Format("Get = {0}", EndPoint));
            bool go = true;

            if (EndPoint != "")
            {
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(EndPoint);
                request.Method = "GET";
                request.Headers.Add("Cache-Control", "no-store");
                request.Accept = "application/json";
                if (sendform == ContenType.URLENCODED)
                    request.ContentType = "application/x-www-form-urlencoded";
                else
                    request.ContentType = "application/json";
                UTF8Encoding encoding = new System.Text.UTF8Encoding();
                if (go)
                {
                    try
                    {
                        System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                        TrappedError.StatusCode = (int)response.StatusCode;
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string responseValue = string.Empty;
                            using (System.IO.Stream responseStream = response.GetResponseStream())
                            {
                                if (responseStream != null)
                                {
                                    using (System.IO.StreamReader reader = new System.IO.StreamReader(responseStream))
                                    {
                                        responseValue = reader.ReadToEnd();
                                    }
                                }
                            }
                            TrappedError.StatusDescription = response.StatusCode.ToString();
                            TrappedError.ResponseBody = responseValue;
                        }
                    }
                    catch (System.Net.WebException Error)
                    {
                        TreatError(Error);

                    }
                }
            }
        }

        private static R GetWithObject<R>(string EndPoint, string action, ContenType sendform)
        {
            R RETURN_VALUE = (R)Activator.CreateInstance(typeof(R));
            if (EndPoint.EndsWith("/"))
                EndPoint = EndPoint + action;
            else
                EndPoint = EndPoint + "/" + action;
            TrappedError.AddMessage(string.Format("Conectando {0}", EndPoint));
            if (!String.IsNullOrWhiteSpace(DataPosted))
                EndPoint += "?" + DataPosted;
            TrappedError.AddMessage(String.Format("Get = {0}", EndPoint));
            bool go = true;

            if (EndPoint != "")
            {
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(EndPoint);
                request.Method = "GET";
                request.Headers.Add("Cache-Control", "no-store");
                request.Accept = "application/json";
                if (sendform == ContenType.URLENCODED)
                    request.ContentType = "application/x-www-form-urlencoded";
                else
                    request.ContentType = "application/json";
                UTF8Encoding encoding = new System.Text.UTF8Encoding();
                if (go)
                {
                    try
                    {
                        System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                        TrappedError.StatusCode = (int)response.StatusCode;
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            string responseValue = string.Empty;
                            using (System.IO.Stream responseStream = response.GetResponseStream())
                            {
                                if (responseStream != null)
                                {
                                    using (System.IO.StreamReader reader = new System.IO.StreamReader(responseStream))
                                    {
                                        responseValue = reader.ReadToEnd();
                                    }
                                }
                            }
                            TrappedError.StatusDescription = response.StatusCode.ToString();
                            TrappedError.ResponseBody = responseValue;
                        }
                    }
                    catch (System.Net.WebException Error)
                    {
                        TreatError(Error);
                    }
                }

                if (!string.IsNullOrEmpty(TrappedError.ResponseBody))
                    RETURN_VALUE = JsonConvert.DeserializeObject<R>(TrappedError.ResponseBody);

            }
            return RETURN_VALUE;
        }


        private static void TreatError(System.Net.WebException Error)
        {
            if (Error.Status == System.Net.WebExceptionStatus.ProtocolError)
            {
                System.Net.HttpWebResponse exceptionResponse = Error.Response as System.Net.HttpWebResponse;
                if (exceptionResponse != null)
                {
                    string exceptionResponseValue = string.Empty;
                    using (System.IO.Stream exceptionResponseStream = exceptionResponse.GetResponseStream())
                    {
                        if (exceptionResponseStream != null)
                        {
                            using (System.IO.StreamReader exceptionReader = new System.IO.StreamReader(exceptionResponseStream))
                            {
                                exceptionResponseValue = exceptionReader.ReadToEnd();
                            }
                        }
                    }
                    TrappedError.StatusCode = (int)exceptionResponse.StatusCode;
                    TrappedError.StatusDescription = exceptionResponse.StatusDescription;
                    TrappedError.ResponseBody = exceptionResponseValue;
                    TrappedError.SetError(TrappedError.StatusCode.ToString());
                }
                else
                    TrappedError.SetError("INVALIDPROTOCOL");
            }
            else
                TrappedError.SetError("INVALIDPROTOCOL");
            TrappedError.ErrorMessage = Error.Message;
            TrappedError.ErrorObject = Error;
        }
    }
}