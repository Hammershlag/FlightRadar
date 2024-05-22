using System.Text;
using NetworkSourceSimulator;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.factories.entityFactories;
using OOD_24L_01180689.src.factories.entityFactories.airports;
using OOD_24L_01180689.src.factories.entityFactories.cargo;
using OOD_24L_01180689.src.factories.entityFactories.flights;
using OOD_24L_01180689.src.factories.entityFactories.people;
using OOD_24L_01180689.src.factories.entityFactories.planes;
using OOD_24L_01180689.src.serverSimulator;

namespace OOD_24L_01180689.src.readers;

public class ServerReader : Reader
{
    protected static readonly Dictionary<string, EntityFactory> factoryMethods =
        new()
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
        var message = simulator.GetMessageAt(e.MessageIndex);
        ProcessMessage(message);
    }

    private void ProcessMessage(Message message)
    {
        var objectType = Encoding.Default.GetString(message.MessageBytes, 0, 3);
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