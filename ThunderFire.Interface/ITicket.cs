using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for Ticket
/// </summary>
    public interface ITicket
    {
           /// <summary>
    /// Insere um registro na tabela TBREGBOL (Registro de Boletos)
    /// </summary>
    ///<param name="model">Ticket</param>
    /// <returns>int</returns>
ExecutionResponse Insert(Ticket model);
    /// <summary>
    /// Altera um registro da tabela TBREGBOL (Registro de Boletos)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">Ticket</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(Ticket model);
    /// <summary>
    /// Seleciona o registro de Boleto de acordo com o ID
    /// </summary>
        /// <param name="pNIDBOL">ID do Boleto</param>
    /// <returns>Ticket</returns>
Ticket Select(System.Int32 pNIDBOL);
    /// <summary>
    /// Encerra um boleto
    /// </summary>
/// <returns>int</returns>
int CloseTicket( int pNIDBOL,System.Byte pSTAREC,System.Int16 pTIPBXA,System.Int16 pSTABOL,System.DateTime pDATPGT,int pUPDUSU);

    }
}
