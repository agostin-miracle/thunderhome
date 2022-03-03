using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace ThunderFire
{
    /// <summary>
    /// Ação de Tratamento de Arquivos XML
    /// </summary>
    public enum DocumentAction
    {
        /// <summary>
        /// Nenhuma ação a ser executada
        /// </summary>
        None = 0,
        /// <summary>
        /// Ação de Remoção para definição de Utf-16
        /// </summary>
        RemoveUTF16 = 1
    }

    /// <summary>
    /// Tratativa de documentos
    /// </summary>
    public class Documents 
    {
        /// <summary>
        /// Captura de Erros
        /// </summary>
        public static TrapError TrappedError = new TrapError();
        /// <summary>
        /// Mensagem padrão de retorno
        /// </summary>
        public static string MessageDefault = "<p>Não foi possível renderizar as informações sobre o documento solicitado</p>";


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string AddToString(string source, string target)
        {
            source = source.Replace("-", "");
            source = source.Replace(":", "");

            target = target.Replace("-", "");
            target = target.Replace(":", "");

            source = source.ToLower().Trim();
            target = target.ToLower().Trim();

            string[] array = target.Split(',');
            string[] fonte = source.Split(',');
            ArrayList retorno = new ArrayList();
            bool _add = true;
            foreach (string t in fonte)
            {
                _add = true;
                if (retorno.Count >= 0)
                {
                    for (int k = 0; k < retorno.Count; k++)
                    {
                        if (retorno[k].ToString() == t)
                        {
                            _add = false;
                            break;
                        }
                    }
                }
                if (_add)
                {
                    if (t != "")
                    {
                        retorno.Add(t);
                    }
                }

            }


            for (int i = 0; i < array.Length; i++)
            {
                _add = true;
                if (retorno.Count >= 0)
                {
                    for (int k = 0; k < retorno.Count; k++)
                    {
                        if (retorno[k].ToString() == array[i].ToString())
                        {
                            _add = false;
                            break;
                        }
                    }
                }
                if (_add)
                {
                    if (array[i].ToString() != "")
                    {
                        retorno.Add(array[i]);
                    }
                }
            }

            source = "";
            foreach (string t in retorno)
            {
                source += t + ",";

            }
            return source.Substring(0, source.Length - 1);
        }

        //public static string GetGroup(string xmlfile, string id)
        //{
        //    string _retorno = "";
        //    try
        //    {
        //        XmlDocument doc = new XmlDocument();

        //        doc.Load(xmlfile);
        //        XmlNode node = doc.SelectSingleNode("//documentos/documento[@id='" + id.ToString() + "']");
        //        if (node != null)
        //            _retorno = node.SelectSingleNode("@group").Value.ToString().ToLower();
        //        node = null;
        //        doc = null;
        //    }
        //    catch
        //    { }

        //    return _retorno;
        //}

        private static bool FileExists(string filename, int types)
        {
            if (File.Exists(filename))
            {
                return true;
            }
            else
            {
                TrappedError.ErrorCode = "XMLERROR";
                if (types == 1)
                    TrappedError.UserError = "Arquivo de Dados não existente";
                if (types == 2)
                    TrappedError.UserError = "Arquivo de Transformação não existente";
            }
            return false;
        }

        private static void TratError(XmlException Error)
        {
            TrappedError.ErrorCode = "XMLERROR";
            TrappedError.UserError = "Erro de Tratamento de XML";
            TrappedError.ErrorMessage = Error.Message;
            TrappedError.ErrorObject = Error;
        }
        private static void TratError(Exception Error)
        {
            TrappedError.ErrorCode = "XMLERROR";
            TrappedError.UserError = "Erro não determinado de Transformação";
            TrappedError.ErrorMessage = Error.Message;
            TrappedError.ErrorObject = Error;
        }
        private static void TratError(System.Xml.Xsl.XsltException Error)
        {
            TrappedError.ErrorCode = "XMLERROR";
            TrappedError.UserError = "Erro interno do arquivo de Transformação";
            TrappedError.ErrorMessage = Error.Message;
            TrappedError.ErrorObject = Error;
        }
        private static void TratError(DirectoryNotFoundException Error)
        {
            TrappedError.ErrorCode = "XMLERROR";
            TrappedError.UserError = "Não pude localizar o Arquivo Alvo de Processamento";
            TrappedError.ErrorMessage = Error.Message;
            TrappedError.ErrorObject = Error;
        }

        /// <summary>
        /// Transforma um arquivo xml com base em uma folha de estilo existente
        /// </summary>
        /// <param name="xmlFile">Nome do Arquivo Xml</param>
        /// <param name="xslFile">Nome do Arquivo Xlst</param>
        /// <returns>string, arquivo transformado</returns>
        public static string Transform(string xmlFile, string xslFile)
        {
            StringBuilder sb = new StringBuilder();
            string _retorno = "";
            TrappedError.SetError();
            if (FileExists(xmlFile, 1))
            {
                if (FileExists(xslFile, 2))
                {
                    try
                    {
                        XslCompiledTransform xslDoc = new XslCompiledTransform();
                        XsltSettings settings = new XsltSettings(false, true);
                        XsltArgumentList Args = new XsltArgumentList();
                        settings.EnableDocumentFunction = true;
                        xslDoc.Load(xslFile, settings, new XmlUrlResolver());
                        StringWriter stringWriter = new StringWriter(sb);
                        XsltUtil obj = new XsltUtil();
                        Args.AddExtensionObject("urn:util", obj);
                        xslDoc.Transform(xmlFile, Args, stringWriter);
                        stringWriter.Close();
                        _retorno = sb.ToString();
                    }
                    catch (XmlException Error)
                    {
                        TratError(Error);
                        _retorno = MessageDefault;
                    }
                    catch (Exception Error)
                    {
                        TratError(Error);
                        _retorno = MessageDefault;
                    }
                }
                else
                {
                    _retorno = MessageDefault;
                }
            }
            else
            {
                _retorno = MessageDefault;

            }
            return _retorno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <param name="xslFile"></param>
        /// <param name="id"></param>
        /// <returns>IODocument</returns>
        public static string Transform(string xmlFile, string xslFile, string id)
        {
            StringBuilder sb = new StringBuilder();
            //GetDocumentParam(1, retorno, xmlFile, id);
            TrappedError.SetError();
            if (FileExists(xmlFile, 1))
            {
                if (FileExists(xslFile, 2))
                {
                    try
                    {
                        XslCompiledTransform xslDoc = new XslCompiledTransform();
                        XsltArgumentList Args = new XsltArgumentList();
                        XsltSettings settings = new XsltSettings(false, true);
                        settings.EnableDocumentFunction = true;
                        xslDoc.Load(xslFile, settings, new XmlUrlResolver());
                        StringWriter stringWriter = new StringWriter(sb);
                        XsltUtil obj = new XsltUtil();
                        Args.AddExtensionObject("urn:util", obj);
                        Args.AddParam("pid", "", id.ToString());
                        xslDoc.Transform(xmlFile, Args, stringWriter);
                        stringWriter.Close();
                        return sb.ToString();
                    }

                    catch (XmlException Error)
                    {
                        TratError(Error);
                    }
                    catch (Exception Error)
                    {
                        TratError(Error);
                    }
                }
            }
            return MessageDefault;
        }

        /// <summary>
        /// Executa a transformação de um documento xml 
        /// </summary>
        /// <param name="xmlFile">Arquivo XML</param>
        /// <param name="xslFile">Arquivo XSL/XSLT</param>
        /// <param name="Args">Argumentos de Pesquisa</param>
        /// <returns>string HTML</returns>
        public static string Transform(string xmlFile, string xslFile, XsltArgumentList Args)
        {
            StringBuilder sb = new StringBuilder();
            if (FileExists(xmlFile, 1))
            {
                if (FileExists(xslFile, 2))
                {
                    try
                    {
                        XslCompiledTransform xslDoc = new XslCompiledTransform();
                        XsltSettings settings = new XsltSettings(false, true);
                        settings.EnableDocumentFunction = true;
                        xslDoc.Load(xslFile, settings, new XmlUrlResolver());
                        StringWriter stringWriter = new StringWriter(sb);
                        XsltUtil obj = new XsltUtil();
                        Args.AddExtensionObject("urn:util", obj);
                        xslDoc.Transform(xmlFile, Args, stringWriter);
                        stringWriter.Close();
                        return sb.ToString();
                    }
                    catch (XmlException Error)
                    {
                        TratError(Error);
                    }
                    catch (System.Xml.Xsl.XsltException Error)
                    {
                        TratError(Error);

                    }
                    catch (Exception Error)
                    {
                        TratError(Error);
                    }
                }
            }
            return MessageDefault;
        }


        //public static IODocument GetDocumentWithPage(string xmlFile, string xslFile, string id)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    IODocument retorno = new IODocument();
        //    try
        //    {
        //        XslCompiledTransform xslDoc = new XslCompiledTransform();
        //        XsltArgumentList Args = new XsltArgumentList();
        //        XsltSettings settings = new XsltSettings(false, true);
        //        settings.EnableDocumentFunction = true;
        //        xslDoc.Load(xslFile, settings, new XmlUrlResolver());
        //        StringWriter stringWriter = new StringWriter(sb);
        //        XsltUtil obj = new XsltUtil();
        //        Args.AddExtensionObject("urn:util", obj);
        //        Args.AddParam("pid", "", id.ToString());
        //        xslDoc.Transform(xmlFile, Args, stringWriter);
        //        stringWriter.Close();
        //        retorno.Data = sb.ToString();
        //    }
        //    catch (XmlException Error)
        //    {
        //        TratError(Error);
        //        retorno.Data = MessageDefault;
        //    }
        //    catch (System.Xml.Xsl.XsltException Error)
        //    {
        //        TratError(Error);
        //        retorno.Data = MessageDefault;
        //    }
        //    catch (Exception Error)
        //    {
        //        TratError(Error);
        //        retorno.Data = MessageDefault;
        //    }

        //    finally
        //    {
        //        sb = null;
        //    }
        //    return retorno;
        //}

        //public static IODocument GetDocumentWithPage(string xmlFile, string xslFile, XsltArgumentList Args)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    IODocument retorno = new IODocument();
        //    try
        //    {
        //        XslCompiledTransform xslDoc = new XslCompiledTransform();
        //        XsltSettings settings = new XsltSettings(false, true);
        //        settings.EnableDocumentFunction = true;
        //        xslDoc.Load(xslFile, settings, new XmlUrlResolver());
        //        StringWriter stringWriter = new StringWriter(sb);
        //        xslDoc.Transform(xmlFile, Args, stringWriter);
        //        stringWriter.Close();
        //        retorno.Data = sb.ToString();
        //    }
        //    catch (XmlException Error)
        //    {
        //        TratError(Error);
        //        retorno.Data = MessageDefault;
        //    }
        //    catch (System.Xml.Xsl.XsltException Error)
        //    {
        //        TratError(Error);
        //        retorno.Data = MessageDefault;
        //    }
        //    catch (Exception Error)
        //    {
        //        TratError(Error);
        //        retorno.Data = MessageDefault;
        //    }
        //    finally
        //    {
        //        sb = null;
        //    }
        //    return retorno;
        //}

        ///// <summary>
        ///// Transforma um arquivo XML a partir de uma string de Transformação XSLT
        ///// </summary>
        ///// <param name="xmlFile">Nome do Arquivo XML</param>
        ///// <param name="XsltString">String no formato Xslt</param>
        ///// <param name="Args">XsltArgumentList</param>
        ///// <returns>IODocument</returns>
        //public static IODocument TransformXmlWithXsltString(string xmlFile, string XsltString, XsltArgumentList Args)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    IODocument retorno = new IODocument();
        //    if (FileExists(xmlFile, 1))
        //    {
        //        try
        //        {
        //            XslCompiledTransform xslDoc = new XslCompiledTransform();
        //            XsltSettings settings = new XsltSettings(false, true);
        //            settings.EnableDocumentFunction = true;
        //            xslDoc.Load(XmlReader.Create(new StringReader(XsltString)), settings, new XmlUrlResolver());
        //            StringWriter stringWriter = new StringWriter(sb);
        //            xslDoc.Transform(xmlFile, Args, stringWriter);
        //            stringWriter.Close();
        //            retorno.Data = sb.ToString();
        //        }
        //        catch (XmlException Error)
        //        {
        //            TratError(Error);
        //            retorno.Data = MessageDefault;
        //        }
        //        catch (System.Xml.Xsl.XsltException Error)
        //        {
        //            TratError(Error);
        //            retorno.Data = MessageDefault;
        //        }
        //        catch (DirectoryNotFoundException Error)
        //        {
        //            TratError(Error);
        //            retorno.Data = MessageDefault;
        //        }

        //        catch (Exception Error)
        //        {
        //            TratError(Error);
        //            retorno.Data = MessageDefault;
        //        }

        //        finally
        //        {
        //            sb = null;
        //        }
        //    }
        //    else

        //        retorno.Data = MessageDefault;
        //    return retorno;
        //}
        ///// <summary>
        ///// Transforma um arquivo XML a partir de uma string de Transformação XSLT
        ///// </summary>
        ///// <param name="xmlFile">Nome do Arquivo XML</param>
        ///// <param name="xsltstring">String no formato Xslt</param>
        ///// <param name="id">Id de Filtro</param>
        ///// <returns>IODocument</returns>
        //public static IODocument TransformXmlWithXsltString(string xmlFile, string xsltstring, string id)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    IODocument retorno = new IODocument();
        //    if (FileExists(xmlFile, 1))
        //    {
        //        try
        //        {
        //            XslCompiledTransform xslDoc = new XslCompiledTransform();
        //            XsltArgumentList Args = new XsltArgumentList();
        //            XsltSettings settings = new XsltSettings(false, true);
        //            settings.EnableDocumentFunction = true;
        //            xslDoc.Load(XmlReader.Create(new StringReader(xsltstring)), settings, new XmlUrlResolver());
        //            StringWriter stringWriter = new StringWriter(sb);
        //            XsltUtil obj = new XsltUtil();
        //            //Args.AddExtensionObject("urn:util", obj);
        //            Args.AddParam("field", "", id.ToString());
        //            xslDoc.Transform(xmlFile, Args, stringWriter);
        //            stringWriter.Close();
        //            retorno.Data = sb.ToString();
        //        }

        //        catch (XmlException Error)
        //        {
        //            TratError(Error);
        //            retorno.Data = MessageDefault;
        //        }
        //        catch (System.Xml.Xsl.XsltException Error)
        //        {
        //            TratError(Error);
        //            retorno.Data = MessageDefault;
        //        }
        //        catch (DirectoryNotFoundException Error)
        //        {
        //            TratError(Error);
        //            retorno.Data = MessageDefault;
        //        }

        //        catch (Exception Error)
        //        {
        //            TratError(Error);
        //            retorno.Data = MessageDefault;
        //        }

        //        finally
        //        {
        //            sb = null;
        //        }
        //    }
        //    else
        //        retorno.Data = MessageDefault;
        //    return retorno;
        //}


        ///// <summary>
        ///// Le os parametros de um documento XML
        ///// </summary>
        ///// <param name="mode">1 - Arquivo Físico, 2 - String XML</param>
        ///// <param name="document">Objeto IoDocument de contenção</param>
        ///// <param name="xmlFile">Arquivo ou String XML</param>
        ///// <param name="id">ID do Documento</param>
        //public static void GetDocumentParam(int mode, IODocument document, string xmlFile, string id)
        //{
        //    document.TrappedError.SetError();
        //    try
        //    {
        //        if ((mode == 1) || (mode == 2))
        //        {
        //            XmlDocument _doc = new XmlDocument();
        //            if (mode == 1)
        //                _doc.Load(xmlFile);
        //            if (mode == 2)
        //                _doc.LoadXml(xmlFile);

        //            XmlNode node = _doc.SelectSingleNode("//documentos/documento[@id='" + id.ToString() + "']");
        //            if (node != null)
        //            {
        //                if (node.SelectSingleNode("@title") != null)
        //                    document.Title = node.SelectSingleNode("@title").Value.ToString();
        //                if (node.SelectSingleNode("@UpdateData") != null)
        //                    document.UpdateData = node.SelectSingleNode("@UpdateData").Value.ToString();

        //            }
        //            node = null;
        //            _doc = null;
        //        }
        //    }

        //    catch (Exception Error)
        //    {
        //        document.TrappedError.ErrorCode = "XMLERROR";
        //        document.TrappedError.ErrorMessage = Error.Message;
        //        document.TrappedError.ErrorObject = Error;
        //        document.Data = "Não foi possível localizar informações sobre o documento solicitado";
        //    }
        //}
    }

}

//namespace ThunderFire
//{
//    public class Documents
//    {

//        /// <summary>
//        /// Código do Erro
//        /// </summary>
//        public static string ErrorCode { get; internal set; }

//        /// <summary>
//        /// Retorna a string do ultimo erro ocorrido
//        /// </summary>
//        public static string ErrorMessage{ get; internal set; }

//        public static string ErrorMessageForUser { get; internal set; }
//        /// <summary>
//        /// Retorna o ultimo objeto exception de Erro
//        /// </summary>
//        public static object ErrorObject { get; internal set; }

//        /// <summary>
//        /// Retorna true se ocorreu algum erro
//        /// </summary>
//        /// <returns>bool</returns>
//        public static bool HasError()
//        {
//            if (String.IsNullOrEmpty(ErrorMessage) || string.IsNullOrWhiteSpace(ErrorMessage))
//                return false;
//            return true;
//        }

//        private static void SetError()
//        {
//            ErrorMessageForUser = "";
//            ErrorMessage = "";
//            ErrorObject = null;
//            ErrorCode = "";
//        }

//        public static string Code(string password)
//        {
//            string retorno = "";
//            for (int i = 0; i < password.Length; i++)
//            {
//                char c = Convert.ToChar(password.Substring(i, 1));

//                if ((c >= 48 && c <= 57) || (c >= 65 && c <= 90) || (c >= 97 && c <= 122))
//                {
//                    char p = (char)(c + 48);
//                    retorno += Convert.ToString(p);
//                }

//            }
//            return retorno;
//        }


//        public static string Decode(string password)
//        {
//            string retorno = "";
//            for (int i = 0; i < password.Length; i++)
//            {
//                char c = Convert.ToChar(password.Substring(i, 1));
//                char p = (char)(c - 48);
//                retorno += Convert.ToString(p);
//            }
//            return retorno;
//        }



//        public static string TratarEnvioEmail(string s)
//        {
//            s = s.Replace("[", "<");
//            s = s.Replace("]", ">");
//            s = s.Replace("class=\"alinhartexto\"", "style=\"font-family:Tahoma,'Arial Unicode MS';line-height: normal; letter-spacing: normal; text-align: justify;text-indent:18px;font-size:14px;\"");
//            s = s.Replace("class=\"itemlista\"", "style=\"margin:0px 2px 2px 2px;LINE-HEIGHT: 14px; LETTER-SPACING: normal; text-align: justify;font-family: Tahoma, Verdana, Arial, Helvetica, sans-serif;	font-size:14px;\"");
//            s = s.Replace("class=\"titulo\"", "style=\"FONT-WEIGHT:bold;font-size:140%; COLOR:black; LINE-HEIGHT: 14px; FONT-FAMILY:'Lucida Sans Unicode',Verdana,Arial; text-align:left;margin-left:auto;\"");
//            s = s.Replace("class=\"titulopagina\"", "style=\"FONT-WEIGHT: bold; FONT-SIZE: 150%; LINE-HEIGHT: 19px; COLOR: #0e4ece; FONT-FAMILY: Tahoma,'Lucida Sans Unicode',Arial;text-align:center;\"");
//            s = s.Replace("class=\"subtitulo\"", "style=\"FONT-WEIGHT:bold;font-size:140%; COLOR:black; LINE-HEIGHT: 14px; FONT-FAMILY:'Lucida Sans Unicode',Verdana,Arial; text-align:left;margin-left:auto;\"");
//            s = s.Replace("class=\"subtitulo2\"", "style=\"FONT-WEIGHT: bold; FONT-SIZE: 120%; COLOR: black; FONT-FAMILY: Tahoma,'Lucida Sans Unicode',Verdana,Arial; text-align:left; margin-left:auto;text-indent:24px;\"");
//            s = s.Replace("class=\"alinharfala\"", "style=\"line-height: 13px; text-align: justify;margin:1px 0px 0px 0px;font-style:italic;font-weight:200;text-indent:18px;\"");
//            s = s.Replace("id=\"botao_de_retorno\"", "style=\"display:none;\"");
//            s = s.Replace("class=\"table_footer\"", "style=\"display:none;\"");
//            s = s.Replace("class=\"bloco_destaque\"", "style=\"COLOR: black;font-style:italic; background-color:#e0e0e0; letter-spacing: 1px; text-align:justify;\"");
//            s = s.Replace("class=\"alinharbloco\"", "style=\"color:black;line-height:12px;background-color:#ffffff; margin:2px 6px 2px 6px;padding:3px 3px 3px 3px;width:auto;border:solid 1px #660000;);\"");
//            return "<div style=\"vertical-align:top;text-align:left;background-color:#ffffff;\">" + s + "</div>";
//        }

//        public static string AddToString(string source, string target)
//        {
//            source = source.Replace("-", "");
//            source = source.Replace(":", "");

//            target = target.Replace("-", "");
//            target = target.Replace(":", "");

//            source = source.ToLower().Trim();
//            target = target.ToLower().Trim();

//            string[] array = target.Split(',');
//            string[] fonte = source.Split(',');
//            ArrayList retorno = new ArrayList();
//            bool _add = true;
//            foreach (string t in fonte)
//            {
//                _add = true;
//                if (retorno.Count >= 0)
//                {
//                    for (int k = 0; k < retorno.Count; k++)
//                    {
//                        if (retorno[k].ToString() == t)
//                        {
//                            _add = false;
//                            break;
//                        }
//                    }
//                }
//                if (_add)
//                {
//                    if (t != "")
//                    {
//                        retorno.Add(t);
//                    }
//                }

//            }


//            for (int i = 0; i < array.Length; i++)
//            {
//                _add = true;
//                if (retorno.Count >= 0)
//                {
//                    for (int k = 0; k < retorno.Count; k++)
//                    {
//                        if (retorno[k].ToString() == array[i].ToString())
//                        {
//                            _add = false;
//                            break;
//                        }
//                    }
//                }
//                if (_add)
//                {
//                    if (array[i].ToString() != "")
//                    {
//                        retorno.Add(array[i]);
//                    }
//                }
//            }

//            source = "";
//            foreach (string t in retorno)
//            {
//                source += t + ",";

//            }

//            return source.Substring(0, source.Length - 1);
//        }

//        public static string GetGroup(string xmlfile, string id)
//        {
//            string _retorno = "";
//            try
//            {
//                XmlDocument doc = new XmlDocument();

//                doc.Load(xmlfile);
//                XmlNode node = doc.SelectSingleNode("//documentos/documento[@id='" + id.ToString() + "']");
//                if (node != null)
//                    _retorno = node.SelectSingleNode("@group").Value.ToString().ToLower();
//                node = null;
//                doc = null;
//            }
//            catch
//            { }

//            return _retorno;
//        }

//        //public static string GetMenuDocument(string xmlFile, string xslFile, string id)
//        //{
//        //    StringBuilder sb = new StringBuilder();
//        //    string _retorno = "";
//        //    try
//        //    {
//        //        XslCompiledTransform xslDoc = new XslCompiledTransform();
//        //        XsltArgumentList Args = new XsltArgumentList();
//        //        XsltSettings settings = new XsltSettings(false, true);
//        //        settings.EnableDocumentFunction = true;
//        //        xslDoc.Load(xslFile, settings, new XmlUrlResolver());
//        //        StringWriter stringWriter = new StringWriter(sb);
//        //        Util obj = new Util();
//        //        Args.AddExtensionObject("urn:util", obj);
//        //        Args.AddParam("pid", "", id.ToString());
//        //        //Args.AddParam("pautorithy", "", autorithy);
//        //        xslDoc.Transform(xmlFile, Args, stringWriter);
//        //        stringWriter.Close();
//        //        _retorno = sb.ToString();
//        //    }

//        //    catch (XmlException Error)
//        //    {
//        //        LastError = Error.Message;
//        //        ErrorObject = Error;
//        //        _retorno = Error.Message;
//        //    }
//        //    catch (FileNotFoundException Error)
//        //    {
//        //        LastError = Error.Message;
//        //        ErrorObject = Error;
//        //        _retorno = "<p>Arquivo não encontrado</p>";
//        //    }
//        //    catch (Exception Error)
//        //    {
//        //        LastError = Error.Message;
//        //        ErrorObject = Error;
//        //        _retorno = "<p>Não foi possível localizar informações sobre o documento solicitado</p>";
//        //    }
//        //    return _retorno;
//        //}

//        //public static string GetMenuDocument(string xmlFile, string xslFile, string id, int autorithy)
//        //{
//        //    StringBuilder sb = new StringBuilder();
//        //    string _retorno = "";
//        //    try
//        //    {
//        //        XslCompiledTransform xslDoc = new XslCompiledTransform();
//        //        XsltArgumentList Args = new XsltArgumentList();
//        //        XsltSettings settings = new XsltSettings(false, true);
//        //        settings.EnableDocumentFunction = true;
//        //        xslDoc.Load(xslFile, settings, new XmlUrlResolver());
//        //        StringWriter stringWriter = new StringWriter(sb);
//        //        Util obj = new Util();
//        //        Args.AddExtensionObject("urn:util", obj);
//        //        Args.AddParam("pid", "", id.ToString());
//        //        Args.AddParam("pautorithy", "", autorithy);
//        //        xslDoc.Transform(xmlFile, Args, stringWriter);
//        //        stringWriter.Close();
//        //        _retorno = sb.ToString();
//        //    }

//        //    catch (XmlException Error)
//        //    {
//        //        LastError = Error.Message;
//        //        ErrorObject = Error;
//        //        _retorno = Error.Message;
//        //    }
//        //    catch (FileNotFoundException Error)
//        //    {
//        //        LastError = Error.Message;
//        //        ErrorObject = Error;
//        //        _retorno = "<p>Arquivo não encontrado</p>";
//        //    }
//        //    catch (Exception Error)
//        //    {
//        //        LastError = Error.Message;
//        //        ErrorObject = Error;
//        //        _retorno = "<p>Não foi possível localizar informações sobre o documento solicitado</p>";
//        //    }
//        //    return _retorno;
//        //}

//        public static string GetDocument(string xmlFile, string xslFile)
//        {
//            StringBuilder sb = new StringBuilder();
//            string _retorno = "";
//            //try
//            //{
//                XslCompiledTransform xslDoc = new XslCompiledTransform();
//                XsltSettings settings = new XsltSettings(false, true);
//                XsltArgumentList Args = new XsltArgumentList();
//                settings.EnableDocumentFunction = true;
//                xslDoc.Load(xslFile, settings, new XmlUrlResolver());
//                StringWriter stringWriter = new StringWriter(sb);
//                Util obj = new Util();
//                Args.AddExtensionObject("urn:util", obj);
//                xslDoc.Transform(xmlFile, Args, stringWriter);
//                stringWriter.Close();
//                _retorno = sb.ToString();
//            //}

//            //catch (XmlException Error)
//            //{
//            //    LastError = Error.Message;
//            //    ErrorObject = Error;
//            //    _retorno = Error.Message;
//            //}
//            //catch (FileNotFoundException Error)
//            //{
//            //    LastError = Error.Message;
//            //    ErrorObject = Error;
//            //    _retorno = "<p>Arquivo não encontrado</p>";
//            //}
//            //catch (Exception Error)
//            //{
//            //    LastError = Error.Message;
//            //    ErrorObject = Error;
//            //    _retorno = "<p>Não foi possível renderizar as informações sobre o documento solicitado</p>";
//            //}
//            return _retorno;
//        }


//        public static IODocument GetDocument(string xmlFile, string xslFile, string id)
//        {
//            StringBuilder sb = new StringBuilder();
//            IODocument retorno = new IODocument();
//            GetDocumentParam(1, retorno, xmlFile, id);
//            try
//            {
//                XslCompiledTransform xslDoc = new XslCompiledTransform();
//                XsltArgumentList Args = new XsltArgumentList();
//                XsltSettings settings = new XsltSettings(false, true);
//                settings.EnableDocumentFunction = true;
//                xslDoc.Load(xslFile, settings, new XmlUrlResolver());
//                StringWriter stringWriter = new StringWriter(sb);
//                Util obj = new Util();
//                Args.AddExtensionObject("urn:util", obj);
//                Args.AddParam("pid", "", id.ToString());
//                xslDoc.Transform(xmlFile, Args, stringWriter);
//                stringWriter.Close();
//                retorno.Data = sb.ToString();
//            }

//            catch (XmlException Error)
//            {
//                retorno.ErrorMessage = Error.Message;
//                retorno.ErrorObject = Error;
//            }
//            catch (FileNotFoundException Error)
//            {
//                retorno.ErrorMessage = Error.Message;
//                retorno.ErrorObject = Error;
//                if (Error.FileName.Contains(".xslt"))
//                    retorno.Data = "Arquivo de Transformação de Dados não encontrado";
//                else
//                    retorno.Data = "Arquivo de Dados não encontrado";
//            }
//            catch (Exception Error)
//            {
//                retorno.ErrorMessage = Error.Message;
//                retorno.ErrorObject = Error;
//                retorno.Data = Error.Message;
//            }
//            return retorno;
//        }

//        /// <summary>
//        /// Executa a transformação de um documento xml 
//        /// </summary>
//        /// <param name="xmlFile">Arquivo XML</param>
//        /// <param name="xslFile">Arquivo XSL/XSLT</param>
//        /// <param name="Args">Argumentos de Pesquisa</param>
//        /// <returns>string HTML</returns>
//        public static IODocument GetDocument(string xmlFile, string xslFile, XsltArgumentList Args)
//        {
//            StringBuilder sb = new StringBuilder();
//            IODocument retorno = new IODocument();
//            try
//            {
//                XslCompiledTransform xslDoc = new XslCompiledTransform();
//                XsltSettings settings = new XsltSettings(false, true);
//                settings.EnableDocumentFunction = true;
//                xslDoc.Load(xslFile, settings, new XmlUrlResolver());
//                StringWriter stringWriter = new StringWriter(sb);
//                Util obj = new Util();
//                Args.AddExtensionObject("urn:util", obj);
//                xslDoc.Transform(xmlFile, Args, stringWriter);
//                stringWriter.Close();
//                retorno.Data = sb.ToString();
//            }
//            catch (XmlException Error)
//            {
//                retorno.ErrorMessage = Error.Message;
//                retorno.ErrorObject = Error;
//            }
//            catch (FileNotFoundException Error)
//            {
//                retorno.ErrorMessage = Error.Message;
//                retorno.ErrorObject = Error;
//                if (Error.FileName.Contains(".xslt"))
//                    retorno.Data = "Arquivo de Transformação de Dados não encontrado";
//                else
//                    retorno.Data = "Arquivo de Dados não encontrado";
//            }
//            catch (System.Xml.Xsl.XsltException Error)
//            {
//                retorno.ErrorMessage = Error.Message;
//                retorno.ErrorObject = Error;

//                retorno.Data = "Arquivo de Transformação de Dados não encontrado";
//            }

//            catch (Exception Error)
//            {
//                retorno.ErrorMessage = Error.Message;
//                retorno.ErrorObject = Error;
//                retorno.Data = Error.Message;
//            }
//            return retorno;
//        }



//        public static IODocument GetDocumentWithPage(string xmlFile, string xslFile, string id)
//        {
//            StringBuilder sb = new StringBuilder();
//            IODocument retorno = new IODocument();
//            try
//            {
//                XslCompiledTransform xslDoc = new XslCompiledTransform();
//                XsltArgumentList Args = new XsltArgumentList();
//                XsltSettings settings = new XsltSettings(false, true);
//                settings.EnableDocumentFunction = true;
//                xslDoc.Load(xslFile, settings, new XmlUrlResolver());
//                StringWriter stringWriter = new StringWriter(sb);
//                Util obj = new Util();
//                Args.AddExtensionObject("urn:util", obj);
//                Args.AddParam("pid", "", id.ToString());
//                xslDoc.Transform(xmlFile, Args, stringWriter);
//                stringWriter.Close();
//                retorno.Data = sb.ToString();
//            }

//            catch (XmlException Error)
//            {
//                retorno.ErrorMessage = Error.Message;
//                retorno.ErrorObject = Error;
//            }
//            catch (FileNotFoundException Error)
//            {
//                retorno.ErrorMessage= Error.Message;
//                retorno.ErrorObject = Error;
//                retorno.Data = "Arquivo não encontrado";
//            }
//            catch (Exception Error)
//            {
//                retorno.ErrorMessage = Error.Message;
//                retorno.ErrorObject = Error;
//                retorno.Data = "Não foi possível localizar informações sobre o documento solicitado";
//            }
//            finally
//            {
//                sb = null;
//            }
//            return retorno;
//        }

//        public static IODocument GetDocumentWithPage(string xmlFile, string xslFile, XsltArgumentList Args)
//        {
//            StringBuilder sb = new StringBuilder();
//            IODocument retorno = new IODocument();
//            try
//            {
//                XslCompiledTransform xslDoc = new XslCompiledTransform();
//                XsltSettings settings = new XsltSettings(false, true);
//                settings.EnableDocumentFunction = true;
//                xslDoc.Load(xslFile, settings, new XmlUrlResolver());
//                StringWriter stringWriter = new StringWriter(sb);
//                xslDoc.Transform(xmlFile, Args, stringWriter);
//                stringWriter.Close();
//                retorno.Data = sb.ToString();
//            }
//            catch (XmlException Error)
//            {
//                retorno.ErrorMessage = Error.Message;
//                retorno.ErrorObject = Error;
//            }
//            catch (FileNotFoundException Error)
//            {
//                retorno.ErrorMessage = Error.Message;
//                retorno.ErrorObject = Error;
//                retorno.Data = "Arquivo não encontrado";
//            }
//            catch (Exception Error)
//            {
//                retorno.ErrorMessage = Error.Message;
//                retorno.ErrorObject = Error;
//                retorno.Data = "Não foi possível localizar informações sobre o documento solicitado";
//            }
//            finally
//            {
//                sb = null;
//            }
//            return retorno;
//        }

//        public static IODocument GetDocumentFromString(string xmlFile, string xsltstring, XsltArgumentList Args)
//        {
//            StringBuilder sb = new StringBuilder();
//            IODocument retorno = new IODocument();
//            try
//            {
//                XslCompiledTransform xslDoc = new XslCompiledTransform();
//                XsltSettings settings = new XsltSettings(false, true);
//                settings.EnableDocumentFunction = true;
//                //XmlTextReader xslfile = new XmlTextReader(xsltstring);
//                xslDoc.Load(XmlReader.Create(new StringReader(xsltstring)), settings, new XmlUrlResolver());

//                //xslDoc.Load(xslfile);
//                //xslDoc.Load(XmlReader.Create(new StringReader(xsltstring)), settings, new XmlUrlResolver());
//                StringWriter stringWriter = new StringWriter(sb);
//                //Util obj = new Util();
//                //Args.AddExtensionObject("urn:util", obj);
//                xslDoc.Transform(xmlFile, Args, stringWriter);
//                stringWriter.Close();
//                retorno.Data = sb.ToString();
//            }
//            catch (XmlException Error)
//            {
//                retorno.ErrorMessage = Error.Message;
//                retorno.ErrorObject = Error;
//            }

//            catch (DirectoryNotFoundException Error)
//            {
//                retorno.ErrorMessage = "Não pude localizar o Arquivo Alvo de Processamento";
//                retorno.ErrorObject = Error;
//                retorno.Data = "Arquivo não encontrado";
//            }
//            catch (FileNotFoundException Error)
//            {
//                retorno.ErrorMessage = "Não pude localizar o Arquivo Alvo de Processamento";
//                retorno.ErrorObject = Error;
//                retorno.Data = "Arquivo não encontrado";
//            }

//            catch (Exception Error)
//            {
//                retorno.ErrorMessage = Error.Message;
//                retorno.ErrorObject = Error;
//                retorno.Data = Error.Message;
//            }
//            finally
//            {
//                sb = null;
//            }
//            return retorno;
//        }

//        public static IODocument GetDocumentFromString(string xmlFile, string xsltstring, string id)
//        {
//            StringBuilder sb = new StringBuilder();
//            IODocument retorno = new IODocument();
//            try
//            {
//                XslCompiledTransform xslDoc = new XslCompiledTransform();
//                XsltArgumentList Args = new XsltArgumentList();
//                XsltSettings settings = new XsltSettings(false, true);
//                settings.EnableDocumentFunction = true;
//                xslDoc.Load(XmlReader.Create(new StringReader(xsltstring)), settings, new XmlUrlResolver());
//                StringWriter stringWriter = new StringWriter(sb);
//                Util obj = new Util();
//                //Args.AddExtensionObject("urn:util", obj);
//                Args.AddParam("field", "", id.ToString());
//                xslDoc.Transform(xmlFile, Args, stringWriter);
//                stringWriter.Close();
//                retorno.Data = sb.ToString();
//            }

//            catch (XmlException Error)
//            {
//                retorno.ErrorMessage = Error.Message;
//                retorno.ErrorObject = Error;
//            }
//            catch (DirectoryNotFoundException Error)
//            {
//                retorno.ErrorMessage = "Não pude localizar o Arquivo Alvo de Processamento";
//                retorno.ErrorObject = Error;
//                retorno.Data = "Arquivo não encontrado";
//            }
//            catch (FileNotFoundException Error)
//            {
//                retorno.ErrorMessage = "Não pude localizar o Arquivo Alvo de Processamento";
//                retorno.ErrorObject = Error;
//                retorno.Data = "Arquivo não encontrado";
//            }
//            catch (Exception Error)
//            {
//                retorno.ErrorMessage = Error.Message;
//                retorno.ErrorObject = Error;
//                retorno.Data = "Não foi possível localizar informações sobre o documento solicitado";
//            }
//            finally
//            {
//                sb = null;
//            }
//            return retorno;
//        }


//        /// <summary>
//        /// Le os parametros de um documento XML
//        /// </summary>
//        /// <param name="mode">1 - Arquivo Físico, 2 - String XML</param>
//        /// <param name="document">Objeto IoDocument de contenção</param>
//        /// <param name="xmlFile">Arquivo ou String XML</param>
//        /// <param name="id">ID do Documento</param>
//        public static void GetDocumentParam(int mode, IODocument document, string xmlFile, string id)
//        {
//            try
//            {
//                if ((mode == 1) || (mode == 2))
//                {
//                    XmlDocument _doc = new XmlDocument();
//                    if (mode == 1)
//                        _doc.Load(xmlFile);
//                    if (mode == 2)
//                        _doc.LoadXml(xmlFile);

//                    XmlNode node = _doc.SelectSingleNode("//documentos/documento[@id='" + id.ToString() + "']");
//                    if (node != null)
//                    {
//                        if (node.SelectSingleNode("@title") != null)
//                            document.Title = node.SelectSingleNode("@title").Value.ToString();
//                        if (node.SelectSingleNode("@UpdateData") != null)
//                            document.UpdateData = node.SelectSingleNode("@UpdateData").Value.ToString();

//                    }
//                    node = null;
//                    _doc = null;
//                }
//            }

//            catch (Exception Error)
//            {
//                document.ErrorMessage = Error.Message;
//                document.ErrorObject = Error;
//                document.Data = "Não foi possível localizar informações sobre o documento solicitado";
//            }
//        }
//    }
//}