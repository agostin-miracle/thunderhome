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
/// Regra de Negócio para TBTIPLCT
///</summary>
///<remarks>
///Empresa     : Agostinho da Silva Milagres
///Copyright   : Copyright © 2012
///Descricao   : Gerador de Objetos
///Produto     : SQLDBTools
///Titulo      : SQLDBTools
///Version     : 1.3.0.0
///Data        : 22/04/2022 18:33
///Alias       : accountentrytype
///Descrição   : Account Entry Type
///</remarks>
    public partial class AccountEntryTypeDao : BusinessBase, IAccountEntryType
    {
private static Logger _logger = LogManager.GetCurrentClassLogger();
/// <summary>
/// Instância Base
/// </summary>
public AccountEntryTypeDao()
{
this.Connected=true;
this.KeyTableId =40;

}
       
    /// <summary>
    /// Insere um registro na tabela TBTIPLCT (Account Entry Type)
    /// </summary>
    ///<param name="model">AccountEntryType</param>
    /// <returns>int</returns>
        public ExecutionResponse Insert(AccountEntryType model)
        {
            ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            this.HasError =false;
            this.ProcessCode= 10;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
string _changed = Objects.GetPropertiesValue("Account Entry Type",model,true);

            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@TIPLCT", model.TIPLCT, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@DSCLCT", model.DSCLCT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@INDLCT", model.INDLCT, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@INDDEB", model.INDDEB, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@ORGDEB", model.ORGDEB, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@INDCRD", model.INDCRD, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@ORGCRD", model.ORGCRD, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@CODTAR", model.CODTAR, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@USETRF", model.USETRF, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRTIPLCTINS", p,commandType: CommandType.StoredProcedure);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
respond.ReturnValue = RETURN_VALUE;
string _errormessage="";
                if(RETURN_VALUE>0)
                {
                    respond.MessageToUser ="TIPO DE LANÇAMENTO INCLUIDO COM SUCESSO";
_errormessage="";
                }
                if(RETURN_VALUE==-1)
                {
                    respond.ErrorCode="FAILALL";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==-2)
                {
                    respond.ErrorCode="RECORDFOUND";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==-3)
                {
                    respond.ErrorCode="ATTRIBUTEFOUNDED";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser ="O NOME FORNECIDO COMO ARGUMENTO JÁ ESTÁ CADASTRADO";
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
    /// Altera um registro da tabela TBTIPLCT (Account Entry Type)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">AccountEntryType</param>
    /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(AccountEntryType model)
        {
ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode= 10;
            this.HasError =false;
                            AccountEntryType ModelAud = Select(model.TIPLCT);
string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Account Entry Type",model,true);

                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@TIPLCT", model.TIPLCT, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@DSCLCT", model.DSCLCT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@INDLCT", model.INDLCT, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@INDDEB", model.INDDEB, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@ORGDEB", model.ORGDEB, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@INDCRD", model.INDCRD, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@ORGCRD", model.ORGCRD, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@CODTAR", model.CODTAR, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@USETRF", model.USETRF, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATUPD", model.DATUPD, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRTIPLCTUPD", p,commandType: CommandType.StoredProcedure);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
                respond.ReturnValue= RETURN_VALUE;
string _errormessage="";
                if(RETURN_VALUE>0)
                {
                    Auditing audit = new Auditing();
          audit.UPDUSU =model.UPDUSU;
          audit.AUDDAT=DateTime.Now;
          audit.AUDKEY =KeyTableId;
          audit.AUDIDR =RETURN_VALUE;
          audit.AUDIMS =0;
          audit.AUDTSK =this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
          audit.AUDOBJ = "PRTIPLCTUPD";
          audit.AUDSRC =_original;
          audit.AUDCHG =_changed;
          audit.NIDTOK =0;
          audit.NUMIPA =Environment.MachineName;
          _AUDNUM=WriteAuditing.Insert(audit);
          respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="TIPO DE LANCAMENTO ALTERADO COM SUCESSO";
_errormessage="";
                }
                if(RETURN_VALUE==-1)
                {
                    respond.ErrorCode="FAILALL";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==-2)
                {
                    respond.ErrorCode="RECORDNOTFOUND";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==-3)
                {
                    respond.ErrorCode="ATTRIBUTEFOUNDED";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser ="O NOME FORNECIDO COMO ARGUMENTO JÁ ESTÁ CADASTRADO";
_errormessage="";
                }
            }
            catch (Exception Error)
            {
            _logger.Error(Error);
            this.HasError =true;
            respond.ReturnValue=RETURN_VALUE;
            respond.StatusCode=400;
            respond.MessageToUser="FALHA NA ALTERAÇÃO DO REGISTRO";
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
    /// Obtêm o registro de Tipo de Lançamento com base no id informado
    /// </summary>
        /// <param name="pTIPLCT">Tipo de Lançamento</param>
    /// <returns>AccountEntryType</returns>
    public AccountEntryType Select(System.Int16 pTIPLCT)
        {
        this.Found=false;
            this.ProcessCode= 100;
    AccountEntryType RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
string _sql = @"SELECT * FROM TBTIPLCT (NOLOCK) A WHERE (A.TIPLCT=@TIPLCT)";
 RETURN_VALUE = _conn.Query<AccountEntryType>(sql:_sql, param:new {TIPLCT=pTIPLCT
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
