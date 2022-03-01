using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for ProductManagement
/// </summary>
    public interface IProductManagement
    {
           /// <summary>
    /// Insere um registro na tabela TBUSUPRO (Gerencia de Produtos)
    /// </summary>
    ///<param name="model">ProductManagement</param>
    /// <returns>int</returns>
ExecutionResponse Insert(ProductManagement model);
    /// <summary>
    /// 
    /// </summary>
    ///<param name="model">ProductManagement</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(ProductManagement model);
    /// <summary>
    /// Obtêm o registro de Gerencia de Produto de acordo com ocódigo do gestor
    /// </summary>
        /// <param name="pUSUPRO">ID do Gestor</param>
    /// <returns>ProductManagement</returns>
ProductManagement Select(System.Int32 pUSUPRO);
    /// <summary>
    /// Obtêm o Código do Gestor de Produto com base no produto e usuário
    /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pCODPRO">Código do Produto</param>
    /// <returns>ProductManagement</returns>
ProductManagement Select(System.Int32 pCODUSU, System.Int16 pCODPRO);
    /// <summary>
    /// Obtêm uma lista de todos os registros de Gestores de Produtos de acordo com o usuario e/ou  o produto informado
    /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pCODPRO">Código do Produto</param>

    /// <returns>List of QueryProductManagement</returns>
List<QueryProductManagement> List(System.Int32? pCODUSU= null, System.Int16? pCODPRO= null);

    }
}
