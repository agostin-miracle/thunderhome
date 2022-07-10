using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for AccountEntryPosting
/// </summary>
    public interface IAccountEntryPosting
    {
           /// <summary>
    /// Insere um registro na tabela TBCADLCT (Tabela de Cadastro de Lançamentos)
    /// </summary>
    ///<param name="model">AccountEntryPosting</param>
    /// <returns>int</returns>
ExecutionResponse Insert(AccountEntryPosting model);
    /// <summary>
    /// Altera um registro da tabela TBCADLCT (Tabela de Cadastro de Lançamentos)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">AccountEntryPosting</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(AccountEntryPosting model);
    /// <summary>
    /// Obtêm o registro de Cadastro de Lançamento com base no id informado
    /// </summary>
        /// <param name="pNIDLCT">ID do Registro de Cadastro de Lançamento</param>
    /// <returns>AccountEntryPosting</returns>
AccountEntryPosting Select(System.Int32 pNIDLCT);
    /// <summary>
    /// Copia um conjunto de cadastro de lançamentos para outros
    /// </summary>
/// <returns>int</returns>
int Copy( System.Int16 pTIPLCT,System.Int16 pNEWTIP,System.Int32 pUPDUSU);
    /// <summary>
    /// Exclui um conjunto de cadastro de lançamentos
    /// </summary>
/// <returns>int</returns>
int Delete( System.Int16 pTIPLCT,System.Int32 pUPDUSU);

    }
}
