using System;
using System.Linq;
using ThunderFire;
using Dapper;
using NLog;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using ThunderFire.Connector;
using ThunderFire.Domain.Models;
using ThunderFire.Domain.DTO;
using ThunderFire.Interface;


namespace ThunderFire.Business
{

    ///<summary>
    /// Regra de Negócio para TBTARMOV
    ///</summary>
    ///<remarks>
    ///Empresa     : Agostinho da Silva Milagres
    ///Copyright   : Copyright © 2012
    ///Descricao   : Gerador de Objetos
    ///Produto     : SQLDBTools
    ///Titulo      : SQLDBTools
    ///Version     : 1.3.0.0
    ///Data        : 13/03/2022 14:03
    ///Alias       : tariffoperations
    ///Descrição   : Associação de Tarifas e Operações
    ///</remarks>
    public partial class TariffOperationsDao : BusinessBase, ITariffOperations
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Instância Base
        /// </summary>
        public TariffOperationsDao()
        {
            this.Connected = true;
            this.KeyTableId = 31;

        }

        /// <summary>
        /// Insere um registro na tabela TBTARMOV (Associação de Tarifas e Operações)
        /// </summary>
        ///<param name="model">TariffOperations</param>
        /// <returns>int</returns>
        public ExecutionResponse Insert(TariffOperations model)
        {
            ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.HasError = false;
            this.ProcessCode = 10;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _changed = Objects.GetPropertiesValue("Associação de Tarifas e Operações", model, true);

                    var p = new DynamicParameters();
                    p.Add("@RETURN_VALUE", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@CODTAR", model.CODTAR, dbType: DbType.Int16, direction: ParameterDirection.Input);
                    p.Add("@CODMOV", model.CODMOV, dbType: DbType.Int16, direction: ParameterDirection.Input);
                    p.Add("@IDEPRE", model.IDEPRE, dbType: DbType.Int16, direction: ParameterDirection.Input);
                    p.Add("@STAREC", model.STAREC, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@UPDUSU", model.UPDUSU, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    _conn.Execute("PRTARMOVINS", p, commandType: CommandType.StoredProcedure);
                    RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
                    respond.ReturnValue = RETURN_VALUE;
                    string _errormessage = "";
                    if (RETURN_VALUE > 0)
                    {
                        Auditing audit = new Auditing();
                        audit.UPDUSU = model.UPDUSU;
                        audit.AUDDAT = DateTime.Now;
                        audit.AUDKEY = KeyTableId;
                        audit.AUDIDR = RETURN_VALUE;
                        audit.AUDIMS = 0;
                        audit.AUDTSK = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
                        audit.AUDOBJ = "PRTARMOVINS";
                        audit.AUDSRC = _changed;
                        audit.AUDCHG = "";
                        audit.NIDTOK = 0;
                        audit.NUMIPA = Environment.MachineName;
                        _AUDNUM = WriteAuditing.Insert(audit);
                        respond.Logged = _AUDNUM > 0;
                        respond.MessageToUser = "REGISTRO INCLUIDO COM SUCESSO";
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -1)
                    {
                        respond.ErrorCode = "FAILALL";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -2)
                    {
                        respond.ErrorCode = "INVALIDARGUMENT";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        _errormessage = String.Format(_errormessage, "CODIGO DA TARIFA");
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -3)
                    {
                        respond.ErrorCode = "INVALIDARGUMENT";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        _errormessage = String.Format(_errormessage, "CODIGO DO MOVIMENTO");
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                }
                catch (Exception Error)
                {
                    _logger.Info(Error);
                    this.HasError = true;
                    respond.ReturnValue = RETURN_VALUE;
                    respond.StatusCode = 400;
                    respond.MessageToUser = "FALHA NA INCLUSAO DO REGISTRO";
                    respond.ErrorMessage = Error.Message;
                    var msg = ErrorManager.GetError(ErrorManager.GetErrorCode(Error.Message));
                    if (msg != null)
                    {
                        respond.SourceError = msg.Source;
                        respond.ErrorCode = msg.ErrorCode;
                        respond.ErrorObject = Error;
                        if (!String.IsNullOrEmpty(msg.Message))
                            respond.MessageToUser = msg.Message;
                        respond.Severity = msg.Severity;
                    }
                }
            }
            return respond;
        }

        /// <summary>
        /// Altera um registro da tabela TBTARMOV (Associação de Tarifas e Operações)  de acordo com a chave identity
        /// </summary>
        ///<param name="model">TariffOperations</param>
        /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(TariffOperations model)
        {
            ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode = 10;
            this.HasError = false;
            TariffOperations ModelAud = Select(model.NIDTXM);
            string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Associação de Tarifas e Operações", model, true);

            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@RETURN_VALUE", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@NIDTXM", model.NIDTXM, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@CODTAR", model.CODTAR, dbType: DbType.Int16, direction: ParameterDirection.Input);
                    p.Add("@CODMOV", model.CODMOV, dbType: DbType.Int16, direction: ParameterDirection.Input);
                    p.Add("@IDEPRE", model.IDEPRE, dbType: DbType.Int16, direction: ParameterDirection.Input);
                    p.Add("@STAREC", model.STAREC, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@DATUPD", model.DATUPD, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                    p.Add("@UPDUSU", model.UPDUSU, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    _conn.Execute("PRTARMOVUPD", p, commandType: CommandType.StoredProcedure);
                    RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
                    respond.ReturnValue = RETURN_VALUE;
                    string _errormessage = "";
                    if (RETURN_VALUE > 0)
                    {
                        Auditing audit = new Auditing();
                        audit.UPDUSU = model.UPDUSU;
                        audit.AUDDAT = DateTime.Now;
                        audit.AUDKEY = KeyTableId;
                        audit.AUDIDR = RETURN_VALUE;
                        audit.AUDIMS = 0;
                        audit.AUDTSK = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
                        audit.AUDOBJ = "PRTARMOVUPD";
                        audit.AUDSRC = _original;
                        audit.AUDCHG = _changed;
                        audit.NIDTOK = 0;
                        audit.NUMIPA = Environment.MachineName;
                        _AUDNUM = WriteAuditing.Insert(audit);
                        respond.Logged = _AUDNUM > 0;
                        respond.MessageToUser = "REGISTRO ALTERADO COM SUCESSO";
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -1)
                    {
                        respond.ErrorCode = "FAILALL";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -2)
                    {
                        respond.ErrorCode = "INVALIDARGUMENT";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        _errormessage = String.Format(_errormessage, "CODIGO DA TARIFA");
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -3)
                    {
                        respond.ErrorCode = "INVALIDARGUMENT";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        _errormessage = String.Format(_errormessage, "CODIGO DO MOVIMENTO");
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                }
                catch (Exception Error)
                {
                    _logger.Error(Error);
                    this.HasError = true;
                    respond.ReturnValue = RETURN_VALUE;
                    respond.StatusCode = 400;
                    respond.MessageToUser = "FALHA NA ALTERAÇÃO DO REGISTRO";
                    respond.ErrorMessage = Error.Message;
                    var msg = ErrorManager.GetError(ErrorManager.GetErrorCode(Error.Message));
                    if (msg != null)
                    {
                        respond.SourceError = msg.Source;
                        respond.ErrorCode = msg.ErrorCode;
                        respond.ErrorObject = Error;
                        if (!String.IsNullOrEmpty(msg.Message))
                            respond.MessageToUser = msg.Message;
                        respond.Severity = msg.Severity;
                    }
                }
            }
            return respond;
        }
        /// <summary>
        /// Seleciona o registro de acordo com o codigo do produto
        /// </summary>
        /// <param name="pNIDTXM">ID Tarifa x Movimento</param>
        /// <returns>TariffOperations</returns>
        public TariffOperations Select(int pNIDTXM)
        {
            this.Found = false;
            this.ProcessCode = 100;
            TariffOperations RETURN_VALUE = null;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    RETURN_VALUE = _conn.Query<TariffOperations>(sql: "PRTARMOVSEL", param: new
                    {
                        NIDTXM = pNIDTXM
                    }, commandType: CommandType.StoredProcedure, commandTimeout: 120).FirstOrDefault();

                    if (RETURN_VALUE != null)
                        this.Found = true;
                }
                catch (Exception Error)
                {
                    this.HasError = true;
                    this.Found = false;
                    _logger.Info(Error);
                }
            }
            return RETURN_VALUE;
        }

        /// <summary>
        /// Seleciona todos os registros de expansão de tarifa do id de tarifação fornecido
        /// </summary>
        /// <returns>Listof TariffOperations</returns>
        public List<TariffOperations> List()
        {
            this.ProcessCode = 115;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<TariffOperations>(sql: "PRTARMOVSELALL", param: new { }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();
                    this.Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    this.HasError = true;
                    this.Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm a Lista de Tarifas
        /// </summary>
        /// <returns>Listof TariffType</returns>
        public List<TariffType> ListTariffs()
        {
            this.ProcessCode = 125;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT CODTAR, DSCTAR FROM TBTIPTAR (NOLOCK) WHERE STAREC=1 ORDER BY DSCTAR";
                    var result = _conn.Query<TariffType>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList(); this.Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    this.HasError = true;
                    this.Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm a Lista de Operacoes
        /// </summary>
        /// <returns>Listof Operations</returns>
        public List<Operations> ListOperations()
        {
            this.ProcessCode = 130;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT CODMOV, DSCMOV FROM TBTIPMOV (NOLOCK) A WHERE STAREC=1 ORDER BY DSCMOV";
                    var result = _conn.Query<Operations>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList(); this.Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    this.HasError = true;
                    this.Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

    }
}
