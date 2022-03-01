using Dapper;
using System;
using System.Data;
using ThunderFire.Connector;
using ThunderFire.Domain.Models;

namespace ThunderFire.Business
{
    public class WriteAuditing
    {

        public static int Insert(Auditing model)
        {
            int RETURN_VALUE = 0;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@RETURN_VALUE", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@UPDUSU", model.UPDUSU, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@AUDDAT", model.AUDDAT, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                    p.Add("@AUDKEY", model.AUDKEY, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@AUDIDR", model.AUDIDR, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@AUDIMS", model.AUDIMS, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@AUDTSK", model.AUDTSK, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@AUDOBJ", model.AUDOBJ, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@AUDSRC", model.AUDSRC, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@AUDCHG", model.AUDCHG, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@NIDTOK", model.NIDTOK, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@NUMIPA", model.NUMIPA, dbType: DbType.String, direction: ParameterDirection.Input);
                    _conn.Execute("PRAUDDATINS", p, commandType: CommandType.StoredProcedure);
                    RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
                }
                catch (Exception Error)
                {
                }
                finally
                {
                }
            }
            return RETURN_VALUE;
        }

    }

}
