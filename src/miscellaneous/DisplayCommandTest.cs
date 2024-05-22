using OOD_24L_01180689.src.console.commands;
using OOD_24L_01180689.src.console.commands.command;
using OOD_24L_01180689.src.factories.readers;

namespace OOD_24L_01180689.src.miscellaneous;

public class DisplayCommandTest
{
    private static void MainDisplay(string[] args)
    {
        var dir = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
        var input = Path.Combine(dir, "data", "input_example.ftr");
        var outputDir = "data";
        var minDelay = 10;
        var maxDelay = 20;


        IFileReaderFactory fileReaderFactory = new FTRReaderFactory();
        var reader = fileReaderFactory.Create();
        reader.ReadData(input);

        var airportTypes = new List<string>();
        airportTypes.Add("ID");
        airportTypes.Add("NAME");
        airportTypes.Add("TYPE");
        airportTypes.Add("CODE");
        airportTypes.Add("LONGITUDE");
        airportTypes.Add("LATITUDE");
        airportTypes.Add("AMSL");
        airportTypes.Add("COUNTRYISO");

        var cargoTypes = new List<string>();
        cargoTypes.Add("ID");
        cargoTypes.Add("TYPE");
        cargoTypes.Add("WEIGHT");
        cargoTypes.Add("CODE");
        cargoTypes.Add("DESCRIPTION");

        var flightTypes = new List<string>();
        flightTypes.Add("ID");
        flightTypes.Add("TYPE");
        flightTypes.Add("ORIGIN ID");
        flightTypes.Add("TARGET ID");
        flightTypes.Add("TAKEOFFTIME");
        flightTypes.Add("LANDINGTIME");
        flightTypes.Add("LONGITUDE");
        flightTypes.Add("LATITUDE");
        flightTypes.Add("AMSL");
        flightTypes.Add("PLANEID");
        flightTypes.Add("CREWID");
        flightTypes.Add("LOADID");

        var crewTypes = new List<string>();
        crewTypes.Add("ID");
        crewTypes.Add("TYPE");
        crewTypes.Add("NAME");
        crewTypes.Add("AGE");
        crewTypes.Add("EMAIL");
        crewTypes.Add("PHONE");
        crewTypes.Add("PRACTICE");
        crewTypes.Add("ROLE");

        var passengerTypes = new List<string>();
        passengerTypes.Add("ID");
        passengerTypes.Add("TYPE");
        passengerTypes.Add("NAME");
        passengerTypes.Add("AGE");
        passengerTypes.Add("EMAIL");
        passengerTypes.Add("PHONE");
        passengerTypes.Add("CLASS");
        passengerTypes.Add("MILES");

        var cargoPlaneTypes = new List<string>();
        cargoPlaneTypes.Add("ID");
        cargoPlaneTypes.Add("TYPE");
        cargoPlaneTypes.Add("SERIAL");
        cargoPlaneTypes.Add("COUNTRYISO");
        cargoPlaneTypes.Add("MODEL");
        cargoPlaneTypes.Add("MAX LOAD");

        var passengerPlaneTypes = new List<string>();
        passengerPlaneTypes.Add("ID");
        passengerPlaneTypes.Add("TYPE");
        passengerPlaneTypes.Add("SERIAL");
        passengerPlaneTypes.Add("COUNTRYISO");
        passengerPlaneTypes.Add("MODEL");
        passengerPlaneTypes.Add("FIRSTCLASSSIZE");
        passengerPlaneTypes.Add("BUSINESSCLASSSIZE");
        passengerPlaneTypes.Add("ECONOMYCLASSSIZE");

        var wrongTypes = new List<string>();


        var conditionsListAirport = new ConditionsList();
        conditionsListAirport.AddCondition(new Condition("AMSL", (float)700, Condition.ConditionType.GREATER));
        conditionsListAirport.AndOrs.Add(ConditionsList.Conjunctions.AND);
        conditionsListAirport.AddCondition(new Condition("AMSL", (float)1300, Condition.ConditionType.LESS));

        var nameTypeDict =
            new Dictionary<string, List<string>>
            {
                { "Airport", airportTypes },
                { "Cargo", cargoTypes },
                { "Flight", flightTypes },
                { "Crew", crewTypes },
                { "Passenger", passengerTypes },
                { "CargoPlane", cargoPlaneTypes },
                { "PassengerPlane", passengerPlaneTypes },
                { "Wrong", wrongTypes }
            };

        var key = "Flight";
        nameTypeDict.TryGetValue(key, out var list);

        var dc = new DisplayCommand(key, list, conditionsListAirport);
        dc.Execute();
    }
}