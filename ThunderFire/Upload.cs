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
    /// Upload de Arquivos via FTP
    /// </summary>
    public class Upload
    {
        /// <summary>
        /// Captura de Erros
        /// </summary>
        public static TrapError TrappedError = new TrapError();

        /// <summary>
        /// Executa um FTP de Arquivo
        /// </summary>
        /// <param name="ftpHost">Host</param>
        /// <param name="ftpPath">Caminho base + nome de arquivo</param>
        /// <param name="ftpUser">Usuario</param>
        /// <param name="ftpPassword">Senha</param>
        /// <param name="ftpTargetFile">Arquivo de Destino</param>
        /// <returns>Int</returns>
        /// <remarks>
        /// <para>1 - Sucesso</para>
        /// <para>-1 - Erro de FTP</para>
        /// </remarks>
        public static int FtpUploadFile(string ftpHost, string ftpPath, string ftpUser, string ftpPassword, string ftpTargetFile)
        {
            int retorno = 0;
            TrappedError.SetError();
            try
            {
                /*
                 * Uri.UriSchemeFtp
                 */
                string ftpfullpath = "ftp://" + ftpHost + ftpPath;
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;
                FileStream fs = File.OpenRead(ftpTargetFile);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();
                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
                retorno = 1;
            }
            catch (WebException Error)
            {

                TrappedError.ErrorMessage = Error.Message;
                TrappedError.ErrorObject = Error;
                retorno = -1;
            }
            return retorno;
        }
    }
}