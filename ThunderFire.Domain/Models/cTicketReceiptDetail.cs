using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBBXABOL Alias Registro de Detalhe do Recebimento de Boleto
///</summary>

    public class TicketReceiptDetail
    {
               /// <summary>
        /// 
        /// </summary>
        public int NIDRBD{ get;set;} = 0;

        /// <summary>
        /// ID do Registro de Baixa de Boleto
        /// </summary>
        public int NIDRBB{ get;set;} = 0;

        /// <summary>
        /// ID do Boleto
        /// </summary>
        public int NIDBOL{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public short IDXSEQ{ get;set;} = 0;

        /// <summary>
        /// Tipo de Baixa
        /// </summary>
        /// <remarks>
/// <para>0 - Baixa de Registro Bancário</para>
/// <para>1 - Baixa de Registro de Boleto</para>
/// </remarks>
        public byte TIPBXA{ get;set;} = 0;

        /// <summary>
        /// Data de Pagamento
        /// </summary>
        public DateTime DATPGT{ get;set;} = DateTime.Now;

        /// <summary>
        /// Número da Parcela
        /// </summary>
        public short NUMPCL{ get;set;} = 1;

        /// <summary>
        /// Valor do Pagamento
        /// </summary>
        public double VLRPAG{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public double VLRMOR{ get;set;} = 0;

        /// <summary>
        /// Valor dos Juros
        /// </summary>
        public double VLRJUR{ get;set;} = 0;

        /// <summary>
        /// Valor do desconto
        /// </summary>
        public double VLRDES{ get;set;} = 0;

        /// <summary>
        /// Valor líquido
        /// </summary>
        public double VLRLIQ{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public double VLRTEX{ get;set;} = 0;

        /// <summary>
        /// Observações
        /// </summary>
        public string DSCOBS{ get;set;} = "";

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
}
