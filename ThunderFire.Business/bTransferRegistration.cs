using System;
using System.Linq;
using ThunderFire;
using Dapper;
using NLog;
using System.Data;
using  System.Reflection;
using System.Collections.Generic;
using ThunderFire.Connector;
using ThunderFire.Domain.Models;
using  ThunderFire.Domain.DTO;
using ThunderFire.Interface;


namespace ThunderFire.Business
{

///<summary>
/// Regra de Negócio para TBREGMOV
///</summary>
///<remarks>
///Empresa     : Agostinho da Silva Milagres
///Copyright   : Copyright © 2012
///Descricao   : Gerador de Objetos
///Produto     : SQLDBTools
///Titulo      : SQLDBTools
///Version     : 1.3.0.0
///Data        : 22/04/2022 19:16
///Alias       : transferregistration
///Descrição   : Tabela de Registro de Transferências
///</remarks>
    public partial class TransferRegistrationDao : BusinessBase, ITransferRegistration
    {
private static Logger _logger = LogManager.GetCurrentClassLogger();
/// <summary>
/// Instância Base
/// </summary>
public TransferRegistrationDao()
{
this.Connected=true;
this.KeyTableId =42;

}
       
    /// <summary>
    /// Insere um registro na tabela TBREGMOV (Tabela de Registro de Transferências)
    /// </summary>
    ///<param name="model">TransferRegistration</param>
    /// <returns>int</returns>
        public ExecutionResponse Insert(TransferRegistration model)
        {
            ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            this.HasError =false;
            this.ProcessCode= 10;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
string _changed = Objects.GetPropertiesValue("Tabela de Registro de Transferências",model,true);

            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@SUBSYS", model.SUBSYS, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@TIPLCT", model.TIPLCT, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@STAMOV", model.STAMOV, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@DATMOV", model.DATMOV, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@CODTRM", model.CODTRM, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@CODMOE", model.CODMOE, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@USUPRO", model.USUPRO, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODUSU", model.CODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODCRT", model.CODCRT, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODRSP", model.CODRSP, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@USRDEB", model.USRDEB, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@USRCRD", model.USRCRD, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@USUDEB", model.USUDEB, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@INDDEB", model.INDDEB, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@ORGDEB", model.ORGDEB, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@CTADEB", model.CTADEB, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@USUCRD", model.USUCRD, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@INDCRD", model.INDCRD, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@ORGCRD", model.ORGCRD, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@CTACRD", model.CTACRD, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@VLRMOV", model.VLRMOV, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@OMTSLD", model.OMTSLD, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@OMTTAR", model.OMTTAR, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NIDCAL", model.NIDCAL, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@VLRTAR", model.VLRTAR, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@DSCERR", model.DSCERR, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@SLDDAT", model.SLDDAT, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@SLDVAL", model.SLDVAL, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@NIDTRA", model.NIDTRA, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@CANTRA", model.CANTRA, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@LOTFIN", model.LOTFIN, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRREGMOVINS", p,commandType: CommandType.StoredProcedure);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
respond.ReturnValue = RETURN_VALUE;
string _errormessage="";
                if(RETURN_VALUE>0)
                {
                    respond.MessageToUser ="REGISTRO INCLUIDO COM SUCESSO";
_errormessage="";
                }
                if(RETURN_VALUE==-1)
                {
                    respond.ErrorCode="FAILALL";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser ="REGISTRO INCLUIDO POREM NAO PROCESSADO";
_errormessage="";
                }
            }
            catch (Exception Error)
            {
            _logger.Info(Error);
            this.HasError =true;
            respond.ReturnValue=RETURN_VALUE;
            respond.StatusCode=400;
            respond.MessageToUser="FALHA NA INCLUSAO DO REGISTRO";
            respond.ErrorMessage=Error.Message;
            var msg = ErrorManager.GetError(ErrorManager.GetErrorCode(Error.Message));
            if(msg!=null)
            {
            respond.SourceError=msg.Source;
            respond.ErrorCode=msg.ErrorCode;
            respond.ErrorObject=Error;
            if(!String.IsNullOrEmpty(msg.Message))
            respond.MessageToUser = msg.Message;
            respond.Severity=msg.Severity;
            }
            }
            }
        return respond;
        }
    /// <summary>
    /// Obtêm o registro de transferência com base no id informado
    /// </summary>
        /// <param name="pNIDHOL">ID do Feriado</param>
    /// <returns>TransferRegistration</returns>
    public TransferRegistration Select(System.Int32 pNIDHOL)
        {
        this.Found=false;
            this.ProcessCode= 100;
    TransferRegistration RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
string _sql = @"SELECT * FROM TBREGMOV (NOLOCK) A WHERE (A.NIDHOL=@NIDHOL)";
 RETURN_VALUE = _conn.Query<TransferRegistration>(sql:_sql, param:new {NIDHOL=pNIDHOL
},  commandType: CommandType.Text, commandTimeout: 120).FirstOrDefault();
                    if(RETURN_VALUE!=null)
                    this.Found = true;
                    }
                    catch (Exception Error)
                    {
                    this.HasError = true;
                    this.Found=false;
                    _logger.Info(Error);
            }
            }
                    return RETURN_VALUE;
            }

    }
}
