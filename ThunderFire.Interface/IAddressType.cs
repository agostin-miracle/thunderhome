using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for AddressType
/// </summary>
    public interface IAddressType
    {
           /// <summary>
    /// Insere um registro na tabela TBTIPEND (Address Type)
    /// </summary>
    ///<param name="model">AddressType</param>
    /// <returns>int</returns>
ExecutionResponse Insert(AddressType model);
    /// <summary>
    /// Altera um registro da tabela TBTIPEND (Address Type)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">AddressType</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(AddressType model);
    /// <summary>
    /// Obtêm o registro de Tipo de Endereço com base no id informado
    /// </summary>
        /// <param name="pTIPEND">Tipo de Endereço</param>
    /// <returns>AddressType</returns>
AddressType Select(System.Byte pTIPEND);

    }
}
