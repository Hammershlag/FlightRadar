using NetworkSourceSimulator;
using OOD_24L_01180689.src.dto.airports;
using OOD_24L_01180689.src.dto.cargo;
using OOD_24L_01180689.src.dto.flights;
using OOD_24L_01180689.src.dto.people;
using OOD_24L_01180689.src.dto.planes;
using OOD_24L_01180689.src.factories;
using OOD_24L_01180689.src.serverSimulator;
using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OOD_24L_01180689.src.readers
{
    public class ServerReader : Reader
    {
        protected static readonly Dictionary<string, EntityFactory> factoryMethods = new Dictionary<string, EntityFactory>
        {
            { "NCP", new CargoPlaneFactory()},
            { "NPP", new PassengerPlaneFactory()},
            { "NAI", new AirportFactory()},
            { "NCA", new CargoFactory()},
            { "NFL", new FlightFactory()},
            { "NCR", new CrewFactory()},
            { "NPA", new PassengerFactory()}
        };

        public ServerReader(ServerSimulator serverSimulator)
        {
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
            else
            {
                Console.WriteLine($"Unsupported message type: {objectType}");
            }
        }
    }

    public class ServerReaderFactory : IFileReaderFactory
    {
        private ServerSimulator _serverSimulator;

        public ServerReaderFactory(ServerSimulator serverSimulator)
        {
            _serverSimulator = serverSimulator;
        }

        public IDataSource Create()
        {
            return new ServerReader(_serverSimulator);
        }
    }
}
