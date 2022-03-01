using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for ProductCards
/// </summary>
    public interface IProductCards
    {
           /// <summary>
    /// Insere um registro na tabela TBCADCRT (Cadastro de Cartões)
    /// </summary>
    ///<param name="model">ProductCards</param>
    /// <returns>int</returns>
ExecutionResponse Insert(ProductCards model);
    /// <summary>
    /// Efetua a migração de Registros da tabela TBCADCRT (Cadastro de Cartões)
    /// </summary>
    ///<param name="model">ActiveCards</param>
    /// <returns>int</returns>
ExecutionResponse MigrateTo(ActiveCards model);
    /// <summary>
    /// Altera um registro da tabela TBCADCRT (Cadastro de Cartões)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">ProductCards</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(ProductCards model);
    /// <summary>
    /// Seleciona o registro de acordo com o Código do Cartão
    /// </summary>
        /// <param name="pCODCRT">Código do Cartão</param>
    /// <returns>ProductCards</returns>
ProductCards Select(System.Int32 pCODCRT);
    /// <summary>
    /// Seleciona o registro de acordo com o Código Extendido do Cartão
    /// </summary>
        /// <param name="pNUMCRT">Código Extendido do Cartão</param>
    /// <returns>ProductCards</returns>
ProductCards Select(System.String pNUMCRT);
    /// <summary>
    /// Verifica se o cartão já foi alocado para um portador
    /// </summary>
/// <returns>int</returns>
    /// <summary>
    /// ADM : Altera o Status de um Cartão
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

    }
}
