using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace ThunderFire
{
    /// <summary>
    /// Diretórios
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(true)]
    public class Directories
    {
        /// <summary>
        /// Captura de Erro
        /// </summary>
        public static TrapError TrappedError = new TrapError();

        /// <summary>
        /// Logon de Usuario
        /// </summary>
        /// <param name="lpszUsername">Usuario</param>
        /// <param name="lpszDomain">Dominio</param>
        /// <param name="lpszPassword">Senha</param>
        /// <param name="dwLogonType">LogonType</param>
        /// <param name="dwLogonProvider">LogonProvider</param>
        /// <param name="phToken"></param>
        /// <returns></returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, out IntPtr phToken);

        /// <summary>
        /// Retorna true se ocorreu algum erro
        /// </summary>
        /// <returns>bool</returns>
        public static bool HasError()
        {
            if (String.IsNullOrEmpty(TrappedError.ErrorMessage))
                return false;
            return true;
        }

        /// <summary>
        /// Combina uma pasta de arquivos a um nome de arquivo
        /// </summary>
        /// <param name="dir">Nome do Diretorio ou pasta</param>
        /// <param name="file">Nome do Arquivo/Diretório</param>
        /// <returns>Fullpath</returns>
        public static string CombinePath(string dir, string file)
        {
            string r = "";
            if (dir.EndsWith("\\"))
                r = dir + file;
            else
                r = dir + "\\" + file;
            return r;
        }

        /// <summary>
        /// Desmembra uma string no formato de um full path e organiza de acordo com os 'niveis' de diretorio
        /// </summary>
        /// <param name="path">Full Path</param>
        /// <param name="level">Número de Diretórios a considerar na rota</param>
        /// <returns>string</returns>
        public static string GetRootPath(string path, int level)
        {
            string r = path;
            if (path != "")
            {
                var t = path.Split('\\');
                string s = "";
                for (int i = 0; i <= level; i++)
                {
                    if (i <= level)
                    {
                        s += t[i] + "\\";
                    }
                }
                r = s;
            }
            return r;
        }

        /// <summary>
        /// Remove o caracter de single backslash (\) para double backslash (\\)
        /// </summary>
        /// <param name="_s">string a ser analisada</param>
        /// <returns>string alterada</returns>
        public static string ChangeBackSlash(string _s)
        {
            string _fc = "";
            for (int i = 0; i < _s.Length; i++)
            {
                if (_s.Substring(i, 1).CompareTo(@"\") == 0)
                    _fc += @"\\";
                else
                    _fc += _s.Substring(i, 1);
            }
            return _fc;
        }

        /// <summary>
        /// Cria um diretório com base no usuário do domínio
        /// </summary>
        /// <param name="User">Usuário</param>
        /// <param name="Domain">Domínio</param>
        /// <param name="Password">Senha</param>
        /// <param name="diretorio">Diretório</param>
        /// <returns>true, se concluiu a operação</returns>
        /// <remarks>
        /// <para>Os parametros de usuario, senha e dominio tem de estar encriptados pela funcção encryption</para>
        /// </remarks>
        public static bool LoggedCreateDir(string User, string Domain, string Password, string diretorio)
        {
            return ActionOnDir("CREATE", User, Domain, Password, diretorio);
        }
        /// <summary>
        /// Deleta um diretório com base no usuário do domínio
        /// </summary>
        /// <param name="User">Usuário</param>
        /// <param name="Domain">Domínio</param>
        /// <param name="Password">Senha</param>
        /// <param name="diretorio">Diretório</param>
        /// <returns>true, se concluiu a operação</returns>
        /// <remarks>
        /// <para>Os parametros de usuario, senha e dominio tem de estar encriptados pela funcção encryption</para>
        /// </remarks>

        public static bool LoggedDeleteDir(string User, string Domain, string Password, string diretorio)
        {
            return ActionOnDir("DELETE", User, Domain, Password, diretorio);
        }


        private static bool ActionOnDir(string command, string User, string Domain, string Password, string diretorio)
        {
            bool _retorno = false;
            IntPtr admin_token;
            WindowsIdentity wid_current = WindowsIdentity.GetCurrent();
            WindowsIdentity wid_admin = null;
            WindowsImpersonationContext wic = null;
            TrappedError.SetError();
            try
            {
                if (LogonUser(UseRijndael.Decrypt(User).ToString(), UseRijndael.Decrypt(Domain).ToString(), UseRijndael.Decrypt(Password).ToString(), 9, 0, out admin_token))
                {
                    wid_admin = new WindowsIdentity(admin_token);
                    wic = wid_admin.Impersonate();
                    try
                    {
                        if (command == "DELETE")
                            System.IO.Directory.Delete(diretorio);
                        if (command == "CREATE")
                            System.IO.Directory.Delete(diretorio);

                        TrappedError.AddMessage(String.Format("{0} Directory {1}", command, diretorio));
                        _retorno = true;
                    }
                    catch (Exception Error)
                    {
                        TrappedError.AddMessage(String.Format("{0} Direcory {1} Erro: {2}", command, diretorio, Error.Message));
                        TrappedError.SetError(Error);
                        TrappedError.CurrentMethod = MethodBase.GetCurrentMethod().Name;
                        if (wic != null)
                            wic.Undo();
                    }
                }
            }
            catch (Exception Error)
            {
                TrappedError.AddMessage(String.Format("{0} Directory {1} Erro: {2}", command, diretorio, Error.Message));
                TrappedError.SetError(Error);
                if (wic != null)
                    wic.Undo();
            }
            finally
            {
                if (wic != null)
                    wic.Undo();
            }
            return _retorno;
        }

        /// <summary>
        /// Cria um diretório
        /// </summary>
        /// <param name="dirpath">Nome do Diretório</param>
        /// <returns>true, se o diretório existe</returns>
        public static bool CreateDirectory(string dirpath)
        {
            try
            {
                if (!System.IO.Directory.Exists(dirpath))
                {
                    System.IO.Directory.CreateDirectory(dirpath);
                }
            }
            catch (Exception Error)
            {
                TrappedError.ErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                if (TrappedError.ErrorCode != "")
                    TrappedError.SetError(TrappedError.ErrorCode);
                TrappedError.ErrorMessage = Error.Message;
                TrappedError.ErrorObject = Error;
                return false;
            }
            return System.IO.Directory.Exists(dirpath);
        }

    }
}