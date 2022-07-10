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
/// Regra de Negócio para TBREGMEN
///</summary>
///<remarks>
///Empresa     : Agostinho da Silva Milagres
///Copyright   : Copyright © 2012
///Descricao   : Gerador de Objetos
///Produto     : SQLDBTools
///Titulo      : SQLDBTools
///Version     : 1.3.0.0
///Data        : 29/03/2022 14:17
///Alias       : monthlypayment
///Descrição   : Monthly Payment
///</remarks>
    public partial class MonthlyPaymentDao : BusinessBase, IMonthlyPayment
    {
private static Logger _logger = LogManager.GetCurrentClassLogger();
/// <summary>
/// Instância Base
/// </summary>
public MonthlyPaymentDao()
{
this.Connected=true;
this.KeyTableId =24;

}
       
    /// <summary>
    /// Insere um registro na tabela TBREGMEN (Monthly Payment)
    /// </summary>
    ///<param name="model">MonthlyPayment</param>
    /// <returns>int</returns>
        public ExecutionResponse Insert(MonthlyPayment model)
        {
            ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM =0;
            this.HasError =false;
            this.ProcessCode= 10;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
string _changed = Objects.GetPropertiesValue("Monthly Payment",model,true);

            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@SUBSYS", model.SUBSYS, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@USUPRO", model.USUPRO, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODUSU", model.CODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@USRREF", model.USRREF, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@TIPMEN", model.TIPMEN, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@CODREF", model.CODREF, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@MODREG", model.MODREG, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@REGBXA", model.REGBXA, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@STAMEN", model.STAMEN, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@SRCCAN", model.SRCCAN, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@NUMMES", model.NUMMES, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@NUMANO", model.NUMANO, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@NUMPCL", model.NUMPCL, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@DATMEN", model.DATMEN, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@DATVCT", model.DATVCT, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@SIGOPE", model.SIGOPE, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@VLRCOB", model.VLRCOB, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@LOTINS", model.LOTINS, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NUMTEN", model.NUMTEN, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRREGMENINS", p,commandType: CommandType.StoredProcedure);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
respond.ReturnValue = RETURN_VALUE;
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
          audit.AUDOBJ = "PRREGMENINS";
          audit.AUDSRC =_changed;
          audit.AUDCHG ="";
          audit.NIDTOK =0;
          audit.NUMIPA =Environment.MachineName;
          _AUDNUM=WriteAuditing.Insert(audit);
          respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="REGISTRO DE MENSALIDADE INCLUIDO COM SUCESSO";
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
    /// Altera um registro da tabela TBREGMEN (Monthly Payment)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">MonthlyPayment</param>
    /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(MonthlyPayment model)
        {
ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode= 10;
            this.HasError =false;
                            MonthlyPayment ModelAud = Select(model.NIDMEN);
string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Monthly Payment",model,true);

                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@NIDMEN", model.NIDMEN, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@SUBSYS", model.SUBSYS, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@USUPRO", model.USUPRO, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODUSU", model.CODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@USRREF", model.USRREF, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@TIPMEN", model.TIPMEN, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@CODREF", model.CODREF, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@MODREG", model.MODREG, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@REGBXA", model.REGBXA, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@STAMEN", model.STAMEN, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@SRCCAN", model.SRCCAN, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@NUMMES", model.NUMMES, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@NUMANO", model.NUMANO, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@NUMPCL", model.NUMPCL, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@DATMEN", model.DATMEN, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@DATVCT", model.DATVCT, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@SIGOPE", model.SIGOPE, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@VLRCOB", model.VLRCOB, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@LOTINS", model.LOTINS, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NUMTEN", model.NUMTEN, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATUPD", model.DATUPD, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRREGMENUPD", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRREGMENINS";
          audit.AUDSRC =_changed;
          audit.AUDCHG ="";
          audit.NIDTOK =0;
          audit.NUMIPA =Environment.MachineName;
          _AUDNUM=WriteAuditing.Insert(audit);
          respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="REGISTRO DE MENSALIDADE ALTERADO COM SUCESSO";
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
    /// Obtêm o registro de mensalidade de acordo com o ID
    /// </summary>
        /// <param name="pNIDMEN">ID do Registro de Mensalidade</param>
    /// <returns>MonthlyPayment</returns>
    public MonthlyPayment Select(int pNIDMEN)
        {
        this.Found=false;
            this.ProcessCode= 0;
    MonthlyPayment RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
string _sql = @"SELECT * FROM TBREGMEN (nolock) A WHERE (A.NIDMEN=@NIDMEN)";
 RETURN_VALUE = _conn.Query<MonthlyPayment>(sql:_sql, param:new {NIDMEN=pNIDMEN
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

    /// <summary>
    /// Localiza um registro de mensalidae conforme os parâmetros abaixo
    /// </summary>
        /// <param name="pTIPMEN">Tipo de Mensalidade</param>
    /// <param name="pCODREF">Código de Referência</param>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pREGBXA">Indicador de Baixa</param>
    /// <returns>List of MonthlyPayment</returns>
    public List<MonthlyPayment> Select(System.Int32 pTIPMEN, System.Int32 pCODREF, System.Int32 pCODUSU, System.Byte pREGBXA= 0)
        {
        this.Found=false;
            this.ProcessCode= 0;
    List<MonthlyPayment> RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE = _conn.Query<MonthlyPayment>(sql:"PRREGMENSELMEN", param:new {TIPMEN=pTIPMEN,
CODREF=pCODREF,
CODUSU=pCODUSU,
REGBXA=pREGBXA
},  commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

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
