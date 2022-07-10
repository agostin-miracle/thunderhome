using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBREGMEN Alias Monthly Payment
///</summary>

    public class MonthlyPayment
    {
               /// <summary>
        /// ID do Registro de Mensalidade
        /// </summary>
        public int NIDMEN{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public byte SUBSYS{ get;set;} = 1;

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
        /// Usuário de Origem do Débito, utilizado para identificar o usuário que transferiu a mensalidade para outro pagador
        /// </summary>
        public int USRREF{ get;set;} = 0;

        /// <summary>
        /// Tipo de Mensalidade
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 50</para>
/// <para>1	CARTAO</para>
/// <para>2	MAQUINA POS</para>
/// <para>3	INATIVIDADE</para>
/// <para>4	REGISTRO DE ATIVACAO</para>
/// <para>5	CARTAO - TOUCH</para>
/// <para>6	REPASSE DE INTERMEDIACAO BANCARIA</para>
/// <para>7	EXPEDICAO DE DOCUMENTOS</para>
/// </remarks>
        public byte TIPMEN{ get;set;} = 1;

        /// <summary>
        /// Código de Referência
        /// </summary>
        /// <remarks>
/// <para>Para a tabela de Mensalides (TBREGMEN ) corresponde ao código de identificação do cartão associada ao tipo de mensalidade igual a 1</para>
/// </remarks>
        public int CODREF{ get;set;} = 0;

        /// <summary>
        /// Modo de Registro
        /// </summary>
        /// <remarks>
/// <para>define:</para>
/// <para></para>
/// <para>Se 1, registro de provisão,</para>
/// <para>2 -Registro de Baixa</para>
/// </remarks>
        public byte MODREG{ get;set;} = 1;

        /// <summary>
        /// Identificar de Processsamento de Baixa de um Boleto
        /// </summary>
        /// <remarks>
/// <para>Registra o número de tentativas de baixa de um determinado boleto</para>
/// </remarks>
        public byte REGBXA{ get;set;} = 0;

        /// <summary>
        /// Código do Status da Mensalidade
        /// </summary>
        public short STAMEN{ get;set;} = 0;

        /// <summary>
        /// Origem do Cancelamento
        /// </summary>
        /// <remarks>
/// <para>0 - Normal</para>
/// <para>1 - Cancelamento do usuário</para>
/// <para>2-Cancelado pelo sistema</para>
/// </remarks>
        public byte SRCCAN{ get;set;} = 0;

        /// <summary>
        /// MÊS
        /// </summary>
        public byte NUMMES{ get;set;} = 0;

        /// <summary>
        /// ANO
        /// </summary>
        public short NUMANO{ get;set;} = 0;

        /// <summary>
        /// Número da Parcela
        /// </summary>
        public short NUMPCL{ get;set;} = 0;

        /// <summary>
        /// Data da Mensalidade
        /// </summary>
        public DateTime DATMEN{ get;set;} = DateTime.Now;

        /// <summary>
        /// Data de Vencimento
        /// </summary>
        public DateTime DATVCT{ get;set;} = DateTime.Now;

        /// <summary>
        /// Sinal da Operação
        /// </summary>
        /// <remarks>
/// <para>Tabela geral 10</para>
/// </remarks>
        public short SIGOPE{ get;set;} = 1;

        /// <summary>
        /// Valor de Cobrança
        /// </summary>
        public double VLRCOB{ get;set;} = 0;

        /// <summary>
        /// Número do Lote de Inscrição
        /// </summary>
        public int LOTINS{ get;set;} = 0;

        /// <summary>
        /// Numero de Tentativas de Registro
        /// </summary>
        public int NUMTEN{ get;set;} = 0;

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

    }
        /// <summary>
        /// Objeto de Retenção de Informações sobre Mensalidades Abertas
        /// </summary>
public  class QueryMonthlyPayment
{
 #region "Variáveis Privadas"        
private string _NOMUSU="";
 #endregion "Variáveis Privadas"
        /// <summary>
        /// ID do Registro de Mensalidade
        /// </summary>
        
        public int NIDMEN{ get;set;} = 0;

        /// <summary>
        /// Código do Usuário Gestor
        /// </summary>
        /// <remarks>
/// <para>O usuário gestor é determinado pela associação do usuário com o produto</para>
/// </remarks>
        
        public int USUPRO{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        
        public string NOMGST{get;set;} = "";

        /// <summary>
        /// Código do Usuário
        /// </summary>
        
        public int CODUSU{ get;set;} = 0;

        /// <summary>
        /// Nome do usuário
        /// </summary>
        
        public string NOMUSU
        { 
            get { return _NOMUSU;}
            set { _NOMUSU= value.ToUpper().NoAccents();}
        }

        /// <summary>
        /// Usuário de Origem do Débito, utilizado para identificar o usuário que transferiu a mensalidade para outro pagador
        /// </summary>
        
        public int USRREF{ get;set;} = 0;

        /// <summary>
        /// Tipo de Mensalidade
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 50</para>
/// <para>1	CARTAO</para>
/// <para>2	MAQUINA POS</para>
/// <para>3	INATIVIDADE</para>
/// <para>4	REGISTRO DE ATIVACAO</para>
/// <para>5	CARTAO - TOUCH</para>
/// <para>6	REPASSE DE INTERMEDIACAO BANCARIA</para>
/// <para>7	EXPEDICAO DE DOCUMENTOS</para>
/// </remarks>
        
        public byte TIPMEN{ get;set;} = 1;

        /// <summary>
        /// 
        /// </summary>
        
        public string DSCMEN{get;set;} = "";

        /// <summary>
        /// Código de Referência
        /// </summary>
        /// <remarks>
/// <para>Para a tabela de Mensalides (TBREGMEN ) corresponde ao código de identificação do cartão associada ao tipo de mensalidade igual a 1</para>
/// </remarks>
        
        public int CODREF{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        
        public string REFMEN{get;set;} = "";

        /// <summary>
        /// Modo de Registro
        /// </summary>
        /// <remarks>
/// <para>define:</para>
/// <para></para>
/// <para>Se 1, registro de provisão,</para>
/// <para>2 -Registro de Baixa</para>
/// </remarks>
        
        public byte MODREG{ get;set;} = 1;

        /// <summary>
        /// 
        /// </summary>
        
        public string DSCMOD{get;set;} = "";

        /// <summary>
        /// Identificar de Processsamento de Baixa de um Boleto
        /// </summary>
        /// <remarks>
/// <para>Registra o número de tentativas de baixa de um determinado boleto</para>
/// </remarks>
        
        public byte REGBXA{ get;set;} = 0;

        /// <summary>
        /// Código do Status da Mensalidade
        /// </summary>
        
        public short STAMEN{ get;set;} = 0;

        /// <summary>
        /// Descrição do Status
        /// </summary>
        
        public string DSCSTA{get;set;} = "";

        /// <summary>
        /// MÊS
        /// </summary>
        
        public byte NUMMES{ get;set;} = 0;

        /// <summary>
        /// ANO
        /// </summary>
        
        public short NUMANO{ get;set;} = 0;

        /// <summary>
        /// Número da Parcela
        /// </summary>
        
        public short NUMPCL{ get;set;} = 0;

        /// <summary>
        /// Data da Mensalidade
        /// </summary>
        
        public DateTime DATMEN{ get;set;} = DateTime.Now;

        /// <summary>
        /// Data de Vencimento
        /// </summary>
        
        public DateTime DATVCT{ get;set;} = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        
        public string CNVMEN{get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        
        public string CNVVCT{get;set;} = "";

        /// <summary>
        /// Sinal da Operação
        /// </summary>
        /// <remarks>
/// <para>Tabela geral 10</para>
/// </remarks>
        
        public short SIGOPE{ get;set;} = 1;

        /// <summary>
        /// Valor de Cobrança
        /// </summary>
        
        public double VLRCOB{ get;set;} = 0;

        /// <summary>
        /// Valor do Movimento
        /// </summary>
        
        public double? VLRMOV{ get;set;} = 0;

        /// <summary>
        /// Número do Lote de Inscrição
        /// </summary>
        
        public int LOTINS{ get;set;} = 0;

        /// <summary>
        /// Numero de Tentativas de Registro
        /// </summary>
        
        public int NUMTEN{ get;set;} = 0;

        /// <summary>
        /// Código do Status de Registro
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 07</para>
/// </remarks>
        
        public byte STAREC{ get;set;} = 1;

        /// <summary>
        /// Descrição do Status de Registro
        /// </summary>
        
        public string DSCREC{get;set;} = "";

        /// <summary>
        /// Data de Inclusão ou cadastramento
        /// </summary>
        
        public string DATCAD{get;set;} = "";

        /// <summary>
        /// Data da Ultima Atualização
        /// </summary>
        
        public string DATUPD{get;set;} = "";

        /// <summary>
        /// Usuário de Atualização
        /// </summary>
        
        public int UPDUSU{ get;set;} = 0;

        /// <summary>
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        
        public string LGNUSU{get;set;} = "";

}
}
