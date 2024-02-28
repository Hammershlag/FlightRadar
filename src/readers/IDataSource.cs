namespace OOD_24L_01180689.src.readers
{
    public interface IDataSource
    {
        IEnumerable<object> ReadData(string dir, string filename);
    }
}