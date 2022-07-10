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
/// Regra de Negócio para TBREGBOL
///</summary>
///<remarks>
///Empresa     : Agostinho da Silva Milagres
///Copyright   : Copyright © 2012
///Descricao   : Gerador de Objetos
///Produto     : SQLDBTools
///Titulo      : SQLDBTools
///Version     : 1.3.0.0
///Data        : 10/04/2022 09:40
///Alias       : ticket
///Descrição   : Registro de Boletos
///</remarks>
    public partial class TicketDao : BusinessBase, ITicket
    {
private static Logger _logger = LogManager.GetCurrentClassLogger();
/// <summary>
/// Instância Base
/// </summary>
public TicketDao()
{
this.Connected=true;
this.KeyTableId =33;

}
       
    /// <summary>
    /// Insere um registro na tabela TBREGBOL (Registro de Boletos)
    /// </summary>
    ///<param name="model">Ticket</param>
    /// <returns>int</returns>
        public ExecutionResponse Insert(Ticket model)
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
string _changed = Objects.GetPropertiesValue("Registro de Boletos",model,true);

            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@MODCAL", model.MODCAL, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@NIDCBL", model.NIDCBL, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@USUPRO", model.USUPRO, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@USUCED", model.USUCED, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@USUSAC", model.USUSAC, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODAVA", model.CODAVA, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODEND", model.CODEND, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODMOE", model.CODMOE, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@STABOL", model.STABOL, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@TIPBOL", model.TIPBOL, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@TIPBXA", model.TIPBXA, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@TIPENT", model.TIPENT, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATEMI", model.DATEMI, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@DATVCT", model.DATVCT, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@DATPGT", model.DATPGT, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@DATVAL", model.DATVAL, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@DATREG", model.DATREG, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@NUMPCL", model.NUMPCL, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@CODCED", model.CODCED, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NIDLIM", model.NIDLIM, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NUMIPA", model.NUMIPA, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@ORGBOL", model.ORGBOL, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@INSBC1", model.INSBC1, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@INSBC2", model.INSBC2, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@INSBC3", model.INSBC3, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@DSCBOL", model.DSCBOL, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@URLRET", model.URLRET, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@CODRET", model.CODRET, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@FILNAM", model.FILNAM, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NIDCTA", model.NIDCTA, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NUMDVF", model.NUMDVF, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@PEDCLI", model.PEDCLI, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@LINDIG", model.LINDIG, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@CODESP", model.CODESP, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMCTR", model.NUMCTR, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NOSNUM", model.NOSNUM, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@IMGSAV", model.IMGSAV, dbType:DbType.Boolean,  direction: ParameterDirection.Input);
            p.Add("@INDCNC", model.INDCNC, dbType:DbType.Boolean,  direction: ParameterDirection.Input);
            p.Add("@APLCAL", model.APLCAL, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@REGPRO", model.REGPRO, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@RSPTAR", model.RSPTAR, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@VLRBOL", model.VLRBOL, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@APLTAR", model.APLTAR, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@VLRTAR", model.VLRTAR, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@APLTDP", model.APLTDP, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@VLRTDP", model.VLRTDP, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRREGBOLINS", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRREGBOLINS";
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
    /// Altera um registro da tabela TBREGBOL (Registro de Boletos)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">Ticket</param>
    /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(Ticket model)
        {
ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode= 10;
            this.HasError =false;
                            Ticket ModelAud = Select(model.NIDBOL);
string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Registro de Boletos",model,true);

                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@NIDBOL", model.NIDBOL, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@MODCAL", model.MODCAL, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@NIDCBL", model.NIDCBL, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@USUPRO", model.USUPRO, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@USUCED", model.USUCED, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@USUSAC", model.USUSAC, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODAVA", model.CODAVA, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODEND", model.CODEND, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODMOE", model.CODMOE, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@STABOL", model.STABOL, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@TIPBOL", model.TIPBOL, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@TIPBXA", model.TIPBXA, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@TIPENT", model.TIPENT, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATEMI", model.DATEMI, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@DATVCT", model.DATVCT, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@DATPGT", model.DATPGT, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@DATVAL", model.DATVAL, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@DATREG", model.DATREG, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@NUMPCL", model.NUMPCL, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@CODCED", model.CODCED, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NIDLIM", model.NIDLIM, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NUMIPA", model.NUMIPA, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@ORGBOL", model.ORGBOL, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@INSBC1", model.INSBC1, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@INSBC2", model.INSBC2, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@INSBC3", model.INSBC3, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@DSCBOL", model.DSCBOL, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@URLRET", model.URLRET, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@CODRET", model.CODRET, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@FILNAM", model.FILNAM, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NIDCTA", model.NIDCTA, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NUMDVF", model.NUMDVF, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@PEDCLI", model.PEDCLI, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@LINDIG", model.LINDIG, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@CODESP", model.CODESP, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMCTR", model.NUMCTR, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NOSNUM", model.NOSNUM, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@IMGSAV", model.IMGSAV, dbType:DbType.Boolean,  direction: ParameterDirection.Input);
            p.Add("@INDCNC", model.INDCNC, dbType:DbType.Boolean,  direction: ParameterDirection.Input);
            p.Add("@APLCAL", model.APLCAL, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@REGPRO", model.REGPRO, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@RSPTAR", model.RSPTAR, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@VLRBOL", model.VLRBOL, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@APLTAR", model.APLTAR, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@VLRTAR", model.VLRTAR, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@APLTDP", model.APLTDP, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@VLRTDP", model.VLRTDP, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Int32,  direction: ParameterDirection.Input);
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
          audit.AUDOBJ = "PRREGBOLUPD";
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
    /// Seleciona o registro de Boleto de acordo com o ID
    /// </summary>
        /// <param name="pNIDBOL">ID do Boleto</param>
    /// <returns>Ticket</returns>
    public Ticket Select(System.Int32 pNIDBOL)
        {
        this.Found=false;
            this.ProcessCode= 100;
    Ticket RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE  = _conn.Query<Ticket>(sql:"PRREGBOLSEL", param:new {NIDBOL=pNIDBOL
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
    /// Encerra um boleto
    /// </summary>
    /// <param name="pNIDBOL">ID do Boleto</param>
    /// <param name="pSTAREC">Status do Registro</param>
    /// <param name="pTIPBXA">Tipo de Baixa</param>
    /// <param name="pSTABOL">Status do Boleto</param>
    /// <param name="pDATPGT">Data de Pagamento</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <returns>int</returns>
    public int CloseTicket( int pNIDBOL,System.Byte pSTAREC,System.Int16 pTIPBXA,System.Int16 pSTABOL,System.DateTime pDATPGT,int pUPDUSU)
    {
            this.ProcessCode= 200;
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
p.Add("@NIDBOL", pNIDBOL, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@STAREC", pSTAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
p.Add("@TIPBXA", pTIPBXA, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@STABOL", pSTABOL, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@DATPGT", pDATPGT, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
p.Add("@UPDUSU", pUPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRREGBOLCLOTKT", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
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
