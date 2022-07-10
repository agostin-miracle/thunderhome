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
    /// Regra de Negócio para TBTIPTAR
    ///</summary>
    ///<remarks>
    ///Empresa     : Agostinho da Silva Milagres
    ///Copyright   : Copyright © 2012
    ///Descricao   : Gerador de Objetos
    ///Produto     : SQLDBTools
    ///Titulo      : SQLDBTools
    ///Version     : 1.3.0.0
    ///Data        : 13/03/2022 10:27
    ///Alias       : tarifftype
    ///Descrição   : Tariff Type
    ///</remarks>
    public partial class TariffTypeDao : BusinessBase, ITariffType
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Instância Base
        /// </summary>
        public TariffTypeDao()
        {
            this.Connected = true;
            this.KeyTableId = 25;

        }

        /// <summary>
        /// Insere um registro na tabela TBTIPTAR (Tariff Type)
        /// </summary>
        ///<param name="model">TariffType</param>
        /// <returns>int</returns>
        public ExecutionResponse Insert(TariffType model)
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
                    string _changed = Objects.GetPropertiesValue("Tariff Type", model, true);

                    var p = new DynamicParameters();
                    p.Add("@RETURN_VALUE", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@CODTAR", model.CODTAR, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@DSCTAR", model.DSCTAR, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@CODMOV", model.CODMOV, dbType: DbType.Int16, direction: ParameterDirection.Input);
                    p.Add("@STAREC", model.STAREC, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@UPDUSU", model.UPDUSU, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    _conn.Execute("PRTIPTARINS", p, commandType: CommandType.StoredProcedure);
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
                        audit.AUDOBJ = "PRTIPTARINS";
                        audit.AUDSRC = _changed;
                        audit.AUDCHG = "";
                        audit.NIDTOK = 0;
                        audit.NUMIPA = Environment.MachineName;
                        _AUDNUM = WriteAuditing.Insert(audit);
                        respond.Logged = _AUDNUM > 0;
                        respond.MessageToUser = "TIPO DE TARIFA INCLUIDO COM SUCESSO";
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
                        respond.ErrorCode = "RECORDFOUND";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -3)
                    {
                        respond.ErrorCode = "ATTRIBUTEFOUNDED";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = "O NOME FORNECIDO COMO ARGUMENTO JÁ ESTÁ CADASTRADO";
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
        /// Altera um registro da tabela TBTIPTAR (Tariff Type)  de acordo com a chave primaria
        /// </summary>
        ///<param name="model">TariffType</param>
        /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(TariffType model)
        {
            ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode = 10;
            this.HasError = false;
            TariffType ModelAud = Select(model.CODTAR);
            string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Tariff Type", model, true);

            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@RETURN_VALUE", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@CODTAR", model.CODTAR, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@DSCTAR", model.DSCTAR, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@CODMOV", model.CODMOV, dbType: DbType.Int16, direction: ParameterDirection.Input);
                    p.Add("@STAREC", model.STAREC, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@DATUPD", model.DATUPD, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                    p.Add("@UPDUSU", model.UPDUSU, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    _conn.Execute("PRTIPTARUPD", p, commandType: CommandType.StoredProcedure);
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
                        audit.AUDOBJ = "PRTIPTARUPD";
                        audit.AUDSRC = _original;
                        audit.AUDCHG = _changed;
                        audit.NIDTOK = 0;
                        audit.NUMIPA = Environment.MachineName;
                        _AUDNUM = WriteAuditing.Insert(audit);
                        respond.Logged = _AUDNUM > 0;
                        respond.MessageToUser = "TIPO DE TARIFA ALTERADA COM SUCESSO";
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
                        respond.ErrorCode = "RECORDNOTFOUND";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -3)
                    {
                        respond.ErrorCode = "ATTRIBUTEFOUNDED";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = "O NOME FORNECIDO COMO ARGUMENTO JÁ ESTÁ CADASTRADO";
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
        /// Obtêm o registro de Tipo de Tarifa com base no id informado
        /// </summary>
        /// <param name="pCODTAR">Tipo de Tarifa</param>
        /// <returns>TariffType</returns>
        public TariffType Select(System.Byte pCODTAR)
        {
            this.Found = false;
            this.ProcessCode = 100;
            TariffType RETURN_VALUE = null;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT A.*, DSCREC= ISNULL(B.DSCTAB,''), 
LGNUSU = ISNULL(C.LGNUSU,''), D.DSCMOV 
FROM TBTIPTAR (NOLOCK) A
INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB =7 AND B.KEYCOD=A.STAREC)
 LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV =1)
INNER JOIN TBTIPMOV (NOLOCK) D ON (D.CODMOV = A.CODMOV) WHERE (A.CODTAR=@CODTAR)";
                    RETURN_VALUE = _conn.Query<TariffType>(sql: _sql, param: new
                    {
                        CODTAR = pCODTAR
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
        /// Obtêm uma Lista dos Tipos de Tarifa
        /// </summary>
        /// <returns>Listof TariffType</returns>
        public List<TariffType> List()
        {
            this.ProcessCode = 105;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<TariffType>(sql: "PRTIPTARSELALL", param: new { }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();
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
