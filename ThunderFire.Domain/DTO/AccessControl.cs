using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderFire.Domain.Models;

namespace ThunderFire.Domain.DTO
{
    /// <summary>
    /// Controle de Acesso
    /// </summary>
    public class AccessControl
    {
        /// <summary>
        /// Retorna true se o objeto foi bem carregado
        /// </summary>
        public bool IsValid { get; set; } = false;

        /// <summary>
        /// Verifica se é necessário resetar a senha de acesso
        /// </summary>
        /// <returns></returns>
        public bool ResetPassWord { get; set; }

        /// <summary>
        /// Define se o usuário e POS
        /// </summary>
        public bool IsPointOfSale { get; set; } = false;

        /// <summary>
        /// Retorna true, se o usuário corrente é aprovador de TED
        /// </summary>
        public bool IsTedApproval { get; set; } = false;

        /// <summary>
        /// Menu do Usuário
        /// </summary>
        public string Menu { get; internal set; } = "";

        /// <summary>
        /// Lista de Menus associados ao usuario
        /// </summary>
        public List<string> CODMNU { get; set; } = new List<string>();


        /// <summary>
        /// Id do Registro de Acesso
        /// </summary>
        public int LGNNUM { get; set; } = 0;

        public VirtualAccount Account { get; set; }

        public List<ActiveCards> Cards { get; set; }

        public GeneralRegistry User { get; set; }

        public AddressBook Address { get; set; } 

        public String Mail { get; set; }
        public String Mobile { get; set; }



        /// <summary>
        /// Retorna true se o usuário é associado a um gestor
        /// </summary>
        public bool IsManagerProduct { get; set; } = false;

        /// <summary>
        /// Retorna true se o menu correspondente está logado para o usuário
        /// </summary>
        /// <param name="pCODMNU">Código do Menu</param>
        /// <returns>true, se estiver logado</returns>
        public bool IsLogged(string pCODMNU)
        {
            bool found = false;
            foreach (string item in CODMNU)
            {
                if (item == pCODMNU)
                {
                    found = true;
                    break;
                }
            }
            return found;
        }


        public AccessControl()
        {
            this.Cards = new List<ActiveCards>();
        }
    }
}
