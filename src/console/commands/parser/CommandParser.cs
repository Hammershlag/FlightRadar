using OOD_24L_01180689.src.console.commands.command;
using OOD_24L_01180689.src.factories.commandFactories;

namespace OOD_24L_01180689.src.console.commands.parser;

public class CommandParser : Parser
{
    private readonly ConditionParser conditionParser = new();

    private readonly Dictionary<string, CommandFactory> factories = new()
    {
        { "add", new AddCommandFactory() },
        { "display", new DisplayCommandFactory() },
        { "delete", new RemoveCommandFactory() },
        { "update", new UpdateCommandFactory() }
    };

    public override Command Parse(string command)
    {
        var parts = command.Split(' ').ToList();
        var commandType = parts[0].ToLower();

        if (factories.ContainsKey(commandType))
        {
            var factory = factories[commandType];
            parts.RemoveAt(0);

            var object_class = "";
            Dictionary<string, IComparable> key_val_set = null;
            List<string> object_fields = null;
            ConditionsList conditionsList = null;
            var testFromOneField = new List<string>
                { "Airport", "Cargo", "Flight", "Crew", "Passenger", "CargoPlane", "PassengerPlane" };

            if (!testFromOneField.Contains(parts[0]))
            {
                object_fields = parts[0].Split(',').ToList();
                parts.RemoveAt(0);
                parts.RemoveAt(0);
            }

            object_class = parts[0];
            parts.RemoveAt(0);


            while (parts.Count > 0)
                if (parts[0] == "where")
                {
                    conditionsList = ParseConditionsList(parts.Skip(1).ToArray());
                    break;
                }
                else if (parts[0] == "set" || parts[0] == "new")
                {
                    parts.RemoveAt(0);

                    key_val_set = ParseKeyValSet(parts[0]);
                    parts.RemoveAt(0);
                }

            return factory.Create(object_class, key_val_set, object_fields, conditionsList);
        }

        ConsoleHandler.DisplayHelp();

        return null;
    }

    private Dictionary<string, IComparable>? ParseKeyValSet(string part)
    {
        var pairs = part.Trim('(', ')').Split(';');

        var keyValSet = new Dictionary<string, IComparable>();

        foreach (var pair in pairs)
        {
            var keyValue = pair.Split('=');

            var key = keyValue[0];
            var value = keyValue[1];

            key = key.Trim().ToUpper();
            value = value.Trim();

            if (ulong.TryParse(value, out var ulongValue))
            {
                keyValSet[key] = ulongValue;
            }
            else if (float.TryParse(value, out var floatValue))
            {
                keyValSet[key] = floatValue;
            }
            else if (value.StartsWith("\"") && value.EndsWith("\""))
            {
                var stringValue = value.Trim('"');
                keyValSet[key] = stringValue;
            }
            else
            {
                throw new FormatException($"Invalid value format for key '{key}'.");
            }
        }

        return keyValSet;
    }


    private ConditionsList ParseConditionsList(string[] parts)
    {
        if (parts.Length == 0) return null;

        var conditionsString = string.Join(" ", parts);

        var cp = new ConditionParser();
        return cp.Parse(conditionsString);
    }
}