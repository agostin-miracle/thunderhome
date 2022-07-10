using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for TariffModality
/// </summary>
    public interface ITariffModality
    {
           /// <summary>
    /// Insere um registro na tabela TBMODTAR (Tariff Modality)
    /// </summary>
    ///<param name="model">TariffModality</param>
    /// <returns>int</returns>
ExecutionResponse Insert(TariffModality model);
    /// <summary>
    /// Altera um registro da tabela TBMODTAR (Tariff Modality)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">TariffModality</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(TariffModality model);
    /// <summary>
    /// Seleciona o registro de acordo com o codigo dA modalidade
    /// </summary>
        /// <param name="pMODCRT">Modalidade de Aplicação da Tarifa</param>
    /// <returns>TariffModality</returns>
TariffModality Select(int pMODCRT);
/// <summary>
/// Seleciona todos os registros de modalidade de tarifa existentes
/// </summary>
/// <returns>Listof TariffModality</returns>
List<TariffModality> List();

    }
}
