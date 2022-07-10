using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBCADLCT Alias Tabela de Cadastro de Lançamentos
///</summary>

    public class AccountEntryPosting
    {
                #region "Variáveis Privadas"
        private string _DSCLCT="";
        private string _DSCIDB="";
         #endregion "Variáveis Privadas"
        /// <summary>
        /// ID do Lançamento
        /// </summary>
        public int NIDLCT{ get;set;} = 0;

        /// <summary>
        /// Tipo de Lancamento
        /// </summary>
        /// <remarks>
/// <para>Tabela de Tipos de Lançamento</para>
/// </remarks>
        public short TIPLCT{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public short LINLCT{ get;set;} = 0;

        /// <summary>
        /// Código do Tipo de Transação de Movimento
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 59</para>
/// </remarks>
        public byte CODTRM{ get;set;} = 1;

        /// <summary>
        /// 
        /// </summary>
        public short NUMORD{ get;set;} = 0;

        /// <summary>
        /// Indicador de Débito
        /// </summary>
        /// <remarks>
/// <para>Tabela de Indicador de Lançamentos</para>
/// </remarks>
        public int INDDEB{ get;set;} = 0;

        /// <summary>
        /// Indicador de Crédito
        /// </summary>
        /// <remarks>
/// <para>Tabela de Indicador de Lançamentos</para>
/// </remarks>
        public int INDCRD{ get;set;} = 0;

        /// <summary>
        /// Código da Conta Débito
        /// </summary>
        public int MOVDEB{ get;set;} = 0;

        /// <summary>
        /// Código da Conta Crédito
        /// </summary>
        public int MOVCRD{ get;set;} = 0;

        /// <summary>
        /// Código do Status da Transação Registro
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 70</para>
/// </remarks>
        public int STAREG{ get;set;} = 0;

        /// <summary>
        /// Indicador de Precedência
        /// </summary>
        public byte IDEPRE{ get;set;} = 0;

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

        /// <summary>
        /// Descrição do Tipo de Lançamento
        /// </summary>
        public string DSCLCT
        { 
            get { return _DSCLCT;}
            set { if(!String.IsNullOrWhiteSpace(value))
_DSCLCT= value .ToUpper().NoAccents();
else
_DSCLCT= "";
}

        }

        /// <summary>
        /// Indicador de Lançamento Débito
        /// </summary>
        public string DSCIDB
        { 
            get { return _DSCIDB;}
            set { if(!String.IsNullOrWhiteSpace(value))
_DSCIDB= value .ToUpper().NoAccents();
else
_DSCIDB= "";
}

        }

        /// <summary>
        /// 
        /// </summary>
        public string DSCICR{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public string DSCMDB{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public string DSCMCR{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public string DSCTRM{ get;set;} = "";

        /// <summary>
        /// Descrição do Status
        /// </summary>
        public string DSCSTA{ get;set;} = "";

    }
}
