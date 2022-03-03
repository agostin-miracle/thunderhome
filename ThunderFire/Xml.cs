using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace ThunderFire
{
    /// <summary>
    /// Tratamento de Arquivos XML
    /// </summary>
    public static class Xml
    {
        /// <summary>
        /// Captura de Erros
        /// </summary>
        public static TrapError TrappedError = new TrapError();

        /// <summary>
        /// Transforma uma string XML com base em um arquivo de transformação XSLT 
        /// </summary>
        /// <param name="XmlString">String Xml</param>
        /// <param name="XsltFile">Arquivo Xslt</param>
        /// <returns>string transformada</returns>
        public static string Transform(string XmlString, string XsltFile)
        {
            StringBuilder sb = new StringBuilder();
            TrappedError.SetError();
            if (XmlString != "")
            {
                try
                {
                    XslCompiledTransform xslDoc = new XslCompiledTransform();
                    XsltSettings settings = new XsltSettings(false, true);
                    XsltArgumentList Args = new XsltArgumentList();
                    settings.EnableDocumentFunction = true;
                    xslDoc.Load(XsltFile, settings, new XmlUrlResolver());
                    StringWriter stringWriter = new StringWriter(sb);
                    XsltUtil obj = new XsltUtil();
                    Args.AddExtensionObject("urn:util", obj);
                    xslDoc.Transform(XmlReader.Create(new StringReader(XmlString)), Args, stringWriter);
                    stringWriter.Close();
                    return sb.ToString();
                }
                catch (Exception Error)
                {
                    string LastErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                    if (!String.IsNullOrWhiteSpace(LastErrorCode))
                        TrappedError.SetError(LastErrorCode);
                    else
                    {
                        TrappedError.ErrorCode = LastErrorCode;
                        TrappedError.ErrorMessage = Error.Message;
                        TrappedError.UserError = Error.Message;
                    }
                    TrappedError.ErrorObject = Error;
                }
            }
            return "";
        }

        /// <summary>
        /// Transforma uma string XML com base em um arquivo de transformação XSLT 
        /// </summary>
        /// <param name="XmlString">String Xml</param>
        /// <param name="XsltFile">Arquivo Xslt</param>
        /// <param name="Args">XsltArgumentList</param>
        /// <returns>string transformada</returns>
        public static string Transform(string XmlString, string XsltFile, XsltArgumentList Args)
        {
            StringBuilder sb = new StringBuilder();
            TrappedError.SetError();
            if (XmlString != "")
            {
                try
                {
                    XslCompiledTransform xslDoc = new XslCompiledTransform();
                    XsltSettings settings = new XsltSettings(false, true);
                    settings.EnableDocumentFunction = true;
                    xslDoc.Load(XsltFile, settings, new XmlUrlResolver());
                    StringWriter stringWriter = new StringWriter(sb);
                    XsltUtil obj = new XsltUtil();
                    Args.AddExtensionObject("urn:util", obj);
                    xslDoc.Transform(XmlReader.Create(new StringReader(XmlString)), Args, stringWriter);
                    stringWriter.Close();
                    return sb.ToString();
                }
                catch (Exception Error)
                {
                    string LastErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                    if (!String.IsNullOrWhiteSpace(LastErrorCode))
                        TrappedError.SetError(LastErrorCode);
                    else
                    {
                        TrappedError.ErrorCode = LastErrorCode;
                        TrappedError.ErrorMessage = Error.Message;
                        TrappedError.UserError = Error.Message;
                    }
                    TrappedError.ErrorObject = Error;
                }
            }
            return "";
        }

        /// <summary>
        /// Transforma uma string XML com base em uma string de transformação XSLT 
        /// </summary>
        /// <param name="XmlString">String Xml</param>
        /// <param name="XsltString">String Xslt</param>
        /// <returns>string transformada</returns>
        public static string RenderStrings(string XmlString, string XsltString)
        {
            StringBuilder sb = new StringBuilder();
            TrappedError.SetError();
            try
            {
                //string XsltString = Livre.Security.Properties.Resources.menus;
                XslCompiledTransform xslDoc = new XslCompiledTransform();
                XsltSettings settings = new XsltSettings(false, true);
                XsltArgumentList Args = new XsltArgumentList();
                settings.EnableDocumentFunction = true;
                xslDoc.Load(XmlReader.Create(new StringReader(XsltString)), settings, new XmlUrlResolver());
                StringWriter stringWriter = new StringWriter(sb);
                XsltUtil obj = new XsltUtil();
                Args.AddExtensionObject("urn:util", obj);
                xslDoc.Transform(XmlReader.Create(new StringReader(XmlString)), Args, stringWriter);
                stringWriter.Close();
                return sb.ToString();
            }
            catch (Exception Error)
            {
                string LastErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                if (!String.IsNullOrWhiteSpace(LastErrorCode))
                    TrappedError.SetError(LastErrorCode);
                else
                {
                    TrappedError.ErrorCode = LastErrorCode;
                    TrappedError.ErrorMessage = Error.Message;
                    TrappedError.UserError = Error.Message;
                }
                TrappedError.ErrorObject = Error;
            }
            return "";
        }

        /// <summary>
        /// Transforma uma string XML com base em um arquivo de transformação XSLT 
        /// </summary>
        /// <param name="XmlFile">String Xml</param>
        /// <param name="XsltFile">Arquivo Xslt</param>
        /// <param name="Args">XsltArgumentList</param>
        /// <returns>string transformada</returns>
        public static string TransformDocuments(string XmlFile, string XsltFile, XsltArgumentList Args)
        {
            StringBuilder sb = new StringBuilder();
            TrappedError.SetError();
            if (XmlFile != "")
            {
                try
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(XmlFile);
                    XslCompiledTransform xslDoc = new XslCompiledTransform();
                    XsltSettings settings = new XsltSettings(false, true);
                    settings.EnableDocumentFunction = true;
                    xslDoc.Load(XsltFile, settings, new XmlUrlResolver());
                    StringWriter stringWriter = new StringWriter(sb);
                    XsltUtil obj = new XsltUtil();
                    Args.AddExtensionObject("urn:util", obj);
                    xslDoc.Transform(xDoc, Args, stringWriter);
                    stringWriter.Close();
                    return sb.ToString();
                }
                catch (Exception Error)
                {
                    string LastErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                    if (!String.IsNullOrWhiteSpace(LastErrorCode))
                        TrappedError.SetError(LastErrorCode);
                    else
                    {
                        TrappedError.ErrorCode = LastErrorCode;
                        TrappedError.ErrorMessage = Error.Message;
                        TrappedError.UserError = Error.Message;
                    }
                    TrappedError.ErrorObject = Error;
                }
            }
            return "";
        }


        /// <summary>
        /// Converte um DataTable para uma String XML
        /// </summary>
        /// <param name="table">DataTable Object</param>
        /// <returns>string xml</returns>
        public static string DataTableToXml(DataTable table)
        {
            string s = "";
            if ((table != null) && (table.Rows.Count > 0))
            {
                System.IO.StringWriter writer = new System.IO.StringWriter();
                table.WriteXml(writer, XmlWriteMode.IgnoreSchema, true);
                s = writer.ToString().Replace("DocumentElement", "root");
            }
            return s;
        }
    }
}