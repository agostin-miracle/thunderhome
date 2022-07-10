using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for TariffOperations
/// </summary>
    public interface ITariffOperations
    {
           /// <summary>
    /// Insere um registro na tabela TBTARMOV (Associação de Tarifas e Operações)
    /// </summary>
    ///<param name="model">TariffOperations</param>
    /// <returns>int</returns>
ExecutionResponse Insert(TariffOperations model);
    /// <summary>
    /// Altera um registro da tabela TBTARMOV (Associação de Tarifas e Operações)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">TariffOperations</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(TariffOperations model);
    /// <summary>
    /// Seleciona o registro de acordo com o codigo do produto
    /// </summary>
        /// <param name="pNIDTXM">ID Tarifa x Movimento</param>
    /// <returns>TariffOperations</returns>
TariffOperations Select(int pNIDTXM);
/// <summary>
/// Seleciona todos os registros de expansão de tarifa do id de tarifação fornecido
/// </summary>
/// <returns>Listof TariffOperations</returns>
List<TariffOperations> List();
/// <summary>
/// Obtêm a Lista de Tarifas
/// </summary>
/// <returns>Listof TariffType</returns>
List<TariffType> ListTariffs();
/// <summary>
/// Obtêm a Lista de Operacoes
/// </summary>
/// <returns>Listof Operations</returns>
List<Operations> ListOperations();

    }
}
