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
/// Regra de Negócio para TBVALBOL
///</summary>
///<remarks>
///Empresa     : Agostinho da Silva Milagres
///Copyright   : Copyright © 2012
///Descricao   : Gerador de Objetos
///Produto     : SQLDBTools
///Titulo      : SQLDBTools
///Version     : 1.3.0.0
///Data        : 16/03/2022 17:19
///Alias       : ticketvalues
///Descrição   : Registro de Valores de Boletos
///</remarks>
    public partial class TicketValuesDao : BusinessBase, ITicketValues
    {
private static Logger _logger = LogManager.GetCurrentClassLogger();
/// <summary>
/// Instância Base
/// </summary>
public TicketValuesDao()
{
this.Connected=true;
this.KeyTableId =34;

}
       
    /// <summary>
    /// Insere um registro na tabela TBVALBOL (Registro de Valores de Boletos)
    /// </summary>
    ///<param name="model">TicketValues</param>
    /// <returns>int</returns>
        public ExecutionResponse Insert(TicketValues model)
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
string _changed = Objects.GetPropertiesValue("Registro de Valores de Boletos",model,true);

            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@NIDBOL", model.NIDBOL, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@TIPVAL", model.TIPVAL, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@VLRBAS", model.VLRBAS, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@VLRPCT", model.VLRPCT, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@SIGOPE", model.SIGOPE, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@VLROPE", model.VLROPE, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRVALBOLINS", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRVALBOLINS";
          audit.AUDSRC =_changed;
          audit.AUDCHG ="";
          audit.NIDTOK =0;
          audit.NUMIPA =Environment.MachineName;
          _AUDNUM=WriteAuditing.Insert(audit);
          respond.Logged=_AUDNUM>0;
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
    /// Altera um registro da tabela TBVALBOL (Registro de Valores de Boletos)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">TicketValues</param>
    /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(TicketValues model)
        {
ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode= 10;
            this.HasError =false;
                            TicketValues ModelAud = Select(model.NIDVBL);
string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Registro de Valores de Boletos",model,true);

                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@NIDVBL", model.NIDVBL, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NIDBOL", model.NIDBOL, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@TIPVAL", model.TIPVAL, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@VLRBAS", model.VLRBAS, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@VLRPCT", model.VLRPCT, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@SIGOPE", model.SIGOPE, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@VLROPE", model.VLROPE, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATUPD", model.DATUPD, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRCFGBOLUPD", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRVALBOLUPD";
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
    /// Seleciona o registro de acordo com o codigo do produto
    /// </summary>
        /// <param name="pNIDCBL">ID do Registro de Configuração de Boleto</param>
    /// <returns>TicketValues</returns>
    public TicketValues Select(System.Int32 pNIDCBL)
        {
        this.Found=false;
            this.ProcessCode= 100;
    TicketValues RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE  = _conn.Query<TicketValues>(sql:"PRVALBOLSEL", param:new {NIDCBL=pNIDCBL
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
/// Seleciona todos os registros de um determinado boleto
/// </summary>
/// <returns>Listof TicketValues</returns>
    public List<TicketValues> List()
        {
            this.ProcessCode= 115;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            var result = _conn.Query<TicketValues>(sql:"PRVALBOLSELALL", param:new {},  commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();
                    this.Found = true;
                    return result.ToList();
                    }
                    catch (Exception Error)
                    {
                    this.HasError = true;
                    this.Found=false;
                    _logger.Info(Error);
            }
            }
                    return null;
            }

    }
}
