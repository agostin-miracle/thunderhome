using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBREGMOV Alias Tabela de Registro de Transferências
///</summary>

    public class TransferRegistration
    {
                #region "Variáveis Privadas"
        private string _DSCLCT="";
         #endregion "Variáveis Privadas"
        /// <summary>
        /// 
        /// </summary>
        public int NIDTRF{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public byte SUBSYS{ get;set;} = 1;

        /// <summary>
        /// Tipo de Lancamento
        /// </summary>
        /// <remarks>
/// <para>Tabela de Tipos de Lançamento</para>
/// </remarks>
        public short TIPLCT{ get;set;} = 0;

        /// <summary>
        /// Código do Status do Movimento do Registro
        /// </summary>
        /// <remarks>
/// <para>Se status igual a 1,o registro estará habilitado para registro financeiro</para>
/// </remarks>
        public short STAMOV{ get;set;} = 0;

        /// <summary>
        /// Data de Movimento
        /// </summary>
        public DateTime DATMOV{ get;set;} = DateTime.Now;

        /// <summary>
        /// Código do Tipo de Transação de Movimento
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 59</para>
/// </remarks>
        public byte CODTRM{ get;set;} = 1;

        /// <summary>
        /// Codigo da Moeda
        /// </summary>
        public int CODMOE{ get;set;} = 1;

        /// <summary>
        /// Código do Usuário Gestor
        /// </summary>
        /// <remarks>
/// <para>O usuário gestor é determinado pela associação do usuário com o produto</para>
/// </remarks>
        public int USUPRO{ get;set;} = 0;

        /// <summary>
        /// Código do Usuário
        /// </summary>
        public int CODUSU{ get;set;} = 0;

        /// <summary>
        /// Código do Cartão 
        /// </summary>
        /// <remarks>
/// <para>Tabela de Próximos Números : 5</para>
/// </remarks>
        public int CODCRT{ get;set;} = 0;

        /// <summary>
        /// Código de Resposta
        /// </summary>
        /// <remarks>
/// <para>Códigos de Resposta de processamento de transacoes via ISO-8583</para>
/// </remarks>
        public string CODRSP{ get;set;} = "00";

        /// <summary>
        /// Usuário de Débito
        /// </summary>
        public int USRDEB{ get;set;} = 0;

        /// <summary>
        /// Usuário Credito
        /// </summary>
        public int USRCRD{ get;set;} = 0;

        /// <summary>
        /// Usuário de Débito
        /// </summary>
        /// <remarks>
/// <para>Obtido através de checagem de lançamento</para>
/// </remarks>
        public short USUDEB{ get;set;} = 0;

        /// <summary>
        /// Indicador de Débito
        /// </summary>
        /// <remarks>
/// <para>Tabela de Indicador de Lançamentos</para>
/// </remarks>
        public short INDDEB{ get;set;} = 0;

        /// <summary>
        /// Associação de Origem do Débito
        /// </summary>
        public byte ORGDEB{ get;set;} = 0;

        /// <summary>
        /// Conta Débito
        /// </summary>
        public int CTADEB{ get;set;} = 0;

        /// <summary>
        /// Usuário de Crédito
        /// </summary>
        /// <remarks>
/// <para>Obtido através de checagem de lançamento</para>
/// </remarks>
        public int USUCRD{ get;set;} = 0;

        /// <summary>
        /// Indicador de Crédito
        /// </summary>
        /// <remarks>
/// <para>Tabela de Indicador de Lançamentos</para>
/// </remarks>
        public short INDCRD{ get;set;} = 0;

        /// <summary>
        /// Associação de Origem do Crédito
        /// </summary>
        public byte ORGCRD{ get;set;} = 0;

        /// <summary>
        /// Conta Crédito
        /// </summary>
        public int CTACRD{ get;set;} = 0;

        /// <summary>
        /// Valor do Movimento
        /// </summary>
        public double VLRMOV{ get;set;} = 0;

        /// <summary>
        /// Omitir Verificação de Saldo
        /// </summary>
        public string OMTSLD{ get;set;} = "S";

        /// <summary>
        /// Omitir aplicação de tarifação
        /// </summary>
        public string OMTTAR{ get;set;} = "S";

        /// <summary>
        /// 
        /// </summary>
        public int NIDCAL{ get;set;} = 0;

        /// <summary>
        /// Valor da Tarifa
        /// </summary>
        public double VLRTAR{ get;set;} = 0;

        /// <summary>
        /// Descrição do Erro
        /// </summary>
        public string DSCERR{ get;set;} = "";

        /// <summary>
        /// Data da Apuração do Saldo
        /// </summary>
        public DateTime? SLDDAT{ get;set;} = null;

        /// <summary>
        /// Valor do Saldo Obtido
        /// </summary>
        public double SLDVAL{ get;set;} = 0;

        /// <summary>
        /// Número da Transação Financeira
        /// </summary>
        public string NIDTRA{ get;set;} = "";

        /// <summary>
        /// Código da Transação Cancelada
        /// </summary>
        public string CANTRA{ get;set;} = "";

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
        /// 
        /// </summary>
        public string NUMDEB{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public string NUMCRD{ get;set;} = "";

        /// <summary>
        /// Descrição do Status de Registro
        /// </summary>
        public string DSCREC{ get;set;} = "";

        /// <summary>
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        public string LGNUSU{ get;set;} = "";

        /// <summary>
        /// Descrição do Status
        /// </summary>
        public string DSCSTA{ get;set;} = "";

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

    }
}
