namespace EODG.ElementaryEncryption.Engine
{
    public class AesEncryptionData
    {
        public AesEncryptionData(string cipher, string initializationVector, string key)
        {
            Cipher = cipher;
            InitializationVector = initializationVector;
            Key = key;
        }

        public string Cipher { get; }
        public string InitializationVector { get; }
        public string Key { get; }
    }
}
