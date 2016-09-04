namespace EODG.ElementaryEncryption.Test.Engine
{
    internal static class StaticValues
    {
        #region CONSTANTS

        const string EXPECTED_PLAIN_TEXT = "This is only a test.";
        const string EXPECTED_CIPHER = "25c57bead0daf3e7a9bb665b3898182d7db5dabcc84a2d20c22644742a3075f4";
        const string EXPECTED_KEY = "ab2c271f218b55c0fe1bab8297e502e4fe90682f03fe5b844630b770d02945f5";
        const string EXPECTED_INITIALIZATION_VECTOR = "94193b186223ebbd3609fe1761dc4dab";

        const string INVALID_CIPHER = "";
        const string INVALID_KEY = "";
        const string INVALID_INITIALIZATION_VECTOR = "";

        #endregion

        #region PROPERTIES

        public static string ExpectedPlainText
        {
            get { return EXPECTED_PLAIN_TEXT; }
        }

        public static string ExpectedCipher
        {
            get { return EXPECTED_CIPHER; }
        }

        public static string ExpectedKey
        {
            get { return EXPECTED_KEY; }
        }

        public static string ExpectedInitializationVector
        {
            get { return EXPECTED_INITIALIZATION_VECTOR; }
        }

        public static string InvalidCipher
        {
            get { return INVALID_CIPHER; }
        }

        public static string InvalidKey
        {
            get { return INVALID_KEY; }
        }

        public static string InvalidInitializationVector
        {
            get { return INVALID_INITIALIZATION_VECTOR; }
        }

        #endregion
    }
}
