using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for TransferRegistration
/// </summary>
    public interface ITransferRegistration
    {
           /// <summary>
    /// Insere um registro na tabela TBREGMOV (Tabela de Registro de Transferências)
    /// </summary>
    ///<param name="model">TransferRegistration</param>
    /// <returns>int</returns>
ExecutionResponse Insert(TransferRegistration model);
    /// <summary>
    /// Obtêm o registro de transferência com base no id informado
    /// </summary>
        /// <param name="pNIDHOL">ID do Feriado</param>
    /// <returns>TransferRegistration</returns>
TransferRegistration Select(System.Int32 pNIDHOL);

    }
}
