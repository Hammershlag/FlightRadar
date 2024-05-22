using OOD_24L_01180689.src.console.commands;
using OOD_24L_01180689.src.console.commands.command;

namespace OOD_24L_01180689.src.factories.commandFactories;

public class RemoveCommandFactory : CommandFactory
{
    public override Command Create(string object_class, Dictionary<string, IComparable> key_val_set,
        List<string> object_fields, ConditionsList conditionsList)
    {
        return new RemoveCommand(object_class, conditionsList);
    }
}