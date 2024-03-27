using FlightTrackerGUI;
using OOD_24L_01180689.src.writers;
using OOD_24L_01180689.src.serverSimulator;
using OOD_24L_01180689.src.dataStorage;
using IDataSource = OOD_24L_01180689.src.readers.IDataSource;
using OOD_24L_01180689.src.factories.readers;
using OOD_24L_01180689.src.factories.writersFactories;
using OOD_24L_01180689.src.visualization;
using OOD_24L_01180689.src.threads;
using OOD_24L_01180689.src.console;

class Program
{
    static void Main(string[] args)
    {
        string dir = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
        string input = Path.Combine(dir, "data", "example1.ftr");
        string outputDir = "data";
        int minDelay = 0;
        int maxDelay = 0;

        var objectCountDisplay = ObjectCountDisplay.GetInstance;
        objectCountDisplay.Start();

        var ss = ServerSimulator.GetInstance(input, minDelay, maxDelay);
        ss.Start();

        var flightTrackerUpdater = FlightTrackerUpdater.GetInstance();
        flightTrackerUpdater.Start();


        IFileReaderFactory fileReaderFactory = new ServerReaderFactory(ss);
        IDataSource serverReader = fileReaderFactory.Create();


        IFileWriterFactory fileWriterFactory = new JSONWriterFactory();
        IWriter jsonWriter = fileWriterFactory.Create();

        var consoleHandler = new ConsoleHandler(jsonWriter, dir, outputDir);
        consoleHandler.HandleConsoleInput();

        ss.Stop();
        objectCountDisplay.Stop();
        flightTrackerUpdater.Stop();
    }
}