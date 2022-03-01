using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for ProductLine
/// </summary>
    public interface IProductLine
    {
           /// <summary>
    /// Insere um registro na tabela TBLINPRO (Linha de Produto)
    /// </summary>
    ///<param name="model">ProductLine</param>
    /// <returns>int</returns>
ExecutionResponse Insert(ProductLine model);
    /// <summary>
    /// Altera um registro da tabela TBLINPRO (Linha de Produto)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">ProductLine</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(ProductLine model);
    /// <summary>
    /// Seleciona o registro de acordo com o Código da Linha
    /// </summary>
        /// <param name="pLINPRO">Linha de Produto</param>
    /// <returns>ProductLine</returns>
ProductLine Select(System.Int32 pLINPRO);
    /// <summary>
    /// Obtêm todos os registros de linha de produtos existentes
    /// </summary>
    /// <returns>List of ProductLine</returns>
List<ProductLine> List();

    }
}
