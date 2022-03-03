using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThunderFire
{
    /// <summary>
    /// Utilitário de Localização de IP
    /// </summary>
    public class PingUtility
    {
        /// <summary>
        /// Retorna a String do Ultimo Erro Ocorrido
        /// </summary>
        public static string LastError = "";
        /// <summary>
        /// Objeto do Ultimo Erro Ocorrido
        /// </summary>
        public static object LastObjectError = null;
        /// <summary>
        /// Mensagem de Resposta
        /// </summary>
        public static string Message = "";
        /// <summary>
        /// Obtêm o endereço IP de uma localização
        /// </summary>
        /// <param name="args">Host ou IP</param>
        public static void Ping(string[] args)
        {
            if (args.Length == 0)
                throw new ArgumentException("Ping needs a host or IP Address.");
            Message = "";
            string who = args[0];
            AutoResetEvent waiter = new AutoResetEvent(false);

            Ping pingSender = new Ping();

            pingSender.PingCompleted += new PingCompletedEventHandler(PingCompletedCallback);

            // Create a buffer of 32 bytes of data to be transmitted. 
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);

            // Wait 12 seconds for a reply. 
            int timeout = 12000;

            // Set options for transmission: 
            // The data can go through 64 gateways or routers 
            // before it is destroyed, and the data packet 
            // cannot be fragmented.
            PingOptions options = new PingOptions(64, true);

            Console.WriteLine("Time to live: {0}", options.Ttl);
            Console.WriteLine("Don't fragment: {0}", options.DontFragment);

            // Send the ping asynchronously. 
            // Use the waiter as the user token. 
            // When the callback completes, it can wake up this thread.
            pingSender.SendAsync(who, timeout, buffer, options, waiter);

            // Prevent this example application from ending. 
            // A real application should do something useful 
            // when possible.
            //waiter.WaitOne ();
            Message+="Ping completado.";

        }

        private static void PingCompletedCallback(object sender, PingCompletedEventArgs e)
        {
            // If the operation was canceled, display a message to the user. 
            if (e.Cancelled)
            {
                Message += "Ping cancelado" + Environment.NewLine;

                // Let the main thread resume.  
                // UserToken is the AutoResetEvent object that the main thread  
                // is waiting for.
                ((AutoResetEvent)e.UserState).Set();
            }

            // If an error occurred, display the exception to the user. 
            if (e.Error != null)
            {
                Message += "Ping Fallho" + Environment.NewLine;
                Message += e.Error.ToString() + Environment.NewLine;

                // Let the main thread resume. 
                ((AutoResetEvent)e.UserState).Set();
            }

            PingReply reply = e.Reply;

            DisplayReply(reply);

            // Let the main thread resume.
            ((AutoResetEvent)e.UserState).Set();
        }

        /// <summary>
        /// Exibe a resposta de Aplicação de um Ping
        /// </summary>
        /// <param name="reply">PingReply</param>
        public static void DisplayReply(PingReply reply)
        {
            if (reply == null)
                return;
            Message += string.Format("Ping Status {0}", reply.Status) + Environment.NewLine;
            Console.WriteLine("ping status: {0}", reply.Status);
            if (reply.Status == IPStatus.Success)
            {
                Message += string.Format("Address: {0}", reply.Address.ToString()) + Environment.NewLine;
                Message += string.Format("RoundTrip time: {0}", reply.RoundtripTime) + Environment.NewLine;
                Message += string.Format("Time to live: {0}", reply.Options.Ttl) + Environment.NewLine;
                Message += string.Format("Don't fragment: {0}", reply.Options.DontFragment) + Environment.NewLine;
                Message += string.Format("Buffer size: {0}", reply.Buffer.Length) + Environment.NewLine;
            }
        }
        /// <summary>
        /// Obtêm o nome da Máquina através do IP
        /// </summary>
        /// <param name="ipAdress">Endereço IP</param>
        /// <returns>string</returns>
        public static string GetMachineNameFromIPAddress(string ipAdress)
        {
            string machineName = string.Empty;
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(ipAdress);
                machineName = hostEntry.HostName;
            }
            catch (Exception Error)
            {
                LastError = Error.Message;
                LastObjectError = Error;
            }
            return machineName;
        }
    }

}
