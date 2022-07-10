using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBLINPRO Alias Linha de Produto
///</summary>

    public class ProductLine
    {
                #region "Variáveis Privadas"
        private string _DSCLIN="";
         #endregion "Variáveis Privadas"
        /// <summary>
        /// Código da Linha de Produto
        /// </summary>
        public short LINPRO{ get;set;} = 0;

        /// <summary>
        /// Descrição da Linha de Produto
        /// </summary>
        public string DSCLIN
        { 
            get { return _DSCLIN;}
            set { if(!String.IsNullOrWhiteSpace(value))
_DSCLIN= value .ToUpper().NoAccents();
else
_DSCLIN= "";
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
