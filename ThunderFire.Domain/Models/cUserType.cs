using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBTIPUSU Alias Tipo de Usuário
///</summary>

    public class UserType
    {
               /// <summary>
        /// Tipo de Usuário
        /// </summary>
        /// <remarks>
/// <para>Os códigos disponívels estão contidas na Tabela TBTIPUSU</para>
/// </remarks>
        public byte TIPUSU{ get;set;} = 1;

        /// <summary>
        /// Descrição do Tipo de Usuário
        /// </summary>
        public string DSCTUS{ get;set;} = "";

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
