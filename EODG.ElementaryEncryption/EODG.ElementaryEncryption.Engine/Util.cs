using System.Text.RegularExpressions;

namespace EODG.ElementaryEncryption.Engine
{
    /// <summary>
    /// Utility class for helper functions
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Returns whether or not supplied string is a valid hexadecimal string
        /// </summary>
        /// <param name="hexString">String to be tested</param>
        /// <returns>True for valid hex strings, false otherwise</returns>
        public static bool IsValidHexString(string hexString)
        {
            var hexRgx = new Regex(@"^[0-9a-f]*$", RegexOptions.IgnoreCase);
            return hexRgx.IsMatch(hexString) && hexString.Length % 2 == 0;
        }
    }
}
