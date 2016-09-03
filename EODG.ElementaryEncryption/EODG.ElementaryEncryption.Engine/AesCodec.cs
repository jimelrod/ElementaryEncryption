using EODG.ElementaryEncryption.Engine.Converters;
using System.IO;
using System.Security.Cryptography;

namespace EODG.ElementaryEncryption.Engine
{
    public class AesCodec
    {
        #region FIELDS

        private IByteStringConverter _converter;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes instance of the AesCodec class
        /// </summary>
        /// <param name="converter">Instance of a class implementing the IByteStringConverter interface that can convert a string to a byte array and vice versa</param>
        public AesCodec(IByteStringConverter converter)
        {
            _converter = converter;
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Encrypts supplied text with randomly generated encryption key and initialization vector
        /// </summary>
        /// <param name="plainText">Text to be encrypted</param>
        /// <returns>EncryptionData instance containing cipher, key, and initialization vector</returns>
        public AesEncryptionData Encrypt(string plainText)
        {
            using (var aesAlg = new AesManaged())
            {
                return Encrypt(plainText, aesAlg);
            }
        }

        /// <summary>
        /// Encrypts supplied text with supplied encryption key and initialization vector
        /// </summary>
        /// <param name="plainText">Text to be encrypted</param>
        /// <param name="key">Encryption Key</param>
        /// <param name="initializationVector">Initialization Vector</param>
        /// <returns>EncryptionData instance containing cipher, key, and initialization vector</returns>
        public AesEncryptionData Encrypt(string plainText, string key, string initializationVector)
        {
            using (var aesAlg = new AesManaged())
            {
                aesAlg.Key = _converter.ConvertHex(key);
                aesAlg.IV = _converter.ConvertHex(initializationVector);

                return Encrypt(plainText, aesAlg);
            }
        }

        /// <summary>
        /// Decrypt the supplied EncryptionData instance
        /// </summary>
        /// <param name="encryptionData">EncryptionData instance containing cipher, key, and initialization vector</param>
        /// <returns>Plain text of decrypted cipher</returns>
        public string Decrypt(AesEncryptionData encryptionData)
        {
            return Decrypt(encryptionData.Cipher, encryptionData.Key, encryptionData.InitializationVector);
        }

        /// <summary>
        /// Decrypt the supplies cipher based on supplied encryption key and initialization vector
        /// </summary>
        /// <param name="cipher">Cipher to decrypt</param>
        /// <param name="key">Encryption Key</param>
        /// <param name="initializationVector">Initialization Vector</param>
        /// <returns>Plain text of decrypted cipher</returns>
        public string Decrypt(string cipher, string key, string initializationVector)
        {
            string plainText = null;

            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = _converter.ConvertHex(key);
                aesAlg.IV = _converter.ConvertHex(initializationVector);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(_converter.ConvertHex(cipher)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plainText = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plainText;
        }

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Encrypts supplied text utilizing instance of the AesManaged class
        /// </summary>
        /// <param name="plainText">Text to be encrypted<</param>
        /// <param name="aesAlg">AesManaged instance</param>
        /// <returns>EncryptionData instance containing cipher, key, and initialization vector</returns>
        private AesEncryptionData Encrypt(string plainText, AesManaged aesAlg)
        {
            AesEncryptionData encryptionData;
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }

                    string cipher = _converter.ConvertHex(msEncrypt.ToArray());
                    string initializationVector = _converter.ConvertHex(aesAlg.IV);
                    string key = _converter.ConvertHex(aesAlg.Key);

                    encryptionData = new AesEncryptionData(cipher, initializationVector, key);
                }
            }

            return encryptionData;
        }

        #endregion
    }
}
