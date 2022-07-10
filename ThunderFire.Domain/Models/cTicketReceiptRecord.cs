using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBRBBBOL Alias Ticket Receipt Record
///</summary>

    public class TicketReceiptRecord
    {
               /// <summary>
        /// ID do Registro de Baixa de Boleto
        /// </summary>
        public int NIDRBB{ get;set;} = 0;

        /// <summary>
        /// ID do Lote de Importação
        /// </summary>
        public int NIDLIM{ get;set;} = 0;

        /// <summary>
        /// Tipo de Baixa
        /// </summary>
        /// <remarks>
/// <para>0 - Baixa de Registro Bancário</para>
/// <para>1 - Baixa de Registro de Boleto</para>
/// </remarks>
        public byte TIPBXA{ get;set;} = 1;

        /// <summary>
        /// Data de Pagamento
        /// </summary>
        public DateTime DATPGT{ get;set;} = DateTime.Now;

        /// <summary>
        /// Nome do Arquivo de origem
        /// </summary>
        public string FILNAM{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public short NUMROW{ get;set;} = 0;

        /// <summary>
        /// Código de usuário associado ao banco
        /// </summary>
        public int USUBCO{ get;set;} = 0;

        /// <summary>
        /// Referência Externa
        /// </summary>
        public string NUMREX{ get;set;} = "";

        /// <summary>
        /// Valor total dos Pagamentos
        /// </summary>
        public double TOTPAG{ get;set;} = 0;

        /// <summary>
        /// Valor total dos Juros
        /// </summary>
        public double TOTJUR{ get;set;} = 0;

        /// <summary>
        /// Valor total dos Descontos
        /// </summary>
        public double TOTDES{ get;set;} = 0;

        /// <summary>
        /// Valor total das Tarifas
        /// </summary>
        public double TOTTAR{ get;set;} = 0;

        /// <summary>
        /// Valor total das Tarifas Externas
        /// </summary>
        public double TOTTEX{ get;set;} = 0;

        /// <summary>
        /// Valor total das Tarifas por decurso de prazo
        /// </summary>
        public double TOTDCP{ get;set;} = 0;

        /// <summary>
        /// Valor total líquido reebido
        /// </summary>
        public double TOTLIQ{ get;set;} = 0;

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
        /// ID do Boleto
        /// </summary>
        public int NIDBOL{ get;set;} = 0;

    }
}
