using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for TicketReceiptDetail
/// </summary>
    public interface ITicketReceiptDetail
    {
           /// <summary>
    /// Insere um registro na tabela TBBXABOL (Registro de Detalhe do Recebimento de Boleto)
    /// </summary>
    ///<param name="model">TicketReceiptDetail</param>
    /// <returns>int</returns>
ExecutionResponse Insert(TicketReceiptDetail model);
    /// <summary>
    /// Obtêm o Registro de Baixa de acordo com o id
    /// </summary>
        /// <param name="pNIDRBD">ID do Registro de Detalhe de Recebimento de Boleto</param>
    /// <returns>TicketReceiptDetail</returns>
TicketReceiptDetail Select(System.Int32 pNIDRBD);

    }
}
