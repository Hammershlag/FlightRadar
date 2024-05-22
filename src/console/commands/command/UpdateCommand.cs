using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.entities;

namespace OOD_24L_01180689.src.console.commands.command;

public class UpdateCommand : Command
{
    private readonly ConditionsList conditionsList;
    private readonly Dictionary<string, IComparable> keyValSet;
    private readonly string objectClass;

    public UpdateCommand(string objectClass, Dictionary<string, IComparable> keyValSet, ConditionsList conditionsList)
    {
        this.conditionsList = conditionsList == null ? new ConditionsList() : conditionsList;
        this.keyValSet = keyValSet;
        this.objectClass = objectClass;
    }

    public override bool Execute()
    {
        if (objectClass == null)
        {
            Console.WriteLine("Invalid object class");
            return false;
        }

        var updated = new List<ulong>();

        var factories = Entity.entityFactories;

        if (!factories.ContainsKey(objectClass))
        {
            Console.WriteLine($"No factory found for {objectClass}");
            return false;
        }

        var def = factories[objectClass];
        var ent = def.Create();

        foreach (var obj in DataStorage.GetInstance.GetIDEntityMap().Values)
            if (ent.TryParse(obj, out var output))
            {
                if (!conditionsList.Check(output)) continue;
                updated.Add(output.ID);
                DataStorage.GetInstance.Remove(output);


                foreach (var kvp in keyValSet)
                    if (output.fieldSetters.TryGetValue(kvp.Key, out var setter))
                        setter(kvp.Value);
                DataStorage.GetInstance.Add(output);
            }


        Console.WriteLine($"Updated {updated.Count} {objectClass}s");

        return true;
    }
}