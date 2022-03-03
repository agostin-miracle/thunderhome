using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace ThunderFire
{
    /// <summary>
    /// Mensagem de Erro
    /// </summary>
    public class ErrorMsg
    {
        /// <summary>
        /// Nome do Método
        /// </summary>
        public string Method { get; set; } = "";
        /// <summary>
        /// ID do Errro
        /// </summary>
        public int ErrorId { get; set; } = -1;
        /// <summary>
        /// Código do Erro
        /// </summary>
        public string ErrorCode { get; set; } = "";
        /// <summary>
        /// Severidade do Erro
        /// </summary>
        public string Severity { get; set; } = "";
        /// <summary>
        /// Mensagem
        /// </summary>
        public string Message { get; set; } = "ERRO OU FALHA NAO CATALOGADA";
        /// <summary>
        /// Descrição
        /// </summary>
        public string Description { get; set; } = "";
        /// <summary>
        /// Origem do Erro
        /// </summary>
        public string Source { get; set; } = "";

        /// <summary>
        /// ID do Erro
        /// </summary>
        public int ID { get; set; } = -1;

        /// <summary>
        /// Construtor
        /// </summary>
        public ErrorMsg() { }


        /// <summary>
        /// Construtor de Mensagens
        /// </summary>
        /// <param name="errorid">ID do erro</param>
        /// <param name="errorcode">Código do Erro</param>
        /// <param name="severity">Severidade do Erro</param>
        /// <param name="message">Mensagem do erro</param>
        /// <param name="description">Descrição do Erro</param>
        /// 
        public ErrorMsg(int errorid, string errorcode, string severity, string message, string description) : this(errorid, errorcode, severity, message, description, "") { }

        /// <summary>
        /// Construtor de Mensagens
        /// </summary>
        /// <param name="errorid">ID do erro</param>
        /// <param name="errorcode">Código do Erro</param>
        /// <param name="severity">Severidade do Erro</param>
        /// <param name="message">Mensagem do erro</param>
        /// <param name="description">Descrição do Erro</param>
        /// <param name="source">Origem do Erro</param>
        public ErrorMsg(int errorid, string errorcode, string severity, string message, string description, string source)
        {
            this.ErrorId = errorid;
            this.ErrorCode = errorcode;
            this.Severity = severity;
            this.Message = message;
            this.Description = description;
            this.Source = source;
        }
    }

    /// <summary>
    /// Gerenciador de Erros
    /// </summary>
    public static class ErrorManager
    {
        /// <summary>
        /// Nome do Objeto relacionado ao Erro
        /// </summary>
        public static string FieldName { get; internal set; } = "";

        /// <summary>
        /// Retorna o ultimo objeto Exception
        /// </summary>
        public static object ErrorObject { get; set; }

        /// <summary>
        /// Retorna a mensagem do erro ocorrido
        /// </summary>
        /// <param name="code">Código do Erro</param>
        /// <returns>string</returns>
        public static string GetStringMsg(string code)
        {
            return GetError(code.ToUpper()).Message;
        }
        /// <summary>
        /// Retorna a mensagem do erro ocorrido com substituição do argumento em arg1
        /// </summary>
        /// <param name="code">Código do Erro</param>
        /// <param name="arg1">Argumento de Substituição</param>
        /// <returns>string</returns>
        public static string GetStringMsg(string code, string arg1)
        {
            string result = GetError(code.ToUpper()).Message;
            if (result != "")
            {
                result = string.Format(result, arg1);
            }
            return result;
        }

        /// <summary>
        /// Obtem o Código de Erro associado a exceção SQL lançada
        /// </summary>
        /// <param name="error">SQLException</param>
        /// <returns>string, código do erro</returns>
        public static string GetErrorCode(SqlException error)
        {
            return GetErrorCode(error.Message);
        }

        /// <summary>
        /// Obtem o Código de Erro associado a exceção lançada
        /// </summary>
        /// <param name="_Error">Exception</param>
        /// <returns>string, código do erro</returns>
        public static string GetErrorCode(Exception _Error)
        {
            return GetErrorCode(_Error.Message);
        }

        /// <summary>
        /// Retorna o código do Erro associado à string
        /// </summary>
        /// <param name="ErrorMessage">String do Erro ocorrido</param>
        /// <returns>Código Interno do Erro</returns>
        public static string GetErrorCode(string ErrorMessage)
        {
            FieldName = "";
            if (!String.IsNullOrEmpty(ErrorMessage))
            {
                if (ErrorMessage.Contains("A propriedade ConnectionString não foi inicializada."))
                    return "EMPTYCONNECTIONSTRING";

                if (ErrorMessage.Contains("The given path's format is not supported"))
                    return "INVALIDPATH";

                if (ErrorMessage.Contains("Could not find file"))
                {
                    FieldName = ErrorMessage.Replace("Could not find file", "").Trim();
                    return "CONVERTFILENOTGIVEN";
                }

                if (ErrorMessage.Contains("The timeout period elapsed prior to obtaining a connection from the pool."))
                {
                    return "DBPOOLTIMEOUT";
                }
                if (ErrorMessage.Contains("Could not find a part of the path"))
                {
                    return "INVALIDDIRECTORY";
                }
                if (ErrorMessage.Contains("Method not found"))
                {
                    FieldName = ErrorMessage.Replace("Method not found: '", "").Replace("'.", "").Trim();
                    return "METHODNOTFOUND";
                }
                if (ErrorMessage.Contains("Specified key is a known weak key for"))
                {
                    return "WEAKKEY";
                }
                if (ErrorMessage.Contains("Unable to connect to the remote server"))
                {
                    return "RESOURCEFAILREAD";
                }
                if (ErrorMessage.Contains("fornecido diversas vezes") && ErrorMessage.Contains("Parâmetro"))
                {
                    return "INTERNALDBSPPARAM";
                }

                if (ErrorMessage.Contains("ConnectionRefusedError") || ErrorMessage.Contains("ConnectionRefusedError"))
                    return "NETWORKERROR";
                if (ErrorMessage.Contains("O estado atual da conexão é fechada"))
                    return "FAILCONNECTION";
                if (ErrorMessage.Contains("An existing connection was forcibly closed by the remote host"))
                    return "ABORTEDCONNECTION";
                if (ErrorMessage.Contains("Erro de rede ou específico à instância ao estabelecer conexão com o SQL Server."))
                {
                    return "FAILCONNECTIONNET";
                }
                if (ErrorMessage.Contains("Falha de logon do usuário "))
                {
                    FieldName = ErrorMessage.Replace("Falha de logon do usuário ", "").Replace("'", "").Replace(".", "").Trim();
                    return "INTERNALDBLOGON";
                }
                if (ErrorMessage.Contains("Nome de objeto ") && (ErrorMessage.Contains("inválido.")))
                {
                    FieldName = ErrorMessage.Replace("Nome de objeto  ", "").Replace("'", "").Replace(".", "").Trim();
                    FieldName = FieldName.Replace("inválido.", "").Replace("'", "").Replace(".", "").Trim();
                    return "INTERNALDBOBJECTERROR";
                }

                if (ErrorMessage.Contains("Column ") && (ErrorMessage.Contains(" does not belong to table .")))
                {
                    FieldName = ErrorMessage.Replace("does not belong to table.", "").Replace("'", "").Replace(".", "").Trim();
                    FieldName = FieldName.Replace("Column", "").Replace("'", "").Replace(".", "").Trim();
                    return "INTERNALDBCOLUMNFAULT";
                }

                if (ErrorMessage.Contains("Column ") && (ErrorMessage.Contains(" does not belong to table item.")))
                {

                    FieldName = ErrorMessage.Replace("does not belong to table item.", "").Replace("'", "").Replace(".", "").Trim();
                    FieldName = FieldName.Replace("Column", "").Replace("'", "").Replace(".", "").Trim();
                    return "INTERNALDBCOLUMNFAULT";
                }

                if (ErrorMessage.Contains("Não foi possível localizar uma parte do caminho"))
                {
                    FieldName = FieldName.Replace("Não foi possível localizar uma parte do caminho", "").Trim();
                    return "INVALIDDIRECTORYOUTPUT";
                }

                //Procedure or function PRCADENDUPD has too many arguments specified.
                if ((ErrorMessage.Contains("Procedure or function")) && (ErrorMessage.Contains("has too many arguments specified")))
                {
                    FieldName = ErrorMessage.Replace("Procedure or function", "");
                    FieldName = FieldName.Replace("has too many arguments specified", "").Trim();
                    return "INTERNALDBPROCMANYARGS";
                }
                if (ErrorMessage.Contains("Invalid object name "))
                {
                    FieldName = ErrorMessage.Replace("Invalid object name ", "").Replace("'", "").Replace(".", "").Trim();
                    return "INTERNALDBOBJECT";
                }
                if (ErrorMessage.Contains("O processo não pode acessar o arquivo"))
                    return "FILEINUSE";
                if (ErrorMessage.Contains("Violação da restrição PRIMARY KEY"))
                    return "INTERNALDBERRORRECORDFOUND";

                if (ErrorMessage.Contains("Violation of PRIMARY KEY constraint") && ErrorMessage.Contains("Cannot insert duplicate key in object"))
                    return "INTERNALDBERRORRECORDFOUND";

                if (ErrorMessage.Contains("A instrução DELETE conflitou com a restrição do"))
                    return "INTERNALDBERRORRESTRICTDELETE";
                if (ErrorMessage.Contains("Não foi possível encontrar o procedimento armazenado"))
                {
                    FieldName = ErrorMessage.Replace("Não foi possível encontrar o procedimento armazenado ", "").Replace("'", "").Replace(".", "").Trim();
                    return "INTERNALDBERRORPROCNOTFOUND";
                }
                if (ErrorMessage.Contains("Could not find stored procedure"))
                {
                    FieldName = ErrorMessage.Replace("Could not find stored procedure ", "").Replace("'", "").Replace(".", "").Trim();
                    return "INTERNALDBERRORPROCNOTFOUND";
                }
                if (ErrorMessage.Contains("espera o parâmetro") && ErrorMessage.Contains("que não foi fornecido"))
                    return "INTERNALDBERRORPARAMETERSFAULT";

                if (ErrorMessage.Contains("expects parameter") && ErrorMessage.Contains("which was not supplied")) {
                    FieldName = ErrorMessage.Replace("Procedure or function ", "").Replace("expects parameter", "").Replace(", which was not supplied.", "").Trim();
                    return "INTERNALDBERRORPARAMETERSFAULT";
                }

                if (ErrorMessage.Contains("A instrução INSERT conflitou com a restrição do FOREIGN KEY"))
                    return "INTERNALDBERRORKEYFAULT";
                if (ErrorMessage.Contains("The INSERT statement conflicted with the FOREIGN KEY constraint"))
                {


                    FieldName = ErrorMessage.Replace("The INSERT statement conflicted with the FOREIGN KEY constraint ", "");
                    FieldName = FieldName.Replace("The conflict occurred in database", "");
                    FieldName = FieldName.Replace("The statement has been terminated.", "");
                    FieldName = FieldName.Replace("\"", "");

                    return "INTERNALDBERRORKEYFAULT";
                }
                if (ErrorMessage.Contains("Há muito argumentos especificados para o procedimento ou a função"))
                    return "INTERNALDBERRORARGSEXCESS";
                if (ErrorMessage.Contains("Não é possível inserir o valor NULL na coluna"))
                    return "INTERNALDBERRORFIELDTONULL";
                if (ErrorMessage.Contains("A coluna") && (ErrorMessage.Contains("não pertence à tabela")))
                    return "INTERNALDBCOLUMNFAULT";
                if (ErrorMessage.Contains("O tempo limite de espera foi atingido"))
                    return "INTERNALDBTIMEOUTT";
                if (ErrorMessage.Contains("Falha ao converter de uma cadeia de caracteres em identificador exclusivo"))
                    return "INTERNALCONVERTIONKEY";
                if (ErrorMessage.Contains("Tempo Limite de Execução Expirado"))
                    return "CONNECTIONTIMEOUT";
                if (ErrorMessage.Contains("SqlParameterCollection não contém SqlParameter com ParameterName"))
                    return "INTERNALDBERRORPARAMETERS";

                if (ErrorMessage.Contains("Cannot insert the value NULL into column")) {

                    FieldName = ErrorMessage.Replace("Cannot insert the value NULL into column ", "");
                    FieldName = FieldName.Replace("The statement has been terminated.", "");
                    FieldName = FieldName.Replace("; column does not allow nulls.", "Coluna não permite nulo.");
                    FieldName = FieldName.Replace(".UPDATE fails.", "O comando UPDATE falhou");
                    return "INTERNALDBERRORFIELDTONULL";
                }

                if (ErrorMessage.Contains("A instrução UPDATE conflitou com a restrição do FOREIGN KEY "))
                    return "INTERNALDBCHECKCONSTRAINT";
                if (ErrorMessage.Contains("The UPDATE statement conflicted with the CHECK constraint "))
                    return "INTERNALDBCHECKCONSTRAINT";
                if (ErrorMessage.Contains("The UPDATE statement conflicted with the CHECK constraint "))
                    return "INTERNALDBCHECKCONSTRAINT";
                if (ErrorMessage.Contains("The UPDATE statement conflicted with the FOREIGN KEY constraint"))
                    return "INTERNALDBFOREIGNCONSTRAINT";
                if (ErrorMessage.Contains("Um nome não pode ser iniciado pelo caractere '"))
                    return "XMLFILERROR";
                if (ErrorMessage.Contains("Erro durante a análise de EntityName."))
                    return "XMLFILERROR";
            }
            return "";
        }

        /// <summary>
        /// Retorna uma string representando o codigo do Erro  
        /// </summary>
        /// <param name="ErrorMessage">Mensagem de Erro</param>
        /// <param name="defaulterror">Código de Erro Padrão</param>
        /// <returns></returns>
        public static string GetErrorCode(string ErrorMessage, string defaulterror)
        {
            string RETURN_VALUE = GetErrorCode(ErrorMessage);
            if (RETURN_VALUE == "")
                RETURN_VALUE = defaulterror;
            return RETURN_VALUE;
        }

        /// <summary>
        /// Lê uma mensagem de erro configurada
        /// </summary>
        /// <param name="CodeError">Código da mensagem de erro</param>
        /// <returns>ErrorMsg</returns>
        public static ErrorMsg GetError(string CodeError)
        {
            return GetError(-1, CodeError);
        }

        /// <summary>
        /// Retorna o ErrorMsg correspondente conforme ErrorMessage ou o ID do Erro
        /// </summary>
        /// <param name="pErrorId">ID do Erro</param>
        /// <param name="pErrorCode">Código do Erro</param>
        /// <returns>ErrorMsg</returns>
        private static ErrorMsg GetError(int pErrorId, string pErrorCode)
        {
            if (pErrorId >= 0)
                return GetResourceError(pErrorId);
            if (pErrorCode != "")
                return GetResourceError(pErrorCode);
            return new ErrorMsg();
        }

        private static ErrorMsg GetResourceError(string ErrorId)
        {
            try
            {
                string TextXml = ThunderFire.Properties.Resources.deferrpt;
                using (var stream = new StringReader(TextXml))
                {
                    XDocument doc0 = XDocument.Load(stream);
                    XElement doc = XElement.Parse(doc0.ToString());
                    var query = (from item in doc.Elements("code")
                                 where (string)item.Attribute("id").Value == ErrorId
                                 select new
                                 {
                                     errorid = (int)item.Attribute("number"),
                                     errorcode = (string)item.Attribute("id"),
                                     message = (string)item.Element("message"),
                                     description = (string)item.Element("description"),
                                     severity = (string)item.Element("severity")
                                 }).SingleOrDefault();

                    if (query != null)
                    {
                        return new ErrorMsg(query.errorid, query.errorcode, query.severity, query.message, query.description, FieldName);
                    }
                }
            }
            catch (Exception Error)
            {
                Console.Write(Error.Message);
            }
            return new ErrorMsg();
        }

        private static ErrorMsg GetResourceError(int ErrorId)
        {
            try
            {
                string TextXml = ThunderFire.Properties.Resources.deferrpt;
                using (var stream = new StringReader(TextXml))
                {
                    XDocument doc0 = XDocument.Load(stream);
                    XElement doc = XElement.Parse(doc0.ToString());
                    var query = (from item in doc.Elements("code")
                                 where (string)item.Attribute("number").Value == ErrorId.ToString()
                                 select new
                                 {
                                     errorid = (int)item.Attribute("number"),
                                     errorcode = (string)item.Attribute("id"),
                                     message = (string)item.Element("message"),
                                     description = (string)item.Element("description"),
                                     severity = (string)item.Element("severity")
                                 }).SingleOrDefault();

                    if (query != null)
                    {
                        return new ErrorMsg(query.errorid, query.errorcode, query.severity, query.message, query.description, FieldName);
                    }
                }
            }
            catch { }
            return new ErrorMsg();
        }

        /// <summary>
        /// Grava um documento HTML com as definições de Erro existentes
        /// </summary>
        /// <param name="htmlfile">Nome do Arquivo</param>
        public static void WriteHtmlDocument(string htmlfile)
        {
            string TextXml = ThunderFire.Properties.Resources.deferrpt;
            string TextXslt = ThunderFire.Properties.Resources.lsterrpt;
            string result = "";
            StringBuilder sb = new StringBuilder();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(TextXml);
                XslCompiledTransform xslDoc = new XslCompiledTransform();
                XsltArgumentList Args = new XsltArgumentList();
                XsltSettings settings = new XsltSettings(false, true);
                settings.EnableDocumentFunction = true;
                xslDoc.Load(XmlReader.Create(new StringReader(TextXslt)), settings, new XmlUrlResolver());
                StringWriter stringWriter = new StringWriter(sb);
                xslDoc.Transform(doc, Args, stringWriter);
                stringWriter.Close();
                result = sb.ToString();
            }

            catch (XmlException Error)
            {
                ErrorObject = Error;
            }
            catch (DirectoryNotFoundException Error)
            {
                ErrorObject = Error;
            }
            catch (FileNotFoundException Error)
            {
                ErrorObject = Error;
            }
            catch (Exception Error)
            {
                ErrorObject = Error;
            }
            finally
            {
                sb = null;
            }
            if (result != "")
            {
                Files.RemoveFile(htmlfile);
                Files.WriteAllText(htmlfile, result);
            }
        }

        internal static string IFormatException(Exception erro)
        {
            string _result = "";
            if (erro != null)
            {
                _result = "Erro : " + erro.Message.ToString() + Environment.NewLine;
                _result += "Stack Trace : " + erro.StackTrace.ToString() + Environment.NewLine;
                _result += "Source :  " + erro.Source.ToString() + Environment.NewLine;
                if (erro.InnerException != null)
                    _result += "Inner Exception: " + erro.InnerException.ToString() + Environment.NewLine;
            }
            return _result;
        }
        /// <summary>
        /// Formata um objeto do tipo Exception Error
        /// </summary>
        /// <param name="Error">Exception</param>
        /// <returns>String</returns>
        public static string FormatException(Exception Error)
        {
            return IFormatException(Error);
        }
        /// <summary>
        /// Formata um objeto generico para o tipo Exception Error
        /// </summary>
        /// <param name="Error">Object</param>
        /// <returns>string</returns>
        public static string FormatException(object Error)
        {
            return IFormatException((Exception)Error);
        }
        /// <summary>
        /// Formata um objeto SQLException para o tipo Exception Error
        /// </summary>
        /// <param name="Error">SqlException</param>
        /// <returns>string</returns>
        public static string FormatException(SqlException Error)
        {
            return IFormatException(Error);
        }
    }
}