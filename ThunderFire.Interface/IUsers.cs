using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for Users
/// </summary>
    public interface IUsers
    {
           /// <summary>
    /// Insere um registro na tabela TBCADGER (Cadastro Geral de Usuários)
    /// </summary>
    ///<param name="model">Users</param>
    /// <returns>int</returns>
ExecutionResponse Insert(Users model);
    /// <summary>
    /// Altera um registro da tabela TBCADGER (Cadastro Geral de Usuários)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">Users</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(Users model);
    /// <summary>
    /// Seleciona o registro de acordo com o Código do Usuário
    /// </summary>
        /// <param name="pCODUSU">Código do Usuario</param>
    /// <returns>Users</returns>
Users Select(System.Int32 pCODUSU);
    /// <summary>
    /// Obtêm o nome de login default do aplicativo
    /// </summary>
        /// <param name="pNOMUSU">Nome do Usuário</param>
    /// <returns>string</returns>
string GetDefaultLoginName(System.String pNOMUSU);

    }
}
