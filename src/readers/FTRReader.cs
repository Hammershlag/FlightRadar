using OOD_24L_01180689.src.dto.airports;
using OOD_24L_01180689.src.dto.cargo;
using OOD_24L_01180689.src.dto.flights;
using OOD_24L_01180689.src.dto.people;
using OOD_24L_01180689.src.dto.planes;
using OOD_24L_01180689.src.factories;
using System;
namespace OOD_24L_01180689.src.readers
{

    public class FTRReader : Reader
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
        public override IEnumerable<object> ReadData(string dir, string filename)
        {
            var filePath = dir + "\\" + filename;

            if (!filePath.EndsWith(".ftr"))
            {
                throw new ArgumentException("Invalid file type");
            }


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
                        lock (Program.objectListLock)
                        {
                            Program.objectList.Add(obj);
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Invalid object type");
                    }
                }
            }

            return Program.objectList;
        }
    }

    public class FTRReaderFactory : IFileReaderFactory
    {
        public IDataSource Create()
        {
            return new FTRReader();
        }
    }
}