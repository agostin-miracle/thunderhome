using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for Products
/// </summary>
    public interface IProducts
    {
           /// <summary>
    /// Insere um registro na tabela TBCADPRO (Products)
    /// </summary>
    ///<param name="model">Products</param>
    /// <returns>int</returns>
ExecutionResponse Insert(Products model);
    /// <summary>
    /// Altera um registro da tabela TBCADPRO (Products)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">Products</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(Products model);
    /// <summary>
    /// Obtêm o registro do produto de acorco o código de produto informado
    /// </summary>
        /// <param name="pCODPRO">Código do Produto</param>
    /// <returns>Products</returns>
Products Select(System.Int32 pCODPRO);
    /// <summary>
    /// Obtêm uma lista de todos os produtos
    /// </summary>
        /// <param name="pLINPRO">Linha de Produto</param>

    /// <returns>List of QueryProducts</returns>
List<QueryProducts> List(System.Int16? pLINPRO);

    }
}
