using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Runtime.InteropServices;
using System.Security.Principal;


namespace ThunderFire
{
    /// <summary>
    /// Operações com Arquivos
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(true)]
    public class Files
    {
        /// <summary>
        /// Histórico de Execução
        /// </summary>
        public static StringBuilder Logging;
        /// <summary>
        /// Captura de Erros
        /// </summary>
        public static TrapError TrappedError = new TrapError();
        /// <summary>
        /// Le/Define a Versão do XML
        /// </summary>
        public static string XmlVersion { get; set; } = "1.0";
        /// <summary>
        /// Le/Define o encoding do XML a ser gerado
        /// </summary>
        public static string Encoding { get; set; } = "utf-8";
        /// <summary>
        /// Le/define o Root do arquivo 
        /// </summary>
        public static string RootName { get; set; } = "root";
        /// <summary>
        /// Le/Define o nome do registro
        /// </summary>
        public static string RecordName { get; set; } = "item";

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
        /// Copia um arquivo baseado em um logon Administrativo
        /// </summary>
        /// <param name="User">Usuário</param>
        /// <param name="Domain">Domínio</param>
        /// <param name="Password">Senha</param>
        /// <param name="sourcefile">Arquivo de Origem</param>
        /// <param name="targetfile">Arquivo de Destino</param>
        /// <returns>true, se copiado o arquivo</returns>
        public bool CopyFile(string User, string Domain, string Password, string sourcefile, string targetfile)
        {
            return ActionOnFile("COPY", User, Domain, Password, sourcefile, targetfile);
        }
        /// <summary>
        /// Move um arquivo baseado em um logon Administrativo
        /// </summary>
        /// <param name="User">Usuário</param>
        /// <param name="Domain">Domínio</param>
        /// <param name="Password">Senha</param>
        /// <param name="sourcefile">Arquivo de Origem</param>
        /// <param name="targetfile">Arquivo de Destino</param>
        /// <returns>true, se movido o arquivo</returns>
        public bool MoveFile(string User, string Domain, string Password, string sourcefile, string targetfile)
        {
            return ActionOnFile("MOVE", User, Domain, Password, sourcefile, targetfile);
        }
        /// <summary>
        /// Cria um arquivo baseado em um logon Administrativo
        /// </summary>
        /// <param name="User">Usuário</param>
        /// <param name="Domain">Domínio</param>
        /// <param name="Password">Senha</param>
        /// <param name="sourcefile">Arquivo de Origem</param>
        /// <param name="data">string de dados a serem copiados</param>
        /// <returns>true, se o arquivo foi criado</returns>
        public bool CreateFile(string User, string Domain, string Password, string sourcefile, string data)
        {
            return ActionOnFile("CREATE", User, Domain, Password, sourcefile, data);
        }
        /// <summary>
        /// Deletea um arquivo baseado em um logon Administrativo
        /// </summary>
        /// <param name="User">Usuário</param>
        /// <param name="Domain">Domínio</param>
        /// <param name="Password">Senha</param>
        /// <param name="sourcefile">Arquivo de Origem</param>
        /// <returns>true, se o arquivo foi excluido</returns>
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
            TrappedError = new TrapError();

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

                        TrappedError.SetError(Error.Message, Error);

                        if (wic != null)
                            wic.Undo();

                    }
                    catch (DirectoryNotFoundException Error)
                    {
                        if (targetfile != "")
                            Logging.Append(String.Format("{0} File {1} {2} Erro: {3}", command, sourcefile, targetfile, "Diretório não existe"));
                        else
                            Logging.Append(String.Format("{0} File {1} Erro: {2}", command, sourcefile, "Diretório não exist"));

                        TrappedError.SetError(Error.Message, Error);
                        if (wic != null)
                            wic.Undo();

                    }
                    catch (UnauthorizedAccessException Error)
                    {
                        if (targetfile != "")
                            Logging.Append(String.Format("{0} File {1} {2} Erro: {3}", command, sourcefile, targetfile, "Acesso não autorizado"));
                        else
                            Logging.Append(String.Format("{0} File {1} Erro: {2}", command, sourcefile, "Acesso não autorizado"));
                        TrappedError.SetError(Error.Message, Error);

                        if (wic != null)
                            wic.Undo();

                    }
                    catch (IOException Error)
                    {
                        if (targetfile != "")
                            Logging.Append(String.Format("{0} File {1} {2} Erro: {3}", command, sourcefile, targetfile, "Erro de I/O"));
                        else
                            Logging.Append(String.Format("{0} File {1} Erro: {2}", command, sourcefile, "Erro de I/O"));

                        TrappedError.SetError(Error.Message, Error);
                        if (wic != null)
                            wic.Undo();

                    }
                    catch (Exception Error)
                    {
                        if (targetfile != "")
                            Logging.Append(String.Format("{0} File {1} {2} Erro: {3}", command, sourcefile, targetfile, Error.Message));
                        else
                            Logging.Append(String.Format("{0} File {1} Erro: {2}", command, sourcefile, Error.Message));
                        TrappedError.SetError(Error.Message, Error);
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
                TrappedError.SetError(Error.Message, Error);
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
        /// Grava o conteúdo de DataTable para um Arquivo CSV
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <param name="outfile">Arquivo de Saida</param>
        /// <param name="header">Header, se não informado assume o nome das colunas</param>
        /// <param name="delimiter">Delimitador, se não informado assume ';'</param>
        /// <param name="AlphaNum">Array de Campos alfanuméricos</param>
        public static void WriteTableToCSVFile(DataTable table, string outfile, string[] header, string delimiter = ";", string[] AlphaNum=null)
        {
            bool go = true;
            TrappedError.SetError();
            StreamWriter TargetFile = null;
            try
            {
                if (File.Exists(outfile))
                    File.Delete(outfile);
            }
            catch (System.IO.IOException Error)
            {
                if (Error.Message.Contains("O processo não pode acessar o arquivo"))
                    TrappedError.SetError(Error.Message, Error);
                go = false;
            }
            catch (Exception Error)
            {
                TrappedError.SetError(Error.Message, Error);
                go = false;
            }


            if (go)
            {
                TargetFile = new StreamWriter(outfile, true);
                string result = "";
                if (table != null)
                {
                    if (table.Rows.Count > 0)
                    {
                        if (header != null)
                        {
                            for (int i = 0; i < header.Length; i++)
                            {
                                result += header[i].ToUpper() + delimiter;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < table.Columns.Count; i++)
                            {
                                result += table.Columns[i].ColumnName.ToUpper() + delimiter;
                            }
                        }

                        TargetFile.WriteLine(result);

                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            result = "";

                            for (int j = 0; j < table.Columns.Count; j++)
                            {
                                string colName = table.Columns[j].ColumnName.ToUpper();
                                string colValue = table.Rows[i][j].ToString();
                                var colType = table.Columns[j].DataType;
                                if (colType == typeof(DateTime))
                                {
                                    if (!String.IsNullOrWhiteSpace(colValue))
                                        result += Convert.ToDateTime(colValue).ToString("dd/MM/yyyy");
                                    else
                                        result += "00/00/0000";
                                }
                                else if (colType == typeof(double))
                                    result += string.Format("{0:F2}", colValue);
                                else if (colType == typeof(int))
                                    result += table.Rows[i][j].ToString();
                                else if (colType == typeof(float))
                                    result += string.Format("{0:F2}", colValue);
                                else if (colType == typeof(decimal))
                                    result += string.Format("{0:0.00}", Convert.ToDecimal(colValue));
                                else if (colType == typeof(bool))
                                {
                                    var r = Convert.ToBoolean(colValue);
                                    if (r)
                                        result += "1";
                                    else
                                        result += "-1";
                                }

                                else
                                {
                                    if (colName == "NUMCRT" || colName == "CARTAO" || colName == "DSCREF")
                                        result += "=\"" + colValue.NoAccents() + "\"";
                                    else
                                        result += "\"" + colValue.NoAccents() + "\"";
                                }
                                result += delimiter;
                            }
                            TargetFile.WriteLine(result);
                        }
                        TargetFile.Flush();
                        TargetFile.Close();
                        TrappedError.UserError = String.Format("Arquivo {0} gravado com sucesso", outfile);
                    }
                    else
                    {
                        TrappedError.SetError("EMPTYDATA");
                    }
                }
            }
        }


        /// <summary>
        /// Converte um DataTable para XML
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <returns>String</returns>
        public static string TableToXml(DataTable table)
        {
            string RETURN_VALUE = "";
            RETURN_VALUE = String.Format("<?xml version=\"{0}\" encoding=\"{1}\" ?>", XmlVersion, Encoding);
            RETURN_VALUE += string.Format("<{0}>", RootName);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                RETURN_VALUE += string.Format("<{0}>", RecordName);
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    RETURN_VALUE += String.Format("<{0}>{1}</{0}>", table.Columns[j].ColumnName.ToLower(), table.Rows[i][j].ToString());
                }
                RETURN_VALUE += string.Format("</{0}>", RecordName);
            }
            RETURN_VALUE += string.Format("</{0}>", RootName);
            return RETURN_VALUE;
        }

        /// <summary>
        /// Grava uma linha no arquivo
        /// </summary>
        /// <param name="ptext">Texto a ser gravado</param>
        /// <param name="pfilename">Nome do Arquivo</param>
        public static void AppendText(string ptext, string pfilename)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(pfilename))
                {
                    sw.WriteLine(ptext);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception Error)
            {
                TrappedError.SetError(Error.Message, Error);
            }
        }
        /// <summary>
        /// Grava um string builder para um arquivo
        /// </summary>
        /// <param name="pfilename">Nome do Arquivo</param>
        /// <param name="ptext">StringBuilder a ser gravado</param>
        public void AppendText(string pfilename, StringBuilder ptext)
        {
            string[] _lines = ptext.ToString().Split('\n');
            for (int i = 0; i < _lines.Length; i++)
            {
                AppendText(_lines[i].ToString(), pfilename);
            }
        }
        /// <summary>
        /// Cria um arquivo texto
        /// </summary>
        /// <param name="pfilename">Nome do Arquivo</param>
        /// <param name="ptext">Texto do Arquivo</param>
        /// <param name="CheckIfExists">Checar se o arquivo existe, se existir remove</param>
        public static void CreateText(string pfilename, string ptext, bool CheckIfExists = false)
        {
            TrappedError.SetError();
            try
            {
                if (CheckIfExists)
                {
                    RemoveFile(pfilename);
                }
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(pfilename))
                {
                    file.Write(ptext);
                    file.Flush();
                    file.Close();
                }
            }
            catch (Exception Error)
            {
                TrappedError.SetError(Error.Message, Error);
                TrappedError.UserError = String.Format("Falha na gravação do arquivo {0}.", pfilename);
            }
        }

        /// <summary>
        /// Lê um arquivo Binário no formato Base64
        /// </summary>
        /// <param name="pfilename">Nome Completo do Arquivo</param>
        /// <returns>string</returns>
        public static string GetImage(string pfilename)
        {
            try
            {
                byte[] t = System.IO.File.ReadAllBytes(pfilename);
                return Convert.ToBase64String(t);
            }
            catch (Exception Error)
            {
                TrappedError.SetError("", Error.Message, String.Format("Falha na remoção do arquivo {0}.", pfilename), Error);
            }
            return "";
        }

        /// <summary>
        /// Copia um arquivo para o formato Base64
        /// </summary>
        /// <param name="psource">Arquivo de Source</param>
        /// <param name="ptarget">Arquivo de Destinos</param>
        public static void CopyFileFromBase64(string psource, string ptarget)
        {
            try
            {
                string t = System.IO.File.ReadAllText(psource);
                System.IO.File.WriteAllBytes(ptarget, Convert.FromBase64String(t));
            }
            catch (Exception Error)
            {
                TrappedError.SetError("", Error.Message, String.Format("Falha na copia do arquivo {0}.", psource), Error);
            }
        }

        /// <summary>
        /// Copia um arquivo para um arquivo no formato Base64
        /// </summary>
        /// <param name="source">Arquivo de origem</param>
        /// <param name="target">Arquivo de Destino</param>
        public static void CopyToFileBase64(string source, string target)
        {
            byte[] t = System.IO.File.ReadAllBytes(source);
            System.IO.File.WriteAllText(target, Convert.ToBase64String(t));
        }

        /// <summary>
        /// Remove um arquivo
        /// </summary>
        /// <param name="pfilename">Nome Completo do arquivo</param>
        public static void RemoveFile(string pfilename)
        {
            TrappedError.SetError();
            try
            {
                if (System.IO.File.Exists(pfilename))
                    System.IO.File.Delete(pfilename);
            }
            catch (Exception Error)
            {
                TrappedError.SetError("", Error.Message, String.Format("Falha na remoção do arquivo {0}.", pfilename), Error);
            }
        }

        /// <summary>
        /// Retorna o conteudo texto de um arquivo, se existir
        /// </summary>
        /// <param name="pfilename">Nome completo do arquivo</param>
        /// <returns>string, conteudo do arquivo</returns>
        public static string ReadFile(string pfilename)
        {
            TrappedError.SetError();
            try
            {
                if (File.Exists(pfilename))
                    return File.ReadAllText(pfilename);
            }
            catch (Exception Error)
            {
                TrappedError.SetError("", Error.Message, String.Format("Falha na leitura do arquivo {0}.", pfilename), Error);
            }
            return "";
        }
        /// <summary>
        /// Grava um Texto
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="result">Texto a ser gravado</param>
        /// <param name="file">Nome do Arquivo</param>
        public static void WriteAllText(string path, string result, string file)
        {
            if (result != "")
            {
                File.WriteAllText(Path.Combine(path, file), result);
            }
        }
        /// <summary>
        /// Grava um Texto
        /// </summary>
        /// <param name="file">Nome do Arquivo</param> 
        /// <param name="result">Texto a ser gravado</param>
        public static void WriteAllText(string file, string result)
        {
            if (result != "")
                File.WriteAllText(file,result);
            
        }

        /// <summary>
        /// Cria um arquivo XML
        /// </summary>
        /// <param name="xmlContent">Contéudo xml à ser gravado</param>
        /// <param name="filename">Nome completo do arquivo</param>
        public static void CreateXmlFile(string xmlContent, string filename)
        {
            TrappedError.SetError();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlContent);
                if (File.Exists(filename))
                    File.Delete(filename);
                doc.Save(filename);
                doc = null;
            }
            catch (Exception Error)
            {
                TrappedError.SetError("", Error.Message, String.Format("Falha na gravação do arquivo {0}.", filename), Error);
            }
        }


        /// <summary>
        /// Thumb de Imagem
        /// </summary>
        /// <param name="FilePathName">Nome do Arquivo de Imagem alvo</param>
        /// <param name="ThumbFile">Nome do Arquivo de thumbmail a ser gerado</param>
        /// <param name="ImgWidth">Largura de Aplicação</param>
        /// <param name="ImgHeight">Altura de Aplicação</param>
        /// <returns>Image</returns>
        public static void ThumbImage(string FilePathName, string ThumbFile, int ImgWidth, int ImgHeight)
        {
            TrappedError = new TrapError();
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
                    TrappedError.SetError("", Error.Message, String.Format("{0}.\nFalha na conversão de imagem para thumbnail.", Error.Message), Error);
                }
                finally
                {
                    oImg.Dispose();
                }
            }
        }

        /// <summary>
        /// Thumb de Imagem
        /// </summary>
        /// <param name="FilePathName">Nome do Arquivo</param>
        /// <param name="ImgWidth">Largura de Aplicação</param>
        /// <param name="ImgHeight">Altura de Aplicação</param>
        /// <returns>Image</returns>
        public static System.Drawing.Image ThumbImage(string FilePathName, int ImgWidth, int ImgHeight)
        {
            TrappedError = new TrapError();
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
                    TrappedError.SetError("", Error.Message, String.Format("{0}.\nFalha na conversão de imagem para thumbnail.", Error.Message), Error);
                }
                finally
                {
                    oImg.Dispose();
                }
            }
            return null;
        }


        /// <summary>
        /// Move um arquivo para uma determinada extensão
        /// </summary>
        /// <param name="pFILENAME">Nome do Arquivo</param>
        /// <param name="extension">Extensão</param>
        public static void Move(string pFILENAME, string extension = "PROCESSADO")
        {
            IMoveTaskFile(pFILENAME, System.IO.Path.GetDirectoryName(pFILENAME), extension);
        }
        /// <summary>
        /// Move um arquivo para uma determinado diretorio e extensão
        /// </summary>
        /// <param name="pFILENAME">Nome do Arquivo</param>
        /// <param name="targetDir">Nome do Diretorio</param>
        /// <param name="extension">Extensão</param>
        public static void Move(string pFILENAME, string targetDir, string extension = "PROCESSADO")
        {
            IMoveTaskFile(pFILENAME, targetDir, extension);
        }

        private static void IMoveTaskFile(string pFILENAME, string targetDir, string extension = "PROCESSADO")
        {
            TrappedError.SetError();
            string oFile = "";
            try
            {
                if (extension.IndexOf(".") < 0)
                    extension = "." + extension;
                oFile = Directories.CombinePath(targetDir, System.IO.Path.GetFileNameWithoutExtension(pFILENAME) + extension);
                File.Move(pFILENAME, oFile);
            }
            catch (Exception Error)
            {
                TrappedError.SetError("", Error.Message, String.Format("Falha na troca de nome do arquivo {0}  para {1}", pFILENAME, oFile), Error);
            }
        }

        /// <summary>
        /// Retorna o número de linhas de um arquivo
        /// </summary>
        /// <param name="sourcefile">Nome do Arquivo</param>
        /// <returns>int</returns>
        public static int GetLines(string sourcefile)
        {
            bool go = true;
            int RETURN_VALUE = 0;
            StreamReader rd = null;
            if (go)
            {
                try
                {
                    rd = new StreamReader(sourcefile);
                }
                catch (FileNotFoundException Error)
                {
                    TrappedError.SetError("FILENOTFOUND");
                    TrappedError.ErrorMessage = Error.Message;
                    TrappedError.ErrorObject = Error;
                    go = false;
                }
            }
            if (go)
            {
                try
                {
                    string _line = "";
                    int _NUMLIN = 0;
                    while ((_line = rd.ReadLine()) != null)
                    {
                        _NUMLIN++;
                    }

                    rd.Close();
                    RETURN_VALUE = _NUMLIN--;

                }
                catch (Exception Error)
                {
                    TrappedError.ErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                    if (TrappedError.ErrorCode != "")
                        TrappedError.SetError(TrappedError.ErrorCode);
                    TrappedError.ErrorMessage = Error.Message;
                    TrappedError.ErrorObject = Error;
                    RETURN_VALUE = 0;
                }
            }
            return RETURN_VALUE;
        }

        /// <summary>
        /// Define um nome de arquivo de saida
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="basefile">Nome Base do Arquivo</param>
        /// <param name="extension">Extensão, assume a extensao '.log' por padrão</param>
        /// <param name="applyserialization">define se deve serializar o nome com uma data, se não informado assume 'true'</param>
        /// <returns>string, contendo o nome do arquivo</returns>
        public static string SetLogFile(string path, string basefile, string extension = ".log", bool applyserialization=true)
        {
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            string file = "";
            if (applyserialization)
                file = Directories.CombinePath(path, basefile + "_" + DateTime.Now.ToString("ddMMyyyy") + extension);
            else
                file = Directories.CombinePath(path, basefile + extension);
            return file;
        }


        /// <summary>
        /// Remove um arquivo
        /// </summary>
        /// <param name="file">Nome do Arquivo</param>
        /// <returns>true, se o arquivo não mais existe</returns>
        public static bool DeleteFile(string file)
        {
            try
            {
                if (System.IO.File.Exists(file))
                {
                    System.IO.File.Delete(file);
                }
            }
            catch (Exception Error)
            {
                TrappedError.ErrorCode = ThunderFire.ErrorManager.GetErrorCode(Error);
                if (TrappedError.ErrorCode != "")
                    TrappedError.SetError(TrappedError.ErrorCode);
                TrappedError.ErrorMessage = Error.Message;
                TrappedError.ErrorObject = Error;

                return false;
            }
            return !System.IO.File.Exists(file);
        }

    }
}