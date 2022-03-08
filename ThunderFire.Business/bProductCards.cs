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
/// Regra de Negócio para TBCADCRT
///</summary>
///<remarks>
///Empresa     : Agostinho da Silva Milagres
///Copyright   : Copyright © 2012
///Descricao   : Gerador de Objetos
///Produto     : SQLDBTools
///Titulo      : SQLDBTools
///Version     : 1.3.0.0
///Data        : 04/03/2022 15:33
///Alias       : productcards
///Descrição   : Cadastro de Cartões
///</remarks>
    public partial class ProductCardsDao : BusinessBase, IProductCards
    {
private static Logger _logger = LogManager.GetCurrentClassLogger();
/// <summary>
/// Instância Base
/// </summary>
public ProductCardsDao()
{
this.Connected=true;
this.KeyTableId =18;

}
       
    /// <summary>
    /// Insere um registro na tabela TBCADCRT (Cadastro de Cartões)
    /// </summary>
    ///<param name="model">ProductCards</param>
    /// <returns>int</returns>
        public ExecutionResponse Insert(ProductCards model)
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
string _changed = Objects.GetPropertiesValue("Cadastro de Cartões",model,true);

            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@CODCRT", model.CODCRT, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@USUGST", model.USUGST, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@KEYACC", model.KEYACC, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@KEYCRT", model.KEYCRT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NIDLIM", model.NIDLIM, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NUMLOT", model.NUMLOT, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NUMCRT", model.NUMCRT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@VALCRT", model.VALCRT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMCVC", model.NUMCVC, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@STACRT", model.STACRT, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@PSWCRT", model.PSWCRT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@FILNAM", model.FILNAM, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@CODBLO", model.CODBLO, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@CODFOR", model.CODFOR, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@LOTMIG", model.LOTMIG, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRCADCRTINS", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRADMCRTINS";
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
    /// Efetua a migração de Registros da tabela TBCADCRT (Cadastro de Cartões)
    /// </summary>
    ///<param name="model">ActiveCards</param>
    /// <returns>int</returns>
        public ExecutionResponse MigrateTo(ActiveCards model)
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
string _changed = Objects.GetPropertiesValue("Cadastro de Cartões",model,true);

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
p.Add("@APLCON", model.APLCON, dbType:DbType.Boolean,  direction: ParameterDirection.Input);
p.Add("@APLSAQ", model.APLSAQ, dbType:DbType.Boolean,  direction: ParameterDirection.Input);
p.Add("@LOTMIG", model.LOTMIG, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRCADCRTREGCRT", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRADMCRTINS";
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
    /// Altera um registro da tabela TBCADCRT (Cadastro de Cartões)  de acordo com a chave primaria
    /// </summary>
    ///<param name="model">ProductCards</param>
    /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(ProductCards model)
        {
ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode= 10;
            this.HasError =false;
                            ProductCards ModelAud = Select(model.CODCRT);
string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Cadastro de Cartões",model,true);

                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@CODCRT", model.CODCRT, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@USUGST", model.USUGST, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@KEYACC", model.KEYACC, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@KEYCRT", model.KEYCRT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NIDLIM", model.NIDLIM, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NUMLOT", model.NUMLOT, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NUMCRT", model.NUMCRT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@VALCRT", model.VALCRT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@NUMCVC", model.NUMCVC, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@STACRT", model.STACRT, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@PSWCRT", model.PSWCRT, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@FILNAM", model.FILNAM, dbType:DbType.String,  direction: ParameterDirection.Input);
            p.Add("@CODBLO", model.CODBLO, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@CODFOR", model.CODFOR, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@LOTMIG", model.LOTMIG, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATUPD", model.DATUPD, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRCADCRTUPD", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRADMCRTUPD";
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
    /// <returns>ProductCards</returns>
    public ProductCards Select(System.Int32 pCODCRT)
        {
        this.Found=false;
            this.ProcessCode= 0;
    ProductCards RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE  = _conn.Query<ProductCards>(sql:"PRADMCRTSEL", param:new {CODCRT=pCODCRT
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
    /// Seleciona o registro de acordo com o Código Extendido do Cartão
    /// </summary>
        /// <param name="pNUMCRT">Código Extendido do Cartão</param>
    /// <returns>ProductCards</returns>
    public ProductCards Select(System.String pNUMCRT)
        {
        this.Found=false;
            this.ProcessCode= 0;
    ProductCards RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE  = _conn.Query<ProductCards>(sql:"PRADMCRTSELCRT", param:new {NUMCRT=pNUMCRT
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
    /// Verifica se o cartão já foi alocado para um portador
    /// </summary>
/// <remarks>
/// <para>ADM : Verifica se o cartão pode ser alocado, retorna :</para>
/// <para>[-1] - indicado que o cartão não possui uma alocação.</para>
/// <para>[ 1] - Indicando que o cartão possui uma alocação.</para>
/// <para>[ 0] - Se o cartão não foi encontrado</para>
/// </remarks>
    /// <param name="pCODCRT">ID do Cartão</param>
    /// <returns>int</returns>
    public int IsAlocated( System.Int32 pCODCRT)
    {
            this.ProcessCode= 0;
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
p.Add("@CODCRT", pCODCRT, dbType:DbType.Int32,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRADMCRTGETATV", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
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
    /// ADM : Altera o Status de um Cartão
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
_conn.Execute(sql:"PRADMCRTCHGSTA", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
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
                    respond.MessageToUser ="STATUS DO CARTAO ALTRADO COM SUCESSO";
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
_conn.Execute(sql:"PRADMCRTCHGPSW", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
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
_conn.Execute(sql:"PRADMCRTCHGCVC", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
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
    /// Efetua o cancelamento da associação de um cartão
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
