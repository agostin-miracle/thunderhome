using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderFire
{
    /// <summary>
    /// 
    /// </summary>
    public class Hex
    {
        private byte[] _data;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public Hex(string data)
        {
            if ((data.Length & 1) != 0) throw new ArgumentException("A sequência hexadecimal deve ter um número par de dígitos.");

            _data = Enumerable.Range(0, data.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(data.Substring(x, 2), 16))
                .ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public Hex(byte[] data)
        {
            _data = data;
        }

        /// <summary>
        /// Retorna uma string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            string hex = BitConverter.ToString(_data);
            return hex.Replace("-", "");
        }

        /// <summary>
        /// Implementa o operador XOR para duas strings
        /// </summary>
        /// <param name="LHS">HEX Object</param>
        /// <param name="RHS">HEX Object</param>
        /// <returns>Hex</returns>
        static public Hex operator ^(Hex LHS, Hex RHS)
        {
            return new Hex
                (
                    LHS._data.Zip
                        (
                            RHS._data,
                            (a, b) => (byte)(a ^ b)
                        )
                    .ToArray()
                );
        }
    }
}