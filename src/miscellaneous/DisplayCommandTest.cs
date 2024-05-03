using OOD_24L_01180689.src.console.commands.command;
using OOD_24L_01180689.src.console.commands;
using OOD_24L_01180689.src.console;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.factories.readers;
using OOD_24L_01180689.src.factories.writersFactories;
using OOD_24L_01180689.src.logging;
using OOD_24L_01180689.src.reports;
using OOD_24L_01180689.src.threads;
using OOD_24L_01180689.src.updating;
using OOD_24L_01180689.src.writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOD_24L_01180689.src.readers;

namespace OOD_24L_01180689.src.miscellaneous
{
    public class DisplayCommandTest
    {
        static void MainDisplay(string[] args)
        {
            string dir = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
            string input = Path.Combine(dir, "data", "input_example.ftr");
            string outputDir = "data";
            int minDelay = 10;
            int maxDelay = 20;


            IFileReaderFactory fileReaderFactory = new FTRReaderFactory();
            IDataSource reader = fileReaderFactory.Create();
            reader.ReadData(input);

            List<string> airportTypes = new List<string>();
            airportTypes.Add("ID");
            airportTypes.Add("NAME");
            airportTypes.Add("TYPE");
            airportTypes.Add("CODE");
            airportTypes.Add("LONGITUDE");
            airportTypes.Add("LATITUDE");
            airportTypes.Add("AMSL");
            airportTypes.Add("COUNTRYISO");

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
            flightTypes.Add("TAKEOFFTIME");
            flightTypes.Add("LANDINGTIME");
            flightTypes.Add("LONGITUDE");
            flightTypes.Add("LATITUDE");
            flightTypes.Add("AMSL");
            flightTypes.Add("PLANEID");
            flightTypes.Add("CREWID");
            flightTypes.Add("LOADID");

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
            cargoPlaneTypes.Add("COUNTRYISO");
            cargoPlaneTypes.Add("MODEL");
            cargoPlaneTypes.Add("MAX LOAD");

            List<string> passengerPlaneTypes = new List<string>();
            passengerPlaneTypes.Add("ID");
            passengerPlaneTypes.Add("TYPE");
            passengerPlaneTypes.Add("SERIAL");
            passengerPlaneTypes.Add("COUNTRYISO");
            passengerPlaneTypes.Add("MODEL");
            passengerPlaneTypes.Add("FIRSTCLASSSIZE");
            passengerPlaneTypes.Add("BUSINESSCLASSSIZE");
            passengerPlaneTypes.Add("ECONOMYCLASSSIZE");

            List<string> wrongTypes = new List<string>();


            ConditionsList conditionsListAirport = new ConditionsList();
            conditionsListAirport.AddCondition(new Condition("AMSL", (float)700, Condition.ConditionType.GREATER));
            conditionsListAirport.AndOrs.Add(ConditionsList.Conjunctions.AND);
            conditionsListAirport.AddCondition(new Condition("AMSL", (float)1300, Condition.ConditionType.LESS));

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

            string key = "Flight";
            nameTypeDict.TryGetValue(key, out List<string> list);

            DisplayCommand dc = new DisplayCommand(key, list, conditionsListAirport);
            dc.Execute();
        }
    }
}
