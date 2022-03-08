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
        public int KeyTableId { get; internal set; } = 0;
       // public int KeyTableCode { get; internal set; } = 0;
        public int ProcessCode { get; set; } = 0;

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
