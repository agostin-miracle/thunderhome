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
/// Regra de Negócio para TBCADTAR
///</summary>
///<remarks>
///Empresa     : Agostinho da Silva Milagres
///Copyright   : Copyright © 2012
///Descricao   : Gerador de Objetos
///Produto     : SQLDBTools
///Titulo      : SQLDBTools
///Version     : 1.3.0.0
///Data        : 30/03/2022 16:41
///Alias       : tariff
///Descrição   : Tariffs
///</remarks>
    public partial class TariffDao : BusinessBase, ITariff
    {
private static Logger _logger = LogManager.GetCurrentClassLogger();
/// <summary>
/// Instância Base
/// </summary>
public TariffDao()
{
this.Connected=true;
this.KeyTableId =32;

}
       

        /// <summary>
        /// ID de Registro da Memória de Cálculo
        /// </summary>
public System.Int32 NIDCAL { get;  set; }

    /// <summary>
    /// Insere um registro na tabela TBCADTAR (Tariffs)
    /// </summary>
    ///<param name="model">Tariff</param>
    /// <returns>int</returns>
        public ExecutionResponse Insert(Tariff model)
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
string _changed = Objects.GetPropertiesValue("Tariffs",model,true);

            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@NIVCFG", model.NIVCFG, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@USUCFG", model.USUCFG, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODTAR", model.CODTAR, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@CODBND", model.CODBND, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@TIPLIN", model.TIPLIN, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@MODCRT", model.MODCRT, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@CODEXP", model.CODEXP, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@CODMOE", model.CODMOE, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@FMTCOB", model.FMTCOB, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@RSPTAR", model.RSPTAR, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@TARBAS", model.TARBAS, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@TARMAX", model.TARMAX, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@PCTTAR", model.PCTTAR, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@VLRTAR", model.VLRTAR, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@VLRINF", model.VLRINF, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@VLRMAX", model.VLRMAX, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRCADTARINS", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRCADTARINS";
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
    /// Altera um registro da tabela TBCADTAR (Tariffs)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">Tariff</param>
    /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(Tariff model)
        {
ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode= 10;
            this.HasError =false;
                            Tariff ModelAud = Select(model.NIDTAR);
string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Tariffs",model,true);

                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@NIDTAR", model.NIDTAR, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NIVCFG", model.NIVCFG, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@USUCFG", model.USUCFG, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODTAR", model.CODTAR, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@CODBND", model.CODBND, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@TIPLIN", model.TIPLIN, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@MODCRT", model.MODCRT, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@CODEXP", model.CODEXP, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@CODMOE", model.CODMOE, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@FMTCOB", model.FMTCOB, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@RSPTAR", model.RSPTAR, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@TARBAS", model.TARBAS, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@TARMAX", model.TARMAX, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@PCTTAR", model.PCTTAR, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@VLRTAR", model.VLRTAR, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@VLRINF", model.VLRINF, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@VLRMAX", model.VLRMAX, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATUPD", model.DATUPD, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRCADTARUPD", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRCADTARUPD";
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
    /// Obtêm o Id de Tarifação Tarifação de Acordo com o ID de Registro de Tarifação fornecido
    /// </summary>
        /// <param name="pNIDTAR">ID do Registro de Tarifação</param>
    /// <returns>Tariff</returns>
    public Tariff Select(int pNIDTAR)
        {
        this.Found=false;
            this.ProcessCode= 100;
    Tariff RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE  = _conn.Query<Tariff>(sql:"PRCADTARSEL", param:new {NIDTAR=pNIDTAR
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
    /// Obtêm a tarifa expandida de registro de tarifação
    /// </summary>
/// <remarks>
/// <para>Retorna o id de Registro de Aplicação de uma Tarifa [TBCALTAR]</para>
/// </remarks>
    /// <param name="pNIDTAR">ID do Registro de Tarifação</param>
    /// <param name="pVLRTRA">Valor da Transação</param>
    /// <returns>int</returns>
    public int GetExpandeTariff( System.Int32 pNIDTAR,System.Double pVLRTRA = 0)
    {
            this.ProcessCode= 0;
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.ReturnValue);
p.Add("@NIDTAR", pNIDTAR, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@VLRTRA", pVLRTRA, dbType:DbType.Decimal,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRCADTARGETEXPTAR", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
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
/// Seleciona todos os registros com base nos parâmetros fornecidos
/// </summary>
    /// <param name="pUSUCFG">Código do Usuario</param>
    /// <param name="pNIVCFG">Nivel de Tarifação</param>
/// <returns>Listof QueryTariff</returns>
    public List<QueryTariff> List(int pUSUCFG, System.Byte pNIVCFG)
        {
            this.ProcessCode= 120;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            var result = _conn.Query<QueryTariff>(sql:"PRCADTARSELALL", param:new {USUCFG=pUSUCFG,NIVCFG=pNIVCFG},  commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();
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
