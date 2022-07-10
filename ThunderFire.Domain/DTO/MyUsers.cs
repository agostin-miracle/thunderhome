using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire.Domain.DTO
{
    /// <summary>
    /// Objeto de Retenção de Informações de Cartões Associados ao usuário
    /// </summary>
    public class MyUsers
    {
        #region "Variáveis Privadas"        
        private string _NOMUSU = "";
        #endregion "Variáveis Privadas"
        /// <summary>
        /// Código do Usuário
        /// </summary>

        public int CODUSU { get; set; } = 0;

        /// <summary>
        /// Nome do usuário
        /// </summary>

        public string NOMUSU
        {
            get { return _NOMUSU; }
            set { _NOMUSU = value.ToUpper().NoAccents(); }
        }

        /// <summary>
        /// 
        /// </summary>

        public int? REFCOD { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>

        public int? REFSEL { get; set; } = 0;

        public int USUPRO { get; set; } = 0;
        public int CODCRT { get; set; } = 0;
        public MyUsers() { }

        public MyUsers(int pCODUSU, string pNOMUSU)
        {
            this.CODUSU = pCODUSU;
            this.NOMUSU = pNOMUSU;
        }

    }
}
