using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBTIPCTA Alias Tipos de Conta
///</summary>

    public class AccountType
    {
                #region "Variáveis Privadas"
        private string _DSCCTA="";
        private string _TIPEXT="";
         #endregion "Variáveis Privadas"
        /// <summary>
        /// Tipo de Conta
        /// </summary>
        public byte TIPCTA{ get;set;} = 1;

        /// <summary>
        /// Descrição da Conta
        /// </summary>
        public string DSCCTA
        { 
            get { return _DSCCTA;}
            set { if(!String.IsNullOrWhiteSpace(value))
_DSCCTA= value .ToUpper().NoAccents();
else
_DSCCTA= "";
}

        }

        /// <summary>
        /// Tipo de Boleto Externo
        /// </summary>
        public string TIPEXT
        { 
            get { return _TIPEXT;}
            set { if(!String.IsNullOrWhiteSpace(value))
_TIPEXT= value .ToUpper().NoAccents();
else
_TIPEXT= "";
}

        }

        /// <summary>
        /// Código do Status de Registro
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 07</para>
/// </remarks>
        public byte STAREC{ get;set;} = 1;

        /// <summary>
        /// Data de Inclusão ou cadastramento
        /// </summary>
        public DateTime DATCAD{ get;set;} = DateTime.Now;

        /// <summary>
        /// Data da Ultima Atualização
        /// </summary>
        public DateTime DATUPD{ get;set;} = DateTime.Now;

        /// <summary>
        /// Usuário de Atualização
        /// </summary>
        public int UPDUSU{ get;set;} = 0;

        /// <summary>
        /// Descrição do Status de Registro
        /// </summary>
        public string DSCREC{ get;set;} = "";

        /// <summary>
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        public string LGNUSU{ get;set;} = "";

    }
}
