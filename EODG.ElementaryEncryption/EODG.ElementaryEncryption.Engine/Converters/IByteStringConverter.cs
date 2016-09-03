namespace EODG.ElementaryEncryption.Engine.Converters
{
    public interface IByteStringConverter
    {
        string ConvertHex(byte[] ba);
        byte[] ConvertHex(string s);
    }
}
