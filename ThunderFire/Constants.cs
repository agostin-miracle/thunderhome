using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire
{
    /// <summary>
    /// Constantes
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Estilo
        /// </summary>
        public static string StyleForDoc = @"<head><style>
    body {font-family:calibri;}
    h1 {margin-left:25px;}
    h2 {margin-left:30px;color:blue;border-bottom:1px solid #000000;}
    h3 {margin-left:40px;color:blue;}
    h4 {margin-left:45px;margin-top:0px;margin-bottom:0px;color:blue;}
    h5 {margin-left:50px;margin-top:0px;margin-bottom:0px;color:brown;}
    p {margin-left:55px;}
ul {display: block;
  font-family:calibri;
  list-style-type: none;
  margin-top: 1em;
  margin-bottom: 1 em;
  margin-left: 0;
  margin-right: 0;
  padding-left: 50px;}
summary {font-family:calibri;}
table {
margin-left: 50px;
     border-width: 1px;
     border-spacing: ;
     border-style: dotted;
     border-color: blue;
     border-collapse: separate;
     background-color: white;
    }
table th {
     border-width: 1px;
     padding: 1px;
     border-style: none;
     border-color: gray;
     background-color: white;
     -moz-border-radius: ;
    }
table td {
     border-width: 1px;
     padding: 1px;
     border-style: none;
     border-color: gray;
     background-color: white;
     -moz-border-radius:}
    </style></head>";



        /// <summary>
        /// Mensagem de sucesso
        /// </summary>
        public const string SUCCESS = "Registro atualizado com sucesso";
        /// <summary>
        /// Mensagem de sucesso de logon
        /// </summary>
        public const string LOGON_SUCCESS = "Usuário logado com sucesso";
        /// <summary>
        /// Arquivo de Comentário não fornecido
        /// </summary>
        public const string NO_COMMENTS_FILE = "Arquivo alvo de comentarios nao fornecido";
        /// <summary>
        /// Arquivo de comentário não existente
        /// </summary>
        public const string NO_COMMENTS_FILE_EXIST = "Arquivo alvo de comentarios nao existente";

        /// <summary>
        /// Password não fornecida
        /// </summary>
        public const string PASSWORD_NOTGIVEN = "Senha de Acesso não fornecida";
        /// <summary>
        /// Password inválida
        /// </summary>
        public const string INVALID_PASSWORD = "Senha de Acesso inválida";

        /// <summary>
        /// Email Existente
        /// </summary>
        public const string EMAIL_HASFOUND = "E-mail já existente";
        /// <summary>
        /// Email não existente
        /// </summary>
        public const string EMAIL_HASNOTFOUND = "E-mail não existente";
        /// <summary>
        /// Email fornecido com dados inválidos
        /// </summary>
        public const string EMAIL_ENTRYERROR = "E-mail inválido";
        /// <summary>
        /// Email não fornecido
        /// </summary>
        public const string EMAIL_NOTGIVEN = "O E-mail deverá ser fornecido";
        /// <summary>
        /// Exclusão de email
        /// </summary>
        public const string EMAIL_DELETED = "O E-mail foi removido com sucesso";
        /// <summary>
        /// Envio de email com sucesso
        /// </summary>
        public const string EMAIL_SEND_SUCESS = "E-mail enviado com sucesso !!!";

        /// <summary>
        /// New Line
        /// </summary>
        public static readonly char NEWLINE = '\n';
        /// <summary>
        /// Spaces
        /// </summary>
        /// <param name="n">Comprimento</param>
        /// <returns>string</returns>
        public static string SPACE(int n)
        {
            return "".PadLeft(n, ' ').ToString();
        }
        /// <summary>
        /// TAB
        /// </summary>
        /// <param name="n">Número de Repetições</param>
        /// <returns>string</returns>
        public static string TAB(int n)
        {
            return "".PadLeft(n, ' ').ToString();
        }
        /// <summary>
        /// TAB'S
        /// </summary>
        /// <param name="n">Número de Repetições</param>
        /// <returns>string</returns>

        public static string TABS(int n)
        {
            return "".PadLeft(n, '\t').ToString(); ;
        }
        /// <summary>
        /// Grava um arquivo Texto
        /// </summary>
        /// <param name="result">Conteúdo</param>
        /// <param name="path">Caminho</param>
        public static void WriteTextFile(string result, string path)
        {
            if (result != "")
            {
                File.WriteAllText(path, result);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="path"></param>
        /// <param name="filename"></param>
        public static void WriteTextFile(string result, string path, string filename)
        {
            if (result != "")
            {
                var file = path + "\\" + filename;
                File.WriteAllText(file, result);
            }
        }

        /// <summary>
        /// Embaralha uma strinf
        /// </summary>
        /// <param name="text">Texto a ser embaralhado</param>
        /// <returns>string</returns>
        public static string Shuffle(string text)
        {
            string retorno = "";
            for (int i = 0; i < text.Length; i++)
            {
                char c = Convert.ToChar(text.Substring(i, 1));

                if ((c >= 48 && c <= 57) || (c >= 65 && c <= 90) || (c >= 97 && c <= 122))
                {
                    char p = (char)(c + 48);
                    retorno += Convert.ToString(p);
                }

            }
            return retorno;
        }

        /// <summary>
        /// Desembaralha uma string
        /// </summary>
        /// <param name="text">Texto a ser desembaralhado</param>
        /// <returns>string</returns>
        public static string UnShuffle(string text)
        {
            string retorno = "";
            for (int i = 0; i < text.Length; i++)
            {
                char c = Convert.ToChar(text.Substring(i, 1));
                char p = (char)(c - 48);
                retorno += Convert.ToString(p);
            }
            return retorno;
        }

        /// <summary>
        /// Cria uma string de vinculo com base no tipo de arquivo
        /// </summary>
        /// <param name="file">Nome do Arquivo</param>
        /// <returns>string</returns>
        public static string EmbedFileBase64(string file)
        {
            string imgstr = "";
            try
            {
                if (File.Exists(file))
                {
                    using (Image img = Image.FromFile(file))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            img.Save(ms, img.RawFormat);
                            byte[] imgBytes = ms.ToArray();
                            imgstr = Convert.ToBase64String(imgBytes);
                        }
                    }
                }
                if (imgstr != "")
                    return "data:image/" + Path.GetExtension(file).TrimStart('.') + ";base64," + imgstr;
            }
            catch { }
            return "";
        }



        /// <summary>
        /// Diferença em dias de duas datas
        /// </summary>
        /// <param name="data1">Data Final</param>
        /// <param name="data2">Data Inicial</param>
        /// <returns>Número de Dias</returns>
        public static int DateDiff(DateTime data1, DateTime data2)
        {
            return (data1 - data2).Days;
        }

        /// <summary>
        /// Diferença em dias de duas datas
        /// </summary>
        /// <remarks>Desconsidera os valores representativos de Hora na diferença</remarks>
        /// <param name="data1">Data Final</param>
        /// <param name="data2">Data Inicial</param>
        /// <param name="method">0</param>
        /// <returns>Número de Dias</returns>
        public static int DateDiff(DateTime data1, DateTime data2, byte method)
        {
            DateTime d1 = new DateTime(data1.Year, data1.Month, data1.Day, 0, 0, 0);
            DateTime d2 = new DateTime(data2.Year, data2.Month, data2.Day, 0, 0, 0);
            return (d1 - d2).Days;
        }

        /// <summary>
        /// Remove o caracter "'" da mensagem (JQuery)
        /// </summary>
        /// <param name="message">mensagem</param>
        /// <returns>string</returns>
        public static string MessageResolve(string message)
        {
            return message.Replace("'", "\"");
        }

        /// <summary>
        /// Substitui os caracteres '&lt;&gt;' de uma string para os correspondente '[]' 
        /// </summary>
        /// <param name="_text">Texto a ser analisado</param>
        /// <returns>string</returns>
        public static string StripBracket(string _text)
        {
            _text = _text.Replace("<", "[");
            _text = _text.Replace(">", "]");
            _text = _text.Replace("xmlns:util=\"urn:util\"", "");
            _text = _text.Replace("http://www.w3.org/1999/xhtml", "");
            return _text;
        }
        /// <summary>
        /// Substitui os caracteres '[]' de uma string para os correspondente '&lt;&gt;' e substitui o literal _A_ por \"
        /// </summary>
        /// <param name="_text">Texto a ser analisado</param>
        /// <returns>string</returns>
        public static string NonStripBracket(string _text)
        {
            _text = _text.Replace("[", "<");
            _text = _text.Replace("]", ">");
            _text = _text.Replace("_A_", "\"");
            return _text;
        }


        /// <summary>
        /// Converte uma string para um array de bytes
        /// </summary>
        /// <param name="value">String</param>
        /// <returns>Byte[]</returns>
        public static byte[] StringToByteArray(String value)
        {
            var numberChars = value.Length;
            var bytes = new byte[numberChars / 2];
            for (var i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);
            return bytes;
        }

        /// <summary>
        /// Converte uma string para formato Hexa
        /// </summary>
        /// <param name="value">Valor a ser convertido</param>
        /// <returns>string</returns>
        public static string ToHexString(string value)
        {
            string hex = "";
            foreach (char c in value)
            {
                int tmp = c;
                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }
        /// <summary>
        /// Converte uma string em formato Hexa para String
        /// </summary>
        /// <param name="hexString">String no formato Hexa</param>
        /// <returns>string</returns>
        public static string FromHexString(string hexString)
        {
            var bytes = new byte[hexString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            return Encoding.UTF8.GetString(bytes); // returns: "Hello world" for "48656C6C6F20776F726C64"
        }


        /// <summary>
        /// Converte um array de bytes para uma string
        /// </summary>
        /// <param name="value">Array de Bytes</param>
        /// <returns>string</returns>
        public static string ByteArrayToString(byte[] value)
        {
            var hex = new StringBuilder(value.Length * 2);
            foreach (var b in value)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString().ToUpper();
        }

    }
}