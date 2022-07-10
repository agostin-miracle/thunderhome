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
    /// Regra de Negócio para TBTIPUSU
    ///</summary>
    ///<remarks>
    ///Empresa     : Agostinho da Silva Milagres
    ///Copyright   : Copyright © 2012
    ///Descricao   : Gerador de Objetos
    ///Produto     : SQLDBTools
    ///Titulo      : SQLDBTools
    ///Version     : 1.3.0.0
    ///Data        : 19/03/2022 10:57
    ///Alias       : usertype
    ///Descrição   : Tipo de Usuário
    ///</remarks>
    public partial class UserTypeDao : BusinessBase, IUserType
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Instância Base
        /// </summary>
        public UserTypeDao()
        {
            this.Connected = true;
            this.KeyTableId = 7;

        }

        /// <summary>
        /// Insere um registro na tabela TBTIPUSU (Tipo de Usuário)
        /// </summary>
        ///<param name="model">UserType</param>
        /// <returns>int</returns>
        public ExecutionResponse Insert(UserType model)
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
                    string _changed = Objects.GetPropertiesValue("Tipo de Usuário", model, true);

                    var p = new DynamicParameters();
                    p.Add("@RETURN_VALUE", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@TIPUSU", model.TIPUSU, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@DSCTUS", model.DSCTUS, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@STAREC", model.STAREC, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@UPDUSU", model.UPDUSU, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    _conn.Execute("PRTIPUSUINS", p, commandType: CommandType.StoredProcedure);
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
                        audit.AUDOBJ = "PRTIPUSUINS";
                        audit.AUDSRC = "";
                        audit.AUDCHG = _changed;
                        audit.NIDTOK = 0;
                        audit.NUMIPA = Environment.MachineName;
                        _AUDNUM = WriteAuditing.Insert(audit);
                        respond.Logged = _AUDNUM > 0;
                        respond.MessageToUser = "TIPO DE USUARIO INCLUIDO COM SUCESSO";
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
                        respond.ErrorCode = "INVALIFARGUMENT";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = "A DESCRICAO DO TIPO DE USUARIO DEVE SER FORNECIDA";
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
        /// Altera um registro da tabela TBTIPUSU (Tipo de Usuário)  de acordo com a chave primaria
        /// </summary>
        ///<param name="model">UserType</param>
        /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(UserType model)
        {
            ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode = 10;
            this.HasError = false;
            UserType ModelAud = Select(model.TIPUSU);
            string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Tipo de Usuário", model, true);

            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@RETURN_VALUE", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@TIPUSU", model.TIPUSU, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@DSCTUS", model.DSCTUS, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@STAREC", model.STAREC, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@DATUPD", model.DATUPD, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                    p.Add("@UPDUSU", model.UPDUSU, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    _conn.Execute("PRTIPUSUUPD", p, commandType: CommandType.StoredProcedure);
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
                        audit.AUDOBJ = "PRCADSTAUPD";
                        audit.AUDSRC = _original;
                        audit.AUDCHG = _changed;
                        audit.NIDTOK = 0;
                        audit.NUMIPA = Environment.MachineName;
                        _AUDNUM = WriteAuditing.Insert(audit);
                        respond.Logged = _AUDNUM > 0;
                        respond.MessageToUser = "TIPO DE USUARIO ALTERADO COM SUCESSO";
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
                        respond.ErrorCode = "INVALIFARGUMENT";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = "A DESCRICAO DO TIPO DE USUARIO DEVE SER FORNECIDA";
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
        /// Obtêm o registro do Tipo de Usuário fornecido
        /// </summary>
        /// <param name="pTIPUSU">Tipo de Usuário</param>
        /// <returns>UserType</returns>
        public UserType Select(System.Byte pTIPUSU)
        {
            this.Found = false;
            this.ProcessCode = 100;
            UserType RETURN_VALUE = null;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    RETURN_VALUE = _conn.Query<UserType>(sql: "PRTIPUSUSEL", param: new
                    {
                        TIPUSU = pTIPUSU
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

    }
}
