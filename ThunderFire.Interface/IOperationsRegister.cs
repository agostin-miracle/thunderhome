using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for OperationsRegister
/// </summary>
    public interface IOperationsRegister
    {
           /// <summary>
    /// Insere um registro na tabela TBREGOPE (Registration of Operations)
    /// </summary>
    ///<param name="model">OperationsRegister</param>
    /// <returns>int</returns>
ExecutionResponse Insert(OperationsRegister model);
    /// <summary>
    /// Altera um registro da tabela TBREGOPE (Registration of Operations)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">OperationsRegister</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(OperationsRegister model);
    /// <summary>
    /// Obtêm o registro de operação selecionado
    /// </summary>
        /// <param name="pNIDOPE">ID do Registro de Operacoes</param>
    /// <returns>OperationsRegister</returns>
OperationsRegister Select(System.Int32 pNIDOPE);

    }
}
