using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for ExpandedTypes
/// </summary>
    public interface IExpandedTypes
    {
           /// <summary>
    /// Insere um registro na tabela TBTIPEXP (Tipo de Expansão de Tarifa)
    /// </summary>
    ///<param name="model">ExpandedTypes</param>
    /// <returns>int</returns>
ExecutionResponse Insert(ExpandedTypes model);
    /// <summary>
    /// Altera um registro da tabela TBTIPEXP (Tipo de Expansão de Tarifa)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">ExpandedTypes</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(ExpandedTypes model);
    /// <summary>
    /// Obtêm o registro de Tipo de Expansão com base no id informado
    /// </summary>
        /// <param name="pTIPEXP">Código do Tipo de Expansão</param>
    /// <returns>ExpandedTypes</returns>
ExpandedTypes Select(System.Int16 pTIPEXP);
/// <summary>
/// Obtêm uma Lista dos Tipos de Expansão
/// </summary>
/// <returns>Listof ExpandedTypes</returns>
List<ExpandedTypes> List();
/// <summary>
/// Obtêm uma Lista das Regras de Expansão
/// </summary>
/// <returns>Listof GeneralTable</returns>
List<GeneralTable> ListRules();

    }
}
