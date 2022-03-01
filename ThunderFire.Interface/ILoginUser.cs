using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for LoginUser
/// </summary>
    public interface ILoginUser
    {
           /// <summary>
    /// Insere um registro na tabela TBLGNUSU (Login de Usuário)
    /// </summary>
    ///<param name="model">LoginUser</param>
    /// <returns>int</returns>
ExecutionResponse Insert(LoginUser model);
    /// <summary>
    /// Altera um registro da tabela TBLGNUSU (Login de Usuário)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">LoginUser</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(LoginUser model);
    /// <summary>
    /// Obtêm o registro de Login do Usuário
    /// </summary>
        /// <param name="pLGNNUM">ID de Registro de Login</param>
    /// <returns>LoginUser</returns>
LoginUser Select(System.Int32 pLGNNUM);
    /// <summary>
    /// Obtêm o registro de Login Ativo do Usuário
    /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
    /// <returns>LoginUser</returns>
LoginUser Get(System.Int32 pCODUSU);
    /// <summary>
    /// Obtêm o registro de controle de acesso do usuário
    /// </summary>
        /// <param name="pLGNNUM">ID de Registro de Login</param>
    /// <returns>AccessControl</returns>
AccessControl GetAccessControl(System.Int32 pLGNNUM);
    /// <summary>
    /// Efetua o logoff de um usuario
    /// </summary>
/// <returns>ExecutionResponse</returns>
    /// <summary>
    /// Efetua um login com base nas credenciais de acesso
    /// </summary>
/// <returns>ExecutionResponse</returns>
    /// <summary>
    /// Verifica se o usuario precisa fazer um refresh de senha
    /// </summary>
/// <returns>ExecutionResponse</returns>
    /// <summary>
    /// Efetua um login com base nas credenciais de acesso
    /// </summary>
/// <returns>ExecutionResponse</returns>
    /// <summary>
    /// Efetua um login com base nas credenciais de acesso
    /// </summary>
/// <returns>ExecutionResponse</returns>

    }
}
