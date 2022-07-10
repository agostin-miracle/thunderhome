using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBCADPRO Alias Products
///</summary>

    public class Product
    {
                #region "Variáveis Privadas"
        private string _DSCLIN="";
         #endregion "Variáveis Privadas"
        /// <summary>
        /// Codigo do Produto
        /// </summary>
        public short CODPRO{ get;set;} = 0;

        /// <summary>
        /// Descrição do Produto
        /// </summary>
        public string DSCPRO{ get;set;} = "";

        /// <summary>
        /// Código da Linha de Produto
        /// </summary>
        public short LINPRO{ get;set;} = 0;

        /// <summary>
        /// Nome Fantasia
        /// </summary>
        public string NOMFAN{ get;set;} = "";

        /// <summary>
        /// Define se o produto deverá sofrer processo de ativação cadastral do portador
        /// </summary>
        public bool ATVCDT{ get;set;} = false;

        /// <summary>
        /// Define se o produto deverá sofrer processo de geração de senha de acesso
        /// </summary>
        public bool ATVGPA{ get;set;} = false;

        /// <summary>
        /// Indicador de Marcação de Produto Benefício
        /// </summary>
        public bool INDBNF{ get;set;} = false;

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
        /// Descrição do Status de Registro
        /// </summary>
        public string DSCREC{ get;set;} = "";

        /// <summary>
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        public string LGNUSU{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public string DSCCDT{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public string DSCGPA{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public string DSCBNF{ get;set;} = "";

    }
}
