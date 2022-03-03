using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire
{
    /// <summary>
    /// Recursos HTML
    /// </summary>
    public class Html
    {
        private static string _metatag = "";
        /// <summary>
        /// Prepara uma tag com texto
        /// </summary>
        /// <param name="tagname">Nome da Tag</param>
        /// <param name="text">texto a inserir</param>
        /// <returns>string</returns>
        public static string Tag(string tagname, string text)
        {
            return String.Format("<{0}>{1}</{0}>", tagname.ToLower(), text);
        }
        /// <summary>
        /// Prepara uma tag com texto e alinhamento
        /// </summary>
        /// <param name="tag">Nome da Tag</param>
        /// <param name="text">Texto a inserir</param>
        /// <param name="align">Alinhamento</param>
        /// <returns>string</returns>
        private static string Tag(string tag, string text, string align)
        {
            return string.Format("<{0} align=\"{2}\">{1}</{0}>", tag.ToLower(), text, align);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag">Nome da tag</param>
        /// <param name="text">Texto</param>
        /// <param name="align">alinhamento</param>
        /// <param name="width">largura</param>
        /// <param name="baseWidth">base da largura</param>
        /// <param name="colspan">Especifica o número de colunas que uma célula deve abranger</param>
        /// <param name="rowspan">Especifica o número de linhas que uma célula deve abranger</param>
        /// <param name="style">string representando uma diretiva style</param>
        /// <param name="classname">string representando uma class</param>
        /// <returns>string</returns>

        public static string Tag(string tag, string text, string align, int width = 0, string baseWidth = "%", byte colspan = 0, byte rowspan = 0, string style = "", string classname = "")
        {
            string outtext = tag.ToLower();

            if (colspan > 0)
                outtext += " colspan ='" + colspan.ToString() + "'";
            if (rowspan > 0)
                outtext += " rowspan ='" + rowspan.ToString() + "'";

            if (align != "")
                outtext += " align ='" + align + "'";
            if (classname != "")
                outtext += " class ='" + classname + "'";
            if (style != "")
                outtext += " style ='" + style + "'";
            if (width > 0)
            {
                outtext += " width ='" + width.ToString();
                if (String.IsNullOrWhiteSpace(baseWidth))
                    outtext += "%";
                else
                    outtext += baseWidth;
                outtext += "'";
            }
            outtext = "<" + outtext;
            if (text != "")
                outtext += ">" + text;
            else
                outtext += ">";
            outtext = String.Format(outtext + "</{0}>", tag);
            return outtext;
        }

        /// <summary>
        /// Abre uma tag XML/HTML
        /// </summary>
        /// <param name="tag">nome da tag</param>
        /// <returns>string</returns>
        public static string OpenTag(string tag)
        {
            return string.Format("<{0}>", tag.ToLower());
        }
        /// <summary>
        /// Fecha uma tag XML/HTML
        /// </summary>
        /// <param name="tag">nome da tag</param>
        /// <returns>string</returns>
        public static string CloseTag(string tag)
        {
            return string.Format("</{0}>", tag.ToLower());
        }

        /// <summary>
        /// Adiciona elementos a uma TAG META
        /// </summary>
        /// <param name="name">Nome do Elemento</param>
        /// <param name="value">Valor do Elemento</param>
        public static void AppendMeta(string name, string value)
        {
            _metatag += string.Format(" {0} = \"{1}\"", name, value);
        }


        /// <summary>
        /// Cria uma tag meta e devolve 
        /// </summary>
        /// <param name="name">Nome do Elemento</param>
        /// <param name="value">Valor do Elemento</param>
        /// <returns>string</returns>
        public static string SetMeta(string name, string value)
        {
            return string.Format("<meta {0} = \"{1}\"/>", name, value);
        }

        /// <summary>
        /// Fecha uma Tag Meta e devolve seus valores
        /// </summary>
        /// <returns>string</returns>
        public static string CloseMeta()
        {
            string result = "";
            if (!String.IsNullOrWhiteSpace(_metatag))
            {
                result = string.Format("<meta {0}/>", _metatag);
                _metatag = "";
            }
            return result;
        }

    }
}