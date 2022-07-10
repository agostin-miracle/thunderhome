using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBREGCRT Alias Active Cards
///</summary>

    public class ActiveCards
    {
               /// <summary>
        /// Código do Cartão 
        /// </summary>
        /// <remarks>
/// <para>Tabela de Próximos Números : 5</para>
/// </remarks>
        public int CODCRT{ get;set;} = 0;

        /// <summary>
        /// ID da Conta Virtual
        /// </summary>
        public int NIDCTA{ get;set;} = 0;

        /// <summary>
        /// Código do Usuário Gestor
        /// </summary>
        /// <remarks>
/// <para>O usuário gestor é determinado pela associação do usuário com o produto</para>
/// </remarks>
        public int USUPRO{ get;set;} = 0;

        /// <summary>
        /// Tipo de Produto associado ao registro de cartão
        /// </summary>
        public byte TIPPRO{ get;set;} = 0;

        /// <summary>
        /// Origem da Ativação do Cartão
        /// </summary>
        public byte ORGATV{ get;set;} = 0;

        /// <summary>
        /// Código do Usuário associado ao cartão
        /// </summary>
        public int ASSUSU{ get;set;} = 0;

        /// <summary>
        /// Nível de Segurança
        /// </summary>
        public byte SECLVL{ get;set;} = 3;

        /// <summary>
        /// Data de Ativação
        /// </summary>
        public DateTime? DATATV{ get;set;} = null;

        /// <summary>
        /// Data do Cancelamento
        /// </summary>
        public DateTime? DATCAN{ get;set;} = null;

        /// <summary>
        /// 0 - Não distribuir a mensalidade para o cartão@1 - Distribuir a mensalidade para o cartão@2 - Mensalidade distribuida@9 - Não existe configuração
        /// </summary>
        public byte CALMEN{ get;set;} = 0;

        /// <summary>
        /// Usuário de débito da mensalidade
        /// </summary>
        public int USUMEN{ get;set;} = 0;

        /// <summary>
        /// Nome do Portador do Cartão
        /// </summary>
        public string NOMPRT{ get;set;} = "";

        /// <summary>
        /// Número do Cartão
        /// </summary>
        public string NUMCRT{ get;set;} = "";

        /// <summary>
        /// Validade do Cartão
        /// </summary>
        public string VALCRT{ get;set;} = "00/00";

        /// <summary>
        /// Senha do Cartão
        /// </summary>
        public string PSWCRT{ get;set;} = "0000";

        /// <summary>
        /// Código de  Verificação do Cartão, código adicional impresso no verso do cartão de débito ou crédito. Na maioria dos cartões (Visa, MasterCard, cartões de bancos, etc.) é os últimos três dígitos impressos na faixa para assinatura localizada no verso do cartão.
        /// </summary>
        public short NUMCVC{ get;set;} = 0;

        /// <summary>
        /// Código do Status de Processamento
        /// </summary>
        public short STAPRO{ get;set;} = 0;

        /// <summary>
        /// Código do Status do Cartão
        /// </summary>
        public short STACRT{ get;set;} = 0;

        /// <summary>
        /// Parte 1 do Nome do Portador
        /// </summary>
        public string NOMCR1{ get;set;} = "";

        /// <summary>
        /// Parte 2 do Nome do Portador
        /// </summary>
        public string NOMCR2{ get;set;} = "";

        /// <summary>
        /// Parte 3 do Nome do Portador
        /// </summary>
        public string NOMCR3{ get;set;} = "";

        /// <summary>
        /// Operadora ou processadora do cartão.
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 71</para>
/// </remarks>
        public byte OPRCRT{ get;set;} = 0;

        /// <summary>
        /// Data da ultima solicitação de extrato
        /// </summary>
        public DateTime? DATEXT{ get;set;} = null;

        /// <summary>
        /// Código de bloqueio
        /// </summary>
        /// <remarks>
/// <para></para>
/// <para>0	Ativo	                                    Sem desbloqueio sem Reemissão</para>
/// <para>1	Bloqueio de Envio	                        Com desbloqueio sem Reemissão	Cartão bloqueado durante o processo de geração até a ativação pelo titular</para>
/// <para>10	Problema no Embossing	                    sem desbloqueio com Reemissão	Cartão apresenta algum erro de embossing que permite continuar utilização até que seja substituído</para>
/// <para>12	Pedido de Nova Via	                      sem desbloqueio com Reemissão	Cartão foi bloqueado por algum processo manual (ex.: interface do emissor) que solicitou a geração de uma nova via para o cartão. Via anterior permanece válida até que a nova via seja ativada</para>
/// <para>15	Bloqueado por Renovação	                  sem desbloqueio com Reemissão	Cartão foi bloqueado por processo automatizado que solicitou a geração de uma nova via para o cartão. Via anterior permanece válida até que a nova via seja ativada</para>
/// <para>18	Bloqueio de Segurança	                    Com desbloqueio sem Reemissão	Cartão bloqueado preventivamente pelo emissor por motivo de segurança. Poderá ser desbloqueado e liberado para uso após validações</para>
/// <para>21	Insucesso ou Extravio na Entrega	        Com Desbloqueio com Reemissão	Cartão dado como extraviado durante o processo de entrega. Mesmo que encontrado, não mais poderá ser utilizado</para>
/// <para>22	Extraviado Usuário	                      sem desbloqueio com Reemissão	Cartão dado como extraviado pelo seu usuário ou titular. Mesmo que encontrado pelo titular, não mais poderá ser utilizado</para>
/// <para>30	Bloqueio Preventivo de Fraude	            sem desbloqueio com Reemissão	Cartão bloqueado preventivamente pelo emissor, por suspeição de fraude. Poderá ser desbloqueado e liberado para uso apóss confirmação de uso e posse pelo titular</para>
/// <para>31	Fraude - Suspeita de Fraude 	            sem desbloqueio com Reemissão	Cartão identificado como tendo sido utilizado em situação suspeita, pelo seu titular ou por algum tipo de análise pelo emissor</para>
/// <para>32	Fraude - Conivência com o Estabelecimento	Sem desbloqueio sem Reemissão	Cartão identificado como envolvido em utilização fraudulenta, em conivência com algum estabelecimento também envolvido em atividade fraudulenta.</para>
/// <para>33	Fraude - Autofraude	                      Sem desbloqueio sem Reemissão	Cartão adulterado em alguma de suas características pelo próprio titular</para>
/// <para>34	Fraude - Falsificado	                    sem desbloqueio com Reemissão	Cartão falsificado ou adulterado</para>
/// <para>35	Perda	                                    Sem desbloqueio sem Reemissão	Cartão bloqueado por motivo de perda alegada pelo titular</para>
/// <para>36	Roubo	                                    Sem desbloqueio sem Reemissão	Cartão bloqueado por motivo de roubo alegado pelo titular</para>
/// <para>75	Cartão Expurgado	                        Sem desbloqueio sem Reemissão	Cartão excluido da base por processo automático que analisa condições contratuais, de utilização, saldo, inatividade, etc</para>
/// </remarks>
        public byte CODBLO{ get;set;} = 0;

        /// <summary>
        /// Tipo de Portador
        /// </summary>
        /// <remarks>
/// <para>main - titular</para>
/// <para>additional - Adicional</para>
/// </remarks>
        public byte TIPPRT{ get;set;} = 1;

        /// <summary>
        /// Descrição do Motivo de Bloqueio ou Aprovação
        /// </summary>
        public string DSCMOT{ get;set;} = "";

        /// <summary>
        /// Indica se o cartão pode fazer compras on line
        /// </summary>
        /// <remarks>
/// <para>0-Não</para>
/// <para>1 - Sim</para>
/// </remarks>
        public byte APLCON{ get;set;} = 1;

        /// <summary>
        /// Indica se o cartão pode fazer saques
        /// </summary>
        /// <remarks>
/// <para>0-Não</para>
/// <para>1 - Sim</para>
/// </remarks>
        public byte APLSAQ{ get;set;} = 1;

        /// <summary>
        /// NEXT NUMBER:41 ID do Controle Interno de Lote de Migração de Cartoes
        /// </summary>
        public int LOTMIG{ get;set;} = 0;

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
        /// Objeto de Retenção de Informações de Cartões Associados
        /// </summary>
public  class AssociatedCards
{

        /// <summary>
        /// Código do Cartão 
        /// </summary>
        /// <remarks>
/// <para>Tabela de Próximos Números : 5</para>
/// </remarks>
        
        public int? CODCRT{ get;set;} = 0;

        /// <summary>
        /// Codigo do Produto
        /// </summary>
        
        public short? CODPRO{ get;set;} = 0;

        /// <summary>
        /// Número do Cartão
        /// </summary>
        
        public string NUMCRT{get;set;} = "";

        /// <summary>
        /// Validade do Cartão
        /// </summary>
        
        public string VALCRT{get;set;} = "";

        /// <summary>
        /// Nome do Portador do Cartão
        /// </summary>
        
        public string NOMPRT{get;set;} = "";

}
public class QueryActiveCards
{
         #region "Variáveis Privadas"
        private string _NOMUSU="";
         #endregion "Variáveis Privadas"

        /// <summary>
        /// Código do Cartão 
        /// </summary>
        /// <remarks>
/// <para>Tabela de Próximos Números : 5</para>
/// </remarks>
        public int CODCRT{ get;set;}

        /// <summary>
        /// ID da Conta Virtual
        /// </summary>
        public int NIDCTA{ get;set;}

        /// <summary>
        /// Código do Usuário Gestor
        /// </summary>
        /// <remarks>
/// <para>O usuário gestor é determinado pela associação do usuário com o produto</para>
/// </remarks>
        public int USUPRO{ get;set;}

        /// <summary>
        /// Tipo de Produto associado ao registro de cartão
        /// </summary>
        public byte TIPPRO{ get;set;}

        /// <summary>
        /// Origem da Ativação do Cartão
        /// </summary>
        public byte ORGATV{ get;set;}

        /// <summary>
        /// Código do Usuário associado ao cartão
        /// </summary>
        public int ASSUSU{ get;set;}

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string NOMUSU
        { 
            get { return _NOMUSU;}
            set { _NOMUSU= value.ToUpper().NoAccents();}
        }

        /// <summary>
        /// Nível de Segurança
        /// </summary>
        public byte SECLVL{ get;set;}

        /// <summary>
        /// Data de Ativação
        /// </summary>
        public DateTime? DATATV{ get;set;}

        /// <summary>
        /// Data de Ativação convertida para o formato dd/MM/yyyy
        /// </summary>
        public string CNVATV{ get;set;} = "";

        /// <summary>
        /// Data do Cancelamento
        /// </summary>
        public DateTime? DATCAN{ get;set;}

        /// <summary>
        /// Data de Cancelamento convertida para o formato dd/MM/yyyy
        /// </summary>
        public string CNVCAN{ get;set;} = "";

        /// <summary>
        /// 0 - Não distribuir a mensalidade para o cartão@1 - Distribuir a mensalidade para o cartão@2 - Mensalidade distribuida@9 - Não existe configuração
        /// </summary>
        public byte CALMEN{ get;set;}

        /// <summary>
        /// Usuário de débito da mensalidade
        /// </summary>
        public int USUMEN{ get;set;}

        /// <summary>
        /// 
        /// </summary>
        public string DSCTOM{ get;set;} = "";

        /// <summary>
        /// Nome do Portador do Cartão
        /// </summary>
        public string NOMPRT{ get;set;} = "";

        /// <summary>
        /// Número do Cartão
        /// </summary>
        public string NUMCRT{ get;set;} = "";

        /// <summary>
        /// Validade do Cartão
        /// </summary>
        public string VALCRT{ get;set;} = "00/00";

        /// <summary>
        /// Senha do Cartão
        /// </summary>
        public string PSWCRT{ get;set;} = "0000";

        /// <summary>
        /// Código de  Verificação do Cartão, código adicional impresso no verso do cartão de débito ou crédito. Na maioria dos cartões (Visa, MasterCard, cartões de bancos, etc.) é os últimos três dígitos impressos na faixa para assinatura localizada no verso do cartão.
        /// </summary>
        public short NUMCVC{ get;set;}

        /// <summary>
        /// Código do Status de Processamento
        /// </summary>
        public short STAPRO{ get;set;}

        /// <summary>
        /// Código do Status do Cartão
        /// </summary>
        public short STACRT{ get;set;}

        /// <summary>
        /// Descrição do Status
        /// </summary>
        public string DSCSTA{ get;set;} = "";

        /// <summary>
        /// Parte 1 do Nome do Portador
        /// </summary>
        public string NOMCR1{ get;set;} = "";

        /// <summary>
        /// Parte 2 do Nome do Portador
        /// </summary>
        public string NOMCR2{ get;set;} = "";

        /// <summary>
        /// Parte 3 do Nome do Portador
        /// </summary>
        public string NOMCR3{ get;set;} = "";

        /// <summary>
        /// Operadora ou processadora do cartão.
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 71</para>
/// </remarks>
        public byte OPRCRT{ get;set;}

        /// <summary>
        /// Data da ultima solicitação de extrato
        /// </summary>
        public DateTime? DATEXT{ get;set;}

        /// <summary>
        /// Código de bloqueio
        /// </summary>
        /// <remarks>
/// <para></para>
/// <para>0	Ativo	                                    Sem desbloqueio sem Reemissão</para>
/// <para>1	Bloqueio de Envio	                        Com desbloqueio sem Reemissão	Cartão bloqueado durante o processo de geração até a ativação pelo titular</para>
/// <para>10	Problema no Embossing	                    sem desbloqueio com Reemissão	Cartão apresenta algum erro de embossing que permite continuar utilização até que seja substituído</para>
/// <para>12	Pedido de Nova Via	                      sem desbloqueio com Reemissão	Cartão foi bloqueado por algum processo manual (ex.: interface do emissor) que solicitou a geração de uma nova via para o cartão. Via anterior permanece válida até que a nova via seja ativada</para>
/// <para>15	Bloqueado por Renovação	                  sem desbloqueio com Reemissão	Cartão foi bloqueado por processo automatizado que solicitou a geração de uma nova via para o cartão. Via anterior permanece válida até que a nova via seja ativada</para>
/// <para>18	Bloqueio de Segurança	                    Com desbloqueio sem Reemissão	Cartão bloqueado preventivamente pelo emissor por motivo de segurança. Poderá ser desbloqueado e liberado para uso após validações</para>
/// <para>21	Insucesso ou Extravio na Entrega	        Com Desbloqueio com Reemissão	Cartão dado como extraviado durante o processo de entrega. Mesmo que encontrado, não mais poderá ser utilizado</para>
/// <para>22	Extraviado Usuário	                      sem desbloqueio com Reemissão	Cartão dado como extraviado pelo seu usuário ou titular. Mesmo que encontrado pelo titular, não mais poderá ser utilizado</para>
/// <para>30	Bloqueio Preventivo de Fraude	            sem desbloqueio com Reemissão	Cartão bloqueado preventivamente pelo emissor, por suspeição de fraude. Poderá ser desbloqueado e liberado para uso apóss confirmação de uso e posse pelo titular</para>
/// <para>31	Fraude - Suspeita de Fraude 	            sem desbloqueio com Reemissão	Cartão identificado como tendo sido utilizado em situação suspeita, pelo seu titular ou por algum tipo de análise pelo emissor</para>
/// <para>32	Fraude - Conivência com o Estabelecimento	Sem desbloqueio sem Reemissão	Cartão identificado como envolvido em utilização fraudulenta, em conivência com algum estabelecimento também envolvido em atividade fraudulenta.</para>
/// <para>33	Fraude - Autofraude	                      Sem desbloqueio sem Reemissão	Cartão adulterado em alguma de suas características pelo próprio titular</para>
/// <para>34	Fraude - Falsificado	                    sem desbloqueio com Reemissão	Cartão falsificado ou adulterado</para>
/// <para>35	Perda	                                    Sem desbloqueio sem Reemissão	Cartão bloqueado por motivo de perda alegada pelo titular</para>
/// <para>36	Roubo	                                    Sem desbloqueio sem Reemissão	Cartão bloqueado por motivo de roubo alegado pelo titular</para>
/// <para>75	Cartão Expurgado	                        Sem desbloqueio sem Reemissão	Cartão excluido da base por processo automático que analisa condições contratuais, de utilização, saldo, inatividade, etc</para>
/// </remarks>
        public byte CODBLO{ get;set;}

        /// <summary>
        /// Tipo de Portador
        /// </summary>
        /// <remarks>
/// <para>main - titular</para>
/// <para>additional - Adicional</para>
/// </remarks>
        public byte TIPPRT{ get;set;}

        /// <summary>
        /// Descrição do Motivo de Bloqueio ou Aprovação
        /// </summary>
        public string DSCMOT{ get;set;} = "";

        /// <summary>
        /// Indica se o cartão pode fazer compras on line
        /// </summary>
        /// <remarks>
/// <para>0-Não</para>
/// <para>1 - Sim</para>
/// </remarks>
        public byte APLCON{ get;set;}

        /// <summary>
        /// Indica se o cartão pode fazer saques
        /// </summary>
        /// <remarks>
/// <para>0-Não</para>
/// <para>1 - Sim</para>
/// </remarks>
        public byte APLSAQ{ get;set;}

        /// <summary>
        /// NEXT NUMBER:41 ID do Controle Interno de Lote de Migração de Cartoes
        /// </summary>
        public int LOTMIG{ get;set;}

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
        /// Data de Cadastro convertida para o formato dd/MM/yyyy HH:mm
        /// </summary>
        public string CNVCAD{ get;set;} = "";

        /// <summary>
        /// Data da Ultima Atualização
        /// </summary>
        public DateTime DATUPD{ get;set;}

        /// <summary>
        /// Data de Atualização convertida para o formato dd/MM/yyyy HH:mm
        /// </summary>
        public string CNVUPD{ get;set;} = "";

        /// <summary>
        /// Usuário de Atualização
        /// </summary>
        public int UPDUSU{ get;set;}

        /// <summary>
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        public string LGNUSU{ get;set;} = "";

        /// <summary>
        /// Descrição do Produto
        /// </summary>
        public string DSCPRO{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public int QTDMEN{ get;set;}

}}
