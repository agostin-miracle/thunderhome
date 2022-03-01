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
    /// Insere um registro na tabela TBREGCRT (Registro de Cartões Ativos)
    /// </summary>
    ///<param name="model">ActiveCards</param>
    /// <returns>int</returns>
ExecutionResponse Insert(ActiveCards model);
    /// <summary>
    /// Altera um registro da tabela TBREGCRT (Registro de Cartões Ativos)  de acordo com a chave primaria
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
    /// Obtêm o registro do cartão de acordo com a chave fornecida
    /// </summary>
        /// <param name="pKEYCRT">Código Chave do Cartão</param>
    /// <returns>ActiveCards</returns>
ActiveCards GetKeyCard(System.String pKEYCRT);
    /// <summary>
    /// Altera o campo de permissão de compra on-line
    /// </summary>
/// <returns>ExecutionResponse</returns>
    /// <summary>
    /// Altera o Gestor do Cartão em uso
    /// </summary>
/// <returns>ExecutionResponse</returns>
    /// <summary>
    /// Executa Ações de Atualização pela manutenção do Cartão
    /// </summary>
/// <returns>ExecutionResponse</returns>
    /// <summary>
    /// Verifica se o cartão e o CPF/CNPJ informado são passíveis de ativação
    /// </summary>
/// <returns>int</returns>
    /// <summary>
    /// Operação@1 -Bloqueia o Cartão@2 - Reverte o Bloqueio Anterior@3 -Fixa o cartão como aguardando desbloqueio
    /// </summary>
/// <returns>ExecutionResponse</returns>
    /// <summary>
    /// Altera o Status de um Cartão
    /// </summary>
/// <returns>ExecutionResponse</returns>
    /// <summary>
    /// Efetua a alteração da senha do cartão
    /// </summary>
/// <returns>ExecutionResponse</returns>
    /// <summary>
    /// Efetua a alteração do número o CVC do Cartão
    /// </summary>
/// <returns>ExecutionResponse</returns>
    /// <summary>
    /// Efetua o cancelamento da associação de um cartão
    /// </summary>
/// <returns>ExecutionResponse</returns>
    /// <summary>
    /// Obtêm uma lista de todos os cartões da base ativa conforme parâmetros de pesquisa informados
    /// </summary>
        /// <param name="pSTACRT">Status do Cartão</param>
    /// <param name="pNUMLOT">Número do Lote</param>
    /// <param name="pASSUSU">Usuário Associado</param>
    /// <param name="pUSUMEN">Usuário Mensalidade</param>
    /// <param name="pNUMCRT">Número do Cartão</param>
    /// <param name="pUSUPRO">Usuário Gestor</param>
    /// <param name="pNOMSRC">Nome ou Parte</param>

    /// <returns>List of ActiveCardsQuery</returns>
List<ActiveCardsQuery> SelectAll(System.Int16? pSTACRT= null, System.Int32? pNUMLOT= null, System.Int32? pASSUSU= null, System.Int32? pUSUMEN= null, string pNUMCRT= "", System.Int32? pUSUPRO= null, string pNOMSRC= null);

    }
}
