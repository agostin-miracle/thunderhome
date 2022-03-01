using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBCADCTA Alias Virtual Account
///</summary>

    public class VirtualAccount
    {
               /// <summary>
        /// ID da Conta Virtual
        /// </summary>
        public int NIDCTA{ get;set;} = 0;

        /// <summary>
        /// Código do Usuário
        /// </summary>
        public int CODUSU{ get;set;} = 0;

        /// <summary>
        /// Número da Agência
        /// </summary>
        public string NUMAGE{ get;set;} = " ";

        /// <summary>
        /// Código do Banco
        /// </summary>
        public string NUMBCO{ get;set;} = "000";

        /// <summary>
        /// Número da Conta
        /// </summary>
        public string NUMCTA{ get;set;} = "";

        /// <summary>
        /// Digito Verificador
        /// </summary>
        public string NUMDVE{ get;set;} = "";

        /// <summary>
        /// Código de Origem da Conta
        /// </summary>
        /// <remarks>
/// <para>1 - Conta Virtual</para>
/// <para>2 - Conta Externa</para>
/// <para>3 - Conta Virtual Vinculada ao Cartão</para>
/// <para>4 - Conta Virtual Benefícios</para>
/// </remarks>
        public byte ORGCTA{ get;set;} = 1;

        /// <summary>
        /// Registro Principal
        /// </summary>
        public byte REGATV{ get;set;} = 0;

        /// <summary>
        /// Tipo de Conta
        /// </summary>
        public byte TIPCTA{ get;set;} = 1;

        /// <summary>
        /// Código do Status da Conta
        /// </summary>
        public short STACTA{ get;set;} = 0;

        /// <summary>
        /// Data de Validade
        /// </summary>
        public DateTime DATVAL{ get;set;} 

        /// <summary>
        /// Indica se deve ser aplicado a validação de limite de operação
        /// </summary>
        /// <remarks>
/// <para>Em caso afirmativo, o sistema efetuará a validação do valor da operação corrente</para>
/// </remarks>
        public byte APLLIM{ get;set;} = 0;

        /// <summary>
        /// Valor limite
        /// </summary>
        public double VLRLIM{ get;set;} = 0;

        /// <summary>
        /// Tipo de Beneficiário
        /// </summary>
        /// <remarks>
/// <para></para>
/// <para>1 - O próprio favorecido</para>
/// <para>2 - Transferencia para Terceiros</para>
/// <para>Tabela Geral 20</para>
/// </remarks>
        public int TIPBNF{ get;set;} = 0;

        /// <summary>
        /// CPF/CNPJ
        /// </summary>
        public string CODCMF{ get;set;} = "000000000000000";

        /// <summary>
        /// Nome do Beneficiário
        /// </summary>
        public string NOMBNF{ get;set;} = " ";

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
public class QueryVirtualAccount
{
         #region "Variáveis Privadas"
        private string _NOMUSU="";
        private string _DSCCTA="";
        private string _TIPEXT="";
         #endregion "Variáveis Privadas"

        /// <summary>
        /// ID da Conta Virtual
        /// </summary>
        public int NIDCTA{ get;set;}

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
        /// Número da Agência
        /// </summary>
        public string NUMAGE{ get;set;} = " ";

        /// <summary>
        /// Código do Banco
        /// </summary>
        public string NUMBCO{ get;set;} = "000";

        /// <summary>
        /// 
        /// </summary>
        public string DSCBCO{ get;set;} = "";

        /// <summary>
        /// Número da Conta
        /// </summary>
        public string NUMCTA{ get;set;} = "";

        /// <summary>
        /// Digito Verificador
        /// </summary>
        public string NUMDVE{ get;set;} = "";

        /// <summary>
        /// Código de Origem da Conta
        /// </summary>
        /// <remarks>
/// <para>1 - Conta Virtual</para>
/// <para>2 - Conta Externa</para>
/// <para>3 - Conta Virtual Vinculada ao Cartão</para>
/// <para>4 - Conta Virtual Benefícios</para>
/// </remarks>
        public byte ORGCTA{ get;set;}

        /// <summary>
        /// 
        /// </summary>
        public string DSCORG{ get;set;} = "";

        /// <summary>
        /// Tipo de Conta
        /// </summary>
        public byte TIPCTA{ get;set;}

        /// <summary>
        /// Descrição da Conta
        /// </summary>
        public string DSCCTA
        { 
            get { return _DSCCTA;}
            set { _DSCCTA= value.ToUpper().NoAccents();}
        }

        /// <summary>
        /// Tipo de Boleto Externo
        /// </summary>
        public string TIPEXT
        { 
            get { return _TIPEXT;}
            set { _TIPEXT= value.ToUpper().NoAccents();}
        }

        /// <summary>
        /// Código do Status da Conta
        /// </summary>
        public short STACTA{ get;set;}

        /// <summary>
        /// Descrição do Status
        /// </summary>
        public string DSCSTA{ get;set;} = "";

        /// <summary>
        /// Data de Validade
        /// </summary>
        public DateTime DATVAL{ get;set;}

        /// <summary>
        /// Indica se deve ser aplicado a validação de limite de operação
        /// </summary>
        /// <remarks>
/// <para>Em caso afirmativo, o sistema efetuará a validação do valor da operação corrente</para>
/// </remarks>
        public byte APLLIM{ get;set;}

        /// <summary>
        /// Valor limite
        /// </summary>
        public double VLRLIM{ get;set;}

        /// <summary>
        /// Tipo de Beneficiário
        /// </summary>
        /// <remarks>
/// <para></para>
/// <para>1 - O próprio favorecido</para>
/// <para>2 - Transferencia para Terceiros</para>
/// <para>Tabela Geral 20</para>
/// </remarks>
        public int TIPBNF{ get;set;}

        /// <summary>
        /// 
        /// </summary>
        public string DSCBNF{ get;set;} = "";

        /// <summary>
        /// CPF/CNPJ
        /// </summary>
        public string CODCMF{ get;set;} = "000000000000000";

        /// <summary>
        /// Nome do Beneficiário
        /// </summary>
        public string NOMBNF{ get;set;} = " ";

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
        /// Usuário de Atualização
        /// </summary>
        public int UPDUSU{ get;set;}

        /// <summary>
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        public string LGNUSU{ get;set;} = "";

}}
