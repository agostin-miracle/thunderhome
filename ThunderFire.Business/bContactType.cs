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
    /// Regra de Negócio para TBTIPCTO
    ///</summary>
    ///<remarks>
    ///Empresa     : Agostinho da Silva Milagres
    ///Copyright   : Copyright © 2012
    ///Descricao   : Gerador de Objetos
    ///Produto     : SQLDBTools
    ///Titulo      : SQLDBTools
    ///Version     : 1.3.0.0
    ///Data        : 19/03/2022 10:01
    ///Alias       : contacttype
    ///Descrição   : Tipos de Contato
    ///</remarks>
    public partial class ContactTypeDao : BusinessBase, IContactType
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Instância Base
        /// </summary>
        public ContactTypeDao()
        {
            this.Connected = true;
            this.KeyTableId = 11;

        }

        /// <summary>
        /// Insere um registro na tabela TBTIPCTO (Tipos de Contato)
        /// </summary>
        ///<param name="model">ContactType</param>
        /// <returns>int</returns>
        public ExecutionResponse Insert(ContactType model)
        {
            ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.HasError = false;
            this.ProcessCode = 0;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _changed = Objects.GetPropertiesValue("Tipos de Contato", model, true);

                    var p = new DynamicParameters();
                    p.Add("@RETURN_VALUE", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@TIPCTO", model.TIPCTO, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@DSCCTO", model.DSCCTO, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@STAREC", model.STAREC, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@UPDUSU", model.UPDUSU, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    _conn.Execute("PRTIPCTOINS", p, commandType: CommandType.StoredProcedure);
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
                        audit.AUDOBJ = "PRTIPCTOINS";
                        audit.AUDSRC = "";
                        audit.AUDCHG = _changed;
                        audit.NIDTOK = 0;
                        audit.NUMIPA = Environment.MachineName;
                        _AUDNUM = WriteAuditing.Insert(audit);
                        respond.Logged = _AUDNUM > 0;
                        respond.MessageToUser = "INCLUSAO EFETUADA COM SUCESSO";
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -1)
                    {
                        respond.ErrorCode = "FAILALL";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = "FALHA NA INCLUSAO DO REGISTRO";
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -2)
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
        /// Altera um registro da tabela TBTIPCTO (Tipos de Contato)  de acordo com a chave primaria
        /// </summary>
        ///<param name="model">ContactType</param>
        /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(ContactType model)
        {
            ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode = 0;
            this.HasError = false;
            ContactType ModelAud = Select(model.TIPCTO);
            string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Tipos de Contato", model, true);

            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@RETURN_VALUE", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@TIPCTO", model.TIPCTO, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@DSCCTO", model.DSCCTO, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@STAREC", model.STAREC, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@DATUPD", model.DATUPD, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                    p.Add("@UPDUSU", model.UPDUSU, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    _conn.Execute("PRTIPCTOUPD", p, commandType: CommandType.StoredProcedure);
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
                        audit.AUDOBJ = "PRTIPCTOUPD";
                        audit.AUDSRC = _original;
                        audit.AUDCHG = _changed;
                        audit.NIDTOK = 0;
                        audit.NUMIPA = Environment.MachineName;
                        _AUDNUM = WriteAuditing.Insert(audit);
                        respond.Logged = _AUDNUM > 0;
                        respond.MessageToUser = "ALTERACAO EFETUADA COM SUCESSO";
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
                        respond.ErrorCode = "ATTRIBUTEFOUNDED";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = "O NOME FORNECIDO COMO ARGUMENTO JÁ ESTÁ CADASTRADO PARA OUTRO REGISTRO";
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
        /// Obtêm o registro do Tipo de contato informado
        /// </summary>
        /// <param name="pTIPCTO">Tipo de Contato</param>
        /// <returns>ContactType</returns>
        public ContactType Select(System.Int32 pTIPCTO)
        {
            this.Found = false;
            this.ProcessCode = 100;
            ContactType RETURN_VALUE = null;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTIPCTO (nolock) A WHERE (A.TIPCTO=@TIPCTO)";
                    RETURN_VALUE = _conn.Query<ContactType>(sql: _sql, param: new
                    {
                        TIPCTO = pTIPCTO
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

    }
}
