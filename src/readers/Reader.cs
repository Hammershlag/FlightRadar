using OOD_24L_01180689.src.dto.airports;
using OOD_24L_01180689.src.dto.cargo;
using OOD_24L_01180689.src.dto.flights;
using OOD_24L_01180689.src.dto.people;
using OOD_24L_01180689.src.dto.planes;

namespace OOD_24L_01180689.src.readers
{
    public class Reader : IDataSource
    {
        protected readonly Dictionary<string, Func<string[], object>> factoryMethods;

        public Reader()
        {
            var cargoPlaneFactory = new CargoPlaneFactory();
            var passengerPlaneFactory = new PassengerPlaneFactory();
            var airportFactory = new AirportFactory();
            var cargoFactory = new CargoFactory();
            var flightFactory = new FlightFactory();
            var crewFactory = new CrewFactory();
            var passengerFactory = new PassengerFactory();

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

        public virtual IEnumerable<object> ReadData(string dir, string filename)
        {
            throw new NotImplementedException();
        }
    }
}