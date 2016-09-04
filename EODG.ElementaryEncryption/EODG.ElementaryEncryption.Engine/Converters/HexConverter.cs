using System;
using System.Text;

namespace EODG.ElementaryEncryption.Engine.Converters
{
    /// <summary>
    /// Simple class to convert hex strings to byte arrays and vice-versa
    /// Based on: http://stackoverflow.com/a/311179/2285245
    /// </summary>
    public class HexConverter : IByteStringConverter
    {
        /// <summary>
        /// Converts a string of hexadecimal values to its byte array equivalent
        /// </summary>
        /// <param name="s">String of hexadecimal characters</param>
        /// <returns></returns>
        public byte[] Convert(string s)
        {
            if (!Util.IsValidHexString(s))
            {
                throw new ArgumentException(string.Format("String supplied contains invalid hexadecimal values.\nString supplied: \"{0}\"", s), "hexString");
            }

            var numberChars = s.Length;
            var bytes = new byte[numberChars / 2];

            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = System.Convert.ToByte(s.Substring(i, 2), 16);
            }

            return bytes;
        }

        /// <summary>
        /// Converts a byte array to its hexadecimal string equivalent
        /// </summary>
        /// <param name="ba"></param>
        /// <returns></returns>
        public string Convert(byte[] ba)
        {
            var hex = new StringBuilder(ba.Length * 2);

            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }
    }
}
