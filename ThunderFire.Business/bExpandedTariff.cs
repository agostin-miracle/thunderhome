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
    /// Regra de Negócio para TBEXPTAR
    ///</summary>
    ///<remarks>
    ///Empresa     : Agostinho da Silva Milagres
    ///Copyright   : Copyright © 2012
    ///Descricao   : Gerador de Objetos
    ///Produto     : SQLDBTools
    ///Titulo      : SQLDBTools
    ///Version     : 1.3.0.0
    ///Data        : 14/03/2022 20:56
    ///Alias       : expandedtariff
    ///Descrição   : Expanded Tariff
    ///</remarks>
    public partial class ExpandedTariffDao : BusinessBase, IExpandedTariff
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Instância Base
        /// </summary>
        public ExpandedTariffDao()
        {
            this.Connected = true;
            this.KeyTableId = 27;

        }

        /// <summary>
        /// Insere um registro na tabela TBEXPTAR (Expanded Tariff)
        /// </summary>
        ///<param name="model">ExpandedTariff</param>
        /// <returns>int</returns>
        public ExecutionResponse Insert(ExpandedTariff model)
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
                    string _changed = Objects.GetPropertiesValue("Expanded Tariff", model, true);

                    var p = new DynamicParameters();
                    p.Add("@RETURN_VALUE", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@TIPEXP", model.TIPEXP, dbType: DbType.Int16, direction: ParameterDirection.Input);
                    p.Add("@LVLAPL", model.LVLAPL, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@VLRTAR", model.VLRTAR, dbType: DbType.Double, direction: ParameterDirection.Input);
                    p.Add("@VLRMIN", model.VLRMIN, dbType: DbType.Double, direction: ParameterDirection.Input);
                    p.Add("@VLRMAX", model.VLRMAX, dbType: DbType.Double, direction: ParameterDirection.Input);
                    p.Add("@STAREC", model.STAREC, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@UPDUSU", model.UPDUSU, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    _conn.Execute("PREXPTARINS", p, commandType: CommandType.StoredProcedure);
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
                        audit.AUDOBJ = "PREXPTARINS";
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
        /// Altera um registro da tabela TBEXPTAR (Expanded Tariff)  de acordo com a chave identity
        /// </summary>
        ///<param name="model">ExpandedTariff</param>
        /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(ExpandedTariff model)
        {
            ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode = 10;
            this.HasError = false;
            ExpandedTariff ModelAud = Select(model.NIDEXP);
            string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Expanded Tariff", model, true);

            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@RETURN_VALUE", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@NIDEXP", model.NIDEXP, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@TIPEXP", model.TIPEXP, dbType: DbType.Int16, direction: ParameterDirection.Input);
                    p.Add("@LVLAPL", model.LVLAPL, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@VLRTAR", model.VLRTAR, dbType: DbType.Double, direction: ParameterDirection.Input);
                    p.Add("@VLRMIN", model.VLRMIN, dbType: DbType.Double, direction: ParameterDirection.Input);
                    p.Add("@VLRMAX", model.VLRMAX, dbType: DbType.Double, direction: ParameterDirection.Input);
                    p.Add("@STAREC", model.STAREC, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@DATUPD", model.DATUPD, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                    p.Add("@UPDUSU", model.UPDUSU, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    _conn.Execute("PREXPTARUPD", p, commandType: CommandType.StoredProcedure);
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
                        audit.AUDOBJ = "PREXPTARUPD";
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
        /// <param name="pNIDEXP">ID do Registro de Expansão</param>
        /// <returns>ExpandedTariff</returns>
        public ExpandedTariff Select(int pNIDEXP)
        {
            this.Found = false;
            this.ProcessCode = 100;
            ExpandedTariff RETURN_VALUE = null;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT A.*, B.DSCEXP
      ,ISNULL(E.LGNUSU,'') LGNUSU 
    	,ISNULL(C.DSCTAB,'') DSCREC
  FROM TBEXPTAR (NOLOCK) A 
 INNER JOIN TBTIPEXP (NOLOCK) B ON (B.TIPEXP = A.TIPEXP)
 INNER JOIN TBTABGER (NOLOCK) C ON (C.NUMTAB = 7 AND C.KEYCOD = A.STAREC)
  LEFT JOIN TBLGNUSU (NOLOCK) E ON (E.CODUSU = B.UPDUSU AND E.REGATV=1 WHERE (A.NIDEXP=@NIDEXP)";
                    RETURN_VALUE = _conn.Query<ExpandedTariff>(sql: _sql, param: new
                    {
                        NIDEXP = pNIDEXP
                    }, commandType: CommandType.Text, commandTimeout: 120).FirstOrDefault();
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
        /// <param name="pTIPEXP">Código da Expansão</param>
        /// <returns>Listof ExpandedTariff</returns>
        public List<ExpandedTariff> List(System.Int16? pTIPEXP)
        {
            this.ProcessCode = 115;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<ExpandedTariff>(sql: "PREXPTARSELALL", param: new { TIPEXP = pTIPEXP }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();
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
        /// Obtêm uma Lista das Expansões Associadas
        /// </summary>
        /// <returns>Listof ExpandedTypes</returns>
        public List<ExpandedTypes> ListTypes()
        {
            this.ProcessCode = 110;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT TIPEXP, DSCEXP FROM TBTIPEXP (NOLOCK) WHERE STAREC IN (1,2)";
                    var result = _conn.Query<ExpandedTypes>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList(); this.Found = true;
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
