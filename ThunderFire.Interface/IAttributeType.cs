using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for AttributeType
/// </summary>
    public interface IAttributeType
    {
           /// <summary>
    /// Insere um registro na tabela TBTIPATR (Tipo de Atributo)
    /// </summary>
    ///<param name="model">AttributeType</param>
    /// <returns>int</returns>
ExecutionResponse Insert(AttributeType model);
    /// <summary>
    /// Altera um registro da tabela TBTIPATR (Tipo de Atributo)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">AttributeType</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(AttributeType model);
    /// <summary>
    /// Obtêm o registro do tipo de atributo informado
    /// </summary>
        /// <param name="pCODATR">Código do Atributo</param>
    /// <returns>AttributeType</returns>
AttributeType Select(short pCODATR);
    /// <summary>
    /// Obtêm todos os registros de tipo de atributo cadastrados
    /// </summary>
    /// <returns>List of AttributeType</returns>
List<AttributeType> List();

    }
}
