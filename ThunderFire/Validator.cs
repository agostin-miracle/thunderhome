using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ThunderFire
{
    /// <summary>
    /// Classe de Validação de Propósito Geral
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// Data Retida pelo sistema
        /// </summary>
        public static DateTime? FIELDDATE = null;
        /// <summary>
        /// Nome
        /// </summary>
        public const string VALIDATENAME = @"^([a-zA-Z0-9]{3,30})$";
        /// <summary>
        /// String
        /// </summary>
        public const string VALIDATESTRING = @"^([a-zA-Z0-9 ])*$";
        /// <summary>
        /// Senha de Acesso
        /// </summary>
        public const string VALIDATEPASSWORD = @"^[A-Za-z0-9!@#_]{6,20}$";
        /// <summary>
        /// Web Site
        /// </summary>
        public const string VALIDATEWEBSITE = @"^((ht|f)tp(s?)\:\/\/|~/|/)?([\w]+:\w+@)?([a-zA-Z]{1}([\w\-]+\.)+([\w]{2,5}))(:[\d]{1,5})?((/?\w+/)+|/?)(\w+\.[\w]{3,4})?((\?\w+=\w+)?(&\w+=\w+)*)?";
        /// <summary>
        /// Email
        /// </summary>
        //public const string VALIDATEMAIL = @"^[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
        public const string VALIDATEMAIL = @"^[A-Za-z0-9](([_.-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([.-]?[a-zA-Z0-9]+)*)([.][A-Za-z]{2,4})$";
        /// <summary>
        /// Determina a expressão de Validação do Prazo de Validade de um cartão
        /// </summary>
        public static string VALIDATECARTAOVALIDADE = @"[0-1]{1}[0-9]{1}\/[0-9]{2}";
        /// <summary>
        /// Constante de Validação de CEP
        /// </summary>
        public static string VALIDATECEP = @"^\d{2}[\.]?\d{3}-?\d{3}$";
        /// <summary>
        /// Telefone
        /// </summary>
        public static string VALIDATEPHONE = @"^\d{8}$";
        /// <summary>
        /// Telefone Ativacao
        /// </summary>
        public static string VALIDATEACTIVATIONPHONE= @"[0-9]{2}[0-9]{2}[0-9]{8}";
        /// <summary>
        /// Celular Ativação
        /// </summary>
        //public static string VALIDATEACTIVATIONMOBILE = @"[0-9]{2}[0-9]{2}[0-9]{9}";
        public static string VALIDATEACTIVATIONMOBILE= @"[0-9]{9}";
        /// <summary>
        /// Caputa de Erros
        /// </summary>
        public static TrapError TrappedError = new TrapError();
        ///<summary>
        ///Valida uma Expressão Regular
        ///</summary>
        ///<param name="Text">Valor de Pesquisa</param>
        ///<param name="PatternMatch">Expressão Regular de Validação</param>
        ///<remarks>
        ///<list type="table">
        ///<listheader>
        ///<term>Expressão Regular</term>
        ///<description>Descrição</description>
        ///</listheader>
        ///<item>
        ///<term>VALIDATENAME</term>
        ///<description>Avalia um nome definido pelo tamanho minimo de 3 até 30 caracteres, permitindo somente caracteres alfanuméricos e espaços em branco</description>
        ///</item>
        ///<item>
        ///<term>VALIDATESTRING</term>
        ///<description>Avalia um expressão de texto</description>
        ///</item>
        ///<item>
        ///<term>VALIDATEPASSWORD</term>
        ///<description>Avalia um expressão do tipo senha de acesso</description>
        ///</item>
        ///<item>
        ///<term>VALIDATEWEBSITE</term>
        ///<description>Avalia um expressão do tipo endereço de site</description>
        ///</item>
        ///<item>
        ///<term>VALIDATEMAIL</term>
        ///<description>Avalia um expressão do tipo e-mail</description>
        ///</item>
        ///<item>
        ///<term>VALIDATECEP</term>
        ///<description>@"^\d{2}[\.]?\d{3}-?\d{3}$"</description>
        ///</item>
        ///<item>
        ///<term>VALIDATEPHONE</term>
        ///<description>@"^\d{8}$"</description>
        ///</item>
        ///<item>
        ///<term>VALIDATEACTIVATIONPHINE</term>
        ///<description>@"[0-9]{2}[0-9]{2}[0-9]{8}</description>
        ///</item>
        ///<item>
        ///<term>VALIDATEACTIVATIONMOBILE</term>
        ///<description>@"[0-9]{9}"</description>
        ///</item>
        ///</list>
        ///</remarks>
        ///<returns>bool</returns>
        public static bool isValid(string Text, string PatternMatch)
        {
            Regex pattern = new Regex(PatternMatch);
            return pattern.IsMatch(Text);
        }

        /// <summary>
        /// Retorna true se o email é valido
        /// </summary>
        /// <param name="input">Email</param>
        /// <returns>bool, se email válido</returns>
        public static bool IsValidMail(string input)
        {
            return IsValidMail(input, VALIDATEMAIL);
        }
        /// <summary>
        /// Retorna true se o email é valido
        /// </summary>
        /// <param name="input">Email</param>
        /// <param name="patternstring">Pattern String de Validação</param>
        /// <returns>bool, se email válido</returns>
        public static bool IsValidMail(string input, string patternstring)
        {
            TrappedError.SetError();
            if (input == "")
                TrappedError.SetError("EMAIL_NOTGIVEN");
            else
            {
                Regex pattern = new Regex(patternstring);
                Match match = pattern.Match(input);
                if (match.Success)
                    return true;
                else
                    TrappedError.SetError("INVALIDMAIL");
            }
            return false;
        }

        /// <summary>
        /// Verifica se a string passada corresponde a um Código de CEP válido
        /// </summary>
        /// <param name="input">string correspondente ao código do CEP</param>
        /// <returns>True, caso seja um CEP válido </returns>
        public static bool IsCepFormatoValido(string input)
        {
            return Regex.IsMatch(input, VALIDATECEP);
        }
        /// <summary>
        /// Verifica se a string passada corresponde a um Telefone válido
        /// </summary>
        /// <param name="input">String alfanumérica do telefone</param>
        /// <returns>true, se o telefone for válido</returns>
        public static bool IsTelefoneFormatoValido(string input)
        {
            bool go = true;
            TrappedError.SetError();
            if (input.Trim() != "")
            {
                go = Regex.IsMatch(input, VALIDATEPHONE);
            }
            if (!go)
                TrappedError.SetError("INVALIDPHONE");
            return go;
        }

        /// <summary>
        /// Verifica se o telefone esta num formato válido
        /// </summary>
        /// <param name="input">Telefone</param>
        /// <returns>true, se o formato é válido</returns>
        public static bool IsTelefoneAtivacaoFormatoValido(string input)
        {
            bool go = true;
            TrappedError.SetError();
            if (input.Trim() != "")
            {
        //string pattern = @"[0-9]{2}[0-9]{2}[0-9]{8}";
                go = Regex.IsMatch(input, VALIDATEACTIVATIONPHONE);
            }
            if (!go)
                TrappedError.SetError("INVALIDPHONE");
            return go;

        }
        /// <summary>
        /// Retorna true, se o telefone móvel está em formato válido 
        /// </summary>
        /// <param name="input">Número do Celular no formato 99999999</param>
        /// <remarks>
        /// Se o valor de input estiver vazio ou esta em um formato inválido, o código de erro 'INVALIDMOBILE' será atribuido à LastErrotCode
        /// </remarks>
        /// <returns></returns>
        public static bool IsCelularAtivacaoFormatoValido(string input)
        {
            bool go = true;
            TrappedError.SetError();
            if (input.Trim() != "")
            {
                //string pattern = @"[0-9]{2}[0-9]{2}[0-9]{9}";
                go = Regex.IsMatch(input, VALIDATEACTIVATIONMOBILE);
            }
            if (!go)
                TrappedError.SetError("INVALIDMOBILE");
            return go;

        }

        /// <summary>
        /// Verifica se a string passada é composta somente de letras
        /// </summary>
        /// <param name="_value">string a ser avaliada</param>
        /// <returns>True, se a string for composta somente de letras</returns>
        public static bool IsAlpha(string _value)
        {
            Regex oValidExpression = new Regex("[^a-zA-Z]");
            return !oValidExpression.IsMatch(_value);
        }

        /// <summary>
        /// Retorna true se o valor é composto somente de numeros
        /// </summary>
        /// <param name="value">string a ser avaliada</param>
        /// <returns>bool</returns>
        public static bool IsDigits(string value)
        {
            Regex oValidExpression = new Regex("[^0-9]");
            return !oValidExpression.IsMatch(value);
        }

        /// <summary>
        /// Verifica se a string passada é composta de letras e números
        /// </summary>
        /// <param name="_value">string a ser avaliada</param>
        /// <returns>True, se a string for composta de letras e números</returns>
        public static bool IsAlphaNumeric(string _value)
        {
            Regex oValidExpression = new Regex("[^a-zA-Z0-9]");
            return !oValidExpression.IsMatch(_value);
        }

        /// <summary>
        /// Verifica se a string passada corresponde a um valor inteiro
        /// </summary>
        /// <param name="strNumber">string a ser avaliada</param>
        /// <returns>True, se a string for correspondente a um valor inteiro</returns>
        public static bool IsInteger(string strNumber)
        {
            Regex objNotIntPattern = new Regex("[^0-9-]");
            Regex objIntPattern = new Regex("^-[0-9]+$|^[0-9]+$");

            return !objNotIntPattern.IsMatch(strNumber) &&
            objIntPattern.IsMatch(strNumber);
        }
        /// <summary>
        /// Verifica se a string passada corresponde a um número
        /// </summary>
        /// <param name="_value">string a ser avaliada</param>
        /// <returns>True, se a string for correspondente a um número</returns>
        public static bool IsNumber(string _value)
        {
            if (!String.IsNullOrWhiteSpace(_value))
            {
                Regex objNotNumberPattern = new Regex("[^0-9.-]");
                Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
                Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
                String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
                String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
                Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

                return !objNotNumberPattern.IsMatch(_value) &&
                !objTwoDotPattern.IsMatch(_value) &&
                !objTwoMinusPattern.IsMatch(_value) &&
                objNumberPattern.IsMatch(_value);
            }
            return false;
        }

        /// <summary>
        /// Retorna true se a string passada corresponde a um CNPJ/CPF
        /// </summary>
        /// <param name="pCODCMF">Código do CPF/CNPJ</param>
        /// <returns>bool</returns>
        public static bool isValidCMF(string pCODCMF)
        {
            if (pCODCMF.Length == 11)
                return isValidCPF(pCODCMF);
            return isValidCNPJ(pCODCMF);
        }


        /// <summary>
        /// Avalia se a string passada corresponde a um CNPJ
        /// </summary>
        /// <param name="cnpj">String com o valor do CNPJ</param>
        /// <remarks>
        /// CNPJ com valores consecutivos são considerados inválidos
        /// </remarks>
        /// <returns>True, se for um CNPJ válido</returns>
        public static bool isValidCNPJ(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
            {
                TrappedError.SetError("CMFINVALIDLENGTH");
                return false;
            }
            TrappedError.SetError();
            bool RETURN_VALUE = false;
            if (!IsSEQUENCECODE(cnpj))
            {

                tempCnpj = cnpj.Substring(0, 12);

                soma = 0;
                for (int i = 0; i < 12; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = resto.ToString();

                tempCnpj = tempCnpj + digito;
                soma = 0;
                for (int i = 0; i < 13; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = digito + resto.ToString();

                RETURN_VALUE = cnpj.EndsWith(digito);
                if (!RETURN_VALUE)
                    TrappedError.SetError("CMFERROR");

            }
            else
            {
                TrappedError.SetError("CMFSEQUENCEERROR");
            }
            return RETURN_VALUE;
        }

        /// <summary>
        /// Avalia se a string passada corresponde a um CPF
        /// </summary>
        /// <param name="cpf">string com o valor do CPF</param>
        /// CPF com valores consecutivos são considerados inválidos
        /// <returns>true, se for um CPF válido</returns>
        public static bool isValidCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            TrappedError.SetError();
            string tempCpf;
            string digito;
            int soma;
            int resto;
            bool RETURN_VALUE = false;
            if (cpf.Length != 11)
            {
                TrappedError.SetError("CMFINVALIDLENGTH");
                return false;
            }


            if (!IsSEQUENCECODE(cpf))
            {

                cpf = cpf.Trim();
                cpf = cpf.Replace(".", "").Replace("-", "");

                if (cpf.Length != 11)
                    return false;

                tempCpf = cpf.Substring(0, 9);
                soma = 0;
                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = resto.ToString();

                tempCpf = tempCpf + digito;

                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = digito + resto.ToString();
                RETURN_VALUE = cpf.EndsWith(digito);
                if(!RETURN_VALUE)
                    TrappedError.SetError("CMFERROR");
            }
            else
            {
                TrappedError.SetError("CMFSEQUENCEERROR");
            }
            return RETURN_VALUE;
        }
        /// <summary>
        /// Remove os caracteres '.-/' de uma string
        /// </summary>
        /// <param name="text">string a ser utilizada</param>
        /// <returns>string</returns>
        public static string Strip(string text)
        {
            return text.Replace(".", "").Replace("-", "").Replace("/", "").Replace("+", "");
        }



        /// <summary>
        /// /Retorna true se o Número do CARTAO é válido
        /// </summary>
        /// <param name="text">Número do Crtão</param>
        /// <returns></returns>
        public static bool IsValidNumeroCartao(string text)
        {
            Regex pattern = new Regex(@"[0-9]{16}");
            return pattern.IsMatch(text);
        }


        /// <summary>
        /// Retorna true se a validade do CARTAO é válida
        /// </summary>
        /// <param name="text">Validade do CARTAO</param>
        /// <returns>true, se a validade do cartão é válida</returns>
        public static bool IsValidValidadeCartao(string text)
        {
            Regex pattern = new Regex(VALIDATECARTAOVALIDADE);
            return pattern.IsMatch(text);
        }

        /// <summary>
        /// Determina se a regra de vencimento é valida
        /// </summary>
        /// <param name="_RegraVencimento">String da Regra de Vencimento</param>
        /// <returns>true, se a regra for válida</returns>
        /// <remarks>
        /// <para>A validação da regra é baseada em</para>
        /// <para>Os tres primeiros caracteres correspondem a digitos de 0 a 9</para>
        /// <para>Os tres caracteres subsequentes correspondem a qualquer letra</para>/// 
        /// </remarks>
        public static bool isValidRule(string _RegraVencimento)
        {
            Regex pattern = new Regex(@"[0-9]{3}[A-Z]{3}");
            return pattern.IsMatch(_RegraVencimento);
        }

        /// <summary>
        /// Verifica se é uma data válida
        /// </summary>
        /// <param name="_value">string no formato data</param>
        /// <returns>bool</returns>
        public static bool IsDate(string _value)
        {
            TrappedError.SetError();
            bool RETURN_VALUE = false;
            DateTime o = DateTime.Now;
            CultureInfo culture;
            DateTimeStyles styles;
            styles = DateTimeStyles.None;
            culture = CultureInfo.CreateSpecificCulture("pt-BR");
            if (DateTime.TryParse(_value, culture, styles, out o))
                RETURN_VALUE= true;

            if (!RETURN_VALUE)
                TrappedError.SetError("INVALID_DATE");
            return RETURN_VALUE;
        }


        /// <summary>
        /// Verifica se é um valor float válido
        /// </summary>
        /// <param name="_value">valor</param>
        /// <returns>true</returns>
        public static bool IsFloat(string _value)
        {
            float o = 0;
            if (float.TryParse(_value, out o))
                return true;
            return false;
        }
        /// <summary>
        /// Verifica se é um valor double válido
        /// </summary>
        /// <param name="_value">valor</param>
        /// <returns>true</returns>
        public static bool IsDouble(string _value)
        {
            double o = 0;
            if (double.TryParse(_value, out o))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se é um valor SHORT válido
        /// </summary>
        /// <param name="_value">valor</param>
        /// <returns>true</returns>
        public static bool IsShort(string _value)
        {
            short o = 0;
            if (short.TryParse(_value, out o))
                return true;
            return false;
        }
        /// <summary>
        ///  Verifica se é um valor do tipo Byte válido
        /// </summary>
        /// <param name="_value">String representando o valor a ser analisado</param>
        /// <returns>true, se for um valor do tipo byte válido</returns>
        public static bool IsByte(string _value)
        {
            byte o = 0;
            if (byte.TryParse(_value, out o))
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o CNPJ/CPF são uma sequencia repetida de valores
        /// </summary>
        /// <param name="pCODCNF">String do CNPJ/CPF</param>
        /// <returns>true, se for uma sequencia repetida de valores</returns>
        public static bool IsSEQUENCECODE(string pCODCNF)
        {
            int length = pCODCNF.Length;
            string r = "";
            bool RETURN_VALUE = false;
            for (int i = 0; i <= 9; i++)
            {
                r = "";
                for (int j = 0; j < length; j++)
                {
                    r += i.ToString();
                }
                if (pCODCNF == r)
                {
                    RETURN_VALUE = true;
                    break;
                }
            }
            return RETURN_VALUE;
        }

        /// <summary>
        /// Converte uma data no formato string para uma data válida
        /// </summary>
        /// <param name="date">string no formato de data</param>
        /// <remarks>
        /// Se a expressão 'date' estiver vazia retem o Código de Erro 'EMPTYDATE'
        /// Se o formato de entrada DD/MM/YYYY não corresponder ao formato de leitura retem o Código de Erro 'INVALID_FORMAT_DATE'
        /// </remarks>
        /// <returns>DateTime</returns>
        public static DateTime? StripDate(string date)
        {
            TrappedError.SetError();
            if (String.IsNullOrEmpty(date))
            {
                TrappedError.SetError("EMPTYDATE");
                return null;
            }
            try
            {
                var _d = int.Parse(date.Substring(0, 2));
                var _m = int.Parse(date.Substring(3, 2));
                var _a = int.Parse(date.Substring(6, 4));
                return new DateTime(_a, _m, _d);
            }
            catch (Exception Error)
            {
                TrappedError.SetError("INVALID_FORMAT_DATE");
                TrappedError.ErrorMessage= Error.Message;
            }
            return null;
        }

        /// <summary>
        /// Verifica se a data de nascimento é valida
        /// </summary>
        /// <param name="pDATNAC">Data de Nascimento no formadto dd/MM/yyyy</param>
        /// <remarks>
        /// Se o formato de entrada DD/MM/YYYY não corresponder ao formato de leituram,  retem o Código de Erro 'INVALIDDATNAC'
        /// Se o formato da data corresponder, o valor será atribuido para o campo 'FIELDDATE' com o tipo apropriado
        /// </remarks>
        /// <returns>bool, true se é uma data válida</returns>
        public static bool ValidDateofBirth(string pDATNAC)
        {
            TrappedError.SetError();
            FIELDDATE = null;
            bool RETURN_VALUE = false;
            if (!String.IsNullOrEmpty(pDATNAC.Trim()))
            {
                FIELDDATE = StripDate(pDATNAC);
                if (!TrappedError.HasError())
                {

                    if (FIELDDATE.HasValue)
                    {
                        try
                        {
                            var _d = int.Parse(pDATNAC.Substring(0, 2));
                            var _m = int.Parse(pDATNAC.Substring(3, 2));
                            var _a = int.Parse(pDATNAC.Substring(6, 4));
                            FIELDDATE = new DateTime(_a, _m, _d);
                            if (FIELDDATE.HasValue)
                                RETURN_VALUE = true;
                        }
                        catch (Exception Error)
                        {
                            TrappedError.SetError("INVALIDDATNAC");
                            TrappedError.ErrorMessage= Error.Message;
                            TrappedError.ErrorObject = Error;
                        }
                    }
                }
            }
            return RETURN_VALUE;
        }

        /// <summary>
        /// Obtêm um DateTime a partir de uma string do tipo data no formato YYYYMMDD
        /// </summary>
        /// <param name="value">String no formato de Data</param>
        /// <returns>Datetime</returns>
        public static DateTime GetDate(string value)
        {
            TrappedError.SetError();
            try
            {
                var _a = int.Parse(value.Substring(0, 4));
                var _m = int.Parse(value.Substring(5, 2));
                var _d = int.Parse(value.Substring(8, 2));
                return new DateTime(_a, _m, _d);
            }
            catch (Exception Error)
            {
                TrappedError.UserError = Error.Message;
                TrappedError.ErrorMessage = Error.Message;
                TrappedError.ErrorObject = Error;
            }
            return new DateTime(0, 0, 0);
        }


        /// <summary>
        /// Convert uma string numérica para um valor float
        /// </summary>
        /// <param name="value">string numérica</param>
        /// <returns>int</returns>
        public static float ToFloat(string value)
        {
            if (IsFloat(value))
            {
                if (value != "")
                    return Convert.ToSingle(value);
            }
            return 0;
        }

        /// <summary>
        /// Converte uma string em uma data válida 
        /// </summary>
        /// <param name="_value">string no formato data</param>
        /// <returns>DateTime</returns>
        public static DateTime? ToDate(string _value)
        {
            if (IsDate(_value))
            {
                DateTime o = DateTime.Now;
                CultureInfo culture;
                DateTimeStyles styles;
                styles = DateTimeStyles.None;
                culture = CultureInfo.CreateSpecificCulture("pt-BR");

                if (DateTime.TryParse(_value, culture, styles, out o))
                    return o;
            }
            return null;
        }

        /// <summary>
        /// Verifica se a string corresponde a um CNPJ
        /// </summary>
        /// <param name="input">CNPJ</param>
        /// <returns>true</returns>
        public static bool IsCNPJ(string input)
        {
            return (input.Length > 11);
        }


        /// <summary>
        /// Verifica se o valor fornecido é um inteiro dentro da faixa de valores especificados
        /// </summary>
        /// <param name="input">String</param>
        /// <returns>true, se for um inteiro válido</returns>
        public static bool IsIntValue(string input)
        {
            int value = int.Parse(input);
            if (value <= int.MaxValue)
                return true;
            return false;
        }

        /// <summary>
        /// Verifica se o valor fornecido é um long dentro da faixa de valores especificados
        /// </summary>
        /// <param name="input">String</param>
        /// <returns>true, se for um long válido</returns>
        public static bool IsLongValue(string input)
        {
            long value = long.Parse(input);
            if (value <= long.MaxValue)
                return true;
            return false;
        }

        /// <summary>
        ///  Verifica se a string 'input' é valida, caso contrário retorna o valor em defaultValue
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue"></param>
        /// <returns>string</returns>
        public static string StringValue(string input, string defaultValue)
        {
            string s = input;
            if (String.IsNullOrEmpty(input))
            {
                if (defaultValue != "")
                    s = defaultValue;
            }
            if (String.IsNullOrWhiteSpace(input))
            {
                if (defaultValue != "")
                    s = defaultValue;
            }
            return s;
        }

        /// <summary>
        /// Verifica se uma data está no formato DD/MM/YYYY
        /// </summary>
        /// <remarks>
        /// <para>onde DD [ 1 a 31, bissexto ]</para>
        /// <para>onde MM [ 1 a 12]</para>
        /// </remarks>
        /// <param name="input">string no formato data a ser avaliada</param>
        /// <returns>true, se estiver no formato base</returns>
        public static bool IsValidBaseDate(string input)
        {
            bool go = true;
            TrappedError.SetError();
            if (input.Trim() != "")
            {
                 go = Regex.IsMatch(input, @"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$");
            }
            if (!go)
                TrappedError.SetError("INVALID_FORMAT_DATE");
            return go;

        }
    }
}