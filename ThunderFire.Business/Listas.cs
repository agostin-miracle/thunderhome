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

namespace ThunderFire.Business
{
    public static partial class Lists
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public static int ProcessCode { get; set; }
        public static bool Found { get; set; } = false;
        public static bool HasError { get; set; } = false;

        /// <summary>
        /// Seleciona todos os Códigos de Índices da Tabela Geral
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> Tables()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT KEYCOD, DSCTAB FROM TBTABGR (NOLOCK) WHERE NUMTAB=0 AND STAREC IN (1,2) AND USRDSP=1 ORDER BY KEYCOD";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Países
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> Countries()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTABGER (NOLOCK) WHERE NUMTAB=1 AND STAREC=1";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Unidades da Federação
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> UF()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT NUMTAB,USECOD,KEYCOD,KEYTXT = LTRIM(RTRIM(KEYTXT)),NUMREF,DSCTAB,USRDSP,IDEPRE,STAREC,DATCAD,DATUPD,UPDUSU FROM TBTABGER (NOLOCK) A WHERE NUMTAB=2 AND STAREC IN(1,2)";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista Status de Registro
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> RegistrationStatus()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTABGER (NOLOCK) WHERE NUMTAB=7 AND STAREC IN (1,2)";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Sinal de Operação
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> Signals()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTABGER (NOLOCK) WHERE NUMTAB=10 AND STAREC=1";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Bancos
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> Banks()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT NUMTAB,USECOD,KEYCOD,KEYTXT = LTRIM(RTRIM(KEYTXT)),NUMREF,DSCTAB,USRDSP,IDEPRE,STAREC,DATCAD,DATUPD,UPDUSU FROM TBTABGER (NOLOCK) WHERE NUMTAB=12 AND STAREC=1";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Gêneros
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> Genders()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTABGER (NOLOCK) WHERE NUMTAB=13 AND STAREC=1";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Módulos do Aplicativo
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> Modules()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTABGER (NOLOCK) WHERE NUMTAB=14 AND STAREC=1";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista Tipos de Boleto
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> TicketTypes()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTABGER (NOLOCK) WHERE NUMTAB=16 AND STAREC=1";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista Tipos de Beneficiario
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> BeneficiaryTypes()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTABGER (NOLOCK) WHERE NUMTAB=20 AND STAREC IN(1,2)";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista Origem da Conta
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> AccountOrigin()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTABGER (NOLOCK) WHERE NUMTAB=23 AND STAREC=1";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista Condição de Bloqueio
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> BlockingCondition()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTABGER (NOLOCK) WHERE NUMTAB=29 AND STAREC=1";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista Indicadores de Lançamento
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> AccountEntryIndicators()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTABGER (NOLOCK) WHERE NUMTAB=39 AND STAREC=1";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Operadoras de Telefonia
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> TelephoneOperators()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTABGER (NOLOCK) WHERE NUMTAB=45 AND STAREC=1";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Tipos de Mensalidade
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> MonthlyPaymentTypes()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTABGER (NOLOCK) WHERE NUMTAB=50 AND STAREC=1";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Níveis de Confinaça
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> TrustLevel()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTABGER (NOLOCK) WHERE NUMTAB=54 AND STAREC=1";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Tipo de Transação de Movimento
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> TransactionMovement()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTABGER (NOLOCK) WHERE NUMTAB=59 AND STAREC=1";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Nacionalidades
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> Nationalities()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTABGER (NOLOCK) WHERE NUMTAB=80 AND STAREC=1";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Logradouros
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> PublicPlaces()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTABGER (NOLOCK) WHERE NUMTAB=81 AND STAREC=1";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Subsistemas
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> Subsystems()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTABGER (NOLOCK) WHERE NUMTAB=93 AND STAREC=1";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Responsável pela Tarifa
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> TariffResponsible()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTABGER (NOLOCK) WHERE NUMTAB=105 AND STAREC IN (1,2)";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Associação de Origem
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> OriginAssociation()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTABGER (NOLOCK) WHERE NUMTAB=106 AND STAREC IN (1,2)";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Regra de Aplicação da Taxa do Depositante
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> DepositorFee()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTABGER (NOLOCK) WHERE NUMTAB=912 AND STAREC IN (1,2)";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Nivel de Configuração
        /// </summary>
        /// <returns>Listof GeneralTable</returns>
        public static List<GeneralTable> ConfigurationLevel()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBTABGER (NOLOCK) WHERE NUMTAB=947 AND STAREC IN (1,2)";
                    var result = _conn.Query<GeneralTable>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm o registro de calculo conforme o id informado
        /// </summary>
        /// <param name="pNIDCAL">Id do Registro de Cálculo</param>
        /// <returns>of Tariffed</returns>
        public static Tariffed GetTariffed(System.Int32 pNIDCAL)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT * FROM TBCALTAR (NOLOCK) A WHERE (A.NIDCAL=@NIDCAL)";
                    var result = _conn.Query<Tariffed>(sql: _sql, param: new { NIDCAL = pNIDCAL }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.FirstOrDefault();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Avalista
        /// </summary>
        /// <returns>Listof MyUsers</returns>
        public static List<MyUsers> Guarantor()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT CODUSU, NOMUSU FROM TBCADGER (NOLOCK) A WHERE A.CODATR= 1 AND A.STAREC IN (1,2) AND A.STAUSU=61";
                    var result = _conn.Query<MyUsers>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Gestores Ativos associados à tabela de usuários
        /// </summary>
        /// <returns>Listof MyUsers</returns>
        public static List<MyUsers> Managers()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT CODUSU = USUPRO, NOMUSU = B.NOMUSU + ' -' + C.DSCPRO FROM TBUSUPRO (NOLOCK) A INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.CODUSU) INNER JOIN TBCADPRO (NOLOCK) C ON (C.CODPRO = A.CODPRO)  WHERE A.STAREC = 1 AND B.STAREC=1 AND C.STAREC=1";
                    var result = _conn.Query<MyUsers>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Gestores Ativos associados à tabela de usuários por usuário
        /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
        /// <returns>Listof MyUsers</returns>
        public static List<MyUsers> Managers(System.Int32 pCODUSU)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT CODUSU = USUPRO, NOMUSU = B.NOMUSU + ' -' + C.DSCPRO FROM TBUSUPRO (NOLOCK) A INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.CODUSU) INNER JOIN TBCADPRO (NOLOCK) C ON (C.CODPRO = A.CODPRO)  WHERE A.STAREC = 1 AND B.STAREC=1 AND C.STAREC=1 AND (A.CODUSU=@CODUSU)";
                    var result = _conn.Query<MyUsers>(sql: _sql, param: new { CODUSU = pCODUSU }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Gestores por linha de produto
        /// </summary>
        /// <param name="pLINPRO">Linha de Produto</param>
        /// <returns>Listof MyUsers</returns>
        public static List<MyUsers> ManagersByLine(System.Int16 pLINPRO)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT CODUSU = A.USUPRO, NOMUSU = LTRIM(RTRIM(NOMUSU)) + ' - ' + C.DSCPRO, REFCOD= A.CODUSU
  FROM TBUSUPRO (NOLOCK) A
 INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.CODUSU)
 INNER JOIN TBCADPRO (NOLOCK) C ON (C.CODPRO = A.CODPRO) WHERE (A.LINPRO=@LINPRO) ORDER BY C.DSCPRO";
                    var result = _conn.Query<MyUsers>(sql: _sql, param: new { LINPRO = pLINPRO }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm uma lista de usuários com contas virtuais ativas
        /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
        /// <returns>Listof MyUsers</returns>
        public static List<MyUsers> UsersAccounts(System.Int32? pCODUSU)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<MyUsers>(sql: "PRCADGERSELCTA", param: new { CODUSU = pCODUSU }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Contas Virtuais com o propósito de efetuar a vinculação para um determinado usuário
        /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
        /// <returns>Listof MyUsers</returns>
        public static List<MyUsers> UsersLinkeds(System.Int32 pCODUSU)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT CODUSU = NIDCTA, 
NOMUSU = C.NOMUSU + ' CONTA : ' + NUMAGE + '.' + LTRIM(RTRIM(NUMCTA))+'-'+NUMDVE + ' BANCO :' +A.NUMBCO + ' ' + B.DSCTAB
+ ' BENFICIARIO : ' + CASE WHEN A.ORGCTA IN (1,3,4)  THEN 'O PROPRIO' ELSE NOMBNF END
,REFCOD=A.CODUSU
FROM TBCADCTA (NOLOCK) A
INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB=12 AND B.KEYTXT = A.NUMBCO)
INNER JOIN TBCADGER (NOLOCK) C ON (C.CODUSU = A.CODUSU)
WHERE A.STAREC=1 AND STACTA=250 AND NIDCTA NOT IN (SELECT NIDCTA FROM TBCADCVL  WHERE CODUSU = @CODUSU) ORDER BY A.NOMBNF";
                    var result = _conn.Query<MyUsers>(sql: _sql, param: new { CODUSU = pCODUSU }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm uma lista de usuários vinculados ao gerenciamento de Produto
        /// </summary>
        /// <param name="pCODPRO">Código do Produto</param>
        /// <returns>Listof MyUsers</returns>
        public static List<MyUsers> UsersByProduct(System.Int16? pCODPRO)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<MyUsers>(sql: "PRCADGERSELUSRPRO", param: new { CODPRO = pCODPRO }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Usuários com atributo gestor
        /// </summary>
        /// <returns>Listof MyUsers</returns>
        public static List<MyUsers> UsersManagers()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT CODUSU, NOMUSU, REFCOD = TIPUSU FROM TBCADGER (NOLOCK) A WHERE CODATR=3";
                    var result = _conn.Query<MyUsers>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm uma lista de usuários por tipo de usuário
        /// </summary>
        /// <param name="pTIPUSU">Tipo de Usuário</param>
        /// <param name="pCODUSU">Código do Usuário</param>
        /// <returns>Listof MyUsers</returns>
        public static List<MyUsers> UsersByType(System.Byte? pTIPUSU, System.Int32? pCODUSU)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT CODUSU, NOMUSU, REFCOD = TIPUSU FROM TBCADGER (NOLOCK) A WHERE (A.TIPUSU=@TIPUSU) AND (A.CODUSU=@CODUSU)";
                    var result = _conn.Query<MyUsers>(sql: _sql, param: new { TIPUSU = pTIPUSU, CODUSU = pCODUSU }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm uma lista de usuários com permissão de uso da tarifação
        /// </summary>
        /// <returns>Listof MyUsers</returns>
        public static List<MyUsers> UserTarifacion()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT CODUSU, NOMUSU, REFCOD = TIPUSU FROM TBCADGER (NOLOCK) A  WHERE STAREC=1 AND STAUSU=61 AND CODATR IN (SELECT CODATR FROM TBTIPATR WHERE USETAR=1)";
                    var result = _conn.Query<MyUsers>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm uma lista de usuários com base no nome fornecido
        /// </summary>
        /// <param name="pNOMUSU">Nome do Usuário</param>
        /// <returns>Listof MyUsers</returns>
        public static List<MyUsers> GetUsersByName(string pNOMUSU)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT CODUSU, NOMUSU, REFCOD = TIPUSU 
                  FROM TBCADGER (NOLOCK) A 
                  WHERE STAREC=1 AND STAUSU=61 AND (@NOMUSU IS NULL OR A.NOMUSU LIKE '%'+@NOMUSU+'%')";
                    var result = _conn.Query<MyUsers>(sql: _sql, param: new { NOMUSU = pNOMUSU }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Usuários contidos em um boleto
        /// </summary>
        /// <param name="pORGREA">Origem de Leitura</param>
        /// <returns>Listof MyUsers</returns>
        public static List<MyUsers> UsersInTickets(System.Byte pORGREA)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"IF(@ORGREA=1)
  SELECT DISTINCT CODUSU = A.USUCED, B.NOMUSU FROM TBREGBOL (NOLOCK) A INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.USUCED)
IF(@ORGREA=2)
  SELECT DISTINCT CODUSU = A.USUSAC, B.NOMUSU FROM TBREGBOL (NOLOCK) A INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.USUSAC)
IF(@ORGREA=3)
  BEGIN
      SELECT CODUSU = A.USUPRO, NOMUSU = LTRIM(RTRIM(C.NOMUSU)) + ' - ' + D.DSCPRO, REFCOD=C.CODUSU
        FROM TBREGBOL (NOLOCK) A
       INNER JOIN TBUSUPRO (NOLOCK) B ON (B.USUPRO = A.USUPRO)
       INNER JOIN TBCADGER (NOLOCK) C ON (C.CODUSU = B.CODUSU)
       INNER JOIN TBCADPRO (NOLOCK) D ON (D.CODPRO = B.CODPRO)
  END";
                    var result = _conn.Query<MyUsers>(sql: _sql, param: new { ORGREA = pORGREA }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Usuários contidos em mensalidades
        /// </summary>
        /// <returns>Listof MyUsers</returns>
        public static List<MyUsers> UsersInMonthly()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT DISTINCT CODUSU, B.NOMUSU, REFCOD=NIDMEN FROM TBREGBMEN (NOLOCK) A INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.CODUSU)";
                    var result = _conn.Query<MyUsers>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Usuários contidos em um boleto
        /// </summary>
        /// <param name="pSRCUSU">Usuário Gestor</param>
        /// <returns>Listof MyUsers</returns>
        public static List<MyUsers> UsersForTickets(System.Int32? pSRCUSU)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT CODUSU, NOMUSU, REFCOD = SRCUSU, REFSEL=CODATR FROM TBCADGER (NOLOCK) A  WHERE A.STAREC=1 AND A.STAUSU=61 AND CODATR IN( SELECT CODATR FROM TBTIPATR WHERE USEBOL=1) AND (A.SRCUSU=@SRCUSU)";
                    var result = _conn.Query<MyUsers>(sql: _sql, param: new { SRCUSU = pSRCUSU }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm uma lista de registros do cadastro geral conforme parâmetros informados
        /// </summary>
        /// <param name="pCODATR">Atributo</param>
        /// <param name="pSTAUSU">Status do Usuário</param>
        /// <param name="pSRCUSU">ID do Responsável</param>
        /// <param name="pNOMUSU">Nome</param>
        /// <param name="pSTAREC">Status do Registro</param>
        /// <returns>Listof QueryUsers</returns>
        public static List<QueryUsers> Users(System.Int16? pCODATR, System.Int16? pSTAUSU, System.Int32? pSRCUSU, string pNOMUSU, System.Byte? pSTAREC)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<QueryUsers>(sql: "PRCADGERSELALL", param: new { CODATR = pCODATR, STAUSU = pSTAUSU, SRCUSU = pSRCUSU, NOMUSU = pNOMUSU, STAREC = pSTAREC }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm uma lista de registros do cadastro geral simplificada por codio e descrição
        /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
        /// <param name="pSRCUSU">Código do Gestor</param>
        /// <returns>Listof MyUsers</returns>
        public static List<MyUsers> Users(System.Int32? pCODUSU, System.Int32? pSRCUSU)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<MyUsers>(sql: "PRCADGERSELRED", param: new { CODUSU = pCODUSU, SRCUSU = pSRCUSU }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Tipos de Endereço
        /// </summary>
        /// <returns>Listof AddressType</returns>
        public static List<AddressType> AddressTypes()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT A.*, DSCREC = B.DSCTAB, LGNUSU  = ISNULL(C.LGNUSU,'')
  FROM TBTIPEND (NOLOCK) A
 INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB=7 AND B.KEYCOD=A.STAREC)
  LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV=1) ORDER BY A.TIPEND";
                    var result = _conn.Query<AddressType>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Tipos de Contato
        /// </summary>
        /// <returns>Listof ContactType</returns>
        public static List<ContactType> ContactTypes()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT A.*, B.DSCTAB AS DSCREC, ISNULL(C.LGNUSU,'') LGNUSU FROM TBTIPCTO (NOLOCK) A INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB=7 AND B.KEYCOD = A.STAREC) LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV=1)";
                    var result = _conn.Query<ContactType>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista Tipos de Usuário
        /// </summary>
        /// <returns>Listof UserType</returns>
        public static List<UserType> UserTypes()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT A.*, B.DSCTAB AS DSCREC, ISNULL(C.LGNUSU,'') LGNUSU 
    FROM TBTIPUSU (NOLOCK) A 
   INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB=7 AND B.KEYCOD = A.STAREC) 
   LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV=1) ORDER BY A.TIPUSU";
                    var result = _conn.Query<UserType>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista Tipos de Atributo
        /// </summary>
        /// <returns>Listof AttributeType</returns>
        public static List<AttributeType> AttributeTypes()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT A.*, B.DSCTAB AS DSCREC, ISNULL(C.LGNUSU,'') LGNUSU 
    FROM TBTIPATR (NOLOCK) A 
   INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB=7 AND B.KEYCOD = A.STAREC) 
   LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV=1) ORDER BY A.CODATR";
                    var result = _conn.Query<AttributeType>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista Linha de Produtos
        /// </summary>
        /// <returns>Listof ProductLine</returns>
        public static List<ProductLine> ProductLines()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT A.*, DSCREC= ISNULL(B.DSCTAB,''), LGNUSU = ISNULL(C.LGNUSU,'') FROM TBLINPRO (NOLOCK) A
INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB =7 AND B.KEYCOD=A.STAREC)
 LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU  AND C.REGATV=1) ORDER BY A.LINPRO";
                    var result = _conn.Query<ProductLine>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Tipos de Conta
        /// </summary>
        /// <returns>Listof AccountType</returns>
        public static List<AccountType> AccountTypes()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT A.*, B.DSCTAB AS DSCREC, ISNULL(C.LGNUSU,'') LGNUSU 
FROM TBTIPCTA (NOLOCK) A INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB=7 AND B.KEYCOD = A.STAREC) 
LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV=1)";
                    var result = _conn.Query<AccountType>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm a Lista de Operacoes
        /// </summary>
        /// <param name="pSUBSYS">Subsistema</param>
        /// <returns>Listof Operations</returns>
        public static List<Operations> Operations(System.Byte? pSUBSYS)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<Operations>(sql: "PRTIPMOVSELALL", param: new { SUBSYS = pSUBSYS }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista Status das Transações de acordo com o módulo informado
        /// </summary>
        /// <param name="pCODMOD">Código do Módulo</param>
        /// <returns>Listof TransactionStatus</returns>
        public static List<TransactionStatus> Transactions(System.Int16? pCODMOD)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<TransactionStatus>(sql: "PRCADSTASELALL", param: new { CODMOD = pCODMOD }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm uma lista de todos os endereços de um usuário
        /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
        /// <param name="pTIPEND">Tipo de Endereço</param>
        /// <param name="pREGATV">Registro Ativo</param>
        /// <param name="pSTAREC">Status de Registro</param>
        /// <returns>Listof QueryAddressBook</returns>
        public static List<QueryAddressBook> Addresses(System.Int32 pCODUSU, System.Int16? pTIPEND = -1, System.Int16? pREGATV = -1, System.Int16? pSTAREC = -1)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<QueryAddressBook>(sql: "PRCADENDSELALL", param: new { CODUSU = pCODUSU, TIPEND = pTIPEND, REGATV = pREGATV, STAREC = pSTAREC }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Seleciona todos os registros de contato do usuário fornecido
        /// </summary>
        /// <param name="pCODUSU">Usuário</param>
        /// <returns>Listof QueryContactBook</returns>
        public static List<QueryContactBook> Contacts(System.Int32? pCODUSU)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<QueryContactBook>(sql: "PRCADCTOSELALL", param: new { CODUSU = pCODUSU }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm uma lista de todos os cartões da base ativa conforme parâmetros de pesquisa informados
        /// </summary>
        /// <param name="pUSUPRO">Usuário Gestor</param>
        /// <param name="pSTACRT">Status do Cartão</param>
        /// <param name="pNUMCRT">Parte ou Número do Cartão</param>
        /// <param name="pNOMPRT">Nome ou Parte</param>
        /// <returns>Listof QueryActiveCards</returns>
        public static List<QueryActiveCards> ListCards(System.Int32 pUSUPRO, System.Int16? pSTACRT = null, string pNUMCRT = "", string pNOMPRT = null)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<QueryActiveCards>(sql: "PRREGCRTSELALL", param: new { USUPRO = pUSUPRO, STACRT = pSTACRT, NUMCRT = pNUMCRT, NOMPRT = pNOMPRT }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm os registros de mensalidade conforme os parâmetros informados
        /// </summary>
        /// <param name="pUSUPRO">Gestor de Produto</param>
        /// <param name="pTIPMEN">Tipo de Mensalidade</param>
        /// <param name="pSTAMEN">Status da Mensalidade</param>
        /// <param name="pCODUSU">Código do Usuario</param>
        /// <returns>Listof QueryMonthlyPayment</returns>
        public static List<QueryMonthlyPayment> MonthlyPayments(int pUSUPRO, System.Byte pTIPMEN, System.Int32? pSTAMEN, int? pCODUSU)
        {
            ProcessCode = 200;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<QueryMonthlyPayment>(sql: "PRREGMENSELALL", param: new { USUPRO = pUSUPRO, TIPMEN = pTIPMEN, STAMEN = pSTAMEN, CODUSU = pCODUSU }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm os registros de resumo de mensalidade conforme os parâmetros informados
        /// </summary>
        /// <param name="pUSUPRO">Gestor de Produto</param>
        /// <param name="pTIPMEN">Tipo de Mensalidade</param>
        /// <param name="pSTAMEN">Status da Mensalidade</param>
        /// <param name="pCODUSU">Código do Usuário</param>
        /// <returns>Listof QueryMonthlyPayment</returns>
        public static List<QueryMonthlyPayment> MonthlyPaymentSummary(int pUSUPRO, System.Byte pTIPMEN, System.Int16? pSTAMEN, System.Int32? pCODUSU)
        {
            ProcessCode = 205;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<QueryMonthlyPayment>(sql: "PRREGMENRESALL", param: new { USUPRO = pUSUPRO, TIPMEN = pTIPMEN, STAMEN = pSTAMEN, CODUSU = pCODUSU }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm uma lista de usuários vinculados as mensalidades
        /// </summary>
        /// <param name="pTIPMEN">Tipo de Mensalidade</param>
        /// <returns>Listof MyUsers</returns>
        public static List<MyUsers> ListUsers(System.Byte? pTIPMEN)
        {
            ProcessCode = 210;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT DISTINCT A.CODUSU, B.NOMUSU, REFCOD=0 FROM TBREGMEN (NOLOCK) A INNER JOIN TBCADGER (NOLOCK) B ON (B.CODUSU = A.CODUSU) WHERE (A.TIPMEN=@TIPMEN)";
                    var result = _conn.Query<MyUsers>(sql: _sql, param: new { TIPMEN = pTIPMEN }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm uma lista de todos os produtos
        /// </summary>
        /// <param name="pLINPRO">Linha de Produto</param>
        /// <returns>Listof Product</returns>
        public static List<Product> Products(System.Int16? pLINPRO)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<Product>(sql: "PRCADPROSELALL", param: new { LINPRO = pLINPRO }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista Tipos de Tarifa
        /// </summary>
        /// <returns>Listof TariffType</returns>
        public static List<TariffType> TariffTypes()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT A.*, DSCREC= ISNULL(B.DSCTAB,''), 
LGNUSU = ISNULL(C.LGNUSU,''), D.DSCMOV 
FROM TBTIPTAR (NOLOCK) A
INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB =7 AND B.KEYCOD=A.STAREC)
 LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV =1)
INNER JOIN TBTIPMOV (NOLOCK) D ON (D.CODMOV = A.CODMOV) ORDER BY A.CODTAR";
                    var result = _conn.Query<TariffType>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm todos os registros de contas virtuais registradas conforme parametro fornecido
        /// </summary>
        /// <param name="pSTACTA">Status da Conta</param>
        /// <param name="pNOMUSU">Nome ou parte do nome</param>
        /// <returns>Listof Accounts</returns>
        public static List<Accounts> Accounts(System.Int16? pSTACTA = null, string pNOMUSU = "")
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<Accounts>(sql: "PRCADCTASELALL", param: new { STACTA = pSTACTA, NOMUSU = pNOMUSU }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Contas vinculadas por usuário
        /// </summary>
        /// <param name="pNOMUSU">Código do Usuario</param>
        /// <returns>Listof LinkedAccount</returns>
        public static List<LinkedAccount> LinkedAccounts(string pNOMUSU)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<LinkedAccount>(sql: "PRCADCVLSELALL", param: new { NOMUSU = pNOMUSU }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Listagem de Gestores de Produtos
        /// </summary>
        /// <param name="pCODUSU">Código do Usuário</param>
        /// <param name="pCODPRO">Código do Produto</param>
        /// <returns>Listof ProductManager</returns>
        public static List<ProductManager> ProductManagers(System.Int32? pCODUSU = null, System.Int16? pCODPRO = null)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<ProductManager>(sql: "PRUSUPROSELALL", param: new { CODUSU = pCODUSU, CODPRO = pCODPRO }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm todos os registro de configuração de boleto de acordo com os parâmetros fornecidos
        /// </summary>
        /// <param name="pNIVCFG">Nivel de Configuração</param>
        /// <param name="pUSUCFG">Código do Usuário de configuração</param>
        /// <returns>Listof TicketConfiguration</returns>
        public static List<TicketConfiguration> TicketConfigurations(System.Byte pNIVCFG, System.Int32? pUSUCFG)
        {
            ProcessCode = 115;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<TicketConfiguration>(sql: "PRCFGBOLSELALL", param: new { NIVCFG = pNIVCFG, USUCFG = pUSUCFG }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm informações do boleto para fatura
        /// </summary>
        /// <param name="pNIDBOL">ID do Boleto</param>
        /// <returns>Listof TicketInvoice</returns>
        public static List<TicketInvoice> TicketInvoice(System.Int32 pNIDBOL)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<TicketInvoice>(sql: "PRREGBOLSELTCK", param: new { NIDBOL = pNIDBOL }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm informações do boleto para obtenção de registro
        /// </summary>
        /// <param name="pNIDBOL">ID do Boleto</param>
        /// <returns>of TicketRegister</returns>
        public static TicketRegister TicketForRegister(System.Int32 pNIDBOL)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<TicketRegister>(sql: "PRREGBOLSELREC", param: new { NIDBOL = pNIDBOL }, commandType: CommandType.StoredProcedure, commandTimeout: 120).FirstOrDefault();

                    if (result != null)
                        Found = true;
                    return result;
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Boletos conforme parâmetros
        /// </summary>
        /// <param name="pUSUPRO">ID do Gestor</param>
        /// <param name="pUSUCED">ID do Cedente</param>
        /// <param name="pUSUSAC">ID do Sacado</param>
        /// <param name="pCODAVA">ID do Avalista</param>
        /// <param name="pSTABOL">Status do Boleto</param>
        /// <param name="pTIPBOL">Tipo de Boleto</param>
        /// <param name="pNIDBOL">ID do Boleto</param>
        /// <param name="pDATEMI1">Data de Emissão Inicial (yyyyMMdd)</param>
        /// <param name="pDATEMI2">Data de Emissão Final (yyyyMMdd HH:mm:ss)</param>
        /// <param name="pDATVCT1">Data de Vencimento Inicial (yyyyMMdd)</param>
        /// <param name="pDATVCT2">Data de Vencimento Final (yyyyMMdd HH:mm:ss)</param>
        /// <param name="pDATPGT1">Data de Pagamento Inicial (yyyyMMdd)</param>
        /// <param name="pDATPGT2">Data de Pagamento Final (yyyyMMdd HH:mm:ss)</param>
        /// <returns>Listof TicketQuery</returns>
        public static List<TicketQuery> Tickets(System.Int32? pUSUPRO, System.Int32? pUSUCED, System.Int32? pUSUSAC, System.Int32? pCODAVA, System.Int16? pSTABOL, System.Int16? pTIPBOL, System.Int32? pNIDBOL, string pDATEMI1, string pDATEMI2, string pDATVCT1, string pDATVCT2, string pDATPGT1, string pDATPGT2)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<TicketQuery>(sql: "PRREGBOLSELALL", param: new { USUPRO = pUSUPRO, USUCED = pUSUCED, USUSAC = pUSUSAC, CODAVA = pCODAVA, STABOL = pSTABOL, TIPBOL = pTIPBOL, NIDBOL = pNIDBOL, DATEMI1 = pDATEMI1, DATEMI2 = pDATEMI2, DATVCT1 = pDATVCT1, DATVCT2 = pDATVCT2, DATPGT1 = pDATPGT1, DATPGT2 = pDATPGT2 }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm todos os registros de operações de um susbsistema e ID de referencia específico
        /// </summary>
        /// <param name="pSUBSYS">ID do SubSistema</param>
        /// <param name="pNIDREF">ID de Referência</param>
        /// <returns>Listof OperationsRegister</returns>
        public static List<OperationsRegister> OperationsRegisters(System.Byte pSUBSYS, System.Int32 pNIDREF)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<OperationsRegister>(sql: "PRREGOPESELALL", param: new { SUBSYS = pSUBSYS, NIDREF = pNIDREF }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Tipos de Lançamento
        /// </summary>
        /// <param name="pUSETRF">Usar Transferencias</param>
        /// <returns>Listof AccountEntryType</returns>
        public static List<AccountEntryType> AccountEntryTypes(System.Byte? pUSETRF)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<AccountEntryType>(sql: "PRTIPLCTSELALL", param: new { USETRF = pUSETRF }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Cadastro de Lançamentos
        /// </summary>
        /// <param name="pTIPLCT">Tipo de Lançamento</param>
        /// <returns>Listof AccountEntryPosting</returns>
        public static List<AccountEntryPosting> AccountEntryPostings(System.Int16? pTIPLCT)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<AccountEntryPosting>(sql: "PRCADLCTSELALL", param: new { TIPLCT = pTIPLCT }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Lista de Feriados
        /// </summary>
        /// <returns>Listof Holydays</returns>
        public static List<Holydays> Holydays()
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    string _sql = @"SELECT A.*, DSCREC = B.DSCTAB, LGNUSU  = ISNULL(C.LGNUSU,''),DSCUFE = ISNULL(D.DSCTAB,'')
    FROM TBCADFER (NOLOCK) A
    INNER JOIN TBTABGER (NOLOCK) B ON (B.NUMTAB=7 AND B.KEYCOD=A.STAREC)
    INNER JOIN TBTABGER (NOLOCK) D ON (B.NUMTAB=2 AND B.KEYTXT=A.CODUFE)
    LEFT JOIN TBLGNUSU (NOLOCK) C ON (C.CODUSU = A.UPDUSU AND C.REGATV=1) ORDER BY A.DATMOV, A.CODUFE";
                    var result = _conn.Query<Holydays>(sql: _sql, param: new { }, commandType: CommandType.Text, commandTimeout: 120).ToList();
                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm todos os registros de transferências de acordo com os parâmetros informados
        /// </summary>
        /// <param name="pTIPLCT">Tipo de Lançamento</param>
        /// <param name="pCODUSU">Código do Usuário</param>
        /// <param name="pNIDTRA">ID da Transação</param>
        /// <param name="pSTAREC">Status de Registro</param>
        /// <returns>Listof TransferRegistration</returns>
        public static List<TransferRegistration> TransferRegistrations(System.Int16? pTIPLCT, System.Int32? pCODUSU, string pNIDTRA, System.Byte? pSTAREC)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<TransferRegistration>(sql: "PRREGMOVSELALL", param: new { TIPLCT = pTIPLCT, CODUSU = pCODUSU, NIDTRA = pNIDTRA, STAREC = pSTAREC }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

        /// <summary>
        /// Obtêm uma lista de usuários com contas virtuais ativas
        /// </summary>
        /// <param name="pFUNLCT">Funcao de Lançamento</param>
        /// <param name="pNOMUSU">Nome do Usuário</param>
        /// <returns>Listof MyUsers</returns>
        public static List<MyUsers> AccountsByEntryCondition(System.Byte pFUNLCT, string pNOMUSU)
        {
            ProcessCode = 0;
            Found = false;
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    var result = _conn.Query<MyUsers>(sql: "PRCADCTASELACTCND", param: new { FUNLCT = pFUNLCT, NOMUSU = pNOMUSU }, commandType: CommandType.StoredProcedure, commandTimeout: 120).ToList();

                    if (result != null)
                        Found = true;
                    return result.ToList();
                }
                catch (Exception Error)
                {
                    HasError = true;
                    Found = false;
                    _logger.Info(Error);
                }
            }
            return null;
        }

    }
}
