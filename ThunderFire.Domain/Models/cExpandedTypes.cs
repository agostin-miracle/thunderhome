using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBTIPEXP Alias Tipo de Expansão de Tarifa
///</summary>

    public class ExpandedTypes
    {
               /// <summary>
        /// 
        /// </summary>
        public short TIPEXP{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public string DSCEXP{ get;set;} = "";

        /// <summary>
        /// Nível de Registro aplicado à Expansão de Tarifas
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 921</para>
/// <para>1 - Quantidade</para>
/// <para>2 - Valor</para>
/// </remarks>
        public byte LVLREG{ get;set;} = 0;

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
        public string DSCREG{ get;set;} = "";

    }
}
