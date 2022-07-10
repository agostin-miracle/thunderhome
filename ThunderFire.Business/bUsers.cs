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
/// Regra de Negócio para TBCADGER
///</summary>
///<remarks>
///Empresa     : Agostinho da Silva Milagres
///Copyright   : Copyright © 2012
///Descricao   : Gerador de Objetos
///Produto     : SQLDBTools
///Titulo      : SQLDBTools
///Version     : 1.3.0.0
///Data        : 24/03/2022 10:44
///Alias       : users
///Descrição   : Cadastro Geral de Usuários
///</remarks>
    public partial class UsersDao : BusinessBase, IUsers
    {
private static Logger _logger = LogManager.GetCurrentClassLogger();
/// <summary>
/// Instância Base
/// </summary>
public UsersDao()
{
this.Connected=true;
this.KeyTableId =1;

}
       
    /// <summary>
    /// Insere um registro na tabela TBCADGER (Cadastro Geral de Usuários)
    /// </summary>
    ///<param name="model">Users</param>
    /// <returns>int</returns>
        public ExecutionResponse Insert(Users model)
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
string _changed = Objects.GetPropertiesValue("Cadastro Geral de Usuários",model,true);

            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@CODUSU", model.CODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODATR", model.CODATR, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@SRCUSU", model.SRCUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NIVCFA", model.NIVCFA, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@STAUSU", model.STAUSU, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@TIPUSU", model.TIPUSU, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@TIPPES", model.TIPPES, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@CODPJU", model.CODPJU, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMIRG", model.NUMIRG, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@CODCMF", model.CODCMF, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NOMUSU", model.NOMUSU, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NOMMAE", model.NOMMAE, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@DATNAC", model.DATNAC, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@ATRPPE", model.ATRPPE, dbType:DbType.Boolean,  direction: ParameterDirection.Input);
            p.Add("@DSCOCO", model.DSCOCO, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@CODNAC", model.CODNAC, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRCADGERINS", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRCADGERINS";
          audit.AUDSRC =_changed;
          audit.AUDCHG ="";
          audit.NIDTOK =0;
          audit.NUMIPA =Environment.MachineName;
          _AUDNUM=WriteAuditing.Insert(audit);
          respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="INCLUSAO EFETUADA COM SUCESSO";
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
                    respond.ErrorCode="ALREADYEXISTCMF";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==-4)
                {
                    respond.ErrorCode="FAILALL";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser ="NAO FOI POSSIVEL INDEXAR O REGISTRO, CONSULTE O ADMINISTRADOR DE DADOS";
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
    /// Altera um registro da tabela TBCADGER (Cadastro Geral de Usuários)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">Users</param>
    /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(Users model)
        {
ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode= 10;
            this.HasError =false;
                            Users ModelAud = Select(model.CODUSU);
string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Cadastro Geral de Usuários",model,true);

                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@CODUSU", model.CODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODATR", model.CODATR, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@SRCUSU", model.SRCUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NIVCFA", model.NIVCFA, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@STAUSU", model.STAUSU, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@TIPUSU", model.TIPUSU, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@TIPPES", model.TIPPES, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@CODPJU", model.CODPJU, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMIRG", model.NUMIRG, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@CODCMF", model.CODCMF, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NOMUSU", model.NOMUSU, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NOMMAE", model.NOMMAE, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@DATNAC", model.DATNAC, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@ATRPPE", model.ATRPPE, dbType:DbType.Boolean,  direction: ParameterDirection.Input);
            p.Add("@DSCOCO", model.DSCOCO, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@CODNAC", model.CODNAC, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATUPD", model.DATUPD, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRCADGERUPD", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRCADGERUPD";
          audit.AUDSRC =_original;
          audit.AUDCHG =_changed;
          audit.NIDTOK =0;
          audit.NUMIPA =Environment.MachineName;
          _AUDNUM=WriteAuditing.Insert(audit);
          respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="ALTERACAO EFETUADA COM SUCESSO";
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
    /// Seleciona o registro de acordo com o Código do Usuário
    /// </summary>
        /// <param name="pCODUSU">Código do Usuario</param>
    /// <returns>Users</returns>
    public Users Select(System.Int32 pCODUSU)
        {
        this.Found=false;
            this.ProcessCode= 100;
    Users RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE  = _conn.Query<Users>(sql:"PRCADGERSEL", param:new {CODUSU=pCODUSU
},  commandType: CommandType.StoredProcedure, commandTimeout: 120).FirstOrDefault();

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
    /// Obtêm o nome de login default do aplicativo
    /// </summary>
        /// <param name="pNOMUSU">Nome do Usuário</param>
    /// <returns>string</returns>
    public string GetDefaultLoginName(System.String pNOMUSU)
        {
        this.Found=false;
            this.ProcessCode= 105;
    string RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
string _sql = @"SELECT dbo.GetLoginName(@NOMUSU)";
 RETURN_VALUE = _conn.Query<string>(sql:_sql, param:new {NOMUSU=pNOMUSU
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
