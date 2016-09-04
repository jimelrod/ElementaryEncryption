using EODG.ElementaryEncryption.Engine.Converters;
using EODG.ElementaryEncryption.Engine.Exceptions;
using System;
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
            return Encrypt(plainText, GenerateKey());            
        }

        /// <summary>
        /// Encrypts supplied text with supplied encryption key and randomly generated initialization vector
        /// </summary>
        /// <param name="plainText">Text to be encrypted</param>
        /// <param name="key">AES256 encryption Key</param>
        /// <returns>EncryptionData instance containing cipher, key, and initialization vector</returns>
        public AesEncryptionData Encrypt(string plainText, string key)
        {
            return Encrypt(plainText, GenerateKey(), GenerateInitializationVector());
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
            var aesAlg = new AesManaged();

            try
            {
                TryAssignKeyAndIv(key, initializationVector, ref aesAlg);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }

                        string cipher = _converter.Convert(msEncrypt.ToArray());

                        return new AesEncryptionData(cipher, initializationVector, key);
                    }
                }
            }
            finally
            {
                aesAlg.Dispose();
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
        /// Decrypt the supplied cipher based on supplied encryption key and initialization vector
        /// </summary>
        /// <param name="cipher">Cipher to decrypt</param>
        /// <param name="key">Encryption Key</param>
        /// <param name="initializationVector">Initialization Vector</param>
        /// <returns>Plain text of decrypted cipher</returns>
        public string Decrypt(string cipher, string key, string initializationVector)
        {
            if (!Util.IsValidHexString(cipher))
            {
                throw new InvalidCipherException(string.Format("Invalid cipher supplied: \"{0}\"", cipher));
            }

            var aesAlg = new AesManaged();
            string plainText = null;

            try
            {
                TryAssignKeyAndIv(key, initializationVector, ref aesAlg);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(_converter.Convert(cipher)))
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
            finally
            {
                aesAlg.Dispose();
            }

            return plainText;
        }

        /// <summary>
        /// Generate an AES encryption key
        /// </summary>
        /// <returns>Plain text encryption key</returns>
        public string GenerateKey()
        {
            using (AesManaged aesAlg = new AesManaged())
            {
                return _converter.Convert(aesAlg.Key);
            }
        }

        /// <summary>
        /// Generates an initialization vector
        /// </summary>
        /// <returns>Plain text initialization vector</returns>
        public string GenerateInitializationVector()
        {
            using (AesManaged aesAlg = new AesManaged())
            {
                return _converter.Convert(aesAlg.IV);
            }
        }

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Attempts to assign key/iv to AesManaged instance supplied, and thorws exceptions if unable
        /// </summary>
        /// <param name="key">Encryption key</param>
        /// <param name="initializationVector">Initialization vector</param>
        /// <param name="aesAlg">AesManaged instance</param>
        private void TryAssignKeyAndIv(string key, string initializationVector, ref AesManaged aesAlg)
        {
            try
            {
                aesAlg.Key = _converter.Convert(key);
            }
            catch (Exception ex)
            {
                throw new InvalidAesEncryptionKeyException(string.Format("Invalid key supplied: \"{0}\"", aesAlg.Key), ex);
            }

            try
            {
                aesAlg.IV = _converter.Convert(initializationVector);
            }
            catch (Exception ex)
            {
                throw new InvalidAesEncryptionKeyException(string.Format("Invalid initialization vector supplied: \"{0}\"", aesAlg.Key), ex);
            }
        }

        #endregion
    }
}
