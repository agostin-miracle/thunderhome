using System;
using ThunderFire;
namespace ThunderFire.Domain.Models
{
///<summary>
/// Class of TBMODTAR Alias Tariff Modality
///</summary>

    public class TariffModality
    {
               /// <summary>
        /// Modalidade de Aplicação do Cartão
        /// </summary>
        /// <remarks>
/// <para>Este campo utiliza os atributos da Tabela TBMODTAR para aplicação de cálculo de parcelamento</para>
/// </remarks>
        public short MODCRT{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public string DSCMOD{ get;set;} = "";

        /// <summary>
        /// Número da Parcela Inicial
        /// </summary>
        public byte PARINI{ get;set;} = 0;

        /// <summary>
        /// Número da Parcela Final/Máxima
        /// </summary>
        public byte PARFIN{ get;set;} = 0;

        /// <summary>
        /// 
        /// </summary>
        public string EXTMOD{ get;set;} = "";

        /// <summary>
        /// Código do Status de Registro
        /// </summary>
        /// <remarks>
/// <para>Tabela Geral 07</para>
/// </remarks>
        public byte STAREC{ get;set;} = 1;

        /// <summary>
        /// Descrição do Status de Registro
        /// </summary>
        public string DSCREC{ get;set;} = "";

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
        /// Identificação da Chave de Login do Usuário
        /// </summary>
        public string LGNUSU{ get;set;} = "";

    }
}
