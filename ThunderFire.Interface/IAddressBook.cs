using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for AddressBook
/// </summary>
    public interface IAddressBook
    {
           /// <summary>
    /// Insere um registro na tabela TBCADEND (Cadasto de Endereços)
    /// </summary>
    ///<param name="model">AddressBook</param>
    /// <returns>int</returns>
ExecutionResponse Insert(AddressBook model);
    /// <summary>
    /// Altera um registro da tabela TBCADEND (Cadasto de Endereços)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">AddressBook</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(AddressBook model);
    /// <summary>
    /// Obtêm o registro de Endereço
    /// </summary>
        /// <param name="pCODEND">Código do Endereço</param>
    /// <returns>AddressBook</returns>
AddressBook Select(System.Int32 pCODEND);
    /// <summary>
    /// Obtêm uma lista de todos os endereços de um usuário
    /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pTIPEND">Tipo de Endereço</param>
    /// <param name="pREGATV">Registro Ativo</param>
    /// <param name="pSTAREC">Status de Registro</param>
    /// <returns>List of QueryAddressBook</returns>
List<QueryAddressBook> List(System.Int32 pCODUSU, System.Int16 pTIPEND= -1, System.Int16 pREGATV= -1, System.Int16 pSTAREC= -1);
    /// <summary>
    /// Localiza o ID de um endereço com base nos parametros fornecidos
    /// </summary>
/// <returns>ExecutionResponse</returns>

    }
}
