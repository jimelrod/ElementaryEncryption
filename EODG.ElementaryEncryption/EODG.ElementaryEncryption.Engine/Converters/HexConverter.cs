using System;
using System.Text;

namespace EODG.ElementaryEncryption.Engine.Converters
{
    public class HexConverter : IByteStringConverter
    {
        public byte[] GetByteArray(string s)
        {
            var numberChars = s.Length;
            var bytes = new byte[numberChars / 2];

            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(s.Substring(i, 2), 16);
            }

            return bytes;
        }

        public string GetString(byte[] ba)
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
