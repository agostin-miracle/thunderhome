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
/// Regra de Negócio para TBTIPEXP
///</summary>
///<remarks>
///Empresa     : Agostinho da Silva Milagres
///Copyright   : Copyright © 2012
///Descricao   : Gerador de Objetos
///Produto     : SQLDBTools
///Titulo      : SQLDBTools
///Version     : 1.3.0.0
///Data        : 14/03/2022 20:25
///Alias       : expandedtypes
///Descrição   : Tipo de Expansão de Tarifa
///</remarks>
    public partial class ExpandedTypesDao : BusinessBase, IExpandedTypes
    {
private static Logger _logger = LogManager.GetCurrentClassLogger();
/// <summary>
/// Instância Base
/// </summary>
public ExpandedTypesDao()
{
this.Connected=true;
this.KeyTableId =28;

}
       
    /// <summary>
    /// Insere um registro na tabela TBTIPEXP (Tipo de Expansão de Tarifa)
    /// </summary>
    ///<param name="model">ExpandedTypes</param>
    /// <returns>int</returns>
        public ExecutionResponse Insert(ExpandedTypes model)
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
string _changed = Objects.GetPropertiesValue("Tipo de Expansão de Tarifa",model,true);

            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@TIPEXP", model.TIPEXP, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@DSCEXP", model.DSCEXP, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@LVLREG", model.LVLREG, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRTIPEXPINS", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRTIPEXPINS";
          audit.AUDSRC =_changed;
          audit.AUDCHG ="";
          audit.NIDTOK =0;
          audit.NUMIPA =Environment.MachineName;
          _AUDNUM=WriteAuditing.Insert(audit);
          respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="TIPO DE EXPANSAO INCLUIDO COM SUCESSO";
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
                    respond.ErrorCode="ATTRIBUTEFOUNDED";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser ="O NOME FORNECIDO COMO ARGUMENTO JÁ ESTÁ CADASTRADO";
_errormessage="";
                }
                if(RETURN_VALUE==-4)
                {
                    respond.ErrorCode="INVALIDARGUMENT";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
_errormessage= String.Format(_errormessage,"REGRA DE APLICACAO");
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
    /// Altera um registro da tabela TBTIPEXP (Tipo de Expansão de Tarifa)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">ExpandedTypes</param>
    /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(ExpandedTypes model)
        {
ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode= 10;
            this.HasError =false;
                            ExpandedTypes ModelAud = Select(model.TIPEXP);
string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Tipo de Expansão de Tarifa",model,true);

                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@TIPEXP", model.TIPEXP, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@DSCEXP", model.DSCEXP, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@LVLREG", model.LVLREG, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATUPD", model.DATUPD, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRTIPEXPUPD", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRTIPEXPUPD";
          audit.AUDSRC =_original;
          audit.AUDCHG =_changed;
          audit.NIDTOK =0;
          audit.NUMIPA =Environment.MachineName;
          _AUDNUM=WriteAuditing.Insert(audit);
          respond.Logged=_AUDNUM>0;
                    respond.MessageToUser ="TIPO DE EXPANSAO ALTERADA COM SUCESSO";
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
                    respond.ErrorCode="ATTRIBUTEFOUNDED";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser ="O NOME FORNECIDO COMO ARGUMENTO JÁ ESTÁ CADASTRADO";
_errormessage="";
                }
                if(RETURN_VALUE==-4)
                {
                    respond.ErrorCode="INVALIDARGUMENT";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
_errormessage= String.Format(_errormessage,"REGRA DE APLICACAO");
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
    /// Obtêm o registro de Tipo de Expansão com base no id informado
    /// </summary>
        /// <param name="pTIPEXP">Código do Tipo de Expansão</param>
    /// <returns>ExpandedTypes</returns>
    public ExpandedTypes Select(System.Int16 pTIPEXP)
        {
        this.Found=false;
            this.ProcessCode= 100;
    ExpandedTypes RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
string _sql = @"SELECT A.*, DSCREC= ISNULL(B.DSCTAB,''), LGNUSU = ISNULL(C.LGNUSU,''), DSCREG = D.DSCTAB
FROM TBTIPEXP (NOLOCK) A
INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB =7 AND B.KEYCOD=A.STAREC)
 LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV =1)
INNER JOIN TBTABGER (NOLOCK) D ON (D.NUMTAB =94 AND D.KEYCOD=A.LVLREG) WHERE (A.TIPEXP=@TIPEXP)";
 RETURN_VALUE = _conn.Query<ExpandedTypes>(sql:_sql, param:new {TIPEXP=pTIPEXP
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
/// Obtêm uma Lista dos Tipos de Expansão
/// </summary>
/// <returns>Listof ExpandedTypes</returns>
    public List<ExpandedTypes> List()
        {
            this.ProcessCode= 105;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            var result = _conn.Query<ExpandedTypes>(sql:"PRTIPEXPSELALL", param:new {},  commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();
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
/// Obtêm uma Lista das Regras de Expansão
/// </summary>
/// <returns>Listof GeneralTable</returns>
    public List<GeneralTable> ListRules()
        {
            this.ProcessCode= 110;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
string _sql = @"SELECT KEYCOD, DSCTAB FROM TBTABGER (NOLOCK) A WHERE NUMTAB=94 AND STAREC IN (1,2) ORDER BY A.KEYCOD";
var result = _conn.Query<GeneralTable>(sql:_sql, param:new {},  commandType: CommandType.Text, commandTimeout: 120).ToList();                    this.Found = true;
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
