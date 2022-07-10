using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBVALBOL Alias Registro de Valores de Boletos
///</summary>

    public class TicketValues
    {
               /// <summary>
        /// Id do Registro de Valores do Boleto
        /// </summary>
        public int NIDVBL{ get;set;} = 0;

        /// <summary>
        /// ID do Boleto
        /// </summary>
        public int NIDBOL{ get;set;} = 0;

        /// <summary>
        /// Tipo de Valor de Processamento
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 29</para>
/// <para>Tabela Geral 911 - TIPOS DE VALOR PARA BAIXA DE BOLETO - TBREGBXB</para>
/// </remarks>
        public byte TIPVAL{ get;set;} = 1;

        /// <summary>
        /// Valor Base
        /// </summary>
        public double VLRBAS{ get;set;} = 0;

        /// <summary>
        /// 
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
        /// Valor da Operação
        /// </summary>
        public double VLROPE{ get;set;} = 0;

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
        public string DSCVAL{ get;set;} = "";

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
        public string DSCOPE{ get;set;} = "";

    }
}
