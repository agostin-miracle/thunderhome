using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for ProductManager
/// </summary>
    public interface IProductManager
    {
           /// <summary>
    /// Insere um registro na tabela TBUSUPRO (Gestores de Produtos)
    /// </summary>
    ///<param name="model">ProductManager</param>
    /// <returns>int</returns>
ExecutionResponse Insert(ProductManager model);
    /// <summary>
    /// 
    /// </summary>
    ///<param name="model">ProductManager</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(ProductManager model);
    /// <summary>
    /// Obtêm o registro de Gerencia de Produto de acordo com ocódigo do gestor
    /// </summary>
        /// <param name="pUSUPRO">ID do Gestor</param>
    /// <returns>ProductManager</returns>
ProductManager Select(System.Int32 pUSUPRO);
    /// <summary>
    /// Obtêm o Código do Gestor de Produto com base no produto e usuário
    /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pCODPRO">Código do Produto</param>
    /// <returns>ProductManager</returns>
ProductManager Select(System.Int32 pCODUSU, System.Int16 pCODPRO);

    }
}
