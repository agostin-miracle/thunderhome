using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for TicketReceiptRecord
/// </summary>
    public interface ITicketReceiptRecord
    {
           /// <summary>
    /// Insere um registro na tabela TBRBBBOL (Ticket Receipt Record)
    /// </summary>
    ///<param name="model">TicketReceiptRecord</param>
    /// <returns>int</returns>
ExecutionResponse Insert(TicketReceiptRecord model);
    /// <summary>
    /// Altera um registro da tabela TBRBBBOL (Ticket Receipt Record)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">TicketReceiptRecord</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(TicketReceiptRecord model);
    /// <summary>
    /// Seleciona o registro de acordo com o id de registro do conta corrente
    /// </summary>
        /// <param name="pNIDRBB">ID do Registro de Recebimento de Boleto</param>
    /// <returns>TicketReceiptRecord</returns>
TicketReceiptRecord Select(System.Int32 pNIDRBB);
    /// <summary>
    /// Executa o fechamento de um lote de Registro de Recebimento de Boleto
    /// </summary>
/// <returns>int</returns>
int CloseBatch( System.Int32 pNIDRBB);

    }
}
