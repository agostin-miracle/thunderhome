using System;
using System.Globalization;


namespace ThunderFire
{
    /// <summary>
    /// Classe geral de Formatação de Dados
    /// </summary>
    public static class MaskText
    {

        /// <summary>
        /// Define se deve alterar a disposição de formatadores decimais e milhares de um valor
        /// </summary>
        public static bool ApplyValueCustom { get; set; } = false;
        /// <summary>
        /// Cultura Padrão pt-BR
        /// </summary>
        public static string DefaultCultureOut { get; set; } = "pt-br";
        /// <summary>
        /// Remove caracters pré-definidos da string
        /// </summary>
        /// <param name="text">String que representa o valor a ser avaliado</param>
        /// <returns>String</returns>
        public static string UnMask(string text)
        {
            if (text.CompareTo("") != 0)
                return text.Replace(" ", "").Replace("-", "").Replace("/", "").Replace(",", "").Replace(".", "").Replace(":", "");
            return "";
        }
        /// <summary>
        /// Remove caracteres não numéricos de um valor
        /// </summary>
        /// <param name="text">String que representa o valor a ser avaliado</param>
        /// <returns>String</returns>
        public static string UnMaskValue(string text)
        {
            if (text.CompareTo("") != 0)
            {
                text = text.Replace("/", "");
                text = text.Replace(".", "");
                return text;
            }
            return "0.00";
        }
        /// <summary>
        /// Mascara um string de acordo com o nome do formato passado
        /// </summary>        
        /// <param name="format">Nome pré-definido do Formato</param>
        /// <param name="text">Valor a ser avaliado</param>
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
        /// <returns>string</returns>
        public static string Mask(string format, string text)
        {
            format = format.ToUpper().Trim();
            double _iv = 0;

            NumberFormatInfo provider = new CultureInfo("en-Us", false).NumberFormat;
            provider.NumberDecimalSeparator = ".";
            provider.NumberGroupSeparator = "";
            provider.NumberGroupSizes = new int[] { 3 };
            provider.CurrencySymbol = "R$";

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

            else if (format == "CARD")
            {
                if (text != "")
                {
                    if (text.Length == 16)
                    {
                        return text.Substring(0, 4) + "-XXXX-XXXX-" + text.Substring(12, 4);
                    }
                }
            }

            else if (format == "VALOR")
            {

                if (text != "")
                {
                    text = Converter.AnalyzeDoubleValue(text);
                    Double.TryParse(text, System.Globalization.NumberStyles.AllowParentheses |
                 System.Globalization.NumberStyles.AllowLeadingWhite |
                 System.Globalization.NumberStyles.AllowTrailingWhite |
                 System.Globalization.NumberStyles.AllowThousands |
                 System.Globalization.NumberStyles.AllowDecimalPoint |
                 System.Globalization.NumberStyles.AllowLeadingSign, provider, out _iv);
                }
                return String.Format(CultureInfo.GetCultureInfo(DefaultCultureOut), "{0:N}", _iv);
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
            else if (format == "HORA")
            {
                if (text.CompareTo("") != 0)
                {
                    if (DateTime.TryParse(text, out DateTime _out))
                        return _out.ToString("HH:mm:ss");
                    else
                        return "00:00:00";
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
                    if (Decimal.TryParse(text, out decimal _out))
                        return ExtensiveValue.Get(_out);
                    else
                        return "Valor Invalido";
                }
            }
            else if (format == "EXTENSO.DATA")
            {
                if (text.CompareTo("") != 0)
                {
                    if (DateTime.TryParse(text, out DateTime _out))
                        return _out.DataExtenso(DefaultCultureOut);// _out.Day.ToString("00") + " de " + _internacionalizacao.GetMonths(_out.Month - 1) + " de " + _out.Year.ToString("0000");
                    else
                        return "";
                }
            }
            else if (format == "EXTENSO.DOW")
            {
                if (text.CompareTo("") != 0)
                {
                    if (DateTime.TryParse(text, out DateTime _out))
                        return _out.DayOfWeekString(DefaultCultureOut);
                    else
                        return "";
                }
            }

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

    }


    /// <summary>
    /// Classe que implementa Formatos personalizados de apresentação
    /// </summary>
    public class CustomFormat : IFormatProvider, ICustomFormatter
    {
        /// <summary>
        /// Obtêm o objeto de formatação
        /// </summary>
        /// <param name="formatType">Tipo de Formato</param>
        /// <returns>Object</returns>
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }
        /// <summary>
        /// Implementa a saida de acordo com o Formato personalizado 
        /// </summary>
        /// <param name="format">Formato</param>
        /// <param name="arg">Argumento</param>
        /// <param name="formatProvider">Provider de Utilização</param>
        /// 
        /// <example> 
        /// O exemplo abaixo ilustra o uso de CustomFormat
        /// <code>
        ///Console.WriteLine(String.Format(new CustomFormat(), "{0:CNPJ}", "12345678900001"));
        ///Console.WriteLine(String.Format(new CustomFormat(), "{0:CPF}", "12345678900"));
        ///Console.WriteLine(String.Format(new CustomFormat(), "{0:PHONE}", "12345678"));
        ///Console.WriteLine(String.Format(new CustomFormat(), "{0:CEP}", "12345678"));
        ///Console.WriteLine(String.Format(new CustomFormat(), "{0:MOBILE}", "912345678"));
        ///Console.WriteLine(String.Format(new CustomFormat(), "{0:CARD}", "1234567890123456"));
        ///Console.ReadKey();
        /// </code>
        ///<list type = "table">
        ///<listheader>
        ///<term>Código</term>
        ///<description >Descrição</description>
        ///</listheader>
        ///<item>
        ///<term>CNPJ</term> 
        ///<description>Converter para uma representação do CNPJ</description>
        ///</item>
        ///<item>
        ///<term>CPF</term> 
        ///<description>Converter para uma representação do CPF</description>
        ///</item>
        ///<item>
        ///<term>PHONE</term> 
        ///<description>Converter para uma representação do NUMERO DO TELEFONE</description>
        ///</item>
        ///<item>
        ///<term>CEP</term> 
        ///<description>Converter para uma representação do CEP</description>
        ///</item>
        ///<item>
        ///<term>MOBILE</term> 
        ///<description>Converter para uma representação do CELULAR</description>
        ///</item>
        ///<item>
        ///<term>CARD</term> 
        ///<description>Converter para uma representação do CARTAO</description>
        ///</item>
        ///</list>
        /// </example>
        /// <returns>string</returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (!this.Equals(formatProvider))
                return null;
            string text = arg.ToString();
            return ThunderFire.MaskText.Mask(format, text);
        }
    }
}