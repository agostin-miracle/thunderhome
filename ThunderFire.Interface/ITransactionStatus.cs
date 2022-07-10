using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for TransactionStatus
/// </summary>
    public interface ITransactionStatus
    {
           /// <summary>
    /// Insere um registro na tabela TBCADSTA (Transaction Status)
    /// </summary>
    ///<param name="model">TransactionStatus</param>
    /// <returns>int</returns>
ExecutionResponse Insert(TransactionStatus model);
    /// <summary>
    /// Altera um registro da tabela TBCADSTA (Transaction Status)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">TransactionStatus</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(TransactionStatus model);
    /// <summary>
    /// Seleciona o registro de acordo com o codigo do status fornecido
    /// </summary>
        /// <param name="pCODSTA">Código do Status</param>
    /// <returns>TransactionStatus</returns>
TransactionStatus Select(System.Int32 pCODSTA);

    }
}
