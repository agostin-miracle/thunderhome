using Dapper;
using System;
using System.Data;
using ThunderFire.Connector;

namespace ThunderFire.Business
{
    public class Support
    {

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
                catch {}
             
            }
            return RETURN_VALUE;
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
