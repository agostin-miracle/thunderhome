using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire
{
    /// <summary>
    /// Helper Class para verificação de Configuração
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Captura de Error
        /// </summary>
        public static TrapError TrappedError = new TrapError();

        /// <summary>
        /// Verifica se a Tag Name PADRAO DBACTIVECON existe 
        /// </summary>
        public static bool IsValidConnectionName()
        {
            return IsValidConnectionName("DBACTIVECON");
        }

        /// <summary>
        /// Verifica se a Tag Name de Conexão existe 
        /// </summary>
        /// <param name="connectionName">Tag Name de Configuração</param>
        /// <returns>bool</returns>
        public static bool IsValidConnectionName(string connectionName)
        {
            bool RETURN_VALUE = true;
            string cs = "";
            TrappedError.SetError();
            try
            {
                cs = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;

                if (cs == "")
                    RETURN_VALUE = false;
            }
            catch (Exception Error)
            {
                string ErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                if (String.IsNullOrWhiteSpace(ErrorCode))
                    ErrorCode = "BADDATASOURCENAME";
                TrappedError.SetError(ErrorCode);
                TrappedError.ErrorMessage = Error.Message;
                TrappedError.ErrorObject = Error;
                RETURN_VALUE = false;
            }
            return RETURN_VALUE;
        }

        /// <summary>
        /// Obtêm uma conexão de banco de dados
        /// </summary>
        /// <param name="connectionName">Tag Name</param>
        /// <returns>string</returns>
        public static string GetConnectionString(string connectionName)
        {
            string RETURN_VALUE = "";
            TrappedError.SetError();
            try
            {
                RETURN_VALUE = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            }
            catch (Exception Error)
            {
                string ErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                if (String.IsNullOrWhiteSpace(ErrorCode))
                    ErrorCode = "EMPTYCONNECTIONSTRING";
                TrappedError.SetError(ErrorCode);
                TrappedError.ErrorMessage = Error.Message;
                TrappedError.ErrorObject = Error;
                RETURN_VALUE = "";
            }
            return RETURN_VALUE;
        }

        /// <summary>
        /// Extrai o Catalogo da String de conexão
        /// </summary>
        /// <param name="connectionstring">string de conexao SQL SERVER</param>
        /// <returns>string</returns>
        public static string GetCatalog(string connectionstring)
        {
            string RETURN_VALUE = "";
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionstring);
                RETURN_VALUE = builder.InitialCatalog.Trim().ToUpper();
                builder = null;
            }
            catch (Exception Error)
            {
                string ErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                if(String.IsNullOrWhiteSpace(ErrorCode))
                    ErrorCode = "UNKNOWCATALOG";
                TrappedError.SetError(ErrorCode);
                TrappedError.ErrorMessage = Error.Message;
                TrappedError.ErrorObject = Error;
                RETURN_VALUE = "UNKNOW";
            }
            return RETURN_VALUE;
        }


        /// <summary>
        /// Obtem um valor de configuração de aplicativo
        /// </summary>
        /// <param name="key">Nome da configuração</param>
        /// <returns>string</returns>
        public static string GetAppValue(string key)
        {
            TrappedError.SetError();
            string RETURN_VALUE = "";
            try
            {
                RETURN_VALUE = ConfigurationManager.AppSettings[key].ToString();
            }
            catch (Exception Error)
            {
                string ErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                if (ErrorCode != "")
                    TrappedError.SetError(ErrorCode);
                else
                    TrappedError.UserError = "PARAMETRO CHAVE DE VALOR NAO DEFINIDO";
                TrappedError.ErrorMessage = Error.Message;
                TrappedError.ErrorObject = Error;
                RETURN_VALUE = "";
            }
            return RETURN_VALUE;
        }
        /// <summary>
        /// Obtem um valor de configuração de aplicativo
        /// </summary>
        /// <param name="key">Nome da configuração</param>
        /// <returns>string</returns>
        public static int GetAppValueInt(string key)
        {
            TrappedError.SetError();
            int RETURN_VALUE = 0;
            try
            {
                RETURN_VALUE = int.Parse(ConfigurationManager.AppSettings[key].ToString());
            }
            catch (Exception Error)
            {
                string ErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                if (ErrorCode != "")
                    TrappedError.SetError(ErrorCode);
                else
                    TrappedError.UserError = "PARAMETRO CHAVE DE VALOR NAO DEFINIDO";
                TrappedError.ErrorMessage = Error.Message;
                TrappedError.ErrorObject = Error;
                RETURN_VALUE = 0;
            }
            return RETURN_VALUE;
        }

        /// <summary>
        /// Obtem um valor de configuração de aplicativo
        /// </summary>
        /// <param name="key">Nome da configuração</param>
        /// <param name="defaultvalue">Valor assumido como padrao</param>
        /// <returns>string</returns>
        public static string GetAppValue(string key, string defaultvalue)
        {
            TrappedError.SetError();
            string RETURN_VALUE = "";
            try
            {
                RETURN_VALUE = ConfigurationManager.AppSettings[key].ToString();
                if(String.IsNullOrEmpty(RETURN_VALUE))
                    RETURN_VALUE = defaultvalue;
            }
            catch (Exception Error)
            {
                string ErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                if (ErrorCode != "")
                    TrappedError.SetError(ErrorCode);
                else
                    TrappedError.UserError = "PARAMETRO CHAVE DE VALOR NAO DEFINIDO";
                TrappedError.ErrorMessage = Error.Message;
                TrappedError.ErrorObject = Error;
            }
            return RETURN_VALUE;
        }
    }
}
