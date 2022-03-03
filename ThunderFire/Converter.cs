using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire
{
    /// <summary>
    /// Classe Genérica de Conversão de Dados
    /// </summary>
    public static class Converter
    {
        /// <summary>
        /// Captura de Erros
        /// </summary>
        public static TrapError TrappedError = new TrapError();

        /// <summary>
        /// Cultura padrão de saida de conversao 
        /// </summary>
        public static string DefaultCultureOut { get; set; } = "en-Us";

       /// <summary>
        /// Retorna true se o formato de conversao para double é um formato nativo válido
        /// </summary>
        public static bool IsDoubleValueFormat { get; internal set; } = false;

        /// <summary>
        /// Converte o valor para um tipo short(smallint)
        /// </summary>
        /// <param name="s">texto a ser convertido</param>
        /// <returns>Int16</returns>
        public static short ShortValue(string s)
        {
            if (short.TryParse(s, out short r))
                return r;
            return 0;
        }

        /// <summary>
        /// Converte para um valor do tipo Byte
        /// </summary>
        /// <param name="s">texto a ser convertido</param>
        /// <returns>Byte</returns>
        public static byte ByteValue(string s)
        {
            if (byte.TryParse(s, out byte r))
                return r;
            return 0;
        }

        /// <summary>
        /// Converte para um valor do tipo Float
        /// </summary>
        /// <param name="s">texto a ser convertido</param>
        /// <returns>Float</returns>
        public static float FloatValue(string s)
        {
            if (float.TryParse(s, out float r))
                return r;
            return 0;
        }

        /// <summary>
        /// Converte para um valor do tipo Double
        /// </summary>
        /// <param name="text">texto a ser convertido</param>
        /// <returns>Double</returns>
        public static double DoubleValue(string text)
        {
            if (double.TryParse(text, out double r))
                return r;
            return 0;
        }

        /// <summary>
        /// Converte o valor para um tipo bool
        /// </summary>
        /// <param name="text">texto a ser convertido</param>
        /// <returns>bool</returns>
        public static bool BoolValue(string text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                if (text == "0")
                    return false;
                else if (text == "1")
                    return true;
                else
                {
                    if (bool.TryParse(text, out bool r))
                        return r;
                }
            }
            return false;
        }


        /// <summary>
        /// Converte para um valor do tipo Money
        /// </summary>
        /// <param name="text">texto a ser convertido</param>
        /// <returns>Money</returns>
        public static double MoneyValue(string text)
        {
            if (double.TryParse(text, out double r))
                return r;
            return 0;
        }

        /// <summary>
        /// Converte para um valor do tipo Decimal
        /// </summary>
        /// <param name="text">texto a ser convertido</param>
        /// <returns>Decimal</returns>
        public static decimal DecimalValue(string text)
        {
            if (decimal.TryParse(text, out decimal r))
                return r;
            return 0;
        }

        /// <summary>
        /// Converte para um valor do tipo Int
        /// </summary>
        /// <param name="text">texto a ser convertido</param>
        /// <returns>Int32</returns>
        public static int IntValue(string text)
        {
            if (int.TryParse(text, out int r))
                return r;
            return 0;
        }
        /// <summary>
        /// Converte para um valor do tipo string
        /// </summary>
        /// <param name="text">texto a ser convertido</param>
        /// <returns>string</returns>
        public static string StringValue(string text)
        {
            if (String.IsNullOrWhiteSpace(text))
                return "";
            return text.Trim();
        }

        /// <summary>
        /// Converte para um valor do tipo Datetime
        /// </summary>
        /// <param name="s">texto a ser convertido</param>
        /// <returns>Datetime</returns>
        public static DateTime DateValue(string s)
        {
            if (DateTime.TryParse(s, out DateTime r))
                return r;
            return new DateTime(0, 0, 0);
        }
        /// <summary>
        /// Converte para um valor do tipo Datetime, se houver uma data válida
        /// </summary>
        /// <param name="text">texto a ser convertido</param>
        /// <returns>Datetime?</returns>
        public static DateTime? DateValueNull(string text)
        {
            if (text != "")
            {
                if (DateTime.TryParse(text, out DateTime r))
                    return r;
            }
            return null;
        }

        /// <summary>
        /// Converte para um valor do tipo Long
        /// </summary>
        /// <param name="text">texto a ser convertido</param>
        /// <returns>Int64</returns>
        public static long LongValue(string text)
        {
            if (long.TryParse(text, out long r))
                return r;
            return 0;
        }
        /// <summary>
        /// Converte para um valor do tipo Short
        /// </summary>
        /// <param name="text">texto a ser convertido</param>
        /// <returns>Short</returns>
        public static short ToShort(string text)
        {
            if (text != "")
                return Convert.ToInt16(text);
            return 0;
        }
        /// <summary>
        /// Converte para um valor do tipo Byte
        /// </summary>
        /// <param name="text">texto a ser convertido</param>
        /// <returns>Short</returns>
        public static byte ToByte(string text)
        {
            if (text != "")
                return Convert.ToByte(text);
            return 0;
        }

        /// <summary>
        /// Converte para um valor do tipo Int32
        /// </summary>
        /// <param name="text">texto a ser convertido</param>
        /// <returns>Int32</returns>
        public static int ToInt(string text)
        {
            if (text != "")
                return Convert.ToInt32(text);
            return 0;
        }

        /// <summary>
        /// Converte uma data serial de origem excel para DateTime
        /// </summary>
        /// <param name="SerialDate">Número da Data Serial</param>
        /// <returns>DateTime</returns>
        public static DateTime FromExcelSerialDate(int SerialDate)
        {
            if (SerialDate > 59) SerialDate -= 1; //Excel/Lotus 2/29/1900 bug   
            return new DateTime(1899, 12, 31).AddDays(SerialDate);
        }
        /// <summary>
        /// Converte uma string no formato dd/MM/yyyy para um valor do tipo Datetime
        /// </summary>
        /// <param name="text">String no formato de data dd/MM/yyyy</param>
        /// <returns>DateTime?</returns>
        /// <remarks>
        /// <list type="table">
        /// <listheader>
        ///<term>Erro</term>
        ///<description>Definição</description>
        ///</listheader>
        ///<item>
        ///<term>INVALID_FORMAT_DATE</term>
        ///<description>Erro assinalado para o formato de data diferente de dd/mm/yyyy</description>
        ///</item>
        ///<item>
        ///<term>EMPTYDATE</term>
        ///<description>Erro assinalado para quando a data estiver vazia</description>
        ///</item>
        ///</list>
        ///</remarks>
        public static DateTime? ToDate(string text)
        {
            TrappedError.SetError();
            if (!String.IsNullOrEmpty(text))
            {
                if (!String.IsNullOrWhiteSpace(text))
                {
                    try
                    {
                        if (Validator.IsDate(text))
                        {
                            DateTime o = DateTime.Now;
                            CultureInfo culture;
                            DateTimeStyles styles;
                            styles = DateTimeStyles.None;
                            culture = CultureInfo.CreateSpecificCulture("pt-BR");

                            if (DateTime.TryParse(text, culture, styles, out o))
                                return o;
                        }
                        else
                            TrappedError.SetError("INVALID_FORMAT_DATE");
                    }
                    catch
                    {
                        TrappedError.SetError("INVALID_FORMAT_DATE");
                    }
                }
                else
                    TrappedError.SetError("EMPTYDATE");
            }
            else
                TrappedError.SetError("EMPTYDATE");
            return null;
        }

        /// <summary>
        /// Converte uma string no formato ddMMyyyy para um valor do tipo Datetime
        /// </summary>
        /// <param name="text">String no formato de data ddMMyyyy</param>
        /// <returns>DateTime?</returns>
        public static DateTime? ToDateFromFix(string text)
        {
            TrappedError.SetError();

            if (!String.IsNullOrEmpty(text))
            {
                if (!String.IsNullOrWhiteSpace(text))
                {
                    try
                    {
                        int _day = int.Parse(text.Substring(0, 2));
                        int _month = int.Parse(text.Substring(2, 2));
                        int _year = int.Parse(text.Substring(4, 4));
                        return new DateTime(_year, _month, _day);
                    }
                    catch
                    {
                        TrappedError.SetError("INVALID_FORMAT_DATE");
                    }
                }
                else
                    TrappedError.SetError("EMPTYDATE");
            }
            else
                TrappedError.SetError("EMPTYDATE");
            return null;
        }

        /// <summary>
        /// Convert uma string numérica para um valor float
        /// </summary>
        /// <param name="text">string numérica</param>
        /// <returns>int</returns>
        public static float ToFloat(string text)
        {
            if (ThunderFire.Validator.IsFloat(text))
            {
                if (text != "")
                    return Convert.ToSingle(text);
            }
            return 0;
        }

        /// <summary>
        /// Analisa uma string alfa-numérica e retorna a string no formato ########.##
        /// </summary>
        /// <param name="value">String alfa-numérica</param>
        /// <returns>string</returns>
        public static string AnalyzeDoubleValue(string value)
        {
            int decimalPosition = value.IndexOf(".");
            int groupPosition = value.IndexOf(",");
            IsDoubleValueFormat = false;

            /*
             *  1234,56
             */
            if (groupPosition >= 0 && decimalPosition < 0)
            {
                value = value.Replace(",", ".");
                IsDoubleValueFormat = true;
            }
            else if (decimalPosition >= 0 && groupPosition >= 0)
            {
                /*
                 *  1,234.56
                 */
                if (decimalPosition > groupPosition)
                {
                    value = value.Replace(",", "");
                }
                /*
                 *  1.234,56
                 */
                if (decimalPosition < groupPosition)
                {
                    value = value.Replace(".", "#");
                    value = value.Replace(",", ".");
                    value = value.Replace("#", "");
                }
            }
            else
                IsDoubleValueFormat = true;
            return value;
        }

        /// <summary>
        /// Converte uma string numérica para um valor double
        /// </summary>
        /// <param name="text">string numérica</param>
        /// <returns>int</returns>
        public static double ToDouble(string text)
        {
            text = AnalyzeDoubleValue(text);
            if (IsDoubleValueFormat)
                return double.Parse(text, new CultureInfo(DefaultCultureOut));
            return ToDouble(text, ".", ",");
        }
        /// <summary>
        /// Converte uma string numérica fixa um valor double
        /// </summary>
        /// <param name="value">string numerica</param>
        /// <returns>double</returns>
        public static double ToDoubleFromFix(string value)
        {
            value = value.Replace(",", "").Replace(".", "");
            if (!String.IsNullOrEmpty(value))
                return (Convert.ToDouble(value) / 100);
            return 0;
        }

        /// <summary>
        /// Converte uma string para um valor double
        /// </summary>
        /// <list type="table">
        /// <listheader>
        ///<term>Erro</term>
        ///<description>Definição</description>
        ///</listheader>
        ///<item>
        ///<term>INVALIDFORMATVALUEENTRY</term>
        ///<description>Erro assinalado para o formato de entrada do valor</description>
        ///</item>
        ///</list>
        /// <param name="value">String numérica</param>
        /// <param name="style">Style</param>
        /// <returns>double</returns>
        public static double ToDouble(string value, NumberStyles style)
        {
            TrappedError.SetError();
            double RETURN_VALUE = 0;
            try
            {
                value = AnalyzeDoubleValue(value);
                if (!IsDoubleValueFormat)
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture(DefaultCultureOut);
                    NumberFormatInfo format = new NumberFormatInfo();
                    if (!Double.TryParse(AnalyzeDoubleValue(value), style, culture, out RETURN_VALUE))
                        RETURN_VALUE = 0;
                }
                else
                    RETURN_VALUE = double.Parse(value, new CultureInfo(DefaultCultureOut));
            }
            catch (System.FormatException Error)
            {
                TrappedError.SetError("INVALIDFORMATVALUEENTRY");
                TrappedError.ErrorMessage  = Error.Message;
                TrappedError.ErrorObject = Error;
            }
            return RETURN_VALUE;
        }
        /// <summary>
        /// Converte uma string para um valor double
        /// </summary>
        /// <list type="table">
        /// <listheader>
        ///<term>Erro</term>
        ///<description>Definição</description>
        ///</listheader>
        ///<item>
        ///<term>INVALIDFORMATVALUEENTRY</term>
        ///<description>Erro assinalado para o formato de entrada do valor</description>
        ///</item>
        ///</list>
        /// <param name="value">String numérica</param>
        /// <param name="groupSeparator">Separador de Grupo</param>
        /// <param name="decimalSepatator">Separador decimal</param>
        /// <returns></returns>
        public static double ToDouble(string value, string groupSeparator, string decimalSepatator)
        {
            TrappedError.SetError();
            double RETURN_VALUE = 0;
            try
            {
                value = AnalyzeDoubleValue(value);
                CultureInfo culture = CultureInfo.CreateSpecificCulture(DefaultCultureOut);
                NumberFormatInfo format = new NumberFormatInfo
                {
                    NumberGroupSeparator = groupSeparator,
                    NumberDecimalSeparator = decimalSepatator
                };
                RETURN_VALUE = double.Parse(value, format);
            }
            catch (System.FormatException Error)
            {
                TrappedError.SetError("INVALIDFORMATVALUEENTRY");
                TrappedError.ErrorMessage = Error.Message;
                TrappedError.ErrorObject = Error;
            }
            return RETURN_VALUE;
        }



        /// <summary>
        /// Converte um Bool Value para um Byte Value
        /// </summary>
        /// <param name="value">Bool Value</param>
        /// <returns>byte</returns>
        public static byte BTOB(bool value)
        {
            if (value)
                return 1;
            return 0;
        }
        /// <summary>
        /// Converte um Byte Value para um Boolean Value
        /// </summary>
        /// <param name="value">Byte Value</param>
        /// <returns>bool</returns>
        public static bool BTOB(byte value)
        {
            if (value == 1)
                return true;
            return false;
        }

        /// <summary>
        /// Converte para uma data uma tring no formato DDMMYY
        /// </summary>
        /// <param name="value">string de data</param>
        /// <returns>DateTime</returns>
        public static DateTime ToDateFromShort(string value)
        {
            try
            {
                if (!String.IsNullOrEmpty(value))
                    return new DateTime(2000 + int.Parse(value.Substring(4, 2)), int.Parse(value.Substring(2, 2)), int.Parse(value.Substring(0, 2)));
            }
            catch 
            {
            }
            return new DateTime(1900, 01, 01);
        }

        /// <summary>
        /// Converte uma string no formato HH:mm:ss para hora e atribui à data de registro
        /// </summary>
        /// <param name="value">Texto no formato HH:mm:ss</param>
        /// <param name="pDATTRA">Data de Atribuição</param>
        /// <returns>DateTime</returns>
        public static DateTime ToHour(string value, DateTime pDATTRA)
        {
            if (!String.IsNullOrEmpty(value))
            {
                if (value.Length == 6)
                {
                    return new DateTime(pDATTRA.Year, pDATTRA.Month, pDATTRA.Day, int.Parse(value.Substring(0, 2)), int.Parse(value.Substring(2, 2)), int.Parse(value.Substring(4, 2)));
                }
            }
            return new DateTime(1900, 01, 01, 0, 0, 0);
        }
        /// <summary>
        /// Converte um valor double para uma string formatada fixa com prenchimento de zeros a esquerda e tamanho pré-definido
        /// </summary>
        /// <param name="value">Valor</param>
        /// <param name="size">Tamanho</param>
        /// <returns>string</returns>
        public static string ToText(double value, int size)
        {
            string r = String.Format("{0:0.00}", value);
            r = r.ToString().Replace(".", "");
            r = r.ToString().Replace(",", "");
            r = r.PadLeft(size, '0').Right(size);
            return r;
        }
    }
}