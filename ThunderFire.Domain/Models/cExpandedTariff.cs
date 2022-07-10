using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBEXPTAR Alias Expanded Tariff
///</summary>

    public class ExpandedTariff
    {
               /// <summary>
        /// ID de registro de Código da Expansão de Tarifa
        /// </summary>
        public int NIDEXP{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public short TIPEXP{ get;set;} = 0;

        /// <summary>
        /// Nível de Aplicação
        /// </summary>
        /// <remarks>
/// <para>Sequencia de processamento</para>
/// </remarks>
        public byte LVLAPL{ get;set;} = 1;

        /// <summary>
        /// Valor da Tarifa
        /// </summary>
        public double VLRTAR{ get;set;} = 0;

        /// <summary>
        /// Valor Minimo
        /// </summary>
        public double VLRMIN{ get;set;} = 0;

        /// <summary>
        /// Valor Máximo
        /// </summary>
        public double VLRMAX{ get;set;} = 0;

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
        /// Descrição da Expansão de Tarifa
        /// </summary>
        public string DSCEXP{ get;set;} = "";

        /// <summary>
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        public string LGNUSU{ get;set;} = "";

        /// <summary>
        /// Descrição do Status de Registro
        /// </summary>
        public string DSCREC{ get;set;} = "";

    }
}
