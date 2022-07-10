using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBTIPLCT Alias Account Entry Type
///</summary>

    public class AccountEntryType
    {
                #region "Variáveis Privadas"
        private string _DSCLCT="";
        private string _DSCIBS="";
        private string _DSCIDB="";
        private string _DSCTAR="";
        private string _DSCADB="";
        private string _DSCACR="";
         #endregion "Variáveis Privadas"
        /// <summary>
        /// Tipo de Lancamento
        /// </summary>
        /// <remarks>
/// <para>Tabela de Tipos de Lançamento</para>
/// </remarks>
        public short TIPLCT{ get;set;} = 0;

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

        /// <summary>
        /// Indicador de Lançamento
        /// </summary>
        /// <remarks>
/// <para>Tabela de Indicador de Lançamentos</para>
/// </remarks>
        public short INDLCT{ get;set;} = 0;

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
        /// Código da Tarifa
        /// </summary>
        public short CODTAR{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public byte USETRF{ get;set;} = 1;

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
        /// Indicador de Lançamento Base
        /// </summary>
        public string DSCIBS
        { 
            get { return _DSCIBS;}
            set { if(!String.IsNullOrWhiteSpace(value))
_DSCIBS= value .ToUpper().NoAccents();
else
_DSCIBS= "";
}

        }

        /// <summary>
        /// Indicador de Lançamento Débito
        /// </summary>
        public string DSCIDB
        { 
            get { return _DSCIDB;}
            set { if(!String.IsNullOrWhiteSpace(value))
_DSCIDB= value .ToUpper().NoAccents();
else
_DSCIDB= "";
}

        }

        /// <summary>
        /// 
        /// </summary>
        public string DSCICR{ get;set;} = "";

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
        /// Descrição da Associação de Origem do Débito
        /// </summary>
        public string DSCADB
        { 
            get { return _DSCADB;}
            set { if(!String.IsNullOrWhiteSpace(value))
_DSCADB= value .ToUpper().NoAccents();
else
_DSCADB= "";
}

        }

        /// <summary>
        /// Descrição da Associação de Origem do Crédito
        /// </summary>
        public string DSCACR
        { 
            get { return _DSCACR;}
            set { if(!String.IsNullOrWhiteSpace(value))
_DSCACR= value .ToUpper().NoAccents();
else
_DSCACR= "";
}

        }

    }
}
