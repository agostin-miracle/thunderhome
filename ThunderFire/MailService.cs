using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace ThunderFire
{
    /// <summary>
    /// Tipo de Email
    /// </summary>
    public enum MailDataSelector
    {
        /// <summary>
        /// Boas Vindas
        /// </summary>
        WELCOME_MAIL = 1
    }

    /// <summary>
    /// Tipo de Montagem do Email
    /// </summary>
    public enum MailDataAssembyType
    {
        /// <summary>
        /// Inline Embedding (Base64 Encoding)
        /// </summary>
        CID = 1,
        /// <summary>
        /// Data-Uri (Linked Image)
        /// </summary>
        EMBED = 2
    }


    /// <summary>
    /// Classe de Tratamento de Email
    /// </summary>
    public static class MailData
    {
        /// <summary>
        /// Captura de Erro
        /// </summary>
        public static TrapError TrappedError = new TrapError();

        private static string _templatePath = "";
        /// <summary>
        /// Diretiva de Configuracao do Host do Email
        /// </summary>
        public static string MAILHOST = "MailHost";
        /// <summary>
        /// Diretiva de Configuracao do Host Name do Email
        /// </summary>
        public static string MAILHOSTNAME = "MailHostName";
        /// <summary>
        /// Diretiva de Configuracao da Senha do Email
        /// </summary>
        public static string MAILHOSTPASSWORD = "MailHostPassword";
        /// <summary>
        /// Diretiva de Configuracao do originador da mensagem
        /// </summary>
        public static string MAILFROM = "MailFrom";
        /// <summary>
        /// Diretiva de Configuracao da lista de CC do Email
        /// </summary>
        public static string MAILBCC = "MailBcc";

        /// <summary>
        /// Diretiva de Configuracao de Habilitação de SSL
        /// </summary>

        public static string MAILENABLESSL = "MailEnableSSL";

        /// <summary>
        /// Diretiva de Configuracao de Porta
        /// </summary>

        public static string MAILPORT = "MailPort";

        /// <summary>
        /// Path de Localização dos arquivos de template de mensagens de email
        /// </summary>
        /// <returns>string, path de arquivos</returns>
        public static string TemplatePath
        {
            get { return _templatePath; }
            set { _templatePath = value; }
        }
        /// <summary>
        /// Retorna a string do último erro ocorrido
        /// </summary>
        public static string LastError;

        /// <summary>
        /// Carrega as configurações de endereçamento do email
        /// </summary>
        /// <returns>MailService</returns>
        public static MailService Get()
        {
            return GetFromString("");
        }

        /// <summary>
        /// Obtêm um corpo de email definido por uma string e as configurações de endereçamento do email
        /// </summary>
        /// <param name="bodytext">Corpo do Email</param>
        /// <returns>MailService</returns>
        public static MailService GetFromString(string bodytext)
        {
            MailService mail = GetParameterFromConfig();
            if (mail != null)
            {
                try
                {
                    if (bodytext != "")
                    {
                        mail.SourceBody = bodytext.Trim();
                        mail.Body = bodytext.Trim();
                    }
                    return mail;
                }
                catch (Exception Error)
                {

                    string ErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                    if (ErrorCode == "")
                        TrappedError.SetError("INTERNALSERVERERROR");
                    else
                    {
                        TrappedError.SetError(ErrorCode);
                        TrappedError.ErrorMessage = Error.Message;
                    }
                    TrappedError.SourceError = "PARAMETROS DE TEXTO";
                }
            }
            return null;
        }

        

        /// <summary>
        /// Obtêm um corpo de email definido em um arquivo e as configurações de endereçamento do email
        /// </summary>
        /// <param name="sourcebody">Localização do Arquivo</param>
        /// <returns>MailService</returns>
        public static MailService GetFromFile(string sourcebody)
        {
            MailService mail = GetParameterFromConfig();
            if (mail != null)
            {
                try
                {
                    mail.Path = TemplatePath;
                    mail.SourceBody = Path.GetFullPath(sourcebody);
                    mail.Body = GetTextFile(sourcebody);
                    return mail;
                }

                catch (Exception Error)
                {
                    string ErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                    if (ErrorCode == "")
                        TrappedError.SetError("INTERNALSERVERERROR");
                    
                    else
                    {
                        TrappedError.SetError(ErrorCode);
                        TrappedError.ErrorMessage = Error.Message;
                    }
                    TrappedError.SourceError = "PARAMETROS DE ARQUIVO";
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm o nome do arquivo de template 
        /// </summary>
        /// <param name="paramfiletemplate">Nome do Parâmetro que contêm a localização do template</param>
        /// <returns>MailService</returns>
        public static MailService GetFromConfig(string paramfiletemplate)
        {
            MailService mail = GetParameterFromConfig();
            if (mail != null)
            {
                try
                {
                    var reader = new AppSettingsReader();
                    /* Nome do arquivo */
                    string _appFile = reader.GetValue(paramfiletemplate, typeof(string)).ToString();
                    mail.Path = Path.GetDirectoryName(_appFile);
                    mail.SourceBody = Path.GetFullPath(paramfiletemplate);

                    /* conteúdo do arquivo */
                    mail.Body = GetTextFile(_appFile);

                    return mail;
                }
                catch (Exception Error)
                {

                    string ErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                    if (ErrorCode == "")
                        TrappedError.SetError("INTERNALSERVERERROR");
                    else
                        TrappedError.SetError(ErrorCode);
                    
                    TrappedError.ErrorMessage = Error.Message;
                    TrappedError.SourceError = "PARAMETROS DE ARQUIVO CONFIGURACAO";

                }
            }
            return null;
        }


        /// <summary>
        /// Obtêm o conteúdo de um arquivo texto
        /// </summary>
        /// <param name="filename">Localização do Arquivo</param>
        /// <returns>string, com o conteúdo do arquivo</returns>
        public static string GetTextFile(string filename)
        {
            if (File.Exists(filename))
                return File.ReadAllText(filename);
            return "";
        }

        /// <summary>
        /// Efetua uma substituição de dados
        /// </summary>
        /// <param name="body">Corpo do Email</param>
        /// <param name="marker">Marcador</param>
        /// <param name="data">Dados a serem substituidos</param>
        /// <returns>string</returns>
        public static string ReplaceText(string body, string marker, string data)
        {
            return body.Replace(marker, data);
        }

        /// <summary>
        /// Pega uma imagem
        /// </summary>
        /// <param name="filename">Nome do Arquivo</param>
        /// <param name="type">Tipo de Imagem associada</param>
        /// <remarks>
        /// Converte para uma imagem embed base64
        /// </remarks>
        /// <returns>string</returns>
        public static string MakeImageSrcData(string filename, string type="png")
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] filebytes = new byte[fs.Length];
            fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
            string result = string.Format("data:image/{0};base64,",type) + Convert.ToBase64String(filebytes, Base64FormattingOptions.None);
            return result;
        }
        /// <summary>
        /// Pega uma imagem em formato Bitmap
        /// </summary>
        /// <param name="img">Bitmap</param>
        /// <param name="type">Tipo a ser embedado</param>
        /// <param name="imgformat">Formato da Imagem</param>
        /// <remarks>
        /// Converte para uma imagem embed base64
        /// </remarks>
        /// <returns>string</returns>
        private static string MakeImageSrcData(System.Drawing.Bitmap img, string type, System.Drawing.Imaging.ImageFormat imgformat)
        {
            System.Drawing.Bitmap bImage = img;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bImage.Save(ms, imgformat);
            byte[] byteImage = ms.ToArray();
            return string.Format("data:image/{0};base64,", type) + Convert.ToBase64String(byteImage);
        }

        #region "Membros Privados"

        private static MailService GetParameterFromConfig()
        {
            MailService mail = new MailService();
            try
            {
                var reader = new AppSettingsReader();
                mail.MailHost = reader.GetValue(MAILHOST, typeof(string)).ToString();
                mail.MailUser = reader.GetValue(MAILHOSTNAME, typeof(string)).ToString();
                mail.MailPassword = reader.GetValue(MAILHOSTPASSWORD, typeof(string)).ToString();
                mail.From = reader.GetValue(MAILFROM, typeof(string)).ToString();
                mail.Bcc = reader.GetValue(MAILBCC, typeof(string)).ToString();
                mail.Port = int.Parse(reader.GetValue(MAILPORT, typeof(string)).ToString());
                mail.EnableSsl = bool.Parse(reader.GetValue(MAILENABLESSL, typeof(string)).ToString().ToLower());
            }
            catch (Exception Error)
            {
                string ErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                if (ErrorCode == "")
                    TrappedError.SetError("INTERNALSERVERERROR");
                else
                {
                    TrappedError.SetError(ErrorCode);
                    TrappedError.ErrorMessage = Error.Message;
                }
                TrappedError.SourceError = "PARAMETROS DE CONFIGURACAO";
                mail = null;
            }
            return mail;
        }

        private static string CombinePath(string dir, string file)
        {
            string r = "";
            if (dir.EndsWith("\\"))
                r = dir + file;
            else
                r = dir + "\\" + file;
            return r;
        }

        private static string getTemplateData(string templatename)
        {
            string text = "";
            if (File.Exists(templatename))
                text = System.IO.File.ReadAllText(templatename);
            return text;
        }
        #endregion "Membros Privados"
    }

    /// <summary>
    /// Anexos
    /// </summary>
    public class AttachmentAdd
    {
        /// <summary>
        /// Lista de Anexos
        /// </summary>
        public List<Attachment> Anexos = new List<Attachment>();

        /// <summary>
        /// Construtor Base
        /// </summary>
        public AttachmentAdd() { }
        /// <summary>
        /// Adiciona uma anexo
        /// </summary>
        /// <param name="file">Nome do Arquivo</param>
        public AttachmentAdd(string file)
        {
            Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                     ContentDisposition disposition = data.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(file);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
            Anexos.Add(data);
        }
    }
    /// <summary>
    /// Retêm os dados a serem substituidos no email
    /// </summary>
    public class MailDataReplace
    {
        /// <summary>
        /// Chave de Busca
        /// </summary>
        public string Key = "";
        /// <summary>
        /// Valor da chave de Busca
        /// </summary>
        public string Value = "";
        /// <summary>
        /// Define se é uma imagem
        /// </summary>
        public bool IsImage = false;
        /// <summary>
        /// Instância de Classe
        /// </summary>
        /// <param name="pKey">Chave de Busca</param>
        /// <param name="pValue">Valor da Chave de busca</param>
        /// <param name="pIsImage">Indica se é uma imagem</param>
        public MailDataReplace(string pKey, string pValue, bool pIsImage)
        {
            this.Key = pKey;
            this.Value = pValue;
            this.IsImage = pIsImage;
        }

        #region "Membros Privados"
        #endregion "Membros Privados"
    }

    /// <summary>
    /// Tratamento de Email
    /// </summary>
    public class MailService
    {
        /// <summary>
        /// Indica se deve usar a diretiva UseDefaultCredentials
        /// </summary>
        public bool UseDefaultCredentials = false;
        /// <summary>
        /// Assunto
        /// </summary>
        public string Subject { get; set; } = "";
        /// <summary>
        /// Corpo do email
        /// </summary>
        public string Body { get; set; } = "";

        /// <summary>
        /// Path de Localização dos Templates
        /// </summary>
        public string Path  { get; set; }="";
        /// <summary>
        /// Body do Email
        /// </summary>
        public string SourceBody { get; set; } = "";
        /// <summary>
        /// Define se o corpo do email é HTML
        /// </summary>
        public bool isBodyHTML { get; set; } = false;
        /// <summary>
        /// email
        /// </summary>
        public string MailTo { get; set; } = "";
        /// <summary>
        /// Copia oculta
        /// </summary>
        public string Bcc { get; set; } = "";
        /// <summary>
        /// C/C
        /// </summary>
        public string CC { get; set; } = "";
        /// <summary>
        /// De
        /// </summary>
        public string From { get; set; } = "";
        /// <summary>
        /// String do ultimo erro ocorrido
        /// </summary>
        public string LastError { get; set; } = "";
        /// <summary>
        /// Email de Envio
        /// </summary>
        public string Mail { get; set; } = "";
        /// <summary>
        /// Usuario do Email de Envio
        /// </summary>
        public string MailUser { get; set; } = "";
        /// <summary>
        /// Senha do Usuario de Email
        /// </summary>
        public string MailPassword { get; set; } = "";
        /// <summary>
        /// Host do Email
        /// </summary>
        public string MailHost { get; set; } = "";

        /// <summary>
        /// Define o tipo de Corpo do Email
        /// </summary>
        public MailDataAssembyType BodyType = MailDataAssembyType.EMBED;

        /// <summary>
        /// Porta padrão
        /// </summary>
        public int Port { get; set; } = 587;

        /// <summary>
        /// Define se usa SSL
        /// </summary>
        public bool EnableSsl { get; set; } = true;

        /// <summary>
        /// Cria uma instância do Serviço de Email
        /// </summary>
        public MailService()
        {
            this.isBodyHTML = true;
        }

        /// <summary>
        /// Lista de objetos a serem substituidos
        /// </summary>
        public List<MailDataReplace> Replacements = new List<MailDataReplace>();


        /// <summary>
        /// Captura de Erro
        /// </summary>
        public TrapError TrappedError = new TrapError();


        /// <summary>
        /// Envia o Email
        /// </summary>
        /// <returns>bool, true se o email foi enviado com sucesso</returns>
        public bool SendMail(AttachmentAdd anexos=null)
        {
            MailMessage oMail = new MailMessage();
            SmtpClient SMTPC = new SmtpClient();
            string _imagepath = "";
            bool retorno = false;
            TrappedError.SetError();
            try
            {
                //oMail.Body = RemoveChangeBracket(this.Body);
                oMail.Body = this.Body;
                oMail.From = new MailAddress(this.MailUser);
                oMail.IsBodyHtml = this.isBodyHTML;
                oMail.Subject = this.Subject;
                if (!(this.MailTo == null))
                {
                    string[] _item = this.MailTo.Split(new char[] { ';' });
                    for (int i = 0; i < _item.Length; i++)
                    {
                        if (!_item[i].ToString().Equals(""))
                        {
                            oMail.To.Add(new MailAddress(_item[i]));
                        }
                    }
                }
                if (!(this.CC == null))
                {
                    string[] _item = this.CC.Split(new char[] { ';' });
                    for (int i = 0; i < _item.Length; i++)
                    {
                        if (!_item[i].ToString().Equals(""))
                        {
                            oMail.CC.Add(new MailAddress(_item[i]));
                        }
                    }
                }

                for (int i = 0; i < Replacements.Count; i++)
                {
                    if (!Replacements[i].IsImage)
                    {
                        this.Body = this.Body.Replace("@" + Replacements[i].Key, Replacements[i].Value);
                    }
                    else
                    {
                        if (this.BodyType == MailDataAssembyType.EMBED)
                        {
                            _imagepath = CombinePath(this.Path, Replacements[i].Value);
                            var replace = MailData.MakeImageSrcData(_imagepath);
                            this.Body = this.Body.Replace("@" + Replacements[i].Key, replace);
                        }
                        else
                        {
                            this.Body = this.Body.Replace("@" + Replacements[i].Key, "cid:" + Replacements[i].Key);
                        }
                    }
                }

                if (this.BodyType == MailDataAssembyType.CID)
                {
                    AlternateView avHtml = AlternateView.CreateAlternateViewFromString
                            (this.Body, null, System.Net.Mime.MediaTypeNames.Text.Html);

                    for (int i = 0; i < Replacements.Count; i++)
                    {
                        if (Replacements[i].IsImage)
                        {
                            if (this.BodyType == MailDataAssembyType.CID)
                            {
                                avHtml.LinkedResources.Add(AddImage(CombinePath(this.Path, Replacements[i].Value), Replacements[i].Key));
                                oMail.AlternateViews.Add(avHtml);
                            }
                        }
                    }
                }

                if (this.MailHost != "")
                {
                    if (this.Bcc != "")
                    {
                        string[] _item = this.Bcc.Split(new char[] { ';' });
                        for (int i = 0; i < _item.Length; i++)
                        {
                            if (!_item[i].ToString().Equals(""))
                            {
                                oMail.Bcc.Add(new MailAddress(_item[i]));
                            }
                        }
                    }


                    if (anexos != null)
                    {
                        foreach (Attachment item in anexos.Anexos)
                        {
                            oMail.Attachments.Add(item);
                        }
                    }


                    if (this.BodyType == MailDataAssembyType.EMBED)
                        oMail.Body = this.Body;

                    SMTPC.DeliveryMethod = SmtpDeliveryMethod.Network;
                    SMTPC.UseDefaultCredentials = true;// this.UseDefaultCredentials;
                    SMTPC.Host = this.MailHost;
                    SMTPC.Port = this.Port;
                    SMTPC.EnableSsl = this.EnableSsl;
                    SMTPC.Credentials = new System.Net.NetworkCredential(this.MailUser.Trim(), this.MailPassword.Trim());

                    SMTPC.Send(oMail);
                    retorno = true;
                }
            }
            catch (Exception Error)
            {
                string ErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                if (ErrorCode == "")
                    ErrorCode = "INTERNALSERVERERROR";
                TrappedError.SetError(ErrorCode);
                if (ErrorCode== "CONVERTFILENOTGIVEN")
                {
                    TrappedError.UserError = String.Format(TrappedError.UserError,ThunderFire.ErrorManager.FieldName);
                }
                TrappedError.ErrorMessage = Error.Message;
                TrappedError.SourceError = "SENDING";
            }
            return retorno;
        }

        private string CombinePath(string dir, string file)
        {
            string r = "";
            if (dir.EndsWith("\\"))
                r = dir + file;
            else
                r = dir + "\\" + file;
            return r;
        }

        private System.Net.Mail.LinkedResource AddImage(string filePath, string contentid)
        {
            System.Net.Mail.LinkedResource inline = new System.Net.Mail.LinkedResource(filePath);
            inline.ContentId = contentid;
            return inline;
        }
    }
}