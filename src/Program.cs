using Avalonia.Utilities;
using OOD_24L_01180689.src.writers;
using OOD_24L_01180689.src.serverSimulator;
using IDataSource = OOD_24L_01180689.src.readers.IDataSource;
using OOD_24L_01180689.src.factories.readers;
using OOD_24L_01180689.src.factories.writersFactories;
using OOD_24L_01180689.src.threads;
using OOD_24L_01180689.src.console;
using OOD_24L_01180689.src.console.commands;
using OOD_24L_01180689.src.console.commands.command;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.entities;
using OOD_24L_01180689.src.dto.reports.reporters.reporters;
using OOD_24L_01180689.src.logging;
using OOD_24L_01180689.src.reports;
using OOD_24L_01180689.src.updating;

class Program
{
    static void Main(string[] args)
    {
        string dir = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
        string input = Path.Combine(dir, "data", "input_example.ftr");
        string update = Path.Combine(dir, "data", "update_example.ftre");
        string outputDir = "data";
        int minDelay = 10;
        int maxDelay = 20;
        ILogger logger = new FileLogger();

        Console.Clear();

        var flightTrackerUpdater = FlightTrackerUpdater.GetInstance();
        flightTrackerUpdater.Start();

        NewsGenerator.InitializeProviders();
        NewsGenerator newsGenerator = new NewsGenerator(DataStorage.GetInstance.GetNewsProviders(),
            DataStorage.GetInstance.GetReporters());

        IFileReaderFactory fileReaderFactory = new FTRReaderFactory();
        IDataSource reader = fileReaderFactory.Create();
        reader.ReadData(input);


        EventUpdateManager eventHandler = EventUpdateManager.GetInstance(update, minDelay, maxDelay, logger);
        eventHandler.Start();

        IFileWriterFactory fileWriterFactory = new JSONWriterFactory();
        IWriter jsonWriter = fileWriterFactory.Create();
        List<string> airportTypes = new List<string>();
        airportTypes.Add("ID");
        airportTypes.Add("NAME");
        airportTypes.Add("TYPE");
        airportTypes.Add("CODE");
        airportTypes.Add("LONGITUDE");
        airportTypes.Add("LATITUDE");
        airportTypes.Add("AMSL");
        airportTypes.Add("COUNTRY ISO");

        List<string> cargoTypes = new List<string>();
        cargoTypes.Add("ID");
        cargoTypes.Add("TYPE");
        cargoTypes.Add("WEIGHT");
        cargoTypes.Add("CODE");
        cargoTypes.Add("DESCRIPTION");

        List<string> flightTypes = new List<string>();
        flightTypes.Add("ID");
        flightTypes.Add("TYPE");
        flightTypes.Add("ORIGIN ID");
        flightTypes.Add("TARGET ID");
        flightTypes.Add("TAKEOFF TIME");
        flightTypes.Add("LANDING TIME");
        flightTypes.Add("LONGITUDE");
        flightTypes.Add("LATITUDE");
        flightTypes.Add("AMSL");
        flightTypes.Add("PLANE ID");
        flightTypes.Add("CREW ID");
        flightTypes.Add("LOAD ID");

        List<string> crewTypes = new List<string>();
        crewTypes.Add("ID");
        crewTypes.Add("TYPE");
        crewTypes.Add("NAME");
        crewTypes.Add("AGE");
        crewTypes.Add("EMAIL");
        crewTypes.Add("PHONE");
        crewTypes.Add("PRACTICE");
        crewTypes.Add("ROLE");

        List<string> passengerTypes = new List<string>();
        passengerTypes.Add("ID");
        passengerTypes.Add("TYPE");
        passengerTypes.Add("NAME");
        passengerTypes.Add("AGE");
        passengerTypes.Add("EMAIL");
        passengerTypes.Add("PHONE");
        passengerTypes.Add("CLASS");
        passengerTypes.Add("MILES");

        List<string> cargoPlaneTypes = new List<string>();
        cargoPlaneTypes.Add("ID");
        cargoPlaneTypes.Add("TYPE");
        cargoPlaneTypes.Add("SERIAL");
        cargoPlaneTypes.Add("COUNTRY ISO");
        cargoPlaneTypes.Add("MODEL");
        cargoPlaneTypes.Add("MAX LOAD");

        List<string> passengerPlaneTypes = new List<string>();
        passengerPlaneTypes.Add("ID");
        passengerPlaneTypes.Add("TYPE");
        passengerPlaneTypes.Add("SERIAL");
        passengerPlaneTypes.Add("COUNTRY ISO");
        passengerPlaneTypes.Add("MODEL");
        passengerPlaneTypes.Add("FIRST CLASS SIZE");
        passengerPlaneTypes.Add("BUSINESS CLASS SIZE");
        passengerPlaneTypes.Add("ECONOMY CLASS SIZE");

        List<string> wrongTypes = new List<string>();


        ConditionsList conditionsListAirport = new ConditionsList();
        conditionsListAirport.AddCondition(new Condition("ID", "ID", Condition.ConditionType.EQUAL));
        conditionsListAirport.AndOrs.Add(ConditionsList.Conjunctions.AND);
        conditionsListAirport.AddCondition(new Condition("ID", (ulong)5, Condition.ConditionType.LESS_EQUAL));
        conditionsListAirport.AndOrs.Add(ConditionsList.Conjunctions.AND);
        conditionsListAirport.AddCondition(new Condition("ID", (ulong)1, Condition.ConditionType.GREATER));

        Dictionary<string, List<string>> nameTypeDict =
        new Dictionary<string, List<string>>(){
            {"Airport", airportTypes},
            {"Cargo", cargoTypes},
            {"Flight", flightTypes},
            {"Crew", crewTypes},
            {"Passenger", passengerTypes},
            {"CargoPlane", cargoPlaneTypes},
            {"PassengerPlane", passengerPlaneTypes},
            {"Wrong", wrongTypes}
        };

        string key = "Airport";
        nameTypeDict.TryGetValue(key, out List<string> list);

        DisplayCommand dc = new DisplayCommand(key, list, conditionsListAirport);
        dc.Execute();
        return;

        var consoleHandler = new ConsoleHandler(jsonWriter, dir, outputDir, newsGenerator);
        consoleHandler.HandleConsoleInput();

        flightTrackerUpdater.Stop();
        eventHandler.Stop();
    }
}