using OOD_24L_01180689.src.writers;

namespace OOD_24L_01180689.src.factories.writersFactories;

public interface IFileWriterFactory
{
    IWriter Create();
}

public class FileWriterFactory : IFileWriterFactory
{
    public virtual IWriter Create()
    {
        throw new NotImplementedException();
    }
}