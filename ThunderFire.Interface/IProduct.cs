using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for Product
/// </summary>
    public interface IProduct
    {
           /// <summary>
    /// Insere um registro na tabela TBCADPRO (Products)
    /// </summary>
    ///<param name="model">Product</param>
    /// <returns>int</returns>
ExecutionResponse Insert(Product model);
    /// <summary>
    /// Altera um registro da tabela TBCADPRO (Products)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">Product</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(Product model);
    /// <summary>
    /// Obtêm o registro do produto de acorco o código de produto informado
    /// </summary>
        /// <param name="pCODPRO">Código do Produto</param>
    /// <returns>Product</returns>
Product Select(System.Int32 pCODPRO);

    }
}
