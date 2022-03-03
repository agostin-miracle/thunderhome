using System;
using System.IO;

namespace ThunderFire
{
    /// <summary>
    /// Classe utilitaria de implementação for XSLT
    /// </summary>
    /// <remarks>
    /// O Xslt devera conter o namespace atributo xmlns:obj="urn:util"
    /// </remarks>
    public partial class XsltUtil
    {
        /// <summary>
        /// Efetua uma substituição de caracteres
        /// </summary>
        /// <param name="pvalue">Valor Original</param>
        /// <param name="poldchar">Valor à ser substituido</param>
        /// <param name="pnewchar">Valor à ser injetado</param>
        /// <returns>string</returns>
        public static string replace(string pvalue, string poldchar, string pnewchar)
        {
            return pvalue.Replace(poldchar, pnewchar);
        }

        /// <summary>
        /// Retorna true, se o valor de pesquisa existe no valor de análise
        /// </summary>
        /// <param name="search">Valor de Pesquisa</param>
        /// <param name="dados">Valor de Análise</param>
        /// <returns>true, se existir</returns>
        public bool ContainText(string search, string dados)
        {
            return dados.ToLower().Contains(search.ToLower());
        }


        /// <summary>
        /// Retorna true se um dos valores contido em fonte de dados existirem em dados
        /// </summary>
        /// <param name="fonte">Fonte de Dados</param>
        /// <param name="dados">Dados</param>
        /// <remarks>
        /// <para>A fonte de dados pode ser uma lista delimitada por ',', assim como os dados também</para>
        /// </remarks>
        /// <returns>true</returns>
        public bool contains(string fonte, string dados)
        {
            string[] words = dados.Split(',');
            string[] seek = fonte.Split(',');
            for (int j = 0; j < seek.Length; j++)
            {
                for (int p = 0; p < words.Length; p++)
                {
                    if (seek[j].Contains(words[p].ToString()))
                        return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Verifica se o parametro fonte contem qualquer item da string dados
        /// </summary>
        /// <param name="field">Campo a ser pesquisado</param>
        /// <param name="dados">string de dados a ser avaliada</param>
        /// <returns>True, se a string de dados nao tiver nenhum elemento</returns>
        public int containlist(string field, string dados)
        {
            int existe = 0;
            try
            {
                if (field.CompareTo("") != 0)
                {
                    string[] words = dados.Split(',');
                    for (int p = 0; p < words.Length; p++)
                    {
                        if (field.ToLower().Trim() == words[p].ToLower().Trim())
                        {
                            existe = 1;
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                existe = 1;
            }
            return existe;
        }

        /// <summary>
        /// Retorna uma substring do texto informado
        /// </summary>
        /// <param name="text">Texto</param>
        /// <param name="pos">Posição inicial</param>
        /// <param name="length">Posição Final</param>
        /// <returns>string</returns>
        public string substring(string text, int pos, int length)
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                try
                {
                    if (text != "")
                        return text.Substring(pos, length);
                }
                catch { }
            }
            return "";
        }
        /// <summary>
        /// Converte o texto para caixa alta
        /// </summary>
        /// <param name="text">texto</param>
        /// <returns>string</returns>
        public string upper(string text) {  return text.ToUpper().ToString();}

        /// <summary>
        /// Converte o texto para caixa baixa
        /// </summary>
        /// <param name="text">texto</param>
        /// <returns>string</returns>
        public string lower(string text) { return text.ToLower(); }
        

        /// <summary>
        /// Verifica se 'value' está contido em 'list'
        /// </summary>
        /// <param name="value">string a ser pesquisada</param>
        /// <param name="list">lista de string separada por vírgula</param>
        /// <returns>true, se o valor existe na lista</returns>
        public bool inlist(string value, string list)
        {
            string[] _list = list.Split(',');
            for (int i = 0; i < _list.Length; i++)
            {
                if (lower(value) == lower(_list[i]))
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Retorna a data atual
        /// </summary>
        /// <returns>string</returns>
        public string now()
        {
            return DateTime.Now.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Retorna a hora atual
        /// </summary>
        /// <returns>string</returns>
        public string hour()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        /// <summary>
        /// Retorna o valor de um campo
        /// </summary>
        /// <param name="text">Valor do Campo</param>
        /// <remarks>
        /// <para>Se o valor for vazio, retorna um literal "."</para>
        /// </remarks>
        /// <returns>string</returns>
        public string fieldvalue(string text)
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                text = text.Trim();
                if (text == "")
                    return ".";
            }
            return text;
        }


        /// <summary>
        /// Remove os espaços em branco
        /// </summary>
        /// <param name="text">texto</param>
        /// <returns>string</returns>
        public string trim(string text)
        {
            if ((!String.IsNullOrWhiteSpace(text)) || (!String.IsNullOrEmpty(text)))
                return text.Trim();
            return text;
        }

        /// <summary>
        /// Capitaliza o campo
        /// </summary>
        /// <param name="text">Texto a ser capitalizado</param>
        /// <returns>string</returns>
        public string propername(string text)
        {
            return text.ProperName();
        }

        /// <summary>
        /// Retorna uma string alfanumérica se o texto for alfanumérico
        /// </summary>
        /// <param name="text">texto</param>
        /// <returns>string, 0 se não avaliar o valor</returns>
        public string value(string text)
        {
            if ((!String.IsNullOrWhiteSpace(text)) || (!String.IsNullOrEmpty(text)))
                try
                {
                    return int.Parse(text).ToString();
                }
                catch { return "0"; }
            return "0";
        }

        /// <summary>
        /// Retorna true se a string é nula ou vazia
        /// </summary>
        /// <param name="text">Valor do Campo</param>
        /// <returns>true, se estiver vazio</returns>
        public bool empty(string text)
        {
            if (String.IsNullOrEmpty(text) || String.IsNullOrWhiteSpace(text))
                return true;
            return false;
        }

        /// <summary>
        /// Remove os acentos da string fornecida
        /// </summary>
        /// <param name="text">texto</param>
        /// <returns>string</returns>
        public string removeacentos(string text)
        {
            return text.NoAccents().Trim();
        }
        /// <summary>
        /// Cria uma Tag Html com o valor definido
        /// </summary>
        /// <param name="tag">Tag</param>
        /// <param name="text">Valor</param>
        /// <returns>string</returns>
        public string roundhtmltag(string tag, string text)
        {
            return String.Format("<{0}>{1}</{0}>", tag, text);
        }


        /// <summary>
        /// Formata um campo de acordo com as definições de <veja cref="ThunderFire.MaskText"/>
        /// </summary>
        /// <param name="format">Formato</param>
        /// <param name="text">Texto</param>
        /// <remarks>
        /// <list type="table">
        /// <listheader><term>Formato</term><description>Máscara</description></listheader>  
        /// <item><term>CNPJ</term><description>00.000.000/0000-00</description></item>
        /// <item><term>CPF</term><description>000.000.000-00</description></item>
        /// <item><term>CARD</term><description>9999-XXXX-XXXX-9999</description></item>
        /// <item><term>VALOR</term><description>###,###,###.00</description></item>
        /// <item><term>%</term><description>###.00</description></item>
        /// <item><term>QUANTIDADE</term><description>###,###,###.0000</description></item>
        /// <item><term>CEP</term><description>00000-000</description></item>
        /// <item><term>TELEFONE</term><description>0000-0000</description></item>
        /// <item><term>KEY</term><description>000000000</description></item>
        /// <item><term>FATOR</term><description>######.000000000</description></item>
        /// <item><term>DATA</term><description>dd/MM/yyyy</description></item>
        /// <item><term>HORA</term><description>HH:mm:ss</description></item>
        /// <item><term>DATALONGA</term><description>dd/MM/yyyy HH:mm:ss</description></item>
        /// <item><term>DTOI</term><description>Converte uma string data para o formato yyyyMMdd</description></item>
        /// <item><term>ITOD</term><description>Converte uma string do formato yyyyMMdd para dd/MM/yyyy</description></item>
        /// <item><term>LOGICO</term><description>Sim/Não</description></item>
        /// <item><term>EXTENSO.VALOR</term><description>Retorna o valor por extenso</description></item>
        /// <item><term>EXTENSO.DATA</term><description>Retorna a data formatada por extenso</description></item>
        /// <item><term>EXTENSO.DOW</term><description>Retorna o valor do dia da semana correspondente a data por extenso</description></item>
        /// </list>
        /// </remarks>
        /// <returns>string formatada</returns>
        public static string mask(string format, string text)
        {
            return ThunderFire.MaskText.Mask(format, text);
        }
        /// <summary>
        /// Remove caracters pré-definidos da string
        /// </summary>
        /// <param name="text">String que representa o valor a ser avaliado</param>
        /// <returns>String</returns>
        public static string unmask(string text)
        {
            return ThunderFire.MaskText.UnMask(text);
        }


        /// <summary>
        /// Embaralha uma string
        /// </summary>
        /// <param name="text">Texto a ser embaralhado</param>
        /// <returns>string</returns>
        public static string Shuffle(string text)
        {
            return ThunderFire.Constants.Shuffle(text);
            //string retorno = "";
            //for (int i = 0; i < text.Length; i++)
            //{
            //    char c = Convert.ToChar(text.Substring(i, 1));

            //    if ((c >= 48 && c <= 57) || (c >= 65 && c <= 90) || (c >= 97 && c <= 122))
            //    {
            //        char p = (char)(c + 48);
            //        retorno += Convert.ToString(p);
            //    }
            //}
            //return retorno;
        }

        /// <summary>
        /// Desembaralha uma string
        /// </summary>
        /// <param name="text">Texto a ser desembaralhado</param>
        /// <returns>string</returns>
        public static string UnShuffle(string text)
        {
            return ThunderFire.Constants.UnShuffle(text);
            //string retorno = "";
            //for (int i = 0; i < text.Length; i++)
            //{
            //    char c = Convert.ToChar(text.Substring(i, 1));
            //    char p = (char)(c - 48);
            //    retorno += Convert.ToString(p);
            //}
            //return retorno;
        }
        /// <summary>
        /// Verifica se uma imagem existe
        /// </summary>
        /// <param name="root">rota de pesquisa</param>
        /// <returns>string</returns>
        public string getimage(string root)
        {
            string RETURN_VALUE = "~/images/noimage.jpg";
            try
            {
                //RETURN_VALUE = HttpContext.Current.Server.MapPath(root);
            }
            catch
            {

            }
            return RETURN_VALUE;
        }

        /// <summary>
        /// Retorna o nome do arquivo
        /// </summary>
        /// <param name="text">texto</param>
        /// <returns>string</returns>
        public string filename(string text)
        {
            if ((!String.IsNullOrWhiteSpace(text)) || (!String.IsNullOrEmpty(text)))
                return Path.GetFileName(text);
            return text;
        }
    }
}