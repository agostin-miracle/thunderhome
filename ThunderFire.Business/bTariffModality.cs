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
    /// Regra de Negócio para TBMODTAR
    ///</summary>
    ///<remarks>
    ///Empresa     : Agostinho da Silva Milagres
    ///Copyright   : Copyright © 2012
    ///Descricao   : Gerador de Objetos
    ///Produto     : SQLDBTools
    ///Titulo      : SQLDBTools
    ///Version     : 1.3.0.0
    ///Data        : 15/03/2022 19:56
    ///Alias       : tariffmodality
    ///Descrição   : Tariff Modality
    ///</remarks>
    public partial class TariffModalityDao : BusinessBase, ITariffModality
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Instância Base
        /// </summary>
        public TariffModalityDao()
        {
            this.Connected = true;
            this.KeyTableId = 30;

        }

        /// <summary>
        /// Insere um registro na tabela TBMODTAR (Tariff Modality)
        /// </summary>
        ///<param name="model">TariffModality</param>
        /// <returns>int</returns>
        public ExecutionResponse Insert(TariffModality model)
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
                    string _changed = Objects.GetPropertiesValue("Tariff Modality", model, true);

                    var p = new DynamicParameters();
                    p.Add("@RETURN_VALUE", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@MODCRT", model.MODCRT, dbType: DbType.Int16, direction: ParameterDirection.Input);
                    p.Add("@DSCMOD", model.DSCMOD, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@PARINI", model.PARINI, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@PARFIN", model.PARFIN, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@STAREC", model.STAREC, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@UPDUSU", model.UPDUSU, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    _conn.Execute("PRMODTARINS", p, commandType: CommandType.StoredProcedure);
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
                        audit.AUDOBJ = "PRMODTARINS";
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
        /// Altera um registro da tabela TBMODTAR (Tariff Modality)  de acordo com a chave primaria
        /// </summary>
        ///<param name="model">TariffModality</param>
        /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(TariffModality model)
        {
            ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode = 10;
            this.HasError = false;
            TariffModality ModelAud = Select(model.MODCRT);
            string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Tariff Modality", model, true);

            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@RETURN_VALUE", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@MODCRT", model.MODCRT, dbType: DbType.Int16, direction: ParameterDirection.Input);
                    p.Add("@DSCMOD", model.DSCMOD, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@PARINI", model.PARINI, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@PARFIN", model.PARFIN, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@STAREC", model.STAREC, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@DATUPD", model.DATUPD, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                    p.Add("@UPDUSU", model.UPDUSU, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    _conn.Execute("PRMODTARUPD", p, commandType: CommandType.StoredProcedure);
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
                        audit.AUDOBJ = "PRMODTARUPD";
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
        /// Seleciona o registro de acordo com o codigo dA modalidade
        /// </summary>
        /// <param name="pMODCRT">Modalidade de Aplicação da Tarifa</param>
        /// <returns>TariffModality</returns>
        public TariffModality Select(int pMODCRT)
        {
            this.Found = false;
            this.ProcessCode = 100;
            TariffModality RETURN_VALUE = null;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    RETURN_VALUE = _conn.Query<TariffModality>(sql: "PRMODTARSEL", param: new
                    {
                        MODCRT = pMODCRT
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
        /// Seleciona todos os registros de modalidade de tarifa existentes
        /// </summary>
        /// <returns>Listof TariffModality</returns>
        public List<TariffModality> List()
        {
            this.ProcessCode = 115;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<TariffModality>(sql: "PRMODTARSELALL", param: new { }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();
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

    }
}
