using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for TariffType
/// </summary>
    public interface ITariffType
    {
           /// <summary>
    /// Insere um registro na tabela TBTIPTAR (Tariff Type)
    /// </summary>
    ///<param name="model">TariffType</param>
    /// <returns>int</returns>
ExecutionResponse Insert(TariffType model);
    /// <summary>
    /// Altera um registro da tabela TBTIPTAR (Tariff Type)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">TariffType</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(TariffType model);
    /// <summary>
    /// Obtêm o registro de Tipo de Tarifa com base no id informado
    /// </summary>
        /// <param name="pCODTAR">Tipo de Tarifa</param>
    /// <returns>TariffType</returns>
TariffType Select(System.Byte pCODTAR);
/// <summary>
/// Obtêm uma Lista dos Tipos de Tarifa
/// </summary>
/// <returns>Listof TariffType</returns>
List<TariffType> List();

    }
}
