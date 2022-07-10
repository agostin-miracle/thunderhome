using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for Operations
/// </summary>
    public interface IOperations
    {
           /// <summary>
    /// Insere um registro na tabela TBTIPMOV (Operations)
    /// </summary>
    ///<param name="model">Operations</param>
    /// <returns>int</returns>
ExecutionResponse Insert(Operations model);
    /// <summary>
    /// Altera um registro da tabela TBTIPMOV (Operations)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">Operations</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(Operations model);
    /// <summary>
    /// Obtêm o registro de operação com base no id informado
    /// </summary>
        /// <param name="pCODMOV">Código da Tarifa</param>
    /// <returns>Operations</returns>
Operations Select(System.Int16 pCODMOV);

    }
}
