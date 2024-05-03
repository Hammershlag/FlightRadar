using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OOD_24L_01180689.src.console.commands.parser
{
    public class ConditionParser

    {

        private static readonly Dictionary<string, Condition.ConditionType> ComparisonOperators = new Dictionary<string, Condition.ConditionType>()
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
            ConditionsList conditionsList = new ConditionsList();

            // Split the condition string by "and" or "or" to separate conditions and conjunctions
            string[] parts = Regex.Split(conditionString, @"\b(and|or)\b", RegexOptions.IgnoreCase);

            foreach (string part in parts)
            {
                string trimmedPart = part.Trim();

                // Ignore "and" and "or" parts, as they represent conjunctions
                // if (trimmedPart.Equals("and", StringComparison.OrdinalIgnoreCase) ||
                //     trimmedPart.Equals("or", StringComparison.OrdinalIgnoreCase))
                // {
                //     continue;
                // }
                if (/*conditionsList.Conditions.Count > 1 &&*/ (trimmedPart.Equals("or") || trimmedPart.Equals("and")))
                {
                    conditionsList.AndOrs.Add(trimmedPart.StartsWith("or", StringComparison.OrdinalIgnoreCase)
                        ? ConditionsList.Conjunctions.OR
                        : ConditionsList.Conjunctions.AND);
                    continue;
                }
                // Parse each condition
                Condition condition = ParseSingleCondition(trimmedPart);
                try
                {
                    conditionsList.AddCondition(condition);
                }
                catch { continue;}

                // Parse the conjunction after each condition
                
            }

            return conditionsList;
        }

        private Condition ParseSingleCondition(string conditionString)
        {
            // Split the condition string by comparison operators
            string[] parts = conditionString.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 3)
            {
                throw new FormatException("Invalid condition format.");
            }

            string field = parts[0];
            string comparisonOperator = parts[1];
            string value = parts[2];

            // Parse the comparison operator
            if (!ComparisonOperators.TryGetValue(comparisonOperator, out Condition.ConditionType type))
                throw new InvalidDataException("Invalid data");

            // Parse the value to appropriate data type
            IComparable parsedValueRight = ParseValue(value);
            IComparable parsedValueLeft = ParseValue(field);

            // Create and return the condition
            return new Condition(parsedValueLeft, parsedValueRight, type);
        }

        private IComparable ParseValue(string value)
        {
            
            // Try to parse the value as int, if successful, return it
            if (ulong.TryParse(value, out ulong intValue))
            {
                return intValue;
            }

            // Try to parse the value as float, if successful, return it
            if (float.TryParse(value, out float floatValue))
            {
                return floatValue;
            }

            // Otherwise, return it as string
            return value;
        }
    }
}
