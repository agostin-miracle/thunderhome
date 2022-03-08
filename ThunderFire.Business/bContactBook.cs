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
/// Regra de Negócio para TBCADCTO
///</summary>
///<remarks>
///Empresa     : Agostinho da Silva Milagres
///Copyright   : Copyright © 2012
///Descricao   : Gerador de Objetos
///Produto     : SQLDBTools
///Titulo      : SQLDBTools
///Version     : 1.3.0.0
///Data        : 04/03/2022 13:35
///Alias       : contactbook
///Descrição   : Tabela de Contatos
///</remarks>
    public partial class ContactBookDao : BusinessBase, IContactBook
    {
private static Logger _logger = LogManager.GetCurrentClassLogger();
/// <summary>
/// Instância Base
/// </summary>
public ContactBookDao()
{
this.Connected=true;
this.KeyTableId =9;

}
       
    /// <summary>
    /// Insere um registro na tabela TBCADCTO (Tabela de Contatos)
    /// </summary>
    ///<param name="model">ContactBook</param>
    /// <returns>int</returns>
        public ExecutionResponse Insert(ContactBook model)
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
string _changed = Objects.GetPropertiesValue("Tabela de Contatos",model,true);

            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@CODUSU", model.CODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODEND", model.CODEND, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@TIPCTO", model.TIPCTO, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@REGATV", model.REGATV, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@CODPAI", model.CODPAI, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@CODOPR", model.CODOPR, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@NUMDDD", model.NUMDDD, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@ISWHAT", model.ISWHAT, dbType:DbType.Boolean,  direction: ParameterDirection.Input);
            p.Add("@NUMTEL", model.NUMTEL, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@DSCOBS", model.DSCOBS, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRCADCTOINS", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRCADCTOINS";
          audit.AUDSRC =_changed;
          audit.AUDCHG ="";
          audit.NIDTOK =0;
          audit.NUMIPA =Environment.MachineName;
          _AUDNUM=WriteAuditing.Insert(audit);
          respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="CONTATO INCLUIDO COM SUCESSO";
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
    /// Altera um registro da tabela TBCADCTO (Tabela de Contatos)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">ContactBook</param>
    /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(ContactBook model)
        {
ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode= 10;
            this.HasError =false;
                            ContactBook ModelAud = Select(model.CODCTO);
string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Tabela de Contatos",model,true);

                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@CODCTO", model.CODCTO, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODUSU", model.CODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODEND", model.CODEND, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@TIPCTO", model.TIPCTO, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@REGATV", model.REGATV, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@CODPAI", model.CODPAI, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@CODOPR", model.CODOPR, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@NUMDDD", model.NUMDDD, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@ISWHAT", model.ISWHAT, dbType:DbType.Boolean,  direction: ParameterDirection.Input);
            p.Add("@NUMTEL", model.NUMTEL, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@DSCOBS", model.DSCOBS, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATUPD", model.DATUPD, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRCADCTOUPD", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRCADCTOUPD";
          audit.AUDSRC =_original;
          audit.AUDCHG =_changed;
          audit.NIDTOK =0;
          audit.NUMIPA =Environment.MachineName;
          _AUDNUM=WriteAuditing.Insert(audit);
          respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="CONTATO ALTERADO COM SUCESSO";
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
    /// Seleciona o registro de acordo com o código do cadastro de contatos
    /// </summary>
        /// <param name="pCODCTO">ID do Registro de Contato</param>
    /// <returns>ContactBook</returns>
    public ContactBook Select(System.Int32 pCODCTO)
        {
        this.Found=false;
            this.ProcessCode= 0;
    ContactBook RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE  = _conn.Query<ContactBook>(sql:"PRCADCTOSEL", param:new {CODCTO=pCODCTO
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
    /// Obtêm o registro de contato de acordo com os parâmetros informados
    /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pTIPCTO">Tipo de Contato</param>
    /// <param name="pREGATV">Registro Ativo</param>
    /// <param name="pSTAREC">Status de Registro</param>
    /// <returns>ContactBook</returns>
    public ContactBook Select(System.Int32 pCODUSU, System.Byte pTIPCTO, System.Byte pREGATV= 1, System.Byte pSTAREC= 1)
        {
        this.Found=false;
            this.ProcessCode= 0;
    ContactBook RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE  = _conn.Query<ContactBook>(sql:"PRCADCTOSELCTO", param:new {CODUSU=pCODUSU,
TIPCTO=pTIPCTO,
REGATV=pREGATV,
STAREC=pSTAREC
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
    /// Localiza um contato com base nos parâmetros fornecidos
    /// </summary>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pCODEND">Código do Endereço</param>
    /// <param name="pTIPCTO">Tipo de Contato</param>
    /// <param name="pNUMTEL">Telefone</param>
    /// <param name="pCODPAI">Código do Pais</param>
    /// <param name="pCODOPR">Código da Operadora</param>
    /// <param name="pNUMDDD">Número do DDD</param>
    /// <param name="pREGATV">Indicador de Atividade do Registro</param>
    /// <returns>ExecutionResponse</returns>
    public ExecutionResponse  Find( System.Int32 pCODUSU,System.Int32 pCODEND,System.Int32 pTIPCTO,System.String pNUMTEL,System.Int16? pCODPAI,System.Int16? pCODOPR,System.String pNUMDDD,System.Byte? pREGATV)
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
p.Add("@CODUSU", pCODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@CODEND", pCODEND, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@TIPCTO", pTIPCTO, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@NUMTEL", pNUMTEL, dbType:DbType.String,  direction: ParameterDirection.Input);
p.Add("@CODPAI", pCODPAI, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@CODOPR", pCODOPR, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@NUMDDD", pNUMDDD, dbType:DbType.String,  direction: ParameterDirection.Input);
p.Add("@REGATV", pREGATV, dbType:DbType.Byte,  direction: ParameterDirection.Input);
RETURN_VALUE=_conn.ExecuteScalar<Int32>(sql:"PRCADCTOFIN", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
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
    /// Seleciona todos os registros de contato do usuário fornecido
    /// </summary>
        /// <param name="pCODUSU">Usuário</param>
    /// <returns>Listof QueryContactBook</returns>
    public List<QueryContactBook> List(System.Int32? pCODUSU)
        {
            this.ProcessCode= 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            var result = _conn.Query<QueryContactBook>(sql:"PRCADCTOSELALL", param:new {CODUSU=pCODUSU},  commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();
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
