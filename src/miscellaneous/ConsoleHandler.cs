using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.visualization;
using OOD_24L_01180689.src.writers;

namespace OOD_24L_01180689.src.console
{
    public class ConsoleHandler
    {
        private readonly IWriter jsonWriter;
        private readonly string dir;
        private readonly string outputDir;

        public ConsoleHandler(IWriter jsonWriter, string dir, string outputDir)
        {
            this.jsonWriter = jsonWriter;
            this.dir = dir;
            this.outputDir = outputDir;
        }

        public void HandleConsoleInput()
        {
            Console.WriteLine();
            Console.WriteLine("Network source simulator started.");
            Console.WriteLine(
                "Type 'exit' to quit, 'print' to serialize data, 'help' to display commands, or 'change' to change flight visibility.");
            while (true)
            {
                string consoleInput = Console.ReadLine();
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
                }
            }
        }

        private void PrintData()
        {
            var objectListCopy = DataStorage.GetInstance.GetObjectList();
            string outputFilename = DateTime.Now.ToString("'snapshot_'HH_mm_ss'.json'");
            jsonWriter.Write(objectListCopy, Path.Combine(dir, outputDir), outputFilename);
            Console.WriteLine($"Serialized data written to file: {outputFilename}");
        }

        private void DisplayHelp()
        {
            Console.WriteLine($"Type: \n'exit' to quit" +
                              $"\n'print' to serialize data" +
                              $"\n'help' to display commands" +
                              $"\n'change' to change flight visibility: 0 - all flights, 1 - all flights that are in progress, 2 - each plane should appear only once");
        }

        private void ChangeVisibility()
        {
            Console.WriteLine("Change visibility to:");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int flag) && flag >= 0 && flag <= 2)
            {
                FlightsGUIDataImplementation.displayFlightsFlag = flag;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number between 0 and 2.");
            }
        }
    }
}