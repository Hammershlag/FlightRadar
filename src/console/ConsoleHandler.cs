using OOD_24L_01180689.src.console.commands.parser;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.reports;
using OOD_24L_01180689.src.visualization;
using OOD_24L_01180689.src.writers;

namespace OOD_24L_01180689.src.console;

public class ConsoleHandler
{
    private readonly string dir;
    private readonly IWriter jsonWriter;
    private readonly string outputDir;
    private NewsGenerator newsGenerator;
    private readonly CommandParser parser = new();

    public ConsoleHandler(IWriter jsonWriter, string dir, string outputDir, NewsGenerator newsGenerator)
    {
        this.jsonWriter = jsonWriter;
        this.dir = dir;
        this.outputDir = outputDir;
        this.newsGenerator = newsGenerator;
    }

    public ConsoleHandler(IWriter jsonWriter, string dir, string outputDir)
    {
        this.jsonWriter = jsonWriter;
        this.dir = dir;
        this.outputDir = outputDir;
    }

    public void HandleConsoleInput()
    {
        Console.WriteLine(
            "Type 'exit' to quit or 'help' to display commands.");
        while (true)
        {
            var consoleInput = Console.ReadLine();
            switch (consoleInput.ToLower())
            {
                case "exit":
                    return;
                case "print":
                    PrintData();
                    break;
                case "help":
                    DisplayHelp();
                    break;
                case "change":
                    ChangeVisibility();
                    break;
                case "report":
                    Report();
                    break;
                default:
                    var command = parser.Parse(consoleInput);
                    if (command != null) command.Execute();
                    break;
            }
        }
    }

    private void PrintData()
    {
        var objectListCopy = DataStorage.GetInstance.GetObjectList();
        var outputFilename = DateTime.Now.ToString("'snapshot_'HH_mm_ss'.json'");
        jsonWriter.Write(objectListCopy, Path.Combine(dir, outputDir), outputFilename);
        Console.WriteLine($"Serialized data written to file: {outputFilename}");
    }

    public static void DisplayHelp()
    {
        Console.WriteLine($"Type: \n'exit' to quit" +
                          $"\n'print' to serialize data" +
                          $"\n'help' to display commands" +
                          $"\n'change' to change flight visibility: 0 - all flights, 1 - all flights that are in progress, 2 - each plane should appear only once" +
                          $"\n'report to report all data that can be reported" +
                          $"\n'add' to add new entity" +
                          $"\n'display' to display entities" +
                          $"\n'update' to update enity" +
                          $"\n'delete' to remove entity");
    }

    private void ChangeVisibility()
    {
        Console.WriteLine("Change visibility to:");
        var input = Console.ReadLine();
        if (int.TryParse(input, out var flag) && flag >= 0 && flag <= 2)
            FlightsGUIDataAdapter.displayFlightsFlag = flag;
        else
            Console.WriteLine("Invalid input. Please enter a number between 0 and 2.");
    }

    private void Report()
    {
        newsGenerator = new NewsGenerator(DataStorage.GetInstance.GetNewsProviders(),
            DataStorage.GetInstance.GetReporters());

        Console.WriteLine("\n-------");
        Console.WriteLine("REPORTS");
        Console.WriteLine("-------\n");
        while (newsGenerator.HasNext()) Console.WriteLine(newsGenerator.Next());

        Console.WriteLine("\n--------------");
        Console.WriteLine("End of reports");
        Console.WriteLine("--------------\n");
    }
}