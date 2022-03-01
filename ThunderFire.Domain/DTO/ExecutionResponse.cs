using System;

namespace ThunderFire.Domain.DTO
{
    public class ExecutionResponse
    {
        /// <summary>
        /// Objeto de Retorno
        /// </summary>
        public object ReturnValue { get; set; }

        /// <summary>
        /// Retorna a última mensagem de alerta ou informação
        /// </summary>
        public string MessageToUser { get; set; } = "";

        /// <summary>
        /// HTTP Status Code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Indica se a atualizaçao foi logada no sistema
        /// </summary>
        public bool Logged { get; set; } = false;

        /// <summary>
        /// Origem do Erro
        /// </summary>
        public string SourceError { get; set; } = "";

        /// <summary>
        /// Código do Erro
        /// </summary>
        public string ErrorCode { get; set; } = "OK";
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
      

    }


}

