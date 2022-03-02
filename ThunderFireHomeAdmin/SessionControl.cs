using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThunderFire.Business;
using ThunderFire.Domain.DTO;
using ThunderFireHomeAdmin.Models;

namespace ThunderFireHomeAdmin
{
    public class SessionControl
    {
        private static readonly string SESSION_NAME = "USERLOGGED";
        private static AccessControl current =null;

        /// <summary>
        ///  Verifica se o usuario corrente está logado
        /// </summary>
        /// <returns>bool</returns>
        public static bool IsLogged()
        {
            if (HasSession())
                return (current.LGNNUM > 0 && current.IsValid);
            return false;
        }
        /// <summary>
        /// Obtêm o tipo do usuário
        /// </summary>
        /// <returns>byte</returns>
        public static byte GetUserType()
        {
            if (HasSession())
                return current.User.TIPUSU;
            return 0;
        }

        public static AccessControl Current { get { return current; }  }


        public static ExecutionResponse Authenticate(LoginEntry model)
        {
            bool falha = true;
            ExecutionResponse result = new ExecutionResponse();
            result.ErrorCode = "FAILALL";
            result.Logged = false;

            if (model.LGNTYP >= 1 && model.LGNTYP <= 4)
            {
                if (!String.IsNullOrWhiteSpace(model.LGNUSU))
                {
                    if (!String.IsNullOrWhiteSpace(model.PSWUSU))
                    {
                        /*
                         * Decripta a senha de transferencia 
                         */
                        string _PSWUSU = ThunderFire.UseRijndael.Decrypt(model.PSWUSU);

                        if (!string.IsNullOrWhiteSpace(_PSWUSU))
                        {
                            /*
                             * Encripta para o banco de dados 
                             */
                            string _DBWUSU = ThunderFire.Business.Support.Encrypt(_PSWUSU);
                            if (!String.IsNullOrWhiteSpace(_DBWUSU))
                            {
                                LoginUserDao obj = new LoginUserDao();
                                result = obj.Login(model.LGNTYP, model.LGNUSU, _DBWUSU);
                                int _LGNNUM = Convert.ToInt32(result.ReturnValue);
                                if (_LGNNUM > 0)
                                {
                                    var SessionData = obj.GetAccessControl(_LGNNUM);
                                    if (SessionData.IsValid)
                                    {
                                        SessionData.LGNNUM = _LGNNUM;
                                        HttpContext.Current.Session[SESSION_NAME] = SessionData;
                                        falha = false;
                                    }
                                    else
                                        result.MessageToUser = "Não foi possível atribuir o controle de operação";
                                }
                            }
                            else
                                result.MessageToUser = "Não foi possível efetuar a leitura da senha fornecida (I)";
                        }
                    }
                    else
                        result.MessageToUser = "Senha não fornecida";
                }
                else
                    result.MessageToUser = "Login não fornecido";
            }
            else
                result.MessageToUser = "Tipo de Acesso inválido";
            return result;
        }

        private static AccessControl GetSession()
        {
            AccessControl r = null;
            try
            {
                r = (AccessControl)HttpContext.Current.Session[SESSION_NAME];
            }
            catch { }
            return r;
        }

        private static bool HasSession()
        {
            if (current == null)
            {
                current = GetSession();
            }
            return (!(current == null));
        }

    }
}