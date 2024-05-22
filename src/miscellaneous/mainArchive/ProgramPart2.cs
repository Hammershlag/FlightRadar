using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.factories.readers;
using OOD_24L_01180689.src.factories.writersFactories;
using OOD_24L_01180689.src.serverSimulator;
using OOD_24L_01180689.src.writers;

internal class ProgramPart2
{
    private static void Main2(string[] args)
    {
        var dir = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
        var input = Path.Combine(dir, "data", "input_example.ftr");
        var outputDir = "data";
        var minDelay = 1;
        var maxDelay = 1;

        var objectCountDisplay = ObjectCountDisplay.GetInstance;
        objectCountDisplay.Start();

        var ss = ServerSimulator.GetInstance(input, minDelay, maxDelay);

        IFileReaderFactory fileReaderFactory = new ServerReaderFactory(ss);
        var serverReader = fileReaderFactory.Create();

        ss.Start();

        IFileWriterFactory fileWriterFactory = new JSONWriterFactory();
        var jsonWriter = fileWriterFactory.Create();

        HandleConsoleInput(jsonWriter, dir, outputDir);

        ss.Stop();
        objectCountDisplay.Stop();
    }

    private static void HandleConsoleInput(IWriter jsonWriter, string dir, string outputDir)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Network source simulator started. ");
        Console.WriteLine("Type 'exit' to quit, 'print' to serialize data, 'help' to display commands");
        var consoleInput = "";
        while (true)
        {
            consoleInput = Console.ReadLine();
            if (consoleInput.ToLower() == "exit")
            {
                break;
            }

            if (consoleInput.ToLower() == "print")
            {
                var objectListCopy = DataStorage.GetInstance.GetObjectList();
                var outputFilename = DateTime.Now.ToString("'snapshot_'HH_mm_ss'.json'");
                jsonWriter.Write(objectListCopy, Path.Combine(dir, outputDir), outputFilename);
                Console.WriteLine($"Serialized data written to file: {outputFilename}");
            }
            else if (consoleInput.ToLower() == "help")
            {
                Console.WriteLine("Type 'exit' to quit, 'print' to serialize data, 'help' to display commands");
            }
        }
    }
}