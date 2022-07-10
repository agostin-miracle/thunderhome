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
/// Regra de Negócio para TBREGCRT
///</summary>
///<remarks>
///Empresa     : Agostinho da Silva Milagres
///Copyright   : Copyright © 2012
///Descricao   : Gerador de Objetos
///Produto     : SQLDBTools
///Titulo      : SQLDBTools
///Version     : 1.3.0.0
///Data        : 18/03/2022 16:45
///Alias       : activecards
///Descrição   : Active Cards
///</remarks>
    public partial class ActiveCardsDao : BusinessBase, IActiveCards
    {
private static Logger _logger = LogManager.GetCurrentClassLogger();
/// <summary>
/// Instância Base
/// </summary>
public ActiveCardsDao()
{
this.Connected=true;
this.KeyTableId =14;

}
       
    /// <summary>
    /// Insere um registro na tabela TBREGCRT (Active Cards)
    /// </summary>
    ///<param name="model">ActiveCards</param>
    /// <returns>int</returns>
        public ExecutionResponse Insert(ActiveCards model)
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
string _changed = Objects.GetPropertiesValue("Active Cards",model,true);

            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@CODCRT", model.CODCRT, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NIDCTA", model.NIDCTA, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@USUPRO", model.USUPRO, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@TIPPRO", model.TIPPRO, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@ORGATV", model.ORGATV, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@ASSUSU", model.ASSUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@SECLVL", model.SECLVL, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATATV", model.DATATV, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@DATCAN", model.DATCAN, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@CALMEN", model.CALMEN, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@USUMEN", model.USUMEN, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NOMPRT", model.NOMPRT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMCRT", model.NUMCRT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@VALCRT", model.VALCRT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@PSWCRT", model.PSWCRT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMCVC", model.NUMCVC, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@STAPRO", model.STAPRO, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@STACRT", model.STACRT, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@NOMCR1", model.NOMCR1, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NOMCR2", model.NOMCR2, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NOMCR3", model.NOMCR3, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@OPRCRT", model.OPRCRT, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATEXT", model.DATEXT, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@CODBLO", model.CODBLO, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@TIPPRT", model.TIPPRT, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DSCMOT", model.DSCMOT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@APLCON", model.APLCON, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@APLSAQ", model.APLSAQ, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@LOTMIG", model.LOTMIG, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRREGCRTINS", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRREGCRTINS";
          audit.AUDSRC ="";
          audit.AUDCHG =_changed;
          audit.NIDTOK =0;
          audit.NUMIPA =Environment.MachineName;
          _AUDNUM=WriteAuditing.Insert(audit);
          respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="CARTAO INCLUIDO COM SUCESSO";
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
    /// Altera um registro da tabela TBREGCRT (Active Cards)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">ActiveCards</param>
    /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(ActiveCards model)
        {
ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode= 10;
            this.HasError =false;
                            ActiveCards ModelAud = Select(model.CODCRT);
string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Active Cards",model,true);

                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@CODCRT", model.CODCRT, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NIDCTA", model.NIDCTA, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@USUPRO", model.USUPRO, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@TIPPRO", model.TIPPRO, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@ORGATV", model.ORGATV, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@ASSUSU", model.ASSUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@SECLVL", model.SECLVL, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATATV", model.DATATV, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@DATCAN", model.DATCAN, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@CALMEN", model.CALMEN, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@USUMEN", model.USUMEN, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NOMPRT", model.NOMPRT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMCRT", model.NUMCRT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@VALCRT", model.VALCRT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@PSWCRT", model.PSWCRT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMCVC", model.NUMCVC, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@STAPRO", model.STAPRO, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@STACRT", model.STACRT, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@NOMCR1", model.NOMCR1, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NOMCR2", model.NOMCR2, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NOMCR3", model.NOMCR3, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@OPRCRT", model.OPRCRT, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATEXT", model.DATEXT, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@CODBLO", model.CODBLO, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@TIPPRT", model.TIPPRT, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DSCMOT", model.DSCMOT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@APLCON", model.APLCON, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@APLSAQ", model.APLSAQ, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@LOTMIG", model.LOTMIG, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATUPD", model.DATUPD, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRREGCRTUPD", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRREGCRTINS";
          audit.AUDSRC =_original;
          audit.AUDCHG =_changed;
          audit.NIDTOK =0;
          audit.NUMIPA =Environment.MachineName;
          _AUDNUM=WriteAuditing.Insert(audit);
          respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="CARTAO ALTERADO COM SUCESSO";
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
    /// Seleciona o registro de acordo com o Código do Cartão
    /// </summary>
        /// <param name="pCODCRT">Código do Cartão</param>
    /// <returns>ActiveCards</returns>
    public ActiveCards Select(System.Int32 pCODCRT)
        {
        this.Found=false;
            this.ProcessCode= 100;
    ActiveCards RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE  = _conn.Query<ActiveCards>(sql:"PRREGCRTSEL", param:new {CODCRT=pCODCRT
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
    /// Seleciona o registro de acordo com o Código Extendido do Cartão
    /// </summary>
        /// <param name="pNUMCRT">Código Extendido do Cartão</param>
    /// <returns>ActiveCards</returns>
    public ActiveCards Select(System.String pNUMCRT)
        {
        this.Found=false;
            this.ProcessCode= 105;
    ActiveCards RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE  = _conn.Query<ActiveCards>(sql:"PRREGCRTSELCRT", param:new {NUMCRT=pNUMCRT
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
    /// Altera o campo de permissão de compra on-line
    /// </summary>
    /// <param name="pCODCRT">Código do Cartão</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <param name="pAPLCON">Habilitar Compra Online</param>
    /// <returns>ExecutionResponse</returns>
    public ExecutionResponse  ChangeOnlineShop( System.Int32 pCODCRT,System.Int32 pUPDUSU,System.Byte pAPLCON)
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
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.ReturnValue);
p.Add("@CODCRT", pCODCRT, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@UPDUSU", pUPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@APLCON", pAPLCON, dbType:DbType.Byte,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRREGCRTCHGONLPAR", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
respond.ReturnValue= RETURN_VALUE;
string _errormessage="";
                if(RETURN_VALUE==1)
                {
                    respond.Logged=true;
                    respond.MessageToUser ="PERMISSÃO DE COMPRA ON LINE ALTERADA COM SUCESSO";
_errormessage="";
                }
                if(RETURN_VALUE==0)
                {
                    respond.MessageToUser ="NENHUMA ALTERAÇÃO EFETUADA";
_errormessage="";
                }
                if(RETURN_VALUE==-1)
                {
                    respond.ErrorCode="CARDINVALIDID";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==-2)
                {
                    respond.ErrorCode="REQUIREDUSEROPERATION";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==-3)
                {
                    respond.ErrorCode="CARDNOTFOUND";
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


    /// <summary>
    /// Altera o campo de permissão de saque
    /// </summary>
    /// <param name="pCODCRT">Código do Cartão</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <param name="pAPLSAQ">Habilitar Compra Online</param>
    /// <returns>ExecutionResponse</returns>
    public ExecutionResponse  ChangeWithdraw( System.Int32 pCODCRT,System.Int32 pUPDUSU,System.Byte pAPLSAQ)
    {
            int _AUDNUM =0;
            this.ProcessCode= 205;
            ExecutionResponse respond = new ExecutionResponse();
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.ReturnValue);
p.Add("@CODCRT", pCODCRT, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@UPDUSU", pUPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@APLSAQ", pAPLSAQ, dbType:DbType.Byte,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRREGCRTCHGSAQPAR", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
respond.ReturnValue= RETURN_VALUE;
string _errormessage="";
                if(RETURN_VALUE==1)
                {
                    respond.Logged=true;
                    respond.MessageToUser ="PERMISSÃO DE SAQUE ALTERADA COM SUCESSO";
_errormessage="";
                }
                if(RETURN_VALUE==0)
                {
                    respond.MessageToUser ="NENHUMA ALTERAÇÃO EFETUADA";
_errormessage="";
                }
                if(RETURN_VALUE==-1)
                {
                    respond.ErrorCode="CARDINVALIDID";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==-2)
                {
                    respond.ErrorCode="REQUIREDUSEROPERATION";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==-3)
                {
                    respond.ErrorCode="CARDNOTFOUND";
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


    /// <summary>
    /// Altera o Gestor do Cartão em uso
    /// </summary>
    /// <param name="pCODCRT">Código do Cartão</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <param name="pUSUPRO">Código do Gestor</param>
    /// <returns>ExecutionResponse</returns>
    public ExecutionResponse  ChangeOwner( System.Int32 pCODCRT,System.Int32 pUPDUSU,System.Int32 pUSUPRO = 13)
    {
            int _AUDNUM =0;
            this.ProcessCode= 0;
            ExecutionResponse respond = new ExecutionResponse();
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
p.Add("@CODCRT", pCODCRT, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@UPDUSU", pUPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@USUPRO", pUSUPRO, dbType:DbType.Int32,  direction: ParameterDirection.Input);
RETURN_VALUE=_conn.ExecuteScalar<Int32>(sql:"PRREGCRTCHGOWN", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
respond.ReturnValue= RETURN_VALUE;
string _errormessage="";
                if(RETURN_VALUE==1)
                {
                    Auditing audit = new Auditing();
            audit.UPDUSU =pUPDUSU;
            audit.AUDDAT=DateTime.Now;
            audit.AUDKEY =KeyTableId;
            audit.AUDIDR =pCODCRT;
            audit.AUDIMS =0;
            audit.AUDTSK =this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            audit.AUDOBJ = "PRREGCRTCHGOWN";
            audit.AUDSRC ="";
            audit.AUDCHG = "ALTEROU O USUARIO GESTOR DO CARTAO PARA " + pUSUPRO.ToString();
            audit.NIDTOK =0;
            audit.NUMIPA =Environment.MachineName;
            _AUDNUM=WriteAuditing.Insert(audit);
            respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="CARTAO ALTERADO COM SUCESSO";
_errormessage="";
                }
                if(RETURN_VALUE==-1)
                {
                    respond.ErrorCode="CARDINVALIDID";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==-2)
                {
                    respond.ErrorCode="REQUIREDUSEROPERATION";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==-3)
                {
                    respond.ErrorCode="CARDALREADYBELONGMANAGER";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==-4)
                {
                    respond.ErrorCode="CARDINVALIDLENGTH";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==-5)
                {
                    respond.ErrorCode="CARDINVALIDPANNUMBER";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==-6)
                {
                    respond.ErrorCode="INVALIDMANAGERCODE";
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


    /// <summary>
    /// Executa Ações de Atualização pela manutenção do Cartão
    /// </summary>
/// <remarks>
/// <para>1 - Reativa a mensalidade de um usuário do cartão</para>
/// <para>3 - Inativa a mensalidade de um usuário do cartão</para>
/// </remarks>
    /// <param name="pCODCRT">Código do Cartão</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <param name="pCODACT">Código de Ação</param>
    /// <returns>ExecutionResponse</returns>
    public ExecutionResponse  CancelMonthlyFees( System.Int32 pCODCRT,System.Int32 pUPDUSU,System.Byte pCODACT)
    {
            int _AUDNUM =0;
            this.ProcessCode= 0;
            ExecutionResponse respond = new ExecutionResponse();
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.ReturnValue);
p.Add("@CODCRT", pCODCRT, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@UPDUSU", pUPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@CODACT", pCODACT, dbType:DbType.Byte,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRREGCRTCHGMEN", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
respond.ReturnValue= RETURN_VALUE;
string _errormessage="";
                if(RETURN_VALUE>0)
                {
                    Auditing audit = new Auditing();
            audit.UPDUSU =pUPDUSU;
            audit.AUDDAT=DateTime.Now;
            audit.AUDKEY =KeyTableId;
            audit.AUDIDR =pCODCRT;
            audit.AUDIMS =0;
            audit.AUDTSK =this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            audit.AUDOBJ = "PRREGCRTCHGMEN";
            audit.AUDSRC ="";
            audit.AUDCHG = String.Format("{0} REGISTROS DE MENSALIDADE DO CARTAO FORAM {1}",RETURN_VALUE,((pCODACT==1)? "RECUPERADAS":"CANCELADAS"));
            audit.NIDTOK =0;
            audit.NUMIPA =Environment.MachineName;
            _AUDNUM=WriteAuditing.Insert(audit);
            respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="MENSALIDADES DO CARTAO ALTERADAS COM SUCESSO";
_errormessage="";
                }
                if(RETURN_VALUE==0)
                {
                    respond.ErrorCode="EMPTYDATARECORDS";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE>0)
                {
                    respond.ErrorCode="OK";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser ="ATUALIZAÇÃO DE MENSALIDADES EFETUADA COM SUCESSO";
_errormessage="";
                }
                if(RETURN_VALUE==-1)
                {
                    respond.ErrorCode="REQUIREDUSEROPERATION";
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


    /// <summary>
    /// Verifica se o cartão e o CPF/CNPJ informado são passíveis de ativação
    /// </summary>
    /// <param name="pNUMCRT">Número do Cartão</param>
    /// <param name="pCODCMF">Código do CPF/CNPJ</param>
    /// <returns>int</returns>
    public int IsActivatable( System.String pNUMCRT,System.String pCODCMF)
    {
            this.ProcessCode= 0;
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
p.Add("@NUMCRT", pNUMCRT, dbType:DbType.String,  direction: ParameterDirection.Input);
p.Add("@CODCMF", pCODCMF, dbType:DbType.String,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRREGCRTGETATV", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
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
    /// Operação@1 -Bloqueia o Cartão@2 - Reverte o Bloqueio Anterior@3 -Fixa o cartão como aguardando desbloqueio
    /// </summary>
    /// <param name="pCODCRT">Código do Cartão</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <param name="pTIPOPE">Tipo de Operação de bloqueio</param>
    /// <returns>ExecutionResponse</returns>
    public ExecutionResponse  LockCard( System.Int32 pCODCRT,System.Int32 pUPDUSU,System.Byte pTIPOPE)
    {
            int _AUDNUM =0;
            this.ProcessCode= 0;
            ExecutionResponse respond = new ExecutionResponse();
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
p.Add("@CODCRT", pCODCRT, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@UPDUSU", pUPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@TIPOPE", pTIPOPE, dbType:DbType.Byte,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRREGCRTLCKCRT", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
respond.ReturnValue= RETURN_VALUE;
string _errormessage="";
                if(RETURN_VALUE>0)
                {
                    respond.Logged=true;
                    respond.MessageToUser ="BLOQUEIO/DESBLOQUEIO REALIZADO COM SUCESSO";
_errormessage="";
                }
                if(RETURN_VALUE==-1)
                {
                    respond.ErrorCode="LOCKCARDFAILREGISTER";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==-2)
                {
                    respond.ErrorCode="UNLOCKFAILREGISTER";
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


    /// <summary>
    /// Altera o Status de um Cartão
    /// </summary>
    /// <param name="pCODCRT">Código do Cartão</param>
    /// <param name="pSTACRT">Novo Status do Cartão</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <param name="pCODBLO">Código de Desbloqueio</param>
    /// <param name="pDSCMOT">Motivo</param>
    /// <returns>ExecutionResponse</returns>
    public ExecutionResponse  ChangeStatus( System.Int32 pCODCRT,System.Int16 pSTACRT,System.Int32 pUPDUSU,System.Int16 pCODBLO = 0,System.String pDSCMOT = "''")
    {
            int _AUDNUM =0;
            this.ProcessCode= 0;
            ExecutionResponse respond = new ExecutionResponse();
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
p.Add("@CODCRT", pCODCRT, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@STACRT", pSTACRT, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@UPDUSU", pUPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@CODBLO", pCODBLO, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@DSCMOT", pDSCMOT, dbType:DbType.String,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRREGCRTCHGSTA", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
respond.ReturnValue= RETURN_VALUE;
string _errormessage="";
                if(RETURN_VALUE==0)
                {
                    respond.ErrorCode="EMPTYDATA";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==1)
                {
                    respond.Logged=true;
                    respond.MessageToUser ="STATUS DO CARTAO ALTERADO COM SUCESSO";
_errormessage="";
                }
                if(RETURN_VALUE==-1)
                {
                    respond.ErrorCode="FAILUREUPDATECARD";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==-2)
                {
                    respond.ErrorCode="CARDNOTFOUND";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==-3)
                {
                    respond.ErrorCode="RECORDNOTFOUND";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser ="CODIGO DE STATUS INEXISTENTE";
_errormessage="";
                }
                if(RETURN_VALUE==-4)
                {
                    respond.ErrorCode="FAILUREUPDATECARD";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser ="ATUALIZAÇÃO NEGADA: OS STATUS SÃO IGUAIS";
_errormessage="";
                }
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


    /// <summary>
    /// Efetua a alteração da senha do cartão
    /// </summary>
    /// <param name="pCODCRT">Código do Cartão</param>
    /// <param name="pPSWCRT">Novo Senha do Cartão</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <param name="pNUMIPA">Endereço de Localização</param>
    /// <returns>ExecutionResponse</returns>
    public ExecutionResponse  ChangePin( System.Int32 pCODCRT,System.String pPSWCRT,System.Int32 pUPDUSU,System.String pNUMIPA)
    {
            int _AUDNUM =0;
            this.ProcessCode= 0;
            ExecutionResponse respond = new ExecutionResponse();
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
p.Add("@CODCRT", pCODCRT, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@PSWCRT", pPSWCRT, dbType:DbType.String,  direction: ParameterDirection.Input);
p.Add("@UPDUSU", pUPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@NUMIPA", pNUMIPA, dbType:DbType.String,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRREGCRTCHGPSW", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
respond.ReturnValue= RETURN_VALUE;
string _errormessage="";
                if(RETURN_VALUE==0)
                {
                    respond.ErrorCode="EMPTYDATA";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==1)
                {
                    respond.Logged=true;
                    respond.MessageToUser ="SENHA ALTERADA COM SUCESSO";
_errormessage="";
                }
                if(RETURN_VALUE==-1)
                {
                    respond.ErrorCode="FAILUREUPDATECARD";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==-2)
                {
                    respond.ErrorCode="CARDNOTFOUND";
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


    /// <summary>
    /// Efetua a alteração do número o CVC do Cartão
    /// </summary>
    /// <param name="pCODCRT">Código do Cartão</param>
    /// <param name="pNUMCVC">Novo Número do CVC</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <returns>ExecutionResponse</returns>
    public ExecutionResponse  ChangeCVC( System.Int32 pCODCRT,System.Int16 pNUMCVC,System.Int32 pUPDUSU)
    {
            int _AUDNUM =0;
            this.ProcessCode= 0;
            ExecutionResponse respond = new ExecutionResponse();
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
p.Add("@CODCRT", pCODCRT, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@NUMCVC", pNUMCVC, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@UPDUSU", pUPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRREGCRTCHGCVC", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
respond.ReturnValue= RETURN_VALUE;
string _errormessage="";
                if(RETURN_VALUE==0)
                {
                    respond.ErrorCode="EMPTYDATA";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==1)
                {
                    respond.ErrorCode="OK";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==-1)
                {
                    respond.ErrorCode="FAILUREUPDATECARD";
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


    /// <summary>
    /// Cancela a associação de um cartão
    /// </summary>
    /// <param name="pCODCRT">Código do Cartão</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <returns>ExecutionResponse</returns>
    public ExecutionResponse  CancelAssociation( System.Int32 pCODCRT,System.Int32 pUPDUSU)
    {
            int _AUDNUM =0;
            this.ProcessCode= 0;
            ExecutionResponse respond = new ExecutionResponse();
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
p.Add("@CODCRT", pCODCRT, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@UPDUSU", pUPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRREGCRTCHGASS", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
respond.ReturnValue= RETURN_VALUE;
string _errormessage="";
                if(RETURN_VALUE==0)
                {
                    respond.ErrorCode="EMPTYDATA";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==1)
                {
                    respond.ErrorCode="OK";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==-1)
                {
                    respond.ErrorCode="FAILUREUPDATECARD";
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
