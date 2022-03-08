using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBLGNUSU Alias Login de Usuário
///</summary>

    public class LoginUser
    {
               /// <summary>
        /// 
        /// </summary>
        public int LGNNUM{ get;set;} = 0;

        /// <summary>
        /// Código do Usuário
        /// </summary>
        public int CODUSU{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public byte LGNTYP{ get;set;} = 1;

        /// <summary>
        /// Registro Principal
        /// </summary>
        public byte REGATV{ get;set;} = 0;

        /// <summary>
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        public string LGNUSU{ get;set;} = "";

        /// <summary>
        /// Senha criptografada do usuário
        /// </summary>
        public string PSWUSU{ get;set;} = "";

        /// <summary>
        /// 
        /// </summary>
        public byte RSTPSW{ get;set;} = 1;

        /// <summary>
        /// Data do Ultimo Acesso
        /// </summary>
        public DateTime? ULTACE{ get;set;} = null;

        /// <summary>
        /// 
        /// </summary>
        public int NUMACE{ get;set;} = 0;

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
        /// <summary>
        /// Objeto de Retenção de Informações de Cartões Associados ao usuário
        /// </summary>
public  class MyCards
{

        /// <summary>
        /// Código do Cartão 
        /// </summary>
        /// <remarks>
/// <para>Tabela de Próximos Números : 5</para>
/// </remarks>
        
        public int CODCRT{ get;set;} = 0;

        /// <summary>
        /// Número do Cartão
        /// </summary>
        
        public string NUMCRT{get;set;} = "";

}
}
