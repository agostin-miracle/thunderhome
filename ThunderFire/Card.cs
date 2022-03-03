using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire
{
    /// <summary>
    /// Funções de Aplicação em Cartões
    /// </summary>
    public class Card
    {

        /// <summary>
        /// Obtêm o PINBLOCK
        /// </summary>
        /// <param name="pPSWCRT">Senha do Cartão</param>
        /// <returns>PINBLOCK do Cartão</returns>
        public static string PINBLOCK(string pPSWCRT)
        {
            return ("1" + pPSWCRT.Length.ToString() + pPSWCRT + "".PadLeft(16, 'F')).Left(16);
        }

        /// <summary>
        /// Obtêm o PINBLOCK
        /// </summary>
        /// <param name="pNUMCRT">Número do Cartão</param>
        /// <param name="pPSWCRT">Senha do Cartão</param>
        /// <returns>PINBLOCK do Cartão</returns>
        public static string PINBLOCK(string pNUMCRT, string pPSWCRT)
        {
            string PSW = (pPSWCRT.Length.ToString("D2") + pPSWCRT + "".PadLeft(16, 'F')).Left(16);
            Hex p1 = new Hex(PAN(pNUMCRT));
            Hex p2 = new Hex(PSW);
            return (p1 ^ p2).ToString();
        }
        /// <summary>
        /// Obtêm o PAN de um cartão
        /// </summary>
        /// <param name="pNUMCRT">Número do Cartão</param>
        /// <returns>PAN do Cartão</returns>
        public static string PAN(string pNUMCRT)
        {
            if (pNUMCRT.Length == 16)
            {
                return "0000" + pNUMCRT.Substring(3, 12);
            }
            if (pNUMCRT.Length == 19)
            {
                return "0000" + pNUMCRT.Substring(6, 12);
            }
            return "".PadLeft(16, '0');

        }

        ///// <summary>
        ///// Obtêm o PINBLOCK
        ///// </summary>
        ///// <param name="pNUMCRT">Número do Cartão</param>
        ///// <param name="pPSWCRT">Senha do Cartão</param>
        ///// <returns>PINBLOCK do Cartão</returns>
        //public static string PINBLOCK(string pNUMCRT, string pPSWCRT)
        //{
        //    if (pNUMCRT.Length == 16)
        //    {
        //        string PSW = (pPSWCRT.Length.ToString("D2") + pPSWCRT + "".PadLeft(16, 'F')).Left(16);
        //        Hex p1 = new Hex(PAN(pNUMCRT));
        //        Hex p2 = new Hex(PSW);
        //        return (p1 ^ p2).ToString();
        //    }
        //    return "".PadLeft(16, '0');
        //}
        ///// <summary>
        ///// Obtêm o PAN de um cartão
        ///// </summary>
        ///// <param name="pNUMCRT">Número do Cartão</param>
        ///// <returns>PAN do Cartão</returns>
        //public static string PAN(string pNUMCRT)
        //{
        //    //if (pNUMCRT.Length == 16)
        //    //{
        //    //    return "0000" + pNUMCRT.Substring(3, 12);
        //    //}
        //    //return "".PadLeft(16, '0');

        //    if (pNUMCRT.Length == 16)
        //    {
        //        return "0000" + pNUMCRT.Substring(3, 12);
        //    }
        //    if (pNUMCRT.Length == 19)
        //    {
        //        return "0000" + pNUMCRT.Substring(6, 12);
        //    }
        //    return "".PadLeft(16, '0');

        //}
    }

    /// <summary>
    /// Funções de Encriptação de Decriptação de Cartões
    /// </summary>
    public class CardEncription
    {
        /// <summary>
        /// Captura de Erros
        /// </summary>
        public static TrapError TrappedError = new TrapError();
        /// <summary>
        /// Chave de Acesso
        /// </summary>
        public static string Key { get; set; }

        /// <summary>
        /// Encripta um valor
        /// </summary>
        /// <param name="value">valor a ser encriptado</param>
        /// <returns>string</returns>
        public static string Encrypt(string value)
        {
            if (!String.IsNullOrWhiteSpace(Key))
            {
                byte[] value1 = ThunderFire.Constants.StringToByteArray(value);
                byte[] key1 = ThunderFire.Constants.StringToByteArray(Key);

                byte[] result = Encrypt(value1, key1);
                if (result != null)
                    return BitConverter.ToString(result, 0, 8).Replace("-", "");
            }
            else
                TrappedError.SetError("ENCRYPTKEYEMPTY");
            return "";
        }

        /// <summary>
        /// Decripta valor
        /// </summary>
        /// <param name="value">valor a ser decriptado</param>
        /// <returns>string</returns>
        public static string Decrypt(string value)
        {
            if (!String.IsNullOrWhiteSpace(Key))
            {
                byte[] value1 = ThunderFire.Constants.StringToByteArray(value);
                byte[] key1 = ThunderFire.Constants.StringToByteArray(Key);

                byte[] result = Decrypt(value1, key1);
                if (result != null)
                    return BitConverter.ToString(result, 0, 8).Replace("-", "");
            }
            else
                TrappedError.SetError("ENCRYPTKEYEMPTY");


            return "";
        }


        private static byte[] Encrypt(byte[] value, byte[] key)
        {
            try
            {
                SymmetricAlgorithm algorithm = new TripleDESCryptoServiceProvider();
                algorithm.Key = key;
                algorithm.Mode = CipherMode.ECB;
                algorithm.Padding = PaddingMode.Zeros;
                algorithm.IV = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                ICryptoTransform ict = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV);
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, ict, CryptoStreamMode.Write);
                cStream.Write(value, 0, value.Length);
                cStream.FlushFinalBlock();
                cStream.Close();
                return mStream.ToArray();
            }
            catch (Exception Error)
            {
                string ErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                if (ErrorCode != "")
                    TrappedError.SetError(ErrorCode);
                TrappedError.ErrorMessage = Error.Message;
                TrappedError.ErrorObject = Error;
            }
            return null;
        }

        private static byte[] Decrypt(byte[] value, byte[] key)
        {
            try
            {
                SymmetricAlgorithm algorithm = new TripleDESCryptoServiceProvider();
                algorithm.Key = key;
                algorithm.Mode = CipherMode.ECB;
                algorithm.Padding = PaddingMode.Zeros;
                algorithm.IV = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                ICryptoTransform ict = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV);
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, ict, CryptoStreamMode.Write);
                cStream.Write(value, 0, value.Length);
                cStream.FlushFinalBlock();
                cStream.Close();
                return mStream.ToArray();
            }
            catch (Exception Error)
            {
                string ErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                if (ErrorCode != "")
                    TrappedError.SetError(ErrorCode);
                TrappedError.ErrorMessage = Error.Message;
                TrappedError.ErrorObject = Error;
            }
            return null;
        }

    }
}
