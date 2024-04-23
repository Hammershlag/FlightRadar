using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180689.src.miscellaneous.mainArchive
{
    using OOD_24L_01180689.src.writers;
    using OOD_24L_01180689.src.serverSimulator;
    using IDataSource = OOD_24L_01180689.src.readers.IDataSource;
    using OOD_24L_01180689.src.factories.readers;
    using OOD_24L_01180689.src.factories.writersFactories;
    using OOD_24L_01180689.src.threads;
    using OOD_24L_01180689.src.console;
    using OOD_24L_01180689.src.dataStorage;
    using OOD_24L_01180689.src.dto.reports.reporters.reporters;
    using OOD_24L_01180689.src.reports;

    public class ProgramPart4
    {
        static void Main4(string[] args)
        {
            string dir = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
            string input = Path.Combine(dir, "data", "input_example.ftr");
            string outputDir = "data";

            Console.Clear();

            var flightTrackerUpdater = FlightTrackerUpdater.GetInstance();
            flightTrackerUpdater.Start();


            NewsGenerator.InitializeProviders();
            NewsGenerator newsGenerator = new NewsGenerator(DataStorage.GetInstance.GetNewsProviders(),
                DataStorage.GetInstance.GetReporters());


            IFileReaderFactory fileReaderFactory = new FTRReaderFactory();
            IDataSource reader = fileReaderFactory.Create();
            reader.ReadData(input);


            IFileWriterFactory fileWriterFactory = new JSONWriterFactory();
            IWriter jsonWriter = fileWriterFactory.Create();

            var consoleHandler = new ConsoleHandler(jsonWriter, dir, outputDir, newsGenerator);
            consoleHandler.HandleConsoleInput();

            flightTrackerUpdater.Stop();
        }
    }
}
