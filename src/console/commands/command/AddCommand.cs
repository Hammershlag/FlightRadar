using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.entities;

namespace OOD_24L_01180689.src.console.commands.command;

public class AddCommand : Command
{
    public Dictionary<string, IComparable> keyValSet = new();
    public string objectClass;


    public AddCommand(string objectClass, Dictionary<string, IComparable> keyValSet)
    {
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


        var factory = Entity.entityFactories[objectClass];
        var ent = factory.Create();

        Action<IComparable> setter;
        var keys = ent.fieldSetters.Keys.Select(key => key.ToUpper()).ToList();

        foreach (var field in keys)
        {
            setter = ent.fieldSetters[field];
            if (!keyValSet.TryGetValue(field, out var value))
                setter(null);
            else
                setter(value);
        }

        DataStorage.GetInstance.Add(ent);

        Console.WriteLine($"Added Entity: {ent}");
        return true;
    }
}