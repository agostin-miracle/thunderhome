using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBREGOPE Alias Registration of Operations
///</summary>

    public class OperationsRegister
    {
                #region "Variáveis Privadas"
        private string _DSCMOV="";
         #endregion "Variáveis Privadas"
        /// <summary>
        /// ID de Registro de Operações
        /// </summary>
        public int NIDOPE{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public byte SUBSYS{ get;set;} = 1;

        /// <summary>
        /// ID da Referência Externa associada a um subsistema
        /// </summary>
        /// <remarks>
/// <para>Subsistema</para>
/// <para>1 - Mensalidades</para>
/// <para>2 - Boletos</para>
/// <para>3 - Conta Corrente</para>
/// </remarks>
        public int NIDREF{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public int NIDCAL{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public int IDPROC{ get;set;} = 0;

        /// <summary>
        /// Data da operação
        /// </summary>
        public DateTime DATOPE{ get;set;} = DateTime.Now;

        /// <summary>
        /// Código de Movimento
        /// </summary>
        public short CODMOV{ get;set;} = 0;

        /// <summary>
        /// Valor Base da Operação
        /// </summary>
        public double VLRBAS{ get;set;} = 0;

        /// <summary>
        /// Valor do Percentual
        /// </summary>
        public double VLRPCT{ get;set;} = 0;

        /// <summary>
        /// Sinal da Operação
        /// </summary>
        /// <remarks>
/// <para>Tabela geral 10</para>
/// </remarks>
        public short SIGOPE{ get;set;} = 0;

        /// <summary>
        /// Se 1, indica que o valor é principal
        /// </summary>
        public byte VLRPCP{ get;set;} = 0;

        /// <summary>
        /// Valor da Operação
        /// </summary>
        public double VLROPE{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public short USELCT{ get;set;} = 0;

        /// <summary>
        /// Número do Lote de Registro Financeiro
        /// </summary>
        public int LOTFIN{ get;set;} = 0;

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
        /// Descrição do Movimento
        /// </summary>
        public string DSCMOV
        { 
            get { return _DSCMOV;}
            set { if(!String.IsNullOrWhiteSpace(value))
_DSCMOV= value .ToUpper().NoAccents();
else
_DSCMOV= "";
}

        }

        /// <summary>
        /// 
        /// </summary>
        public string DSCOPE{ get;set;} = "";

        /// <summary>
        /// Descrição do Status de Registro
        /// </summary>
        public string DSCREC{ get;set;} = "";

        /// <summary>
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        public string LGNUSU{ get;set;} = "";

        /// <summary>
        /// Valor Total
        /// </summary>
        public double VLRTOT{ get;set;} = 0;

    }
}
