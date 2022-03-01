using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for SystemFeatures
/// </summary>
    public interface ISystemFeatures
    {
           /// <summary>
    /// Insere um registro na tabela TBSYSFUN (Funcionalidades do Sistema)
    /// </summary>
    ///<param name="model">SystemFeatures</param>
    /// <returns>int</returns>
ExecutionResponse Insert(SystemFeatures model);
    /// <summary>
    /// Altera um registro da tabela TBSYSFUN (Funcionalidades do Sistema)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">SystemFeatures</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(SystemFeatures model);
    /// <summary>
    /// Seleciona o tipo de contato de acordo com o código
    /// </summary>
        /// <param name="pSYSFUN">ID da Funcionalidade</param>
    /// <returns>SystemFeatures</returns>
SystemFeatures Select(int pSYSFUN);
    /// <summary>
    /// Seleciona todos os tipos de contato existentes
    /// </summary>
    /// <returns>SystemFeatures</returns>
SystemFeatures List();

    }
}
