using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for ActiveCards
/// </summary>
    public interface IActiveCards
    {
           /// <summary>
    /// Insere um registro na tabela TBREGCRT (Active Cards)
    /// </summary>
    ///<param name="model">ActiveCards</param>
    /// <returns>int</returns>
ExecutionResponse Insert(ActiveCards model);
    /// <summary>
    /// Altera um registro da tabela TBREGCRT (Active Cards)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">ActiveCards</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(ActiveCards model);
    /// <summary>
    /// Seleciona o registro de acordo com o Código do Cartão
    /// </summary>
        /// <param name="pCODCRT">Código do Cartão</param>
    /// <returns>ActiveCards</returns>
ActiveCards Select(System.Int32 pCODCRT);
    /// <summary>
    /// Seleciona o registro de acordo com o Código Extendido do Cartão
    /// </summary>
        /// <param name="pNUMCRT">Código Extendido do Cartão</param>
    /// <returns>ActiveCards</returns>
ActiveCards Select(System.String pNUMCRT);
    /// <summary>
    /// Altera o campo de permissão de compra on-line
    /// </summary>
/// <returns>ExecutionResponse</returns>
ExecutionResponse  ChangeOnlineShop( System.Int32 pCODCRT,System.Int32 pUPDUSU,System.Byte pAPLCON);
    /// <summary>
    /// Altera o campo de permissão de saque
    /// </summary>
/// <returns>ExecutionResponse</returns>
ExecutionResponse  ChangeWithdraw( System.Int32 pCODCRT,System.Int32 pUPDUSU,System.Byte pAPLSAQ);
    /// <summary>
    /// Altera o Gestor do Cartão em uso
    /// </summary>
/// <returns>ExecutionResponse</returns>
ExecutionResponse  ChangeOwner( System.Int32 pCODCRT,System.Int32 pUPDUSU,System.Int32 pUSUPRO = 13);
    /// <summary>
    /// Executa Ações de Atualização pela manutenção do Cartão
    /// </summary>
/// <returns>ExecutionResponse</returns>
ExecutionResponse  CancelMonthlyFees( System.Int32 pCODCRT,System.Int32 pUPDUSU,System.Byte pCODACT);
    /// <summary>
    /// Verifica se o cartão e o CPF/CNPJ informado são passíveis de ativação
    /// </summary>
/// <returns>int</returns>
int IsActivatable( System.String pNUMCRT,System.String pCODCMF);
    /// <summary>
    /// Operação@1 -Bloqueia o Cartão@2 - Reverte o Bloqueio Anterior@3 -Fixa o cartão como aguardando desbloqueio
    /// </summary>
/// <returns>ExecutionResponse</returns>
ExecutionResponse  LockCard( System.Int32 pCODCRT,System.Int32 pUPDUSU,System.Byte pTIPOPE);
    /// <summary>
    /// Altera o Status de um Cartão
    /// </summary>
/// <returns>ExecutionResponse</returns>
ExecutionResponse  ChangeStatus( System.Int32 pCODCRT,System.Int16 pSTACRT,System.Int32 pUPDUSU,System.Int16 pCODBLO = 0,System.String pDSCMOT = "''");
    /// <summary>
    /// Efetua a alteração da senha do cartão
    /// </summary>
/// <returns>ExecutionResponse</returns>
ExecutionResponse  ChangePin( System.Int32 pCODCRT,System.String pPSWCRT,System.Int32 pUPDUSU,System.String pNUMIPA);
    /// <summary>
    /// Efetua a alteração do número o CVC do Cartão
    /// </summary>
/// <returns>ExecutionResponse</returns>
ExecutionResponse  ChangeCVC( System.Int32 pCODCRT,System.Int16 pNUMCVC,System.Int32 pUPDUSU);
    /// <summary>
    /// Cancela a associação de um cartão
    /// </summary>
/// <returns>ExecutionResponse</returns>
ExecutionResponse  CancelAssociation( System.Int32 pCODCRT,System.Int32 pUPDUSU);
    /// <summary>
    /// Obtêm uma lista de todos os cartões da base ativa conforme parâmetros de pesquisa informados
    /// </summary>
        /// <param name="pUSUPRO">Usuário Gestor</param>
    /// <param name="pSTACRT">Status do Cartão</param>
    /// <param name="pNUMCRT">Parte ou Número do Cartão</param>
    /// <param name="pNOMPRT">Nome ou Parte</param>

    /// <returns>List of QueryActiveCards</returns>
List<QueryActiveCards> List(System.Int32? pUSUPRO= null, System.Int16? pSTACRT= null, string pNUMCRT= "", string pNOMPRT= null);

    }
}
