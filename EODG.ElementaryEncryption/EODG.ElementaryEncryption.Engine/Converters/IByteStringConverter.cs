namespace EODG.ElementaryEncryption.Engine.Converters
{
    public interface IByteStringConverter
    {
        string GetString(byte[] ba);
        byte[] GetByteArray(string s);
    }
}
