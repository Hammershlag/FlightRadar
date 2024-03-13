using System;
using System.IO;
using OOD_24L_01180689.src.factories;
using OOD_24L_01180689.src.readers;
using OOD_24L_01180689.src.writers;
using NetworkSourceSimulator;
using OOD_24L_01180689.src.serverSimulator;
using OOD_24L_01180689.src.dataStorage;
using IDataSource = OOD_24L_01180689.src.readers.IDataSource;

class Program
{
    static void Main(string[] args)
    {
        string dir = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
        string input = Path.Combine(dir, "data", "example1.ftr");
        string outputDir = "data";
        int minDelay = 10;
        int maxDelay = 10;

        var objectCountDisplay = ObjectCountDisplay.Instance;
        objectCountDisplay.Start();

        ServerSimulator ss = ServerSimulator.GetInstance(input, minDelay, maxDelay);

        IFileReaderFactory fileReaderFactory = new ServerReaderFactory(ss);
        IDataSource serverReader = fileReaderFactory.Create();

        ss.Run();

        IFileWriterFactory fileWriterFactory = new JSONWriterFactory();
        IWriter jsonWriter = fileWriterFactory.Create();

        HandleConsoleInput(jsonWriter, dir, outputDir);

        ss.Stop();

        objectCountDisplay.Stop();
    }

    private static void HandleConsoleInput(IWriter jsonWriter, string dir, string outputDir)
    {
        Console.WriteLine();
        Console.WriteLine("Network source simulator started. ");
        Console.WriteLine("Type 'exit' to quit, 'print' to serialize data, 'help' to display commands");
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
                var objectListCopy = DataStorage.Instance.Get();
                string outputFilename = DateTime.Now.ToString("'snapshot_'HH_mm_ss'.json'");
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
