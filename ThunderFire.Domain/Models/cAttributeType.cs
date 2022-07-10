using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBTIPATR Alias Tipo de Atributo
///</summary>

    public class AttributeType
    {
               /// <summary>
        /// Codigo do Atributo do Cadastro
        /// </summary>
        public short CODATR{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public string DSCATR{ get;set;} = "";

        /// <summary>
        /// Indica se o atributo deve logar no sistema
        /// </summary>
        /// <remarks>
/// <para>Em caso afirmativo, o sistema permitirá a manutenção da tabela de logins de usuários</para>
/// </remarks>
        public bool USELGN{ get;set;} = true;

        /// <summary>
        /// 
        /// </summary>
        public bool USEACT{ get;set;} = false;

        /// <summary>
        /// 
        /// </summary>
        public bool USETAR{ get;set;} = false;

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
