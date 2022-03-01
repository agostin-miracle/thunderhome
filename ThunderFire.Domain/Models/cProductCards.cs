using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBCADCRT Alias Cadastro de Cartões
///</summary>

    public class ProductCards
    {
               /// <summary>
        /// Código do Cartão 
        /// </summary>
        /// <remarks>
/// <para>Tabela de Próximos Números : 5</para>
/// </remarks>
        public int CODCRT{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public int USUGST{ get;set;} = 0;

        /// <summary>
        /// Identificador único da conta atribuída pelo emissor.
        /// </summary>
        /// <remarks>
/// <para>PaySmart</para>
/// </remarks>
        public string KEYACC{ get;set;} = "";

        /// <summary>
        /// Identificador único do cartão atribuído pelo emissor.
        /// </summary>
        public string KEYCRT{ get;set;} = "";

        /// <summary>
        /// ID do Lote de Importação
        /// </summary>
        public int NIDLIM{ get;set;} = 0;

        /// <summary>
        /// Número do Lote
        /// </summary>
        public int NUMLOT{ get;set;} = 0;

        /// <summary>
        /// Número do Cartão
        /// </summary>
        public string NUMCRT{ get;set;} = "";

        /// <summary>
        /// Validade do Cartão
        /// </summary>
        public string VALCRT{ get;set;} = "00/00";

        /// <summary>
        /// Código de  Verificação do Cartão
        /// </summary>
        public short NUMCVC{ get;set;} = 0;

        /// <summary>
        /// Código do Status do Cartão
        /// </summary>
        public short STACRT{ get;set;} = 0;

        /// <summary>
        /// Senha do Cartão
        /// </summary>
        public string PSWCRT{ get;set;} = "";

        /// <summary>
        /// Nome do Arquivo de origem
        /// </summary>
        public string FILNAM{ get;set;} = "";

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
        /// 
        /// </summary>
        public int CODFOR{ get;set;} = 0;

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
}
