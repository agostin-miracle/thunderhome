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
/// Regra de Negócio para TBCADCTA
///</summary>
///<remarks>
///Empresa     : Agostinho da Silva Milagres
///Copyright   : Copyright © 2012
///Descricao   : Gerador de Objetos
///Produto     : SQLDBTools
///Titulo      : SQLDBTools
///Version     : 1.3.0.0
///Data        : 04/03/2022 15:20
///Alias       : virtualaccount
///Descrição   : Virtual Account
///</remarks>
    public partial class VirtualAccountDao : BusinessBase, IVirtualAccount
    {
private static Logger _logger = LogManager.GetCurrentClassLogger();
/// <summary>
/// Instância Base
/// </summary>
public VirtualAccountDao()
{
this.Connected=true;
this.KeyTableId =12;

}
       
    /// <summary>
    /// Insere um registro na tabela TBCADCTA (Virtual Account)
    /// </summary>
    ///<param name="model">VirtualAccount</param>
    /// <returns>int</returns>
        public ExecutionResponse Insert(VirtualAccount model)
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
string _changed = Objects.GetPropertiesValue("Virtual Account",model,true);

            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@CODUSU", model.CODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NUMAGE", model.NUMAGE, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMBCO", model.NUMBCO, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMCTA", model.NUMCTA, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMDVE", model.NUMDVE, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@ORGCTA", model.ORGCTA, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@REGATV", model.REGATV, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@TIPCTA", model.TIPCTA, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@STACTA", model.STACTA, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@DATVAL", model.DATVAL, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@APLLIM", model.APLLIM, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@VLRLIM", model.VLRLIM, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@TIPBNF", model.TIPBNF, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODCMF", model.CODCMF, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NOMBNF", model.NOMBNF, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRCADCTAINS", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRCADCTAUPD";
          audit.AUDSRC ="";
          audit.AUDCHG =_changed;
          audit.NIDTOK =0;
          audit.NUMIPA =Environment.MachineName;
          _AUDNUM=WriteAuditing.Insert(audit);
          respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="CONTA VIRTUAL INCLUIDA COM SUCESSO";
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
                    respond.MessageToUser ="O USUARIO JA POSSUI UMA CONTA VIRTUAL REGISTRADA";
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
    /// Altera um registro da tabela TBCADCTA (Virtual Account)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">VirtualAccount</param>
    /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(VirtualAccount model)
        {
ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode= 10;
            this.HasError =false;
                            VirtualAccount ModelAud = Select(model.NIDCTA);
string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Virtual Account",model,true);

                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@NIDCTA", model.NIDCTA, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODUSU", model.CODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NUMAGE", model.NUMAGE, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMBCO", model.NUMBCO, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMCTA", model.NUMCTA, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMDVE", model.NUMDVE, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@ORGCTA", model.ORGCTA, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@REGATV", model.REGATV, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@TIPCTA", model.TIPCTA, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@STACTA", model.STACTA, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@DATVAL", model.DATVAL, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@APLLIM", model.APLLIM, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@VLRLIM", model.VLRLIM, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@TIPBNF", model.TIPBNF, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODCMF", model.CODCMF, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NOMBNF", model.NOMBNF, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATUPD", model.DATUPD, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRCADCTAUPD", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRCADCTAUPD";
          audit.AUDSRC ="";
          audit.AUDCHG =_changed;
          audit.NIDTOK =0;
          audit.NUMIPA =Environment.MachineName;
          _AUDNUM=WriteAuditing.Insert(audit);
          respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="CONTA VIRTUAL ALTERADA COM SUCESSO";
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
    /// Seleciona o registro de conta virtual de acordo com o id da conta
    /// </summary>
        /// <param name="pNIDCTA">ID da Conta Virtual</param>
    /// <returns>VirtualAccount</returns>
    public VirtualAccount Select(System.Int32 pNIDCTA)
        {
        this.Found=false;
            this.ProcessCode= 100;
    VirtualAccount RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE  = _conn.Query<VirtualAccount>(sql:"PRCADCTASEL", param:new {NIDCTA=pNIDCTA
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
    /// Seleciona o registro de conta virtual de acordo com os parâmetros fornecidos
    /// </summary>
        /// <param name="pNUMBCO">Banco</param>
    /// <param name="pNUMAGE">Agência</param>
    /// <param name="pNUMCTA">Conta</param>
    /// <param name="pNUMDVE">DV</param>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pORGCTA">Origem da Conta</param>
    /// <param name="pTIPCTA">Tipo de Conta</param>
    /// <returns>VirtualAccount</returns>
    public VirtualAccount Select(System.String pNUMBCO, System.String pNUMAGE, System.String pNUMCTA, System.String pNUMDVE, System.Int32 pCODUSU, System.Byte pORGCTA= 2, System.Int32 pTIPCTA= 2)
        {
        this.Found=false;
            this.ProcessCode= 0;
    VirtualAccount RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE  = _conn.Query<VirtualAccount>(sql:"PRCADCTASELCTA", param:new {NUMBCO=pNUMBCO,
NUMAGE=pNUMAGE,
NUMCTA=pNUMCTA,
NUMDVE=pNUMDVE,
CODUSU=pCODUSU,
ORGCTA=pORGCTA,
TIPCTA=pTIPCTA
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
    /// Obtêm o registro de conta virtual de acordo com o cpf/cnpj informado
    /// </summary>
        /// <param name="pCODCMF">Código do CPF/CNPJ</param>
    /// <param name="pORGCTA">Origem da Conta</param>
    /// <returns>VirtualAccount</returns>
    public VirtualAccount Select(System.String pCODCMF, System.Byte? pORGCTA)
        {
        this.Found=false;
            this.ProcessCode= 105;
    VirtualAccount RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE  = _conn.Query<VirtualAccount>(sql:"PRCADCTASELCMF", param:new {CODCMF=pCODCMF,
ORGCTA=pORGCTA
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
    /// Exclui logicamente um registro de conta
    /// </summary>
    /// <param name="pNIDCTA">Id da Conta</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <returns>ExecutionResponse</returns>
    public ExecutionResponse  DeleteAccountID( System.Int32 pNIDCTA,System.Int32 pUPDUSU)
    {
            int _AUDNUM =0;
            this.ProcessCode= 200;
            ExecutionResponse respond = new ExecutionResponse();
            if(pUPDUSU>0)
            {
            if(ThunderFire.Business.Support.GetUserAccess(GetAuthorizationID(),pUPDUSU)!=1)
            {
            respond.ErrorCode = "APPROVALUSERNOTALLOWED";
            respond.ErrorMessage = ErrorManager.GetStringMsg("APPROVALUSERNOTALLOWED");
        respond.ReturnValue=-3;
        return respond;
            }
            }
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
p.Add("@NIDCTA", pNIDCTA, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@UPDUSU", pUPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
RETURN_VALUE=_conn.ExecuteScalar<Int32>(sql:"PRCADCTADELNID", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
respond.ReturnValue= RETURN_VALUE;
string _errormessage="";
                if(RETURN_VALUE==0)
                {
                    respond.ErrorCode="FAILWRITE";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==1)
                {
                    Auditing audit = new Auditing();
            audit.UPDUSU =pUPDUSU;
            audit.AUDDAT=DateTime.Now;
            audit.AUDKEY =KeyTableId;
            audit.AUDIDR =pNIDCTA;
            audit.AUDIMS =0;
            audit.AUDTSK =this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            audit.AUDOBJ = "PRCADCTADELNID";
            audit.AUDSRC ="";
            audit.AUDCHG ="ALTEROU O STATUS DE REGISTRO DA CONTA PARA INATIVO  E O BLOQUEIO DA DA CONTA PARA OPERACOES";
            audit.NIDTOK =0;
            audit.NUMIPA =Environment.MachineName;
            _AUDNUM=WriteAuditing.Insert(audit);
            respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="CONTA EXCLUIDA COM SUCESSO";
_errormessage="";
                }
                if(RETURN_VALUE==-1)
                {
                    respond.ErrorCode="RECORDNOTFOUND";
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
    /// Executa a aprovação ou rejeição de uma conta digital
    /// </summary>
    /// <param name="pNIDCTA">ID da Conta</param>
    /// <param name="pCODOPE">Código da Operação</param>
    /// <param name="pUPDUSU">Usuário Aprovador</param>
    /// <returns>ExecutionResponse</returns>
    public ExecutionResponse  ChangeStatusAccount( System.Int32 pNIDCTA,System.Byte pCODOPE,System.Int32 pUPDUSU)
    {
            int _AUDNUM =0;
            this.ProcessCode= 205;
            ExecutionResponse respond = new ExecutionResponse();
            if(pUPDUSU>0)
            {
            if(ThunderFire.Business.Support.GetUserAccess(GetAuthorizationID(),pUPDUSU)!=1)
            {
            respond.ErrorCode = "APPROVALUSERNOTALLOWED";
            respond.ErrorMessage = ErrorManager.GetStringMsg("APPROVALUSERNOTALLOWED");
        respond.ReturnValue=-3;
        return respond;
            }
            }
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
p.Add("@NIDCTA", pNIDCTA, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@CODOPE", pCODOPE, dbType:DbType.Byte,  direction: ParameterDirection.Input);
p.Add("@UPDUSU", pUPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRCADCTACHGSTAACC", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
respond.ReturnValue= RETURN_VALUE;
string _errormessage="";
                if(RETURN_VALUE==0)
                {
                    respond.ErrorCode="APPROVALFAIL";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==1)
                {
                    Auditing audit = new Auditing();
            audit.UPDUSU =pUPDUSU;
            audit.AUDDAT=DateTime.Now;
            audit.AUDKEY =KeyTableId;
            audit.AUDIDR =pNIDCTA;
            audit.AUDIMS =0;
            audit.AUDTSK =this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
            audit.AUDOBJ = "PRCADCTADELNID";
            audit.AUDSRC ="";
            if(pCODOPE==1)
            audit.AUDCHG ="Aprovou o Registro de Conta";
            else{
            if(pCODOPE==3)
            audit.AUDCHG ="Reprovou o Registro de Conta";
            }
            audit.NIDTOK =0;
            audit.NUMIPA =Environment.MachineName;
            _AUDNUM=WriteAuditing.Insert(audit);
            respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="ATUALIZACAO EFETUADA COM SUCESSO";
_errormessage="";
                }
                if(RETURN_VALUE==-1)
                {
                    respond.ErrorCode="APPROVALFAIL";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser ="O ID DA CONTA NAO FOI FORNECIDO";
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
                    respond.ErrorCode="APPROVALUSERNOTALLOWED";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser=_errormessage;
_errormessage="";
                }
                if(RETURN_VALUE==-4)
                {
                    respond.ErrorCode="APPROVALFAIL";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser ="OPERACAO INVALIDA";
_errormessage="";
                }
                if(RETURN_VALUE==-5)
                {
                    respond.ErrorCode="APPROVALFAIL";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser ="O USUÁRIO NÃO ESTÁ OPERACIONAL PARA ESTA ACAO, VIDE O STATUS DO USUARIO";
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
    /// Localiza uma conta virtual a partir do código de usuário
    /// </summary>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pORGCTA">Origem da Conta</param>
    /// <param name="pTIPCTA">Tipo de Conta</param>
    /// <returns>ExecutionResponse</returns>
    public ExecutionResponse  Locate( System.Int32 pCODUSU,System.Byte pORGCTA = 1,System.Int32 pTIPCTA = 1)
    {
            int _AUDNUM =0;
            this.ProcessCode= 210;
            ExecutionResponse respond = new ExecutionResponse();
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
p.Add("@CODUSU", pCODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@ORGCTA", pORGCTA, dbType:DbType.Byte,  direction: ParameterDirection.Input);
p.Add("@TIPCTA", pTIPCTA, dbType:DbType.Int32,  direction: ParameterDirection.Input);
RETURN_VALUE=_conn.ExecuteScalar<Int32>(sql:"PRCADCTALOC", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
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

    /// <summary>
    /// Obtêm todos os registros de contas virtuais registradas conforme parametro fornecido
    /// </summary>
        /// <param name="pCODUSU">Usuário</param>
    /// <returns>Listof QueryVirtualAccount</returns>
    public List<QueryVirtualAccount> List(System.Int32? pCODUSU= null)
        {
            this.ProcessCode= 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            var result = _conn.Query<QueryVirtualAccount>(sql:"PRCADCTASELALL", param:new {CODUSU=pCODUSU},  commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();
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
