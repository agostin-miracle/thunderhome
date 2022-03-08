using System.Collections.Generic;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Interface
{
/// <summary>
/// Interface for Groups
/// </summary>
    public interface IGroups
    {
           /// <summary>
    /// Insere um registro na tabela TBSYSGRP (Groups)
    /// </summary>
    ///<param name="model">Groups</param>
    /// <returns>int</returns>
ExecutionResponse Insert(Groups model);
    /// <summary>
    /// Altera um registro da tabela TBSYSGRP (Groups)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">Groups</param>
    /// <returns>ExecutionResponse</returns>
ExecutionResponse Update(Groups model);
    /// <summary>
    /// Obtêm o registro do grupo
    /// </summary>
        /// <param name="pSYSGRP">Código do Grupo</param>
    /// <returns>Groups</returns>
Groups Select(System.Int32 pSYSGRP);
    /// <summary>
    /// Obtêm uma lista de todos os grupos existentes
    /// </summary>
    /// <returns>List of Groups</returns>
List<Groups> List();

    }
}
