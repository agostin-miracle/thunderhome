using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBTARMOV Alias Associação de Tarifas e Operações
///</summary>

    public class TariffOperations
    {
                #region "Variáveis Privadas"
        private string _DSCTAR="";
        private string _DSCMOV="";
         #endregion "Variáveis Privadas"
        /// <summary>
        /// 
        /// </summary>
        public int NIDTXM{ get;set;} = 0;

        /// <summary>
        /// Código da Tarifa
        /// </summary>
        public short CODTAR{ get;set;} = 0;

        /// <summary>
        /// Código de Movimento
        /// </summary>
        public short CODMOV{ get;set;} = 0;

        /// <summary>
        /// Indicador de Precedência
        /// </summary>
        public short IDEPRE{ get;set;} = 0;

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
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        public string LGNUSU{ get;set;} = "";

        /// <summary>
        /// Descrição da Tarifa
        /// </summary>
        public string DSCTAR
        { 
            get { return _DSCTAR;}
            set { if(!String.IsNullOrWhiteSpace(value))
_DSCTAR= value .ToUpper().NoAccents();
else
_DSCTAR= "";
}

        }

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

    }
}
