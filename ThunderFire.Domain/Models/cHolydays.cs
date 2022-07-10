using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBCADFER Alias HolyDays
///</summary>

    public class Holydays
    {
               /// <summary>
        /// 
        /// </summary>
        public int NIDHOL{ get;set;} = 0;

        /// <summary>
        /// UF
        /// </summary>
        public string CODUFE{ get;set;} = "";

        /// <summary>
        /// Data de Movimento
        /// </summary>
        public DateTime DATMOV{ get;set;} = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        public string DSCFER{ get;set;} = "";

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

        /// <summary>
        /// 
        /// </summary>
        public string DSCUFE{ get;set;} = "";

    }
}
