using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for AccountType
/// </summary>
    public interface IAccountType
    {
           /// <summary>
    /// Insere um registro na tabela TBTIPCTA (Tipos de Conta)
    /// </summary>
    ///<param name="model">AccountType</param>
    /// <returns>int</returns>
ExecutionResponse Insert(AccountType model);
    /// <summary>
    /// Altera um registro da tabela TBTIPCTA (Tipos de Conta)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">AccountType</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(AccountType model);
    /// <summary>
    /// Obtêm o registro do tipo de conta informado
    /// </summary>
        /// <param name="pTIPCTA">Tipo de Conta</param>
    /// <returns>AccountType</returns>
AccountType Select(System.Byte pTIPCTA);

    }
}
