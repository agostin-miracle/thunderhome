using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBCADTAR Alias Tariffs
///</summary>

    public class Tariff
    {
               /// <summary>
        /// ID do Registro de Tarifação
        /// </summary>
        public int NIDTAR{ get;set;} = 0;

        /// <summary>
        /// Id do Nível de Configuração do Usuário
        /// </summary>
        /// <remarks>
/// <para></para>
/// <para>0 - N/A</para>
/// <para>1 - Gestor</para>
/// <para>2 - Usuário</para>
/// </remarks>
        public byte NIVCFG{ get;set;} = 2;

        /// <summary>
        /// Código do Usuário
        /// </summary>
        /// <remarks>
/// <para>Este campo acumula função dupla conforme o nivel de configuração do usuário</para>
/// </remarks>
        public int USUCFG{ get;set;} = 0;

        /// <summary>
        /// Código da Tarifa
        /// </summary>
        public byte CODTAR{ get;set;} = 1;

        /// <summary>
        /// Código da Bandeira
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 63</para>
/// </remarks>
        public short CODBND{ get;set;} = 0;

        /// <summary>
        /// Tipo de Linha
        /// </summary>
        /// <remarks>
/// <para>1 - Lançamento de Inscrição de Valo</para>
/// <para>2 - Lançamento de Inscrição de Tarifa</para>
/// </remarks>
        public byte TIPLIN{ get;set;} = 0;

        /// <summary>
        /// Modalidade de Aplicação do Cartão
        /// </summary>
        /// <remarks>
/// <para>Este campo utiliza os atributos da Tabela TBMODTAR para aplicação de cálculo de parcelamento</para>
/// </remarks>
        public short MODCRT{ get;set;} = 0;

        /// <summary>
        /// Código da Expansão de Tarifa
        /// </summary>
        public short CODEXP{ get;set;} = 0;

        /// <summary>
        /// Codigo da Moeda
        /// </summary>
        public short CODMOE{ get;set;} = 1;

        /// <summary>
        /// Formato de Cobrança
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 30</para>
/// <para></para>
/// <para></para>
/// <para>1 - VALOR</para>
/// <para>2 - PERCENTUAL</para>
/// <para>3 - ANTECIPACAO</para>
/// <para>4 - PARCELAMENTO</para>
/// <para>5 - RAV</para>
/// <para>6 - % + VALOR</para>
/// </remarks>
        public byte FMTCOB{ get;set;} = 1;

        /// <summary>
        /// Responsável pela Tarifa 
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 105</para>
/// </remarks>
        public byte RSPTAR{ get;set;} = 2;

        /// <summary>
        /// Valor Mínimo da Tarifa Básica
        /// </summary>
        public double TARBAS{ get;set;} = 0;

        /// <summary>
        /// Valor Máximo da Tarifa Básica
        /// </summary>
        public double TARMAX{ get;set;} = 0;

        /// <summary>
        /// % de Tarifação
        /// </summary>
        public double PCTTAR{ get;set;} = 0;

        /// <summary>
        /// Valor da Tarifa
        /// </summary>
        public double VLRTAR{ get;set;} = 0;

        /// <summary>
        /// Valor Mínimo
        /// </summary>
        public double VLRINF{ get;set;} = 0;

        /// <summary>
        /// Valor Máximo
        /// </summary>
        public double VLRMAX{ get;set;} = 0;

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
        /// <summary>
        /// Objeto de Retenção de Informações de Tarifas
        /// </summary>
public  class QueryTariff
{
 #region "Variáveis Privadas"        
private string _NOMUSU="";
        
private string _DSCTAR="";
        
private string _DSCLIN="";
 #endregion "Variáveis Privadas"
        /// <summary>
        /// ID do Registro de Tarifação
        /// </summary>
        
        public int NIDTAR{ get;set;} = 0;

        /// <summary>
        /// Id do Nível de Configuração do Usuário
        /// </summary>
        /// <remarks>
/// <para></para>
/// <para>0 - N/A</para>
/// <para>1 - Gestor</para>
/// <para>2 - Usuário</para>
/// </remarks>
        
        public byte NIVCFG{ get;set;} = 2;

        /// <summary>
        /// Código do Usuário
        /// </summary>
        /// <remarks>
/// <para>Este campo acumula função dupla conforme o nivel de configuração do usuário</para>
/// </remarks>
        
        public int USUCFG{ get;set;} = 0;

        /// <summary>
        /// Código da Tarifa
        /// </summary>
        
        public byte CODTAR{ get;set;} = 1;

        /// <summary>
        /// Código da Bandeira
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 63</para>
/// </remarks>
        
        public short CODBND{ get;set;} = 0;

        /// <summary>
        /// Tipo de Linha
        /// </summary>
        /// <remarks>
/// <para>1 - Lançamento de Inscrição de Valo</para>
/// <para>2 - Lançamento de Inscrição de Tarifa</para>
/// </remarks>
        
        public byte TIPLIN{ get;set;} = 0;

        /// <summary>
        /// Modalidade de Aplicação do Cartão
        /// </summary>
        /// <remarks>
/// <para>Este campo utiliza os atributos da Tabela TBMODTAR para aplicação de cálculo de parcelamento</para>
/// </remarks>
        
        public short MODCRT{ get;set;} = 0;

        /// <summary>
        /// Código da Expansão de Tarifa
        /// </summary>
        
        public short CODEXP{ get;set;} = 0;

        /// <summary>
        /// Codigo da Moeda
        /// </summary>
        
        public short CODMOE{ get;set;} = 1;

        /// <summary>
        /// Formato de Cobrança
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 30</para>
/// <para></para>
/// <para></para>
/// <para>1 - VALOR</para>
/// <para>2 - PERCENTUAL</para>
/// <para>3 - ANTECIPACAO</para>
/// <para>4 - PARCELAMENTO</para>
/// <para>5 - RAV</para>
/// <para>6 - % + VALOR</para>
/// </remarks>
        
        public byte FMTCOB{ get;set;} = 1;

        /// <summary>
        /// Responsável pela Tarifa 
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 105</para>
/// </remarks>
        
        public byte RSPTAR{ get;set;} = 2;

        /// <summary>
        /// Valor Mínimo da Tarifa Básica
        /// </summary>
        
        public double TARBAS{ get;set;} = 0;

        /// <summary>
        /// Valor Máximo da Tarifa Básica
        /// </summary>
        
        public double TARMAX{ get;set;} = 0;

        /// <summary>
        /// % de Tarifação
        /// </summary>
        
        public double PCTTAR{ get;set;} = 0;

        /// <summary>
        /// Valor da Tarifa
        /// </summary>
        
        public double VLRTAR{ get;set;} = 0;

        /// <summary>
        /// Valor Mínimo
        /// </summary>
        
        public double VLRINF{ get;set;} = 0;

        /// <summary>
        /// Valor Máximo
        /// </summary>
        
        public double VLRMAX{ get;set;} = 0;

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
            set { _NOMUSU= value.ToUpper().NoAccents();}
        }

        /// <summary>
        /// 
        /// </summary>
        
        public string DSCCFG{get;set;} = "";

        /// <summary>
        /// PayLoad de Registro de Resposta
        /// </summary>
        
        public string DSCRSP{get;set;} = "";

        /// <summary>
        /// Descrição da Tarifa
        /// </summary>
        
        public string DSCTAR
        { 
            get { return _DSCTAR;}
            set { _DSCTAR= value.ToUpper().NoAccents();}
        }

        /// <summary>
        /// Descrição da Linha de Produto
        /// </summary>
        
        public string DSCLIN
        { 
            get { return _DSCLIN;}
            set { _DSCLIN= value.ToUpper().NoAccents();}
        }

        /// <summary>
        /// 
        /// </summary>
        
        public string DSCBND{get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        
        public string DSCMOD{get;set;} = "";

        /// <summary>
        /// Descrição da Expansão de Tarifa
        /// </summary>
        
        public string DSCEXP{get;set;} = "";

        /// <summary>
        /// Descrição da Moeda
        /// </summary>
        
        public string DSCMOE{get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        
        public string DSCCOB{get;set;} = "";

        /// <summary>
        /// Descrição do Status de Registro
        /// </summary>
        
        public string DSCREC{get;set;} = "";

        /// <summary>
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        
        public string LGNUSU{get;set;} = "";

}
}
