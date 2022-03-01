using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for ContactBook
/// </summary>
    public interface IContactBook
    {
           /// <summary>
    /// Insere um registro na tabela TBCADCTO (Tabela de Contatos)
    /// </summary>
    ///<param name="model">ContactBook</param>
    /// <returns>int</returns>
ExecutionResponse Insert(ContactBook model);
    /// <summary>
    /// Altera um registro da tabela TBCADCTO (Tabela de Contatos)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">ContactBook</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(ContactBook model);
    /// <summary>
    /// Seleciona o registro de acordo com o código do cadastro de contatos
    /// </summary>
        /// <param name="pCODCTO">ID do Registro de Contato</param>
    /// <returns>ContactBook</returns>
ContactBook Select(System.Int32 pCODCTO);
    /// <summary>
    /// Obtêm o registro de contato de acordo com os parâmetros informados
    /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pTIPCTO">Tipo de Contato</param>
    /// <param name="pREGATV">Registro Ativo</param>
    /// <param name="pSTAREC">Status de Registro</param>
    /// <returns>ContactBook</returns>
ContactBook Select(System.Int32 pCODUSU, System.Byte pTIPCTO, System.Byte pREGATV= 1, System.Byte pSTAREC= 1);
    /// <summary>
    /// Localiza um contato com base nos parâmetros fornecidos
    /// </summary>
/// <returns>ExecutionResponse</returns>
    /// <summary>
    /// Seleciona todos os registros de contato do usuário fornecido
    /// </summary>
        /// <param name="pCODUSU">Usuário</param>

    /// <returns>List of QueryContactBook</returns>
List<QueryContactBook> List(System.Int32? pCODUSU);

    }
}
