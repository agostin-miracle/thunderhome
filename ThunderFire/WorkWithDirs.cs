using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire
{
    /// <summary>
    /// Trabalhando com diretórios
    /// </summary>
    public class WorkWithDirs
    {
        /// <summary>
        /// Histórico de Execução
        /// </summary>
        public static StringBuilder Logging;

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
        /// Retorna a string do ultimo erro ocorrido
        /// </summary>
        public static string ErrorMessage{get; internal set;}
        /// <summary>
        /// Retorna a string do último erro ocorrido
        /// </summary>
        public static string ErrorMessageForUser{get; internal set;}
        /// <summary>
        /// Retorna true se ocorreu algum erro
        /// </summary>
        /// <returns>bool</returns>
        public static bool HasError()
        {
            if (String.IsNullOrEmpty(ErrorMessage))
                return false;
            return true;
        }

        /// <summary>
        /// Objeto do Erro
        /// </summary>
        public static object ErrorObject { get; internal set; }
        private static void SetError()
        {
            ErrorMessageForUser = "";
            ErrorMessage = "";
            ErrorObject = null;
            Logging = new StringBuilder();

        }
        /// <summary>
        /// Combina uma pasta de arquivos a um nome de arquivo
        /// </summary>
        /// <param name="dir">Nome do Diretorio ou pasta</param>
        /// <param name="file">Nome do Arquivo/Diretório</param>
        /// <returns></returns>
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
        /// Verifica se o caminho passado se refere a um protocolo HTTP
        /// </summary>
        /// <param name="address">string do endereço ou caminho</param>
        /// <returns>bool, se o caminho fornecido se refere a um protocolo HTTP</returns>
        public static bool isHTTP(string address)
        {
            return address.ToLower().Substring(0, 7).CompareTo("http://") == 0;
        }
        /// <summary>
        /// Verifica se o caminho passado se refere a um padrao UNC
        /// </summary>
        /// <param name="address">string do endereço ou caminho</param>
        /// <returns>true, se o endereço corresponde ao padrao UNC</returns>
        /// <remarks>
        /// UNC é uma convenção de nomenclatura usada principalmente para especificar e mapear unidades de rede em Microsoft Windows. Nomes UNC são mais comumente utilizados para atingir os servidores de arquivos ou impressoras em uma rede local.
        /// </remarks>
        public static bool isUNC(string address)
        {
            return address.Substring(0, 2).CompareTo("\\\\") == 0;
        }


        /// <summary>
        /// Adiciona um backslash ao final da string
        /// </summary>
        /// <param name="path">path String</param>
        /// <returns>String</returns>
        public static string AddBackSlashEnd(string path)
        {
            if (path != "")
            {
                if (isHTTP(path))
                {
                    if (!path.EndsWith("/"))
                        return path + "/";
                }
                else
                {
                    if (!path.EndsWith(@"\"))
                        return path + @"\";
                }
            }
            return path;
        }
        /// <summary>
        /// Retorna o Back Slash correspondente ao tipo de caminho
        /// </summary>
        /// <param name="part1">Path String</param>
        /// <returns>String</returns>
        public static string GetBackSlash(string part1)
        {
            if (isHTTP(part1))
            {
                if (!part1.EndsWith("/"))

                    return "/";
            }
            else
            {
                if (!part1.EndsWith(@"\"))
                    return @"\";
            }
            return "";
        }

        /// <summary>
        /// Remove o ultimo caracter '\' do argumento de caminho fornecido
        /// </summary>
        /// <param name="path">String relativa ao path (caminho) a ser analisado</param>
        /// <returns>String</returns>
        public static string RemoveBackSlashEnd(string path)
        {
            if (path.EndsWith(@"\"))
                return path.Substring(0, path.Length - 1);
            return path;
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
        /// <remarks>
        /// <para>Os parametros de usuario, senha e dominio tem de estar encriptados pela funcção encryption</para>
        /// </remarks>
        /// <returns>true, se concluiu a operação</returns>
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
            SetError();
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

                        Logging.Append(String.Format("{0} Directory {1}", command, diretorio));
                        _retorno = true;
                    }
                    catch (Exception Error)
                    {
                        Logging.Append(String.Format("{0} Direcory {1} Erro: {2}", command, diretorio, Error.Message));
                        ErrorMessage = Error.Message;
                        ErrorObject = Error;
                        if (wic != null)
                            wic.Undo();
                    }
                }
            }
            catch (Exception Error)
            {
                Logging.Append(String.Format("{0} Directory {1} Erro: {2}", command, diretorio, Error.Message));
                ErrorMessage= Error.Message;
                ErrorObject = Error;
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
    }
} 