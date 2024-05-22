using OOD_24L_01180689.src.console;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.factories.readers;
using OOD_24L_01180689.src.factories.writersFactories;
using OOD_24L_01180689.src.reports;
using OOD_24L_01180689.src.threads;

namespace OOD_24L_01180689.src.miscellaneous.mainArchive;

public class ProgramPart4
{
    private static void Main4(string[] args)
    {
        var dir = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
        var input = Path.Combine(dir, "data", "input_example.ftr");
        var outputDir = "data";

        Console.Clear();

        var flightTrackerUpdater = FlightTrackerUpdater.GetInstance();
        flightTrackerUpdater.Start();


        NewsGenerator.InitializeProviders();
        var newsGenerator = new NewsGenerator(DataStorage.GetInstance.GetNewsProviders(),
            DataStorage.GetInstance.GetReporters());


        IFileReaderFactory fileReaderFactory = new FTRReaderFactory();
        var reader = fileReaderFactory.Create();
        reader.ReadData(input);


        IFileWriterFactory fileWriterFactory = new JSONWriterFactory();
        var jsonWriter = fileWriterFactory.Create();

        var consoleHandler = new ConsoleHandler(jsonWriter, dir, outputDir, newsGenerator);
        consoleHandler.HandleConsoleInput();

        flightTrackerUpdater.Stop();
    }
}