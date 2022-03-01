using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
    ///<summary>
    /// Class of TBAUDDAT Alias Auditoria
    ///</summary>

    public class Auditing
    {
        /// <summary>
        /// ID Auditoria
        /// </summary>
        public int AUDNUM { get; set; } = 0;

        /// <summary>
        /// Usuário de Atualização
        /// </summary>
        public int UPDUSU { get; set; } = 0;

        /// <summary>
        /// Data da Auditoria
        /// </summary>
        public DateTime AUDDAT { get; set; } = DateTime.Now;

        /// <summary>
        /// Tabela Chave
        /// </summary>
        public int AUDKEY { get; set; } = 0;

        /// <summary>
        /// ID Associado a Tabela
        /// </summary>
        public int AUDIDR { get; set; } = 0;

        /// <summary>
        /// ID Auxiliar de Identificação da Operação
        /// </summary>
        public int AUDIMS { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public string AUDTSK { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string AUDOBJ { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string AUDSRC { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string AUDCHG { get; set; } = "";

        /// <summary>
        /// ID do Registro de Token
        /// </summary>
        public int NIDTOK { get; set; } = 0;

        /// <summary>
        /// IP de Origem
        /// </summary>
        public string NUMIPA { get; set; } = "";

    }
}
