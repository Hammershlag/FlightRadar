using OOD_24L_01180689.src.dto.airports;
using OOD_24L_01180689.src.dto.cargo;
using OOD_24L_01180689.src.dto.flights;
using OOD_24L_01180689.src.dto.people;
using OOD_24L_01180689.src.dto.planes;
using OOD_24L_01180689.src.factories;

namespace OOD_24L_01180689.src.readers
{
    public class Reader : IDataSource
    {
        protected static readonly Dictionary<string, EntityFactory> factoryMethods = new Dictionary<string, EntityFactory>
        {
            { "CP", new CargoPlaneFactory()},
            { "PP", new PassengerPlaneFactory()},
            { "AI", new AirportFactory()},
            { "CA", new CargoFactory()},
            { "FL", new FlightFactory()},
            { "C", new CrewFactory()},
            { "P", new PassengerFactory()}
        };

        public virtual IEnumerable<object> ReadData(string dir, string filename)
        {
            throw new NotImplementedException();
        }
    }
}