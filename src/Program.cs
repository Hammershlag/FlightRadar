﻿using FlightTrackerGUI;
using OOD_24L_01180689.src.writers;
using OOD_24L_01180689.src.serverSimulator;
using OOD_24L_01180689.src.dataStorage;
using IDataSource = OOD_24L_01180689.src.readers.IDataSource;
using OOD_24L_01180689.src.factories.readers;
using OOD_24L_01180689.src.factories.writersFactories;
using OOD_24L_01180689.src.visualization;

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

        ServerSimulator ss = ServerSimulator.GetInstance(input, minDelay, maxDelay);

        IFileReaderFactory fileReaderFactory = new ServerReaderFactory(ss);
        IDataSource serverReader = fileReaderFactory.Create();

        ss.Run();
        Thread gui = new Thread(() => Runner.Run());
        gui.Start();

        int co = 1;
        Thread updateThread = new Thread(() =>
        {
            while (true)
            {
                FlightsGUIDataImplementation.updateFlights();
                Console.WriteLine("Updated " + co++);

                //Thread.Sleep(TimeSpan.FromMinutes(1));
                Thread.Sleep(TimeSpan.FromSeconds(1));
                //Thread.Sleep(TimeSpan.FromMilliseconds(50));
            }
        });
        updateThread.Start();


        IFileWriterFactory fileWriterFactory = new JSONWriterFactory();
        IWriter jsonWriter = fileWriterFactory.Create();

        HandleConsoleInput(jsonWriter, dir, outputDir);

        ss.Stop();
        objectCountDisplay.Stop();
        gui.Abort();
        updateThread.Abort();
    }

    private static void HandleConsoleInput(IWriter jsonWriter, string dir, string outputDir)
    {
        Console.WriteLine();
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
                FlightsGUIDataImplementation.updateFlights();
                var objectListCopy = DataStorage.Instance.GetObjectList();
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