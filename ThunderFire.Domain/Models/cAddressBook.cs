using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBCADEND Alias Cadasto de Endereços
///</summary>

    public class AddressBook
    {
                #region "Variáveis Privadas"
        private string _DSCEND="";
        private string _DSCCPL="";
        private string _DSCCID="";
        private string _DSCBAI="";
         #endregion "Variáveis Privadas"
        /// <summary>
        /// Código do Endereço
        /// </summary>
        public int CODEND{ get;set;} = 0;

        /// <summary>
        /// Registro Principal
        /// </summary>
        public byte REGATV{ get;set;} = 0;

        /// <summary>
        /// Código do Usuário
        /// </summary>
        public int CODUSU{ get;set;} = 0;

        /// <summary>
        /// Tipo de Endereço
        /// </summary>
        public byte TIPEND{ get;set;} = 1;

        /// <summary>
        /// Tipo de Logradouro
        /// </summary>
        /// <remarks>
/// <para>Tabela Gral 81</para>
/// </remarks>
        public short TIPLOG{ get;set;} = 0;

        /// <summary>
        /// UF
        /// </summary>
        public string CODUFE{ get;set;} = "  ";

        /// <summary>
        /// Endereço
        /// </summary>
        public string DSCEND
        { 
            get { return _DSCEND;}
            set { if(!String.IsNullOrWhiteSpace(value))
_DSCEND= value .ToUpper().NoAccents();
else
_DSCEND= "";
}

        }

        /// <summary>
        /// Complemento do Endereço
        /// </summary>
        public string DSCCPL
        { 
            get { return _DSCCPL;}
            set { if(!String.IsNullOrWhiteSpace(value))
_DSCCPL= value .ToUpper().NoAccents();
else
_DSCCPL= "";
}

        }

        /// <summary>
        /// Número do Endereço
        /// </summary>
        public int NUMEND{ get;set;} = 0;

        /// <summary>
        /// Cidade
        /// </summary>
        public string DSCCID
        { 
            get { return _DSCCID;}
            set { if(!String.IsNullOrWhiteSpace(value))
_DSCCID= value .ToUpper().NoAccents();
else
_DSCCID= "";
}

        }

        /// <summary>
        /// Bairro
        /// </summary>
        public string DSCBAI
        { 
            get { return _DSCBAI;}
            set { if(!String.IsNullOrWhiteSpace(value))
_DSCBAI= value .ToUpper().NoAccents();
else
_DSCBAI= "";
}

        }

        /// <summary>
        /// CEP
        /// </summary>
        public string CODCEP{ get;set;} = "00000000";

        /// <summary>
        /// Código do Pais
        /// </summary>
        public int CODPAI{ get;set;} = 55;

        /// <summary>
        /// Latitude
        /// </summary>
        public double? LATITU{ get;set;} = 0;

        /// <summary>
        /// Longitude
        /// </summary>
        public double? LONGIT{ get;set;} = 0;

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
public class QueryAddressBook
{
         #region "Variáveis Privadas"
        private string _NOMUSU="";
        private string _DSCTEN="";
        private string _DSCLOG="";
        private string _DSCEND="";
        private string _DSCCPL="";
        private string _DSCCID="";
        private string _DSCBAI="";
         #endregion "Variáveis Privadas"

        /// <summary>
        /// Código do Endereço
        /// </summary>
        public int CODEND{ get;set;}

        /// <summary>
        /// Registro Principal
        /// </summary>
        public byte REGATV{ get;set;}

        /// <summary>
        /// Descrição da Atividade
        /// </summary>
        public string DSCATV{ get;set;} = "";

        /// <summary>
        /// Código do Usuário
        /// </summary>
        public int CODUSU{ get;set;}

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string NOMUSU
        { 
            get { return _NOMUSU;}
            set { _NOMUSU= value.ToUpper().NoAccents();}
        }

        /// <summary>
        /// Tipo de Endereço
        /// </summary>
        public byte TIPEND{ get;set;}

        /// <summary>
        /// Descrição do Tipo de Endereço
        /// </summary>
        public string DSCTEN
        { 
            get { return _DSCTEN;}
            set { _DSCTEN= value.ToUpper().NoAccents();}
        }

        /// <summary>
        /// Define se o tipo de endereço tem referencia com o Cadastro de Contatos
        /// </summary>
        public bool REFCTO{ get;set;}

        /// <summary>
        /// Tipo de Logradouro
        /// </summary>
        /// <remarks>
/// <para>Tabela Gral 81</para>
/// </remarks>
        public short TIPLOG{ get;set;}

        /// <summary>
        /// Descrição do Logradouro
        /// </summary>
        public string DSCLOG
        { 
            get { return _DSCLOG;}
            set { _DSCLOG= value.ToUpper().NoAccents();}
        }

        /// <summary>
        /// UF
        /// </summary>
        public string CODUFE{ get;set;} = "  ";

        /// <summary>
        /// 
        /// </summary>
        public string DSCUFE{ get;set;} = "";

        /// <summary>
        /// Endereço
        /// </summary>
        public string DSCEND
        { 
            get { return _DSCEND;}
            set { _DSCEND= value.ToUpper().NoAccents();}
        }

        /// <summary>
        /// 
        /// </summary>
        public string FULEND{ get;set;} = "";

        /// <summary>
        /// Complemento do Endereço
        /// </summary>
        public string DSCCPL
        { 
            get { return _DSCCPL;}
            set { _DSCCPL= value.ToUpper().NoAccents();}
        }

        /// <summary>
        /// Número do Endereço
        /// </summary>
        public int NUMEND{ get;set;}

        /// <summary>
        /// Cidade
        /// </summary>
        public string DSCCID
        { 
            get { return _DSCCID;}
            set { _DSCCID= value.ToUpper().NoAccents();}
        }

        /// <summary>
        /// Bairro
        /// </summary>
        public string DSCBAI
        { 
            get { return _DSCBAI;}
            set { _DSCBAI= value.ToUpper().NoAccents();}
        }

        /// <summary>
        /// CEP
        /// </summary>
        public string CODCEP{ get;set;} = "00000000";

        /// <summary>
        /// Código do Pais
        /// </summary>
        public int CODPAI{ get;set;}

        /// <summary>
        /// País
        /// </summary>
        public string DSCPAI{ get;set;} = "";

        /// <summary>
        /// Latitude
        /// </summary>
        public double? LATITU{ get;set;}

        /// <summary>
        /// Longitude
        /// </summary>
        public double? LONGIT{ get;set;}

        /// <summary>
        /// Código do Status de Registro
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 07</para>
/// </remarks>
        public byte STAREC{ get;set;}

        /// <summary>
        /// Descrição do Status de Registro
        /// </summary>
        public string DSCREC{ get;set;} = "";

        /// <summary>
        /// Data de Inclusão ou cadastramento
        /// </summary>
        public string DATCAD{ get;set;} = "";

        /// <summary>
        /// Data da Ultima Atualização
        /// </summary>
        public string DATUPD{ get;set;} = "";

        /// <summary>
        /// Usuário de Atualização
        /// </summary>
        public int UPDUSU{ get;set;}

        /// <summary>
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        public string LGNUSU{ get;set;} = "";

}}
