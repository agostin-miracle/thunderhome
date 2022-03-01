using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBSYSFUN Alias Funcionalidades do Sistema
///</summary>

    public class SystemFeatures
    {
               /// <summary>
        /// 
        /// </summary>
        public int SYSFUN{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public byte SYSPRF{ get;set;} = 1;

        /// <summary>
        /// 
        /// </summary>
        public string SYSKEY{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public string SYSMTH{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public string SYSPRC{ get;set;} = "";

        /// <summary>
        /// 
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

    }
}
