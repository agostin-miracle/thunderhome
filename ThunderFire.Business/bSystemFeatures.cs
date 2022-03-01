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
/// Regra de Negócio para TBSYSFUN
///</summary>
///<remarks>
///Empresa     : Agostinho da Silva Milagres
///Copyright   : Copyright © 2012
///Descricao   : Gerador de Objetos
///Produto     : SQLDBTools
///Titulo      : SQLDBTools
///Version     : 1.3.0.0
///Data        : 13/02/2022 12:59
///Alias       : systemfeatures
///Descrição   : Funcionalidades do Sistema
///</remarks>
    public partial class SystemFeaturesDao : BusinessBase, ISystemFeatures
    {
private static Logger _logger = LogManager.GetCurrentClassLogger();
/// <summary>
/// Instância Base
/// </summary>
public SystemFeaturesDao()
{
this.Connected=true;
this.KeyTableId =13;

}
       
    /// <summary>
    /// Insere um registro na tabela TBSYSFUN (Funcionalidades do Sistema)
    /// </summary>
    ///<param name="model">SystemFeatures</param>
    /// <returns>int</returns>
        public ExecutionResponse Insert(SystemFeatures model)
        {
            ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM =0;
            this.HasError =false;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
string _changed = Objects.GetPropertiesValue("Funcionalidades do Sistema",model,true);

            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@SYSFUN", model.SYSFUN, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@SYSPRF", model.SYSPRF, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@SYSKEY", model.SYSKEY, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@SYSMTH", model.SYSMTH, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@SYSPRC", model.SYSPRC, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@SYSDSC", model.SYSDSC, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRSYSFUNINS", p,commandType: CommandType.StoredProcedure);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
respond.ReturnValue = RETURN_VALUE;
                if(RETURN_VALUE>0)
                {
                    Auditing audit = new Auditing();
          audit.UPDUSU =model.UPDUSU;
          audit.AUDDAT=DateTime.Now;
          audit.AUDKEY =KeyTableId;
          audit.AUDIDR =RETURN_VALUE;
          audit.AUDIMS =0;
          audit.AUDTSK =this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
          audit.AUDOBJ = "PRSYSFUNINS";
          audit.AUDSRC ="";
          audit.AUDCHG =_changed;
          audit.NIDTOK =0;
          audit.NUMIPA =Environment.MachineName;
          _AUDNUM=WriteAuditing.Insert(audit);
          respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="INCLUSAO EFETUADA COM SUCESSO";
                }
                if(RETURN_VALUE==-1)
                {
                    respond.ErrorCode="FAILALL";
                    respond.ErrorMessage=ErrorManager.GetStringMsg("FAILALL");
                    respond.MessageToUser ="FALHA NA INCLUSAO DO REGISTRO";
                }
                if(RETURN_VALUE==-2)
                {
                    respond.ErrorCode="RECORDFOUND";
                    respond.ErrorMessage=ErrorManager.GetStringMsg("RECORDFOUND");
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
            respond.ErrorMessage=msg.Message;
            respond.Severity=msg.Severity;
            }
            }
            }
        return respond;
        }

    /// <summary>
    /// Altera um registro da tabela TBSYSFUN (Funcionalidades do Sistema)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">SystemFeatures</param>
    /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(SystemFeatures model)
        {
ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.HasError =false;
                            SystemFeatures ModelAud = Select(model.SYSFUN);
string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Funcionalidades do Sistema",model,true);

                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@SYSFUN", model.SYSFUN, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@SYSPRF", model.SYSPRF, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@SYSKEY", model.SYSKEY, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@SYSMTH", model.SYSMTH, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@SYSPRC", model.SYSPRC, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@SYSDSC", model.SYSDSC, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATUPD", model.DATUPD, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRSYSFUNUPD", p,commandType: CommandType.StoredProcedure);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
                respond.ReturnValue= RETURN_VALUE;
                if(RETURN_VALUE>0)
                {
                    Auditing audit = new Auditing();
          audit.UPDUSU =model.UPDUSU;
          audit.AUDDAT=DateTime.Now;
          audit.AUDKEY =KeyTableId;
          audit.AUDIDR =RETURN_VALUE;
          audit.AUDIMS =0;
          audit.AUDTSK =this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
          audit.AUDOBJ = "PRSYSFUNUPD";
          audit.AUDSRC =_original;
          audit.AUDCHG =_changed;
          audit.NIDTOK =0;
          audit.NUMIPA =Environment.MachineName;
          _AUDNUM=WriteAuditing.Insert(audit);
          respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="ALTERACAO EFETUADA COM SUCESSO";
                }
                if(RETURN_VALUE==-1)
                {
                    respond.ErrorCode="FAILALL";
                    respond.ErrorMessage=ErrorManager.GetStringMsg("FAILALL");
                }
                if(RETURN_VALUE==-2)
                {
                    respond.ErrorCode="RECORDNOTFOUND";
                    respond.ErrorMessage=ErrorManager.GetStringMsg("RECORDNOTFOUND");
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
            respond.ErrorMessage=msg.Message;
            respond.Severity=msg.Severity;
            }
            }
            }
            return respond;
        }
    /// <summary>
    /// Seleciona o tipo de contato de acordo com o código
    /// </summary>
        /// <param name="pSYSFUN">ID da Funcionalidade</param>
    /// <returns>SystemFeatures</returns>
    public SystemFeatures Select(int pSYSFUN)
        {
        this.Found=false;
    SystemFeatures query=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            query  = _conn.Query<SystemFeatures>(sql:"PRSYSFUNSEL", param:new {SYSFUN=pSYSFUN
},  commandType: CommandType.StoredProcedure, commandTimeout: 120).FirstOrDefault();
                    this.Found = true;
                    }
                    catch (Exception Error)
                    {
                    this.HasError = true;
                    this.Found=false;
                    _logger.Info(Error);
            }
            }
                    return query;
            }

    /// <summary>
    /// Seleciona todos os tipos de contato existentes
    /// </summary>
    /// <returns>SystemFeatures</returns>
    public SystemFeatures List()
        {
        this.Found=false;
    SystemFeatures query=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            query  = _conn.Query<SystemFeatures>(sql:"PRSYSFUNSELALL", param:new {},  commandType: CommandType.StoredProcedure, commandTimeout: 120).FirstOrDefault();
                    this.Found = true;
                    }
                    catch (Exception Error)
                    {
                    this.HasError = true;
                    this.Found=false;
                    _logger.Info(Error);
            }
            }
                    return query;
            }

    }
}
