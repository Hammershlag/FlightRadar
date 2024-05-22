using System.Text.RegularExpressions;

namespace OOD_24L_01180689.src.console.commands.parser;

public class ConditionParser

{
    private static readonly Dictionary<string, Condition.ConditionType> ComparisonOperators = new()
    {
        { "==", Condition.ConditionType.EQUAL },
        { "!=", Condition.ConditionType.NOT_EQUAL },
        { ">", Condition.ConditionType.GREATER },
        { "<", Condition.ConditionType.LESS },
        { ">=", Condition.ConditionType.GREATER_EQUAL },
        { "<=", Condition.ConditionType.LESS_EQUAL }
    };

    public ConditionsList Parse(string conditionString)
    {
        var conditionsList = new ConditionsList();

        var parts = Regex.Split(conditionString, @"\b(and|or)\b", RegexOptions.IgnoreCase);

        foreach (var part in parts)
        {
            var trimmedPart = part.Trim();

            if (trimmedPart.Equals("or") || trimmedPart.Equals("and"))
            {
                conditionsList.AndOrs.Add(trimmedPart.StartsWith("or", StringComparison.OrdinalIgnoreCase)
                    ? ConditionsList.Conjunctions.OR
                    : ConditionsList.Conjunctions.AND);
                continue;
            }

            var condition = ParseSingleCondition(trimmedPart);
            try
            {
                conditionsList.AddCondition(condition);
            }
            catch
            {
            }
        }

        return conditionsList;
    }

    private Condition ParseSingleCondition(string conditionString)
    {
        var parts = conditionString.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 3) throw new FormatException("Invalid condition format.");

        var field = parts[0];
        var comparisonOperator = parts[1];
        var value = parts[2];

        if (!ComparisonOperators.TryGetValue(comparisonOperator, out var type))
            throw new InvalidDataException("Invalid data");

        var parsedValueRight = ParseValue(value);
        var parsedValueLeft = ParseValue(field);

        return new Condition(parsedValueLeft, parsedValueRight, type);
    }

    private IComparable ParseValue(string value)
    {
        if (ulong.TryParse(value, out var intValue)) return intValue;

        if (float.TryParse(value, out var floatValue)) return floatValue;

        return value;
    }
}