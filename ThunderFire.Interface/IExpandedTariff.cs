using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for ExpandedTariff
/// </summary>
    public interface IExpandedTariff
    {
           /// <summary>
    /// Insere um registro na tabela TBEXPTAR (Expanded Tariff)
    /// </summary>
    ///<param name="model">ExpandedTariff</param>
    /// <returns>int</returns>
ExecutionResponse Insert(ExpandedTariff model);
    /// <summary>
    /// Altera um registro da tabela TBEXPTAR (Expanded Tariff)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">ExpandedTariff</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(ExpandedTariff model);
    /// <summary>
    /// Seleciona o registro de acordo com o codigo do produto
    /// </summary>
        /// <param name="pNIDEXP">ID do Registro de Expansão</param>
    /// <returns>ExpandedTariff</returns>
ExpandedTariff Select(int pNIDEXP);
/// <summary>
/// Seleciona todos os registros de expansão de tarifa do id de tarifação fornecido
/// </summary>
    /// <param name="pTIPEXP">Código da Expansão</param>
/// <returns>Listof ExpandedTariff</returns>
List<ExpandedTariff> List(System.Int16? pTIPEXP);
/// <summary>
/// Obtêm uma Lista das Expansões Associadas
/// </summary>
/// <returns>Listof ExpandedTypes</returns>
List<ExpandedTypes> ListTypes();

    }
}
