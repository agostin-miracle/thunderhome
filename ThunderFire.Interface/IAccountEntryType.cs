using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for AccountEntryType
/// </summary>
    public interface IAccountEntryType
    {
           /// <summary>
    /// Insere um registro na tabela TBTIPLCT (Account Entry Type)
    /// </summary>
    ///<param name="model">AccountEntryType</param>
    /// <returns>int</returns>
ExecutionResponse Insert(AccountEntryType model);
    /// <summary>
    /// Altera um registro da tabela TBTIPLCT (Account Entry Type)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">AccountEntryType</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(AccountEntryType model);
    /// <summary>
    /// Obtêm o registro de Tipo de Lançamento com base no id informado
    /// </summary>
        /// <param name="pTIPLCT">Tipo de Lançamento</param>
    /// <returns>AccountEntryType</returns>
AccountEntryType Select(System.Int16 pTIPLCT);

    }
}
