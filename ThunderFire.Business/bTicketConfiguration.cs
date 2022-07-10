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
/// Regra de Negócio para TBCFGBOL
///</summary>
///<remarks>
///Empresa     : Agostinho da Silva Milagres
///Copyright   : Copyright © 2012
///Descricao   : Gerador de Objetos
///Produto     : SQLDBTools
///Titulo      : SQLDBTools
///Version     : 1.3.0.0
///Data        : 07/04/2022 13:33
///Alias       : ticketconfiguration
///Descrição   : Ticket Configuration
///</remarks>
    public partial class TicketConfigurationDao : BusinessBase, ITicketConfiguration
    {
private static Logger _logger = LogManager.GetCurrentClassLogger();
/// <summary>
/// Instância Base
/// </summary>
public TicketConfigurationDao()
{
this.Connected=true;
this.KeyTableId =29;

}
       
    /// <summary>
    /// Insere um registro na tabela TBCFGBOL (Ticket Configuration)
    /// </summary>
    ///<param name="model">TicketConfiguration</param>
    /// <returns>int</returns>
        public ExecutionResponse Insert(TicketConfiguration model)
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
string _changed = Objects.GetPropertiesValue("Ticket Configuration",model,true);

            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@USUCFG", model.USUCFG, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NIVCFG", model.NIVCFG, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@TIPBOL", model.TIPBOL, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@APLTAR", model.APLTAR, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@CODTAR", model.CODTAR, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@APLTDP", model.APLTDP, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@TARTDP", model.TARTDP, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@TIPEXT", model.TIPEXT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@INSBC1", model.INSBC1, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@INSBC2", model.INSBC2, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@INSBC3", model.INSBC3, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRCFGBOLINS", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRCFGBOLINS";
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
    /// Altera um registro da tabela TBCFGBOL (Ticket Configuration)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">TicketConfiguration</param>
    /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(TicketConfiguration model)
        {
ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode= 10;
            this.HasError =false;
                            TicketConfiguration ModelAud = Select(model.NIDCBL);
string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Ticket Configuration",model,true);

                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@NIDCBL", model.NIDCBL, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@USUCFG", model.USUCFG, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NIVCFG", model.NIVCFG, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@TIPBOL", model.TIPBOL, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@APLTAR", model.APLTAR, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@CODTAR", model.CODTAR, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@APLTDP", model.APLTDP, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@TARTDP", model.TARTDP, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@TIPEXT", model.TIPEXT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@INSBC1", model.INSBC1, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@INSBC2", model.INSBC2, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@INSBC3", model.INSBC3, dbType:DbType.String,  direction: ParameterDirection.Input);
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
          audit.AUDOBJ = "PRCFGBOLUPD";
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
    /// Obtêm o registro de configuração de boleto conforme o id informado
    /// </summary>
        /// <param name="pNIDCBL">ID do Registro de Configuração de Boleto</param>
    /// <returns>TicketConfiguration</returns>
    public TicketConfiguration Select(System.Int32 pNIDCBL)
        {
        this.Found=false;
            this.ProcessCode= 100;
    TicketConfiguration RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE  = _conn.Query<TicketConfiguration>(sql:"PRCFGBOLSEL", param:new {NIDCBL=pNIDCBL
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
    /// Localiza um id de registro de configuração de boleto de acordo com os parâmetros fornecidos
    /// </summary>
/// <remarks>
/// <para>A busca inicial procura pelo usuário cedente do boleto, no nivel de configuração usuário, caso não encontre, procura pelo Código do Gestor no nivel de gestor, caso não encontre, procura pelo padrão</para>
/// </remarks>
    /// <param name="pUSUPRO">Código do Gestor</param>
    /// <param name="pUSUCED">Código do Cedente</param>
    /// <param name="pTIPBOL">Tipo de Boleto</param>
    /// <returns>int</returns>
    public int Locate( System.Int32 pUSUPRO,System.Int32 pUSUCED,System.Byte pTIPBOL)
    {
            this.ProcessCode= 200;
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.ReturnValue);
p.Add("@USUPRO", pUSUPRO, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@USUCED", pUSUCED, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@TIPBOL", pTIPBOL, dbType:DbType.Byte,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRCFGBOLOCREC", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
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
