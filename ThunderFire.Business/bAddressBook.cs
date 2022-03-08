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
/// Regra de Negócio para TBCADEND
///</summary>
///<remarks>
///Empresa     : Agostinho da Silva Milagres
///Copyright   : Copyright © 2012
///Descricao   : Gerador de Objetos
///Produto     : SQLDBTools
///Titulo      : SQLDBTools
///Version     : 1.3.0.0
///Data        : 04/03/2022 13:37
///Alias       : addressbook
///Descrição   : Cadasto de Endereços
///</remarks>
    public partial class AddressBookDao : BusinessBase, IAddressBook
    {
private static Logger _logger = LogManager.GetCurrentClassLogger();
/// <summary>
/// Instância Base
/// </summary>
public AddressBookDao()
{
this.Connected=true;
this.KeyTableId =8;

}
       
    /// <summary>
    /// Insere um registro na tabela TBCADEND (Cadasto de Endereços)
    /// </summary>
    ///<param name="model">AddressBook</param>
    /// <returns>int</returns>
        public ExecutionResponse Insert(AddressBook model)
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
string _changed = Objects.GetPropertiesValue("Cadasto de Endereços",model,true);

            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@REGATV", model.REGATV, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@CODUSU", model.CODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@TIPEND", model.TIPEND, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@TIPLOG", model.TIPLOG, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@CODUFE", model.CODUFE, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@DSCEND", model.DSCEND, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@DSCCPL", model.DSCCPL, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMEND", model.NUMEND, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@DSCCID", model.DSCCID, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@DSCBAI", model.DSCBAI, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@CODCEP", model.CODCEP, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@CODPAI", model.CODPAI, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@LATITU", model.LATITU, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@LONGIT", model.LONGIT, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRCADENDINS", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRCADENDINS";
          audit.AUDSRC =_changed;
          audit.AUDCHG ="";
          audit.NIDTOK =0;
          audit.NUMIPA =Environment.MachineName;
          _AUDNUM=WriteAuditing.Insert(audit);
          respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="ENDEREÇO INCLUIDO COM SUCESSO";
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
                if(RETURN_VALUE==-3)
                {
                    respond.ErrorCode="INVALIDADDRESS";
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
    /// Altera um registro da tabela TBCADEND (Cadasto de Endereços)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">AddressBook</param>
    /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(AddressBook model)
        {
ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode= 10;
            this.HasError =false;
                            AddressBook ModelAud = Select(model.CODEND);
string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Cadasto de Endereços",model,true);

                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@CODEND", model.CODEND, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@REGATV", model.REGATV, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@CODUSU", model.CODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@TIPEND", model.TIPEND, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@TIPLOG", model.TIPLOG, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@CODUFE", model.CODUFE, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@DSCEND", model.DSCEND, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@DSCCPL", model.DSCCPL, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMEND", model.NUMEND, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@DSCCID", model.DSCCID, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@DSCBAI", model.DSCBAI, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@CODCEP", model.CODCEP, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@CODPAI", model.CODPAI, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@LATITU", model.LATITU, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@LONGIT", model.LONGIT, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATUPD", model.DATUPD, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRCADENDUPD", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRCADENDUPD";
          audit.AUDSRC =_original;
          audit.AUDCHG =_changed;
          audit.NIDTOK =0;
          audit.NUMIPA =Environment.MachineName;
          _AUDNUM=WriteAuditing.Insert(audit);
          respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="ENDEREÇO ALTERADO COM SUCESSO";
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
                    respond.ErrorCode="INVALIDADDRESS";
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
    /// Obtêm o registro de Endereço
    /// </summary>
        /// <param name="pCODEND">Código do Endereço</param>
    /// <returns>AddressBook</returns>
    public AddressBook Select(System.Int32 pCODEND)
        {
        this.Found=false;
            this.ProcessCode= 100;
    AddressBook RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE  = _conn.Query<AddressBook>(sql:"PRCADENDSEL", param:new {CODEND=pCODEND
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
                    return RETURN_VALUE;
            }

    /// <summary>
    /// Obtêm uma lista de todos os endereços de um usuário
    /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pTIPEND">Tipo de Endereço</param>
    /// <param name="pREGATV">Registro Ativo</param>
    /// <param name="pSTAREC">Status de Registro</param>
    /// <returns>List of QueryAddressBook</returns>
    public List<QueryAddressBook> List(System.Int32 pCODUSU, System.Int16 pTIPEND= -1, System.Int16 pREGATV= -1, System.Int16 pSTAREC= -1)
        {
        this.Found=false;
            this.ProcessCode= 105;
    List<QueryAddressBook> RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE = _conn.Query<QueryAddressBook>(sql:"PRCADENDSELALL", param:new {CODUSU=pCODUSU,
TIPEND=pTIPEND,
REGATV=pREGATV,
STAREC=pSTAREC
},  commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();
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
    /// Localiza o ID de um endereço com base nos parametros fornecidos
    /// </summary>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pDSCEND">Descrição do Endereço</param>
    /// <param name="pTIPEND">Tipo de Endereço</param>
    /// <param name="pREGATV">Status de Registro</param>
    /// <returns>ExecutionResponse</returns>
    public ExecutionResponse  Find( System.Int32 pCODUSU,System.String pDSCEND,System.Byte pTIPEND,System.Byte? pREGATV)
    {
            int _AUDNUM =0;
            this.ProcessCode= 200;
            ExecutionResponse respond = new ExecutionResponse();
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
p.Add("@CODUSU", pCODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@DSCEND", pDSCEND, dbType:DbType.String,  direction: ParameterDirection.Input);
p.Add("@TIPEND", pTIPEND, dbType:DbType.Byte,  direction: ParameterDirection.Input);
p.Add("@REGATV", pREGATV, dbType:DbType.Byte,  direction: ParameterDirection.Input);
RETURN_VALUE=_conn.ExecuteScalar<Int32>(sql:"PRCADENDFND", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
respond.ReturnValue= RETURN_VALUE;
string _errormessage="";
        }
        catch (Exception Error)
        {
            _logger.Info(Error);
            this.HasError =true;
            respond.ReturnValue=RETURN_VALUE;
            respond.StatusCode=400;
            respond.MessageToUser="FALHA NO PROCESSAMENTO DA INFORMACAO";
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

    }
}
