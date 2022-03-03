using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire
{
    /// <summary>
    /// Operações com texto
    /// </summary>
    public class Text
    {
        /// <summary>
        /// Captura de Erros
        /// </summary>
        public static TrapError TrappedError = new TrapError();

        /// <summary>
        /// Retorna uma matriz de bytes que contém os resultados de codificação do conjunto de caracteres especificado.
        /// </summary>
        /// <param name="str">String</param>
        /// <returns>Matriz de Bytes</returns>
        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        /// <summary>
        /// Retorna uma cadeia de caracteres que contém os resultados da decodificação da sequência de bytes especificada.
        /// </summary>
        /// <param name="bytes">Matriz de Bytes</param>
        /// <returns>String</returns>
        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }


        /// <summary>
        /// Determines a text file's encoding by analyzing its byte order mark (BOM).
        /// Defaults to ASCII when detection of the text file's endianness fails.
        /// </summary>
        /// <param name="filename">The text file to analyze.</param>
        /// <returns>The detected encoding.</returns>
        public static Encoding GetEncoding(string filename)
        {
            if (File.Exists(filename))
            {
                // Read the BOM
                var bom = new byte[4];
                using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    file.Read(bom, 0, 4);
                }

                // Analyze the BOM
                if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
                if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
                if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
                if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
                if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
                return Encoding.ASCII;
            }
            return null;
        }


    }
}
