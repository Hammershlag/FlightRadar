using OOD_24L_01180689.src.writers;

namespace OOD_24L_01180689.src.factories.writersFactories;

public class JSONWriterFactory : IFileWriterFactory
{
    public IWriter Create()
    {
        return new JSONWriter();
    }
}