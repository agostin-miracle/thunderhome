using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ThunderFire
{
    /// <summary>
    /// Extensions (DateTime)
    /// </summary>
    public static class DateExtensions
    {
        /// <summary>
        /// Retorna a data por extenso variando da cultura pt-BR
        /// </summary>
        /// <param name="current">Date</param>
        /// <returns>Data no formato [Dia da Semana + Dia do mes + ' de ' +  mes + ' de' + ano]</returns>
        public static string DataExtenso(this DateTime current)
        {
            var r = ActionOnDate(current, "pt-BR");
            return String.Format("{0} de {1} de {2}", r.Day, r.Months, r.Year);

        }
        /// <summary>
        /// Retorna a data por extenso
        /// </summary>
        /// <param name="current">Date</param>
        /// <param name="cultureinfo">CultureInfo</param>
        /// <returns>Data no formato [Dia da Semana + Dia do mes + ' de ' +  mes + ' de' + ano]</returns>
        public static string DataExtenso(this DateTime current, string cultureinfo)
        {
            var r = ActionOnDate(current, cultureinfo);
            return String.Format("{0} de {1} de {2}", r.Day, r.Months, r.Year);
        }

        /// <summary>
        /// Dia da Semama (pt-BR)
        /// </summary>
        /// <param name="current">DateTime</param>
        /// <returns>string</returns>
        public static string DayOfWeekString(this DateTime current)
        {
            return ActionOnDate(current, "pt-BR").DayOfWeeks;
        }
        /// <summary>
        /// Dia da Semama
        /// </summary>
        /// <param name="current">Data</param>
        /// <param name="cultureinfo">Culture Info</param>
        /// <returns>String</returns>
        public static string DayOfWeekString(this DateTime current, string cultureinfo)
        {
            return ActionOnDate(current, cultureinfo).DayOfWeeks;
        }

        /// <summary>
        /// Mês por extenso (pt-BR)
        /// </summary>
        /// <param name="current">Data corrente</param>
        /// <returns>string</returns>
        public static string MonthString(this DateTime current)
        {
            return ActionOnDate(current, "pt-BR").Months;
        }
        /// <summary>
        /// Mês por extenso
        /// </summary>
        /// <param name="current">Data corrente</param>
        /// <param name="cultureinfo">Culture</param>
        /// <returns></returns>
        public static string MonthString(this DateTime current, string cultureinfo)
        {
            return ActionOnDate(current, cultureinfo).Months;
        }

        internal static DateInfo ActionOnDate(DateTime current, string cultureinfo)
        {
            CultureInfo culture = new CultureInfo(cultureinfo);
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;
            DateInfo refDate = new DateInfo();
            refDate.Culture = culture.Name;
            refDate.Day = current.Day;
            refDate.Month = current.Month;
            refDate.Year = current.Year;
            refDate.Months = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(current.Month));
            refDate.DayOfWeek = (byte)current.DayOfWeek;
            refDate.DayOfWeeks = culture.TextInfo.ToTitleCase(dtfi.GetDayName(current.DayOfWeek));
            return refDate;
        }
    }

    /// <summary>
    /// Extensions (WEB)
    /// </summary>
    public static class WebExtensions
    {

        /// <summary>
        /// Obtém informações sobre a URL da solicitação anterior do cliente vinculada à URL atual.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string UrlReferrer(this HttpRequestMessage request)
        {
            return request.Headers.Referrer == null ? "unknown" : request.Headers.Referrer.AbsoluteUri;
        }
    }

    /// <summary>
    /// Extensions (String)
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Exibe uma saida formada
        /// </summary>
        /// <param name="text">string</param>
        /// <param name="format">format</param>
        /// <returns>string</returns>
        public static string Mask(this string text, string format)
        {
            format = format.ToUpper().Trim();

            if (format == "CNPJ")
            {
                if (text.Trim() != "")
                    if (text.Length == 11)
                        return Convert.ToDouble(text).ToString(@"000\.000\.000\-00");
                    else
                        return Convert.ToDouble(text).ToString(@"00\.000\.000\/0000\-00");
                else
                    return "00.000.000/0000-00";
            }
            else if (format == "VALOR")
            {
                if (text != "")
                    return Convert.ToDouble(text).ToString("###,###,##0.00");
                else
                    return String.Format("{0:0.00}", 0);
            }
            else if (format == "%")
            {
                if (text != "")
                    return Convert.ToDouble(text).ToString("###.00");
                else
                    return "0.00";
            }
            else if (format == "QUANTIDADE")
            {
                if (text != "")
                    return Convert.ToDouble(text).ToString("###,###,###.0000");
                else
                    return "0.00";
            }
            else if (format == "FATOR")
            {
                if (text != "")
                    return Convert.ToDouble(text).ToString("######.000000000");
                else
                    return "0,000000000";
            }
            else if (format == "CPF")
            {
                if (text != "")
                    return Convert.ToDouble(text).ToString(@"000\.000\.000\-00");
                else
                    return "000.000.000-00";
            }
            else if (format == "CEP")
            {
                if (text != "")
                    return Convert.ToInt32(text).ToString(@"00000\-000");
            }
            else if (format == "TELEFONE")
            {
                if (text.Trim().Length == 8)
                    return Convert.ToInt32(text).ToString(@"0000\-0000");
                if (text.Trim().Length == 9)
                    return Convert.ToInt32(text).ToString(@"0 0000\-0000");

            }
            else if (format == "KEY")
            {
                if (text.CompareTo("") != 0)
                    return Convert.ToInt32(text).ToString("00000000");
                else
                    return "00000000";
            }
            else if (format == "LOGICO")
            {
                if (text.CompareTo("") != 0)
                {
                    if (Boolean.TryParse(text, out bool _out))
                        return _out == true ? "Sim" : "Não";
                    else
                    {
                        if (int.TryParse(text, out int _int))
                            return _int > 0 ? "Sim" : "Não";
                        else
                            return "N/D";
                    }
                }
            }
            else if (format == "DATA")
            {
                if (text.CompareTo("") != 0)
                {
                    if (DateTime.TryParse(text, out DateTime _out))
                        return _out.ToString("dd/MM/yyyy");
                    else
                        return "00/00/0000";
                }
            }
            else if (format == "DATALONGA")
            {
                if (text.CompareTo("") != 0)
                {
                    if (DateTime.TryParse(text, out DateTime _out))
                        return _out.ToString("dd/MM/yyyy HH:mm:ss");
                    else
                        return "00/00/0000";
                }
            }

            else if (format == "EXTENSO.VALOR")
            {
                if (text.CompareTo("") != 0)
                {
                    if (Decimal.TryParse(text, out Decimal _out))
                        return ExtensiveValue.Get(_out);
                    else
                        return "Valor Invalido";
                }
            }
            //else if (format == "EXTENSO.DATA")
            //{
            //    if (text.CompareTo("") != 0)
            //    {
            //        DateTime _out;
            //        if (DateTime.TryParse(text, out _out))
            //            return _out.DataExtenso(DefaultCultureOut);// _out.Day.ToString("00") + " de " + _internacionalizacao.GetMonths(_out.Month - 1) + " de " + _out.Year.ToString("0000");
            //        else
            //            return "";
            //    }
            //}
            //else if (format == "EXTENSO.DOW")
            //{
            //    if (text.CompareTo("") != 0)
            //    {
            //        DateTime _out;
            //        if (DateTime.TryParse(text, out _out))
            //            return _out.DayOfWeekString(DefaultCultureOut);
            //        else
            //            return "";
            //    }
            //}

            else if (format == "DTOI")
            {
                if (text.CompareTo("") != 0)
                {
                    if (DateTime.TryParse(text, out DateTime _out))
                        return _out.Year.ToString("0000") + _out.Month.ToString("00") + _out.Day.ToString("00");
                    else
                        return "";
                }
            }
            else if (format == "ITOD")
            {
                if (text.CompareTo("") != 0)
                    return text.ToString().Substring(5, 2) + "/" + text.ToString().Substring(4, 2) + "/" + text.ToString().Substring(0, 4);
                else
                    return "";
            }
            return text;
        }

        /// <summary>
        /// Retorna a string convertida para integer
        /// </summary>
        /// <param name="t">string alfanumerica</param>
        /// <returns>int</returns>
        public static int AsInt(this string t)
        {
            try
            {
                return int.Parse(t);
            }
            catch
            {

            }
            return 0;
        }
        /// <summary>
        /// Retorna a string convertida para integer, caso ocorra erro retorna o default value
        /// </summary>
        /// <param name="t">string alfanumerica</param>
        /// <param name="defaultValue">Valor padrão de retorno</param>
        /// <returns>int</returns>
        public static int AsInt(this string t, int defaultValue)
        {
            int returnValue = 0;
            try
            {
                returnValue = int.Parse(t);
            }
            catch
            {
                returnValue = defaultValue;
            }
            return returnValue;
        }

        /// <summary>
        /// Retorna um fragmento de uma string a partir da posicao position com comprimento length
        /// </summary>
        /// <param name="current">String</param>
        /// <param name="position">Posição</param>
        /// <param name="length">Tamanho</param>
        /// <returns></returns>
        public static string Mid(this string current, int position, int length)
        {
            return current.Substring(position - 1, length);
        }

        /// <summary>
        /// Retorna o fragmento de uma string a partir de uma posicao
        /// </summary>
        /// <param name="current">String</param>
        /// <param name="position">posição inicial</param>
        /// <returns>string</returns>
        public static string Mid(string current, int position)
        {
            return current.Substring(position);
        }

        /// <summary>
        /// Retorna a parte esquerda da string em n posicoes
        /// </summary>
        /// <param name="current">String</param>
        /// <param name="length">Número de Posições</param>
        /// <returns>string</returns>
        public static string Left(this string current, int length)
        {
            return current.Substring(0, length);
        }
        /// <summary>
        /// Retorna a parte direita da string em n posicoes
        /// </summary>
        /// <param name="current">String</param>
        /// <param name="length">Número de Posições</param>
        /// <returns></returns>
        public static string Right(this string current, int length)
        {
            return current.Substring(current.Length - length, length);
        }

        /// <summary>
        ///  Centraliza uma string decorada por caracter de acordo com o comprimento fornecido
        /// </summary>
        /// <param name="current">Text</param>
        /// <param name="length">comprimento</param>
        /// <param name="caracter">caracter</param>
        /// <returns>string</returns>
        public static string PADC(this string current, int length, char caracter='=')
        {
            int l = current.Length;
            int p = (length - l) / 2;
            return  "".PadLeft(p,caracter) + current + "".PadRight(p, caracter);
        }



        /// <summary>
        /// Insere uma 'pinsert' a partir de uma determinada posição e remove o numero de caracteres fornecido em 'length'
        /// </summary>
        /// <param name="text">String</param>
        /// <param name="position">Posicao Inicial</param>
        /// <param name="length">Número de caracteres a serem excluídos</param>
        /// <param name="pinsert">String a ser inserida</param>
        /// <returns>string</returns>
        public static string Stuff(this string text, int position, int length, string pinsert)
        {
            int l = text.Length;
            string s1 = text.Substring(0, position);
            string s2 = "";
            if (length > 0)
            {
                if ((position + length) <= l)
                    s2 = text.Substring(position + length);
                else
                    s2 = text.Substring(position + (l - position));
            }
            return s1 + pinsert + s2;
        }
        /// <summary>
        /// Insere uma string alternativa a partir de uma determinada posição
        /// </summary>
        /// <param name="text">String</param>
        /// <param name="position">Posicao Inicial</param>
        /// <param name="pinsert">String a ser inserida</param>
        /// <returns>string</returns>
        public static string Stuff(this string text, int position, string pinsert)
        {
            int l = text.Length;
            string s1 = text.Substring(0, position);
            string s2 = text.Substring(position);
            return s1 + pinsert + s2;
        }

        /// <summary>
        /// Lê a primeira palavra de uma frase ou nome
        /// </summary>
        /// <param name="current">Texto da Frase ou Nome</param>
        /// <returns>string</returns>
        public static string GetFirstWord(this string current)
        {
            current = current.Trim();
            if (current != "")
            {
                string[] f = current.Split(' ');
                return f[0].ToString();
            }
            return "";
        }
        /// <summary>
        /// Elimina as conjunções de uma frase
        /// </summary>
        /// <param name="t">String</param>
        /// <returns>string</returns>
        public static string NoConjunction(this string t)
        {
            t = t.Replace(" À ", "").ToString();
            t = t.Replace(" A ", "").ToString();
            t = t.Replace(" De ", "").ToString();
            t = t.Replace(" de ", "").ToString();
            t = t.Replace(" E ", "").ToString();
            t = t.Replace(" Em ", "").ToString();
            t = t.Replace(" Do ", "").ToString();
            t = t.Replace(" Da ", "").ToString();
            t = t.Replace(" De ", "").ToString();
            t = t.Replace(" Das ", "").ToString();
            t = t.Replace(" Dos ", "").ToString();
            t = t.Replace(" Nas ", "").ToString();
            t = t.Replace(" Nos ", "").ToString();
            t = t.Replace(" Na ", "").ToString();
            t = t.Replace(" Of ", "").ToString();
            t = t.Replace(" No ", "").ToString();
            t = t.Replace(" Para ", "").ToString();
            t = t.Replace(" O ", "").ToString();
            t = t.Replace(" Os ", "").ToString();
            t = t.Replace(" Ou ", "").ToString();
            t = t.Replace(" Que ", "").ToString();
            t = t.Replace(" É ", "").ToString();
            t = t.Replace(" Qual ", "").ToString();
            t = t.Replace(" Se ", "").ToString();
            t = t.Replace(" Faz ", "").ToString();
            t = t.Replace(" Of ", "").ToString();
            t = t.Replace(" The ", "").ToString();
            t = t.Replace(" To ", "").ToString();
            return t.Trim();
        }
        /// <summary>
        /// Descapitaliza as conjunções de uma frase
        /// </summary>
        /// <param name="t">string</param>
        /// <returns>string</returns>
        public static string LowerConjunction(this string t)
        {
            t = t.Replace(" À ", " à ").ToString();
            t = t.Replace(" A ", " a ").ToString();
            t = t.Replace(" De ", " de ").ToString();
            t = t.Replace(" E ", " e ").ToString();
            t = t.Replace(" Em ", " em ").ToString();
            t = t.Replace(" Do ", " do ").ToString();
            t = t.Replace(" Da ", " da ").ToString();
            t = t.Replace(" Das ", " das ").ToString();
            t = t.Replace(" Dos ", " dos ").ToString();
            t = t.Replace(" Nas ", " nas ").ToString();
            t = t.Replace(" Nos ", " nos ").ToString();
            t = t.Replace(" Na ", " na ").ToString();
            t = t.Replace(" Of ", " of ").ToString();
            t = t.Replace(" No ", " no ").ToString();
            t = t.Replace(" Para ", " para ").ToString();
            t = t.Replace(" O ", " o ").ToString();
            t = t.Replace(" Os ", " os ").ToString();
            t = t.Replace(" Ou ", " ou ").ToString();
            t = t.Replace(" Que ", " que ").ToString();
            t = t.Replace(" É ", " é ").ToString();
            t = t.Replace(" Qual ", " qual ").ToString();
            t = t.Replace(" Se ", " se ").ToString();
            t = t.Replace(" Faz ", " faz ").ToString();
            t = t.Replace(" Of ", " of ").ToString();
            t = t.Replace(" The ", " the ").ToString();
            t = t.Replace(" To ", " to ").ToString();
            return t;
        }

        /// <summary>
        /// Capitaliza uma string
        /// </summary>
        /// <param name="t">string</param>
        /// <returns>string</returns>
        public static string ProperName(this string t)
        {
            bool force = true;
            string k = "";
            for (int i = 0; i < t.Length; i++)
            {
                force = false;
                if (i == 0)
                    force = true;
                else
                {
                    if (i > 0)
                    {
                        if (t.Substring(i - 1, 1).ToString().CompareTo(" ") == 0)
                            force = true;
                    }
                }
                if (force)
                    k += t.Substring(i, 1).ToUpper();
                else
                    k += t.Substring(i, 1);

            }
            return k;
        }
        /// <summary>
        /// Remove os acentos de uma string
        /// </summary>
        /// <param name="text">string</param>
        /// <returns>string</returns>
        public static string NoAccents(this string text)
        {
            if (text != "")
            {
                text = text.Replace("á", "a");
                text = text.Replace("á", "a");
                text = text.Replace("ă", "a");
                text = text.Replace("â", "a");
                text = text.Replace("à", "a");
                text = text.Replace("à", "a");
                text = text.Replace("À", "A");
                text = text.Replace("Á", "A");
                text = text.Replace("Â", "A");
                text = text.Replace("ã", "a");
                text = text.Replace("Ã", "A");

                text = text.Replace("í", "i");
                text = text.Replace("Í", "I");
                text = text.Replace("ĩ", "i");
                text = text.Replace("ĭ", "i");
                text = text.Replace("î", "i");
                text = text.Replace("î", "i");
                text = text.Replace("ĩ́", "i");


                text = text.Replace("Ĩ", "I");
                text = text.Replace("Î", "I");
                text = text.Replace("ç", "c");
                text = text.Replace("Ç", "C");

                text = text.Replace("ɛ", "e");
                text = text.Replace("ɛ̀", "e");
                text = text.Replace("ɛ̀", "e");
                text = text.Replace("ɛ́", "e");
                text = text.Replace("ɛ̌", "e");

                text = text.Replace("è", "e");
                text = text.Replace("È", "E");
                text = text.Replace("ê", "e");
                text = text.Replace("Ê", "E");
                text = text.Replace("é", "e");
                text = text.Replace("é", "e");
                text = text.Replace("É", "E");
                text = text.Replace("ě", "e");
                text = text.Replace("ẹ", "e");
                text = text.Replace("Ẹ", "e");
                text = text.Replace("ẹ̀", "e");
                text = text.Replace("Ẹ̀", "e");
                text = text.Replace("ẹ́", "e");
                text = text.Replace("Ẹ́", "e");
                text = text.Replace("ê", "e");
                text = text.Replace("ế", "e");
                text = text.Replace("ẽ", "e");
                text = text.Replace("ḗ", "e");

                text = text.Replace("ɔ", "o"); //596
                text = text.Replace("ɔ̀", "o");
                text = text.Replace("ɔ́", "o");


                text = text.Replace("ŏ", "o");
                text = text.Replace("o͂̀", "o");
                text = text.Replace("o͂", "o");
                text = text.Replace("Õ", "O");
                text = text.Replace("ô", "o");
                text = text.Replace("Ô", "O");
                text = text.Replace("ó", "o");
                text = text.Replace("Ó", "O");
                text = text.Replace("ò", "o");
                text = text.Replace("ò", "o");
                text = text.Replace("Ò", "O");
                text = text.Replace("ọ", "o");
                text = text.Replace("Ọ", "o");
                text = text.Replace("ọ̀", "o");
                text = text.Replace("Ọ̀", "o");
                text = text.Replace("ọ́", "o");
                text = text.Replace("Ọ́", "o");

                text = text.Replace("u͂̀", "u");
                text = text.Replace("ŭ", "u");
                text = text.Replace("ù", "u");
                text = text.Replace("Ù", "U");
                text = text.Replace("ú", "u");
                text = text.Replace("ü", "u");
                text = text.Replace("Ü", "U");
                text = text.Replace("Ú", "U");
                text = text.Replace("û", "u");
                text = text.Replace("Û", "U");


                text = text.Replace("è", "e");
                text = text.Replace("ì", "i");

                text = text.Replace("È", "E");
                text = text.Replace("Ì", "I");

                text = text.Replace("ń", "n");
                text = text.Replace("Ń", "N");
                text = text.Replace("ḿ", "m");
                text = text.Replace("Ḿ", "M");

                text = text.Replace("Ḿ", "m");
                text = text.Replace("ḿ;", "m");
                text = text.Replace("Ṁ", "m");
                text = text.Replace("ṁ", "m");
                text = text.Replace("ṣ", "s");
                text = text.Replace("Ṣ", "S");
                text = text.Replace("ɛ́", "e");
                text = text.Replace("Ŋ", "N");
                text = text.Replace("ŋ", "n");
                text = text.Replace("ɖ", "d");
                text = text.Replace("ɣ", "g");
                text = text.Replace("ƒ", "f");
                text = text.Replace("ʋ", "v");
                text = text.Replace("ɖ", "d");
                text = text.Replace("ŋ", "n"); //331
                text = text.Replace("ŋ", "n");
            }
            return text.Trim();
        }

        /// <summary>
        /// Remove os caracteres gráficos de uma string
        /// </summary>
        /// <param name="t">string</param>
        /// <returns>string</returns>
        public static string NoMaskChar(this string t)
        {
            t = t.Replace("-", "");
            t = t.Replace("/", "");
            t = t.Replace(",", "");
            t = t.Replace(".", "");
            t = t.Replace("\\", "");
            t = t.Replace("+", "");
            t = t.Replace("  ", " ");
            t = t.Replace("   ", " ");
            t = t.Replace("    ", " ");
            return t;
        }
        /// <summary>
        /// Adiciona o parametro à string de formato url
        /// </summary>
        /// <param name="t">String</param>
        /// <param name="key">Chave</param>
        /// <param name="name">Nome</param>
        /// <param name="value">Valor</param>
        public static string AppendToUrl(this string t, string key, string name, string value)
        {
            if (t.Length != 0)
                t += key;
            t += name + "=" + value;
            return t;
        }
        /// <summary>
        /// Adiciona o parametro à string de formato url
        /// </summary>
        /// <param name="t">String</param>
        /// <param name="name">Nome</param>
        /// <param name="value">Valor</param>
        public static string AppendToUrl(this string t, string name, string value)
        {
            if (t.Length != 0)
                t += "&";
            t += name + "=" + value;
            return t;
        }


        /// <summary>
        /// Verifica se o 'item' está contido na Lista 'items'
        /// </summary>
        /// <typeparam name="T">String/Int</typeparam>
        /// <param name="item">Argumento a ser pesquisado</param>
        /// <param name="items">Lista de Argumentos</param>
        /// <returns>true, se existe o item em items</returns>
        public static bool In<T>(this T item, params T[] items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            return items.Contains(item);
        }

    }

    /// <summary>
    /// Extensions (Stringbuilder)
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Appenda uma string UrlEncode ao StringBuilder corrente
        /// </summary>
        /// <param name="sb">Objeto StringBuilder</param>
        /// <param name="name">Nome do Parâmetro</param>
        /// <param name="value">Valor do Parâmetro</param>
        public static void AppendUrlEncoded(this StringBuilder sb, string name, string value)
        {
            if (sb.Length != 0)
                sb.Append("&");
            sb.Append(HttpUtility.UrlEncode(name));
            sb.Append("=");
            sb.Append(HttpUtility.UrlEncode(value));
        }
    }

    /// <summary>
    /// Extensions (DataTable)
    /// </summary>
    public static class DataTableExtensions
    {
        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        /// <summary>
        /// Converte um objeto dataTable para um List 
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="dataTable">DataTable</param>
        /// <returns>List of T</returns>
        public static List<T> ToList<T>(this DataTable dataTable) where T : new()
        {
            var dataList = new List<T>();
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
                Select(item => new
                {
                    Name = item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();


            var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
                Select(item => new
                {
                    Name = item.ColumnName,
                    Type = item.DataType
                }).ToList();


            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var classObj = new T();

                foreach (var dtField in dtlFieldNames)
                {
                    PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name);

                    var field = objFieldNames.Find(x => x.Name == dtField.Name);

                    var targetType = IsNullableType(propertyInfos.PropertyType) ? Nullable.GetUnderlyingType(propertyInfos.PropertyType) : propertyInfos.PropertyType;
                    if (field != null)
                    {
                        if (targetType == typeof(DateTime))
                        {
                            var value = dataRow[dtField.Name].ToString();
                            if (!String.IsNullOrEmpty(value))
                            {
                                propertyInfos.SetValue(classObj, ThunderFire.Converter.DateValue(value), null);
                            }
                        }
                        else if (targetType == typeof(short))
                        {
                            propertyInfos.SetValue
                            (classObj, ThunderFire.Converter.ShortValue(dataRow[dtField.Name].ToString()), null);
                        }
                        else if (targetType == typeof(int))
                        {
                            propertyInfos.SetValue
                            (classObj, ThunderFire.Converter.IntValue(dataRow[dtField.Name].ToString()), null);
                        }
                        else if (targetType == typeof(long))
                        {
                            propertyInfos.SetValue
                            (classObj, ThunderFire.Converter.LongValue(dataRow[dtField.Name].ToString()), null);
                        }
                        else if (targetType == typeof(decimal))
                        {
                            propertyInfos.SetValue
                            (classObj, ThunderFire.Converter.DecimalValue(dataRow[dtField.Name].ToString()), null);
                        }
                        else if (targetType == typeof(double))
                        {
                            propertyInfos.SetValue
                            (classObj, ThunderFire.Converter.DoubleValue(dataRow[dtField.Name].ToString()), null);
                        }
                        else if (targetType == typeof(float))
                        {
                            propertyInfos.SetValue
                            (classObj, ThunderFire.Converter.FloatValue(dataRow[dtField.Name].ToString()), null);
                        }
                        else if (targetType == typeof(bool))
                        {
                            propertyInfos.SetValue
                            (classObj, ThunderFire.Converter.BoolValue(dataRow[dtField.Name].ToString()), null);
                        }
                        else if (targetType == typeof(byte))
                        {
                            propertyInfos.SetValue
                            (classObj, ThunderFire.Converter.ByteValue(dataRow[dtField.Name].ToString()), null);
                        }

                        else if (targetType == typeof(String))
                        {
                            propertyInfos.SetValue
                            (classObj, ThunderFire.Converter.StringValue(dataRow[dtField.Name].ToString()), null);
                        }
                        else
                        {
                            Console.WriteLine("Error {0}", dataRow[dtField.Name].ToString());
                        }
                    }
                }
                dataList.Add(classObj);
            }
            return dataList;
        }

        /// <summary>
        /// Obtêm um novo data DataTable baseado nas condições de ordenação e extração de colunas
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <param name="sorting">Define as colunas que deverão ser indexas</param>
        /// <param name="distinct">True, para aplicar somente em valores distintos</param>
        /// <param name="Columns">Lista separada por vírgulas dos campos de extração ou uma matriz de argumentos do tipo especificado. Você também pode não enviar nenhum argumento. Se você não enviar nenhum argumento, o comprimento da lista será zero.</param>
        /// <returns>DataTable</returns>
        public static DataTable ToTable(this DataTable table, string sorting, bool distinct = false, params string[] Columns)
        {
            DataView view = new DataView(table);
            if (!string.IsNullOrWhiteSpace(sorting))
                view.Sort = sorting;
            return view.ToTable(distinct, Columns);
        }

      
        /// <summary>
        /// Converte o corrente DataTable para uma string JSON
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <returns>String</returns>
        public static string ToJson(this DataTable table)
        {
            string jsonString = string.Empty;
            jsonString = JsonConvert.SerializeObject(table);
            return jsonString;
        }


        /// <summary>
        /// Grava o conteúdo de DataTable para uma string em CSV
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <param name="header">Header, se não informado não imprime</param>
        /// <param name="delimiter">Delimitador de campos</param>
        /// <param name="AlphaNum">Array de Campos alfanuméricos</param>
        /// <returns>String</returns>
        public static string ToCSVg(this DataTable table, string[] header, string delimiter = ";", string[] AlphaNum = null)
        {

            StringBuilder result = new StringBuilder();

            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    if (header != null)
                    {
                        string s = "";
                        for (int i = 0; i < header.Length; i++)
                        {
                            s += "\"" + header[i].ToUpper().NoAccents() + "\"" + delimiter;
                        }
                        result.Append(s + "\r\n");
                    }


                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        string d = "";

                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            string colName = table.Columns[j].ColumnName.ToUpper();
                            string colValue = table.Rows[i][j].ToString();
                            var colType = table.Columns[j].DataType;
                            if (colType == typeof(DateTime))
                            {
                                if (!String.IsNullOrWhiteSpace(colValue))
                                    d += Convert.ToDateTime(colValue).ToString("dd/MM/yyyy");
                                else
                                    d += "00/00/0000";
                            }
                            else if (colType == typeof(double))
                                d += string.Format("{0:F2}", colValue);
                            else if (colType == typeof(int))
                                d += table.Rows[i][j].ToString();
                            else if (colType == typeof(float))
                                d += string.Format("{0:F2}", colValue);
                            else if (colType == typeof(decimal))
                                d += string.Format("{0:0.00}", Convert.ToDecimal(colValue));
                            else if (colType == typeof(bool))
                            {
                                var r = Convert.ToBoolean(colValue);
                                if (r)
                                    d += "1";
                                else
                                    d += "-1";
                            }

                            else
                            {
                                if (colName == "NUMCRT" || colName == "CARTAO" || colName == "DSCREF" || colName == "IDEPRO" || colName == "CODCMF" || colName == "NIDCRT")
                                    d += "\"" + colValue + "\"";
                                else
                                    d += "\"" + colValue + "\"";
                            }
                            d += delimiter;
                        }
                        result.Append(d + "\r\n");
                    }
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// Grava o conteúdo de DataTable para uma string em Formato HTML
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <param name="title">Título</param>
        /// <param name="header">Header</param>
        /// <param name="styles">Style Css</param>
        /// <returns></returns>
        public static string ToHtml(this DataTable table, string title, string[] header, string styles = "")
        {
            bool go = true;
            string result = ThunderFire.Html.OpenTag("html");
            result += "<head>";
            result += ThunderFire.Html.Tag("title", title);

            if (!String.IsNullOrEmpty(styles))
            {
                result += ThunderFire.Html.Tag("style", styles);
            }
            result += "</head>";
            result += "<table>";

            if (go)
            {

                if (table != null)
                {
                    if (table.Rows.Count > 0)
                    {
                        if (header != null)
                        {
                            result += "<tr>";
                            for (int i = 0; i < header.Length; i++)
                            {
                                result += ThunderFire.Html.Tag("td", header[i].ToUpper().NoAccents());
                            }
                            result += "</tr>";
                        }


                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            string d = "";
                            for (int j = 0; j < table.Columns.Count; j++)
                            {
                                string v = "";
                                string colName = table.Columns[j].ColumnName.ToUpper();
                                string colValue = table.Rows[i][j].ToString();
                                var colType = table.Columns[j].DataType;
                                if (colType == typeof(DateTime))
                                {
                                    if (!String.IsNullOrWhiteSpace(colValue))
                                        v += Convert.ToDateTime(colValue).ToString("dd/MM/yyyy");
                                    else
                                        v += "00/00/0000";
                                }
                                else if (colType == typeof(double))
                                    v += string.Format("{0:F2}", colValue);
                                else if (colType == typeof(int))
                                    v += table.Rows[i][j].ToString();
                                else if (colType == typeof(float))
                                    v += string.Format("{0:F2}", colValue);
                                else if (colType == typeof(decimal))
                                    v += string.Format("{0:0.00}", Convert.ToDecimal(colValue));
                                else if (colType == typeof(bool))
                                {
                                    var r = Convert.ToBoolean(colValue);
                                    if (r)
                                        v += "Sim";
                                    else
                                        v += "Não";
                                }

                                else
                                {
                                    if (colName == "NUMCRT" || colName == "CARTAO" || colName == "DSCREF" || colName == "IDEPRO" || colName == "CODCMF" || colName == "NIDCRT")
                                        v += colValue.Trim();
                                    else
                                        v += colValue.Trim();
                                }
                                d += ThunderFire.Html.Tag("td", v);
                            }
                            d += "</tr>";
                            result += d + Environment.NewLine;
                        }
                    }
                }
            }
            result += ThunderFire.Html.CloseTag("table");
            return result;
        }



        /// <summary>
        /// Filtra os dados de um DataTable
        /// </summary>
        /// <param name="table">DataTable Object</param>
        /// <param name="filterValue">Valor da condicao de filtro</param>
        /// <returns>DataTable</returns>
        public static DataTable FilterData(this DataTable table, string filterValue)
        {
            DataTable _t01 = table.Clone();
            DataRow[] _itens  = table.Select(filterValue);
            foreach (DataRow row in _itens)
            {
                DataRow newRow = _t01.NewRow();
                newRow.ItemArray = row.ItemArray;
                _t01.Rows.Add(newRow);
            }
            return _t01;

        }

        /// <summary>
        /// Concatena dois campos e atualiza para um terceiro campo de atualização
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <param name="fieldkey1">Campo 1 de concatenação</param>
        /// <param name="fieldkey2">Campo 2 de concatenação</param>
        /// <param name="fieldupdate">Campo de Atualização resultado da concatenação</param>
        /// <param name="delimiter">Delimitar entre valores concatenados</param>
        /// <returns>DataTable</returns>
        public static DataTable ConcatFields(this DataTable table, string fieldkey1, string fieldkey2, string fieldupdate, string delimiter = " - ")
        {
            DataTable RETURN_VALUE = table.Copy();
            if (RETURN_VALUE != null)
            {
                if (RETURN_VALUE.Rows.Count > 0)
                {
                    for (int i = 0; i < RETURN_VALUE.Rows.Count; i++)
                    {
                        var _field1 = RETURN_VALUE.Rows[i][fieldkey1].ToString().Trim();
                        var _field2 = RETURN_VALUE.Rows[i][fieldkey2].ToString().Trim();
                        var value = _field1 + delimiter + _field2;
                        RETURN_VALUE.Rows[i][fieldupdate] = value;
                    }
                }
            }
            return RETURN_VALUE;
        }
    }
}