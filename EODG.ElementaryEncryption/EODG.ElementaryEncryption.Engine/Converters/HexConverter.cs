using System;
using System.Text;
using System.Text.RegularExpressions;

namespace EODG.ElementaryEncryption.Engine.Converters
{
    /// <summary>
    /// Simple class to convert hex strings to byte arrays and vice-versa
    /// Based on: http://stackoverflow.com/a/311179/2285245
    /// </summary>
    public class HexConverter : IByteStringConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hexString">String of hexadecimal characters</param>
        /// <returns></returns>
        public byte[] ConvertHex(string hexString)
        {
            if (!IsValidHexString(hexString))
            {
                throw new ArgumentException(string.Format("String supplied contains invalid hexadecimal values.\nString supplied: \"{0}\"", hexString), "hexString");
            }

            var numberChars = hexString.Length;
            var bytes = new byte[numberChars / 2];

            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }

            return bytes;
        }

        public string ConvertHex(byte[] ba)
        {
            var hex = new StringBuilder(ba.Length * 2);

            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }

        private bool IsValidHexString(string s)
        {
            var hexRgx = new Regex(@"^[0-9a-f]*$", RegexOptions.IgnoreCase);
            return hexRgx.IsMatch(s) && s.Length % 2 == 0;
        }
    }
}
