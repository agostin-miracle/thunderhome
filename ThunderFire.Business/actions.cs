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
/// <summary>
/// Rotinas de execucao imediata
/// </summary>
public static partial class Actions 
{
        
private static Logger _logger = LogManager.GetCurrentClassLogger();
    public static int ProcessCode {get;set;}
    public static bool Found {get;set;}=false;
    public static bool HasError {get;set;}=false;
    public static string MessageToUser {get;set;}


    /// <summary>
    /// Valida um CPF/CNPJ já existente para o mesmo atributo na base de cadastro geral
    /// </summary>
    /// <param name="pCODATR">Código do Atributo</param>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pCODCMF">CPF/CNPJ</param>
    /// <param name="pSRCUSU">Usuário Gestor</param>
    /// <returns>ExecutionResponse</returns>
    public static ExecutionResponse  ValidateEntryCMF( short pCODATR,System.Int32 pCODUSU,System.String pCODCMF,System.Int32 pSRCUSU)
    {
            int _AUDNUM =0;
            ProcessCode= 0;
            ExecutionResponse respond = new ExecutionResponse();
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.ReturnValue);
p.Add("@CODATR", pCODATR, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@CODUSU", pCODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@CODCMF", pCODCMF, dbType:DbType.String,  direction: ParameterDirection.Input);
p.Add("@SRCUSU", pSRCUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRCADGERVALENTRCMF", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
respond.ReturnValue= RETURN_VALUE;
string _errormessage="";
                if(RETURN_VALUE>0)
                {
                    respond.MessageToUser ="FORAM ENCONTRADAS OCORRENCIAS DUPLICADAS";
_errormessage="";
                }
                if(RETURN_VALUE==0)
                {
                    respond.MessageToUser ="NAO FOI ENCONTRADA NENHUMA OCORRENCIA";
_errormessage="";
                }
        }
        catch (Exception Error)
        {
            _logger.Info(Error);
            HasError =true;
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
    /// Verifica se já existe um cadastro com o CPF/CNPJ cadastrado, e retorna o id localizado
    /// </summary>
    /// <param name="pCODATR">Código do Atributo</param>
    /// <param name="pCODCMF">CPF/CNPJ</param>
    /// <returns>ExecutionResponse</returns>
    public static ExecutionResponse  PesquisarCMF( System.Int16 pCODATR,System.String pCODCMF)
    {
            int _AUDNUM =0;
            ProcessCode= 0;
            ExecutionResponse respond = new ExecutionResponse();
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.ReturnValue);
p.Add("@CODATR", pCODATR, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@CODCMF", pCODCMF, dbType:DbType.String,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRCADUSUPSQCMF", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
respond.ReturnValue= RETURN_VALUE;
string _errormessage="";
                if(RETURN_VALUE>0)
                {
                    respond.MessageToUser ="O CPF/CNPJ JA EXISTE";
_errormessage="";
                }
                if(RETURN_VALUE==0)
                {
                    respond.MessageToUser ="NAO FOI ENCONTRADA NENHUMA OCORRENCIA";
_errormessage="";
                }
        }
        catch (Exception Error)
        {
            _logger.Info(Error);
            HasError =true;
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
    /// Localiza um registro de tarifação com base no gestor ou usuário cedente, caso não encontra localiza com base no gestor padrão
    /// </summary>
    /// <param name="pUSUCFG">Código do Gestor/Usuário</param>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pCODTAR">Código da Tarifa</param>
    /// <param name="pCODBND">Código da Bandeira</param>
    /// <param name="pTIPLIN">Código do tipo de linha</param>
    /// <param name="pMODCRT">Modalidade de Cartão</param>
    /// <returns>int</returns>
    public static int TariffLocate( System.Int32 pUSUCFG,System.Int32 pCODUSU,System.Int16 pCODTAR,System.Int16 pCODBND = 0,System.Byte pTIPLIN = 0,System.Byte pMODCRT = 0)
    {
            ProcessCode= 0;
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.Output);
p.Add("@USUCFG", pUSUCFG, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@CODUSU", pCODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@CODTAR", pCODTAR, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@CODBND", pCODBND, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@TIPLIN", pTIPLIN, dbType:DbType.Byte,  direction: ParameterDirection.Input);
p.Add("@MODCRT", pMODCRT, dbType:DbType.Byte,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRCADTARFNDTAR", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
        }
        catch (Exception Error)
        {
            _logger.Info(Error);
            HasError =true;
        }
        }
        return RETURN_VALUE;
    }


    /// <summary>
    /// Procura por um registro de tarifação de acordo com os parâmetros fornecidos
    /// </summary>
    /// <param name="pUSUCFG">Usuário de Configuração de Destino</param>
    /// <param name="pNIVCFG">Nivel de Configuração de Destino</param>
    /// <param name="pCODTAR">Código da Tarifa</param>
    /// <param name="pCODBND">Código da Bandeira</param>
    /// <param name="pTIPLIN">Código do Tipo de Linha de Venda</param>
    /// <param name="pMODCRT">Modalidade de Cartão</param>
    /// <returns>int</returns>
    public static int TariffFind( System.Int32 pUSUCFG,System.Byte pNIVCFG,System.Int16 pCODTAR,System.Int16 pCODBND = 0,System.Byte pTIPLIN = 0,System.Byte pMODCRT = 0)
    {
            ProcessCode= 0;
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.ReturnValue);
p.Add("@USUCFG", pUSUCFG, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@NIVCFG", pNIVCFG, dbType:DbType.Byte,  direction: ParameterDirection.Input);
p.Add("@CODTAR", pCODTAR, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@CODBND", pCODBND, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@TIPLIN", pTIPLIN, dbType:DbType.Byte,  direction: ParameterDirection.Input);
p.Add("@MODCRT", pMODCRT, dbType:DbType.Byte,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRCADTARFND", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
        }
        catch (Exception Error)
        {
            _logger.Info(Error);
            HasError =true;
        }
        }
        return RETURN_VALUE;
    }


    /// <summary>
    /// Efetua o Cálculo de uma tarifa com base nos parâmetros fornecidos
    /// </summary>
    /// <param name="pUSUPRO">Código do Gestor</param>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pCODTAR">Código da Tarifa</param>
    /// <param name="pVLRBAS">Valor Base</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <param name="pNUMPCL">Número de Parcelas</param>
    /// <param name="pCURPCL">Parcela Atual</param>
    /// <param name="pDATVL1">Data de Validação 1</param>
    /// <param name="pDATVL2">Data de Validação 2</param>
    /// <param name="pFINLIV">Marcar Livre como Gestor Padrao</param>
    /// <param name="pCODBND">Código da Bandeira</param>
    /// <param name="pTIPLIN">Código do tipo de linha</param>
    /// <param name="pMODCRT">Modalidade de Cartão</param>
    /// <param name="pTARNID">ID do Registro de Tarifacao</param>
    /// <returns>int</returns>
    public static int ICalculate( System.Int32 pUSUPRO,System.Int32 pCODUSU,System.Int16 pCODTAR,System.Decimal pVLRBAS,System.Int32 pUPDUSU,System.Byte pNUMPCL = 1,System.Byte pCURPCL = 1,System.DateTime? pDATVL1 = null,System.DateTime? pDATVL2 = null,System.Boolean pFINLIV = false,System.Int16 pCODBND = 0,System.Byte pTIPLIN = 0,System.Byte pMODCRT = 0,System.Int32 pTARNID = 0)
    {
            ProcessCode= 0;
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.ReturnValue);
p.Add("@USUPRO", pUSUPRO, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@CODUSU", pCODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@CODTAR", pCODTAR, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@VLRBAS", pVLRBAS, dbType:DbType.Double,  direction: ParameterDirection.Input);
p.Add("@UPDUSU", pUPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@NUMPCL", pNUMPCL, dbType:DbType.Byte,  direction: ParameterDirection.Input);
p.Add("@CURPCL", pCURPCL, dbType:DbType.Byte,  direction: ParameterDirection.Input);
p.Add("@DATVL1", pDATVL1, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
p.Add("@DATVL2", pDATVL2, dbType:DbType.DateTime,  direction: ParameterDirection.Input);
p.Add("@FINLIV", pFINLIV, dbType:DbType.Boolean,  direction: ParameterDirection.Input);
p.Add("@CODBND", pCODBND, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@TIPLIN", pTIPLIN, dbType:DbType.Byte,  direction: ParameterDirection.Input);
p.Add("@MODCRT", pMODCRT, dbType:DbType.Byte,  direction: ParameterDirection.Input);
p.Add("@TARNID", pTARNID, dbType:DbType.Int32,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRCADTARCALV2", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
        }
        catch (Exception Error)
        {
            _logger.Info(Error);
            HasError =true;
        }
        }
        return RETURN_VALUE;
    }


    /// <summary>
    /// Verifica se o cartão já foi alocado para um portador
    /// </summary>
/// <remarks>
/// <para>Calcula as mensalidades do Cartão de acordo com a data de vigência do cartão</para>
/// </remarks>
    /// <param name="pCODCRT">ID do Cartão</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <returns>int</returns>
    public static int CreateMonthlyCard( System.Int32 pCODCRT,System.Int32 pUPDUSU)
    {
            ProcessCode= 0;
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.ReturnValue);
p.Add("@CODCRT", pCODCRT, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@UPDUSU", pUPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRREGMENCRTMENCRT", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
        }
        catch (Exception Error)
        {
            _logger.Info(Error);
            HasError =true;
        }
        }
        return RETURN_VALUE;
    }


    /// <summary>
    /// Verificar se existe o acesso de um usuário para uma rotina específica
    /// </summary>
    /// <param name="pSYSTBL">ID de identificação da tabela de aplicação da funcionalidade</param>
    /// <param name="pSYSROL">ID de identificação da regra de aplicação</param>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <returns>int</returns>
    public static int GetUserAccess( System.Int16 pSYSTBL,System.Int16 pSYSROL,System.Int32 pCODUSU)
    {
            ProcessCode= 0;
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.ReturnValue);
p.Add("@SYSTBL", pSYSTBL, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@SYSROL", pSYSROL, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@CODUSU", pCODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRSYSGETUSRACE", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
        }
        catch (Exception Error)
        {
            _logger.Info(Error);
            HasError =true;
        }
        }
        return RETURN_VALUE;
    }


    /// <summary>
    /// Efetua o calculo de uma tarifa com base no codigo da tarifa fornecido
    /// </summary>
    /// <param name="pUSUPRO">Código do Gestor</param>
    /// <param name="pUSUCED">Usuário Cedente</param>
    /// <param name="pCODTAR">Código da Tarifa</param>
    /// <param name="pVLRBOL">Valor do Boleto</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <returns>Tariffed</returns>
    public static Tariffed TariffValue( System.Int32 pUSUPRO,System.Int32 pUSUCED,System.Int16 pCODTAR,System.Decimal pVLRBOL,System.Int32 pUPDUSU)
    {
            ProcessCode= 0;
int _NIDCAL = ICalculate(pUSUPRO, pUSUCED,pCODTAR,pVLRBOL,pUPDUSU,1,1,null,null,true,0,0,0,0);
        if(_NIDCAL>0)
        return ThunderFire.Business.Lists.GetTariffed(_NIDCAL);
        return new Tariffed();
    }


    /// <summary>
    /// Obtêm o proximo numero de uma transacao especificada pelo codigo de tabelamento interno
    /// </summary>
    /// <param name="pCODTBI">Código da Tabela Interna</param>
    /// <returns>int</returns>
    public static int GetNextNumber( int pCODTBI)
    {
            ProcessCode= 0;
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.ReturnValue);
p.Add("@CODTBI", pCODTBI, dbType:DbType.Int32,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRGETNXTNUM", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
        }
        catch (Exception Error)
        {
            _logger.Info(Error);
            HasError =true;
        }
        }
        return RETURN_VALUE;
    }


    /// <summary>
    /// Obtêm o proximo numero de uma transacao especificada pelo codigo de tabelamento interno e o prefixo de aglutinação
    /// </summary>
    /// <param name="pPREFIX">Código da Tabela Interna</param>
    /// <param name="pCODTBI">Código da Tabela Interna</param>
    /// <returns>String</returns>
    public static String GetNextTransaction( System.String pPREFIX,int pCODTBI)
    {
            ProcessCode= 0;
        string  RETURN_VALUE = "";
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
        }
        catch (Exception Error)
        {
            _logger.Info(Error);
            HasError =true;
        }
        }
        return RETURN_VALUE;
    }


    /// <summary>
    /// Obtêm o saldo da conta conforme o indicador informado
    /// </summary>
    /// <param name="pINDLCT">Indicador de Lançamento</param>
    /// <param name="pNIDCTA">Id da Conta</param>
    /// <returns>String</returns>
    public static String GetCurrentAccountBalance( System.Int16 pINDLCT,int pNIDCTA)
    {
            ProcessCode= 0;
        string  RETURN_VALUE = "";
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.ReturnValue);
p.Add("@INDLCT", pINDLCT, dbType:DbType.Int16,  direction: ParameterDirection.Input);
p.Add("@NIDCTA", pNIDCTA, dbType:DbType.Int32,  direction: ParameterDirection.Input);
        }
        catch (Exception Error)
        {
            _logger.Info(Error);
            HasError =true;
        }
        }
        return RETURN_VALUE;
    }


    /// <summary>
    /// Obtêm o ID da Conta Virtual
    /// </summary>
    /// <param name="pCODUSU">Código do Usuário</param>
    /// <param name="pTIPCTA">Tipo de Conta</param>
    /// <param name="pORGCTA">Origem da Conta</param>
    /// <param name="pSTACTA">Status da Conta</param>
    /// <returns>int</returns>
    public static int GetVirtualAccount( System.Int32 pCODUSU,System.Byte pTIPCTA = 1,System.Byte pORGCTA = 1,System.Int16 pSTACTA = 250)
    {
            ProcessCode= 0;
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.ReturnValue);
p.Add("@CODUSU", pCODUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@TIPCTA", pTIPCTA, dbType:DbType.Byte,  direction: ParameterDirection.Input);
p.Add("@ORGCTA", pORGCTA, dbType:DbType.Byte,  direction: ParameterDirection.Input);
p.Add("@STACTA", pSTACTA, dbType:DbType.Int16,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRGETCTAVRT", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
        }
        catch (Exception Error)
        {
            _logger.Info(Error);
            HasError =true;
        }
        }
        return RETURN_VALUE;
    }


    /// <summary>
    /// Obtêm informações específicas do cartão com status 103, 109
    /// </summary>
/// <remarks>
/// <para>1 -Usuário Associado</para>
/// <para>2 - ID da Cont Virtual Vinculada</para>
/// <para>3 - Gestor do Cartão</para>
/// </remarks>
    /// <param name="pCODCRT">Código do Cartão</param>
    /// <param name="pGETMOD">Tipo de Informação</param>
    /// <returns>int</returns>
    public static int GetCardInformation( System.Int32 pCODCRT,System.Byte pGETMOD)
    {
            ProcessCode= 0;
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.ReturnValue);
p.Add("@CODCRT", pCODCRT, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@GETMOD", pGETMOD, dbType:DbType.Byte,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRGETINFCRT", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
        }
        catch (Exception Error)
        {
            _logger.Info(Error);
            HasError =true;
        }
        }
        return RETURN_VALUE;
    }


    /// <summary>
    /// Executa a gravação do Movimento de Transferencia em conta corrente  e retorna o número do lote gerado
    /// </summary>
    /// <param name="pNIDTRF">ID do Registro de Movimento de Transferência</param>
    /// <param name="pUPDUSU">Usuário de Atualização</param>
    /// <returns>int</returns>
    public static int CreateTransferCode( System.Int32 pNIDTRF,System.Int32 pUPDUSU)
    {
            ProcessCode= 0;
        int RETURN_VALUE = 0;
                    using (IDbConnection _conn = ConnectionFactory.GetConnection())
        {
        try
        {
            var p = new DynamicParameters();
            p.Add("@RETURN_VALUE", 0, dbType:DbType.Int32,  direction: ParameterDirection.ReturnValue);
p.Add("@NIDTRF", pNIDTRF, dbType:DbType.Int32,  direction: ParameterDirection.Input);
p.Add("@UPDUSU", pUPDUSU, dbType:DbType.Int32,  direction: ParameterDirection.Input);
_conn.Execute(sql:"PRREGCCRMAKTRF", param:p,  commandType: CommandType.StoredProcedure, commandTimeout: 120);
                RETURN_VALUE = (int)p.Get<Int32>("@RETURN_VALUE");
        }
        catch (Exception Error)
        {
            _logger.Info(Error);
            HasError =true;
        }
        }
        return RETURN_VALUE;
    }

    }
}
