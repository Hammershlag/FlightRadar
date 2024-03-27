using FlightTrackerGUI;
using OOD_24L_01180689.src.writers;
using OOD_24L_01180689.src.serverSimulator;
using OOD_24L_01180689.src.dataStorage;
using IDataSource = OOD_24L_01180689.src.readers.IDataSource;
using OOD_24L_01180689.src.factories.readers;
using OOD_24L_01180689.src.factories.writersFactories;
using OOD_24L_01180689.src.visualization;
using OOD_24L_01180689.src.threads;

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

        var flightTrackerUpdater = new FlightTrackerUpdater();
        flightTrackerUpdater.Start();


        IFileReaderFactory fileReaderFactory = new ServerReaderFactory(ss);
        IDataSource serverReader = fileReaderFactory.Create();



        IFileWriterFactory fileWriterFactory = new JSONWriterFactory();
        IWriter jsonWriter = fileWriterFactory.Create();



        HandleConsoleInput(jsonWriter, dir, outputDir);

        ss.Stop();
        objectCountDisplay.Stop();
        flightTrackerUpdater.Stop();
    }

    private static void HandleConsoleInput(IWriter jsonWriter, string dir, string outputDir)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Network source simulator started. ");
        Console.WriteLine("Type 'exit' to quit, 'print' to serialize data, 'help' to display commands  or 'change' to change flight visibility");
        string consoleInput = "";
        while (true)
        {
            consoleInput = Console.ReadLine();
            if (consoleInput.ToLower() == "exit")
            {
                break;
            }
            else if (consoleInput.ToLower() == "print")
            {
                var objectListCopy = DataStorage.Instance.GetObjectList();
                string outputFilename = DateTime.Now.ToString("'snapshot_'HH_mm_ss'.json'");
                jsonWriter.Write(objectListCopy, Path.Combine(dir, outputDir), outputFilename);
                Console.WriteLine($"Serialized data written to file: {outputFilename}");
            }
            else if (consoleInput.ToLower() == "help")
            {
                Console.WriteLine($"Type: \n'exit' to quit" +
                                  $"\n'print' to serialize data" +
                                  $"\n'help' to display commands" + 
                                  $"\n'change' to change flight visibility: 0 - all flight, 1 - all flights that are in progress, 2 - each plane should appear only once");
            }
            else if (consoleInput.ToLower() == "change")
            {
                Console.WriteLine("Change visibility to:");
                consoleInput = Console.ReadLine();
                int flag = int.Parse(consoleInput);
                if(flag >= 0 && flag <= 2)
                    FlightsGUIDataImplementation.flag = flag;
            }
        }
    }
}