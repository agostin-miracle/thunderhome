using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire
{
    /// <summary>
    /// Classe Auxiliar de Box
    /// </summary>
    public class ItemValue
    {
        /// <summary>
        /// Construtor da Classe
        /// </summary>
        /// <param name="pStrValue">Texto a ser exibido</param>
        /// <param name="pLngID">Valor Numérico ou Chave de Pesquisa</param>
        public ItemValue(string pStrValue, int pLngID)
        {
            this.Text = pStrValue;
            this.KeyValue = pLngID;
        }
        /// <summary>
        /// Construtor da Classe
        /// </summary>
        /// <param name="pStrValue">Texto a ser exibido</param>
        /// <param name="pStrText">Chave Texto de Controle</param>
        public ItemValue(string pStrValue, string pStrText)
        {
            this.Text = pStrValue;
            this.KeyText = pStrText;
        }

        /// <summary>
        /// Construtor da Classe
        /// </summary>
        public ItemValue()
        {
            this.Text = "";
            this.KeyValue = 0;
            this.KeyText = "";
        }
        /// <summary>
        /// Key Values
        /// </summary>
        public int KeyValue { get; set; }

        /// <summary>
        /// Key Text
        /// </summary>
        public string KeyText { get; set; }

        /// <summary>
        /// Texto a ser exibido
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Texto a ser exibido
        /// </summary>
        /// <returns>Texto a ser exibido</returns>
        public override string ToString()
        {
            return this.Text.ToString();
        }
    }
}
