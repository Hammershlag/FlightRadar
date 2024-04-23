using NetworkSourceSimulator;
using OOD_24L_01180689.src.serverSimulator;
using System.Text;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.factories.entityFactories;
using OOD_24L_01180689.src.factories.entityFactories.airports;
using OOD_24L_01180689.src.factories.entityFactories.cargo;
using OOD_24L_01180689.src.factories.entityFactories.flights;
using OOD_24L_01180689.src.factories.entityFactories.people;
using OOD_24L_01180689.src.factories.entityFactories.planes;

namespace OOD_24L_01180689.src.readers
{
    public class ServerReader : Reader
    {
        protected static readonly Dictionary<string, EntityFactory> factoryMethods =
            new Dictionary<string, EntityFactory>
            {
                { "NCP", new CargoPlaneFactory() },
                { "NPP", new PassengerPlaneFactory() },
                { "NAI", new AirportFactory() },
                { "NCA", new CargoFactory() },
                { "NFL", new FlightFactory() },
                { "NCR", new CrewFactory() },
                { "NPA", new PassengerFactory() }
            };

        public ServerReader(ServerSimulator serverSimulator)
        {
            Console.WriteLine();
            Console.WriteLine("Network source simulator started.");
            serverSimulator.OnDataReady += HandleNewDataReady;
        }

        private void HandleNewDataReady(object sender, NewDataReadyArgs e)
        {
            var simulator = (ServerSimulator)sender;
            Message message = simulator.GetMessageAt(e.MessageIndex);
            ProcessMessage(message);
        }

        private void ProcessMessage(Message message)
        {
            string objectType = Encoding.Default.GetString(message.MessageBytes, 0, 3);
            if (factoryMethods.TryGetValue(objectType, out var factory))
            {
                if (factoryMethods.TryGetValue(objectType, out var factoryMethod))
                {
                    var obj = factoryMethod.Create(message);
                    DataStorage.GetInstance.Add(obj);
                }
                else
                {
                    throw new ArgumentException("Invalid object type");
                }
            }
            else
            {
                Console.WriteLine($"Unsupported message type: {objectType}");
            }
        }
    }
}