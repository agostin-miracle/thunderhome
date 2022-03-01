using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBCADCTO Alias Tabela de Contatos
///</summary>

    public class ContactBook
    {
               /// <summary>
        /// Código do Registro de Contato
        /// </summary>
        public int CODCTO{ get;set;} = 0;

        /// <summary>
        /// Código do Usuário
        /// </summary>
        public int CODUSU{ get;set;} = 0;

        /// <summary>
        /// Código do Endereço
        /// </summary>
        public int CODEND{ get;set;} = 0;

        /// <summary>
        /// Tipo de Contato
        /// </summary>
        public byte TIPCTO{ get;set;} = 1;

        /// <summary>
        /// Registro Principal
        /// </summary>
        public byte REGATV{ get;set;} = 0;

        /// <summary>
        /// Código do Pais
        /// </summary>
        public short CODPAI{ get;set;} = 55;

        /// <summary>
        /// Código da Operadora
        /// </summary>
        public short CODOPR{ get;set;} = 0;

        /// <summary>
        /// Número do DDD
        /// </summary>
        public string NUMDDD{ get;set;} = "";

        /// <summary>
        /// Define se o contato telefônico permite whats app
        /// </summary>
        public bool ISWHAT{ get;set;} = false;

        /// <summary>
        /// Número do Telefone
        /// </summary>
        public string NUMTEL{ get;set;} = "";

        /// <summary>
        /// Observações
        /// </summary>
        public string DSCOBS{ get;set;} = "";

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
public class QueryContactBook
{
         #region "Variáveis Privadas"
        private string _NOMUSU="";
        private string _DSCEND="";
         #endregion "Variáveis Privadas"

        /// <summary>
        /// Código do Registro de Contato
        /// </summary>
        public int CODCTO{ get;set;} = 0;

        /// <summary>
        /// Código do Usuário
        /// </summary>
        public int CODUSU{ get;set;} = 0;

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string NOMUSU
        { 
            get { return _NOMUSU;}
            set { _NOMUSU= value.ToUpper().NoAccents();}
        }

        /// <summary>
        /// Código do Endereço
        /// </summary>
        public int CODEND{ get;set;} = 0;

        /// <summary>
        /// Endereço
        /// </summary>
        public string DSCEND
        { 
            get { return _DSCEND;}
            set { _DSCEND= value.ToUpper().NoAccents();}
        }

        /// <summary>
        /// Tipo de Contato
        /// </summary>
        public byte TIPCTO{ get;set;} = 1;

        /// <summary>
        /// Descrição do Tipo de Contato
        /// </summary>
        public string DSCCTO{ get;set;} = "";

        /// <summary>
        /// Registro Principal
        /// </summary>
        public byte REGATV{ get;set;} = 0;

        /// <summary>
        /// Descrição da Atividade
        /// </summary>
        public string DSCATV{ get;set;} = "";

        /// <summary>
        /// Código do Pais
        /// </summary>
        public short CODPAI{ get;set;} = 55;

        /// <summary>
        /// País
        /// </summary>
        public string DSCPAI{ get;set;} = "";

        /// <summary>
        /// Código da Operadora
        /// </summary>
        public short CODOPR{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public string DSCOPR{ get;set;} = "";

        /// <summary>
        /// Número do DDD
        /// </summary>
        public string NUMDDD{ get;set;} = "";

        /// <summary>
        /// Define se o contato telefônico permite whats app
        /// </summary>
        public bool ISWHAT{ get;set;} = false;

        /// <summary>
        /// Número do Telefone
        /// </summary>
        public string NUMTEL{ get;set;} = "";

        /// <summary>
        /// Observações
        /// </summary>
        public string DSCOBS{ get;set;} = "";

        /// <summary>
        /// Código do Status de Registro
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 07</para>
/// </remarks>
        public byte STAREC{ get;set;} = 1;

        /// <summary>
        /// Descrição do Status de Registro
        /// </summary>
        public string DSCREC{ get;set;} = "";

        /// <summary>
        /// Data de Inclusão ou cadastramento
        /// </summary>
        public string DATCAD{ get;set;}

        /// <summary>
        /// Data da Ultima Atualização
        /// </summary>
        public string DATUPD{ get;set;}

        /// <summary>
        /// Usuário de Atualização
        /// </summary>
        public int UPDUSU{ get;set;} = 0;

        /// <summary>
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        public string LGNUSU{ get;set;} = "";

}}
