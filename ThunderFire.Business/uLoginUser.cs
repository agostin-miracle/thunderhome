using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderFire.Connector;
using ThunderFire.Domain.DTO;
using ThunderFire.Domain.Models;
using ThunderFire.Interface;

namespace ThunderFire.Business
{
    public partial class LoginUserDao : BusinessBase, ILoginUser
    {
        /// <summary>
        /// Obtêm o registro de controle de acesso do usuário
        /// </summary>
        /// <param name="pLGNNUM">ID de Registro de Login</param>
        /// <returns>AccessControl</returns>
        private AccessControl IGetAccessControl(System.Int32 pLGNNUM)
        {
            this.Found = false;
            AccessControl RETURN_VALUE = new AccessControl();
            using (IDbConnection _conn = ConnectionFactory.GetConnection())
            {
                try
                {
                    RETURN_VALUE = _conn.Query<AccessControl>(sql: "PRLGNUSUACECTL", param: new
                    {
                        LGNNUM = pLGNNUM
                    }, commandType: CommandType.StoredProcedure, commandTimeout: 120).FirstOrDefault();
                    this.Found = true;
                }
                catch (Exception Error)
                {
                    this.HasError = true;
                    _logger.Info(Error);
                }

                if (!HasError)
                {
                    try
                    {
                        string SQL = "SELECT * FROM TBCADGER (NOLOCK) WHERE CODUSU = (SELECT CODUSU FROM TBLGNUSU (NOLOCK) WHERE LGNNUM=@LGNNUM)";
                        Users user = _conn.Query<Users>(sql: SQL, param: new
                        {
                            LGNNUM = pLGNNUM
                        }, commandType: CommandType.Text, commandTimeout: 120).FirstOrDefault();

                        if (user != null)
                            RETURN_VALUE.User = user;
                        user = null;
                    }
                    catch (Exception Error)
                    {
                        this.HasError = true;
                        _logger.Info(Error);
                    }
                }
                if (!HasError)
                {
                    try
                    {
                        string SQL = "SELECT TOP 1 * FROM TBCADCTA (NOLOCK) WHERE CODUSU = (SELECT CODUSU FROM TBLGNUSU (NOLOCK) WHERE LGNNUM=@LGNNUM) AND STAREC=1 AND STACTA=250";
                        VirtualAccount va = _conn.Query<VirtualAccount>(sql: SQL, param: new
                        {
                            LGNNUM = pLGNNUM
                        }, commandType: CommandType.Text, commandTimeout: 120).FirstOrDefault();

                        if (va != null)
                            RETURN_VALUE.Account = va;
                        va = null;
                    }
                    catch (Exception Error)
                    {
                        this.HasError = true;
                        _logger.Info(Error);
                    }
                }

                if (!HasError)
                {
                    try
                    {
                        byte _TIPEND = 0;
                        if (RETURN_VALUE.User != null)
                        {
                            _TIPEND = 2;
                            if (RETURN_VALUE.User.CODPJU.ToUpper() == "F")
                            {
                                _TIPEND = 1;
                            }

                        }
                        string SQL = String.Format("SELECT TOP 1 * FROM TBCADEND (NOLOCK) WHERE CODUSU = (SELECT CODUSU FROM TBLGNUSU (NOLOCK) WHERE LGNNUM=@LGNNUM) AND STAREC=1 AND REGATV=1 AND TIPEND={0}", _TIPEND);
                        AddressBook ad = _conn.Query<AddressBook>(sql: SQL, param: new
                        {
                            LGNNUM = pLGNNUM
                        }, commandType: CommandType.Text, commandTimeout: 120).FirstOrDefault();

                        if (ad != null)
                            RETURN_VALUE.Address = ad;
                        ad = null;
                    }
                    catch (Exception Error)
                    {
                        this.HasError = true;
                        _logger.Info(Error);
                    }
                }

                if (!HasError)
                {
                    try
                    {
                        int _ASSUSU = 0;
                        if (RETURN_VALUE.User != null)
                            _ASSUSU = RETURN_VALUE.User.CODUSU;
                        if (_ASSUSU > 0)
                        {
                            string SQL = String.Format("SELECT CODCRT, NUMCRT FROM TBREGCRT (NOLOCK) WHERE ASSUSU = {0} AND STAREC=1 AND STACRT IN (109,103)", _ASSUSU);
                            var ad = _conn.Query<MyCards>(sql: SQL, commandType: CommandType.Text, commandTimeout: 120).ToList();
                            if (ad != null)
                                RETURN_VALUE.Cards = ad;
                            ad = null;
                        }
                    }
                    catch (Exception Error)
                    {
                        this.HasError = true;
                        _logger.Info(Error);
                    }
                }

                /* objeto carregado*/
                if (!HasError)
                {
                    RETURN_VALUE.IsValid = true;
                    RETURN_VALUE.IsLogged = true;
                }

            }
            return RETURN_VALUE;
        }
    }
}
