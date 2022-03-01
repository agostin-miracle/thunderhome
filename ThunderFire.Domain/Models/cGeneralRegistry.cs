using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBCADGER Alias Cadastro Geral
///</summary>

    public class GeneralRegistry
    {
                #region "Variáveis Privadas"
        private string _NOMUSU="";
        private string _NOMMAE="";
         #endregion "Variáveis Privadas"
        /// <summary>
        /// Código do Usuário
        /// </summary>
        public int CODUSU{ get;set;} = 0;

        /// <summary>
        /// Codigo do Atributo do Cadastro
        /// </summary>
        public short CODATR{ get;set;} = 0;

        /// <summary>
        /// Código do Usuário associado ou vinculado ao registro de usuário
        /// </summary>
        public int SRCUSU{ get;set;} = 0;

        /// <summary>
        /// Código do Nível de Confiança
        /// </summary>
        public byte NIVCFA{ get;set;} = 3;

        /// <summary>
        /// Código do Status do Usuário
        /// </summary>
        public short STAUSU{ get;set;} = 2;

        /// <summary>
        /// Tipo de Usuário
        /// </summary>
        /// <remarks>
/// <para>Os códigos disponívels estão contidas na Tabela TBTIPUSU</para>
/// </remarks>
        public byte TIPUSU{ get;set;} = 1;

        /// <summary>
        /// Gênero
        /// </summary>
        /// <remarks>
/// <para>M - Masculino</para>
/// <para>F - Feminino</para>
/// <para>I - Indeterminado</para>
/// </remarks>
        public string TIPPES{ get;set;} = "F";

        /// <summary>
        /// Personalidade Juridica
        /// </summary>
        /// <remarks>
/// <para>F - Pessoa Física,</para>
/// <para>J - Pessoa Jurídica,</para>
/// <para>I - Indeterminado</para>
/// </remarks>
        public string CODPJU{ get;set;} = "I";

        /// <summary>
        /// Número do RG para pessoa física/Inscrição Estadual para pessoa jurídica 
        /// </summary>
        public string NUMIRG{ get;set;} = "";

        /// <summary>
        /// CPF/CNPJ
        /// </summary>
        public string CODCMF{ get;set;} = "";

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string NOMUSU
        { 
            get { return _NOMUSU;}
            set { if(!String.IsNullOrWhiteSpace(value))
_NOMUSU= value .ToUpper().NoAccents();
else
_NOMUSU= "";
}

        }

        /// <summary>
        /// Nome da mãe
        /// </summary>
        public string NOMMAE
        { 
            get { return _NOMMAE;}
            set { if(!String.IsNullOrWhiteSpace(value))
_NOMMAE= value .ToUpper().NoAccents();
else
_NOMMAE= "";
}

        }

        /// <summary>
        /// Data de Nascimento
        /// </summary>
        /// <remarks>
/// <para>A data de nascimento pode ser nula na entrada</para>
/// </remarks>
        public DateTime? DATNAC{ get;set;} = null;

        /// <summary>
        /// Pessoa Políticamente Exposta
        /// </summary>
        /// <remarks>
/// <para>A pessoa politicamente exposta é uma pessoa que trabalha ou trabalhou nos últimos cinco anos, no Brasil ou no exterior, em cargos, empregos ou funções públicas. Pessoas que tenham familiares, cônjuges, representantes, parentes, relacionados direta ou indiretamente, com uma pessoa politicamente exposta também são assim considerados.</para>
/// </remarks>
        public bool ATRPPE{ get;set;} = false;

        /// <summary>
        /// Ocorrências de Registro do Cadastro
        /// </summary>
        public string DSCOCO{ get;set;} = "";

        /// <summary>
        /// Código de Nacionalidade
        /// </summary>
        /// <remarks>
/// <para>Identifica o país de origem, Brasil = 55, Tabela Gral 80</para>
/// </remarks>
        public short CODNAC{ get;set;} = 55;

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
public class QueryGeneralRegistry
{
         #region "Variáveis Privadas"
        private string _NOMUSU="";
        private string _NOMMAE="";
         #endregion "Variáveis Privadas"

        /// <summary>
        /// Código do Usuário
        /// </summary>
        public int CODUSU{ get;set;}

        /// <summary>
        /// Codigo do Atributo do Cadastro
        /// </summary>
        public short CODATR{ get;set;}

        /// <summary>
        /// 
        /// </summary>
        public string DSCATR{ get;set;} = "";

        /// <summary>
        /// Indica se o atributo deve logar no sistema
        /// </summary>
        /// <remarks>
/// <para>Em caso afirmativo, o sistema permitirá a manutenção da tabela de logins de usuários</para>
/// </remarks>
        public bool USELGN{ get;set;}

        /// <summary>
        /// Código do Usuário associado ou vinculado ao registro de usuário
        /// </summary>
        public int SRCUSU{ get;set;}

        /// <summary>
        /// 
        /// </summary>
        public string NOMGST{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public byte? CNTCTA{ get;set;}

        /// <summary>
        /// 
        /// </summary>
        public byte? CNTMAL{ get;set;}

        /// <summary>
        /// 
        /// </summary>
        public byte? CNTCEL{ get;set;}

        /// <summary>
        /// Código do Nível de Confiança
        /// </summary>
        public byte NIVCFA{ get;set;}

        /// <summary>
        /// Código do Status do Usuário
        /// </summary>
        public short STAUSU{ get;set;}

        /// <summary>
        /// Descrição do Status
        /// </summary>
        public string DSCSTA{ get;set;} = "";

        /// <summary>
        /// Tipo de Usuário
        /// </summary>
        /// <remarks>
/// <para>Os códigos disponívels estão contidas na Tabela TBTIPUSU</para>
/// </remarks>
        public byte TIPUSU{ get;set;}

        /// <summary>
        /// Descrição do Tipo de Usuário
        /// </summary>
        public string DSCTUS{ get;set;} = "";

        /// <summary>
        /// Gênero
        /// </summary>
        /// <remarks>
/// <para>M - Masculino</para>
/// <para>F - Feminino</para>
/// <para>I - Indeterminado</para>
/// </remarks>
        public string TIPPES{ get;set;} = "F";

        /// <summary>
        /// 
        /// </summary>
        public string DSCPES{ get;set;} = "";

        /// <summary>
        /// Personalidade Juridica
        /// </summary>
        /// <remarks>
/// <para>F - Pessoa Física,</para>
/// <para>J - Pessoa Jurídica,</para>
/// <para>I - Indeterminado</para>
/// </remarks>
        public string CODPJU{ get;set;} = "I";

        /// <summary>
        /// 
        /// </summary>
        public string DSCPJU{ get;set;} = "";

        /// <summary>
        /// Número do RG para pessoa física/Inscrição Estadual para pessoa jurídica 
        /// </summary>
        public string NUMIRG{ get;set;} = "";

        /// <summary>
        /// CPF/CNPJ
        /// </summary>
        public string CODCMF{ get;set;} = "";

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string NOMUSU
        { 
            get { return _NOMUSU;}
            set { _NOMUSU= value.ToUpper().NoAccents();}
        }

        /// <summary>
        /// Nome da mãe
        /// </summary>
        public string NOMMAE
        { 
            get { return _NOMMAE;}
            set { _NOMMAE= value.ToUpper().NoAccents();}
        }

        /// <summary>
        /// Data de Nascimento
        /// </summary>
        /// <remarks>
/// <para>A data de nascimento pode ser nula na entrada</para>
/// </remarks>
        public DateTime? DATNAC{ get;set;}

        /// <summary>
        /// Pessoa Políticamente Exposta
        /// </summary>
        /// <remarks>
/// <para>A pessoa politicamente exposta é uma pessoa que trabalha ou trabalhou nos últimos cinco anos, no Brasil ou no exterior, em cargos, empregos ou funções públicas. Pessoas que tenham familiares, cônjuges, representantes, parentes, relacionados direta ou indiretamente, com uma pessoa politicamente exposta também são assim considerados.</para>
/// </remarks>
        public bool ATRPPE{ get;set;}

        /// <summary>
        /// 
        /// </summary>
        public string DSCPPE{ get;set;} = "";

        /// <summary>
        /// Controle de Operação
        /// </summary>
        public byte CTLOPE{ get;set;}

        /// <summary>
        /// Ocorrências de Registro do Cadastro
        /// </summary>
        public string DSCOCO{ get;set;} = "";

        /// <summary>
        /// Código de Nacionalidade
        /// </summary>
        /// <remarks>
/// <para>Identifica o país de origem, Brasil = 55, Tabela Gral 80</para>
/// </remarks>
        public short CODNAC{ get;set;}

        /// <summary>
        /// Nacionalidade
        /// </summary>
        public string DSCNAC{ get;set;} = "";

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
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        public string LGNUSU{ get;set;} = "";

}}
