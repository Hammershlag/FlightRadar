using OOD_24L_01180689.src.writers;
using OOD_24L_01180689.src.serverSimulator;
using IDataSource = OOD_24L_01180689.src.readers.IDataSource;
using OOD_24L_01180689.src.factories.readers;
using OOD_24L_01180689.src.factories.writersFactories;
using OOD_24L_01180689.src.threads;
using OOD_24L_01180689.src.console;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.reports;
using OOD_24L_01180689.src.reports.reporters;

class Program
{
    static void Main(string[] args)
    {
        string dir = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
        string input = Path.Combine(dir, "data", "example1.ftr");
        string outputDir = "data";
        int minDelay = 0;
        int maxDelay = 0;

        Console.Clear();

        var ss = ServerSimulator.GetInstance(input, minDelay, maxDelay);
        ss.Start();

        var flightTrackerUpdater = FlightTrackerUpdater.GetInstance();
        flightTrackerUpdater.Start();

        DataStorage.GetInstance.addNewsProvider(new Television("Abelian Television"));
        DataStorage.GetInstance.addNewsProvider(new Television("Channel TV-Tensor"));

        DataStorage.GetInstance.addNewsProvider(new Radio("Quantifier radio"));
        DataStorage.GetInstance.addNewsProvider(new Radio("Shmem radio"));

        DataStorage.GetInstance.addNewsProvider(new Newspaper("Categories Journal"));
        DataStorage.GetInstance.addNewsProvider(new Newspaper("Polytechnical Gazette"));

        NewsGenerator newsGenerator = new NewsGenerator(DataStorage.GetInstance.GetNewsProviders(),
            DataStorage.GetInstance.GetReporters());


        IFileReaderFactory fileReaderFactory = new ServerReaderFactory(ss);
        IDataSource serverReader = fileReaderFactory.Create();


        IFileWriterFactory fileWriterFactory = new JSONWriterFactory();
        IWriter jsonWriter = fileWriterFactory.Create();

        var consoleHandler = new ConsoleHandler(jsonWriter, dir, outputDir, newsGenerator);
        consoleHandler.HandleConsoleInput();

        ss.Stop();
        flightTrackerUpdater.Stop();
    }
}