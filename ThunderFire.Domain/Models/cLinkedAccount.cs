using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBCADCVL Alias Linked Account
///</summary>

    public class LinkedAccount
    {
                #region "Variáveis Privadas"
        private string _NOMUSU="";
        private string _DSCCTA="";
         #endregion "Variáveis Privadas"
        /// <summary>
        /// 
        /// </summary>
        public int NIDCVL{ get;set;} = 0;

        /// <summary>
        /// Código do Usuário
        /// </summary>
        public int CODUSU{ get;set;} = 0;

        /// <summary>
        /// ID da Conta Virtual
        /// </summary>
        public int NIDCTA{ get;set;} = 0;

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
        /// Número da Conta
        /// </summary>
        public string NUMCTA{ get;set;} = "";

        /// <summary>
        /// Número da Agência
        /// </summary>
        public string NUMAGE{ get;set;} = "";

        /// <summary>
        /// Digito Verificador
        /// </summary>
        public string NUMDVE{ get;set;} = "";

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
        /// 
        /// </summary>
        public string DSCBCO{ get;set;} = "";

        /// <summary>
        /// Descrição do Status
        /// </summary>
        public string DSCSTA{ get;set;} = "";

        /// <summary>
        /// CPF/CNPJ
        /// </summary>
        public string CODCMF{ get;set;} = "";

        /// <summary>
        /// Nome do Beneficiário
        /// </summary>
        public string NOMBNF{ get;set;} = "";

        /// <summary>
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        public string LGNUSU{ get;set;} = "";

        /// <summary>
        /// Descrição da Conta
        /// </summary>
        public string DSCCTA
        { 
            get { return _DSCCTA;}
            set { if(!String.IsNullOrWhiteSpace(value))
_DSCCTA= value .ToUpper().NoAccents();
else
_DSCCTA= "";
}

        }

    }
}
