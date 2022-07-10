using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire.Domain
{
    public class ErrorBase
    {

        /// <summary>
        /// Objeto de Retorno
        /// </summary>
        public object ReturnValue { get; set; }

        /// <summary>
        /// Retorna a última mensagem de alerta ou informação
        /// </summary>
        public string MessageToUser { get; set; } = string.Empty;

        /// <summary>
        /// Código do Erro
        /// </summary>
        public string ErrorCode { get; set; } = "OK";


        public object ErrorObject { get; set; } = null;

        public string ErrorMessage { get; set; } = string.Empty;


        public void SetError()
        {
            this.MessageToUser = string.Empty;
            this.ErrorCode = string.Empty;
            this.ErrorObject = null;
            this.ErrorMessage = "";
        }

        public void SetError(string errorcode, string message, string errormessage, object errorobject)
        {
            this.MessageToUser = message;
            this.ErrorCode = errorcode;
            this.ErrorObject = errorobject;
            this.ErrorMessage = errormessage;
        }

        public void SetError(string errorcode)
        {

            this.MessageToUser = ErrorManager.GetStringMsg(errorcode);
            this.ErrorCode = errorcode;
            this.ErrorObject = null;
            this.ErrorMessage = this.MessageToUser;
        }

        public void SetError(string errorcode, string ArgReplace)
        {
            string _message = ErrorManager.GetStringMsg(errorcode);
            this.MessageToUser = string.Format(_message,ArgReplace);
            this.ErrorCode = errorcode;
            this.ErrorObject = null;
            this.ErrorMessage = _message;
        }


    }
}
