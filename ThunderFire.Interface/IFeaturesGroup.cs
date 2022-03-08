using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for FeaturesGroup
/// </summary>
    public interface IFeaturesGroup
    {
           /// <summary>
    /// Insere um registro na tabela TBSYSFXG (Features x Group)
    /// </summary>
    ///<param name="model">FeaturesGroup</param>
    /// <returns>int</returns>
ExecutionResponse Insert(FeaturesGroup model);
    /// <summary>
    /// Altera um registro da tabela TBSYSFXG (Features x Group)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">FeaturesGroup</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(FeaturesGroup model);
    /// <summary>
    /// Obtêm o registro de associação do funcionalidade x grupo
    /// </summary>
        /// <param name="pFUNGRP">ID de Associação Usuário x Grupo</param>
    /// <returns>FeaturesGroup</returns>
FeaturesGroup Select(System.Int32 pFUNGRP);
    /// <summary>
    /// Obtêm uma lista de todos os registros de usuário x grupo existentes
    /// </summary>
        /// <param name="pSYSGRP">ID do Grupo</param>
    /// <returns>List of FeaturesGroup</returns>
List<FeaturesGroup> List(System.Int32 pSYSGRP);

    }
}
