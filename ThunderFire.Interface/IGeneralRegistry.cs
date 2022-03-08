using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for GeneralRegistry
/// </summary>
    public interface IGeneralRegistry
    {
           /// <summary>
    /// Insere um registro na tabela TBCADGER (Cadastro Geral)
    /// </summary>
    ///<param name="model">GeneralRegistry</param>
    /// <returns>int</returns>
ExecutionResponse Insert(GeneralRegistry model);
    /// <summary>
    /// Altera um registro da tabela TBCADGER (Cadastro Geral)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">GeneralRegistry</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(GeneralRegistry model);
    /// <summary>
    /// Seleciona o registro de acordo com o Código do Usuário
    /// </summary>
        /// <param name="pCODUSU">Código do Usuario</param>
    /// <returns>GeneralRegistry</returns>
GeneralRegistry Select(System.Int32 pCODUSU);
    /// <summary>
    /// Valida um CPF/CNPJ já existente para o mesmo atributo na base de cadastro geral
    /// </summary>
/// <returns>ExecutionResponse</returns>
ExecutionResponse  ValidateEntryCMF( short pCODATR,System.Int32 pCODUSU,System.String pCODCMF,System.Int32 pSRCUSU);
    /// <summary>
    /// Verifica se já existe um cadastro com o CPF/CNPJ cadastrado, e retorna o id localizado
    /// </summary>
/// <returns>ExecutionResponse</returns>
ExecutionResponse  PesquisarCMF( System.Int16 pCODATR,System.String pCODCMF);
    /// <summary>
    /// Obtêm uma lista de registros do cadastro geral conforme parâmetros informados
    /// </summary>
        /// <param name="pCODATR">Atributo</param>
    /// <param name="pSTAUSU">Status do Usuário</param>
    /// <param name="pSRCUSU">ID do Responsável</param>
    /// <param name="pNOMUSU">Nome</param>
    /// <param name="pSTAREC">Status do Registro</param>

    /// <returns>List of QueryGeneralRegistry</returns>
List<QueryGeneralRegistry> List(System.Int16? pCODATR, System.Int16? pSTAUSU, System.Int32? pSRCUSU, string pNOMUSU, System.Byte? pSTAREC);
    /// <summary>
    /// Obtêm uma lista de usuários com contas virtuais ativas
    /// </summary>
    /// <returns>List of GeneralRegistry</returns>
List<GeneralRegistry> ListUserAccounts();
    /// <summary>
    /// Obtêm uma lista de usuários vinculados ao gerenciamento de Produto
    /// </summary>
        /// <param name="pCODPRO">Código do Produto</param>

    /// <returns>List of MyUsers</returns>
List<MyUsers> ListUserByProduct(System.Int16? pCODPRO);
    /// <summary>
    /// Obtêm uma lista de usuários por tipo de usuário
    /// </summary>
        /// <param name="pTIPUSU">Tipo de Usuário</param>
    /// <param name="pCODUSU">Código do Usuário</param>

    /// <returns>List of MyUsers</returns>
List<MyUsers> ListUserByUserType(System.Byte pTIPUSU, System.Int32? pCODUSU);

    }
}
