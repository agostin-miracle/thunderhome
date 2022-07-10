using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBREGBOL Alias Registro de Boletos
///</summary>

    public class Ticket
    {
               /// <summary>
        /// ID do Boleto
        /// </summary>
        public int NIDBOL{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public byte MODCAL{ get;set;} = 1;

        /// <summary>
        /// 
        /// </summary>
        public int CODEMP{ get;set;} = 0;

        /// <summary>
        /// Chave de Identificação do Boleto
        /// </summary>
        public System.Guid KEYBOL{ get;set;} = new Guid();

        /// <summary>
        /// 
        /// </summary>
        public byte SUBSYS{ get;set;} = 1;

        /// <summary>
        /// Id do Registro de Configuração de Boleto
        /// </summary>
        public int NIDCBL{ get;set;} = 0;

        /// <summary>
        /// Código do Usuário Gestor
        /// </summary>
        /// <remarks>
/// <para>O usuário gestor é determinado pela associação do usuário com o produto</para>
/// </remarks>
        public int USUPRO{ get;set;} = 0;

        /// <summary>
        /// Código do usuario cedente
        /// </summary>
        public int USUCED{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public int USUSAC{ get;set;} = 0;

        /// <summary>
        /// Código do Avalista
        /// </summary>
        public int CODAVA{ get;set;} = 0;

        /// <summary>
        /// Código do Endereço
        /// </summary>
        public int CODEND{ get;set;} = 0;

        /// <summary>
        /// Codigo da Moeda
        /// </summary>
        public int CODMOE{ get;set;} = 1;

        /// <summary>
        /// Código do Status do Boleto
        /// </summary>
        public short STABOL{ get;set;} = 0;

        /// <summary>
        /// Tipo de Boleto
        /// </summary>
        public byte TIPBOL{ get;set;} = 1;

        /// <summary>
        /// Tipo de Baixa
        /// </summary>
        /// <remarks>
/// <para>0 - Baixa de Registro Bancário</para>
/// <para>1 - Baixa de Registro de Boleto</para>
/// </remarks>
        public byte TIPBXA{ get;set;} = 0;

        /// <summary>
        /// Tipo de Entrada de Associada
        /// </summary>
        /// <remarks>
/// <para>Pré-Cadastro</para>
/// <para></para>
/// <para>1 - Registro de Pré-Cadastro</para>
/// <para>2 - Registro para Embossing</para>
/// <para>3 - Venda de Cartão</para>
/// <para>Conta Corrente (Entry Mode)</para>
/// <para></para>
/// <para>1 - Registro de Transação Online</para>
/// <para>2 - Registro de Transação não online</para>
/// <para>3 - Saque</para>
/// </remarks>
        public byte TIPENT{ get;set;} = 1;

        /// <summary>
        /// Versão de Aplicação
        /// </summary>
        public short CODVER{ get;set;} = 3;

        /// <summary>
        /// Data de Emissão
        /// </summary>
        public DateTime DATEMI{ get;set;} = DateTime.Now;

        /// <summary>
        /// Data de Vencimento
        /// </summary>
        public DateTime DATVCT{ get;set;} = DateTime.Now;

        /// <summary>
        /// Data de Pagamento
        /// </summary>
        public DateTime? DATPGT{ get;set;} = null;

        /// <summary>
        /// Data de Validade
        /// </summary>
        public DateTime DATVAL{ get;set;} = DateTime.Now;

        /// <summary>
        /// Data de Registro
        /// </summary>
        public DateTime DATREG{ get;set;} = DateTime.Now;

        /// <summary>
        /// Número da Parcela
        /// </summary>
        public short NUMPCL{ get;set;} = 1;

        /// <summary>
        /// Código do Cedente
        /// </summary>
        public string CODCED{ get;set;} = "";

        /// <summary>
        /// ID do Lote de Importação
        /// </summary>
        public int NIDLIM{ get;set;} = 0;

        /// <summary>
        /// IP de Origem
        /// </summary>
        public string NUMIPA{ get;set;} = "";

        /// <summary>
        /// Origem do Boleto
        /// </summary>
        public int ORGBOL{ get;set;} = 1;

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
        /// Descrição do Boleto
        /// </summary>
        public string DSCBOL{ get;set;} = "";

        /// <summary>
        /// Url de Retorno
        /// </summary>
        public string URLRET{ get;set;} = "";

        /// <summary>
        /// Código de Retono
        /// </summary>
        public int CODRET{ get;set;} = 0;

        /// <summary>
        /// Nome do Arquivo de origem
        /// </summary>
        public string FILNAM{ get;set;} = "";

        /// <summary>
        /// ID da Conta Virtual
        /// </summary>
        public int NIDCTA{ get;set;} = 0;

        /// <summary>
        /// Número de Dias antes do vencimento para envio da fatura 
        /// </summary>
        public short NUMDVF{ get;set;} = 10;

        /// <summary>
        /// Pedido do Cliente
        /// </summary>
        public string PEDCLI{ get;set;} = "";

        /// <summary>
        /// Linha Digitável
        /// </summary>
        public string LINDIG{ get;set;} = "";

        /// <summary>
        /// Espécie do Documento
        /// </summary>
        public string CODESP{ get;set;} = "";

        /// <summary>
        /// Número da Carteira
        /// </summary>
        public string NUMCTR{ get;set;} = "";

        /// <summary>
        /// Nosso Número
        /// </summary>
        /// <remarks>
/// <para>Número de Registro do boleto no Banco</para>
/// </remarks>
        public int NOSNUM{ get;set;} = 0;

        /// <summary>
        /// Se true, indica que a imagem do código de barras do boleto foi salva
        /// </summary>
        public bool IMGSAV{ get;set;} = false;

        /// <summary>
        /// Se true, indica que o registro foi conciliado pelo usuário
        /// </summary>
        public bool INDCNC{ get;set;} = false;

        /// <summary>
        /// Código de Aplicação de Cálculo
        /// </summary>
        /// <remarks>
/// <para>0 - Nenhum cálculo à ser aplicado</para>
/// <para></para>
/// <para>1 - Cálculo de Tarifa Administrativa</para>
/// <para>O cálculo é baseado no Código de Tarifa 10, aplicado somente se o gestor USUPRO estiver configurado com APLTAD=1</para>
/// </remarks>
        public byte APLCAL{ get;set;} = 0;

        /// <summary>
        /// Registro de Processamento
        /// </summary>
        public int REGPRO{ get;set;} = 0;

        /// <summary>
        /// Responsável pela Tarifa 
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 105</para>
/// </remarks>
        public byte RSPTAR{ get;set;} = 1;

        /// <summary>
        /// Valor do Boleto
        /// </summary>
        public double VLRBOL{ get;set;} = 0;

        /// <summary>
        /// Define se deve aplicar a Tarifa Externa ao controle de tarifação
        /// </summary>
        /// <remarks>
/// <para>Caso o valor atribuido seja 1, o valor da tarifa deverá ser fornecido</para>
/// </remarks>
        public byte APLTAR{ get;set;} = 0;

        /// <summary>
        /// Valor da Tarifa
        /// </summary>
        public double VLRTAR{ get;set;} = 0;

        /// <summary>
        /// Define se deve aplicar a Tarifa de Depositante
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 912</para>
/// </remarks>
        public byte APLTDP{ get;set;} = 0;

        /// <summary>
        /// Valor da Tarifa de Depositante
        /// </summary>
        public double VLRTDP{ get;set;} = 0;

        /// <summary>
        /// Código do Status de Registro
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 07</para>
/// </remarks>
        public int STAREC{ get;set;} = 1;

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
        /// Valor líquido
        /// </summary>
        public double VLRLIQ{ get;set;} = 0;

    }
        /// <summary>
        /// Objeto de Retenção de Pesquisa de Boletos
        /// </summary>
public  class TicketQuery
{

        /// <summary>
        /// ID do Boleto
        /// </summary>
        
        public int NIDBOL{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        
        public byte MODCAL{ get;set;} = 1;

        /// <summary>
        /// 
        /// </summary>
        
        public int CODEMP{ get;set;} = 0;

        /// <summary>
        /// Chave de Identificação do Boleto
        /// </summary>
        
        public System.Guid KEYBOL{ get;set;} = new Guid();

        /// <summary>
        /// 
        /// </summary>
        
        public byte SUBSYS{ get;set;} = 1;

        /// <summary>
        /// Id do Registro de Configuração de Boleto
        /// </summary>
        
        public int NIDCBL{ get;set;} = 0;

        /// <summary>
        /// Código do Usuário Gestor
        /// </summary>
        /// <remarks>
/// <para>O usuário gestor é determinado pela associação do usuário com o produto</para>
/// </remarks>
        
        public int USUPRO{ get;set;} = 0;

        /// <summary>
        /// Código do usuario cedente
        /// </summary>
        
        public int USUCED{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        
        public int USUSAC{ get;set;} = 0;

        /// <summary>
        /// Código do Avalista
        /// </summary>
        
        public int CODAVA{ get;set;} = 0;

        /// <summary>
        /// Código do Endereço
        /// </summary>
        
        public int CODEND{ get;set;} = 0;

        /// <summary>
        /// Codigo da Moeda
        /// </summary>
        
        public int CODMOE{ get;set;} = 1;

        /// <summary>
        /// Código do Status do Boleto
        /// </summary>
        
        public short STABOL{ get;set;} = 0;

        /// <summary>
        /// Tipo de Boleto
        /// </summary>
        
        public byte TIPBOL{ get;set;} = 1;

        /// <summary>
        /// Tipo de Baixa
        /// </summary>
        /// <remarks>
/// <para>0 - Baixa de Registro Bancário</para>
/// <para>1 - Baixa de Registro de Boleto</para>
/// </remarks>
        
        public byte TIPBXA{ get;set;} = 0;

        /// <summary>
        /// Tipo de Entrada de Associada
        /// </summary>
        /// <remarks>
/// <para>Pré-Cadastro</para>
/// <para></para>
/// <para>1 - Registro de Pré-Cadastro</para>
/// <para>2 - Registro para Embossing</para>
/// <para>3 - Venda de Cartão</para>
/// <para>Conta Corrente (Entry Mode)</para>
/// <para></para>
/// <para>1 - Registro de Transação Online</para>
/// <para>2 - Registro de Transação não online</para>
/// <para>3 - Saque</para>
/// </remarks>
        
        public byte TIPENT{ get;set;} = 1;

        /// <summary>
        /// Versão de Aplicação
        /// </summary>
        
        public short CODVER{ get;set;} = 3;

        /// <summary>
        /// Data de Emissão
        /// </summary>
        
        public DateTime DATEMI{ get;set;} = DateTime.Now;

        /// <summary>
        /// Data de Vencimento
        /// </summary>
        
        public DateTime DATVCT{ get;set;} = DateTime.Now;

        /// <summary>
        /// Data de Pagamento
        /// </summary>
        
        public DateTime? DATPGT{ get;set;} = null;

        /// <summary>
        /// Data de Validade
        /// </summary>
        
        public DateTime DATVAL{ get;set;} = DateTime.Now;

        /// <summary>
        /// Data de Registro
        /// </summary>
        
        public DateTime DATREG{ get;set;} = DateTime.Now;

        /// <summary>
        /// Número da Parcela
        /// </summary>
        
        public short NUMPCL{ get;set;} = 1;

        /// <summary>
        /// Código do Cedente
        /// </summary>
        
        public string CODCED{get;set;} = "";

        /// <summary>
        /// ID do Lote de Importação
        /// </summary>
        
        public int NIDLIM{ get;set;} = 0;

        /// <summary>
        /// IP de Origem
        /// </summary>
        
        public string NUMIPA{get;set;} = "";

        /// <summary>
        /// Origem do Boleto
        /// </summary>
        
        public int ORGBOL{ get;set;} = 1;

        /// <summary>
        /// Instrução Bancária 1
        /// </summary>
        
        public string INSBC1{get;set;} = "";

        /// <summary>
        /// Instrução Bancária 2
        /// </summary>
        
        public string INSBC2{get;set;} = "";

        /// <summary>
        /// Instrução Bancária 3
        /// </summary>
        
        public string INSBC3{get;set;} = "";

        /// <summary>
        /// Descrição do Boleto
        /// </summary>
        
        public string DSCBOL{get;set;} = "";

        /// <summary>
        /// Url de Retorno
        /// </summary>
        
        public string URLRET{get;set;} = "";

        /// <summary>
        /// Código de Retono
        /// </summary>
        
        public int CODRET{ get;set;} = 0;

        /// <summary>
        /// Nome do Arquivo de origem
        /// </summary>
        
        public string FILNAM{get;set;} = "";

        /// <summary>
        /// ID da Conta Virtual
        /// </summary>
        
        public int NIDCTA{ get;set;} = 0;

        /// <summary>
        /// Número de Dias antes do vencimento para envio da fatura 
        /// </summary>
        
        public short NUMDVF{ get;set;} = 10;

        /// <summary>
        /// Pedido do Cliente
        /// </summary>
        
        public string PEDCLI{get;set;} = "";

        /// <summary>
        /// Linha Digitável
        /// </summary>
        
        public string LINDIG{get;set;} = "";

        /// <summary>
        /// Espécie do Documento
        /// </summary>
        
        public string CODESP{get;set;} = "";

        /// <summary>
        /// Número da Carteira
        /// </summary>
        
        public string NUMCTR{get;set;} = "";

        /// <summary>
        /// Nosso Número
        /// </summary>
        /// <remarks>
/// <para>Número de Registro do boleto no Banco</para>
/// </remarks>
        
        public int NOSNUM{ get;set;} = 0;

        /// <summary>
        /// Se true, indica que a imagem do código de barras do boleto foi salva
        /// </summary>
        
        public bool IMGSAV{ get;set;} = false;

        /// <summary>
        /// Se true, indica que o registro foi conciliado pelo usuário
        /// </summary>
        
        public bool INDCNC{ get;set;} = false;

        /// <summary>
        /// Código de Aplicação de Cálculo
        /// </summary>
        /// <remarks>
/// <para>0 - Nenhum cálculo à ser aplicado</para>
/// <para></para>
/// <para>1 - Cálculo de Tarifa Administrativa</para>
/// <para>O cálculo é baseado no Código de Tarifa 10, aplicado somente se o gestor USUPRO estiver configurado com APLTAD=1</para>
/// </remarks>
        
        public byte APLCAL{ get;set;} = 0;

        /// <summary>
        /// Registro de Processamento
        /// </summary>
        
        public int REGPRO{ get;set;} = 0;

        /// <summary>
        /// Responsável pela Tarifa 
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 105</para>
/// </remarks>
        
        public byte RSPTAR{ get;set;} = 1;

        /// <summary>
        /// Valor do Boleto
        /// </summary>
        
        public double VLRBOL{ get;set;} = 0;

        /// <summary>
        /// Define se deve aplicar a Tarifa Externa ao controle de tarifação
        /// </summary>
        /// <remarks>
/// <para>Caso o valor atribuido seja 1, o valor da tarifa deverá ser fornecido</para>
/// </remarks>
        
        public byte APLTAR{ get;set;} = 0;

        /// <summary>
        /// Valor da Tarifa
        /// </summary>
        
        public double VLRTAR{ get;set;} = 0;

        /// <summary>
        /// Define se deve aplicar a Tarifa de Depositante
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 912</para>
/// </remarks>
        
        public byte APLTDP{ get;set;} = 0;

        /// <summary>
        /// Valor da Tarifa de Depositante
        /// </summary>
        
        public double VLRTDP{ get;set;} = 0;

        /// <summary>
        /// Código do Status de Registro
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 07</para>
/// </remarks>
        
        public int STAREC{ get;set;} = 1;

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
        /// *** Herdado de TBCADGER***
        /// </summary>
/* *** Herdado de TBCADGER*** */
        
        public string EMPNOM{get;set;} = "";

        /// <summary>
        /// *** Herdado de TBCADGER***
        /// </summary>
/* *** Herdado de TBCADGER*** */
        
        public string CEDNOM{get;set;} = "";

        /// <summary>
        /// CPF CEDENTE*** Herdado de TBCADGER***
        /// </summary>
/* *** Herdado de TBCADGER*** */
        
        public string CEDCMF{get;set;} = "";

        /// <summary>
        /// *** Herdado de TBCADGER***
        /// </summary>
/* *** Herdado de TBCADGER*** */
        
        public string SACNOM{get;set;} = "";

        /// <summary>
        /// CPF/CNPJ do sacado*** Herdado de TBCADGER***
        /// </summary>
/* *** Herdado de TBCADGER*** */
        
        public string SACCMF{get;set;} = "";

        /// <summary>
        /// Email do sacado
        /// </summary>
        
        public string SACMAL{get;set;} = "";

        /// <summary>
        /// Nome do Avalista*** Herdado de TBCADGER***
        /// </summary>
/* *** Herdado de TBCADGER*** */
        
        public string AVANOM{get;set;} = "";

        /// <summary>
        /// CPF/CNPJ - Avalista*** Herdado de TBCADGER***
        /// </summary>
/* *** Herdado de TBCADGER*** */
        
        public string AVACMF{get;set;} = "";

        /// <summary>
        /// Valor líquido
        /// </summary>
        
        public double VLRLIQ{ get;set;} = 0;

        /// <summary>
        /// Descrição do Status*** Herdado de TBCADSTA***
        /// </summary>
/* *** Herdado de TBCADSTA*** */
        
        public string DSCSTA{get;set;} = "";

        /// <summary>
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        
        public string LGNUSU{get;set;} = "";

        /// <summary>
        /// Descrição do Tipo de Boleto*** Herdado de TBTABGER***
        /// </summary>
/* *** Herdado de TBTABGER*** */
        
        public string DSCBLT{get;set;} = "";

}
        /// <summary>
        /// Objeto de Retenção de Pesquisa de Boletos para emissão de fatura
        /// </summary>
public  class TicketInvoice
{

        /// <summary>
        /// ID do Boleto
        /// </summary>
        
        public int NIDBOL{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        
        public byte MODCAL{ get;set;} = 1;

        /// <summary>
        /// 
        /// </summary>
        
        public int CODEMP{ get;set;} = 0;

        /// <summary>
        /// Chave de Identificação do Boleto
        /// </summary>
        
        public System.Guid KEYBOL{ get;set;} = new Guid();

        /// <summary>
        /// 
        /// </summary>
        
        public byte SUBSYS{ get;set;} = 1;

        /// <summary>
        /// Id do Registro de Configuração de Boleto
        /// </summary>
        
        public int NIDCBL{ get;set;} = 0;

        /// <summary>
        /// Código do Usuário Gestor
        /// </summary>
        /// <remarks>
/// <para>O usuário gestor é determinado pela associação do usuário com o produto</para>
/// </remarks>
        
        public int USUPRO{ get;set;} = 0;

        /// <summary>
        /// Código do usuario cedente
        /// </summary>
        
        public int USUCED{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        
        public int USUSAC{ get;set;} = 0;

        /// <summary>
        /// Código do Avalista
        /// </summary>
        
        public int CODAVA{ get;set;} = 0;

        /// <summary>
        /// Código do Endereço
        /// </summary>
        
        public int CODEND{ get;set;} = 0;

        /// <summary>
        /// Codigo da Moeda
        /// </summary>
        
        public int CODMOE{ get;set;} = 1;

        /// <summary>
        /// Código do Status do Boleto
        /// </summary>
        
        public short STABOL{ get;set;} = 0;

        /// <summary>
        /// Tipo de Boleto
        /// </summary>
        
        public byte TIPBOL{ get;set;} = 1;

        /// <summary>
        /// Tipo de Baixa
        /// </summary>
        /// <remarks>
/// <para>0 - Baixa de Registro Bancário</para>
/// <para>1 - Baixa de Registro de Boleto</para>
/// </remarks>
        
        public byte TIPBXA{ get;set;} = 0;

        /// <summary>
        /// Tipo de Entrada de Associada
        /// </summary>
        /// <remarks>
/// <para>Pré-Cadastro</para>
/// <para></para>
/// <para>1 - Registro de Pré-Cadastro</para>
/// <para>2 - Registro para Embossing</para>
/// <para>3 - Venda de Cartão</para>
/// <para>Conta Corrente (Entry Mode)</para>
/// <para></para>
/// <para>1 - Registro de Transação Online</para>
/// <para>2 - Registro de Transação não online</para>
/// <para>3 - Saque</para>
/// </remarks>
        
        public byte TIPENT{ get;set;} = 1;

        /// <summary>
        /// Versão de Aplicação
        /// </summary>
        
        public short CODVER{ get;set;} = 3;

        /// <summary>
        /// Data de Emissão
        /// </summary>
        
        public DateTime DATEMI{ get;set;} = DateTime.Now;

        /// <summary>
        /// Data de Vencimento
        /// </summary>
        
        public DateTime DATVCT{ get;set;} = DateTime.Now;

        /// <summary>
        /// Data de Pagamento
        /// </summary>
        
        public DateTime? DATPGT{ get;set;} = null;

        /// <summary>
        /// Data de Validade
        /// </summary>
        
        public DateTime DATVAL{ get;set;} = DateTime.Now;

        /// <summary>
        /// Data de Registro
        /// </summary>
        
        public DateTime DATREG{ get;set;} = DateTime.Now;

        /// <summary>
        /// Número da Parcela
        /// </summary>
        
        public short NUMPCL{ get;set;} = 1;

        /// <summary>
        /// Código do Cedente
        /// </summary>
        
        public string CODCED{get;set;} = "";

        /// <summary>
        /// ID do Lote de Importação
        /// </summary>
        
        public int NIDLIM{ get;set;} = 0;

        /// <summary>
        /// IP de Origem
        /// </summary>
        
        public string NUMIPA{get;set;} = "";

        /// <summary>
        /// Origem do Boleto
        /// </summary>
        
        public int ORGBOL{ get;set;} = 1;

        /// <summary>
        /// Instrução Bancária 1
        /// </summary>
        
        public string INSBC1{get;set;} = "";

        /// <summary>
        /// Instrução Bancária 2
        /// </summary>
        
        public string INSBC2{get;set;} = "";

        /// <summary>
        /// Instrução Bancária 3
        /// </summary>
        
        public string INSBC3{get;set;} = "";

        /// <summary>
        /// Descrição do Boleto
        /// </summary>
        
        public string DSCBOL{get;set;} = "";

        /// <summary>
        /// Url de Retorno
        /// </summary>
        
        public string URLRET{get;set;} = "";

        /// <summary>
        /// Código de Retono
        /// </summary>
        
        public int CODRET{ get;set;} = 0;

        /// <summary>
        /// Nome do Arquivo de origem
        /// </summary>
        
        public string FILNAM{get;set;} = "";

        /// <summary>
        /// ID da Conta Virtual
        /// </summary>
        
        public int NIDCTA{ get;set;} = 0;

        /// <summary>
        /// Número de Dias antes do vencimento para envio da fatura 
        /// </summary>
        
        public short NUMDVF{ get;set;} = 10;

        /// <summary>
        /// Pedido do Cliente
        /// </summary>
        
        public string PEDCLI{get;set;} = "";

        /// <summary>
        /// Linha Digitável
        /// </summary>
        
        public string LINDIG{get;set;} = "";

        /// <summary>
        /// Espécie do Documento
        /// </summary>
        
        public string CODESP{get;set;} = "";

        /// <summary>
        /// Número da Carteira
        /// </summary>
        
        public string NUMCTR{get;set;} = "";

        /// <summary>
        /// Nosso Número
        /// </summary>
        /// <remarks>
/// <para>Número de Registro do boleto no Banco</para>
/// </remarks>
        
        public int NOSNUM{ get;set;} = 0;

        /// <summary>
        /// Se true, indica que a imagem do código de barras do boleto foi salva
        /// </summary>
        
        public bool IMGSAV{ get;set;} = false;

        /// <summary>
        /// Se true, indica que o registro foi conciliado pelo usuário
        /// </summary>
        
        public bool INDCNC{ get;set;} = false;

        /// <summary>
        /// Código de Aplicação de Cálculo
        /// </summary>
        /// <remarks>
/// <para>0 - Nenhum cálculo à ser aplicado</para>
/// <para></para>
/// <para>1 - Cálculo de Tarifa Administrativa</para>
/// <para>O cálculo é baseado no Código de Tarifa 10, aplicado somente se o gestor USUPRO estiver configurado com APLTAD=1</para>
/// </remarks>
        
        public byte APLCAL{ get;set;} = 0;

        /// <summary>
        /// Registro de Processamento
        /// </summary>
        
        public int REGPRO{ get;set;} = 0;

        /// <summary>
        /// Responsável pela Tarifa 
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 105</para>
/// </remarks>
        
        public byte RSPTAR{ get;set;} = 1;

        /// <summary>
        /// Valor do Boleto
        /// </summary>
        
        public double VLRBOL{ get;set;} = 0;

        /// <summary>
        /// Define se deve aplicar a Tarifa Externa ao controle de tarifação
        /// </summary>
        /// <remarks>
/// <para>Caso o valor atribuido seja 1, o valor da tarifa deverá ser fornecido</para>
/// </remarks>
        
        public byte APLTAR{ get;set;} = 0;

        /// <summary>
        /// Valor da Tarifa
        /// </summary>
        
        public double VLRTAR{ get;set;} = 0;

        /// <summary>
        /// Define se deve aplicar a Tarifa de Depositante
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 912</para>
/// </remarks>
        
        public byte APLTDP{ get;set;} = 0;

        /// <summary>
        /// Valor da Tarifa de Depositante
        /// </summary>
        
        public double VLRTDP{ get;set;} = 0;

        /// <summary>
        /// Código do Status de Registro
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 07</para>
/// </remarks>
        
        public int STAREC{ get;set;} = 1;

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
        /// *** Herdado de TBCADGER***
        /// </summary>
/* *** Herdado de TBCADGER*** */
        
        public string CEDNOM{get;set;} = "";

        /// <summary>
        /// CPF CEDENTE
        /// </summary>
        
        public string CEDCMF{get;set;} = "";

        /// <summary>
        /// *** Herdado de TBCADGER***
        /// </summary>
/* *** Herdado de TBCADGER*** */
        
        public string SACNOM{get;set;} = "";

        /// <summary>
        /// CPF/CNPJ do sacado
        /// </summary>
        
        public string SACCMF{get;set;} = "";

        /// <summary>
        /// Email do sacado*** Herdado de TBCADEND***
        /// </summary>
/* *** Herdado de TBCADEND*** */
        
        public string SACMAL{get;set;} = "";

        /// <summary>
        /// Nome do Avalista*** Herdado de TBCADGER***
        /// </summary>
/* *** Herdado de TBCADGER*** */
        
        public string AVANOM{get;set;} = "";

        /// <summary>
        /// CPF/CNPJ - Avalista
        /// </summary>
        
        public string AVACMF{get;set;} = "";

        /// <summary>
        /// Descrição do Tipo de Boleto*** Herdado de TBTABGER***
        /// </summary>
/* *** Herdado de TBTABGER*** */
        
        public string DSCBLT{get;set;} = "";

        /// <summary>
        /// Descrição do Status*** Herdado de TBCADSTA***
        /// </summary>
/* *** Herdado de TBCADSTA*** */
        
        public string DSCSTA{get;set;} = "";

        /// <summary>
        /// Valor líquido
        /// </summary>
        
        public double VLRLIQ{ get;set;} = 0;

        /// <summary>
        /// *** Herdado de TBCADGER***
        /// </summary>
/* *** Herdado de TBCADGER*** */
        
        public string NOMEMP{get;set;} = "";

}
        /// <summary>
        /// Objeto de Retenção para Registro de Boletos
        /// </summary>
public  class TicketRegister
{
 #region "Variáveis Privadas"        
private string _DSCLOG="";
        
private string _DSCEND="";
        
private string _DSCCPL="";
        
private string _DSCCID="";
        
private string _DSCBAI="";
 #endregion "Variáveis Privadas"
        /// <summary>
        /// ID do Boleto
        /// </summary>
        
        public int NIDBOL{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        
        public int CODEMP{ get;set;} = 0;

        /// <summary>
        /// Chave de Identificação do Boleto
        /// </summary>
        
        public System.Guid KEYBOL{ get;set;} = new Guid();

        /// <summary>
        /// 
        /// </summary>
        
        public byte SUBSYS{ get;set;} = 1;

        /// <summary>
        /// Id do Registro de Configuração de Boleto
        /// </summary>
        
        public int NIDCBL{ get;set;} = 0;

        /// <summary>
        /// Código do Usuário Gestor
        /// </summary>
        /// <remarks>
/// <para>O usuário gestor é determinado pela associação do usuário com o produto</para>
/// </remarks>
        
        public int USUPRO{ get;set;} = 0;

        /// <summary>
        /// Código do usuario cedente
        /// </summary>
        
        public int USUCED{ get;set;} = 0;

        /// <summary>
        /// *** Herdado de TBCADGER***
        /// </summary>
/* *** Herdado de TBCADGER*** */
        
        public string CEDNOM{get;set;} = "";

        /// <summary>
        /// CPF CEDENTE*** Herdado de TBCADGER***
        /// </summary>
/* *** Herdado de TBCADGER*** */
        
        public string CEDCMF{get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        
        public int USUSAC{ get;set;} = 0;

        /// <summary>
        /// *** Herdado de TBCADGER***
        /// </summary>
/* *** Herdado de TBCADGER*** */
        
        public string SACNOM{get;set;} = "";

        /// <summary>
        /// CPF/CNPJ do sacado*** Herdado de TBCADGER***
        /// </summary>
/* *** Herdado de TBCADGER*** */
        
        public string SACCMF{get;set;} = "";

        /// <summary>
        /// Email do sacado*** Herdado de TBCADEND***
        /// </summary>
/* *** Herdado de TBCADEND*** */
        
        public string SACMAL{get;set;} = "";

        /// <summary>
        /// Código do Avalista
        /// </summary>
        
        public int CODAVA{ get;set;} = 0;

        /// <summary>
        /// Nome do Avalista*** Herdado de TBCADGER***
        /// </summary>
/* *** Herdado de TBCADGER*** */
        
        public string AVANOM{get;set;} = "";

        /// <summary>
        /// CPF/CNPJ - Avalista*** Herdado de TBCADGER***
        /// </summary>
/* *** Herdado de TBCADGER*** */
        
        public string AVACMF{get;set;} = "";

        /// <summary>
        /// Codigo da Moeda
        /// </summary>
        
        public int CODMOE{ get;set;} = 1;

        /// <summary>
        /// Tipo de Boleto
        /// </summary>
        
        public byte TIPBOL{ get;set;} = 1;

        /// <summary>
        /// Data de Emissão
        /// </summary>
        
        public string DATEMI{get;set;} = "";

        /// <summary>
        /// Data de Vencimento
        /// </summary>
        
        public string DATVCT{get;set;} = "";

        /// <summary>
        /// Código do Cedente
        /// </summary>
        
        public string CODCED{get;set;} = "";

        /// <summary>
        /// ID do Lote de Importação
        /// </summary>
        
        public int NIDLIM{ get;set;} = 0;

        /// <summary>
        /// Instrução Bancária 1
        /// </summary>
        
        public string INSBC1{get;set;} = "";

        /// <summary>
        /// Instrução Bancária 2
        /// </summary>
        
        public string INSBC2{get;set;} = "";

        /// <summary>
        /// Instrução Bancária 3
        /// </summary>
        
        public string INSBC3{get;set;} = "";

        /// <summary>
        /// Descrição do Boleto
        /// </summary>
        
        public string DSCBOL{get;set;} = "";

        /// <summary>
        /// Linha Digitável
        /// </summary>
        
        public string LINDIG{get;set;} = "";

        /// <summary>
        /// Espécie do Documento
        /// </summary>
        
        public string CODESP{get;set;} = "";

        /// <summary>
        /// Número da Carteira
        /// </summary>
        
        public string NUMCTR{get;set;} = "";

        /// <summary>
        /// Nosso Número
        /// </summary>
        /// <remarks>
/// <para>Número de Registro do boleto no Banco</para>
/// </remarks>
        
        public int NOSNUM{ get;set;} = 0;

        /// <summary>
        /// Se true, indica que a imagem do código de barras do boleto foi salva
        /// </summary>
        
        public bool IMGSAV{ get;set;} = false;

        /// <summary>
        /// Responsável pela Tarifa 
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 105</para>
/// </remarks>
        
        public byte RSPTAR{ get;set;} = 1;

        /// <summary>
        /// Valor do Boleto
        /// </summary>
        
        public double VLRBOL{ get;set;} = 0;

        /// <summary>
        /// Valor da Tarifa
        /// </summary>
        
        public double VLRTAR{ get;set;} = 0;

        /// <summary>
        /// Valor da Tarifa de Depositante
        /// </summary>
        
        public double VLRTDP{ get;set;} = 0;

        /// <summary>
        /// Código do Status de Registro
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 07</para>
/// </remarks>
        
        public int STAREC{ get;set;} = 1;

        /// <summary>
        /// Descrição do Logradouro
        /// </summary>
        
        public string DSCLOG
        { 
            get { return _DSCLOG;}
            set { _DSCLOG= value.ToUpper().NoAccents();}
        }

        /// <summary>
        /// Endereço
        /// </summary>
        
        public string DSCEND
        { 
            get { return _DSCEND;}
            set { _DSCEND= value.ToUpper().NoAccents();}
        }

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
        
        public int NUMEND{ get;set;} = 0;

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
        
        public string CODCEP{get;set;} = "";

        /// <summary>
        /// Valor líquido
        /// </summary>
        
        public double VLRLIQ{ get;set;} = 0;

        /// <summary>
        /// Descrição do Status*** Herdado de TBCADSTA***
        /// </summary>
/* *** Herdado de TBCADSTA*** */
        
        public string DSCSTA{get;set;} = "";

}
}
