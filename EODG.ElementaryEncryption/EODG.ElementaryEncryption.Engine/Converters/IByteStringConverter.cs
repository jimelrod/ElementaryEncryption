namespace EODG.ElementaryEncryption.Engine.Converters
{
    public interface IByteStringConverter
    {
        string Convert(byte[] ba);
        byte[] Convert(string s);
    }
}
