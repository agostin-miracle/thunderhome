using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire
{
    //namespace Encryption
    //{
        /// <summary>
        /// Implementação para Rijndael
        /// </summary>
        public class UseRijndael
        {
            /// <summary>     
            /// Vetor de bytes utilizados para a criptografia (Chave Externa)     
            /// </summary>     
            private static byte[] bIV =
            { 0x50, 0x08, 0xF1, 0xDD, 0xDE, 0x3C, 0xF2, 0x18,
        0x44, 0x74, 0x19, 0x2C, 0x53, 0x49, 0xAB, 0xBC };

            // <summary>     
            // Representação de valor em base 64 (Chave Interna)    
            // O Valor representa a transformação para base64 de     
            // um conjunto de 32 caracteres (8 * 32 = 256bits)    
            // A chave é: "Criptografias com Rijndael / AES"  = Q3JpcHRvZ3JhZmlhcyBjb20gUmluamRhZWwgLyBBRVM=
            // </summary>     

            /// <summary>
            /// Chave de Criptografia/ Descriptografia
            /// </summary>
            public static string Key { get; set; } = "Q3JpcHRvZ3JhZmlhcyBjb20gUmluamRhZWwgLyBBRVM=";

            /// <summary>     
            /// Encripta um texto     
            /// </summary>     
            /// <param name="text">Texto a ser criptografado</param>     
            /// <returns>string</returns>
            public static string Encrypt(string text)
            {
                try
                {
                    // Se a string NAO ESTA vazia, executa a criptografia
                    if (!string.IsNullOrEmpty(text))
                    {
                        // Cria instancias de vetores de bytes com as chaves                
                        byte[] bKey = Convert.FromBase64String(Key);
                        byte[] bText = new UTF8Encoding().GetBytes(text);

                        // Instancia a classe de criptografia Rijndael
                        Rijndael rijndael = new RijndaelManaged();

                        // Define o tamanho da chave "256 = 8 * 32"                
                        // Lembre-se: chaves possíves:                
                        // 128 (16 caracteres), 192 (24 caracteres) e 256 (32 caracteres)                
                        rijndael.KeySize = 256;

                        // Cria o espaço de memória para guardar o valor criptografado:                
                        MemoryStream mStream = new MemoryStream();
                        // Instancia o encriptador                 
                        CryptoStream encryptor = new CryptoStream(
                            mStream,
                            rijndael.CreateEncryptor(bKey, bIV),
                            CryptoStreamMode.Write);

                        // Faz a escrita dos dados criptografados no espaço de memória
                        encryptor.Write(bText, 0, bText.Length);
                        // Despeja toda a memória.                
                        encryptor.FlushFinalBlock();
                        // Pega o vetor de bytes da memória e gera a string criptografada                
                        return Convert.ToBase64String(mStream.ToArray());
                    }
                    else
                    {
                        // Se a string for vazia retorna nulo                
                        return null;
                    }
                }
                catch (Exception Error)
                {
                    throw new ApplicationException("Erro ao descriptografar", Error);
                }
            }

            /// <summary>     
            /// Decripta um valor 
            /// </summary>     
            /// <param name="text">Texto a ser descriptografado</param>     
            /// <returns>String</returns>     
            public static string Decrypt(string text)
            {
                try
                {
                    // Se a string NAO ESTA vazia, executa a criptografia           
                    if (!string.IsNullOrEmpty(text))
                    {
                        // Cria instancias de vetores de bytes com as chaves                
                        byte[] bKey = Convert.FromBase64String(Key);
                        byte[] bText = Convert.FromBase64String(text);

                        // Instancia a classe de criptografia Rijndael                
                        Rijndael rijndael = new RijndaelManaged();

                        // Define o tamanho da chave "256 = 8 * 32"                
                        // Lembre-se: chaves possíves:                
                        // 128 (16 caracteres), 192 (24 caracteres) e 256 (32 caracteres)                
                        rijndael.KeySize = 256;

                        // Cria o espaço de memória para guardar o valor DEScriptografado:               
                        MemoryStream mStream = new MemoryStream();

                        // Instancia o Decriptador                 
                        CryptoStream decryptor = new CryptoStream(
                            mStream,
                            rijndael.CreateDecryptor(bKey, bIV),
                            CryptoStreamMode.Write);

                        // Faz a escrita dos dados criptografados no espaço de memória   
                        decryptor.Write(bText, 0, bText.Length);
                        // Despeja toda a memória.                
                        decryptor.FlushFinalBlock();
                        // Instancia a classe de codificação para que a string venha de forma correta         
                        UTF8Encoding utf8 = new UTF8Encoding();
                        // Com o vetor de bytes da memória, gera a string descriptografada em UTF8       
                        return utf8.GetString(mStream.ToArray());
                    }
                    else
                    {
                        // Se a string for vazia retorna nulo                
                        return null;
                    }
                }
                catch (Exception Error)
                {
                    throw new ApplicationException("Erro ao descriptografar", Error);
                }
            }
        }

        /// <summary>
        /// Implementação para Triple Data Encryption Standard
        /// </summary>
        public class UseTripleDes
        {

            private readonly TripleDESCryptoServiceProvider _des = new TripleDESCryptoServiceProvider();
            private readonly UTF8Encoding _encoding = new UTF8Encoding();
            private byte[] _ivValue;
            private byte[] _keyValue;
            /// <summary>
            /// Obtêm/Define o parametro de uso de Hash
            /// </summary>
            public bool UseHash { get; set; } = true;

            /// <summary>
            /// Chave de Criptografia/ Descriptografia
            /// </summary>
            public string Key { get; set; } = "";


            /// <summary>
            /// Chave usada durante a encriptação/descriptografia
            /// </summary>
            public byte[] KeyUsed
            {
                get { return _keyValue; }
                private set { _keyValue = value; }
            }

            /// <summary>
            /// Vetor usado durante a encriptação/descriptografia
            /// </summary>

            public byte[] IVUsed
            {
                get { return _ivValue; }
                private set { _ivValue = value; }
            }


            /// <summary>
            /// Gera um vetor de inicialização aleatório (IV) a ser usado para o algoritmo.
            /// </summary>
            /// <returns></returns>
            public string CreateVector()
            {
                using (var des = new System.Security.Cryptography.TripleDESCryptoServiceProvider())
                {
                    des.GenerateIV();
                    return Convert.ToBase64String(des.IV);
                }
            }

            /// <summary>
            /// Método para gerar uma chave aleatória quando nenhuma for especificada. Esse método nunca retornará uma chave fraca 
            /// </summary>
            /// <returns></returns>
            public string CreateKey()
            {
                using (var des = new System.Security.Cryptography.TripleDESCryptoServiceProvider())
                {
                    des.GenerateKey();
                    return Convert.ToBase64String(des.Key);
                }
            }
            /// <summary>
            /// Criptografa um texto
            /// </summary>
            /// <param name="value">texto a ser criptografado</param>
            /// <returns>byte []</returns>
            public string Encrypt(string value)
            {
                if (Key != "")
                {
                    var des = Create(Key);
                    var ct = des.CreateEncryptor();
                    var input = Encoding.UTF8.GetBytes(value);
                    var output = ct.TransformFinalBlock(input, 0, input.Length);
                    //return output;
                    //return Encoding.UTF8.GetString(output);
                    return Convert.ToBase64String(output);
                }
                return null;
            }

            /// <summary>
            /// Decriptografa um texto
            /// </summary>
            /// <param name="value">texto a ser descriptografado</param>
            /// <returns>string</returns>
            public string Decrypt(string value)
            {
                if (Key != "")
                {
                    var des = Create(Key);
                    var ct = des.CreateDecryptor();
                    var input = Convert.FromBase64String(value);
                    var output = ct.TransformFinalBlock(input, 0, input.Length);
                    return _encoding.GetString(output);
                }
                return "";
            }

            private TripleDESCryptoServiceProvider Create(string key)
            {
                byte[] keyArray;
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                if (UseHash)
                {
                    MD5 md5 = new MD5CryptoServiceProvider();
                    keyArray = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
                }
                else
                {
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);
                }
                des.Key = keyArray;
                des.IV = new byte[des.BlockSize / 8];
                des.Padding = PaddingMode.PKCS7;
                des.Mode = CipherMode.ECB;
                return des;
            }

            /// <summary>
            /// Cria um KCV a partir de uma Chave em Texto Plano
            /// </summary>
            /// <param name="key">Chave</param>
            /// <returns>string</returns>
            public string GetKeyCheckValue(string key)
            {
                var iv = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                var data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

                TripleDES des = new TripleDESCryptoServiceProvider();

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(StringToByteArray(key), iv), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(data, 0, data.Length);
                        cryptoStream.FlushFinalBlock();
                        return ByteArrayToString(memoryStream.ToArray()).Remove(6);
                    }
                }
            }

            /// <summary>
            /// Converte um array de bytes para uma string
            /// </summary>
            /// <param name="value">Array de Bytes</param>
            /// <returns>string</returns>
            public string ByteArrayToString(byte[] value)
            {
                var hex = new StringBuilder(value.Length * 2);
                foreach (var b in value)
                    hex.AppendFormat("{0:x2}", b);
                return hex.ToString().ToUpper();
            }
            /// <summary>
            /// Converte uma string para um array de bytes
            /// </summary>
            /// <param name="value">String</param>
            /// <returns>Byte[]</returns>
            public byte[] StringToByteArray(String value)
            {
                var numberChars = value.Length;
                var bytes = new byte[numberChars / 2];
                for (var i = 0; i < numberChars; i += 2)
                    bytes[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);
                return bytes;
            }
        }
    //}

    /// <summary>
    /// Tratamento de String Base64
    /// </summary>
    public static class Base64
    {
        /// <summary>
        /// Codifica uma string para Base64 conforme o Encoding
        /// </summary>
        /// <param name="encoding">Encoding</param>
        /// <param name="text">string</param>
        /// <returns>string</returns>
        public static string Encode(this System.Text.Encoding encoding, string text)
        {
            if (text != null)
            {
                byte[] textAsBytes = encoding.GetBytes(text);
                return System.Convert.ToBase64String(textAsBytes);
            }
            return null;

        }
        /// <summary>
        /// Decodifica uma string Base64 para string conforme o Encoding
        /// </summary>
        /// <param name="encoding">Encoding</param>
        /// <param name="text">string</param>
        /// <returns>string</returns>
        public static string Decode(this System.Text.Encoding encoding, string text)
        {
            if (text != null)
            {
                byte[] textAsBytes = System.Convert.FromBase64String(text);
                return encoding.GetString(textAsBytes);
            }
            return null;
        }
    }
}