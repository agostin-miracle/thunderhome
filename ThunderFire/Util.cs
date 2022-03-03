//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using System.Web;

//namespace ThunderFire
//{
//    public class Util : BaseClass
//    {

//        /// <summary>
//        /// Lingua Yoruba
//        /// </summary>
//        public const string YORUBA = "yoruba";
//        /// <summary>
//        /// Lingua Kimbundo
//        /// </summary>
//        public const string KIMBUNDO = "kimbundu";
//        /// <summary>
//        /// Lingua Kicongo
//        /// </summary>
//        public const string KICONGO = "kicongo";
//        /// <summary>
//        /// Lingua EWE
//        /// </summary>
//        public const string EWEGBE = "ewegbe";

//        /// <summary>
//        /// Lingua LOGBA
//        /// </summary>
//        public const string LOGBA = "logba";


//        /// <summary>
//        /// Lingua Base de Entrada para avaliação
//        /// </summary>
//        public string SourceLanguage { get; set; }

//        private string _palavra = "";
//        private int _itemword = 1;


//        public Util()
//        {
//        }

//        public string itemword(string t)
//        {
//            t = t.Trim();
//            if (t.CompareTo(_palavra) == 0)
//            {
//                _itemword++;
//            }
//            else
//            {
//                _palavra = t;
//                _itemword = 1;
//            }
//            return _itemword.ToString();
//        }


//        public static string replace(string _value, string _oldchar, string _newchar)
//        {
//            return _value.Replace(_oldchar, _newchar);
//        }

//        public static string firstchar(string _value)
//        {
//            try
//            {
//                _value = _value.Replace("?", "");
//                _value = _value.Replace("-", "");
//                _value = _value.Replace("'", "");
//                _value = _value.Replace("*", "");
//                _value = _value.Trim();

//                return _value.Substring(0, 1).ToLower();
//            }
//            catch (Exception)
//            {
//                return "";
//            }

//        }
//        public string Name(string texto)
//        {
//            int position = texto.IndexOf(' ');
//            if (position > 0)
//                return texto.Substring(0, position);
//            return texto;
//        }

//        public string SurName(string texto)
//        {
//            int position = texto.IndexOf(' ');
//            if (position > 0)
//                return texto.Substring(position);
//            return "";
//        }

//        public bool ContainText(string search, string dados)
//        {
//            return dados.ToLower().Contains(search.ToLower());
//        }

//        public string AlfaNumerico(string texto)
//        {
//            try
//            {
//                DateTime _date = DateTime.Now;
//                if (DateTime.TryParse(texto, out _date))
//                    return _date.ToString("yyyyMMddhhmmss");
//            }
//            catch
//            {


//            }
//            return texto;
//        }


//        public bool CommentSize(string texto)
//        {
//            try
//            {
//                return (texto.Trim().Length > 100);
//            }
//            catch
//            {
//            }
//            return false;
//        }

//        public bool contains(string fonte, string dados)
//        {
//            string[] words = dados.Split(',');
//            string[] seek = fonte.Split(',');
//            for (int j = 0; j < seek.Length; j++)
//            {
//                for (int p = 0; p < words.Length; p++)
//                {
//                    if (seek[j].Contains(words[p].ToString()))
//                        return true;
//                }
//            }
//            return false;
//        }


//        /// <summary>
//        /// Verifica se o parametro fonte contem qualquer item da string dados
//        /// </summary>
//        /// <param name="fonte">string a ser pesquisada</param>
//        /// <param name="dados">string de dados a ser avaliada</param>
//        /// <returns>True, se a string de dados nao tiver nenhum elemento</returns>
//        [Obsolete()]
//        public bool NoContain(string fonte, string dados)
//        {
//            int existe = 0;
//            try
//            {
//                if (fonte.CompareTo("") != 0)
//                {
//                    string[] words = dados.Split(',');
//                    for (int p = 0; p < words.Length; p++)
//                    {
//                        if (fonte.ToLower().Trim() == words[p].ToLower().Trim())
//                            existe = 1;
//                    }
//                }
//            }
//            catch (Exception)
//            {
//                return true;
//            }
//            return (existe == 0);
//        }
//        /// <summary>
//        /// Verifica se o parametro fonte contem qualquer item da string dados
//        /// </summary>
//        /// <param name="field">Campo a ser pesquisado</param>
//        /// <param name="dados">string de dados a ser avaliada</param>
//        /// <returns>True, se a string de dados nao tiver nenhum elemento</returns>
//        public int containlist(string field, string dados)
//        {
//            int existe = 0;
//            try
//            {
//                if (field.CompareTo("") != 0)
//                {
//                    string[] words = dados.Split(',');
//                    for (int p = 0; p < words.Length; p++)
//                    {
//                        if (field.ToLower().Trim() == words[p].ToLower().Trim())
//                        {
//                            existe = 1;
//                            break;
//                        }
//                    }
//                }
//            }
//            catch (Exception)
//            {
//                existe = 1;
//            }
//            return existe;
//        }

//        public bool Blocked(string tag, string forma)
//        {
//            bool retorno = true;
//            tag = trim(tag);
//            if ((tag == "corpo") || (tag == "folhas") || (tag == "divindades"))
//                retorno = false;

//            if (forma == "s.")
//                retorno = false;
//            return retorno;
//        }

//        //public string GetImage(string root)
//        //{
//        //    return GetImage(root, "");
//        //}
//        /// <summary>
//        /// Verifica se a imagem existe
//        /// </summary>
//        /// <param name="root">Diretório Base</param>
//        /// <param name="image">Nome da Imagem</param>
//        /// <returns>string</returns>
//        //public string GetImage(string root, string image)
//        //{
//        //    string relativename = root + image;
//        //    try
//        //    {
//        //        string fullname = HttpContext.Current.Server.MapPath(relativename);
//        //        if (File.Exists(fullname))
//        //            return relativename;
//        //    }
//        //    catch (StackOverflowException Error)
//        //    {
//        //        this.LastError = Error.Message;
//        //        this.ErrorObject = Error;
//        //        return "images/noimage.jpg";
//        //    }
//        //    catch (Exception Error)
//        //    {
//        //        this.LastError = Error.Message;
//        //        this.ErrorObject = Error;
//        //        return "images/noimage.jpg";
//        //    }
//        //    return "images/noimage.jpg";
//        //}


//        /// <summary>
//        /// Retorna a string fonética do verbete
//        /// </summary>
//        /// <param name="text">Verbete</param>
//        /// <param name="sourcelanguage">Base de Linguagem</param>
//        /// <returns>string</returns>
//        public string fonetica(string text, string sourcelanguage)
//        {
//            this.SourceLanguage = sourcelanguage;
//            return ifonetica(text, "");
//        }

//        /// <summary>
//        /// Retorna a string fonética do verbete
//        /// </summary>
//        /// <param name="text">Verbete</param>
//        /// <param name="sourcelanguage">Base de Linguagem</param>
//        /// <param name="dialeto">Dialeto de Aplicacao</param>
//        /// <returns>string</returns>
//        public string fonetica(string text, string sourcelanguage, string dialeto)
//        {
//            this.SourceLanguage = sourcelanguage;
//            return ifonetica(text, dialeto);
//        }
//        /// <summary>
//        /// Retorna a string fonética do verbete
//        /// </summary>
//        /// <param name="text">Verbete</param>
//        /// <param name="text">dialeto</param>
//        /// <returns>string</returns>
//        private string ifonetica(string text, string dialeto)
//        {
//            string _dialeto = dialeto.ToLower().Trim();
//            text = text.Trim().ToLower();
//            if (this.SourceLanguage == Util.YORUBA)
//            {
//                // u com til 360 Maiuscula
//                // u com til 361 Minuscula

//                text = text.Replace("ẹ", "&#603;");
//                text = text.Replace("ẹn", "&#603;&x#771;");
//                //text = text.Replace("&#7865;", "&#603;");
//                //text = text.Replace("&#7865;n", "&#603;&x#771;");
//                text = text.Replace("ọ", "&#596;");
//                text = text.Replace("ọ́n", "&#596;&#771;");
//                text = text.Replace("ọ̀", "&#596;&#771;");
//                text = text.Replace("&#7885;", "&#596;");
//                text = text.Replace("&#7885;&#x301;n", "&#596;&#771;");
//                text = text.Replace("&#7885;&#x300;n", "&#596;&#771;");
//                text = text.Replace("ṣ", "&#643");
//                text = text.Replace("&#7779;", "&#643");
//                text = text.Replace("un", "&#361;");
//                text = text.Replace("nu", "n&#361;");
//                text = text.Replace("j", "&#676;");
//                /*************/
//                text = text.Replace("an", "ã");
//                text = text.Replace("in", "ĩ");

//                /*************/
//                text = text.Replace("ń", "ɳ");
//                text = text.Replace("Ń", "ɳ");
//                text = text.Replace("&#7744", "ɱ");
//                text = text.Replace("&#7745", "ɱ");

//                //text = text.Replace("p", "kp͡	");
//                //text = text.Replace("gb", "ɡb͡	");
//                text = text.Replace("p", "kp");
//                text = text.Replace("gb", "ɡb");
//                text = text.Replace("&amp;", "&");
//            }
//            if (this.SourceLanguage == Util.KIMBUNDO)
//            {
//                text = text.Replace("j", "&#676;");
//                text = text.Replace("an", "ã");
//                text = text.Replace("amb", "ãb");
//                text = text.Replace("amp", "ãp");
//                text = text.Replace("in", "ĩ");
//                text = text.Replace("im", "ĩ");
//                text = text.Replace("ong", "õg");
//                text = text.Replace("omp", "õp");
//            }
//            if (this.SourceLanguage == Util.EWEGBE)
//            {

//                if (_dialeto == "")
//                {
//                    text = text.Replace("ny", "&#626;");
//                    text = text.Replace("ts", "&#679;");
//                    text = text.Replace("dz", "&#676;");
//                    text = text.Replace("&#331;", "ng");
//                    text = text.Replace("&#651;", "&#946;");
//                    text = text.Replace("ʋ", "&#946;");
//                    text = text.Replace("&amp;", "&");
//                    text = text.Replace("&#596;", "ó");
//                    text = text.Replace("&#598;", "d");
//                    text = text.Replace("ƒ", "&#632;");
//                    text = text.Replace("&#402;", "&#632;");
//                    text = text.Replace("e", "&#603;");
//                    text = text.Replace("è", "&#603;&#x300;");
//                    text = text.Replace("é", "&#603;&#x301;");
//                    text = text.Replace("an", "ã");
//                }
//                else if (_dialeto == "fon")
//                {
//                    text = text.Replace("h", "r");
//                }
//            }

//            return text;
//        }


//        /// <summary>
//        /// Retorna a string extendida do verbete
//        /// </summary>
//        /// <param name="text">Verbete</param>
//        /// <param name="sourcelanguage">Base de Linguagem</param>
//        /// <returns>string</returns>
//        public string extended(string text, string sourcelanguage)
//        {
//            this.SourceLanguage = sourcelanguage;
//            return extended(text);
//        }
//        /// <summary>
//        /// Retorna a string extendida do verbete
//        /// </summary>
//        /// <param name="text">Verbete</param>
//        /// <returns>string</returns>
//        public string extended(string text)
//        {
//            text = text.ToLower();

//            if (this.SourceLanguage == Util.YORUBA)
//            {
//                text = text.Replace("ş", "s");
//                text = text.Replace("&#7742;", "m");
//                text = text.Replace("&#7743;", "m");
//                text = text.Replace("&#7744;", "m");
//                text = text.Replace("&#7745;", "m");
//                text = text.Replace("&#7864;", "e");
//                text = text.Replace("&#7884;", "o");
//                text = text.Replace("&#7778;", "s");
//                text = text.Replace("ẹ", "e");
//                text = text.Replace("ọ", "o");
//                text = text.Replace("&#7865;", "e");
//                text = text.Replace("&#7865;&#x300;", "e");
//                text = text.Replace("&#7865;&#x301;", "e");
//                text = text.Replace("&#7885;", "o");
//                text = text.Replace("&#7885;&#x300;", "o");
//                text = text.Replace("&#7885;&#x301;", "o");
//                text = text.Replace("&#7779;", "s");
//                text = text.Replace("ş", "s");
//                text = text.Replace("ṣ", "s");
//                text = text.Replace("ń", "n");
//                text = text.Replace("Ń", "n");
//                text = text.Replace("ḿ", "m");
//                text = text.Replace("Ḿ", "m");
//                text = text.Replace("&#323;", "n");
//                text = text.Replace("&#324;", "n");
//                text = text.Replace("&#x300;", "");
//                text = text.Replace("&#x301;", "");
//                text = text.Replace("&#504;", "n");
//                text = text.Replace("&#505;", "n");
//                text = text.Replace("-", "");
//                text = text.Replace("!", "");
//                text = text.Replace("?", "");
//                text = text.Replace("̀", "");
//                text = text.Replace("́", "");
//                text = text.Replace(".", "");
//                text = text.Replace("  ", " ");
//            }

//            if (this.SourceLanguage == Util.KIMBUNDO)
//            {
//                text = text.Replace("-", "");
//                text = text.Replace("!", "");
//                text = text.Replace("?", "");
//                text = text.Replace("̀", "");
//                text = text.Replace("́", "");
//                text = text.Replace(".", "");
//                text = text.Replace("  ", " ");
//            }
//            if (this.SourceLanguage == Util.EWEGBE)
//            {
//                text = text.Replace("ʋ", "v");
//                text = text.Replace("ɖ", "d");
//                text = text.Replace("ny", "nh");
//                text = text.Replace("&#331;", "n");
//                text = text.Replace("ŋ", "n");
//                text = text.Replace("x", "ch");
//                text = text.Replace("&#596;", "o");
//                text = text.Replace("ɔ", "o");
//                text = text.Replace("ɖ", "d");
//                text = text.Replace("&#598;", "d");
//                text = text.Replace("ɣ", "g");
//                text = text.Replace("ƒ", "f");
//                text = text.Replace("ă", "a");
//                text = text.Replace("ɛ", "e");
//                text = text.Replace("ò", "o");
//            }

//            if (this.SourceLanguage == Util.LOGBA)
//            {
//                text = text.Replace("ʋ", "v");
//                text = text.Replace("ɖ", "d");
//                text = text.Replace("ny", "nh");
//                text = text.Replace("&#331;", "n");
//                text = text.Replace("ŋ", "n");
//                text = text.Replace("x", "ch");
//                text = text.Replace("&#596;", "o");
//                text = text.Replace("ɔ", "o");
//                text = text.Replace("ɖ", "d");
//                text = text.Replace("&#598;", "d");
//                text = text.Replace("ɣ", "g");
//                text = text.Replace("ƒ", "f");
//            }
//            text = removeacentos(text);
//            return text.Trim();
//        }

//        public string substring(string t, int i, int f)
//        {
//            if (t != "")
//                return t.Substring(i, f).ToUpper();
//            return "";
//        }
//        /// <summary>
//        /// Converte o texto para caixa alta
//        /// </summary>
//        /// <param name="_value">texto</param>
//        /// <returns>string</returns>
//        public string upper(string _value)
//        {
//            return _value.ToUpper().ToString();
//        }
//        /// <summary>
//        /// Converte o texto para caixa baixa
//        /// </summary>
//        /// <param name="_value">texto</param>
//        /// <returns>string</returns>
//        public string lower(string text)
//        {

//            if (text != null)
//            {
//                if (this.SourceLanguage == Util.YORUBA)
//                {
//                    //text = text.Replace("&#7744;", "ṁ");
//                    //text = text.Replace("&#7742;", "ḿ");
//                    //text = text.Replace("&#7864;", "ẹ");
//                    ////text = text.Replace("&#7865;", "ẹ");
//                    //text = text.Replace("&#7884;", "ọ");
//                    //text = text.Replace("&#7864;", "&#7865;");
//                    //text = text.Replace("&#7884;", "&#7885;");
//                    //text = text.Replace("&#7778;", "&#7779;");
//                    //text = text.Replace("&#323;", "ń");
//                    ////text = text.Replace("&#324;", "ń");
//                    //text = text.Replace("&#505;", "&#504;");
//                    ////text = text.Replace("&#x300;", "̀");
//                    ////text = text.Replace("&#x301;", "́");
//                }

//                else if (this.SourceLanguage == Util.EWEGBE)
//                {
//                    text = text.Replace("&#x330;", "&#x331;");
//                }
//                //return text.ToLower().ToString().Trim();
//            }
//            return text.ToLower();
//        }



//        public bool inlist(string _value, string list)
//        {
//            string[] _list = list.Split(',');
//            for (int i = 0; i < _list.Length; i++)
//            {
//                if (lower(_value) == lower(_list[i]))
//                    return true;
//            }
//            return false;
//        }


//        /// <summary>
//        /// Retorna a string fonética do verbete
//        /// </summary>
//        /// <param name="text">Verbete</param>
//        /// <param name="sourcelanguage">Base de Linguagem</param>
//        /// <returns>string</returns>
//        public string translate(string text, string sourcelanguage)
//        {
//            this.SourceLanguage = sourcelanguage;
//            return itranslate(text, "");
//        }

//        public string translate(string text, string sourcelanguage, string dialeto)
//        {
//            this.SourceLanguage = sourcelanguage;
//            return itranslate(text, dialeto);
//        }

//        /// <summary>
//        /// Obtem a fonética do texto passado
//        /// </summary>
//        /// <param name="text">Verberte</param>
//        /// <param name="text">disleto</param>
//        /// <returns>string</returns>
//        private string itranslate(string text, string dialeto)
//        {
//            string c = "";
//            string n = "";
//            string r = "";
//            string p = "";
//            string t = "";
//            bool b = false;
//            text = lower(text);
//            string _dialeto = dialeto.ToLower().Trim();

//            string[] arg = text.Split(' ');
//            string result = "";
//            string word = "";

//            if (this.SourceLanguage == Util.YORUBA)
//            {
//                for (int j = 0; j < arg.Length; j++)
//                {
//                    word = arg[j];

//                    word = word.Replace("mọn", "man");
//                    word = word.Replace("mọ́n", "man");
//                    word = word.Replace("gọ́n", "gan");
//                    word = word.Replace("yọ́n", "yan");
//                    word = word.Replace("fọ́n", "fan");
//                    word = word.Replace("pọ̀n", "pan");
//                    word = word.Replace("ṣọ̀n", "xan");
//                    word = word.Replace("fọn", "fan");
//                    word = word.Replace("à", "â");
//                    word = word.Replace("ã", "aa");
//                    word = word.Replace("ẽ", "ee");
//                    word = word.Replace("è", "ê");
//                    word = word.Replace("ĩ", "ii");
//                    word = word.Replace("õ", "oo");
//                    word = word.Replace("ò", "ô");
//                    word = word.Replace("ṣ", "x");
//                    word = word.Replace("w", "u");
//                    word = word.Replace("y", "i");
//                    word = word.Replace("&#504;", "un");
//                    word = word.Replace("&#7745;", "un");
//                    word = word.Replace("ń", "un");
//                    word = word.Replace("j", "dj");
//                    word = word.Replace("p", "kp");
//                    r = "";
//                    for (int i = 0; i < word.Length; i++)
//                    {
//                        b = isBeginWord(i, word);
//                        c = word.Substring(i, 1);
//                        if ((i + 1) < word.Length)
//                            n = word.Substring(i + 1, 1);

//                        if ((i + 2) < word.Length)
//                            p = word.Substring(i + 2, 1);

//                        if (c == "ẹ")
//                        {
//                            if (n == "̀")
//                                r += "ê";
//                            else
//                                r += "é";
//                        }
//                        else if (c == "ọ")
//                        {
//                            if (n == "́")
//                                r += "ó";
//                            else if (n == "̀")
//                                r += "ô";
//                            else
//                                r += "ó";
//                        }

//                        else if (c == "h")
//                        {
//                            if (b)
//                                r += "r";
//                            else
//                                r += "rr";

//                            //r += "r";

//                        }
//                        else if (c == "s")
//                        {
//                            if (!b)
//                                r += "ss";
//                            else
//                                r += "s";
//                        }

//                        else if (c == "g")
//                        {
//                            if (n == "b")
//                                r += "gui";
//                            else
//                            {
//                                if ((n == "e") || (n == "é"))
//                                    r += "gu";
//                                else
//                                    r += "g";
//                            }
//                        }
//                        else
//                            r += c.Replace("́", "").Replace("̀", "");

//                    }
//                    result += r + " ";
//                }
//            }

//            if (this.SourceLanguage == Util.KICONGO)
//            {
//                for (int j = 0; j < arg.Length; j++)
//                {
//                    word = arg[j];
//                    word = word.Replace("s", "ss");
//                    if (word.Length > 2)
//                    {
//                        if (word.Substring(0, 2) == "ss")
//                        {
//                            word = "s" + word.Substring(2);
//                        }
//                    }
//                    if (word.IndexOf("n") == 0)
//                        word = "un" + word.Substring(1);
//                    r = word.Trim();
//                    result += r + " ";
//                }
//            }

//            if (this.SourceLanguage == Util.KIMBUNDO)
//            {
//                t = "";
//                for (int j = 0; j < arg.Length; j++)
//                {
//                    t += arg[j] + " ";
//                }
//                word = t.Replace("ri", "di");
//                word = word.Replace("s", "ss");
//                word = word.Replace("ge", "gue");
//                word = word.Replace("gi", "gui");
//                word = word.Replace("nh", "ni");
//                if (word.Length > 2)
//                {
//                    if (word.Substring(0, 2) == "ss")
//                        word = "s" + word.Substring(2);
//                    if (word.Substring(0, 3) == "-ss")
//                        word = "s" + word.Substring(3);
//                }
//                word = word.Replace(" ss", "s");
//                result = word.Trim();
//                //if (word.IndexOf("nh") > 0)
//                //    word = word.Replace("nh", "ni");
//                //result += word.Trim() + " ";

//                //for (int j = 0; j < arg.Length; j++)
//                //{
//                //    word = arg[j];
//                //    word = word.Replace("ri", "di");
//                //    word = word.Replace("s", "ss");
//                //    word = word.Replace("ge", "gue");
//                //    word = word.Replace("gi", "gui");
//                //    if (word.Length > 2)
//                //    {
//                //        if (word.Substring(0, 2) == "ss")
//                //            word = "s" + word.Substring(3);
//                //    }
//                //    if (word.IndexOf("nh") > 0)
//                //        word = word.Replace("nh", "ni");
//                //    result += word.Trim() + " ";
//                //}
//            }
//            if (this.SourceLanguage == Util.EWEGBE)
//            {
//                word = text;
//                if (_dialeto == "fon")
//                {
//                    word = TranslateChar(word);
//                    word = word.Replace("h", "r");          // 1 //
//                    word = word.Replace("x", "h");          // 2 //
//                    word = word.Replace("gb", "guib");
//                    word = word.Replace("à", "a");
//                    word = word.Replace("ã", "an");
//                    word = word.Replace("è", "ê");
//                    word = word.Replace("é", "é");
//                    //word = word.Replace("ɛ̀ ", "ê");
//                    //word = word.Replace("ɛ", "e");
//                    word = word.Replace("e͂", "en");
//                    word = word.Replace("ì", "i");
//                    word = word.Replace("ĩ̀", "ìn");
//                    word = word.Replace("ɲ", "nh");
//                    word = word.Replace("&#365;", "u"); // u breve
//                    word = word.Replace("&#331;", "gn");
//                    word = word.Replace("ŋ", "n");
//                    //word = word.Replace("ɔ̀ ", "ô");
//                    //word = word.Replace("ɔ́ ", "ò");      // &#596;&#x300
//                    //word = word.Replace("ɔ͂", "on");
//                    //word = word.Replace("ɔ", "o");
//                    //word = word.Replace("ɔ̀", "ô");


//                    //word = word.Replace("&#598;", "d");
//                    word = word.Replace("ɖ", "d");
//                    word = word.Replace("ɣ", "g");
//                    word = word.Replace("ɣ", "i");
//                    //word = word.Replace("&#402;", "f");
//                    //word = word.Replace("&#611;", "f");

//                    word = word.Replace("onb", "omb");
//                    word = word.Replace("w", "u");
//                    word = word.Replace("ú͂̀", "ún");
//                    word = word.Replace("uù", "û");
//                    word = word.Replace("uu", "u");
//                    word = word.Replace("uú", "ú");
//                    word = word.Replace("ʋ", "v");
//                    word = word.Replace("yĭ", "i");
//                    word = word.Replace("iĭ", "i");
//                    word = word.Replace("ii", "i");
//                    word = word.Replace("j", "dj");
//                }
//                else
//                {

//                    word = word.Replace("gb", "guib");
//                    word = word.Replace("à", "a");
//                    word = word.Replace("ã", "an");
//                    word = word.Replace("è", "ê");
//                    word = word.Replace("é", "é");
//                    word = word.Replace("ɛ", "e");
//                    word = word.Replace("e͂", "en");
//                    word = word.Replace("ĩ́", "ín");
//                    word = word.Replace("ĩ̀", "ìn");
//                    word = word.Replace("ɲ", "nh");
//                    word = word.Replace("&#365;", "u"); // u breve
//                    word = word.Replace("&#331;", "gn");
//                    word = word.Replace("ŋ", "n");
//                    word = word.Replace("x", "ch");
//                    word = word.Replace("ɔ͂", "on");
//                    word = word.Replace("ɔ", "o");

//                    //word = word.Replace("&#598;", "d");
//                    word = word.Replace("ɖ", "d");
//                    //word = word.Replace("&#596;", "o");
//                    word = word.Replace("ɣ", "g");
//                    //word = word.Replace("&#402;", "f");
//                    //word = word.Replace("&#611;", "f");
//                    word = word.Replace("onb", "omb");
//                    word = word.Replace("w", "u");
//                    word = word.Replace("ú͂̀", "ún");
//                    word = word.Replace("u͂", "un");
//                    word = word.Replace("uu", "u");
//                    word = word.Replace("ʋ", "v");
//                    word = word.Replace("ts", "ch");
//                    word = word.Replace("dz", "j");

//                    word = word.Replace("yĭ", "i");
//                }

//                result = word.Trim() + " ";
//            }
//            return result.Trim();
//        }


//        private string TranslateChar(string s)
//        {
//            char[] r = s.ToCharArray();
//            int len = r.Length;
//            string rs = "";
//            string t = "";
//            for (int i = 0; i < len; i++)
//            {
//                if (r[i] == 596)
//                {
//                    if ((i + 1) < len)
//                    {
//                        if (r[i + 1] == 768) //`
//                        {
//                            t = "ŏ";
//                        }
//                        else if (r[i + 1] == 769) //´
//                        {
//                            if ((i + 2) < len)
//                            {
//                                if (r[i + 2] == 774)
//                                    t = "òn";
//                                else if (r[i + 2] == 771)
//                                    t = "òn";
//                                else
//                                    t = "ò";
//                            }
//                            else
//                            {
//                                t = "ò";
//                            }
//                        }
//                        else
//                        {
//                            t = "o";
//                        }


//                    }
//                }
//                else if (r[i] == 603)
//                {
//                    if ((i + 1) < len)
//                    {
//                        if (r[i + 1] == 768)
//                        {
//                            t = "e";
//                        }
//                        else if (r[i + 1] == 769)
//                        {
//                            t = "è";
//                        }
//                        else
//                        {
//                            t = "e";
//                        }

//                    }
//                }
//                else if (r[i] == 7869)
//                {
//                    if ((i + 1) < len)
//                    {
//                        if (r[i + 1] == 768)
//                        {
//                            t = "en";
//                        }
//                        else if (r[i + 1] == 769)
//                        {
//                            t = "én";
//                        }
//                        else
//                        {
//                            t = "en";
//                        }

//                    }
//                }
//                else if (r[i] == 331)
//                {
//                    if ((i + 1) < len)
//                    {
//                        if (r[i + 1] == 768)
//                        {
//                            t = "in";
//                        }
//                        else if (r[i + 1] == 769)
//                        {
//                            t = "ín";
//                        }
//                        else
//                        {
//                            t = "in";
//                        }

//                    }
//                }

//                else
//                {
//                    if ((r[i] == 768) || (r[i] == 769) || (r[i] == 774) || (r[i] == 771))
//                    {
//                        t = "";
//                    }
//                    else
//                    {
//                        t = r[i].ToString();
//                    }
//                }
//                rs += t;
//            }


//            return rs;
//        }

//        /// <summary>
//        /// Retorna a primeira letra do verbete
//        /// </summary>
//        /// <param name="text">Verbete</param>
//        /// <returns>string</returns>
//        public string Letter(string text, string sourcelanguage)
//        {
//            string _letra = text.Trim().ToUpper() + "  ";
//            if (_letra.Trim() == "")
//            {
//                return "";
//            }
//            if (this.SourceLanguage == Util.YORUBA)
//            {
//                _letra = _letra.Substring(0, 2).Trim();
//                if (_letra != "")
//                {
//                    if (_letra.CompareTo("Ẹ̀") == 0)
//                        _letra = "Ẹ";
//                    else if (_letra.CompareTo("Ọ̀") == 0)
//                        _letra = "Ọ";
//                    else if (_letra.CompareTo("Ọ́") == 0)
//                        _letra = "Ọ";
//                    else if (_letra.CompareTo("Ṣ") == 0)
//                        _letra = "Ṣ";
//                    else if ((_letra == "Ẽ") || (_letra.Substring(0, 1) == "Ẽ") || (_letra.Substring(0, 1) == "È") || (_letra.Substring(0, 1) == "Ê"))
//                        _letra = "E";
//                    else if ((_letra == "Ń") || (_letra.Substring(0, 1) == "Ń"))
//                        _letra = "N";
//                    else if ((_letra.Substring(0, 1) == "Ã") || (_letra.Substring(0, 1) == "À") || (_letra.Substring(0, 1) == "Á") || (_letra.Substring(0, 1) == "Â"))
//                        _letra = "A";
//                    else if (_letra.Substring(0, 1) == "Ì")
//                        _letra = "I";
//                    else if (_letra.Substring(0, 1) == "Ḿ")
//                        _letra = "M";
//                    else if ((_letra.Substring(0, 1) == "Õ") || (_letra.Substring(0, 1) == "Ò") || (_letra.Substring(0, 1) == "Ô") || (_letra.Substring(0, 1) == "Ó"))
//                        _letra = "O";
//                    else
//                        _letra = _letra.Substring(0, 1).ToUpper().Trim();
//                }
//            }
//            else
//            {

//                if ((_letra.Substring(0, 1) == "-") || (_letra.Substring(0, 1) == "'"))
//                    _letra = _letra.Substring(1);

//                _letra = _letra.Substring(0, 1).Trim();

//                if ((_letra == "Ẽ") || (_letra.Substring(0, 1) == "Ẽ") || (_letra.Substring(0, 1) == "È") || (_letra.Substring(0, 1) == "Ê") || (_letra.Substring(0, 1) == "É"))
//                    _letra = "E";
//                else if ((_letra == "Ń") || (_letra.Substring(0, 1) == "Ń"))
//                    _letra = "N";
//                else if ((_letra.Substring(0, 1) == "Ã") || (_letra.Substring(0, 1) == "À") || (_letra.Substring(0, 1) == "Á") || (_letra.Substring(0, 1) == "Â"))
//                    _letra = "A";
//                else if ((_letra.Substring(0, 1) == "Ì") || (_letra.Substring(0, 1) == "Í"))
//                    _letra = "I";
//                else if (_letra.Substring(0, 1) == "Ḿ")
//                    _letra = "M";
//                else if ((_letra.Substring(0, 1) == "Õ") || (_letra.Substring(0, 1) == "Ò") || (_letra.Substring(0, 1) == "Ô") || (_letra.Substring(0, 1) == "Ó"))
//                    _letra = "O";
//                else if ((_letra.Substring(0, 1) == "Ú") || (_letra.Substring(0, 1) == "Ù"))
//                    _letra = "U";
//                else if ((_letra.Substring(0, 1) == "Ɖ")) 
//                    _letra = "D";

//                else
//                    _letra = _letra.Substring(0, 1).ToUpper().Trim();

//            }
//            return _letra;
//        }

//        private bool isBeginWord(int p, string s)
//        {
//            string z = "";
//            if (p == 0)
//                return true;
//            else
//            {
//                z = s.Substring(p - 1, 1);
//                if ((z.CompareTo(" ") == 0) || (z.CompareTo("-") == 0))
//                {
//                    return true;
//                }
//            }
//            return false;
//        }
//        /// <summary>
//        /// Retorna a data atual
//        /// </summary>
//        /// <returns></returns>
//        public string now()
//        {
//            return DateTime.Now.ToString("dd/MM/yyyy");
//        }

//        public string fieldvalue(string t)
//        {
//            t = t.Trim();
//            if (t == "")
//                return ".";
//            return t;
//        }


//        /// <summary>
//        /// Remove os espaços em branco
//        /// </summary>
//        /// <param name="t">texto</param>
//        /// <returns>string</returns>
//        public string trim(string t)
//        {
//            if ((!String.IsNullOrWhiteSpace(t)) || (!String.IsNullOrEmpty(t)))
//                return t.Trim();
//            return "";
//        }

//        public string propername(string t)
//        {
//            return t.ProperName();
//        }

//        public string value(string t)
//        {
//            if ((!String.IsNullOrWhiteSpace(t)) || (!String.IsNullOrEmpty(t)))
//                return int.Parse(t).ToString();;
//            return "0";
//        }

//        /// <summary>
//        /// Retorna true se a string é nula ou vazia
//        /// </summary>
//        /// <param name="_value"></param>
//        /// <returns></returns>
//        public bool empty(string _value)
//        {
//            if(String.IsNullOrEmpty(_value) || String.IsNullOrWhiteSpace(_value))
//                return true;
//            return false;
//        }

//        /// <summary>
//        /// Remove os acentos da string fornecida
//        /// </summary>
//        /// <param name="text">texto</param>
//        /// <returns>string</returns>
//        public string removeacentos(string text)
//        {
//            return text.NoAccents().Trim();
//        }

//        public string roundhtmltag(string tag, string t)
//        {
//            return String.Format("<{0}>{1}</{0}>", tag, t);
//        }

//        public string fullword(string text, string sourcelanguage)
//        {
//            this.SourceLanguage = sourcelanguage;
//            return fullword(text);
//        }

//        public string fullword(string text)
//        {
//            text = text.ToLower().Trim();
//            if (this.SourceLanguage == Util.YORUBA)
//            {
//                //text = text.Replace("ş", "s");
//                text = text.Replace("&#7742;", "Ḿ");
//                text = text.Replace("&#7743;", "ḿ");
//                text = text.Replace("&#7744;", "Ḿ");
//                text = text.Replace("&#7745;", "ḿ");
//                text = text.Replace("&#7864;", "Ẹ");
//                text = text.Replace("&#7884;", "Ọ");
//                text = text.Replace("&#7778;", "Ṣ");
//                //text = text.Replace("ẹ", "e");
//                //text = text.Replace("ọ", "o");

//                ////text = text.Replace("&#7865;", "ẹ");
//                ////text = text.Replace("&#7865;&#x300;", "ẹ̀");
//                ////text = text.Replace("&#7865;&#x301;", "ẹ́");
//                ////text = text.Replace("&#7885;", "ọ");
//                ////text = text.Replace("&#7885;&#x300;", "ọ̀");
//                ////text = text.Replace("&#7885;&#x301;", "ọ́");
//                ////text = text.Replace("&#7779;", "ṣ");
//                ////text = text.Replace("ş", "ṣ");
//                text = text.Replace("ẹ̀", "ê");
//                text = text.Replace("ẹ́", "é");
//                text = text.Replace("ẹ", "é");
//                text = text.Replace("ọ̀", "ô");
//                text = text.Replace("ọ́", "ó");
//                text = text.Replace("ọ", "ó");

//                text = text.Replace("&#7865;", "é");
//                text = text.Replace("&#7865;&#x300;", "ê");
//                text = text.Replace("&#7865;&#x301;", "é");
//                text = text.Replace("&#7885;", "ó");
//                text = text.Replace("&#7885;&#x300;", "ô");
//                text = text.Replace("&#7885;&#x301;", "ó");
//                text = text.Replace("&#7779;", "s");
//                text = text.Replace("ş", "s");
//                text = text.Replace("ṣ", "s");

//                //text = text.Replace("ṣ", "s");
//                //text = text.Replace("ń", "n");
//                //text = text.Replace("Ń", "n");
//                //text = text.Replace("ḿ", "m");
//                //text = text.Replace("Ḿ", "m");
//                //text = text.Replace("&#323;", "n");
//                text = text.Replace("&#324;", "ń");
//                //text = text.Replace("&#x300;", "");
//                //text = text.Replace("&#x301;", "");
//                //text = text.Replace("&#504;", "n");
//                text = text.Replace("&#505;", "ń");
//                //text = text.Replace("-", "");
//                //text = text.Replace("!", "");
//                //text = text.Replace("?", "");
//                //text = text.Replace("̀", "");
//                //text = text.Replace("́", "");

//                //text = text.Replace("à", "a");
//                //text = text.Replace("è", "e");
//                //text = text.Replace("ì", "i");
//                //text = text.Replace("ò", "o");
//                //text = text.Replace("ù", "u");
//                //text = text.Replace("á", "a");
//                //text = text.Replace("é", "e");
//                //text = text.Replace("í", "i");
//                //text = text.Replace("ó", "o");
//                //text = text.Replace("ú", "u");

//                text = text.Replace(".", "");
//                text = text.Replace("  ", " ");
//                text = text.Replace("-", "");

//                //text = WordCharNumber(text);

//            }

//            if (this.SourceLanguage == Util.KIMBUNDO)
//            {
//                text = text.Replace(".", "");
//                text = text.Replace("  ", " ");
//                text = text.Replace("-", "");
//            }

//            if (this.SourceLanguage == Util.EWEGBE)
//            {
//                text = text.Replace("ʋ", "v");
//                text = text.Replace("ɖ", "d");
//                text = text.Replace("ny", "nh");
//                text = text.Replace("&#331;", "n");
//                text = text.Replace("ŋ", "n");
//                text = text.Replace("x", "ch");
//                text = text.Replace("ɔ", "o");
//                text = text.Replace("ɔ", "o");
//                text = text.Replace("ɖ", "d");
//                text = text.Replace("ɖ", "d");
//                text = text.Replace("ɣ", "g");
//                text = text.Replace("ƒ", "f");
//            }

//            if (this.SourceLanguage == Util.LOGBA)
//            {

//                text = text.Replace("ʋ", "v");
//                text = text.Replace("ɖ", "d");
//                text = text.Replace("ny", "nh");
//                text = text.Replace("Ŋ", "n");
//                text = text.Replace("ŋ", "n");
//                text = text.Replace("x", "ch");
//                text = text.Replace("ɔ", "o");
//                text = text.Replace("ɖ", "d");
//                text = text.Replace("ɣ", "g");
//                text = text.Replace("ƒ", "f");
//            }
//            return text.Trim();
//        }

     

//        /// <summary>
//        /// Substitui e Adiciona um texto
//        /// </summary>
//        /// <param name="text">Texto  a ser avaliado</param>
//        /// <param name="replacetext">Texto a ser substituido e justaposot</param>
//        /// <returns>string</returns>
//        public static string replaceadd(string text, string replacetext)
//        {
//            text = text.Trim();
//            text = text.Replace(replacetext, "");
//            text += replacetext;
//            return text;
//        }

//        /// <summary>
//        /// Verifica se a imagem existe
//        /// </summary>
//        /// <param name="root">Diretório Base</param>
//        /// <param name="image">Nome da Imagem</param>
//        /// <returns>string</returns>
//        public string GetImage(string root, string image)
//        {
//            string relativename = root + image;
//            try
//            {
//                string fullname = HttpContext.Current.Server.MapPath(relativename);
//                if (File.Exists(fullname))
//                    return relativename;
//            }
//            //catch (System.StackOverflowException Error)
//            //{
//            //    return "images/noimage.jpg";
//            //}
//            catch
//            {
//                return "images/noimage.jpg";
//            }

//            return "images/noimage.jpg";
//        }
//    }
//}
