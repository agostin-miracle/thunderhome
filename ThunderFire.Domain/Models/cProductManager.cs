using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBUSUPRO Alias Gestores de Produtos
///</summary>

    public class ProductManager
    {
                #region "Variáveis Privadas"
        private string _NOMUSU="";
         #endregion "Variáveis Privadas"
        /// <summary>
        /// Código do Usuário Gestor
        /// </summary>
        /// <remarks>
/// <para>O usuário gestor é determinado pela associação do usuário com o produto</para>
/// </remarks>
        public int USUPRO{ get;set;} = 0;

        /// <summary>
        /// Código do Usuário
        /// </summary>
        public int CODUSU{ get;set;} = 0;

        /// <summary>
        /// Codigo do Produto
        /// </summary>
        public short CODPRO{ get;set;} = 0;

        /// <summary>
        /// Define se o registro é de aplicação ou controle interno
        /// </summary>
        public bool APLINT{ get;set;} = false;

        /// <summary>
        /// Valor Minimo
        /// </summary>
        public double VLRMIN{ get;set;} = 0;

        /// <summary>
        /// Valor Máximo
        /// </summary>
        public double VLRMAX{ get;set;} = 0;

        /// <summary>
        /// Define (1) se aplica a regra de vencimento, caso contrário nçao aplica
        /// </summary>
        public bool APLRVC{ get;set;} = false;

        /// <summary>
        /// Número de dias a ser adicionado a regra de vencimento, caso APLRVC=1
        /// </summary>
        public byte REGVCT{ get;set;} = 10;

        /// <summary>
        /// Indica se o gestor tem aplicação de geração de conta virtual especial para o cartão
        /// </summary>
        /// <remarks>
/// <para>A conta criada será atribuída do código de origem ORGCTA=3</para>
/// </remarks>
        public byte APLCES{ get;set;} = 0;

        /// <summary>
        /// Define se deve aplicar a Tarifa Administrativa
        /// </summary>
        public byte APLTAD{ get;set;} = 0;

        /// <summary>
        /// Define se deve aplicar a Mensalidade ã distribuição de mensalidade para o cartão
        /// </summary>
        public byte APLMEN{ get;set;} = 0;

        /// <summary>
        /// Valor da Mensalidade
        /// </summary>
        public double VLRMEN{ get;set;} = 0;

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
        /// Descrição do Produto
        /// </summary>
        public string DSCPRO{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public string DSCAPL{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public string DSCRVC{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public string DSCCES{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public string DSCTAD{ get;set;} = "";

        /// <summary>
        /// Descrição do Status de Registro
        /// </summary>
        public string DSCREC{ get;set;} = "";

        /// <summary>
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        public string LGNUSU{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public bool? HASCFG{ get;set;} = false;

        /// <summary>
        /// 
        /// </summary>
        public int CNTTAR{ get;set;} = 0;

    }
}
