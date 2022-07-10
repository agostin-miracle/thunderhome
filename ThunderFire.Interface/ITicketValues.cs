using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for TicketValues
/// </summary>
    public interface ITicketValues
    {
           /// <summary>
    /// Insere um registro na tabela TBVALBOL (Registro de Valores de Boletos)
    /// </summary>
    ///<param name="model">TicketValues</param>
    /// <returns>int</returns>
ExecutionResponse Insert(TicketValues model);
    /// <summary>
    /// Altera um registro da tabela TBVALBOL (Registro de Valores de Boletos)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">TicketValues</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(TicketValues model);
    /// <summary>
    /// Seleciona o registro de acordo com o codigo do produto
    /// </summary>
        /// <param name="pNIDCBL">ID do Registro de Configuração de Boleto</param>
    /// <returns>TicketValues</returns>
TicketValues Select(System.Int32 pNIDCBL);
/// <summary>
/// Seleciona todos os registros de um determinado boleto
/// </summary>
/// <returns>Listof TicketValues</returns>
List<TicketValues> List();

    }
}
