using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Xsl;

namespace ThunderFire
{
    /// <summary>
    /// Classe Utilitário para geração de HTML customizado
    /// </summary>
    public class HtmlDictionary:MazeFireBase
    {
        private string _htmlbase = "<html><head><meta charset = \"UTF-8\" /><style>body {font-family: 'Arial Unicode MS',Tahoma;color:#000000;width:600px;</style></head><body>@BODY</body></html>";

        /// <summary>
        /// Define um HTML Body
        /// </summary>
        public string HtmlBase
        {
            get { return _htmlbase; }
            set { _htmlbase = value; }
        }
        /// <summary>
        /// Arquivo de Saida
        /// </summary>
        public string OutPutFile { get; set; }

        /// <summary>
        /// Folha Base de Estilos
        /// </summary>
        public string StyleBase { get; set; }
        /// <summary>
        /// Titulo
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// Meta Tag Http-Equiv
        /// </summary>
        public string[] HttpEquiv { get; set; } = new string[] { "content-type", "text/html; charset=utf-8" };


        /// <summary>
        /// Abre uma 
        /// </summary>
        /// <param name="poutput">Arquivo de Saida</param>
        /// <param name="pstyle">StyleSheet associado</param>
        /// <param name="ptitle">Título</param>
        public void Open(string poutput, string pstyle, string ptitle)
        {
            TrappedError.SetError();
            this.OutPutFile = poutput;
            this.StyleBase = pstyle;
            this.Title = ptitle;
            TrappedError.AddMessage("Start");
            TrappedError.AddMessage(String.Format("OutPutfile = {0}", this.OutPutFile));
            TrappedError.AddMessage(String.Format("StyleBase = {0}", this.StyleBase));
            TrappedError.AddMessage(String.Format("Title = {0}", this.Title));
            Open();
        }


        /// <summary>
        /// Lista de Tags 'Meta'
        /// </summary>
        public Dictionary<string, string> Meta { get; set; }

        /// <summary>
        /// Inicia o processo de montagem do Arquivo HTML
        /// </summary>
        /// <remarks>
        /// A Ação somente inicia se OutPutFile estiver definido
        /// </remarks>
        public void Open()
        {
            if (this.OutPutFile != "")
            {
                TrappedError.AddMessage(String.Format("OutPutfile {0} removido", this.OutPutFile));
                ThunderFire.Files.RemoveFile(this.OutPutFile);
                AppendFile(this.OutPutFile, "<html>");
                AppendFile(this.OutPutFile, "<head>");
                AppendFile(this.OutPutFile, "<title>" + this.Title + "</title>");
                Html.AppendMeta("name", this.HttpEquiv[0]);
                Html.AppendMeta("content", this.HttpEquiv[1]);
                AppendFile(this.OutPutFile, Html.CloseMeta());
                if (Meta != null)
                {
                    foreach (var pair in Meta)
                    {
                        AppendFile(this.OutPutFile, Html.SetMeta(pair.Key, pair.Value));
                    }
                }
                if (this.StyleBase != "")
                {
                    AppendFile(this.OutPutFile, "<style type=\"text/css\" media=\"all\">");
                    AppendFile(this.OutPutFile, ThunderFire.Files.ReadFile(this.StyleBase));
                    AppendFile(this.OutPutFile, "</style>");
                }
                AppendFile(this.OutPutFile, "</head>");
                AppendFile(this.OutPutFile, "<body>");
            }
        }

        /// <summary>
        /// Fecha e conclui a montagem do HTML
        /// </summary>
        public void Close()
        {
            AppendFile(this.OutPutFile, "</body></html>");
            TrappedError.AddMessage("Closed");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Args">XsltArgumentList Object</param>
        /// <param name="file">Arquivo XML Alvo</param>
        /// <param name="xsltfile">Arquivo de Transformação Xslt</param>
        public void AddItem(XsltArgumentList Args, string file, string xsltfile)
        {
            AppendFile(this.OutPutFile, Documents.Transform(file, xsltfile, Args).ToString());
            TrappedError.AddMessage(String.Format("Added Args = {0}", Args.ToString()));
            TrappedError.AddMessage(String.Format("Added Arquivo = {0}", file));
            TrappedError.AddMessage(String.Format("Added XsltFile = {0}", xsltfile));
            Args = null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Id de Seleção</param>
        /// <param name="file">Arquivo XML Alvo</param>
        /// <param name="xsltfile">Arquivo de Transformação Xslt</param>
        public void AddItem(string id, string file, string xsltfile)
        {
            AppendFile(this.OutPutFile,ThunderFire.Documents.Transform(file, xsltfile, id).ToString());
            TrappedError.AddMessage(String.Format("Added Id = {0}", id.ToString()));
            TrappedError.AddMessage(String.Format("Added Arquivo = {0}", file));
            TrappedError.AddMessage(String.Format("Added XsltFile = {0}", xsltfile));
        }


        private void AppendFile(string fileName, string Text)
        {
            using (StreamWriter sw = File.AppendText(fileName))
            {
                Text = Text.Replace("encoding=\"utf-16\"", "encoding=\"utf-8\"");
                Text = Text.Replace("xmlns=\"http://www.w3.org/1999/xhtml\"", "");
                Text = Text.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
                Text = Text.Replace("src=\"images/", "src=\"../images/");
                sw.Write(Text);
            }
        }

        /// <summary>
        /// Cria um arquivo HTML baseado em um Arquivo de Dados XML e um template de Transformação
        /// </summary>
        /// <param name="sourcefile">Arquivo Xml de Origem</param>
        /// <param name="templatefile">Arquivo XSLT de Transformação</param>
        /// <param name="outfile">Path de Gravação do Arquivo resultante</param>
        /// <returns>true, se gerou o arquivo</returns>
        public bool CreateHTML(string sourcefile, string templatefile, string outfile)
        {
            try
            {
                //string result = Xml.Transform(sourcefile, templatefile);
                string result =ThunderFire.Documents.Transform(sourcefile, templatefile);
                ThunderFire.Files.CreateText(outfile, result, true);
            }
            catch (Exception Error)
            {
                TrappedError.SetError(Error);
            }
            return File.Exists(outfile);
        }



        /// <summary>
        /// Cria um arquivo HTML baseado em um Arquivo de Dados XML e um template de Transformação
        /// </summary>
        /// <param name="sourcefile">Arquivo Xml de Origem</param>
        /// <param name="templatefile">Arquivo XSLT de Transformação</param>
        /// <param name="outfile">Path de Gravação do Arquivo resultante</param>
        /// <param name="styles">Style definido para o Arquivo de Saida</param>
        /// <remarks>
        /// <para>O Arquivo de Transformação deverá ter o marcador @STYLE@ para a substituição do conteúdo da variável 'styles'</para>
        /// </remarks>
        /// <returns>true, se o arquivo foi gerado</returns>
        public bool CreateHTML(string sourcefile, string templatefile, string outfile, string styles)
        {
            try
            {
                string result =ThunderFire.Documents.Transform(sourcefile, templatefile);
                if (result != "")
                    result = result.Replace("@STYLE@", styles);
                ThunderFire.Files.CreateText(outfile, result, true);
            }
            catch (Exception Error)
            {
                TrappedError.SetError(Error);
            }
            return File.Exists(outfile);
        }


        /// <summary>
        /// Altera o conteudo de HTMLBase substituindo p parâmetro 'param' pelo valor 'replacevalue'
        /// </summary>
        /// <param name="param">Chave ou Identificador de Valor</param>
        /// <param name="replacevalue">Conteúdo à ser substituido</param>
        /// <returns>String com o valor substituido</returns>
        public string ChangeHtml(string param, string replacevalue)
        {
            string result = HtmlBase;
            if (result != "")
            {
                try
                {
                    result = result.Replace(param, replacevalue);
                }
                catch (Exception Error)
                {
                    TrappedError.SetError(Error);
                    result = "";
                }
            }
            return result;
        }
    }
}