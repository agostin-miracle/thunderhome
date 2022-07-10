using Dapper;
using NLog;
using System;
using System.Data;
using ThunderFire.Connector;
using ThunderFire.Domain.DTO;

namespace ThunderFire.Business
{
    public class Support
    {

        private static Logger _logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Objeto de Retorno
        /// </summary>
        public static object ReturnValue { get; set; }

        /// <summary>
        /// Retorna a última mensagem de alerta ou informação
        /// </summary>
        public static string MessageToUser { get; set; } = "";

        /// <summary>
        /// Código do Erro
        /// </summary>
        public static string ErrorCode { get; set; } = "OK";


        /// <summary>
        /// rETORNA 1 SE O USUARIO TEM ACESSO A ROTINA
        /// </summary>
        /// <param name="pSYSFUN">id DA rOTINA</param>
        /// <param name="pCODUSU">cODIGO DO uSUÁRIO</param>
        /// <returns>BYTE</returns>
        public static byte GetUserAccess(long pSYSFUN, int pCODUSU)
        {
            byte RETURN_VALUE = 0;


            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    RETURN_VALUE = _conn.QuerySingle<byte>("SELECT dbo.GetUserAcess(@SYSFUN,@CODUSU)", new { SYSFUN = pSYSFUN, CODUSU = pCODUSU }, commandType: CommandType.Text);


                }
                catch { }

            }
            return RETURN_VALUE;
        }


        /// <summary>
        /// Verificar se existe o acesso de um usuário para uma rotina específica
        /// </summary>
        /// <param name="pSYSTBL">ID de identificação da tabela de aplicação da funcionalidade</param>
        /// <param name="pSYSROL">ID de identificação da regra de aplicação</param>
        /// <param name="pCODUSU">Código do Usuário</param>
        /// <returns>int</returns>
        public static int GetUserAccess(System.Int16 pSYSTBL, System.Int16 pSYSROL, System.Int32 pCODUSU)
        {
            int RETURN_VALUE = 0;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@RETURN_VALUE", 0, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                    p.Add("@SYSTBL", pSYSTBL, dbType: DbType.Int16, direction: ParameterDirection.Input);
                    p.Add("@SYSROL", pSYSROL, dbType: DbType.Int16, direction: ParameterDirection.Input);
                    p.Add("@CODUSU", pCODUSU, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    _conn.Execute(sql: "PRSYSGETUSRACE", param: p, commandType: CommandType.StoredProcedure, commandTimeout: 120);
                    RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
                }
                catch (Exception Error)
                {
                    _logger.Info(Error);
                }
            }
            return RETURN_VALUE;
        }


        /// <summary>
        ///  Verifica se o usuario tem acesso a uma determinada rotina
        /// </summary>
        /// <param name="pUPDUSU">Código do Usuário</param>
        /// <param name="pSYSTBL">ID de identificação da tabela de aplicação da funcionalidade</param>
        /// <param name="pSYSROL">ID de identificação da regra de aplicação</param>
        /// <returns>bool</returns>
        public static bool HasAccess(short pSYSTBL, short pSYSROL, int pUPDUSU)
        {
            return GetUserAccess(pSYSTBL, pSYSROL, pUPDUSU)==1;
        }


        public static bool IsAdministrator(int pCODUSU)
        {
            bool RETURN_VALUE = false;


            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string SQL = "SELECT SYSGRP FROM TBSYSUXG WHERE STAREC=1 AND CODUSU =@CODUSU";
                    int _SYSGRP = _conn.QuerySingle<int>(SQL, new { CODUSU = pCODUSU }, commandType: CommandType.Text);
                    return (_SYSGRP == 1);
                }
                catch { }

            }
            return RETURN_VALUE;
        }


        /// <summary>
        ///  Verifica o acesso de um usuário para execucao
        /// </summary>
        /// <param name="pUPDUSU">Código do Usuário de atualização</param>
        /// <param name="pSYSTBL">ID de identificação da tabela de aplicação da funcionalidade</param>
        /// <param name="pSYSROL">ID de identificação da regra de aplicação</param>
        /// <returns>int</returns>
        public static int CheckAccess(short pSYSTBL, short pSYSROL,int pUPDUSU)
        {
            string _ec = "";            // Error Code
            int _rsp = 0;               // Resposta
            if (pUPDUSU <= 0)
            {
                _rsp = -1;
                _ec = "REQUIREDUSEROPERATION";
            }

            if (!IsAdministrator(pUPDUSU))
            {
                if (_rsp == 0)
                {
                    _rsp = GetUserAccess(pSYSTBL, pSYSROL, pUPDUSU);

                    if (_rsp == 1)
                        _ec = "OK";


                    if (_rsp == 0)
                        _ec = "APPROVALEXENOTALLOWED";

                    if (_rsp >= 2)
                        _ec = "APRROVALEXEMULTIDEFS";
                }
            }
            else
            {
                _rsp = 1;
                _ec = "OK";
            }

            ErrorCode = _ec;
            MessageToUser = ErrorManager.GetStringMsg(_ec);
            ReturnValue = -100;
            return _rsp;
        }




        /// <summary>
        /// Encripta uma string
        /// </summary>
        /// <param name="value">Valor a ser encriptado</param>
        /// <returns>String</returns>
        public static string Encrypt(string value)
        {
            string RETURN_VALUE = "";
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@CODIFIED", "", dbType: DbType.String, direction: ParameterDirection.Output);
                    p.Add("@TEXT_VALUE", value, dbType: DbType.String, direction: ParameterDirection.Input);
                    _conn.Execute("SPK_CREATECODIFIEDKEY", p, commandType: CommandType.StoredProcedure);
                    RETURN_VALUE = p.Get<string>("@CODIFIED");
                }
                catch { }

            }
            return RETURN_VALUE;
        }
        /// <summary>
        /// Decripta uma string
        /// </summary>
        /// <param name="value">Valor a ser decripitado</param>
        /// <returns>String</returns>
        public static string Decrypt(string value)
        {
            string RETURN_VALUE = "";
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@DEODIFIED", "", dbType: DbType.String, direction: ParameterDirection.Output);
                    p.Add("@TEXT_VALUE", value, dbType: DbType.String, direction: ParameterDirection.Input);
                    _conn.Execute("SPK_CREATEDECODIFIEDKEY", p, commandType: CommandType.StoredProcedure);
                    RETURN_VALUE = p.Get<string>("@DECODIFIED");
                }
                catch { }

            }
            return RETURN_VALUE;
        }


    }
}
