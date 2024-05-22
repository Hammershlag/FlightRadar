using OOD_24L_01180689.src.console;
using OOD_24L_01180689.src.factories.readers;
using OOD_24L_01180689.src.factories.writersFactories;
using OOD_24L_01180689.src.serverSimulator;
using OOD_24L_01180689.src.threads;

internal class ProgramPart3
{
    private static void Main3(string[] args)
    {
        var dir = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
        var input = Path.Combine(dir, "data", "input_example.ftr");
        var outputDir = "data";
        var minDelay = 0;
        var maxDelay = 0;

        var objectCountDisplay = ObjectCountDisplay.GetInstance;
        objectCountDisplay.Start();

        var ss = ServerSimulator.GetInstance(input, minDelay, maxDelay);
        ss.Start();

        var flightTrackerUpdater = FlightTrackerUpdater.GetInstance();
        flightTrackerUpdater.Start();


        IFileReaderFactory fileReaderFactory = new ServerReaderFactory(ss);
        var serverReader = fileReaderFactory.Create();


        IFileWriterFactory fileWriterFactory = new JSONWriterFactory();
        var jsonWriter = fileWriterFactory.Create();

        var consoleHandler = new ConsoleHandler(jsonWriter, dir, outputDir);
        consoleHandler.HandleConsoleInput();

        ss.Stop();
        objectCountDisplay.Stop();
        flightTrackerUpdater.Stop();
    }
}