using OOD_24L_01180689.src.factories.entityFactories.airports;
using OOD_24L_01180689.src.factories.entityFactories.cargo;
using OOD_24L_01180689.src.factories.entityFactories.flights;
using OOD_24L_01180689.src.factories.entityFactories.people;
using OOD_24L_01180689.src.factories.entityFactories.planes;
using OOD_24L_01180689.src.factories.entityFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.entities;

namespace OOD_24L_01180689.src.console.commands.command
{
    public class AddCommand : Command
    {
        public string objectClass;
        public Dictionary<string, IComparable> keyValSet = new Dictionary<string, IComparable>();
        protected static readonly Dictionary<string, EntityFactory> factoryMethods =
            new Dictionary<string, EntityFactory>
            {
                { "CargoPlane", new CargoPlaneFactory() },
                { "PassengerPlane", new PassengerPlaneFactory() },
                { "Airport", new AirportFactory() },
                { "Carggo", new CargoFactory() },
                { "Flight", new FlightFactory() },
                { "Crew", new CrewFactory() },
                { "Passenger", new PassengerFactory() }
            };

        public AddCommand(string objectClass, Dictionary<string, IComparable> keyValSet)
        {
            this.keyValSet = keyValSet;
            this.objectClass = objectClass;
        }

        public override bool Execute()
        {
            if (this.objectClass == null)
            {
                Console.WriteLine("Invalid object class");
                return false;
            }

            EntityFactory factory = factoryMethods[this.objectClass];
            Entity ent = factory.Create();

            Action<IComparable> setter;
            List<string> keys = ent.fieldSetters.Keys.Select(key => key.ToUpper()).ToList();

            foreach (var field in keys)
            {
                setter = ent.fieldSetters[field];
                if (!keyValSet.TryGetValue(field, out IComparable value))
                {
                    setter(null);
                }
                else
                {
                    setter(value);
                }
            }

            DataStorage.GetInstance.Add(ent);

            Console.WriteLine($"Added Entity: {ent.ToString()}");
            return true;
        }
    }
}
