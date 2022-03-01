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
///Data        : 01/03/2022 15:27
///Alias       : generalregistry
///Descrição   : Cadastro Geral
///</remarks>
    public partial class GeneralRegistryDao : BusinessBase, IGeneralRegistry
    {
private static Logger _logger = LogManager.GetCurrentClassLogger();
/// <summary>
/// Instância Base
/// </summary>
public GeneralRegistryDao()
{
this.Connected=true;
this.KeyTableId =1;

}
       
    /// <summary>
    /// Insere um registro na tabela TBCADGER (Cadastro Geral)
    /// </summary>
    ///<param name="model">GeneralRegistry</param>
    /// <returns>int</returns>
        public ExecutionResponse Insert(GeneralRegistry model)
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
string _changed = Objects.GetPropertiesValue("Cadastro Geral",model,true);

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
    /// Altera um registro da tabela TBCADGER (Cadastro Geral)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">GeneralRegistry</param>
    /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(GeneralRegistry model)
        {
ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode= 10;
            this.HasError =false;
                            GeneralRegistry ModelAud = Select(model.CODUSU);
string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Cadastro Geral",model,true);

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
    /// <returns>GeneralRegistry</returns>
    public GeneralRegistry Select(System.Int32 pCODUSU)
        {
        this.Found=false;
            this.ProcessCode= 0;
    GeneralRegistry RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE  = _conn.Query<GeneralRegistry>(sql:"PRCADGERSEL", param:new {CODUSU=pCODUSU
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
    /// Valida um CPF/CNPJ já existente para o mesmo atributo na base de cadastro geral
    /// </summary>
    /// <param name="pCODATR">Código do Atributo</param>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pCODCMF">CPF/CNPJ</param>
    /// <param name="pSRCUSU">Usuário Gestor</param>
    /// <returns>ExecutionResponse</returns>
    public ExecutionResponse  ValidateEntryCMF( short pCODATR,System.Int32 pCODUSU,System.String pCODCMF,System.Int32 pSRCUSU)
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
p.Add("@CODATR", pCODATR, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@CODUSU", pCODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@CODCMF", pCODCMF, dbType:DbType.String,  direction: ParameterDirection.Input);
p.Add("@SRCUSU", pSRCUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRCADGERVALENTRCMF", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
respond.ReturnValue= RETURN_VALUE;
string _errormessage="";
                if(RETURN_VALUE>0)
                {
                    respond.MessageToUser ="FORAM ENCONTRADAS OCORRENCIAS DUPLICADAS";
_errormessage="";
                }
                if(RETURN_VALUE==0)
                {
                    respond.MessageToUser ="NAO FOI ENCONTRADA NENHUMA OCORRENCIA";
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
    /// Verifica se já existe um cadastro com o CPF/CNPJ cadastrado, e retorna o id localizado
    /// </summary>
    /// <param name="pCODATR">Código do Atributo</param>
    /// <param name="pCODCMF">CPF/CNPJ</param>
    /// <returns>ExecutionResponse</returns>
    public ExecutionResponse  PesquisarCMF( System.Int16 pCODATR,System.String pCODCMF)
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
p.Add("@CODATR", pCODATR, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@CODCMF", pCODCMF, dbType:DbType.String,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRCADUSUPSQCMF", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
respond.ReturnValue= RETURN_VALUE;
string _errormessage="";
                if(RETURN_VALUE>0)
                {
                    respond.MessageToUser ="O CPF/CNPJ JA EXISTE";
_errormessage="";
                }
                if(RETURN_VALUE==0)
                {
                    respond.MessageToUser ="NAO FOI ENCONTRADA NENHUMA OCORRENCIA";
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
    /// Obtêm uma lista de registros do cadastro geral conforme parâmetros informados
    /// </summary>
        /// <param name="pCODATR">Atributo</param>
    /// <param name="pSTAUSU">Status do Usuário</param>
    /// <param name="pSRCUSU">ID do Responsável</param>
    /// <param name="pNOMUSU">Nome</param>
    /// <param name="pSTAREC">Status do Registro</param>
    /// <returns>Listof QueryGeneralRegistry</returns>
    public List<QueryGeneralRegistry> List(System.Int16? pCODATR, System.Int16? pSTAUSU, System.Int32? pSRCUSU, string pNOMUSU, System.Byte? pSTAREC)
        {
            this.ProcessCode= 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            var result = _conn.Query<QueryGeneralRegistry>(sql:"PRCADGERSELALL", param:new {CODATR=pCODATR,STAUSU=pSTAUSU,SRCUSU=pSRCUSU,NOMUSU=pNOMUSU,STAREC=pSTAREC},  commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();
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

    /// <summary>
    /// Obtêm uma lista de registros do cadastro geral conforme parâmetros informados
    /// </summary>
        /// <param name="pCODATR">Atributo</param>
    /// <param name="pSTAUSU">Status do Usuário</param>
    /// <param name="pSRCUSU">ID do Responsável</param>
    /// <param name="pNOMUSU">Nome</param>
    /// <param name="pSTAREC">Status do Registro</param>
    /// <returns>Listof QueryGeneralRegistry</returns>
    /// <summary>
    /// Obtêm uma lista de usuários com contas virtuais ativas
    /// </summary>
    /// <returns>Listof GeneralRegistry</returns>
    public List<GeneralRegistry> ListUserAccounts()
        {
            this.ProcessCode= 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            var result = _conn.Query<GeneralRegistry>(sql:"PRCADGERSELCTA", param:new {},  commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();
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
