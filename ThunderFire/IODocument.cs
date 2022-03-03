using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire
{
    /// <summary>
    /// Classe de Tratamento de Documentos
    /// </summary>
    public class IODocument:MazeFireBase
    {
        /// <summary>
        /// Mensagem de retorno padrão em caso de dados vazios
        /// </summary>
        public string DEFAULTMESSAGERETURN = "Não foi possível localizar informações sobre o documento solicitado";

        private string _data = "";
        /// <summary>
        /// Dados obtidos
        /// </summary>
        public string Data
        {
            get
            {
                if (_data != "")
                    return _data;
                else
                    return DEFAULTMESSAGERETURN;
            }
            set
            {
                if (value != "")
                    _data = value;
                else
                {
                    _data = DEFAULTMESSAGERETURN;
                }
            }
        }

        /// <summary>
        /// Substitui ocorrencias de Texto
        /// </summary>
        /// <param name="source">Texto a ser substituido</param>
        /// <param name="target">Texto a substituir</param>
        public void ReplaceText(string source, string target)
        {
            _data = _data.Replace(source, target);
        }
        /// <summary>
        /// Retorna o Título do documento
        /// </summary>
        public string Title { get; set; } = "";

        /// <summary>
        /// Retorna a Data de Atualização do Documento
        /// </summary>
        public string UpdateData { get; set; }

        /// <summary>
        /// Inicializa o objeto de documentos
        /// </summary>
        public IODocument()
        {
            TrappedError.SetError();
            _data = "";
            this.Title = "";
            this.UpdateData = "";
        }

        /// <summary>
        /// Retorna s string XML do documento
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _data.ToString().Replace("xmlns:util=\"urn:util\"", "");
        }
        /// <summary>
        /// Grava o conteudo para um Documento
        /// </summary>
        /// <param name="pfilename">Nome do Arquivo a ser gravado</param>
        /// <param name="action">DocumentAction</param>
        public void Save(string pfilename, DocumentAction action)
        {
            TrappedError.SetError();
            if (action == DocumentAction.RemoveUTF16)
                ReplaceText("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            Files.CreateXmlFile(_data, pfilename);
            TrappedError = Files.TrappedError;
        }
    }
}
