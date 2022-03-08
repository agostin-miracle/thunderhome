using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBCADSTA Alias Transaction Status
///</summary>

    public class TransactionStatus
    {
               /// <summary>
        /// Código do Status
        /// </summary>
        public short CODSTA{ get;set;} = 0;

        /// <summary>
        /// Descrição do Status
        /// </summary>
        public string DSCSTA{ get;set;} = "";

        /// <summary>
        /// Módulo de Operação
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 14</para>
/// </remarks>
        public byte CODMOD{ get;set;} = 1;

        /// <summary>
        /// Sinal da Operação
        /// </summary>
        public short SIGOPE{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public int NXTSTA{ get;set;} = 0;

        /// <summary>
        /// Define se o código de status de transação pode sofrer alterações externas
        /// </summary>
        public byte CANCHG{ get;set;} = 0;

        /// <summary>
        /// Define se o código de status de transação permite a exclusão de uma mensalidade
        /// </summary>
        public byte DELMEN{ get;set;} = 0;

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
public class QueryTransactionStatus
{
        /// <summary>
        /// Código do Status
        /// </summary>
        public short CODSTA{ get;set;}

        /// <summary>
        /// Descrição do Status
        /// </summary>
        public string DSCSTA{ get;set;} = "";

        /// <summary>
        /// Módulo de Operação
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 14</para>
/// </remarks>
        public byte CODMOD{ get;set;}

        /// <summary>
        /// Sinal da Operação
        /// </summary>
        public short SIGOPE{ get;set;}

        /// <summary>
        /// 
        /// </summary>
        public int NXTSTA{ get;set;}

        /// <summary>
        /// Define se o código de status de transação pode sofrer alterações externas
        /// </summary>
        public byte CANCHG{ get;set;}

        /// <summary>
        /// Define se o código de status de transação permite a exclusão de uma mensalidade
        /// </summary>
        public byte DELMEN{ get;set;}

        /// <summary>
        /// Código do Status de Registro
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 07</para>
/// </remarks>
        public byte STAREC{ get;set;}

        /// <summary>
        /// Data de Inclusão ou cadastramento
        /// </summary>
        public DateTime DATCAD{ get;set;}

        /// <summary>
        /// Data da Ultima Atualização
        /// </summary>
        public DateTime DATUPD{ get;set;}

        /// <summary>
        /// Usuário de Atualização
        /// </summary>
        public int UPDUSU{ get;set;}

        /// <summary>
        /// 
        /// </summary>
        public string DSCCHG{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public string DSCDEL{ get;set;} = "";

        /// <summary>
        /// Descrição do Status de Registro
        /// </summary>
        public int DSCREC{ get;set;}

        /// <summary>
        /// 
        /// </summary>
        public int DSCMOD{ get;set;}

        /// <summary>
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        public string LGNUSU{ get;set;} = "";

}}
