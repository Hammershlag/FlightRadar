using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.entities;

namespace OOD_24L_01180689.src.console.commands.command;

public class RemoveCommand : Command
{
    private readonly ConditionsList conditionsList;
    private readonly string objectClass;

    public RemoveCommand(string objectClass, ConditionsList conditionsList)
    {
        this.conditionsList = conditionsList == null ? new ConditionsList() : conditionsList;
        this.objectClass = objectClass;
    }

    public override bool Execute()
    {
        if (objectClass == null)
        {
            Console.WriteLine("Invalid object class");
            return false;
        }

        var removed = new List<ulong>();

        var key = objectClass;
        var factories = Entity.entityFactories;
        var def = factories[key];
        var ent = def.Create();
        foreach (var obj in DataStorage.GetInstance.GetIDEntityMap().Values)
            if (ent.TryParse(obj, out var output))
            {
                if (!conditionsList.Check(output)) continue;
                removed.Add(output.ID);
                DataStorage.GetInstance.Remove(output);
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