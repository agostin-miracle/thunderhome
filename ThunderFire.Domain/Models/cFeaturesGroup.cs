using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBSYSFXG Alias Features x Group
///</summary>

    public class FeaturesGroup
    {
               /// <summary>
        /// ID do registro de funcionalidade x grupo
        /// </summary>
        public int FUNGRP{ get;set;} = 0;

        /// <summary>
        /// ID da funcionalidade
        /// </summary>
        public int SYSFUN{ get;set;} = 0;

        /// <summary>
        /// ID do grupo
        /// </summary>
        public int SYSGRP{ get;set;} = 0;

        /// <summary>
        /// Código do Status de Registro
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 07</para>
/// </remarks>
        public byte STAREC{ get;set;} = 1;

        /// <summary>
        /// Data de Inclusão ou cadastramento
        /// </summary>
        public DateTime DATCAD{ get;set;} = DateTime.Now;

        /// <summary>
        /// Data da Ultima Atualização
        /// </summary>
        public DateTime DATUPD{ get;set;} = DateTime.Now;

        /// <summary>
        /// Usuário de Atualização
        /// </summary>
        public int UPDUSU{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public string SYSDSC{ get;set;} = "";

        /// <summary>
        /// Descrição do Grupo
        /// </summary>
        public string DSCGRP{ get;set;} = "";

        /// <summary>
        /// Descrição do Status de Registro
        /// </summary>
        public string DSCREC{ get;set;} = "";

        /// <summary>
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        public string LGNUSU{ get;set;} = "";

    }
}
