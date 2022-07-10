using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for Tariff
/// </summary>
    public interface ITariff
    {
       
    /// <summary>
    /// ID de Registro da Memória de Cálculo
    /// </summary>
System.Int32 NIDCAL { get;  set; }    /// <summary>
    /// Insere um registro na tabela TBCADTAR (Tariffs)
    /// </summary>
    ///<param name="model">Tariff</param>
    /// <returns>int</returns>
ExecutionResponse Insert(Tariff model);
    /// <summary>
    /// Altera um registro da tabela TBCADTAR (Tariffs)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">Tariff</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(Tariff model);
    /// <summary>
    /// Obtêm o Id de Tarifação Tarifação de Acordo com o ID de Registro de Tarifação fornecido
    /// </summary>
        /// <param name="pNIDTAR">ID do Registro de Tarifação</param>
    /// <returns>Tariff</returns>
Tariff Select(int pNIDTAR);
    /// <summary>
    /// Obtêm a tarifa expandida de registro de tarifação
    /// </summary>
/// <returns>int</returns>
int GetExpandeTariff( System.Int32 pNIDTAR,System.Double pVLRTRA = 0);
/// <summary>
/// Seleciona todos os registros com base nos parâmetros fornecidos
/// </summary>
    /// <param name="pUSUCFG">Código do Usuario</param>
    /// <param name="pNIVCFG">Nivel de Tarifação</param>
/// <returns>Listof QueryTariff</returns>
List<QueryTariff> List(int pUSUCFG, System.Byte pNIVCFG);

    }
}
