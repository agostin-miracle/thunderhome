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
/// Regra de Negócio para TBRBBBOL
///</summary>
///<remarks>
///Empresa     : Agostinho da Silva Milagres
///Copyright   : Copyright © 2012
///Descricao   : Gerador de Objetos
///Produto     : SQLDBTools
///Titulo      : SQLDBTools
///Version     : 1.3.0.0
///Data        : 10/04/2022 11:51
///Alias       : ticketreceiptrecord
///Descrição   : Ticket Receipt Record
///</remarks>
    public partial class TicketReceiptRecordDao : BusinessBase, ITicketReceiptRecord
    {
private static Logger _logger = LogManager.GetCurrentClassLogger();
/// <summary>
/// Instância Base
/// </summary>
public TicketReceiptRecordDao()
{
this.Connected=true;
this.KeyTableId =38;

}
       
    /// <summary>
    /// Insere um registro na tabela TBRBBBOL (Ticket Receipt Record)
    /// </summary>
    ///<param name="model">TicketReceiptRecord</param>
    /// <returns>int</returns>
        public ExecutionResponse Insert(TicketReceiptRecord model)
        {
            ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            this.HasError =false;
            this.ProcessCode= 10;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
string _changed = Objects.GetPropertiesValue("Ticket Receipt Record",model,true);

            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@NIDLIM", model.NIDLIM, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@TIPBXA", model.TIPBXA, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATPGT", model.DATPGT, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@FILNAM", model.FILNAM, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMROW", model.NUMROW, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@USUBCO", model.USUBCO, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NUMREX", model.NUMREX, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@TOTPAG", model.TOTPAG, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@TOTJUR", model.TOTJUR, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@TOTDES", model.TOTDES, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@TOTTAR", model.TOTTAR, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@TOTTEX", model.TOTTEX, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@TOTDCP", model.TOTDCP, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@TOTLIQ", model.TOTLIQ, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRRBBBOLINS", p,commandType: CommandType.StoredProcedure);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
respond.ReturnValue = RETURN_VALUE;
string _errormessage="";
                if(RETURN_VALUE==-1)
                {
                    respond.ErrorCode="FAILALL";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser ="FALHA NA INCLUSAO DO REGISTRO";
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
                    respond.ErrorCode="AUTHORIZATIONPAYMENTUSED";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser ="AUTORIZAÇÃO DE PAGAMENTO UTILIZADA";
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
    /// Altera um registro da tabela TBRBBBOL (Ticket Receipt Record)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">TicketReceiptRecord</param>
    /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(TicketReceiptRecord model)
        {
ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            this.ProcessCode= 10;
            this.HasError =false;
                            TicketReceiptRecord ModelAud = Select(model.NIDRBB);
string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Ticket Receipt Record",model,true);

                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@NIDRBB", model.NIDRBB, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NIDLIM", model.NIDLIM, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@TIPBXA", model.TIPBXA, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATPGT", model.DATPGT, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@FILNAM", model.FILNAM, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMROW", model.NUMROW, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@USUBCO", model.USUBCO, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NUMREX", model.NUMREX, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@TOTPAG", model.TOTPAG, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@TOTJUR", model.TOTJUR, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@TOTDES", model.TOTDES, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@TOTTAR", model.TOTTAR, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@TOTTEX", model.TOTTEX, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@TOTDCP", model.TOTDCP, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@TOTLIQ", model.TOTLIQ, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATUPD", model.DATUPD, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRRBBBOLUPD", p,commandType: CommandType.StoredProcedure);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
                respond.ReturnValue= RETURN_VALUE;
string _errormessage="";
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
    /// Seleciona o registro de acordo com o id de registro do conta corrente
    /// </summary>
        /// <param name="pNIDRBB">ID do Registro de Recebimento de Boleto</param>
    /// <returns>TicketReceiptRecord</returns>
    public TicketReceiptRecord Select(System.Int32 pNIDRBB)
        {
        this.Found=false;
            this.ProcessCode= 0;
    TicketReceiptRecord RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE  = _conn.Query<TicketReceiptRecord>(sql:"PRRBBBOLSEL", param:new {NIDRBB=pNIDRBB
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
    /// Executa o fechamento de um lote de Registro de Recebimento de Boleto
    /// </summary>
    /// <param name="pNIDRBB">id do Registro de Recebimento de Boleto</param>
    /// <returns>int</returns>
    public int CloseBatch( System.Int32 pNIDRBB)
    {
            this.ProcessCode= 200;
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
p.Add("@NIDRBB", pNIDRBB, dbType:DbType.Int32,  direction: ParameterDirection.Input);
RETURN_VALUE=_conn.ExecuteScalar<Int32>(sql:"PRRBBBOLCLOBAT", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
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
