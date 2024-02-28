using OOD_24L_01180689.src.readers;

namespace OOD_24L_01180689.src.factories
{
    public interface IFileReaderFactory
    {
        IDataSource Create();
    }

    public abstract class FileReaderFactory : IFileReaderFactory
    {
        public virtual IDataSource Create()
        {
            throw new NotImplementedException();
        }
    }
}