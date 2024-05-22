using OOD_24L_01180689.src.console.commands;
using OOD_24L_01180689.src.console.commands.command;

namespace OOD_24L_01180689.src.factories.commandFactories;

public abstract class CommandFactory
{
    public abstract Command Create(string object_class, Dictionary<string, IComparable> key_val_set,
        List<string> object_fields, ConditionsList conditionsList);
}