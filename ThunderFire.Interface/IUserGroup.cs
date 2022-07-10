using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for UserGroup
/// </summary>
    public interface IUserGroup
    {
           /// <summary>
    /// Insere um registro na tabela TBSYSUXG (User Group)
    /// </summary>
    ///<param name="model">UserGroup</param>
    /// <returns>int</returns>
ExecutionResponse Insert(UserGroup model);
    /// <summary>
    /// Altera um registro da tabela TBSYSUXG (User Group)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">UserGroup</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(UserGroup model);
    /// <summary>
    /// Obtêm o registro de associação do usuário e grupo
    /// </summary>
        /// <param name="pUSUGRP">ID de Associação Usuário x Grupo</param>
    /// <returns>UserGroup</returns>
UserGroup Select(System.Int32 pUSUGRP);
    /// <summary>
    /// Obtêm uma lista de todos os registros de usuário x grupo existentes
    /// </summary>
    /// <returns>List of Groups</returns>
List<Groups> List();

    }
}
