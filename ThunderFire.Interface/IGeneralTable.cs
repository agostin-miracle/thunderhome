using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for GeneralTable
/// </summary>
    public interface IGeneralTable
    {
           /// <summary>
    /// Insere um registro na tabela TBTABGER (Tabela Geral do Sistema)
    /// </summary>
    ///<param name="model">GeneralTable</param>
    /// <returns>int</returns>
ExecutionResponse Insert(GeneralTable model);
    /// <summary>
    /// Altera um registro da tabela TBTABGER (Tabela Geral do Sistema)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">GeneralTable</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(GeneralTable model);
    /// <summary>
    /// Seleciona o registro de acordo com o id de registro da tabela geral
    /// </summary>
        /// <param name="pKEYTAB">ID de Registro da Tabela</param>
    /// <returns>GeneralTable</returns>
GeneralTable Select(System.Int32 pKEYTAB);
    /// <summary>
    /// Seleciona um Tipo de Tabela Específica ou Todas se o número da tabela não for especificado
    /// </summary>
        /// <param name="pNUMTAB">Código da Tabela de Acesso</param>
    /// <returns>List of GeneralTable</returns>
List<GeneralTable> List(System.Int32 pNUMTAB);
    /// <summary>
    /// Obtêm o Id de Registro de uma Tabela Geral Baseada no Código Chave da Tabela
    /// </summary>
/// <returns>ExecutionResponse</returns>
    /// <summary>
    /// Obtêm o Id de Registro de uma Tabela Geral Baseada no Código Chave Texto da Tabela
    /// </summary>
/// <returns>ExecutionResponse</returns>

    }
}
