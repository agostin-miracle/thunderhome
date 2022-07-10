using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for LinkedAccount
/// </summary>
    public interface ILinkedAccount
    {
           /// <summary>
    /// Insere um registro na tabela TBCADCVL (Linked Account)
    /// </summary>
    ///<param name="model">LinkedAccount</param>
    /// <returns>int</returns>
ExecutionResponse Insert(LinkedAccount model);
    /// <summary>
    /// Altera um registro da tabela TBCADCVL (Linked Account)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">LinkedAccount</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(LinkedAccount model);
    /// <summary>
    /// Obtêm o registro da conta vinculada com base no id informado
    /// </summary>
        /// <param name="pNIDCVL">ID da Conta Vinculada</param>
    /// <returns>LinkedAccount</returns>
LinkedAccount Select(System.Int32 pNIDCVL);
    /// <summary>
    /// Remove um conta vinculada da lista
    /// </summary>
/// <returns>int</returns>
int RemoveLinkedID( int pCODUSU,int pNIDCTA,int pUPDUSU);
    /// <summary>
    /// Localiza uma conta digital conforme os parâmetros fornecidos
    /// </summary>
/// <returns>int</returns>
int LocateAccount( System.Byte pMETPSQ,System.String pNOMPSQ,System.Int32 pCODUSU);

    }
}
