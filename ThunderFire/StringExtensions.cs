//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ThunderFire
//{
//    public static class StringExtensions
//    {
//        /// <summary>
//        /// Retorna um fragmento de uma string a partir da posicao position com comprimento length
//        /// </summary>
//        /// <param name="t"></param>
//        /// <param name="p"></param>
//        /// <param name="n"></param>
//        /// <returns></returns>
//        public static string Mid(this string t, int position, int length)
//        {
//            return t.Substring(position - 1, length);
//        }

//        /// <summary>
//        /// Retorna o fragmento de uma string a partir de uma posicao
//        /// </summary>
//        /// <param name="t">string</param>
//        /// <param name="position">posição inicial</param>
//        /// <returns>string</returns>
//        public static string Mid(string t, int position)
//        {
//            return t.Substring(position);
//        }

//        /// <summary>
//        /// Retorna a parte esquerda da string em n posicoes
//        /// </summary>
//        /// <param name="t">String</param>
//        /// <param name="n">Número de Posições</param>
//        /// <returns>string</returns>
//        public static string Left(this string t, int length)
//        {
//            return t.Substring(0, length);
//        }
//        /// <summary>
//        /// Retorna a parte esquerda da string em n posicoes
//        /// </summary>
//        /// <param name="t">String</param>
//        /// <param name="length">Número de Posições</param>
//        /// <returns></returns>
//        public static string Right(this string t, int length)
//        {
//            return t.Substring(t.Length - length, length);
//        }
//        public static string Stuff(this string text, int position, int length, string replace)
//        {
//            int l = text.Length;
//            string s1 = text.Substring(0, position);
//            string s2 = "";
//            if ((position + length) <= l)
//                s2 = text.Substring(position + length);
//            else
//                s2 = text.Substring(position + (l - position));
//            return s1 + replace.Substring(0, length) + s2;
//        }
//        public static string Stuff(this string text, int position, string replace)
//        {
//            int l = text.Length;
//            string s1 = text.Substring(0, position);
//            string s2 = text.Substring(position);
//            return s1 + replace + s2;
//        }

//        /// <summary>
//        /// Lê a primeira palavra de uma frase ou nome
//        /// </summary>
//        /// <param name="texto">Texto da Frase ou Nome</param>
//        /// <returns>string</returns>
//        public static string GetFirstWord(this string text)
//        {
//            text = text.Trim();
//            if (text != "")
//            {
//                string[] f = text.Split(' ');
//                return f[0].ToString();
//            }
//            return "";
//        }
//        public static string NoConjunction(this string t)
//        {
//            t = t.Replace(" À ", "").ToString();
//            t = t.Replace(" A ", "").ToString();
//            t = t.Replace(" De ", "").ToString();
//            t = t.Replace(" de ", "").ToString();
//            t = t.Replace(" E ", "").ToString();
//            t = t.Replace(" Em ", "").ToString();
//            t = t.Replace(" Do ", "").ToString();
//            t = t.Replace(" Da ", "").ToString();
//            t = t.Replace(" De ", "").ToString();
//            t = t.Replace(" Das ", "").ToString();
//            t = t.Replace(" Dos ", "").ToString();
//            t = t.Replace(" Nas ", "").ToString();
//            t = t.Replace(" Nos ", "").ToString();
//            t = t.Replace(" Na ", "").ToString();
//            t = t.Replace(" Of ", "").ToString();
//            t = t.Replace(" No ", "").ToString();
//            t = t.Replace(" Para ", "").ToString();
//            t = t.Replace(" O ", "").ToString();
//            t = t.Replace(" Os ", "").ToString();
//            t = t.Replace(" Ou ", "").ToString();
//            t = t.Replace(" Que ", "").ToString();
//            t = t.Replace(" É ", "").ToString();
//            t = t.Replace(" Qual ", "").ToString();
//            t = t.Replace(" Se ", "").ToString();
//            t = t.Replace(" Faz ", "").ToString();
//            t = t.Replace(" Of ", "").ToString();
//            t = t.Replace(" The ", "").ToString();
//            t = t.Replace(" To ", "").ToString();
//            return t.Trim();
//        }
//        public static string LowerConjunction(this string t)
//        {
//            t = t.Replace(" À ", " à ").ToString();
//            t = t.Replace(" A ", " a ").ToString();
//            t = t.Replace(" De ", " de ").ToString();
//            t = t.Replace(" E ", " e ").ToString();
//            t = t.Replace(" Em ", " em ").ToString();
//            t = t.Replace(" Do ", " do ").ToString();
//            t = t.Replace(" Da ", " da ").ToString();
//            t = t.Replace(" Das ", " das ").ToString();
//            t = t.Replace(" Dos ", " dos ").ToString();
//            t = t.Replace(" Nas ", " nas ").ToString();
//            t = t.Replace(" Nos ", " nos ").ToString();
//            t = t.Replace(" Na ", " na ").ToString();
//            t = t.Replace(" Of ", " of ").ToString();
//            t = t.Replace(" No ", " no ").ToString();
//            t = t.Replace(" Para ", " para ").ToString();
//            t = t.Replace(" O ", " o ").ToString();
//            t = t.Replace(" Os ", " os ").ToString();
//            t = t.Replace(" Ou ", " ou ").ToString();
//            t = t.Replace(" Que ", " que ").ToString();
//            t = t.Replace(" É ", " é ").ToString();
//            t = t.Replace(" Qual ", " qual ").ToString();
//            t = t.Replace(" Se ", " se ").ToString();
//            t = t.Replace(" Faz ", " faz ").ToString();
//            t = t.Replace(" Of ", " of ").ToString();
//            t = t.Replace(" The ", " the ").ToString();
//            t = t.Replace(" To ", " to ").ToString();
//            return t;
//        }
//        public static string ProperName(this string t)
//        {
//            bool force = true;
//            string k = "";
//            for (int i = 0; i < t.Length; i++)
//            {
//                force = false;
//                if (i == 0)
//                    force = true;
//                else
//                {
//                    if (i > 0)
//                    {
//                        if (t.Substring(i - 1, 1).ToString().CompareTo(" ") == 0)
//                            force = true;
//                    }
//                }
//                if (force)
//                    k += t.Substring(i, 1).ToUpper();
//                else
//                    k += t.Substring(i, 1);

//            }
//            return k;
//        }

//        public static string NoAccents(this string text)
//        {
//            text = text.Replace("á", "a");
//            text = text.Replace("á", "a");
//            text = text.Replace("ă", "a");
//            text = text.Replace("â", "a");
//            text = text.Replace("à", "a");
//            text = text.Replace("à", "a");
//            text = text.Replace("À", "A");
//            text = text.Replace("Á", "A");
//            text = text.Replace("Â", "A");
//            text = text.Replace("ã", "a");
//            text = text.Replace("Ã", "A");
            
//            text = text.Replace("í", "i");
//            text = text.Replace("Í", "I");
//            text = text.Replace("ĩ", "i");
//            text = text.Replace("ĭ", "i");
//            text = text.Replace("î", "i");
//            text = text.Replace("î", "i");
//            text = text.Replace("ĩ́", "i");


//            text = text.Replace("Ĩ", "I");
//            text = text.Replace("Î", "I");
//            text = text.Replace("ç", "c");
//            text = text.Replace("Ç", "C");

//            text = text.Replace("ɛ", "e");
//            text = text.Replace("ɛ̀", "e");
//            text = text.Replace("ɛ̀", "e");
//            text = text.Replace("ɛ́", "e");
//            text = text.Replace("ɛ̌", "e");

//            text = text.Replace("è", "e");
//            text = text.Replace("È", "E");
//            text = text.Replace("ê", "e");
//            text = text.Replace("Ê", "E");
//            text = text.Replace("é", "e");
//            text = text.Replace("é", "e");
//            text = text.Replace("É", "E");
//            text = text.Replace("ě", "e");
//            text = text.Replace("ẹ", "e");
//            text = text.Replace("Ẹ", "e");
//            text = text.Replace("ẹ̀", "e");
//            text = text.Replace("Ẹ̀", "e");
//            text = text.Replace("ẹ́", "e");
//            text = text.Replace("Ẹ́", "e");
//            text = text.Replace("ê", "e");
//            text = text.Replace("ế", "e");
//            text = text.Replace("ẽ", "e");
//            text = text.Replace("ḗ", "e");

//            text = text.Replace("ɔ", "o"); //596
//            text = text.Replace("ɔ̀", "o");
//            text = text.Replace("ɔ́", "o");

            
//            text = text.Replace("ŏ", "o");
//            text = text.Replace("o͂̀", "o");
//            text = text.Replace("o͂", "o");
//            text = text.Replace("Õ", "O");
//            text = text.Replace("ô", "o");
//            text = text.Replace("Ô", "O");
//            text = text.Replace("ó", "o");
//            text = text.Replace("Ó", "O");
//            text = text.Replace("ò", "o");
//            text = text.Replace("ò", "o");
//            text = text.Replace("Ò", "O");
//            text = text.Replace("ọ", "o");
//            text = text.Replace("Ọ", "o");
//            text = text.Replace("ọ̀", "o");
//            text = text.Replace("Ọ̀", "o");
//            text = text.Replace("ọ́", "o");
//            text = text.Replace("Ọ́", "o");

//            text = text.Replace("u͂̀", "u");
//            text = text.Replace("ŭ", "u");
//            text = text.Replace("ù", "u");
//            text = text.Replace("Ù", "U");
//            text = text.Replace("ú", "u");
//            text = text.Replace("ü", "u");
//            text = text.Replace("Ü", "U");
//            text = text.Replace("Ú", "U");
//            text = text.Replace("û", "u");
//            text = text.Replace("Û", "U");


//            text = text.Replace("è", "e");
//            text = text.Replace("ì", "i");

//            text = text.Replace("È", "E");
//            text = text.Replace("Ì", "I");

//            text = text.Replace("ń", "n");
//            text = text.Replace("Ń", "N");
//            text = text.Replace("ḿ", "m");
//            text = text.Replace("Ḿ", "M");

//            text = text.Replace("Ḿ", "m");
//            text = text.Replace("ḿ;", "m");
//            text = text.Replace("Ṁ", "m");
//            text = text.Replace("ṁ", "m");
//            text = text.Replace("ṣ", "s");
//            text = text.Replace("Ṣ", "S");
//            text = text.Replace("ɛ́", "e");
//            text = text.Replace("Ŋ", "N");
//            text = text.Replace("ŋ", "n");
//            text = text.Replace("ɖ", "d");
//            text = text.Replace("ɣ", "g");
//            text = text.Replace("ƒ", "f");
//            text = text.Replace("ʋ", "v");
//            text = text.Replace("ɖ", "d");
//            text = text.Replace("ŋ", "n"); //331
//            text = text.Replace("ŋ", "n");
//            return text.Trim();
//        }

//        public static string NoMaskChar(this string t)
//        {
//            t = t.Replace("-", "");
//            t = t.Replace("/", "");
//            t = t.Replace(",", "");
//            t = t.Replace(".", "");
//            t = t.Replace("\\", "");
//            t = t.Replace("+", "");
//            t = t.Replace("  ", " ");
//            t = t.Replace("   ", " ");
//            t = t.Replace("    ", " ");
//            return t;
//        }
//    }
//}
