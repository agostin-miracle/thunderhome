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
/// Regra de Negócio para TBTABGER
///</summary>
///<remarks>
///Empresa     : Agostinho da Silva Milagres
///Copyright   : Copyright © 2012
///Descricao   : Gerador de Objetos
///Produto     : SQLDBTools
///Titulo      : SQLDBTools
///Version     : 1.3.0.0
///Data        : 23/03/2022 14:58
///Alias       : generaltable
///Descrição   : Tabela Geral do Sistema
///</remarks>
    public partial class GeneralTableDao : BusinessBase, IGeneralTable
    {
private static Logger _logger = LogManager.GetCurrentClassLogger();
/// <summary>
/// Instância Base
/// </summary>
public GeneralTableDao()
{
this.Connected=true;
this.KeyTableId =4;

}
       
    /// <summary>
    /// Insere um registro na tabela TBTABGER (Tabela Geral do Sistema)
    /// </summary>
    ///<param name="model">GeneralTable</param>
    /// <returns>int</returns>
        public ExecutionResponse Insert(GeneralTable model)
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
string _changed = Objects.GetPropertiesValue("Tabela Geral do Sistema",model,true);

            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@NUMTAB", model.NUMTAB, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@USECOD", model.USECOD, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@KEYCOD", model.KEYCOD, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@KEYTXT", model.KEYTXT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMREF", model.NUMREF, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@DSCTAB", model.DSCTAB, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@USRDSP", model.USRDSP, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@IDEPRE", model.IDEPRE, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRTABGERINS", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRTABGERINS";
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
                    respond.MessageToUser ="A Tabela já contem um registro com a chave informada";
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
    /// Altera um registro da tabela TBTABGER (Tabela Geral do Sistema)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">GeneralTable</param>
    /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(GeneralTable model)
        {
ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode= 10;
            this.HasError =false;
                            GeneralTable ModelAud = Select(model.KEYTAB);
string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Tabela Geral do Sistema",model,true);

                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@KEYTAB", model.KEYTAB, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NUMTAB", model.NUMTAB, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@USECOD", model.USECOD, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@KEYCOD", model.KEYCOD, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@KEYTXT", model.KEYTXT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMREF", model.NUMREF, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@DSCTAB", model.DSCTAB, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@USRDSP", model.USRDSP, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@IDEPRE", model.IDEPRE, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATUPD", model.DATUPD, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRTABGERUPD", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRTABGERUPD";
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
    /// Seleciona o registro de acordo com o id de registro da tabela geral
    /// </summary>
        /// <param name="pKEYTAB">ID de Registro da Tabela</param>
    /// <returns>GeneralTable</returns>
    public GeneralTable Select(System.Int32 pKEYTAB)
        {
        this.Found=false;
            this.ProcessCode= 100;
    GeneralTable RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE  = _conn.Query<GeneralTable>(sql:"PRTABGERSEL", param:new {KEYTAB=pKEYTAB
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
    /// Obtêm o Id de Registro de uma Tabela Geral Baseada no Código Chave da Tabela
    /// </summary>
    /// <param name="pNUMTAB">Código do Tabela</param>
    /// <param name="pKEYCOD">Código Numérico da Chave</param>
    /// <returns>ExecutionResponse</returns>
    public ExecutionResponse  FindKey( System.Int32 pNUMTAB,System.Int32 pKEYCOD)
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
p.Add("@NUMTAB", pNUMTAB, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@KEYCOD", pKEYCOD, dbType:DbType.Int32,  direction: ParameterDirection.Input);
RETURN_VALUE=_conn.ExecuteScalar<Int32>(sql:"PRTABGERFNDKEY", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
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
    /// Obtêm o Id de Registro de uma Tabela Geral Baseada no Código Chave Texto da Tabela
    /// </summary>
    /// <param name="pNUMTAB">Código do Tabela</param>
    /// <param name="pKEYTXT">Código Texto da Chave</param>
    /// <returns>ExecutionResponse</returns>
    public ExecutionResponse  FindKeyText( System.Int32 pNUMTAB,System.String pKEYTXT)
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
p.Add("@NUMTAB", pNUMTAB, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@KEYTXT", pKEYTXT, dbType:DbType.String,  direction: ParameterDirection.Input);
RETURN_VALUE=_conn.ExecuteScalar<Int32>(sql:"PRTABGERFNDTXT", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
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
/// Seleciona todos os registros de um Tipo de tabela informado
/// </summary>
    /// <param name="pNUMTAB">Código da Tabela de Acesso</param>
/// <returns>Listof GeneralTable</returns>
    public List<GeneralTable> List(System.Int32? pNUMTAB)
        {
            this.ProcessCode= 105;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            var result = _conn.Query<GeneralTable>(sql:"PRTABGERCOD", param:new {NUMTAB=pNUMTAB},  commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();
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
