using Avalonia.Utilities;
using OOD_24L_01180689.src.writers;
using OOD_24L_01180689.src.serverSimulator;
using IDataSource = OOD_24L_01180689.src.readers.IDataSource;
using OOD_24L_01180689.src.factories.readers;
using OOD_24L_01180689.src.factories.writersFactories;
using OOD_24L_01180689.src.threads;
using OOD_24L_01180689.src.console;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.reports.reporters.reporters;
using OOD_24L_01180689.src.logging;
using OOD_24L_01180689.src.reports;
using OOD_24L_01180689.src.updating;

class Program
{
    static void Main(string[] args)
    {
        string dir = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
        string input = Path.Combine(dir, "data", "input_example.ftr");
        string update = Path.Combine(dir, "data", "update_example.ftre");
        string outputDir = "data";
        int minDelay = 100;
        int maxDelay = 200;
        ILogger logger = new FileLogger();

        Console.Clear();

        var flightTrackerUpdater = FlightTrackerUpdater.GetInstance();
        flightTrackerUpdater.Start();

        NewsGenerator.InitializeProviders();
        NewsGenerator newsGenerator = new NewsGenerator(DataStorage.GetInstance.GetNewsProviders(),
            DataStorage.GetInstance.GetReporters());

        IFileReaderFactory fileReaderFactory = new FTRReaderFactory();
        IDataSource reader = fileReaderFactory.Create();
        reader.ReadData(input);


        EventUpdateManager eventHandler = EventUpdateManager.GetInstance(update, minDelay, maxDelay, logger);
        eventHandler.Start();

        IFileWriterFactory fileWriterFactory = new JSONWriterFactory();
        IWriter jsonWriter = fileWriterFactory.Create();

        var consoleHandler = new ConsoleHandler(jsonWriter, dir, outputDir, newsGenerator);
        consoleHandler.HandleConsoleInput();

        flightTrackerUpdater.Stop();
        eventHandler.Stop();
    }
}