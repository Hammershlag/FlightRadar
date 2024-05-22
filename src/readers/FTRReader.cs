using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.factories.entityFactories;
using OOD_24L_01180689.src.factories.entityFactories.airports;
using OOD_24L_01180689.src.factories.entityFactories.cargo;
using OOD_24L_01180689.src.factories.entityFactories.flights;
using OOD_24L_01180689.src.factories.entityFactories.people;
using OOD_24L_01180689.src.factories.entityFactories.planes;

namespace OOD_24L_01180689.src.readers;

public class FTRReader : Reader
{
    protected static readonly Dictionary<string, EntityFactory> factoryMethods =
        new()
        {
            { "CP", new CargoPlaneFactory() },
            { "PP", new PassengerPlaneFactory() },
            { "AI", new AirportFactory() },
            { "CA", new CargoFactory() },
            { "FL", new FlightFactory() },
            { "C", new CrewFactory() },
            { "P", new PassengerFactory() }
        };

    public override void ReadData(string filename)
    {
        Console.WriteLine();
        Console.WriteLine("FTR Reader started.");
        var filePath = filename;

        if (!filePath.EndsWith(".ftr")) throw new ArgumentException("Invalid file type");


        using (var reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var data = line.Split(',');
                var objectType = data[0];

                if (factoryMethods.TryGetValue(objectType, out var factoryMethod))
                {
                    var obj = factoryMethod.Create(data);
                    DataStorage.GetInstance.Add(obj);
                }
                else
                {
                    throw new ArgumentException("Invalid object type");
                }
            }
        }
    }
}