using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire
{
    /// <summary>
    /// Gerenciamento e Captura de Erros
    /// </summary>
    public class TrapError:ResponseMessage
    {
        /// <summary>
        /// Mensagens
        /// </summary>
        public StringBuilder Message { get; set; } = null;

        /// <summary>
        /// Origem do Erro
        /// </summary>
        public string SourceError { get; set; } = "";

        /// <summary>
        /// Método utilizado
        /// </summary>
        public string CurrentMethod { get; set; } = "";
        /// <summary>
        /// Caller Class 
        /// </summary>
        public string CurrentClassName { get; set; } = "";
        /// <summary>
        /// Código do Erro
        /// </summary>
        public string ErrorCode { get; set; } = "";
        /// <summary>
        /// Retorna a última mensagem de alerta ou informação
        /// </summary>
        public string UserError { get; set; } = "";
        /// <summary>
        /// Retorna o ultimo objeto Exception
        /// </summary>
        public object ErrorObject { get; set; } = null;
        /// <summary>
        /// Retorna a string do ultimo erro ocorrido
        /// </summary>
        public string ErrorMessage { get; set; } = "";
        /// <summary>
        /// Severidade do Erro
        /// </summary>
        public string Severity { get; set; } = "";

        /// <summary>
        /// Response Code (ISO-8583)
        /// </summary>
        public string ISO8583RC { get; set; } = "00";
        /// <summary>
        /// Retorna true se houve um erro de processamento
        /// </summary>
        /// <remarks>
        /// A definição de sucesso de um processamento se dá quando ErrorCode é igual a 'OK', 'COMPLETED' ou '200' 
        /// </remarks>
        /// <returns>bool</returns>
        public bool HasError()
        {
            if (!String.IsNullOrEmpty(this.ErrorCode))
            {
                if (this.ErrorCode == "OK" || this.ErrorCode == "COMPLETED" || this.ErrorCode == "200")
                    return false;
            }
            if (!String.IsNullOrEmpty(this.ErrorMessage))
                return true;
            if (!String.IsNullOrWhiteSpace(this.ErrorMessage))
                return true;
            if (this.ErrorObject != null)
                return true;
            return false;
        }

        /// <summary>
        /// Inicialização do objeto
        /// </summary>
        public TrapError()
        {
            StackTrace stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(1).GetMethod();
            CurrentClassName = method.DeclaringType.Name.ToString();
            stackTrace = null;
            if (this.Message == null)
                this.Message = new StringBuilder();
        }


        /// <summary>
        /// Limpa a Atual definição de Erro
        /// </summary>
        public void SetError()
        {
            this.SourceError = "";
            this.ErrorMessage = "";
            this.ErrorCode = "";
            this.ErrorObject = null;
            this.UserError = "";
            this.ISO8583RC = "00";
        }
        /// <summary>
        /// Define um erro com base no código de Erro
        /// </summary>
        /// <param name="errorcode">Código do Erro</param>
        public void SetError(string errorcode)
        {
            try
            {
                StackTrace stackTrace = new StackTrace();
                var method = stackTrace.GetFrame(1).GetMethod();
                CurrentMethod = method.Name;
                stackTrace = null;
            }
            catch { }

            this.ErrorCode = errorcode;
            if (this.ErrorCode != "")
            {
                var r = ErrorManager.GetError(this.ErrorCode);
                if (r != null)
                {
                    this.ErrorMessage = r.Message;
                    this.UserError = r.Message;
                    this.Severity = r.Severity;
                }
            }
            this.ErrorObject = null;
       }


        /// <summary>
        /// Define um erro com base no código de Erro
        /// </summary>
        /// <param name="errorcode">Código do Erro</param>
        /// <param name="objeto">TrapError</param> 
        public void SetError(string errorcode, ref TrapError objeto)
        {
            objeto.ErrorCode = errorcode;
            if (objeto.ErrorCode != "")
            {
                var r = ErrorManager.GetError(objeto.ErrorCode);
                if (r != null)
                {
                    objeto.ErrorMessage = r.Message;
                    objeto.UserError = r.Message;
                    objeto.Severity = r.Severity;
                }
            }
            objeto.ErrorObject = null;
        }

        /// <summary>
        /// Define um erro com base na mensagem de erro e ao objeto exception lançado
        /// </summary>
        /// <param name="message">Mensagem de Erro</param>
        /// <param name="error">Exception</param>
        public void SetError(string message, Exception error)
        {
            try
            {
                StackTrace stackTrace = new StackTrace();
                var method = stackTrace.GetFrame(1).GetMethod();
                CurrentMethod = method.Name;
                stackTrace = null;
            }
            catch { }

            this.ErrorMessage = message;
            this.ErrorObject = error;
            this.UserError = message;
            if (error != null)
                this.ErrorCode = ErrorManager.GetError(error.Message).ErrorCode;

            if (this.ErrorCode != "")
            {
                var r = ErrorManager.GetError(this.ErrorCode);
                if (r != null)
                {
                    this.UserError = r.Message;
                    this.Severity = r.Severity;
                }
            }

        }

        /// <summary>
        /// Define um erro com base na exception lançada
        /// </summary>
        /// <param name="error">Exception Error</param>
        public void SetError(Exception error)
        {
            try
            {
                StackTrace stackTrace = new StackTrace();
                var method = stackTrace.GetFrame(1).GetMethod();
                CurrentMethod = method.Name;
                stackTrace = null;
            }
            catch { }

            if (error != null)
                SetError(error.Message, error);
            else
                InternalDetectError();
        }

        /// <summary>
        /// Define e Localiza um erro
        /// </summary>
        /// <param name="errorcode">Código do Erro</param>
        /// <param name="errormessage">Mensagem de Erro</param>
        /// <param name="errorobject">Exception Lançada</param>
        public void SetError(string errorcode, string errormessage, Exception errorobject = null)
        {
            try
            {
                StackTrace stackTrace = new StackTrace();
                var method = stackTrace.GetFrame(1).GetMethod();
                CurrentMethod = method.Name;
                stackTrace = null;
            }
            catch { }

            this.ErrorCode = errorcode;
            this.ErrorObject = errorobject;
            this.ErrorMessage = errormessage;
            this.UserError = errormessage;
            if (this.ErrorCode != "")
            {
                var r = ErrorManager.GetError(this.ErrorCode);
                if (r != null)
                {
                    this.UserError = r.Message;
                    this.Severity = r.Severity;
                }
            }

        }
        /// <summary>
        /// Define um erro direto sem localização
        /// </summary>
        /// <param name="errorcode">Código do Erro</param>
        /// <param name="errormessage">Mensagem do Erro</param>
        /// <param name="usererror">Mensagem para o usuário</param>
        /// <param name="errorobject">Objeto do Erro</param>
        public void SetError(string errorcode, string errormessage, string usererror, Exception errorobject = null)
        {
            try
            {
                StackTrace stackTrace = new StackTrace();
                var method = stackTrace.GetFrame(1).GetMethod();
                CurrentMethod = method.Name;
                stackTrace = null;
            }
            catch { }

            this.ErrorCode = errorcode;
            this.UserError = usererror;
            this.ErrorObject = errorobject;
            this.ErrorMessage = errormessage;
        }

        private void InternalDetectError()
        {
            var r = ErrorManager.GetError("INTERNALDETECTERROR");
            if (r != null)
            {
                this.ErrorCode = r.ErrorCode;
                this.ErrorMessage = r.Message;
                this.UserError = r.Message;
                this.Severity = r.Severity;
            }
        }

        /// <summary>
        /// Adiciona as mensagens internas a Message
        /// </summary>
        public StringBuilder Dump()
        {
            StringBuilder _dump = new StringBuilder();
            _dump.AppendFormat("Class              = {0} ", this.CurrentClassName).AppendLine();
            _dump.AppendFormat("Method             = {0} ", this.CurrentMethod).AppendLine();
            _dump.AppendFormat("ErrorCode          = {0} ", this.ErrorCode).AppendLine();
            _dump.AppendFormat("User Message       = {0} ", this.UserError).AppendLine();
            _dump.AppendFormat("System Message     = {0} ", this.ErrorMessage).AppendLine();
            _dump.AppendFormat("Status Code        = {0} ", this.StatusCode).AppendLine();
            _dump.AppendFormat("Status Description = {0} ", this.StatusDescription).AppendLine();
            _dump.AppendFormat("Response Body      = {0} ", this.ResponseBody).AppendLine();
            _dump.AppendFormat("Response Code ISO  = {0} ", this.ISO8583RC).AppendLine();

            string[] _lines = Message.ToString().Split('\n');
            for (int i = 0; i < _lines.Length; i++)
            {
                _dump.AppendFormat("{0}", _lines[i].ToString()).AppendLine();
            }

            return _dump;
        }

        private bool PrintValid(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            if (string.IsNullOrWhiteSpace(s))
                return false;
            return true;
        }
        /// <summary>
        /// Adiciona um texto a Message
        /// </summary>
        /// <param name="text">Texto a ser adicionado</param>
        public void AddMessage(string text)
        {
            if (this.Message == null)
                this.Message = new StringBuilder();
            this.Message.Append(text).AppendLine(); 
        }
    }
}