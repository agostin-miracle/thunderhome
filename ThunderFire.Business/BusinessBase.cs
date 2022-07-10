using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire.Business
{
    public class BusinessBase
    {
        /// <summary>
        /// Retorna true se houve um erro de processamento
        /// </summary>
        public bool HasError { get; internal set; } = false;
        /// <summary>
        /// Retorna true se uma ocorrencia de pesquisa foi encontrada
        /// </summary>
        public bool Found { get; internal set; } = false;
        /// <summary>
        /// Retorna true se o objeto corrente está conectado
        /// </summary>
        public bool Connected { get; internal set; } = false;
        public short KeyTableId { get; internal set; } = 0;
       // public int KeyTableCode { get; internal set; } = 0;
        public short ProcessCode { get; set; } = 0;


        /// <summary>
        /// Objeto de Retorno
        /// </summary>
        public object ReturnValue { get; set; }

        /// <summary>
        /// Retorna a última mensagem de alerta ou informação
        /// </summary>
        public string MessageToUser { get; set; } = "";

        /// <summary>
        /// Código do Erro
        /// </summary>
        public string ErrorCode { get; set; } = "OK";

        /// <summary>
        /// Chave base processamento
        /// </summary>
        /// <returns></returns>
        public long GetAuthorizationID()
        {
            try
            {
                long.Parse("1" + this.KeyTableId.ToString("D4") + this.ProcessCode.ToString("D4"));
            }
            catch { }
            return 0;
        }
    }
}
