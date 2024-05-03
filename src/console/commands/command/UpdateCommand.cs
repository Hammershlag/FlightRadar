using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_24L_01180689.src.console.commands.command
{
    public class UpdateCommand : Command
    {
        private string objectClass;
        private Dictionary<string, IComparable> keyValSet;
        private ConditionsList conditionsList;
        public UpdateCommand(string objectClass, Dictionary<string, IComparable> keyValSet, ConditionsList conditionsList)
        {
            this.conditionsList = conditionsList == null ? new ConditionsList() : conditionsList;
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

            List<ulong> updated = new List<ulong>();

            var factories = Entity.entityFactories;

            if (!factories.ContainsKey(objectClass))
            {
                Console.WriteLine($"No factory found for {objectClass}");
                return false;
            }

            var def = factories[objectClass];
            var ent = def.Create();

            foreach (var obj in DataStorage.GetInstance.GetIDEntityMap().Values)
            {
                if (ent.TryParse(obj, out Entity output))
                {

                    if (!conditionsList.Check(output)) continue;
                    updated.Add(output.ID);
                    DataStorage.GetInstance.Remove(output);


                    foreach (var kvp in keyValSet)
                    {
                        if (output.fieldSetters.TryGetValue(kvp.Key, out Action<IComparable> setter))
                        {
                            setter(kvp.Value);
                        }
                    }
                    DataStorage.GetInstance.Add(output);

                }
            }

            

            Console.WriteLine($"Updated {updated.Count} {objectClass}s");

            return true;
        }

    }
}
