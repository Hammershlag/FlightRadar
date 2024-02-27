using OOD_24L_01180689.src.factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180689.src.readers
{
    public class FTRReader : IDataSource
    {
        private readonly Dictionary<string, Func<string[], object>> factoryMethods;

        public FTRReader()
        {
            // Initialize all factories
            var cargoPlaneFactory = new CargoPlaneFactory();
            var passengerPlaneFactory = new PassengerPlaneFactory();
            var airportFactory = new AirportFactory();
            var cargoFactory = new CargoFactory();
            var flightFactory = new FlightFactory();
            var crewFactory = new CrewFactory();
            var passengerFactory = new PassengerFactory();

            // Create a dictionary mapping object types to their factory methods
            factoryMethods = new Dictionary<string, Func<string[], object>>
            {
                { "CP", cargoPlaneFactory.Create },
                { "PP", passengerPlaneFactory.Create },
                { "AI", airportFactory.Create },
                { "CA", cargoFactory.Create },
                { "FL", flightFactory.Create },
                { "C", crewFactory.Create },
                { "P", passengerFactory.Create }
            };
        }

        public IEnumerable<object> ReadData(string filePath)
        {
            if (!filePath.EndsWith(".ftr"))
            {
                throw new ArgumentException("Invalid file type");
            }
            var objects = new List<object>();

            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var data = line.Split(',');
                    var objectType = data[0]; //Type of an object

                    if (factoryMethods.TryGetValue(objectType, out var factoryMethod))
                    {
                        var obj = factoryMethod(data);
                        objects.Add(obj);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid object type");
                    }

                }
            }

            return objects;
        }
    }
}
