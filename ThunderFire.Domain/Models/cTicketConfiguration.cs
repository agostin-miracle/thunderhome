using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBCFGBOL Alias Ticket Configuration
///</summary>

    public class TicketConfiguration
    {
                #region "Variáveis Privadas"
        private string _TIPEXT="";
        private string _DSCTAR="";
        private string _NOMUSU="";
        private string _DSCTDP="";
         #endregion "Variáveis Privadas"
        /// <summary>
        /// Id do Registro de Configuração de Boleto
        /// </summary>
        public int NIDCBL{ get;set;} = 0;

        /// <summary>
        /// Código do Usuário
        /// </summary>
        /// <remarks>
/// <para>Este campo acumula função dupla conforme o nivel de configuração do usuário</para>
/// </remarks>
        public int USUCFG{ get;set;} = 0;

        /// <summary>
        /// Id do Nível de Configuração do Usuário
        /// </summary>
        /// <remarks>
/// <para></para>
/// <para>0 - N/A</para>
/// <para>1 - Gestor</para>
/// <para>2 - Usuário</para>
/// </remarks>
        public byte NIVCFG{ get;set;} = 1;

        /// <summary>
        /// Tipo de Boleto
        /// </summary>
        public byte TIPBOL{ get;set;} = 1;

        /// <summary>
        /// Define se deve aplicar a Tarifa Externa ao controle de tarifação
        /// </summary>
        /// <remarks>
/// <para>Caso o valor atribuido seja 1, o valor da tarifa deverá ser fornecido</para>
/// </remarks>
        public byte APLTAR{ get;set;} = 0;

        /// <summary>
        /// Código da Tarifa
        /// </summary>
        public short CODTAR{ get;set;} = 0;

        /// <summary>
        /// Define se deve aplicar a Tarifa de Depositante
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 912</para>
/// </remarks>
        public byte APLTDP{ get;set;} = 0;

        /// <summary>
        /// Código da Tarifa de Depositante
        /// </summary>
        public short TARTDP{ get;set;} = 0;

        /// <summary>
        /// Tipo de Boleto Externo
        /// </summary>
        public string TIPEXT
        { 
            get { return _TIPEXT;}
            set { if(!String.IsNullOrWhiteSpace(value))
_TIPEXT= value .ToUpper().NoAccents();
else
_TIPEXT= "";
}

        }

        /// <summary>
        /// Instrução Bancária 1
        /// </summary>
        public string INSBC1{ get;set;} = "";

        /// <summary>
        /// Instrução Bancária 2
        /// </summary>
        public string INSBC2{ get;set;} = "";

        /// <summary>
        /// Instrução Bancária 3
        /// </summary>
        public string INSBC3{ get;set;} = "";

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
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        public string LGNUSU{ get;set;} = "";

        /// <summary>
        /// Descrição do Status de Registro
        /// </summary>
        public string DSCREC{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public string DSCTBL{ get;set;} = "";

        /// <summary>
        /// Descrição do Método de Aplicação da Taxa de Depositante
        /// </summary>
        public string DSCTDP
        { 
            get { return _DSCTDP;}
            set { if(!String.IsNullOrWhiteSpace(value))
_DSCTDP= value .ToUpper().NoAccents();
else
_DSCTDP= "";
}

        }

    }
}
