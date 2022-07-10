using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBTIPMOV Alias Operations
///</summary>

    public class Operations
    {
                #region "Variáveis Privadas"
        private string _DSCMOV="";
         #endregion "Variáveis Privadas"
        /// <summary>
        /// Código de Movimento
        /// </summary>
        public short CODMOV{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public byte SUBSYS{ get;set;} = 1;

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
        /// Descrição Externa
        /// </summary>
        public string DSCEXT{ get;set;} = "";

        /// <summary>
        /// Sinal da Operação
        /// </summary>
        /// <remarks>
/// <para>Tabela geral 10</para>
/// </remarks>
        public short SIGOPE{ get;set;} = 0;

        /// <summary>
        /// Condição de Bloqueio de Operações
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 29</para>
/// </remarks>
        public byte CNDBLO{ get;set;} = 1;

        /// <summary>
        /// Número de Dias
        /// </summary>
        public short NUMDIA{ get;set;} = 0;

        /// <summary>
        /// Código de Estorno
        /// </summary>
        public short CODEST{ get;set;} = 0;

        /// <summary>
        /// Código de Cancelamento
        /// </summary>
        public short CODCAN{ get;set;} = 0;

        /// <summary>
        /// Descrição Externa do Cancelamento/Estorno
        /// </summary>
        public string DSCEXC{ get;set;} = "";

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
        /// Descrição do Bloqueio
        /// </summary>
        public string DSCBLO{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public string DSCOPE{ get;set;} = "";

        /// <summary>
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        public string LGNUSU{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public string DSCSYS{ get;set;} = "";

    }
}
