using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire
{
    /// <summary>
    /// Utilitários JSON
    /// </summary>
    public class JsonUtil
    {
        /// <summary>
        /// Deserializa uma string Json para uma lista de objeto
        /// </summary>
        /// <typeparam name="T">Objeto de Referencia</typeparam>
        /// <param name="json">Json String</param>
        /// <returns>List de Objeto</returns>
        public static List<T> DeSerialize<T>(string json)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<T>),
                  new DataContractJsonSerializerSettings
                  {
                      DateTimeFormat = new DateTimeFormat("yyyy-MM-dd")
                  }
                );
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            return (List<T>)ser.ReadObject(stream);
        }
        /// <summary>
        /// Deserializa uma string Json para um objeto
        /// </summary>
        /// <typeparam name="T">Objeto de Referência</typeparam>
        /// <param name="json">String json</param>
        /// <param name="p">Referencia de Assinatura</param>
        /// <returns>Objeto em T</returns>
        public static T DeSerialize<T>(string json, byte p = 0)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            return (T)ser.ReadObject(stream);
        }

        /// <summary>
        /// Serializa um objeto
        /// </summary>
        /// <typeparam name="T">Objeto a ser serializado</typeparam>
        /// <param name="t">Instancia do Objeto</param>
        /// <returns>string json</returns>
        public static string Serialize<T>(T t)
        {
            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(T),
                          new DataContractJsonSerializerSettings
                          {
                              DateTimeFormat = new DateTimeFormat("yyyy-MM-dd")
                          });
            MemoryStream stream = new MemoryStream();
            ds.WriteObject(stream, t);
            string j = Encoding.UTF8.GetString(stream.ToArray());
            stream.Close();
            return j;
        }

        /// <summary>
        /// Converte uma string Json para um DataTable
        /// </summary>
        /// <param name="pJsonString">String no formato Json</param>
        /// <returns>DataTable</returns>
        public static DataTable JToDataTable(string pJsonString)
        {
            DataTable dt = (DataTable)JsonConvert.DeserializeObject(pJsonString, (typeof(DataTable)));
            return dt;
        }
    }
}