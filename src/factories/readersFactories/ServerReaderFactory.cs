using OOD_24L_01180689.src.readers;
using OOD_24L_01180689.src.serverSimulator;

namespace OOD_24L_01180689.src.factories.readers;

public class ServerReaderFactory : IFileReaderFactory
{
    private readonly ServerSimulator _serverSimulator;

    public ServerReaderFactory(ServerSimulator serverSimulator)
    {
        _serverSimulator = serverSimulator;
    }

    public IDataSource Create()
    {
        return new ServerReader(_serverSimulator);
    }
}