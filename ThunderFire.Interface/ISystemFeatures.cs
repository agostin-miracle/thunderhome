using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for SystemFeatures
/// </summary>
    public interface ISystemFeatures
    {
           /// <summary>
    /// Insere um registro na tabela TBSYSFUN (System Features)
    /// </summary>
    ///<param name="model">SystemFeatures</param>
    /// <returns>int</returns>
ExecutionResponse Insert(SystemFeatures model);
    /// <summary>
    /// Altera um registro da tabela TBSYSFUN (System Features)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">SystemFeatures</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(SystemFeatures model);
    /// <summary>
    /// Obtêm o registro de uma funcionalidade de acordo com o id
    /// </summary>
        /// <param name="pSYSFUN">ID da Funcionalidade</param>
    /// <returns>SystemFeatures</returns>
SystemFeatures Select(System.Int32 pSYSFUN);
/// <summary>
/// Obtêm todos os registros de funcionalidades específicas para uma tabela
/// </summary>
    /// <param name="pSYSTBL">ID da Tabela</param>
/// <returns>Listof SystemFeatures</returns>
List<SystemFeatures> List(System.Int16? pSYSTBL);

    }
}
