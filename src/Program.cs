using OOD_24L_01180689.src.console;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.factories.readers;
using OOD_24L_01180689.src.factories.writersFactories;
using OOD_24L_01180689.src.logging;
using OOD_24L_01180689.src.reports;
using OOD_24L_01180689.src.threads;
using OOD_24L_01180689.src.updating;

internal class Program
{
    private static void Main(string[] args)
    {
        var dir = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
        var input = Path.Combine(dir, "data", "input_example.ftr");
        var update = Path.Combine(dir, "data", "update_example.ftre");
        var outputDir = "data";
        var minDelay = 10;
        var maxDelay = 20;
        ILogger logger = new FileLogger();

        Console.Clear();

        var flightTrackerUpdater = FlightTrackerUpdater.GetInstance();
        flightTrackerUpdater.Start();

        NewsGenerator.InitializeProviders();
        var newsGenerator = new NewsGenerator(DataStorage.GetInstance.GetNewsProviders(),
            DataStorage.GetInstance.GetReporters());

        IFileReaderFactory fileReaderFactory = new FTRReaderFactory();
        var reader = fileReaderFactory.Create();
        reader.ReadData(input);


        var eventHandler = EventUpdateManager.GetInstance(update, minDelay, maxDelay, logger);
        eventHandler.Start();

        IFileWriterFactory fileWriterFactory = new JSONWriterFactory();
        var jsonWriter = fileWriterFactory.Create();

        var consoleHandler = new ConsoleHandler(jsonWriter, dir, outputDir, newsGenerator);
        consoleHandler.HandleConsoleInput();

        flightTrackerUpdater.Stop();
        eventHandler.Stop();
    }
}