using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for VirtualAccount
/// </summary>
    public interface IVirtualAccount
    {
           /// <summary>
    /// Insere um registro na tabela TBCADCTA (Virtual Account)
    /// </summary>
    ///<param name="model">VirtualAccount</param>
    /// <returns>int</returns>
ExecutionResponse Insert(VirtualAccount model);
    /// <summary>
    /// Altera um registro da tabela TBCADCTA (Virtual Account)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">VirtualAccount</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(VirtualAccount model);
    /// <summary>
    /// Seleciona o registro de conta virtual de acordo com o id da conta
    /// </summary>
        /// <param name="pNIDCTA">ID da Conta Virtual</param>
    /// <returns>VirtualAccount</returns>
VirtualAccount Select(System.Int32 pNIDCTA);
    /// <summary>
    /// Seleciona o registro de conta virtual de acordo com os parâmetros fornecidos
    /// </summary>
        /// <param name="pNUMBCO">Banco</param>
    /// <param name="pNUMAGE">Agência</param>
    /// <param name="pNUMCTA">Conta</param>
    /// <param name="pNUMDVE">DV</param>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pORGCTA">Origem da Conta</param>
    /// <param name="pTIPCTA">Tipo de Conta</param>
    /// <returns>VirtualAccount</returns>
VirtualAccount Select(System.String pNUMBCO, System.String pNUMAGE, System.String pNUMCTA, System.String pNUMDVE, System.Int32 pCODUSU, System.Byte pORGCTA= 2, System.Int32 pTIPCTA= 2);
    /// <summary>
    /// Obtêm o registro de conta virtual de acordo com o cpf/cnpj informado
    /// </summary>
        /// <param name="pCODCMF">Código do CPF/CNPJ</param>
    /// <param name="pORGCTA">Origem da Conta</param>
    /// <returns>VirtualAccount</returns>
VirtualAccount Select(System.String pCODCMF, System.Byte? pORGCTA);
    /// <summary>
    /// Exclui logicamente um registro de conta
    /// </summary>
/// <returns>ExecutionResponse</returns>
    /// <summary>
    /// Executa a aprovação ou rejeição de uma conta digital
    /// </summary>
/// <returns>ExecutionResponse</returns>
    /// <summary>
    /// Localiza uma conta virtual a partir do código de usuário
    /// </summary>
/// <returns>ExecutionResponse</returns>
    /// <summary>
    /// Obtêm todos os registros de contas virtuais registradas conforme parametro fornecido
    /// </summary>
        /// <param name="pCODUSU">Usuário</param>

    /// <returns>List of QueryVirtualAccount</returns>
List<QueryVirtualAccount> List(System.Int32? pCODUSU= null);

    }
}
