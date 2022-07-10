using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBSYSFUN Alias System Features
///</summary>

    public class SystemFeatures
    {
               /// <summary>
        /// ID da funcionalidade
        /// </summary>
        public int SYSFUN{ get;set;} = 0;

        /// <summary>
        /// ID de aplicação de funcionalidade
        /// </summary>
        public byte SYSAPL{ get;set;} = 0;

        /// <summary>
        /// ID de identificação da tabela de aplicação da funcionalidade
        /// </summary>
        public short SYSTBL{ get;set;} = 0;

        /// <summary>
        /// ID de identificação da regra de aplicação
        /// </summary>
        public short SYSROL{ get;set;} = 0;

        /// <summary>
        /// Nome do Método da regra de aplicação
        /// </summary>
        public string SYSMTH{ get;set;} = "";

        /// <summary>
        /// Nome do objeto nativo de aplicação da regra
        /// </summary>
        public string SYSPRC{ get;set;} = "";

        /// <summary>
        /// Descrição da aplicação do método
        /// </summary>
        public string SYSDSC{ get;set;} = "";

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
        /// Descrição do Status de Registro
        /// </summary>
        public string DSCREC{ get;set;} = "";

        /// <summary>
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        public string LGNUSU{ get;set;} = "";

    }
}
