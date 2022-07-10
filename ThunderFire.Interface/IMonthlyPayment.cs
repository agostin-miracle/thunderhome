using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for MonthlyPayment
/// </summary>
    public interface IMonthlyPayment
    {
           /// <summary>
    /// Insere um registro na tabela TBREGMEN (Monthly Payment)
    /// </summary>
    ///<param name="model">MonthlyPayment</param>
    /// <returns>int</returns>
ExecutionResponse Insert(MonthlyPayment model);
    /// <summary>
    /// Altera um registro da tabela TBREGMEN (Monthly Payment)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">MonthlyPayment</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(MonthlyPayment model);
    /// <summary>
    /// Obtêm o registro de mensalidade de acordo com o ID
    /// </summary>
        /// <param name="pNIDMEN">ID do Registro de Mensalidade</param>
    /// <returns>MonthlyPayment</returns>
MonthlyPayment Select(int pNIDMEN);
    /// <summary>
    /// Localiza um registro de mensalidae conforme os parâmetros abaixo
    /// </summary>
        /// <param name="pTIPMEN">Tipo de Mensalidade</param>
    /// <param name="pCODREF">Código de Referência</param>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pREGBXA">Indicador de Baixa</param>
    /// <returns>List of MonthlyPayment</returns>
List<MonthlyPayment> Select(System.Int32 pTIPMEN, System.Int32 pCODREF, System.Int32 pCODUSU, System.Byte pREGBXA= 0);

    }
}
