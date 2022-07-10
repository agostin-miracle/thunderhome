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
/// Regra de Negócio para TBCADLCT
///</summary>
///<remarks>
///Empresa     : Agostinho da Silva Milagres
///Copyright   : Copyright © 2012
///Descricao   : Gerador de Objetos
///Produto     : SQLDBTools
///Titulo      : SQLDBTools
///Version     : 1.3.0.0
///Data        : 17/04/2022 15:10
///Alias       : accountentryposting
///Descrição   : Tabela de Cadastro de Lançamentos
///</remarks>
    public partial class AccountEntryPostingDao : BusinessBase, IAccountEntryPosting
    {
private static Logger _logger = LogManager.GetCurrentClassLogger();
/// <summary>
/// Instância Base
/// </summary>
public AccountEntryPostingDao()
{
this.Connected=true;
this.KeyTableId =43;

}
       
    /// <summary>
    /// Insere um registro na tabela TBCADLCT (Tabela de Cadastro de Lançamentos)
    /// </summary>
    ///<param name="model">AccountEntryPosting</param>
    /// <returns>int</returns>
        public ExecutionResponse Insert(AccountEntryPosting model)
        {
            ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            this.HasError =false;
            this.ProcessCode= 10;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
string _changed = Objects.GetPropertiesValue("Tabela de Cadastro de Lançamentos",model,true);

            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@TIPLCT", model.TIPLCT, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@LINLCT", model.LINLCT, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@CODTRM", model.CODTRM, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@NUMORD", model.NUMORD, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@INDDEB", model.INDDEB, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@INDCRD", model.INDCRD, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@MOVDEB", model.MOVDEB, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@MOVCRD", model.MOVCRD, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@STAREG", model.STAREG, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@IDEPRE", model.IDEPRE, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRCADLCTINS", p,commandType: CommandType.StoredProcedure);
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
    /// Altera um registro da tabela TBCADLCT (Tabela de Cadastro de Lançamentos)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">AccountEntryPosting</param>
    /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(AccountEntryPosting model)
        {
ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode= 10;
            this.HasError =false;
                            AccountEntryPosting ModelAud = Select(model.NIDLCT);
string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Tabela de Cadastro de Lançamentos",model,true);

                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@NIDLCT", model.NIDLCT, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@TIPLCT", model.TIPLCT, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@LINLCT", model.LINLCT, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@CODTRM", model.CODTRM, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@NUMORD", model.NUMORD, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@INDDEB", model.INDDEB, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@INDCRD", model.INDCRD, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@MOVDEB", model.MOVDEB, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@MOVCRD", model.MOVCRD, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@STAREG", model.STAREG, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@IDEPRE", model.IDEPRE, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATUPD", model.DATUPD, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRCADLCTUPD", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRCADLCTUPD";
          audit.AUDSRC =_original;
          audit.AUDCHG =_changed;
          audit.NIDTOK =0;
          audit.NUMIPA =Environment.MachineName;
          _AUDNUM=WriteAuditing.Insert(audit);
          respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="REGISTRO ALTERADO COM SUCESSO";
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
    /// Obtêm o registro de Cadastro de Lançamento com base no id informado
    /// </summary>
        /// <param name="pNIDLCT">ID do Registro de Cadastro de Lançamento</param>
    /// <returns>AccountEntryPosting</returns>
    public AccountEntryPosting Select(System.Int32 pNIDLCT)
        {
        this.Found=false;
            this.ProcessCode= 100;
    AccountEntryPosting RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
string _sql = @"SELECT * FROM TBCADLCT (NOLOCK) A WHERE (A.NIDLCT=@NIDLCT)";
 RETURN_VALUE = _conn.Query<AccountEntryPosting>(sql:_sql, param:new {NIDLCT=pNIDLCT
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
    /// Copia um conjunto de cadastro de lançamentos para outros
    /// </summary>
    /// <param name="pTIPLCT">Tipo de Lançamento de Origem</param>
    /// <param name="pNEWTIP">Tipo de Lançamento de Destino</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <returns>int</returns>
    public int Copy( System.Int16 pTIPLCT,System.Int16 pNEWTIP,System.Int32 pUPDUSU)
    {
            this.ProcessCode= 30;
            if(ThunderFire.Business.Support.CheckAccess(this.KeyTableId, this.ProcessCode,pUPDUSU)!=1)
            {
        this.ReturnValue=ThunderFire.Business.Support.ReturnValue;
        this.MessageToUser=ThunderFire.Business.Support.MessageToUser;
        this.ErrorCode=ThunderFire.Business.Support.ErrorCode;
        return -100;
            }
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.ReturnValue);
p.Add("@TIPLCT", pTIPLCT, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@NEWTIP", pNEWTIP, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@UPDUSU", pUPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRCADLCTCPY", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
        }
        catch (Exception Error)
        {
            _logger.Info(Error);
            this.HasError =true;
        }
        }
        return RETURN_VALUE;
    }


    /// <summary>
    /// Exclui um conjunto de cadastro de lançamentos
    /// </summary>
    /// <param name="pTIPLCT">Tipo de Lançamento de Origem</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <returns>int</returns>
    public int Delete( System.Int16 pTIPLCT,System.Int32 pUPDUSU)
    {
            this.ProcessCode= 40;
            if(ThunderFire.Business.Support.CheckAccess(this.KeyTableId, this.ProcessCode,pUPDUSU)!=1)
            {
        this.ReturnValue=ThunderFire.Business.Support.ReturnValue;
        this.MessageToUser=ThunderFire.Business.Support.MessageToUser;
        this.ErrorCode=ThunderFire.Business.Support.ErrorCode;
        return -100;
            }
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.ReturnValue);
p.Add("@TIPLCT", pTIPLCT, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@UPDUSU", pUPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRCADLCTDEL", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
        }
        catch (Exception Error)
        {
            _logger.Info(Error);
            this.HasError =true;
        }
        }
        return RETURN_VALUE;
    }

    }
}
