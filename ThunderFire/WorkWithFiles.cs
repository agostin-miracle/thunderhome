using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ThunderFire
{
    /// <summary>
    /// Files 
    /// </summary>
    [Obsolete()]
    public class WorkWithFiles
    {
        /// <summary>
        /// Histórico de Execução
        /// </summary>
        public static StringBuilder Logging;

        /// <summary>
        /// Código do Erro
        /// </summary>
        public static string ErrorCode { get; internal set; }

        /// <summary>
        /// Objeto do Erro
        /// </summary>
        public static object ErrorObject { get; internal set; }
        /// <summary>
        /// Retorna a string do ultimo erro ocorrido
        /// </summary>
        public static string ErrorMessage{get; internal set;}
        /// <summary>
        /// Retorna a string do último erro ocorrido
        /// </summary>
        public static string ErrorMessageForUser {get; internal set;}

        /// <summary>
        /// Retorna true se ocorreu algum erro
        /// </summary>
        /// <returns>bool</returns>
        public static bool HasError()
        {
            if (String.IsNullOrEmpty(ErrorMessage) || string.IsNullOrWhiteSpace(ErrorMessage))
                return false;
            return true;
        }

        private static void SetError()
        {
            ErrorMessageForUser="";
            ErrorMessage = "";
            ErrorObject = null;
            ErrorCode = "";
            Logging = new StringBuilder();

        }


        /// <summary>
        /// Logon de Usuario
        /// </summary>
        /// <param name="lpszUsername">Usuario</param>
        /// <param name="lpszDomain">Dominio</param>
        /// <param name="lpszPassword">Senha</param>
        /// <param name="dwLogonType">LogonType</param>
        /// <param name="dwLogonProvider">LogonProvider</param>
        /// <param name="phToken"></param>
        /// <returns></returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, out IntPtr phToken);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="User">Usuário</param>
        /// <param name="Domain">Dominio</param>
        /// <param name="Password">Password</param>
        /// <param name="sourcefile">Arquivo de Origem</param>
        /// <param name="targetfile">Arquivo de Destino</param>
        /// <returns></returns>
        public bool CopyFile(string User, string Domain, string Password, string sourcefile, string targetfile)
        {
            return ActionOnFile("COPY", User, Domain, Password, sourcefile, targetfile);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="User">Usuário</param>
        /// <param name="Domain">Dominio</param>
        /// <param name="Password">Password</param>
        /// <param name="sourcefile">Arquivo de Origem</param>
        /// <param name="targetfile">Arquivo de Destino</param>
        /// <returns></returns>
        public bool MoveFile(string User, string Domain, string Password, string sourcefile, string targetfile)
        {
            return ActionOnFile("MOVE", User, Domain, Password, sourcefile, targetfile);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="User">Usuário</param>
        /// <param name="Domain">Dominio</param>
        /// <param name="Password">Password</param>
        /// <param name="sourcefile">Arquivo de Origem</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool CreateFile(string User, string Domain, string Password, string sourcefile, string data)
        {
            return ActionOnFile("CREATE", User, Domain, Password, sourcefile, data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="User">Usuário</param>
        /// <param name="Domain">Dominio</param>
        /// <param name="Password">Password</param>
        /// <param name="sourcefile">Arquivo de Origem</param>
        /// <returns></returns>
        public bool DeleteFile(string User, string Domain, string Password, string sourcefile)
        {
            return ActionOnFile("DELETE", User, Domain, Password, sourcefile, "");
        }


        private static bool ActionOnFile(string command, string User, string Domain, string Password, string sourcefile, string targetfile)
        {
            bool _retorno = false;
            IntPtr admin_token;
            WindowsIdentity wid_current = WindowsIdentity.GetCurrent();
            WindowsIdentity wid_admin = null;
            WindowsImpersonationContext wic = null;
            SetError();

            try
            {
                if (LogonUser(UseRijndael.Decrypt(User).ToString(), UseRijndael.Decrypt(Domain).ToString(), UseRijndael.Decrypt(Password).ToString(), 9, 0, out admin_token))
                {
                    wid_admin = new WindowsIdentity(admin_token);
                    wic = wid_admin.Impersonate();
                    try
                    {
                        if (command == "COPY")
                        {
                            System.IO.File.Copy(sourcefile, targetfile, true);
                            _retorno = true;
                        }

                        else if (command == "DELETE")
                        {
                            System.IO.File.Delete(sourcefile);
                            _retorno = true;
                        }

                        else if (command == "MOVE")
                        {
                            System.IO.File.Move(sourcefile, targetfile);
                            _retorno = true;
                        }
                        else if (command == "CREATE")
                        {
                            System.IO.File.WriteAllText(sourcefile, targetfile);
                            _retorno = true;
                        }
                        if (targetfile == "")
                            Logging.Append(String.Format("{0} File {1}", command, sourcefile));
                        else
                            Logging.Append(String.Format("{0} File {1} {2}", command, sourcefile, targetfile));
                        _retorno = true;
                    }
                    catch (PathTooLongException Error)
                    {
                        if (targetfile != "")
                            Logging.Append(String.Format("{0} File {1} {2} Erro: {3}", command, sourcefile, targetfile, "Caminho longo"));
                        else
                            Logging.Append(String.Format("{0} File {1} Erro: {2}", command, sourcefile, "Caminho Longo"));
                        ErrorObject = Error;
                        ErrorMessage= Error.Message;

                        if (wic != null)
                            wic.Undo();

                    }
                    catch (DirectoryNotFoundException Error)
                    {
                        if (targetfile != "")
                            Logging.Append(String.Format("{0} File {1} {2} Erro: {3}", command, sourcefile, targetfile, "Diretório não existe"));
                        else
                            Logging.Append(String.Format("{0} File {1} Erro: {2}", command, sourcefile, "Diretório não exist"));
                        ErrorObject = Error;
                        ErrorMessage= Error.Message;
                        if (wic != null)
                            wic.Undo();

                    }
                    catch (UnauthorizedAccessException Error)
                    {
                        if (targetfile != "")
                            Logging.Append(String.Format("{0} File {1} {2} Erro: {3}", command, sourcefile, targetfile, "Acesso não autorizado"));
                        else
                            Logging.Append(String.Format("{0} File {1} Erro: {2}", command, sourcefile, "Acesso não autorizado"));
                        ErrorObject = Error;
                        ErrorMessage = Error.Message;
                        if (wic != null)
                            wic.Undo();

                    }
                    catch (IOException Error)
                    {
                        if (targetfile != "")
                            Logging.Append(String.Format("{0} File {1} {2} Erro: {3}", command, sourcefile, targetfile, "Erro de I/O"));
                        else
                            Logging.Append(String.Format("{0} File {1} Erro: {2}", command, sourcefile, "Erro de I/O"));
                        ErrorObject = Error;
                        ErrorMessage = Error.Message;
                        if (wic != null)
                            wic.Undo();

                    }
                    catch (Exception Error)
                    {
                        if (targetfile != "")
                            Logging.Append(String.Format("{0} File {1} {2} Erro: {3}", command, sourcefile, targetfile, Error.Message));
                        else
                            Logging.Append(String.Format("{0} File {1} Erro: {2}", command, sourcefile, Error.Message));
                        ErrorObject = Error;
                        ErrorMessage= Error.Message;
                        if (wic != null)
                            wic.Undo();

                    }
                }
            }
            catch (Exception Error)
            {
                if (targetfile != "")
                    Logging.Append(String.Format("{0} File {1} {2} Erro: {3}", command, sourcefile, targetfile, Error.Message));
                else
                    Logging.Append(String.Format("{0} File {1} Erro: {2}", command, sourcefile, Error.Message));
                ErrorObject = Error;
                ErrorMessage = Error.Message;
                if (wic != null)
                    wic.Undo();
            }
            finally
            {
                if (wic != null)
                    wic.Undo();
            }

            return _retorno;
        }

        /// <summary>
        /// Thumb de Imagem
        /// </summary>
        /// <param name="FilePathName">Nome do Arquivo</param>
        /// <param name="ImgWidth">Largura de Aplicação</param>
        /// <param name="ImgHeight">Altura de Aplicação</param>
        /// <returns>Image</returns>
        public static System.Drawing.Image ThumbNailImage(string FilePathName, int ImgWidth, int ImgHeight)
        {
            SetError();
            System.Drawing.Image oImg;
            if (File.Exists(FilePathName))
            {
                oImg = System.Drawing.Image.FromFile(FilePathName);
                try
                {
                    System.Drawing.Image oThumbNail = new Bitmap(ImgWidth, ImgHeight, PixelFormat.Format32bppPArgb);
                    System.Drawing.Graphics oGraphic = System.Drawing.Graphics.FromImage(oThumbNail);
                    oGraphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    oGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    oGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    Rectangle oRectangle = new Rectangle(0, 0, ImgWidth, ImgHeight);
                    oGraphic.DrawImage(oImg, oRectangle);
                    return oThumbNail;
                }
                catch (Exception Error)
                {
                    ErrorMessageForUser = String.Format("{0}.\nFalha na conversão de imagem para thumbnail.", Error.Message);
                    ErrorMessage = Error.Message;
                    ErrorObject = Error;
                }
                finally
                {
                    oImg.Dispose();
                }
            }
            return null;
        }

        /// <summary>
        /// Thumb de Imagem
        /// </summary>
        /// <param name="FilePathName">Nome do Arquivo de Imagem alvo</param>
        /// <param name="ThumbFile">Nome do Arquivo de thumbmail a ser gerado</param>
        /// <param name="ImgWidth">Largura de Aplicação</param>
        /// <param name="ImgHeight">Altura de Aplicação</param>
        /// <returns>Image</returns>
        public static void ThumbNailImage(string FilePathName, string ThumbFile, int ImgWidth, int ImgHeight)
        {
            SetError();
            System.Drawing.Image oImg;
            if (File.Exists(FilePathName))
            {
                oImg = System.Drawing.Image.FromFile(FilePathName);
                try
                {
                    System.Drawing.Image oThumbNail = new Bitmap(ImgWidth, ImgHeight, PixelFormat.Format32bppPArgb);
                    System.Drawing.Graphics oGraphic = System.Drawing.Graphics.FromImage(oThumbNail);
                    oGraphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    oGraphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    oGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    Rectangle oRectangle = new Rectangle(0, 0, ImgWidth, ImgHeight);
                    oGraphic.DrawImage(oImg, oRectangle);
                    oThumbNail.Save(ThumbFile);
                    oThumbNail.Dispose();
                    oGraphic.Dispose();

                    //return oThumbNail;
                }
                catch (Exception Error)
                {
                    ErrorMessageForUser = String.Format("{0}.\nFalha na conversão de imagem para thumbnail.", Error.Message);
                    ErrorMessage = Error.Message;
                    ErrorObject = Error;
                }
                finally
                {
                    oImg.Dispose();
                }
            }
            //return null;
        }

        //[Obsolete()]
        //public static void CopyFileFromBase64(string source, string target)
        //{
        //    string t = System.IO.File.ReadAllText(source);
        //    System.IO.File.WriteAllBytes(target, Convert.FromBase64String(t));
        //}
        //[Obsolete()]
        //public static void CopyToFileBase64(string source, string target)
        //{
        //    byte[] t = System.IO.File.ReadAllBytes(source);
        //    System.IO.File.WriteAllText(target, Convert.ToBase64String(t));
        //}
    
        /// <summary>
        /// Lê um arquivo binário e retorna uma string base64
        /// </summary>
        /// <param name="psource">Nome do Arquivo de Origem</param>
        /// <returns>string</returns>
        public static string GetImage(string psource)
        {
            byte[] t = System.IO.File.ReadAllBytes(psource);
            return Convert.ToBase64String(t);
        }
        //[Obsolete()]
        //public static bool CreateSemaphore(string file)
        //{
        //    if (!File.Exists(file))
        //        File.WriteAllText(file, "");
        //    return File.Exists(file);
        //}
        //[Obsolete()]
        //public static string SetSemaphore(Environment.SpecialFolder basepath, string root, string prefix = "Semaphore_", string extension = ".log")
        //{
        //    return SetLogFile(basepath, root, prefix, extension);
        //}
        //[Obsolete()]
        //public static string SetLogFile(Environment.SpecialFolder basepath, string root, string prefix = "Log_", string extension = ".log")
        //{
        //    string path = Environment.GetFolderPath(basepath) + root;
        //    if (!System.IO.Directory.Exists(path))
        //        System.IO.Directory.CreateDirectory(path);
        //    string _serviceLogFile = WorkWithDirs.CombinePath(path, prefix + DateTime.Now.ToString("ddMMyyyy") + extension);
        //    return _serviceLogFile;
        //}
        //[Obsolete()]
        //public static string SetLogFile(string basepath, string prefix = "Log_", string extension = ".log")
        //{
        //    return WorkWithDirs.CombinePath(basepath, prefix + DateTime.Now.ToString("ddMMyyyy") + extension);
        //}


        /// <summary>
        /// Remove um arquivo
        /// </summary>
        /// <param name="pfilename">Nome do Arquivo</param>
        public static void RemoveFile(string pfilename)
        {
            SetError();
            try
            {
                if (System.IO.File.Exists(pfilename))
                    System.IO.File.Delete(pfilename);
            }
            catch (Exception Error)
            {
                ErrorMessageForUser = String.Format("Falha na remoção do arquivo {0}.", pfilename);
                ErrorMessage = Error.Message;
                ErrorObject = Error;
            }
        }

        /// <summary>
        /// Le um arquivo 
        /// </summary>
        /// <param name="pFILENAME">Nome do Arquivo</param>
        /// <returns>string</returns>
        public static string ReadFile(string pFILENAME)
        {
            try
            {
                if (File.Exists(pFILENAME))
                    return File.ReadAllText(pFILENAME);
            }
            catch (Exception Error)
            {
                ErrorMessageForUser = String.Format("Falha na leitura do arquivo {0}.", pFILENAME);
                ErrorMessage = Error.Message;
                ErrorObject = Error;
            }
            return "";
        }
        /// <summary>
        /// Salva para um arquivo xml
        /// </summary>
        /// <param name="ptext">Texto no formato XML</param>
        /// <param name="pFILENAME">Nome do Arquivo</param>
        public static void SaveToXml(string ptext, string pFILENAME)
        {
            SetError();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(ptext);
                if (File.Exists(pFILENAME))
                    File.Delete(pFILENAME);
                doc.Save(pFILENAME);
                doc = null;
            }
            catch (Exception Error)
            {
                ErrorMessageForUser = String.Format("Falha na gravação do arquivo {0}.", pFILENAME);
                ErrorMessage = Error.Message;
                ErrorObject = Error;
            }
        }

        //public static void SaveToXml(string ptext, string pFILENAME)
        //{
        //    SetError();
        //    try
        //    {
        //        if (ptext != "")
        //        {
        //            SaveToXml(ptext, pFILENAME);
        //        }
        //        else
        //        {
        //            ErrorMessageForUser = "Não existem dados a serem gravados";
        //            ErrorMessage = ErrorMessageForUser;
        //        }
        //    }
        //    catch (Exception Error)
        //    {
        //        ErrorMessageForUser = String.Format("Falha na gravação do arquivo {0}.", pFILENAME);
        //        ErrorMessage = Error.Message;
        //        ErrorObject = Error;
        //    }
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ptext"></param>
        /// <param name="pFILENAME"></param>
        public static void SaveToText(string ptext, string pFILENAME)
        {
            SetError();
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(pFILENAME))
                {
                    file.Write(ptext);
                    file.Flush();
                    file.Close();
                }
            }
            catch (Exception Error)
            {
                ErrorMessageForUser = String.Format("Falha na gravação do arquivo {0}.", pFILENAME);
                ErrorMessage = Error.Message;
                ErrorObject = Error;
            }
        }

        /// <summary>
        /// Move um arquivo para uma determinada extensão
        /// </summary>
        /// <param name="pFILENAME">Nome do Arquivo</param>
        /// <param name="extension">Extensão</param>
        public static void MoveTaskFile(string pFILENAME, string extension = "PROCESSADO")
        {
            IMoveTaskFile(pFILENAME, System.IO.Path.GetDirectoryName(pFILENAME), extension);
        }
        /// <summary>
        /// Move um arquivo para uma determinado diretorio e extensão
        /// </summary>
        /// <param name="pFILENAME">Nome do Arquivo</param>
        /// <param name="targetDir">Nome do Diretorio</param>
        /// <param name="extension">Extensão</param>
        public static void MoveTaskFile(string pFILENAME, string targetDir, string extension = "PROCESSADO")
        {
            IMoveTaskFile(pFILENAME, targetDir, extension);
        }

        private static void IMoveTaskFile(string pFILENAME, string targetDir, string extension = "PROCESSADO")
        {
            SetError();
            string oFile = "";
            try
            {
                if (extension.IndexOf(".") < 0)
                    extension = "." + extension;
                oFile = WorkWithDirs.CombinePath(targetDir, System.IO.Path.GetFileNameWithoutExtension(pFILENAME) + extension);
                File.Move(pFILENAME, oFile);
            }
            catch (Exception Error)
            {
                ErrorMessageForUser = String.Format("Falha na troca de nome do arquivo {0}  para {1}", pFILENAME, oFile);
                ErrorMessage = Error.Message;
                ErrorObject = Error;
            }
        }


    }
}
