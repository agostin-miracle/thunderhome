using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for Holydays
/// </summary>
    public interface IHolydays
    {
           /// <summary>
    /// Insere um registro na tabela TBCADFER (HolyDays)
    /// </summary>
    ///<param name="model">Holydays</param>
    /// <returns>int</returns>
ExecutionResponse Insert(Holydays model);
    /// <summary>
    /// Altera um registro da tabela TBCADFER (HolyDays)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">Holydays</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(Holydays model);
    /// <summary>
    /// Obtêm o registro de Feriado com base no id informado
    /// </summary>
        /// <param name="pNIDHOL">ID do Feriado</param>
    /// <returns>Holydays</returns>
Holydays Select(System.Int32 pNIDHOL);

    }
}
