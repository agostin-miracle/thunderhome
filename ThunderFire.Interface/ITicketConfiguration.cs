using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for TicketConfiguration
/// </summary>
    public interface ITicketConfiguration
    {
           /// <summary>
    /// Insere um registro na tabela TBCFGBOL (Ticket Configuration)
    /// </summary>
    ///<param name="model">TicketConfiguration</param>
    /// <returns>int</returns>
ExecutionResponse Insert(TicketConfiguration model);
    /// <summary>
    /// Altera um registro da tabela TBCFGBOL (Ticket Configuration)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">TicketConfiguration</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(TicketConfiguration model);
    /// <summary>
    /// Obtêm o registro de configuração de boleto conforme o id informado
    /// </summary>
        /// <param name="pNIDCBL">ID do Registro de Configuração de Boleto</param>
    /// <returns>TicketConfiguration</returns>
TicketConfiguration Select(System.Int32 pNIDCBL);
    /// <summary>
    /// Localiza um id de registro de configuração de boleto de acordo com os parâmetros fornecidos
    /// </summary>
/// <returns>int</returns>
int Locate( System.Int32 pUSUPRO,System.Int32 pUSUCED,System.Byte pTIPBOL);

    }
}
