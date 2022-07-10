using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for UserType
/// </summary>
    public interface IUserType
    {
           /// <summary>
    /// Insere um registro na tabela TBTIPUSU (Tipo de Usuário)
    /// </summary>
    ///<param name="model">UserType</param>
    /// <returns>int</returns>
ExecutionResponse Insert(UserType model);
    /// <summary>
    /// Altera um registro da tabela TBTIPUSU (Tipo de Usuário)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">UserType</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(UserType model);
    /// <summary>
    /// Obtêm o registro do Tipo de Usuário fornecido
    /// </summary>
        /// <param name="pTIPUSU">Tipo de Usuário</param>
    /// <returns>UserType</returns>
UserType Select(System.Byte pTIPUSU);

    }
}
