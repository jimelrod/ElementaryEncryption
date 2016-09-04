using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EODG.ElementaryEncryption.Engine;
using EODG.ElementaryEncryption.Engine.Converters;

namespace EODG.ElementaryEncryption.Test.Engine
{
    [TestClass]
    public class AesCodecEncryptTests
    {
        private AesCodec _codec;

        [TestInitialize]
        public void Initialize()
        {
            _codec = new AesCodec(new HexConverter());
        }

        [TestMethod]
        public void EncryptReturnsValidObjectWithPlainTextProvided()
        {
            var aesEncryptionData = _codec.Encrypt(StaticValues.ExpectedPlainText);

            Assert.IsTrue(_codec.Decrypt(aesEncryptionData) == StaticValues.ExpectedPlainText);
        }

        [TestMethod]
        public void EncryptReturnsValidObjectWithPlainTextAndIvProvided()
        {
            var aesEncryptionData = _codec.Encrypt(StaticValues.ExpectedPlainText, StaticValues.ExpectedKey);

            Assert.IsTrue(_codec.Decrypt(aesEncryptionData) == StaticValues.ExpectedPlainText);
        }

        [TestMethod]
        public void EncryptReturnsValidObjectWithPlainTextIvAndKeyProvided()
        {
            var aesEncryptionData = _codec.Encrypt(StaticValues.ExpectedPlainText, StaticValues.ExpectedKey, StaticValues.ExpectedInitializationVector);

            Assert.IsTrue(_codec.Decrypt(aesEncryptionData) == StaticValues.ExpectedPlainText);
        }
    }
}
