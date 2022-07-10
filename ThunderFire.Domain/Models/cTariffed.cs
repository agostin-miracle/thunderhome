using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
    ///<summary>
    /// Class of TBCALTAR Alias Memória de Cáluclo de Tarifas
    ///</summary>

    public class Tariffed
    {
        /// <summary>
        /// 
        /// </summary>
        public int NIDCAL { get; set; } = 0;

        /// <summary>
        /// Tipo de Referencia Externa
        /// </summary>
        /// <remarks>
        /// <para></para>
        /// <para>1 - Registro de Vendas</para>
        /// <para>2 - Operações com cartão de crédito</para>
        /// <para>3 - Registro de Inatividade</para>
        /// <para>4 - Mensalidade Máquina</para>
        /// <para>5 - Comissões</para>
        /// <para>6 - Baixa de Boleto</para>
        /// <para>11 - Registro de Transação Interna (Transferencias, Créditos, etc...)</para>
        /// <para>21 - Registro de Pagamento</para>
        /// </remarks>
        public byte TIPRFE { get; set; } = 0;

        /// <summary>
        /// ID da Referência Externa associada a um subsistema
        /// </summary>
        /// <remarks>
        /// <para>Subsistema</para>
        /// <para>1 - Mensalidades</para>
        /// <para>2 - Boletos</para>
        /// <para>3 - Conta Corrente</para>
        /// </remarks>
        public int NIDREF { get; set; } = 0;

        /// <summary>
        /// ID do Registro de Tarifação
        /// </summary>
        public int NIDTAR { get; set; } = 0;

        /// <summary>
        /// Código do Usuário
        /// </summary>
        /// <remarks>
        /// <para>Este campo acumula função dupla conforme o nivel de configuração do usuário</para>
        /// </remarks>
        public int USUCFG { get; set; } = 0;

        /// <summary>
        /// Id do Nível de Configuração do Usuário
        /// </summary>
        /// <remarks>
        /// <para></para>
        /// <para>0 - N/A</para>
        /// <para>1 - Gestor</para>
        /// <para>2 - Usuário</para>
        /// </remarks>
        public byte NIVCFG { get; set; } = 0;

        /// <summary>
        /// Código da Tarifa
        /// </summary>
        public int CODTAR { get; set; } = 0;

        /// <summary>
        /// Código da Expansão de Tarifa
        /// </summary>
        public int CODEXP { get; set; } = 0;

        /// <summary>
        /// Nível de Aplicação
        /// </summary>
        /// <remarks>
        /// <para>Sequencia de processamento</para>
        /// </remarks>
        public byte LVLAPL { get; set; } = 0;

        /// <summary>
        /// Valor Minimo
        /// </summary>
        public double VLRMIN { get; set; } = 0;

        /// <summary>
        /// Valor Máximo
        /// </summary>
        public double VLRMAX { get; set; } = 0;

        /// <summary>
        /// Valor Base da Operação
        /// </summary>
        public double VLRBAS { get; set; } = 0;

        /// <summary>
        /// Valor da Tarifa
        /// </summary>
        public double VLRTAR { get; set; } = 0;

        /// <summary>
        /// Valor calculado da tarifa
        /// </summary>
        public double EXTVLR { get; set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public byte? NUMDYS { get; set; } = 1;

        /// <summary>
        /// Data de Validação Inicial
        /// </summary>
        public DateTime? DATVL1 { get; set; } = null;

        /// <summary>
        /// Data de Validação Final
        /// </summary>
        public DateTime? DATVL2 { get; set; } = null;

        /// <summary>
        /// Código do Status de Registro
        /// </summary>
        /// <remarks>
        /// <para>Tabela Geral 07</para>
        /// </remarks>
        public byte STAREC { get; set; } = 1;

        /// <summary>
        /// Data de Inclusão ou cadastramento
        /// </summary>
        public DateTime DATCAD { get; set; } = DateTime.Now;

        /// <summary>
        /// Data da Ultima Atualização
        /// </summary>
        public DateTime DATUPD { get; set; } = DateTime.Now;

        /// <summary>
        /// Usuário de Atualização
        /// </summary>
        public int UPDUSU { get; set; } = 0;

    }
}
