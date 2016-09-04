using EODG.ElementaryEncryption.Engine.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace EODG.ElementaryEncryption.Test.Engine
{
    [TestClass]
    public class HexConverterTests
    {
        private HexConverter _converter;
        private static readonly string _expectedHexString = "1234567890abcdef";
        private static readonly byte[] _expectedHexByteArray = new byte[] { 18, 52, 86, 120, 144, 171, 205, 239 };
        private static readonly string _invalidHexString = "zzz";

        [TestInitialize]
        public void Initialize()
        {
            _converter = new HexConverter();
        }

        [TestMethod]
        public void ConvertStringToByteArrayGivesProperResult()
        {
            var hexByteAry = _converter.Convert(_expectedHexString);
            Assert.IsTrue(hexByteAry.SequenceEqual(_expectedHexByteArray));
        }

        [TestMethod]
        public void ConvertStringToByteArrayThrowsErrorOnInvalidString()
        {
            bool isSuccess = true;

            try
            {
                _converter.Convert(_invalidHexString);
            }
            catch
            {
                isSuccess = false;
            }

            Assert.IsFalse(isSuccess);
        }

        [TestMethod]
        public void ConvertByteArrayToStringGivesProperResult()
        {
            var hexString = _converter.Convert(_expectedHexByteArray);            
            Assert.IsTrue(hexString == _expectedHexString);
        }
    }
}
