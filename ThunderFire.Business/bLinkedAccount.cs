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
/// Regra de Negócio para TBCADCVL
///</summary>
///<remarks>
///Empresa     : Agostinho da Silva Milagres
///Copyright   : Copyright © 2012
///Descricao   : Gerador de Objetos
///Produto     : SQLDBTools
///Titulo      : SQLDBTools
///Version     : 1.3.0.0
///Data        : 26/03/2022 17:58
///Alias       : linkedaccount
///Descrição   : Linked Account
///</remarks>
    public partial class LinkedAccountDao : BusinessBase, ILinkedAccount
    {
private static Logger _logger = LogManager.GetCurrentClassLogger();
/// <summary>
/// Instância Base
/// </summary>
public LinkedAccountDao()
{
this.Connected=true;
this.KeyTableId =35;

}
       
    /// <summary>
    /// Insere um registro na tabela TBCADCVL (Linked Account)
    /// </summary>
    ///<param name="model">LinkedAccount</param>
    /// <returns>int</returns>
        public ExecutionResponse Insert(LinkedAccount model)
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
string _changed = Objects.GetPropertiesValue("Linked Account",model,true);

            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@CODUSU", model.CODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NIDCTA", model.NIDCTA, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATUPD", model.DATUPD, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRCADCVLINS", p,commandType: CommandType.StoredProcedure);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
respond.ReturnValue = RETURN_VALUE;
string _errormessage="";
                if(RETURN_VALUE==1)
                {
                    respond.ErrorCode="OK";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser ="CONTA VINCULADA COM SUCESSO";
_errormessage="";
                }
                if(RETURN_VALUE==2)
                {
                    respond.ErrorCode="OK";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser ="CONTA REVINCULADA COM SUCESSO";
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
                if(RETURN_VALUE==-3)
                {
                    respond.ErrorCode="ACCOUNTNOTFOUND";
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
    /// Altera um registro da tabela TBCADCVL (Linked Account)  de acordo com a chave identity
    /// </summary>
    ///<param name="model">LinkedAccount</param>
    /// <returns>ExecutionResponse</returns>
        public ExecutionResponse Update(LinkedAccount model)
        {
ExecutionResponse respond = new ExecutionResponse();
            int RETURN_VALUE = 0;
            int _AUDNUM = 0;
            this.ProcessCode= 10;
            this.HasError =false;
                            LinkedAccount ModelAud = Select(model.NIDCVL);
string _original = Objects.GetPropertiesValue(ModelAud);

            string _changed = Objects.GetPropertiesValue("Linked Account",model,true);

                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
            try
            {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
            p.Add("@NIDCVL", model.NIDCVL, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@CODUSU", model.CODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@NIDCTA", model.NIDCTA, dbType:DbType.Int32,  direction: ParameterDirection.Input);
            p.Add("@STAREC", model.STAREC, dbType:DbType.Byte,  direction: ParameterDirection.Input);
            p.Add("@DATUPD", model.DATUPD, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
            p.Add("@UPDUSU", model.UPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
                _conn.Execute("PRCADCVLUPD", p,commandType: CommandType.StoredProcedure);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
                respond.ReturnValue= RETURN_VALUE;
string _errormessage="";
                if(RETURN_VALUE==2)
                {
                    respond.ErrorCode="OK";
_errormessage= ErrorManager.GetStringMsg(respond.ErrorCode);
                    respond.ErrorMessage=_errormessage;
                    respond.MessageToUser ="VINCULO RESTABELECIDO COM SUCESSO";
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
    /// Obtêm o registro da conta vinculada com base no id informado
    /// </summary>
        /// <param name="pNIDCVL">ID da Conta Vinculada</param>
    /// <returns>LinkedAccount</returns>
    public LinkedAccount Select(System.Int32 pNIDCVL)
        {
        this.Found=false;
            this.ProcessCode= 100;
    LinkedAccount RETURN_VALUE=null;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
                    {
                    try
                    {
string _sql = @"SELECT A.*
       ,B.NUMCTA 
	   ,B.NUMAGE
	   ,B.NUMDVE
	   ,C.NOMUSU
	   ,DSCBCO = NUMBCO + ' - ' + ISNULL(D.DSCTAB,''),DSCSTA
	   ,CODCMF = DBO.FormatCMF(B.CODCMF)
	   ,NOMBNF = CASE WHEN B.ORGCTA IN (1,3,4)  THEN 'O PROPRIO' ELSE B.NOMBNF END
     ,LGNUSU = ISNULL(F.LGNUSU,'')
     ,G.DSCCTA
  FROM TBCADCVL (NOLOCK) A 
 INNER JOIN TBCADCTA (NOLOCK) B ON (B.NIDCTA = A.NIDCTA)
 INNER JOIN TBCADGER (NOLOCK) C ON (C.CODUSU = B.CODUSU)
 INNER JOIN TBTABGER (NOLOCK) D ON (D.NUMTAB=12 AND D.KEYTXT = B.NUMBCO) 
 INNER JOIN TBCADSTA (NOLOCK) E ON (E.CODSTA = B.STACTA)  
  LEFT JOIN TBLGNUSU (NOLOCK) F ON (F.CODUSU = A.UPDUSU AND F.REGATV = 1)   
 INNER JOIN TBTIPCTA (NOLOCK) G ON (G.TIPCTA = B.TIPCTA) WHERE (A.NIDCVL=@NIDCVL)";
 RETURN_VALUE = _conn.Query<LinkedAccount>(sql:_sql, param:new {NIDCVL=pNIDCVL
},  commandType: CommandType.Text, commandTimeout: 120).FirstOrDefault();
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
    /// Remove um conta vinculada da lista
    /// </summary>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pNIDCTA">Id da Conta</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <returns>int</returns>
    public int RemoveLinkedID( int pCODUSU,int pNIDCTA,int pUPDUSU)
    {
            this.ProcessCode= 200;
            if(ThunderFire.Business.Support.CheckAccess(this.KeyTableId, this.ProcessCode,pUPDUSU)!=1)
            {
        this.ReturnValue=ThunderFire.Business.Support.ReturnValue;
        this.MessageToUser=ThunderFire.Business.Support.MessageToUser;
        this.ErrorCode=ThunderFire.Business.Support.ErrorCode;
        return -100;
            }
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
p.Add("@CODUSU", pCODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@NIDCTA", pNIDCTA, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@UPDUSU", pUPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
RETURN_VALUE=_conn.ExecuteScalar<Int32>(sql:"PRCADCVLREMLNK", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
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
    /// Localiza uma conta digital conforme os parâmetros fornecidos
    /// </summary>
/// <remarks>
/// <para>Retona o ID da Conta Virtual associada, caso contrário indica erro de pesquisa</para>
/// </remarks>
    /// <param name="pMETPSQ">Método de Pesquisa (1 - POR CPF/CNPF, 2 - POR CONTA)</param>
    /// <param name="pNOMPSQ">Valor de Pesquisa</param>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <returns>int</returns>
    public int LocateAccount( System.Byte pMETPSQ,System.String pNOMPSQ,System.Int32 pCODUSU)
    {
            this.ProcessCode= 205;
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
p.Add("@METPSQ", pMETPSQ, dbType:DbType.Byte,  direction: ParameterDirection.Input);
p.Add("@NOMPSQ", pNOMPSQ, dbType:DbType.String,  direction: ParameterDirection.Input);
p.Add("@CODUSU", pCODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRCADCVLLOCACT", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
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
