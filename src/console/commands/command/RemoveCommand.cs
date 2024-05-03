using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.entities;
using OOD_24L_01180689.src.dto.entities.airports;
using OOD_24L_01180689.src.factories.entityFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData;

namespace OOD_24L_01180689.src.console.commands.command
{
    public class RemoveCommand : Command
    {
        string objectClass;
        ConditionsList conditionsList;
        public RemoveCommand(string objectClass, ConditionsList conditionsList)
        {
            this.conditionsList = conditionsList == null ? new ConditionsList() : conditionsList;
            this.objectClass = objectClass;
        }

        public override bool Execute()
        {
            if (this.objectClass == null)
            {
                Console.WriteLine("Invalid object class");
                return false;
            }

            List<ulong> removed = new List<ulong>();

            string key = this.objectClass;
            var factories = Entity.entityFactories;
            var def = factories[key];
            var ent = def.Create();
            foreach (var obj in DataStorage.GetInstance.GetIDEntityMap().Values)
            {
                if (ent.TryParse(obj, out Entity output))
                {
                    if (!conditionsList.Check(output)) continue;
                    removed.Add(output.ID);
                    DataStorage.GetInstance.Remove(output);
                }
            }

            foreach (var flight in DataStorage.GetInstance.GetFlights())
            {
                flight.CrewID = flight.CrewID.Except(removed).ToArray();
                flight.LoadID = flight.LoadID.Except(removed).ToArray();
            }

            Console.WriteLine("Removed " + removed.Count + " " + objectClass + "s");

            return true;
        }
    }
}
