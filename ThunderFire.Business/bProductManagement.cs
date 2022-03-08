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
/// Regra de Negócio para TBUSUPRO
///</summary>
///<remarks>
///Empresa     : Agostinho da Silva Milagres
///Copyright   : Copyright © 2012
///Descricao   : Gerador de Objetos
///Produto     : SQLDBTools
///Titulo      : SQLDBTools
///Version     : 1.3.0.0
///Data        : 08/03/2022 11:54
///Alias       : productmanagement
///Descrição   : Gerencia de Produtos
///</remarks>
    public partial class ProductManagementDao : BusinessBase, IProductManagement
    {
private static Logger _logger = LogManager.GetCurrentClassLogger();
/// <summary>
/// Instância Base
/// </summary>
public ProductManagementDao()
{
this.Connected=true;
this.KeyTableId =17;

}
       
    /// <summary>
    /// Insere um registro na tabela TBUSUPRO (Gerencia de Produtos)
    /// </summary>
    ///<param name="model">ProductManagement</param>
    /// <returns>int</returns>
        public ExecutionResponse Insert(ProductManagement model)
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
string _changed = Objects.GetPropertiesValue("Gerencia de Produtos",model,true);

            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@USUPRO", model.USUPRO, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODUSU", model.CODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODPRO", model.CODPRO, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@APLINT", model.APLINT, dbType:DbType.Boolean,  direction: ParameterDirection.Input);
            p.Add("@VLRMIN", model.VLRMIN, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@VLRMAX", model.VLRMAX, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@APLRVC", model.APLRVC, dbType:DbType.Boolean,  direction: ParameterDirection.Input);
            p.Add("@REGVCT", model.REGVCT, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@APLCES", model.APLCES, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@APLTAD", model.APLTAD, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@APLMEN", model.APLMEN, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@VLRMEN", model.VLRMEN, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRUSUPROINS", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRUSUPROINS";
          audit.AUDSRC ="";
          audit.AUDCHG =_changed;
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
    /// 
    /// </summary>
    ///<param name="model">ProductManagement</param>
    /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(ProductManagement model)
        {
ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode= 10;
            this.HasError =false;
                            ProductManagement ModelAud = Select(model.USUPRO);
string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Gerencia de Produtos",model,true);

                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@USUPRO", model.USUPRO, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODUSU", model.CODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODPRO", model.CODPRO, dbType:DbType.Int16,  direction: ParameterDirection.Input);
            p.Add("@APLINT", model.APLINT, dbType:DbType.Boolean,  direction: ParameterDirection.Input);
            p.Add("@VLRMIN", model.VLRMIN, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@VLRMAX", model.VLRMAX, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@APLRVC", model.APLRVC, dbType:DbType.Boolean,  direction: ParameterDirection.Input);
            p.Add("@REGVCT", model.REGVCT, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@APLCES", model.APLCES, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@APLTAD", model.APLTAD, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@APLMEN", model.APLMEN, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@VLRMEN", model.VLRMEN, dbType:DbType.Double,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATUPD", model.DATUPD, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRUSUPROUPD", p,commandType: CommandType.StoredProcedure);
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
          audit.AUDOBJ = "PRUSUPROUPD";
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
    /// Obtêm o registro de Gerencia de Produto de acordo com ocódigo do gestor
    /// </summary>
        /// <param name="pUSUPRO">ID do Gestor</param>
    /// <returns>ProductManagement</returns>
    public ProductManagement Select(System.Int32 pUSUPRO)
        {
        this.Found=false;
            this.ProcessCode= 105;
    ProductManagement RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE  = _conn.Query<ProductManagement>(sql:"PRUSUPROSEL", param:new {USUPRO=pUSUPRO
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
    /// Obtêm o Código do Gestor de Produto com base no produto e usuário
    /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pCODPRO">Código do Produto</param>
    /// <returns>ProductManagement</returns>
    public ProductManagement Select(System.Int32 pCODUSU, System.Int16 pCODPRO)
        {
        this.Found=false;
            this.ProcessCode= 110;
    ProductManagement RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            RETURN_VALUE  = _conn.Query<ProductManagement>(sql:"PRUSUPROLOC", param:new {CODUSU=pCODUSU,
CODPRO=pCODPRO
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
    /// Obtêm uma lista de gestores, se informada a linha de produto, extrai somente os gestores ligados à linha
    /// </summary>
        /// <param name="pLINPRO">Linha de Produto</param>
    /// <returns>Listof MyUsers</returns>
    public List<MyUsers> List(System.Int16? pLINPRO)
        {
            this.ProcessCode= 110;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            var result = _conn.Query<MyUsers>(sql:"PRUSUPROSELGSTLIN", param:new {LINPRO=pLINPRO},  commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();
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
    /// Obtêm uma lista de gestores, se informada a linha de produto, extrai somente os gestores ligados à linha
    /// </summary>
        /// <param name="pLINPRO">Linha de Produto</param>
    /// <returns>Listof MyUsers</returns>
    /// <summary>
    /// Obtêm uma lista de todos os registros de Gestores de Produtos de acordo com o usuario e/ou  o produto informado
    /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pCODPRO">Código do Produto</param>
    /// <returns>Listof QueryProductManagement</returns>
    public List<QueryProductManagement> List(System.Int32? pCODUSU= null, System.Int16? pCODPRO= null)
        {
            this.ProcessCode= 115;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
            var result = _conn.Query<QueryProductManagement>(sql:"PRUSUPROSELALL", param:new {CODUSU=pCODUSU,CODPRO=pCODPRO},  commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();
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
