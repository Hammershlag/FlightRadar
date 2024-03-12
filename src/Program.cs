using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using OOD_24L_01180689.src.factories;
using OOD_24L_01180689.src.readers;
using OOD_24L_01180689.src.writers;
using NetworkSourceSimulator;
using OOD_24L_01180689.src.serverSimulator;
using IDataSource = OOD_24L_01180689.src.readers.IDataSource;

class Program
{

    public static List<object> objectList = new List<object>();
    public static readonly object objectListLock = new object();
    private static readonly object consoleLock = new object();
    private static bool stay = true;



    static void Main(string[] args)
    {
        string dir = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
        string input = Path.Combine(dir, "data", "example1.ftr");
        string outputDir = "data";
        string outputFilename = "defaultFilename.json";
        int minDelay = 0;
        int maxDelay = 0;

        var countDisplayThread = new Thread(DisplayObjectCount);
        countDisplayThread.IsBackground = true;
        countDisplayThread.Start();

        ServerSimulator ss = ServerSimulator.GetInstance(input, minDelay, maxDelay);

        IFileReaderFactory fileReaderFactory = new ServerReaderFactory(ss);
        IDataSource serverReader = fileReaderFactory.Create();



        ss.Run();

        IFileWriterFactory fileWriterFactory = new JSONWriterFactory();
        IWriter jsonWriter = fileWriterFactory.Create();

        HandleConsoleInput(jsonWriter, dir, outputDir);

        ss.Stop();

        try
        {
            if (countDisplayThread != null && countDisplayThread.IsAlive)
            {
                stay = false;
                countDisplayThread.Join();
            }
        }
        catch
        {
            Console.WriteLine("Count Thread Interrupted");
        }

    }

    static void DisplayObjectCount()
    {
        while (stay)
        {
            lock (objectListLock)
            {
                int previousLeft = Console.CursorLeft;
                int previousTop = Console.CursorTop;
                Console.SetCursorPosition(0, 0);
                Console.Write($"Current number of objects: {objectList.Count}\n");
                Console.SetCursorPosition(previousLeft, previousTop);
            }

            Thread.Sleep(500);
        }
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
            lock (consoleLock)
            {
                if (consoleInput.ToLower() == "exit")
                {
                    break;
                }
                else if (consoleInput.ToLower() == "print")
                {
                    lock (objectListLock)
                    {
                        string outputFilename = DateTime.Now.ToString("'snapshot_'HH_mm_ss'.json'");
                        jsonWriter.Write(objectList, Path.Combine(dir, outputDir), outputFilename);
                        Console.WriteLine($"Serialized data written to file: {outputFilename}");
                    }
                }
                else if (consoleInput.ToLower() == "help")
                {
                    Console.WriteLine("Type 'exit' to quit, 'print' to serialize data, 'help' to display commands");
                }

            }
        }
    }
}
