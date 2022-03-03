using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire
{
    /// <summary>
    /// Download de Arquivos
    /// </summary>
    public class DownLoad
    {

        /// <summary>
        /// Captura de Erro
        /// </summary>
        public static TrapError TrappedError = new TrapError();

        private static byte[] downloadedData;


        /// <summary>
        /// Retorna o Status do Ultimo Download
        /// </summary>
        public static bool Status { get; internal set; } = false;

        /// <summary>
        /// Efetua o Download de um arquivo
        /// </summary>
        /// <param name="url">URI do arquivo</param>
        /// <param name="file">nome do arquivo a ser salvo</param>
        public static void Get(string url, string file)
        {
            TrappedError.SetError();
            downloadedData = new byte[0];
            Status = false;
            try
            {
                WebRequest req = WebRequest.Create(url);
                WebResponse response = req.GetResponse();
                Stream stream = response.GetResponseStream();
                byte[] buffer = new byte[1024];
                int dataLength = (int)response.ContentLength;
                MemoryStream memStream = new MemoryStream();
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    else
                    {
                        memStream.Write(buffer, 0, bytesRead);
                    }
                }
                downloadedData = memStream.ToArray();
                stream.Close();
                memStream.Close();

                try
                {
                    FileStream newFile = new FileStream(file, FileMode.Create);
                    newFile.Write(downloadedData, 0, downloadedData.Length);
                    newFile.Close();
                    Status = true;
                }
                catch (Exception Error)
                {
                    string ErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                    if (ErrorCode != "")
                        TrappedError.SetError(TrappedError.ErrorCode);
                    else
                        TrappedError.UserError = "FALHA NO DOWNLOAD DO ARQUIVO";
                    TrappedError.ErrorMessage = Error.Message;
                    TrappedError.ErrorObject = Error;
                }
                stream = null;
                memStream = null;

            }
            catch (Exception Error)
            {
                string ErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                if (ErrorCode != "")
                    TrappedError.SetError(TrappedError.ErrorCode);
                else
                    TrappedError.UserError = "FALHA NO DOWNLOAD DO ARQUIVO";
                TrappedError.ErrorMessage = Error.Message;
                TrappedError.ErrorObject = Error;
            }
        }
    }
}