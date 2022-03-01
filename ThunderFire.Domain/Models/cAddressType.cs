using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBTIPEND Alias Address Type
///</summary>

    public class AddressType
    {
                #region "Variáveis Privadas"
        private string _DSCTEN="";
         #endregion "Variáveis Privadas"
        /// <summary>
        /// Tipo de Endereço
        /// </summary>
        public byte TIPEND{ get;set;} = 1;

        /// <summary>
        /// Descrição do Tipo de Endereço
        /// </summary>
        public string DSCTEN
        { 
            get { return _DSCTEN;}
            set { if(!String.IsNullOrWhiteSpace(value))
_DSCTEN= value .ToUpper().NoAccents();
else
_DSCTEN= "";
}

        }

        /// <summary>
        /// Define se o tipo de endereço tem referencia com o Cadastro de Contatos
        /// </summary>
        public bool REFCTO{ get;set;} = false;

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
