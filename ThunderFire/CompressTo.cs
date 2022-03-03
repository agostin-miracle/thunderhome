using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire
{
    /// <summary>
    /// Utilitário de Compressão de Arquivos
    /// </summary>
    public class CompressTo : MazeFireBase
    {
         
        /// <summary>
        /// method for compressing a single file into a zip file
        /// </summary>
        /// <param name="file">the file we're compressing</param>
        /// <param name="outputFile">the output zip file</param>
        /// <returns></returns>
        /// public bool
        public bool CompressFile(string file, string outputFile)
        {
            TrappedError.SetError();
            bool RETURN_VALUE = false;
            try
            {
                using (var inFile = File.OpenRead(file))
                {

                    using (var outFile = File.Create(outputFile))
                    {

                        using (var compress = new GZipStream(outFile, CompressionMode.Compress, false))
                        {

                            byte[] buffer = new byte[inFile.Length];


                            int read = inFile.Read(buffer, 0, buffer.Length);

                            while (read > 0)
                            {
                                compress.Write(buffer, 0, read);
                                read = inFile.Read(buffer, 0, buffer.Length);
                            }
                        }
                    }
                }
                RETURN_VALUE = true;
            }
            catch (IOException Error)
            {
                string ErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                if (!String.IsNullOrWhiteSpace(ErrorCode))
                    TrappedError.SetError(ErrorCode);
                TrappedError.ErrorMessage = Error.Message;
                TrappedError.ErrorObject = Error;

            }
            return RETURN_VALUE;
        }

        /// <summary>
        /// method for decompressing a zip file
        /// </summary>
        /// <param name="source">the zip file we're decompressing</param>
        /// <param name="destFile">the destination</param>
        /// <returns></returns>
        public bool Decompress(string source, string destFile)
        {
            TrappedError.SetError();
            bool RETURN_VALUE = false;
            try
            {

                using (var inStream = new FileStream(source, FileMode.Open, FileAccess.Read, FileShare.Read))
                {

                    using (var outStream = new FileStream(destFile, FileMode.Create, FileAccess.Write, FileShare.None))
                    {

                        using (var zipStream = new GZipStream(inStream, CompressionMode.Decompress, true))
                        {

                            byte[] buffer = new byte[inStream.Length];

                            while (true)
                            {
                                int count = zipStream.Read(buffer, 0, buffer.Length);

                                if (count != 0)
                                    outStream.Write(buffer, 0, count);

                                if (count != buffer.Length)

                                    break;
                            }
                        }
                    }
                }
                RETURN_VALUE=true;
            }
            catch (Exception Error)
            {
                string ErrorCode =ThunderFire.ErrorManager.GetErrorCode(Error);
                if (!String.IsNullOrWhiteSpace(ErrorCode))
                    TrappedError.SetError(ErrorCode);
                TrappedError.ErrorMessage = Error.Message;
                TrappedError.ErrorObject = Error;
            }
            return RETURN_VALUE;
        }
    }
}