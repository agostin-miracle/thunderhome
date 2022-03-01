using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for ContactType
/// </summary>
    public interface IContactType
    {
           /// <summary>
    /// Insere um registro na tabela TBTIPCTO (Tipos de Contato)
    /// </summary>
    ///<param name="model">ContactType</param>
    /// <returns>int</returns>
ExecutionResponse Insert(ContactType model);
    /// <summary>
    /// Altera um registro da tabela TBTIPCTO (Tipos de Contato)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">ContactType</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(ContactType model);
    /// <summary>
    /// Obtêm o registro do Tipo de contato informado
    /// </summary>
        /// <param name="pTIPCTO">Tipo de Contato</param>
    /// <returns>ContactType</returns>
ContactType Select(System.Int32 pTIPCTO);
    /// <summary>
    /// Obtêm uma lista de todos os tipos de contatos
    /// </summary>
    /// <returns>List of ContactType</returns>
List<ContactType> List();

    }
}
