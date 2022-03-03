namespace ThunderFire
{
    /// <summary>
    /// Response Message Object
    /// </summary>
    public class ResponseMessage
    {
        /// <summary>
        /// Código do Status de Retorno
        /// </summary>
        public int StatusCode { get;  set; }
        /// <summary>
        /// Descrição do Status
        /// </summary>
        public string StatusDescription { get;  set; }
        /// <summary>
        /// Response Body
        /// </summary>
        public string ResponseBody { get;  set; }
    }
}
