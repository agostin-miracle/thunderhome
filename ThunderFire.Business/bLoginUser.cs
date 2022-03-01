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
    /// Regra de Negócio para TBLGNUSU
    ///</summary>
    ///<remarks>
    ///Empresa     : Agostinho da Silva Milagres
    ///Copyright   : Copyright © 2012
    ///Descricao   : Gerador de Objetos
    ///Produto     : SQLDBTools
    ///Titulo      : SQLDBTools
    ///Version     : 1.3.0.0
    ///Data        : 26/02/2022 18:11
    ///Alias       : loginuser
    ///Descrição   : Login de Usuário
    ///</remarks>
    public partial class LoginUserDao : BusinessBase, ILoginUser
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Instância Base
        /// </summary>
        public LoginUserDao()
        {
            this.Connected = true;
            this.KeyTableId = 2;

        }

        /// <summary>
        /// Insere um registro na tabela TBLGNUSU (Login de Usuário)
        /// </summary>
        ///<param name="model">LoginUser</param>
        /// <returns>int</returns>
        public ExecutionResponse Insert(LoginUser model)
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
                    string _changed = Objects.GetPropertiesValue("Login de Usuário", model, true);

                    var p = new DynamicParameters();
                    p.Add("@RETURN_VALUE", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@CODUSU", model.CODUSU, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@REGATV", model.REGATV, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@LGNUSU", model.LGNUSU, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@PSWUSU", model.PSWUSU, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@RSTPSW", model.RSTPSW, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@NUMACE", model.NUMACE, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@STAREC", model.STAREC, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@UPDUSU", model.UPDUSU, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    _conn.Execute("PRLGNUSUINS", p, commandType: CommandType.StoredProcedure);
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
                        audit.AUDIMS = 1;
                        audit.AUDTSK = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
                        audit.AUDOBJ = "PRLGNUSUINS";
                        audit.AUDSRC = _changed;
                        audit.AUDCHG = "";
                        audit.NIDTOK = 0;
                        audit.NUMIPA = Environment.MachineName;
                        _AUDNUM = WriteAuditing.Insert(audit);
                        respond.Logged = _AUDNUM > 0;
                        respond.MessageToUser = "LOGIN INCLUIDO COM SUCESSO";
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
                        respond.ErrorCode = "LOGINBLANK";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -3)
                    {
                        respond.ErrorCode = "INVALID_LOGIN";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -4)
                    {
                        respond.ErrorCode = "LOGINALREADYEXISTS";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -5)
                    {
                        respond.ErrorCode = "PASSWORDEMPTY";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -6)
                    {
                        respond.ErrorCode = "PASSOWRDALREADYEXISTS";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -7)
                    {
                        respond.ErrorCode = "INVALIDUSER";
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
                        respond.ErrorMessage = msg.Message;
                        respond.Severity = msg.Severity;
                    }
                }
            }
            return respond;
        }

        /// <summary>
        /// Altera um registro da tabela TBLGNUSU (Login de Usuário)  de acordo com a chave identity
        /// </summary>
        ///<param name="model">LoginUser</param>
        /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(LoginUser model)
        {
            ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode = 0;
            this.HasError = false;
            LoginUser ModelAud = Select(model.LGNNUM);
            string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Login de Usuário", model, true);

            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@RETURN_VALUE", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@LGNNUM", model.LGNNUM, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@CODUSU", model.CODUSU, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@REGATV", model.REGATV, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@LGNUSU", model.LGNUSU, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@PSWUSU", model.PSWUSU, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@RSTPSW", model.RSTPSW, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@NUMACE", model.NUMACE, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@STAREC", model.STAREC, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@DATUPD", model.DATUPD, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                    p.Add("@UPDUSU", model.UPDUSU, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    _conn.Execute("PRLGNUSUUPD", p, commandType: CommandType.StoredProcedure);
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
                        audit.AUDIMS = 2;
                        audit.AUDTSK = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
                        audit.AUDOBJ = "PRLGNUSUUPD";
                        audit.AUDSRC = _original;
                        audit.AUDCHG = _changed;
                        audit.NIDTOK = 0;
                        audit.NUMIPA = Environment.MachineName;
                        _AUDNUM = WriteAuditing.Insert(audit);
                        respond.Logged = _AUDNUM > 0;
                        respond.MessageToUser = "LOGIN ALTERADO COM SUCESSO";
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
                        respond.ErrorCode = "LOGINBLANK";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -3)
                    {
                        respond.ErrorCode = "INVALID_LOGIN";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -4)
                    {
                        respond.ErrorCode = "LOGINALREADYEXISTS";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -5)
                    {
                        respond.ErrorCode = "PASSWORDEMPTY";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -6)
                    {
                        respond.ErrorCode = "PASSOWRDALREADYEXISTS";
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
                        respond.ErrorMessage = msg.Message;
                        respond.Severity = msg.Severity;
                    }
                }
            }
            return respond;
        }
        /// <summary>
        /// Obtêm o registro de Login do Usuário
        /// </summary>
        /// <param name="pLGNNUM">ID de Registro de Login</param>
        /// <returns>LoginUser</returns>
        public LoginUser Select(System.Int32 pLGNNUM)
        {
            this.Found = false;
            this.ProcessCode = 0;
            LoginUser RETURN_VALUE = null;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    RETURN_VALUE = _conn.Query<LoginUser>(sql: "PRLGNUSUSEL", param: new
                    {
                        LGNNUM = pLGNNUM
                    }, commandType: CommandType.StoredProcedure, commandTimeout: 120).FirstOrDefault();
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
        /// Obtêm o registro de Login Ativo do Usuário
        /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
        /// <returns>LoginUser</returns>
        public LoginUser Get(System.Int32 pCODUSU)
        {
            this.Found = false;
            this.ProcessCode = 0;
            LoginUser RETURN_VALUE = null;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    RETURN_VALUE = _conn.Query<LoginUser>(sql: "PRLGNUSUGET", param: new
                    {
                        CODUSU = pCODUSU
                    }, commandType: CommandType.StoredProcedure, commandTimeout: 120).FirstOrDefault();
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
        /// Obtêm o registro de controle de acesso do usuário
        /// </summary>
        /// <param name="pLGNNUM">ID de Registro de Login</param>
        /// <returns>AccessControl</returns>
        public AccessControl GetAccessControl(System.Int32 pLGNNUM)
        {
            this.Found = false;
            this.ProcessCode = 0;
            AccessControl RETURN_VALUE = new AccessControl();
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var query = _conn.Query<AccessControl>("PRLGNUSUACECTL",
                                                new[]
                                                {
                                typeof(AccessControl),
                                typeof(VirtualAccount),
                                typeof(GeneralRegistry),
                                typeof(ActiveCards),
                                typeof(AddressBook)
                                                },
                                                objects =>
                                                {
                                                    var ac = objects[0] as AccessControl;
                                                    var va = objects[1] as VirtualAccount;
                                                    var us = objects[2] as GeneralRegistry;
                                                    var cr = objects[3] as ActiveCards;
                                                    var ad = objects[4] as AddressBook;


                                                    RETURN_VALUE.Account = va;
                                                    RETURN_VALUE.Address = ad;
                                                    RETURN_VALUE.User = us;
                                                    RETURN_VALUE.Cards.Add(cr);
                                                    return ac;
                                                },
                                                                    splitOn: "nidcta,codusu, codcrt,codend", param: new
                                                                    {
                                                                        LGNNUM = pLGNNUM
                                                                    }, commandType: CommandType.StoredProcedure, commandTimeout: 120);
                    RETURN_VALUE.IsValid = true;
                    query = null;
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
        /// Efetua o logoff de um usuario
        /// </summary>
        /// <param name="pLGNNUM">ID do Logon</param>
        /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Logoff(System.Int32 pLGNNUM)
        {
            int _AUDNUM = 0;
            this.ProcessCode = 0;
            ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            try
            {
                RETURN_VALUE = pLGNNUM;
                LoginUser model = Select(pLGNNUM);
                if (this.Found)
                {
                    Auditing audit = new Auditing();
                    audit.UPDUSU = model.CODUSU;
                    audit.AUDDAT = DateTime.Now;
                    audit.AUDKEY = KeyTableId;
                    audit.AUDIDR = RETURN_VALUE;
                    audit.AUDIMS = 6;
                    audit.AUDTSK = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
                    audit.AUDOBJ = "Application";
                    audit.AUDSRC = "";
                    audit.AUDCHG = "EFETUOU LOGOFF DO APLICATIVO EM " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    audit.NIDTOK = 0;
                    audit.NUMIPA = Environment.MachineName;
                    _AUDNUM = WriteAuditing.Insert(audit);
                    respond.Logged = _AUDNUM > 0;
                }
                else
                    RETURN_VALUE = -1;
                model = null;
                respond.ReturnValue = RETURN_VALUE;
                string _errormessage = "";
                if (RETURN_VALUE > 0)
                {
                    respond.ErrorCode = "OK";
                    _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage = _errormessage;
                    respond.MessageToUser = "LOGOFF EFETUADO COM SUCESSO";
                    _errormessage = "";
                }
                if (RETURN_VALUE == -1)
                {
                    respond.ErrorCode = "LOGINCANNOTBEREAD";
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
                respond.MessageToUser = "FALHA NO PROCESSAMENTO DA INFORMACAO";
                respond.ErrorMessage = Error.Message;
                var msg = ErrorManager.GetError(ErrorManager.GetErrorCode(Error.Message));
                if (msg != null)
                {
                    respond.SourceError = msg.Source;
                    respond.ErrorCode = msg.ErrorCode;
                    respond.ErrorObject = Error;
                    respond.ErrorMessage = msg.Message;
                    respond.Severity = msg.Severity;
                }
            }
            return respond;
        }


        /// <summary>
        /// Efetua um login com base nas credenciais de acesso
        /// </summary>
        /// <param name="pLGNTYP">Tipo de Acesso</param>
        /// <param name="pLGNUSU">Login</param>
        /// <param name="pPSWUSU">Senha</param>
        /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Login(System.Byte pLGNTYP, System.String pLGNUSU, System.String pPSWUSU)
        {
            int _AUDNUM = 0;
            this.ProcessCode = 0;
            ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@RETURN_VALUE", 0, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                    p.Add("@LGNTYP", pLGNTYP, dbType: DbType.Byte, direction: ParameterDirection.Input);
                    p.Add("@LGNUSU", pLGNUSU, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@PSWUSU", pPSWUSU, dbType: DbType.String, direction: ParameterDirection.Input);
                    _conn.Execute(sql: "PRLGNUSULOG", param: p, commandType: CommandType.StoredProcedure, commandTimeout: 120);
                    RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
                    respond.ReturnValue = RETURN_VALUE;
                    string _errormessage = "";
                    if (RETURN_VALUE > 0)
                    {
                        respond.ErrorCode = "OK";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = "LOGIN EFETUADO COM SUCESSO";
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -1)
                    {
                        respond.ErrorCode = "LOGINCREDENTIALNEEDRESET";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -2)
                    {
                        respond.ErrorCode = "USERNOTFOUND";
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
                    respond.MessageToUser = "FALHA NO PROCESSAMENTO DA INFORMACAO";
                    respond.ErrorMessage = Error.Message;
                    var msg = ErrorManager.GetError(ErrorManager.GetErrorCode(Error.Message));
                    if (msg != null)
                    {
                        respond.SourceError = msg.Source;
                        respond.ErrorCode = msg.ErrorCode;
                        respond.ErrorObject = Error;
                        respond.ErrorMessage = msg.Message;
                        respond.Severity = msg.Severity;
                    }
                }
            }
            return respond;
        }


        /// <summary>
        /// Verifica se o usuario precisa fazer um refresh de senha
        /// </summary>
        /// <param name="pLGNNUM">ID do Login</param>
        /// <returns>ExecutionResponse</returns>
        public ExecutionResponse NeedRefresh(System.Int32 pLGNNUM)
        {
            int _AUDNUM = 0;
            this.ProcessCode = 0;
            ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@RETURN_VALUE", 0, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                    p.Add("@LGNNUM", pLGNNUM, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    _conn.Execute(sql: "PRLGNUSUREF", param: p, commandType: CommandType.StoredProcedure, commandTimeout: 120);
                    RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
                    respond.ReturnValue = RETURN_VALUE;
                    string _errormessage = "";
                    if (RETURN_VALUE == 0)
                    {
                        respond.MessageToUser = "NAO FOI POSSIVEL EFETUAR A VERIFICACAO";
                        _errormessage = "";
                    }
                    if (RETURN_VALUE > 0)
                    {
                        respond.MessageToUser = "VERIFICACAO EFETUADA COM SUCESSO";
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -1)
                    {
                        respond.ErrorCode = "USERNOTFOUND";
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
                    respond.MessageToUser = "FALHA NO PROCESSAMENTO DA INFORMACAO";
                    respond.ErrorMessage = Error.Message;
                    var msg = ErrorManager.GetError(ErrorManager.GetErrorCode(Error.Message));
                    if (msg != null)
                    {
                        respond.SourceError = msg.Source;
                        respond.ErrorCode = msg.ErrorCode;
                        respond.ErrorObject = Error;
                        respond.ErrorMessage = msg.Message;
                        respond.Severity = msg.Severity;
                    }
                }
            }
            return respond;
        }


        /// <summary>
        /// Efetua um login com base nas credenciais de acesso
        /// </summary>
        /// <remarks>
        /// <para>Códigos de Retorno</para>
        /// <para>[-1]= O Usuário deverá fazer o reset de senha</para>
        /// <para>[-2] = Usuário não existente</para>
        /// <para>[>0] = Id de Registro de Login</para>
        /// </remarks>
        /// <param name="pLGNUSU">Login</param>
        /// <param name="pPSWOLD">Senha Anterior</param>
        /// <param name="pPSWUSU">Senha</param>
        /// <returns>ExecutionResponse</returns>
        public ExecutionResponse ChangePassword(System.String pLGNUSU, System.String pPSWOLD, System.String pPSWUSU)
        {
            int _AUDNUM = 0;
            this.ProcessCode = 0;
            ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@RETURN_VALUE", 0, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                    p.Add("@LGNUSU", pLGNUSU, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@PSWOLD", pPSWOLD, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@PSWUSU", pPSWUSU, dbType: DbType.String, direction: ParameterDirection.Input);
                    _conn.Execute(sql: "PRLGNUSUCHGPSW", param: p, commandType: CommandType.StoredProcedure, commandTimeout: 120);
                    RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
                    respond.ReturnValue = RETURN_VALUE;
                    string _errormessage = "";
                    if (RETURN_VALUE > 0)
                    {
                        LoginUser model = Select(@RETURN_VALUE);
                        if (this.Found)
                        {
                            Auditing audit = new Auditing();
                            audit.UPDUSU = model.UPDUSU;
                            audit.AUDDAT = DateTime.Now;
                            audit.AUDKEY = KeyTableId;
                            audit.AUDIDR = RETURN_VALUE;
                            audit.AUDIMS = 3;
                            audit.AUDTSK = this.GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
                            audit.AUDOBJ = "PRLGNUSUCHGPSW";
                            audit.AUDSRC = String.Format("'{0}':'{1}'", "PSWUSU", pPSWOLD);
                            audit.AUDCHG = String.Format("'{0}':'{1}'", "PSWUSU", pPSWUSU);
                            audit.NIDTOK = 0;
                            audit.NUMIPA = Environment.MachineName;
                            _AUDNUM = WriteAuditing.Insert(audit);
                            respond.Logged = _AUDNUM > 0;
                        }
                        model = null;
                        respond.MessageToUser = "SENHA ALTERADA COM SUCESSO";
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
                        respond.ErrorCode = "LOGINBLANK";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -3)
                    {
                        respond.ErrorCode = "INVALID_LOGIN";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -4)
                    {
                        respond.ErrorCode = "LOGINALREADYEXISTS";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -5)
                    {
                        respond.ErrorCode = "PASSWORDEMPTY";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -6)
                    {
                        respond.ErrorCode = "PASSOWRDALREADYEXISTS";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -7)
                    {
                        respond.MessageToUser = "LOGIN NAO EXISTE";
                        _errormessage = "";
                    }
                }
                catch (Exception Error)
                {
                    _logger.Info(Error);
                    this.HasError = true;
                    respond.ReturnValue = RETURN_VALUE;
                    respond.StatusCode = 400;
                    respond.MessageToUser = "FALHA NO PROCESSAMENTO DA INFORMACAO";
                    respond.ErrorMessage = Error.Message;
                    var msg = ErrorManager.GetError(ErrorManager.GetErrorCode(Error.Message));
                    if (msg != null)
                    {
                        respond.SourceError = msg.Source;
                        respond.ErrorCode = msg.ErrorCode;
                        respond.ErrorObject = Error;
                        respond.ErrorMessage = msg.Message;
                        respond.Severity = msg.Severity;
                    }
                }
            }
            return respond;
        }


        /// <summary>
        /// Efetua um login com base nas credenciais de acesso
        /// </summary>
        /// <param name="pLGNNUM">Login</param>
        /// <param name="pPSWUSU">Senha</param>
        /// <param name="pUPDUSU">Usuário de Atualização</param>
        /// <returns>ExecutionResponse</returns>
        public ExecutionResponse ResetPassword(System.Int32 pLGNNUM, System.String pPSWUSU, System.Int32 pUPDUSU)
        {
            int _AUDNUM = 0;
            this.ProcessCode = 0;
            ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var p = new DynamicParameters();
                    p.Add("@RETURN_VALUE", 0, dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                    p.Add("@LGNNUM", pLGNNUM, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add("@PSWUSU", pPSWUSU, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add("@UPDUSU", pUPDUSU, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    _conn.Execute(sql: "PRLGNUSURSTPSW", param: p, commandType: CommandType.StoredProcedure, commandTimeout: 120);
                    RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
                    respond.ReturnValue = RETURN_VALUE;
                    string _errormessage = "";
                    if (RETURN_VALUE == -1)
                    {
                        respond.ErrorCode = "LOGINBLANK";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -2)
                    {
                        respond.ErrorCode = "PASSWORDEMPTY";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -3)
                    {
                        respond.ErrorCode = "PASSWORDINVALIDLEN";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -4)
                    {
                        respond.ErrorCode = "REQUIREDUSEROPERATION";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -5)
                    {
                        respond.ErrorCode = "USERNOTFOUND";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = _errormessage;
                        _errormessage = "";
                    }
                    if (RETURN_VALUE == -6)
                    {
                        respond.ErrorCode = "FAILALL";
                        _errormessage = ErrorManager.GetStringMsg(respond.ErrorCode);
                        respond.ErrorMessage = _errormessage;
                        respond.MessageToUser = "FALHA DE ATUALIZAÇÃO, TENTE NOVAMENTE MAIS TARDE";
                        _errormessage = "";
                    }
                }
                catch (Exception Error)
                {
                    _logger.Info(Error);
                    this.HasError = true;
                    respond.ReturnValue = RETURN_VALUE;
                    respond.StatusCode = 400;
                    respond.MessageToUser = "FALHA NO PROCESSAMENTO DA INFORMACAO";
                    respond.ErrorMessage = Error.Message;
                    var msg = ErrorManager.GetError(ErrorManager.GetErrorCode(Error.Message));
                    if (msg != null)
                    {
                        respond.SourceError = msg.Source;
                        respond.ErrorCode = msg.ErrorCode;
                        respond.ErrorObject = Error;
                        respond.ErrorMessage = msg.Message;
                        respond.Severity = msg.Severity;
                    }
                }
            }
            return respond;
        }

    }
}
