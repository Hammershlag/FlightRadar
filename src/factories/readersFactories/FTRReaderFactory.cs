using OOD_24L_01180689.src.readers;

namespace OOD_24L_01180689.src.factories.readers
{
    public class FTRReaderFactory : IFileReaderFactory
    {
        public IDataSource Create()
        {
            return new FTRReader();
        }
    }
}